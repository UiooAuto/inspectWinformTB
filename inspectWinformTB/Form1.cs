using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using inspectWinform;
using Newtonsoft.Json;

namespace InspectWinformTB
{
    public partial class Form1 : Form
    {

        public string readCmd = "01WRDD";
        public string writeCmd = "01WWRD";

        private AllConnectData allConnectData = new AllConnectData();

        string filePath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")) +
                          "\\saveData.JSON"; //xml文件地址

        private string inspectPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")) +
                                     "\\startInspect.bat"; //批处理文件启动inspect路径

        //用于存放读取结果的数组
        public byte[] resByteArr = new byte[1024];

        //用于通信的Socket
        /*Socket inspectSocket;
        Socket plcSocket1;*/

        //用于多线程执行的对象
        //static Work work1 = new Work();

        //准备plc所用的各种地址
        //string cam1CmdAds;
        //string cam1ResAds;

        bool connectStatus = false;
        public int overTimeSet;

        private Thread thread;

        public Form1()
        {
            InitializeComponent();
            init();
        }

        #region 初始化

        /// <summary>
        /// 窗口初始化
        /// </summary>
        public void init()
        {
            //查询inspect是否已启动，未启动则自动启动
            bool inspectRun = false;

            //取消关闭按钮
            this.ControlBox = false;
            //新建Socket连接

            //读取前一次的连接数据
            readSaveData();

            inspectIp.Text = allConnectData.inspectIp;
            inspectPort.Text = allConnectData.inspectPort;
            plcIp1.Text = allConnectData.plcIp1;
            plcPort1.Text = allConnectData.plcPort1;

            trigger1.Text = allConnectData.cam1CmdAds;
            result1.Text = allConnectData.cam1ResAds;

            autoConTimeSet.Text = allConnectData.autoConnTime;

            cam1En.Checked = allConnectData.cam1En;
            cam2En.Checked = allConnectData.cam2En;
            
            if (isEmpty(allConnectData.overTimeSet))
            {
                overTime.Text = "2";
            }
            else
            {
                overTime.Text = allConnectData.overTimeSet;
            }


            //创建窗口对象
            AutoConnectForm autoConnectForm = new AutoConnectForm();
            //如果文件中没有保存等待时间，则默认为10
            if (isEmpty(allConnectData.autoConnTime))
            {
                autoConnectForm.autoConnTime = "10";
            }
            else
            {
                autoConnectForm.autoConnTime = allConnectData.autoConnTime;
            }

            //运行窗口，阻塞主体程序运行
            autoConnectForm.ShowDialog();
            
            
            if (isEmpty(allConnectData.delayStartInspect))
            {
                autoStartInspectTime.Text = "5";
            }
            else
            {
                autoStartInspectTime.Text = allConnectData.delayStartInspect;
            }


            if (autoConnectForm.autoConn)
            {
                //拉取进程列表
                Process[] processes = Process.GetProcesses();
                //查找有没有inspect的进程
                foreach (Process process in processes)
                {
                    if (process.ProcessName.Equals("iworks"))
                    {
                        inspectRun = true;
                    }
                }

                //没有找到说明inspect没启动，启动inspect
                if (!inspectRun)
                {
                    Process.Start(inspectPath);
                    
                    if (isEmpty(allConnectData.delayStartInspect))
                    {
                        Thread.Sleep(5000);
                        autoStartInspectTime.Text = "5";
                    }
                    else
                    {
                        autoStartInspectTime.Text = allConnectData.delayStartInspect;
                        Thread.Sleep(int.Parse(allConnectData.delayStartInspect)*1000);
                    }
                }

                //开始连接
                bool v = startConnect();
                if (v)
                {
                    minWindow();//连接成功才会最小化
                }
                else
                {
                    MessageBox.Show("连接失败");
                }
            }
        }

        #endregion

        #region 点击连接按钮

        private void connectAll_Click(object sender, EventArgs e)
        {
            startConnect();
        }

        #endregion

        #region 重做部分

        #region 连接参数

        public Socket inspect;
        public Socket plc;

        #endregion

        #region 程序主要工作流程

