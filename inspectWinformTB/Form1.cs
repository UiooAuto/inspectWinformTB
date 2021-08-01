using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using inspectWinform;
using Newtonsoft.Json;

namespace inspectWinformTB
{
    public partial class Form1 : Form
    {
        private AllConnectData allConnectData = new AllConnectData();

        string filePath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")) +
                          "\\saveData.JSON"; //xml文件地址

        private string inspectPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")) +
                                     "\\startInspect.bat"; //批处理文件启动inspect路径

        //inspect和plc的连接信息
        ConnectInfo inspectConnectInfo;
        ConnectInfo[] plcConnectArr = new ConnectInfo[3];

        //用于存放读取结果的数组
        public byte[] resByteArr = new byte[1024];

        //用于通信的Socket
        Socket inspectSocket;
        Socket plcSocket1;

        //用于多线程执行的对象
        static Work work1 = new Work();

        //准备plc所用的各种地址
        string cam1CmdAds;
        string cam1ResAds;

        bool connectStatus = false;

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

            //取消关闭安妮
            this.ControlBox = false;
            //新建Socket连接
            inspectConnectInfo = new ConnectInfo();
            plcConnectArr[0] = new ConnectInfo();

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
                startConnect();
                minWindow();
            }
        }

        #endregion

        #region 点击连接按钮

        private void connectAll_Click(object sender, EventArgs e)
        {
            startConnect();
        }

        #endregion

        #region 连接功能

        private void startConnect()
        {
            testMsg.Text = "无";
            testMsg.BackColor = Color.Silver;
            //给PLC连接地址赋值
            if (!isEmpty(trigger1.Text) && !isEmpty(result1.Text))
            {
                cam1CmdAds = "D" + trigger1.Text + " 01";
                cam1ResAds = "D" + result1.Text + " 01";
            }
            else
            {
                cam1CmdAds = null;
                cam1ResAds = null;
            }

            //当在有链接的时候点击，需要关闭所有连接
            if ((inspectSocket != null && inspectSocket.Connected)
                || plcSocket1 != null && plcSocket1.Connected)
            {
                closeAllSocket();
                connectAll.Text = "连接";
                connectAll.BackColor = Color.Silver;
            }
            else
            {
                connectAllcon();
                startWork();
            }

            enTextBoxs();
        }

        #endregion

        #region 启动程序主要功能

        /// <summary>
        /// 启动程序主要功能
        /// </summary>
        public void startWork()
        {
            //仅当对应plc有连接的时候，才开启线程
            if (plcSocket1 != null)
            {
                work1.plcSocket = plcSocket1;
                work1.localSocket = inspectSocket;
                work1.camCmdAds = cam1CmdAds;
                work1.camResAds = cam1ResAds;
                work1.san = true;
                if (cam1En.Checked && !cam2En.Checked)
                {
                    work1.camMode = 1;
                }
                else if (!cam1En.Checked && cam2En.Checked)
                {
                    work1.camMode = 2;
                }
                else if (cam1En.Checked && cam2En.Checked)
                {
                    work1.camMode = 3;
                }

                Thread thread1 = new Thread(new ThreadStart(work1.go));
                thread1.Name = "cam1";
                thread1.Start();
            }
        }

        #endregion

        #region 连接所有连接

        /// <summary>
        /// 建立所有连接
        /// </summary>
        public void connectAllcon()
        {
            if (!cam1En.Checked && !cam2En.Checked)
            {
                MessageBox.Show("至少开启一个相机");
                return;
            }
            //用于标识是否有连接建立成功
            bool flag = false;

            if (plcSocket1 == null & !isEmpty(plcIp1.Text) & !isEmpty(plcPort1.Text))
            {
                plcConnectArr[0].ip = plcIp1.Text;
                plcConnectArr[0].port = int.Parse(plcPort1.Text);
                plcSocket1 = InspectUtilsTB.connectToTarget(plcConnectArr[0].ip, plcConnectArr[0].port);
                if (plcSocket1 != null)
                {
                    flag = true;
                }
            }

            if (flag)
            {
                //只有在有连接参数、有PLC连接成功时才连接Inspect
                if (inspectSocket == null & !isEmpty(inspectIp.Text) & !isEmpty(inspectPort.Text))
                {
                    inspectConnectInfo.ip = inspectIp.Text;
                    inspectConnectInfo.port = int.Parse(inspectPort.Text);
                    inspectSocket = InspectUtilsTB.connectToTarget(inspectConnectInfo.ip, inspectConnectInfo.port);
                }
                connectAll.Text = "关闭连接";
                connectAll.BackColor = Color.LimeGreen;
                connectStatus = true;
            }
            else
            {
                MessageBox.Show("连接失败");
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
            if (str.Length != 0 & str != null)
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
            string str = "c1;";
            testMsg.Text = "无";
            testMsg.BackColor = Color.Silver;
            InspectUtilsTB.sendCmdToTarget(inspectSocket, str);
            var receiveData = InspectUtilsTB.receiveDataFromTarget(inspectSocket, resByteArr);
            if (work1.camMode == 1) //仅开启上面的相机
            {
                if (receiveData == "2" || receiveData == "1") //上ok下ng
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0001\r\n");
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "上面胶条 OK";
                    testMsg.BackColor = Color.LimeGreen;
                }
                else
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0003\r\n"); //给PLC发送3代表上面NG
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "上面胶条 NG";
                    testMsg.BackColor = Color.Red;
                }
            }
            else if (work1.camMode == 2)
            {
                if (receiveData == "3" || receiveData == "1") //上ng下ok
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0001\r\n");
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "下面胶条 OK";
                    testMsg.BackColor = Color.LimeGreen;
                }
                else
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0002\r\n"); //2代表下面NG
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "下面胶条 NG";
                    testMsg.BackColor = Color.Red;
                }
            }
            else if (work1.camMode == 3)//上下面相机都正常工作
            {
                if (receiveData == "1") //上下都ok
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0001\r\n");
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "全部OK";
                    testMsg.BackColor = Color.LimeGreen;
                }
                else if (receiveData == "2") //上OK下NG
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0002\r\n");
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "下面胶条NG";
                    testMsg.BackColor = Color.Red;
                }
                else if (receiveData == "3") //上NG下OK
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0003\r\n");
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "上面胶条NG";
                    testMsg.BackColor = Color.Red;
                }
                else if (receiveData == "4") //上下都NG
                {
                    setPlcCmd(plcSocket1, cam1ResAds, " 0004\r\n");
                    setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
                    testMsg.Text = "全部NG";
                    testMsg.BackColor = Color.Red;
                }
            }
        }

        #endregion

        #region 关闭所有连接

        public void closeAllSocket()
        {
            if (inspectSocket != null)
            {
                InspectUtilsTB.shutDownConnect(inspectSocket);
                inspectSocket.Close();
                inspectSocket = null;
            }

            if (plcSocket1 != null)
            {
                InspectUtilsTB.shutDownConnect(plcSocket1);
                plcSocket1.Close();
                plcSocket1 = null;
            }

            work1.camMode = 0;
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

                cam1En.Enabled = false;
                cam2En.Enabled = false;
            }
            else
            {
                inspectIp.ReadOnly = false;
                inspectPort.ReadOnly = false;
                plcIp1.ReadOnly = false;
                plcPort1.ReadOnly = false;

                trigger1.ReadOnly = false;
                result1.ReadOnly = false;

                cam1En.Enabled = true;
                cam2En.Enabled = true;
            }
        }

        #endregion

        #region 退出按钮

        private void ExitButton_Click(object sender, EventArgs e)
        {
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

        #region 给plc发送信息

        private string setPlcCmd(Socket socket, string plcAddress, string setResult)
        {
            string rtn = InspectUtilsTB.sendCmdToTarget(socket, "01WWR" + plcAddress + setResult + "\r\n");
            //MessageBox.Show("01WWR" + plcAddress + setResult + "\r\n");
            return rtn;
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
}