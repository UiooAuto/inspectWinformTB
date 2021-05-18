using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace inspectWinformTB
{
    public partial class Form1 : Form
    {
        private AllConnectData allConnectData = new AllConnectData();

        string path = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")) +
                      "\\saveData.JSON"; //xml文件地址

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

            //给PLC连接地址赋值
            if (!isEmpty(trigger1.Text) && !isEmpty(result1.Text))
            {
                cam1CmdAds = "D" + trigger1.Text + " 01";
                cam1ResAds = "D" + result1.Text + " 01";
            }
        }

        #endregion

        #region 点击连接按钮

        private void connectAll_Click(object sender, EventArgs e)
        {
            //给PLC连接地址赋值
            if (!isEmpty(trigger1.Text) && !isEmpty(result1.Text))
            {
                cam1CmdAds = "D" + trigger1.Text + " 01";
                cam1ResAds = "D" + result1.Text + " 01";
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
            //用于标识是否有连接建立成功
            bool flag = false;
            //只有在有连接参数的时候才连接
            if (inspectSocket == null & !isEmpty(inspectIp.Text) & !isEmpty(inspectPort.Text))
            {
                inspectConnectInfo.ip = inspectIp.Text;
                inspectConnectInfo.port = int.Parse(inspectPort.Text);
                inspectSocket = InspectUtilsTB.connectToTarget(inspectConnectInfo.ip, inspectConnectInfo.port);
                if (inspectSocket != null)
                {
                    flag = true;
                }
            }

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
            InspectUtilsTB.sendCmdToTarget(inspectSocket, str);
            MessageBox.Show("已发送");
            var receiveData = InspectUtilsTB.receiveDataFromTarget(inspectSocket, resByteArr);
            MessageBox.Show("已接收:" + receiveData);
            if (receiveData == "1")
            {
                setPlcCmd(plcSocket1, cam1ResAds, " 0001\r\n");
            }
            else if (receiveData == "2")
            {
                setPlcCmd(plcSocket1, cam1ResAds, " 0002\r\n");
            }
            else if (receiveData == "3")
            {
                setPlcCmd(plcSocket1, cam1ResAds, " 0003\r\n");
            }
            else if (receiveData == "4")
            {
                setPlcCmd(plcSocket1, cam1ResAds, " 0004\r\n");
            }
            setPlcCmd(plcSocket1, cam1CmdAds, " 0000\r\n");
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
            DialogResult
                TS = MessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //弹出提示是否退出
            if (TS == DialogResult.Yes)
            {
                closeAllSocket();
                System.Environment.Exit(0);
            }
        }

        #endregion

        #region 保存连接数据

        public void saveDatas()
        {
            allConnectData.inspectIp = inspectIp.Text;
            allConnectData.inspectPort = inspectPort.Text;
            allConnectData.plcIp1 = plcIp1.Text;
            allConnectData.plcPort1 = plcPort1.Text;

            allConnectData.cam1CmdAds = trigger1.Text;
            allConnectData.cam1ResAds = result1.Text;

            File.WriteAllText(path, JsonConvert.SerializeObject(allConnectData));
        }

        public void readSaveData()
        {
            if (File.Exists(path))
            {
                string readAllText = File.ReadAllText(path, Encoding.UTF8);
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
                FileStream fileStream = File.Create(path);
                fileStream.Close();
            }
        }

        #endregion

        #region 给plc发送信息

        private string setPlcCmd(Socket socket, string plcAddress, string setResult)
        {
            string rtn = InspectUtilsTB.sendCmdToTarget(socket, "01WWR" + plcAddress + setResult + "\r\n");
            MessageBox.Show("01WWR" + plcAddress + setResult + "\r\n");
            return rtn;
        }

        #endregion

        private void save_Click(object sender, EventArgs e)
        {
            saveDatas();
        }
    }
}