        public static bool san = false;
        public static bool readByPass = false;//暂停主线程读取 = false;
        public void Work()
        {
            string lastCmd = "";
            while (san)
            {
                string cam1Result = "";
                string cam2Result = "";
                int triggerCam = 0;

                if (!readByPass)
                {
                    //读取plc
                    byte[] recBytes = new byte[1024 * 1024];
                    string cmd = readCmd + trigger1.Text + " 01\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmd));
                    int v = plc.Receive(recBytes);

                    Thread.Sleep(30);

                    string cmdString = Encoding.UTF8.GetString(recBytes, 0, v);

                    int indexOf = cmdString.IndexOf('\r');
                    if (indexOf != -1)
                    {
                        cmdString = cmdString.Substring(0, indexOf);
                    }

                    if (cmdString != lastCmd)//代表指令有更新
                    {
                        if ("11OK0000".Equals(cmdString))//0代表一个也不触发
                        {
                            triggerCam = 0;
                            trigger1State.BackColor = Color.Yellow;
                            trigger2State.BackColor = Color.Yellow;
                        }
                        else if ("11OK0001".Equals(cmdString))//发1代表全部触发
                        {
                            triggerCam = 3;
                            trigger1State.BackColor = Color.Green;
                            trigger2State.BackColor = Color.Green;
                        }
                        else if ("11OK0002".Equals(cmdString))//发2代表触发上
                        {
                            triggerCam = 1;
                            trigger1State.BackColor = Color.Green;
                        }
                        else if ("11OK0003".Equals(cmdString))//发3代表触发下
                        {
                            triggerCam = 2;
                            trigger2State.BackColor = Color.Green;
                        }
                        lastCmd = cmdString;
                    }
                    else
                    {
                        lastCmd = cmdString;
                        continue;
                    }


                    if (triggerCam == 0)//不触发需要更新标签显示并开启下一次循环
                    {
                        continue;
                    }
                    if ((triggerCam == 1) || (triggerCam == 3))//如果命令上相机检测或所有相机检测，则上相机检测
                    {
                        Array.Clear(recBytes, 0, recBytes.Length);
                        inspect.Send(Encoding.UTF8.GetBytes("c1;"));
                        int v1 = inspect.Receive(recBytes);
                        Thread.Sleep(30);
                        cam1Result = Encoding.UTF8.GetString(recBytes, 0, v1);
                    }
                    if ((triggerCam == 2) || (triggerCam == 3))//如果命令下相机检测或所有相机检测，则下相机检测
                    {
                        Array.Clear(recBytes, 0, recBytes.Length);
                        inspect.Send(Encoding.UTF8.GetBytes("c2;"));
                        int v2 = inspect.Receive(recBytes);
                        Thread.Sleep(30);
                        cam2Result = Encoding.UTF8.GetString(recBytes, 0, v2);
                    }

                    if (triggerCam == 1)
                    {
                        if ("1" == cam1Result)//只检测上面并且检测ok则返回plc1，检测NG则返回3
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0001\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                        else
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0003\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                    }
                    else if (triggerCam == 2)//只检测下面并且检测ok则返回plc1，检测NG则返回2
                    {
                        if ("1" == cam2Result)
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0001\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                        else
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0002\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                    }
                    else if (triggerCam == 3)
                    {
                        if (("1" == cam1Result) && ("1" == cam2Result))//上下全检测，全部ok
                        {
                            //需要给plc写入结果
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0001\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                        else if (("1" == cam1Result) && ("1" != cam2Result))//上下全检测，上ok下ng
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0002\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                        else if (("1" != cam1Result) && ("1" == cam2Result))//上下全检测，上ng下ok
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0003\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                        else if (("1" != cam1Result) && ("1" != cam2Result))//上下全检测，全部ng
                        {
                            Array.Clear(recBytes, 0, recBytes.Length);
                            string cmdStr = writeCmd + result1.Text + " 01 0004\r\n";
                            plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                            int v1 = plc.Receive(recBytes);
                            Thread.Sleep(30);
                        }
                    }
                    //清楚PLC的触发位
                    Array.Clear(recBytes, 0, recBytes.Length);
                    string cmdStr1 = writeCmd + trigger1.Text + " 01 0000\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr1));
                    plc.Receive(recBytes);
                    Thread.Sleep(30);
                }
            }
        }

        #endregion

        #endregion

        #region 连接功能

        private bool startConnect()
        {
            testMsg.Text = "无";
            testMsg.BackColor = Color.Silver;

            //当在有链接的时候点击，需要关闭所有连接
            if ("关闭连接".Equals(connectAll.Text))
            {
                closeAllSocket();
                connectAll.Text = "连接";
                connectAll.BackColor = Color.Silver;
                trigger1State.BackColor = Color.Silver;
                trigger2State.BackColor = Color.Silver;
            }
            else
            {
                plc = null;
                bool v = connectAllcon();
                if (v)
                {
                    startWork();
                }
                else
                {
                    return false;
                }
            }
            enTextBoxs();
            return true;
        }

        #endregion

        #region 启动程序主要功能

        /// <summary>
        /// 启动程序主要功能
        /// </summary>
        Thread cheakthread;
        public void startWork()
        {
            //仅当对应plc有连接的时候，才开启线程
            if (plc != null)
            {
                san = true;
                cheakthread = new Thread(new ThreadStart(Work));
                cheakthread.IsBackground = true;
                cheakthread.Start();
            }
        }

        #endregion

        #region 连接所有连接

        /// <summary>
        /// 建立所有连接
        /// </summary>
        public bool connectAllcon()
        {
            if (!cam1En.Checked && !cam2En.Checked)
            {
                MessageBox.Show("至少开启一个相机");
                return false;
            }
            //用于标识是否有连接建立成功
            bool flag = false;
            bool inspectRun = false;//inspect是否开启了
            Process[] processes = Process.GetProcesses();
            //查找有没有inspect的进程
            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals("iworks"))
                {
                    inspectRun = true;
                }
            }

