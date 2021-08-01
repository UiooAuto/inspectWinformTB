using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;

namespace inspectWinformTB
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

        private Thread currentThread;

        public void go()
        {
            currentThread = Thread.CurrentThread;

            while (san)
            {
                for (int i = 0; i < resBytes.Length; i++)
                {
                    resBytes[i] = 0;
                }

                lock (this)
                {
                    result = "";
                    /*
                     *var plcCmdStr = InspectUtilsTB.receiveDataFromTarget(plcSocket,resBytes);
                     *var plcCmd = int.Parse(plcCmdStr);
                     */
                    /*
                     * 读取PLC命令
                     */
                    int plcCmd = getPlcCmd(plcSocket, camCmdAds);
                    if (plcCmd != 0)
                    {
                        result = readInspect(plcCmd);
                    }

                    if (camMode == 1) //仅开启上面的相机
                    {
                        if (result == "2" || result == "1") //上ok下ng
                        {
                            setPlcCmd(plcSocket, camResAds, " 0001\r\n");
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                        else
                        {
                            setPlcCmd(plcSocket, camResAds, " 0003\r\n"); //3代表上面NG
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                    }
                    else if (camMode == 2) //仅开启下面的相机
                    {
                        if (result == "3" || result == "1") //上ng下ok
                        {
                            setPlcCmd(plcSocket, camResAds, " 0001\r\n");
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                        else
                        {
                            setPlcCmd(plcSocket, camResAds, " 0002\r\n"); //2代表下面NG
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                    }
                    else if (camMode == 3)
                    {
                        if (result == "1")
                        {
                            setPlcCmd(plcSocket, camResAds, " 0001\r\n");
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                        else if (result == "2")
                        {
                            setPlcCmd(plcSocket, camResAds, " 0002\r\n");
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                        else if (result == "3")
                        {
                            setPlcCmd(plcSocket, camResAds, " 0003\r\n");
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                        else if (result == "4")
                        {
                            setPlcCmd(plcSocket, camResAds, " 0004\r\n");
                            setPlcCmd(plcSocket, camCmdAds, " 0000\r\n");
                        }
                    }

                    result = "";
                    Thread.Sleep(100);
                }
            }
        }

        private string setPlcCmd(Socket socket, string plcAddress, string setResult)
        {
            string rtn = InspectUtilsTB.sendCmdToTarget(socket, writeCmd + plcAddress + setResult);
            return rtn;
        }

        private int getPlcCmd(Socket socket, string plcAddress)
        {
            int enCamId = 0;
            InspectUtilsTB.sendCmdToTarget(socket, readCmd + plcAddress + "\r\n");
            var cmd = InspectUtilsTB.receiveDataFromTarget(socket, new byte[1024]);
            var indexOf = cmd.IndexOf('\r');
            if (indexOf != -1)
            {
                cmd = cmd.Substring(0, indexOf);
            }

            //根据读取到的触发状态和界面选择的触发模式，更新触发指示
            if ("11OK0001".Equals(cmd) && camMode == 1)
            {
                triggerState1.BackColor = Color.LimeGreen;
            }
            else if ("11OK0001".Equals(cmd) && camMode == 2)
            {
                triggerState2.BackColor = Color.LimeGreen;
            }
            else if ("11OK0001".Equals(cmd) && camMode == 3)
            {
                triggerState1.BackColor = Color.LimeGreen;
                triggerState2.BackColor = Color.LimeGreen;
            }
            else if ("11OK0000".Equals(cmd))
            {
                triggerState1.BackColor = Color.Yellow;
                triggerState2.BackColor = Color.Yellow;
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
            }
            else if (camMode == 2) //仅触发下面相机
            {
                str = "c2;";
            }
            else if (camMode == 3) //全部触发
            {
                str = "c3;";
            }

            InspectUtilsTB.sendCmdToTarget(localSocket, str);
            var receiveData = InspectUtilsTB.receiveDataFromTarget(localSocket, resBytes);
            return receiveData;
        }
    }
}