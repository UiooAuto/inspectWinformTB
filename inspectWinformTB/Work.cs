using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace InspectWinformTB
{
    class Work
    {
        public Socket plcSocket;
        public Socket localSocket;
        public string readCmd = "01WRD";
        public string writeCmd = "01WWR";
        public string camCmdAds;
        public string camResAds;
        public byte[] resBytes = new byte[1024];
        public string result;
        public bool san = true;
        private string lastCmd = "";
        public int camMode;

        public Label triggerState1;
        public Label triggerState2;
        public Label triggerState3;

        private Thread currentThread;

        private bool inspectOK;

        private Timer timer;
        private int plcCmd;

        public void go()
        {
            currentThread = Thread.CurrentThread;
            
            timer = new Timer();
            timer.Interval = 1000;
            //timer.Enabled = true;
            timer.AutoReset = false;
            timer.Elapsed += timer_Tick;
            while (san)
            {
                for (int i = 0; i < resBytes.Length; i++)
                {
                    resBytes[i] = 0;
                }

                lock (this)
                {
                    result = "";
                    int plcCmd = getPlcCmd(plcSocket, camCmdAds);
                    if (plcCmd != 0)
                    {
                        triggerState3.BackColor = Color.Silver;
                        triggerState3.Text = "无";
                        result = readInspect(plcCmd);
                    }

                    if (!string.IsNullOrEmpty(result) && inspectOK)
                    {
                        inspectOK = false;
                        timer.Stop();
                        timer.Enabled = false;
                        resSender();
                    }

                    result = "";
                    Thread.Sleep(100);
                }
            }
        }

        private void resSender()
        {
            if (!string.IsNullOrEmpty(result) && !"Receive_Fail".Equals(result))
            {
                if (camMode == 1) //仅开启上面的相机
                {
                    if (result == "2" || result == "1") //上ok下ng
                    {
                        setPlcCmd(plcSocket, camResAds, " 0001\r\n");
                        triggerState3.Text = "上面胶条OK";
                        triggerState3.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        setPlcCmd(plcSocket, camResAds, " 0003\r\n"); //3代表上面NG
                        triggerState3.Text = "上面胶条NG";
                        triggerState3.BackColor = Color.Red;
                    }
                }
                else if (camMode == 2) //仅开启下面的相机
                {
                    if (result == "3" || result == "1") //上ng下ok
                    {
                        setPlcCmd(plcSocket, camResAds, " 0001\r\n");
                        triggerState3.Text = "下面胶条OK";
                        triggerState3.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        setPlcCmd(plcSocket, camResAds, " 0002\r\n"); //2代表下面NG
                        triggerState3.Text = "下面胶条NG";
                        triggerState3.BackColor = Color.Red;
                    }
                }
                else if (camMode == 3)
                {
                    if (result == "1")
                    {
                        setPlcCmd(plcSocket, camResAds, " 0001\r\n");
                        triggerState3.Text = "全部OK";
                        triggerState3.BackColor = Color.LimeGreen;
                    }
                    else if (result == "2")
                    {
                        setPlcCmd(plcSocket, camResAds, " 0002\r\n");
                        triggerState3.Text = "下面胶条NG";
                        triggerState3.BackColor = Color.Red;
                    }
                    else if (result == "3")
                    {
                        setPlcCmd(plcSocket, camResAds, " 0003\r\n");
                        triggerState3.Text = "上面胶条NG";
                        triggerState3.BackColor = Color.Red;
                    }
                    else if (result == "4")
                    {
                        setPlcCmd(plcSocket, camResAds, " 0004\r\n");
                        triggerState3.Text = "全部NG";
                        triggerState3.BackColor = Color.Red;
                    }
                }
            }

            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
        }

        private string setPlcCmd(Socket socket, string plcAddress, string setResult)
        {
            string rtn = SocketUtilsTB.sendCmdToTarget(socket, writeCmd + plcAddress + setResult);
            return rtn;
        }

        private int getPlcCmd(Socket socket, string plcAddress)
        {
            int enCamId = 0;
            SocketUtilsTB.sendCmdToTarget(socket, readCmd + plcAddress + "\r\n");
            var cmd = SocketUtilsTB.receiveDataFromTarget(socket, new byte[1024]);
            var indexOf = cmd.IndexOf('\r');
            if (indexOf != -1)
            {
                cmd = cmd.Substring(0, indexOf);
            }

            //根据读取到的触发状态和界面选择的触发模式，更新触发指示
            if ("11OK0001".Equals(cmd) && camMode == 1)
            {
                triggerState1.BackColor = Color.LimeGreen;
                inspectOK = true; //开启触发超时定时器
                timer.Start();
            }
            else if ("11OK0001".Equals(cmd) && camMode == 2)
            {
                triggerState2.BackColor = Color.LimeGreen;
                inspectOK = true; //开启触发超时定时器
                timer.Start();
            }
            else if ("11OK0001".Equals(cmd) && camMode == 3)
            {
                triggerState1.BackColor = Color.LimeGreen;
                triggerState2.BackColor = Color.LimeGreen;
                inspectOK = true; //开启触发超时定时器
                timer.Start();
            }
            else if ("11OK0000".Equals(cmd))
            {
                triggerState1.BackColor = Color.Yellow;
                triggerState2.BackColor = Color.Yellow;
                inspectOK = false; //关闭触发超时定时器
                timer.Stop();
                timer.Enabled = false;
            }

            if (cmd == "11OK0001" && cmd != lastCmd)
            {
                Console.WriteLine(Thread.CurrentThread.Name + "触发");
                enCamId = 1;
            }

            lastCmd = (string) cmd.Clone();
            return enCamId;
        }

        private string readInspect(int camId)
        {
            //根据界面的复选框选择触发模式
            string str = "";
            if (camMode == 1) //仅触发上面相机
            {
                str = "c1;";
                SocketUtilsTB.sendCmdToTarget(localSocket, str);
                var receiveData = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                if (!string.IsNullOrEmpty(receiveData) && !"Receive_Fail".Equals(receiveData))
                {
                    return receiveData;
                }
                else
                {
                    return "";
                }
            }
            else if (camMode == 2) //仅触发下面相机
            {
                str = "c2;";
                SocketUtilsTB.sendCmdToTarget(localSocket, str);
                var receiveData = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                if (!string.IsNullOrEmpty(receiveData) && !"Receive_Fail".Equals(receiveData))
                {
                    return receiveData;
                }
                else
                {
                    return "";
                }
            }
            else if (camMode == 3) //全部触发
            {
                str = "c1;";
                SocketUtilsTB.sendCmdToTarget(localSocket, str);
                var receiveData1 = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                Array.Clear(resBytes, 0, resBytes.Length);
                str = "c2;";
                SocketUtilsTB.sendCmdToTarget(localSocket, str);
                var receiveData2 = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                if (!string.IsNullOrEmpty(receiveData1) && !string.IsNullOrEmpty(receiveData2) &&
                    !"Receive_Fail".Equals(receiveData1) && !"Receive_Fail".Equals(receiveData2))
                {
                    if ("1".Equals(receiveData1) && "1".Equals(receiveData2))
                    {
                        return "1";
                    }

                    if ("1".Equals(receiveData1) && !"1".Equals(receiveData2))
                    {
                        return "2";
                    }

                    if (!"1".Equals(receiveData1) && "1".Equals(receiveData2))
                    {
                        return "3";
                    }

                    if (!"1".Equals(receiveData1) && !"1".Equals(receiveData2))
                    {
                        return "4";
                    }
                }
            }

            return "";
        }

        #region 收到触发信号后超时

        private void timer_Tick(object sender, EventArgs e)
        {

            if (inspectOK)
            {
                triggerState3.BackColor = Color.Blue;
                //根据界面的复选框选择触发模式
                string str = "";
                if (camMode == 1) //仅触发上面相机
                {
                    str = "c1;";
                    SocketUtilsTB.sendCmdToTarget(localSocket, str);
                    result = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                }
                else if (camMode == 2) //仅触发下面相机
                {
                    str = "c2;";
                    SocketUtilsTB.sendCmdToTarget(localSocket, str);
                    result = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                }
                else if (camMode == 3) //全部触发
                {
                    str = "c1;";
                    SocketUtilsTB.sendCmdToTarget(localSocket, str);
                    var receiveData1 = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                    Array.Clear(resBytes, 0, resBytes.Length);
                    str = "c2;";
                    SocketUtilsTB.sendCmdToTarget(localSocket, str);
                    var receiveData2 = SocketUtilsTB.receiveDataFromTarget(localSocket, resBytes);
                    if (!string.IsNullOrEmpty(receiveData1) && !string.IsNullOrEmpty(receiveData2) &&
                        !"Receive_Fail".Equals(receiveData1) && !"Receive_Fail".Equals(receiveData2))
                    {
                        if ("1".Equals(receiveData1) && "1".Equals(receiveData2))
                        {
                            result = "1";
                        }

                        if ("1".Equals(receiveData1) && !"1".Equals(receiveData2))
                        {
                            result = "2";
                        }

                        if (!"1".Equals(receiveData1) && "1".Equals(receiveData2))
                        {
                            result = "3";
                        }

                        if (!"1".Equals(receiveData1) && !"1".Equals(receiveData2))
                        {
                            result = "4";
                        }
                    }
                }

                resSender();
                setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                result = "";
            }

            inspectOK = false;
        }

        #endregion
    }
}