            if (!inspectRun)
            {
                MessageBox.Show("视觉检测程序未开启");
                return false;
            }

            if (plc == null & !isEmpty(plcIp1.Text) & !isEmpty(plcPort1.Text))//连接PLC
            {
                plc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                plc .SendTimeout = 500;
                plc .ReceiveTimeout = 500;

                Ping ping = new Ping();
                PingReply pingReply = ping.Send(plcIp1.Text, 1000);
                if (pingReply.Status == IPStatus.Success)
                {
                    plc.Connect(new IPEndPoint(IPAddress.Parse(plcIp1.Text), int.Parse(plcPort1.Text)));
                    flag = true;
                }
                else
                {
                    MessageBox.Show("PLC连接超时");
                    return false;
                }
            }

            if (flag)
            {
                //只有在有连接参数、有PLC连接成功、Inspect开启时才连接Inspect
                if (inspect == null & !isEmpty(inspectIp.Text) & !isEmpty(inspectPort.Text))
                {
                    inspect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    inspect.SendTimeout = 5000;
                    inspect.ReceiveTimeout = 5000;
                    inspect.Connect(new IPEndPoint(IPAddress.Parse(inspectIp.Text), int.Parse(inspectPort.Text)));
                }
                
                testMsg.Text = "无";
                testMsg.BackColor = Color.Silver;
                connectAll.Text = "关闭连接";
                connectAll.BackColor = Color.LimeGreen;
                connectStatus = true;

                return true;
            }
            else
            {
                MessageBox.Show("连接失败");
                return false;
            }
        }

        #endregion

        #region 字符串非空

        /// <summary>
        /// 判断字符串非空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool isEmpty(string str)
        {
            if ( str != null && str.Length != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region 相机触发测试

        private void cmdCam1_Click(object sender, EventArgs e)
        {
            readByPass = true;
            Thread.Sleep(700);

            string str = "";
            string receiveData = "";
            int camMode = 0;
            if (cam1En.Checked && !cam2En.Checked)
            {
                camMode = 1;
            }
            else if (!cam1En.Checked && cam2En.Checked)
            {
                camMode = 2;
            }
            else if (cam1En.Checked && cam2En.Checked)
            {
                camMode = 3;
            }

            byte[] Bytes = new byte[2048];
            if (camMode == 1) //仅触发上面相机
            {
                Array.Clear(Bytes, 0, Bytes.Length);
                inspect.Send(Encoding.UTF8.GetBytes("c1;"));
                int v1 = inspect.Receive(Bytes);
                receiveData = Encoding.UTF8.GetString(Bytes, 0, v1);
            }
            else if (camMode == 2) //仅触发下面相机
            {
                Array.Clear(Bytes, 0, Bytes.Length);
                inspect.Send(Encoding.UTF8.GetBytes("c2;"));
                int v1 = inspect.Receive(Bytes);
                receiveData = Encoding.UTF8.GetString(Bytes, 0, v1);
            }
            else if (camMode == 3) //全部触发
            {
                Array.Clear(Bytes, 0, Bytes.Length);
                inspect.Send(Encoding.UTF8.GetBytes("c1;"));
                int v1 = inspect.Receive(Bytes);
                string receiveData1 = Encoding.UTF8.GetString(Bytes, 0, v1);

                Array.Clear(Bytes, 0, Bytes.Length);
                inspect.Send(Encoding.UTF8.GetBytes("c2;"));
                int v2 = inspect.Receive(Bytes);
                string receiveData2 = Encoding.UTF8.GetString(Bytes, 0, v2);

                if ("1".Equals(receiveData1) && "1".Equals(receiveData2))
                {
                    receiveData = "1";
                }else if ("1".Equals(receiveData1) && !"1".Equals(receiveData2))
                {
                    receiveData = "2";
                }else if (!"1".Equals(receiveData1) && "1".Equals(receiveData2))
                {
                    receiveData = "3";
                }else if (!"1".Equals(receiveData1) && !"1".Equals(receiveData2))
                {
                    receiveData = "4";
                }
            }
            
            if (camMode == 1) //仅开启上面的相机
            {
                if (receiveData == "2" || receiveData == "1") //上ok下ng
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0001\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "上面胶条OK";
                    testMsg.BackColor = Color.LimeGreen;
                }
                else
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0003\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "上面胶条NG";
                    testMsg.BackColor = Color.Red;
                }
            }
            else if (camMode == 2) //仅开启下面的相机
            {
                if (receiveData == "3" || receiveData == "1") //上ng下ok
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0001\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "下面胶条OK";
                    testMsg.BackColor = Color.LimeGreen;
                }
                else
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0002\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "下面胶条NG";
                    testMsg.BackColor = Color.Red;
                }
            }
            else if (camMode == 3)
            {
                if (receiveData == "1")
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0001\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "全部OK";
                    testMsg.BackColor = Color.LimeGreen;
                }
                else if (receiveData == "2")
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0002\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "下面胶条NG";
                    testMsg.BackColor = Color.Red;
                }
                else if (receiveData == "3")
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0003\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "上面胶条NG";
                    testMsg.BackColor = Color.Red;
                }
                else if (receiveData == "4")
                {
                    Array.Clear(Bytes, 0, Bytes.Length);
                    string cmdStr = writeCmd + result1.Text + " 01 0004\r\n";
                    plc.Send(Encoding.UTF8.GetBytes(cmdStr));
                    int v1 = plc.Receive(Bytes);

                    testMsg.Text = "全部NG";
                    testMsg.BackColor = Color.Red;
                }
            }
            Array.Clear(Bytes, 0, Bytes.Length);
            string cmdStr1 = writeCmd + trigger1.Text + " 01 0000\r\n";
            plc.Send(Encoding.UTF8.GetBytes(cmdStr1));
            plc.Receive(Bytes);

            readByPass = false;
        }

        #endregion
        
        #region 自动检测连接状态并更新界面

        public void AutoCheckConnect()
        {
            while (true)
            {
                if (plc != null)
                {
                    if (!plc.Connected)
                    {
                        startConnect();
                        testMsg.Text = "连接中断";
                        testMsg.BackColor = Color.Red;
                        return;
                    }
                }
            }
        }

        #endregion

        #region 关闭所有连接

        public void closeAllSocket()
        {
            san = false;

            Thread.Sleep(500);
            cheakthread.Abort();

            if (inspect != null)
            {
                try
                {
                    inspect.Shutdown(SocketShutdown.Both);
                }
                catch
                {
                }
                try
                {
                    inspect.Close();
                }
                catch
                {
                }
            }

            if (plc != null)
            {
                try
                {
                    plc.Shutdown(SocketShutdown.Both);
                }
                catch
                {
                }
                try
                {
                    plc.Close();
                }
                catch
                {
                }
            }
            try
            {
                //((IDisposable)this).Dispose();
            }
            catch
            {
            }

            connectStatus = false;
        }

        #endregion

        #region 根据连接状态禁用或启用输入框

        public void enTextBoxs()
        {
            if (connectStatus)
            {
                inspectIp.ReadOnly = true;
                inspectPort.ReadOnly = true;
                plcIp1.ReadOnly = true;
                plcPort1.ReadOnly = true;

                trigger1.ReadOnly = true;
                result1.ReadOnly = true;
            }
            else
            {
                inspectIp.ReadOnly = false;
                inspectPort.ReadOnly = false;
                plcIp1.ReadOnly = false;
                plcPort1.ReadOnly = false;

                trigger1.ReadOnly = false;
                result1.ReadOnly = false;
            }
        }

        #endregion

        #region 退出按钮

        private void ExitButton_Click(object sender, EventArgs e)
        {
            san = false;
            Thread.Sleep(30);
            DialogResult
                TS = MessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //弹出提示是否退出
            if (TS == DialogResult.Yes)
            {
                closeAllSocket();
                System.Environment.Exit(0);
            }
        }

        #endregion

        #region 保存、读取连接数据

        public void saveDatas()
        {
            allConnectData.inspectIp = inspectIp.Text;
            allConnectData.inspectPort = inspectPort.Text;
            allConnectData.plcIp1 = plcIp1.Text;
            allConnectData.plcPort1 = plcPort1.Text;

            allConnectData.cam1CmdAds = trigger1.Text;
            allConnectData.cam1ResAds = result1.Text;

            allConnectData.autoConnTime = autoConTimeSet.Text;

            allConnectData.cam1En = cam1En.Checked;
            allConnectData.cam2En = cam2En.Checked;
            
            allConnectData.delayStartInspect = autoStartInspectTime.Text;
            allConnectData.overTimeSet = overTime.Text;

            File.WriteAllText(filePath, JsonConvert.SerializeObject(allConnectData));
        }

        public void readSaveData()
        {
            if (File.Exists(filePath))
            {
                string readAllText = File.ReadAllText(filePath, Encoding.UTF8);
                if (!isEmpty(readAllText))
                {
                    try
                    {
                        allConnectData = JsonConvert.DeserializeObject<AllConnectData>(readAllText);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("读取存档失败");
                        allConnectData = new AllConnectData();
                        saveDatas();
                    }
                }
            }
            else
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }
        }

        #endregion

        private void save_Click(object sender, EventArgs e)
        {
            saveDatas();
        }

        private void savePath_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + filePath);
        }

         #region 手动打开Inspect

        private void handStartInspect_Click(object sender, EventArgs e)
        {
            bool inspectRun = false;
            //拉取进程列表
            Process[] processes = Process.GetProcesses();
            //查找有没有inspect的进程
            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals("iworks"))
                {
                    inspectRun = true;
                }
            }

            //没有找到说明inspect没启动，启动inspect
            if (!inspectRun)
            {
                Process.Start(inspectPath);

                if (isEmpty(allConnectData.delayStartInspect))
                {
                    Thread.Sleep(5000);
                    autoStartInspectTime.Text = "5";
                }
                else
                {
                    autoStartInspectTime.Text = allConnectData.delayStartInspect;
                    Thread.Sleep(int.Parse(allConnectData.delayStartInspect) * 1000);
                }
            }
            else
            {
                MessageBox.Show("Inspect已启动");
            }
        }

        #endregion

        #region 点击系统托盘图标，还原程序窗口

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        #endregion

        #region 将程序最小化到托盘

        private void minWindow()
        {
            if (WindowState == FormWindowState.Normal)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Minimized;
                //任务栏区显示图标
                this.ShowInTaskbar = false;
                //托盘区图标隐藏
                notifyIcon1.Visible = true;
            }
        }

        #endregion

        #region 点击最小化按钮事件

        private void minForm_Click(object sender, EventArgs e)
        {
            minWindow();
        }

        #endregion
    }
    #region 圆形标签类
    public class CircleLabel : Label//继承标签类    重新生成解决方案就能看见我啦
    {
        protected override void OnPaint(PaintEventArgs e)//重新设置控件的形状   protected 保护  override重新
        {
            base.OnPaint(e);//递归  每次重新都发生此方法,保证其形状为自定义形状
            System.Drawing.Drawing2D.GraphicsPath path =new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(2, 2,this.Width - 6,this.Height - 6);
            //Graphics g = e.Graphics;
            //g.DrawEllipse(new Pen(Color.Red, 2), 2, 2, Width - 6, Height - 6);
            Region =new Region(path);
        }
    }
 
    #endregion
}