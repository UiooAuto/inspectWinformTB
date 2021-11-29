namespace InspectWinformTB
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
	        this.components = new System.ComponentModel.Container();
	        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
	        this.savePath = new System.Windows.Forms.Button();
	        this.save = new System.Windows.Forms.Button();
	        this.ExitButton = new System.Windows.Forms.Button();
	        this.cmdCam1 = new System.Windows.Forms.Button();
	        this.inspectPort = new System.Windows.Forms.TextBox();
	        this.inspectIp = new System.Windows.Forms.TextBox();
	        this.label4 = new System.Windows.Forms.Label();
	        this.label3 = new System.Windows.Forms.Label();
	        this.label1 = new System.Windows.Forms.Label();
	        this.label2 = new System.Windows.Forms.Label();
	        this.label5 = new System.Windows.Forms.Label();
	        this.label8 = new System.Windows.Forms.Label();
	        this.plcIp1 = new System.Windows.Forms.TextBox();
	        this.plcPort1 = new System.Windows.Forms.TextBox();
	        this.connectAll = new System.Windows.Forms.Button();
	        this.label11 = new System.Windows.Forms.Label();
	        this.label14 = new System.Windows.Forms.Label();
	        this.trigger1 = new System.Windows.Forms.TextBox();
	        this.result1 = new System.Windows.Forms.TextBox();
	        this.label6 = new System.Windows.Forms.Label();
	        this.autoConTimeSet = new System.Windows.Forms.TextBox();
	        this.label7 = new System.Windows.Forms.Label();
	        this.cam1En = new System.Windows.Forms.CheckBox();
	        this.cam2En = new System.Windows.Forms.CheckBox();
	        this.label19 = new System.Windows.Forms.Label();
	        this.autoStartInspectTime = new System.Windows.Forms.TextBox();
	        this.label20 = new System.Windows.Forms.Label();
	        this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
	        this.panel1 = new System.Windows.Forms.Panel();
	        this.groupBox1 = new System.Windows.Forms.GroupBox();
	        this.testMsg = new System.Windows.Forms.Label();
	        this.minForm = new System.Windows.Forms.Button();
	        this.handStartInspect = new System.Windows.Forms.Button();
	        this.trigger1State = new InspectWinformTB.CircleLabel();
	        this.trigger2State = new InspectWinformTB.CircleLabel();
	        this.label9 = new System.Windows.Forms.Label();
	        this.label10 = new System.Windows.Forms.Label();
	        this.overTime = new System.Windows.Forms.TextBox();
	        this.label12 = new System.Windows.Forms.Label();
	        this.label13 = new System.Windows.Forms.Label();
	        this.panel1.SuspendLayout();
	        this.groupBox1.SuspendLayout();
	        this.SuspendLayout();
	        // 
	        // savePath
	        // 
	        this.savePath.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.savePath.Location = new System.Drawing.Point(39, 311);
	        this.savePath.Name = "savePath";
	        this.savePath.Size = new System.Drawing.Size(214, 33);
	        this.savePath.TabIndex = 11;
	        this.savePath.Text = "打开参数保存位置";
	        this.savePath.UseVisualStyleBackColor = true;
	        this.savePath.Click += new System.EventHandler(this.savePath_Click);
	        // 
	        // save
	        // 
	        this.save.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.save.Location = new System.Drawing.Point(39, 277);
	        this.save.Name = "save";
	        this.save.Size = new System.Drawing.Size(214, 33);
	        this.save.TabIndex = 10;
	        this.save.Text = "保存连接参数";
	        this.save.UseVisualStyleBackColor = true;
	        this.save.Click += new System.EventHandler(this.save_Click);
	        // 
	        // ExitButton
	        // 
	        this.ExitButton.BackColor = System.Drawing.Color.Red;
	        this.ExitButton.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.ExitButton.ForeColor = System.Drawing.Color.White;
	        this.ExitButton.Location = new System.Drawing.Point(39, 403);
	        this.ExitButton.Name = "ExitButton";
	        this.ExitButton.Size = new System.Drawing.Size(214, 37);
	        this.ExitButton.TabIndex = 9;
	        this.ExitButton.Text = "Exit";
	        this.ExitButton.UseVisualStyleBackColor = false;
	        this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
	        // 
	        // cmdCam1
	        // 
	        this.cmdCam1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.cmdCam1.Location = new System.Drawing.Point(39, 181);
	        this.cmdCam1.Name = "cmdCam1";
	        this.cmdCam1.Size = new System.Drawing.Size(214, 33);
	        this.cmdCam1.TabIndex = 6;
	        this.cmdCam1.Text = "相机触发测试";
	        this.cmdCam1.UseVisualStyleBackColor = true;
	        this.cmdCam1.Click += new System.EventHandler(this.cmdCam1_Click);
	        // 
	        // inspectPort
	        // 
	        this.inspectPort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.inspectPort.Location = new System.Drawing.Point(119, 126);
	        this.inspectPort.Name = "inspectPort";
	        this.inspectPort.Size = new System.Drawing.Size(134, 29);
	        this.inspectPort.TabIndex = 5;
	        this.inspectPort.Text = "5024";
	        // 
	        // inspectIp
	        // 
	        this.inspectIp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.inspectIp.Location = new System.Drawing.Point(119, 90);
	        this.inspectIp.Name = "inspectIp";
	        this.inspectIp.Size = new System.Drawing.Size(134, 29);
	        this.inspectIp.TabIndex = 4;
	        this.inspectIp.Text = "127.0.0.1";
	        // 
	        // label4
	        // 
	        this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label4.Location = new System.Drawing.Point(39, 126);
	        this.label4.Name = "label4";
	        this.label4.Size = new System.Drawing.Size(74, 32);
	        this.label4.TabIndex = 3;
	        this.label4.Text = "PORT:";
	        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // label3
	        // 
	        this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label3.Location = new System.Drawing.Point(39, 86);
	        this.label3.Name = "label3";
	        this.label3.Size = new System.Drawing.Size(74, 32);
	        this.label3.TabIndex = 2;
	        this.label3.Text = "IP:";
	        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // label1
	        // 
	        this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label1.Location = new System.Drawing.Point(31, 9);
	        this.label1.Name = "label1";
	        this.label1.Size = new System.Drawing.Size(222, 32);
	        this.label1.TabIndex = 0;
	        this.label1.Text = "检测程序连接";
	        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // label2
	        // 
	        this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label2.Location = new System.Drawing.Point(454, 27);
	        this.label2.Name = "label2";
	        this.label2.Size = new System.Drawing.Size(165, 32);
	        this.label2.TabIndex = 1;
	        this.label2.Text = "PLC连接";
	        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // label5
	        // 
	        this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label5.Location = new System.Drawing.Point(315, 117);
	        this.label5.Name = "label5";
	        this.label5.Size = new System.Drawing.Size(74, 32);
	        this.label5.TabIndex = 4;
	        this.label5.Text = "IP:";
	        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        // 
	        // label8
	        // 
	        this.label8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label8.Location = new System.Drawing.Point(536, 116);
	        this.label8.Name = "label8";
	        this.label8.Size = new System.Drawing.Size(74, 32);
	        this.label8.TabIndex = 4;
	        this.label8.Text = "PORT:";
	        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // plcIp1
	        // 
	        this.plcIp1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.plcIp1.Location = new System.Drawing.Point(378, 120);
	        this.plcIp1.Name = "plcIp1";
	        this.plcIp1.Size = new System.Drawing.Size(152, 29);
	        this.plcIp1.TabIndex = 6;
	        this.plcIp1.Text = "192.168.0.3";
	        // 
	        // plcPort1
	        // 
	        this.plcPort1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.plcPort1.Location = new System.Drawing.Point(616, 120);
	        this.plcPort1.Name = "plcPort1";
	        this.plcPort1.Size = new System.Drawing.Size(152, 29);
	        this.plcPort1.TabIndex = 9;
	        this.plcPort1.Text = "12289";
	        // 
	        // connectAll
	        // 
	        this.connectAll.BackColor = System.Drawing.Color.Silver;
	        this.connectAll.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.connectAll.Location = new System.Drawing.Point(307, 391);
	        this.connectAll.Name = "connectAll";
	        this.connectAll.Size = new System.Drawing.Size(461, 38);
	        this.connectAll.TabIndex = 14;
	        this.connectAll.Text = "连接";
	        this.connectAll.UseVisualStyleBackColor = false;
	        this.connectAll.Click += new System.EventHandler(this.connectAll_Click);
	        // 
	        // label11
	        // 
	        this.label11.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label11.Location = new System.Drawing.Point(315, 182);
	        this.label11.Name = "label11";
	        this.label11.Size = new System.Drawing.Size(105, 32);
	        this.label11.TabIndex = 15;
	        this.label11.Text = "触发地址：";
	        this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        // 
	        // label14
	        // 
	        this.label14.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.label14.Location = new System.Drawing.Point(530, 182);
	        this.label14.Name = "label14";
	        this.label14.Size = new System.Drawing.Size(105, 32);
	        this.label14.TabIndex = 18;
	        this.label14.Text = "结果地址：";
	        this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // trigger1
	        // 
	        this.trigger1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.trigger1.Location = new System.Drawing.Point(405, 182);
	        this.trigger1.Name = "trigger1";
	        this.trigger1.Size = new System.Drawing.Size(125, 29);
	        this.trigger1.TabIndex = 21;
	        this.trigger1.Text = "6030";
	        // 
	        // result1
	        // 
	        this.result1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.result1.Location = new System.Drawing.Point(641, 182);
	        this.result1.Name = "result1";
	        this.result1.Size = new System.Drawing.Size(127, 29);
	        this.result1.TabIndex = 24;
	        this.result1.Text = "6032";
	        // 
	        // label6
	        // 
	        this.label6.Location = new System.Drawing.Point(645, 62);
	        this.label6.Name = "label6";
	        this.label6.Size = new System.Drawing.Size(78, 23);
	        this.label6.TabIndex = 25;
	        this.label6.Text = "自动连接等待";
	        this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
	        // 
	        // autoConTimeSet
	        // 
	        this.autoConTimeSet.Location = new System.Drawing.Point(723, 63);
	        this.autoConTimeSet.Name = "autoConTimeSet";
	        this.autoConTimeSet.Size = new System.Drawing.Size(23, 21);
	        this.autoConTimeSet.TabIndex = 26;
	        this.autoConTimeSet.Text = "10";
	        this.autoConTimeSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
	        // 
	        // label7
	        // 
	        this.label7.Location = new System.Drawing.Point(748, 62);
	        this.label7.Name = "label7";
	        this.label7.Size = new System.Drawing.Size(20, 23);
	        this.label7.TabIndex = 27;
	        this.label7.Text = "秒";
	        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        // 
	        // cam1En
	        // 
	        this.cam1En.Checked = true;
	        this.cam1En.CheckState = System.Windows.Forms.CheckState.Checked;
	        this.cam1En.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.cam1En.Location = new System.Drawing.Point(315, 257);
	        this.cam1En.Name = "cam1En";
	        this.cam1En.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
	        this.cam1En.Size = new System.Drawing.Size(232, 24);
	        this.cam1En.TabIndex = 30;
	        this.cam1En.Text = "测试上面胶条检测相机";
	        this.cam1En.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
	        this.cam1En.UseVisualStyleBackColor = true;
	        // 
	        // cam2En
	        // 
	        this.cam2En.Checked = true;
	        this.cam2En.CheckState = System.Windows.Forms.CheckState.Checked;
	        this.cam2En.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.cam2En.Location = new System.Drawing.Point(315, 319);
	        this.cam2En.Name = "cam2En";
	        this.cam2En.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
	        this.cam2En.Size = new System.Drawing.Size(232, 24);
	        this.cam2En.TabIndex = 31;
	        this.cam2En.Text = "测试下面胶条检测相机";
	        this.cam2En.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
	        this.cam2En.UseVisualStyleBackColor = true;
	        // 
	        // label19
	        // 
	        this.label19.Location = new System.Drawing.Point(748, 89);
	        this.label19.Name = "label19";
	        this.label19.Size = new System.Drawing.Size(20, 23);
	        this.label19.TabIndex = 37;
	        this.label19.Text = "秒";
	        this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        // 
	        // autoStartInspectTime
	        // 
	        this.autoStartInspectTime.Location = new System.Drawing.Point(723, 90);
	        this.autoStartInspectTime.Name = "autoStartInspectTime";
	        this.autoStartInspectTime.Size = new System.Drawing.Size(23, 21);
	        this.autoStartInspectTime.TabIndex = 36;
	        this.autoStartInspectTime.Text = "5";
	        this.autoStartInspectTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
	        // 
	        // label20
	        // 
	        this.label20.Location = new System.Drawing.Point(569, 89);
	        this.label20.Name = "label20";
	        this.label20.Size = new System.Drawing.Size(154, 23);
	        this.label20.TabIndex = 35;
	        this.label20.Text = "自动启动检测程序等待时间";
	        this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
	        // 
	        // notifyIcon1
	        // 
	        this.notifyIcon1.Text = "12";
	        this.notifyIcon1.Visible = true;
	        this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
	        // 
	        // panel1
	        // 
	        this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
	        this.panel1.Controls.Add(this.groupBox1);
	        this.panel1.Controls.Add(this.minForm);
	        this.panel1.Controls.Add(this.handStartInspect);
	        this.panel1.Controls.Add(this.inspectIp);
	        this.panel1.Controls.Add(this.savePath);
	        this.panel1.Controls.Add(this.label1);
	        this.panel1.Controls.Add(this.label3);
	        this.panel1.Controls.Add(this.save);
	        this.panel1.Controls.Add(this.label4);
	        this.panel1.Controls.Add(this.inspectPort);
	        this.panel1.Controls.Add(this.ExitButton);
	        this.panel1.Controls.Add(this.cmdCam1);
	        this.panel1.Location = new System.Drawing.Point(0, 0);
	        this.panel1.Name = "panel1";
	        this.panel1.Size = new System.Drawing.Size(279, 498);
	        this.panel1.TabIndex = 38;
	        // 
	        // groupBox1
	        // 
	        this.groupBox1.Controls.Add(this.testMsg);
	        this.groupBox1.Location = new System.Drawing.Point(43, 219);
	        this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
	        this.groupBox1.Name = "groupBox1";
	        this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
	        this.groupBox1.Size = new System.Drawing.Size(210, 50);
	        this.groupBox1.TabIndex = 15;
	        this.groupBox1.TabStop = false;
	        this.groupBox1.Text = "测试结果";
	        // 
	        // testMsg
	        // 
	        this.testMsg.BackColor = System.Drawing.Color.Silver;
	        this.testMsg.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.testMsg.Location = new System.Drawing.Point(11, 12);
	        this.testMsg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        this.testMsg.Name = "testMsg";
	        this.testMsg.Size = new System.Drawing.Size(187, 32);
	        this.testMsg.TabIndex = 0;
	        this.testMsg.Text = "无";
	        this.testMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	        // 
	        // minForm
	        // 
	        this.minForm.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.minForm.Location = new System.Drawing.Point(39, 364);
	        this.minForm.Name = "minForm";
	        this.minForm.Size = new System.Drawing.Size(214, 33);
	        this.minForm.TabIndex = 13;
	        this.minForm.Text = "最小化到系统托盘";
	        this.minForm.UseVisualStyleBackColor = true;
	        this.minForm.Click += new System.EventHandler(this.minForm_Click);
	        // 
	        // handStartInspect
	        // 
	        this.handStartInspect.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	        this.handStartInspect.Location = new System.Drawing.Point(39, 51);
	        this.handStartInspect.Name = "handStartInspect";
	        this.handStartInspect.Size = new System.Drawing.Size(214, 33);
	        this.handStartInspect.TabIndex = 12;
	        this.handStartInspect.Text = "手动启动检测程序";
	        this.handStartInspect.UseVisualStyleBackColor = true;
	        this.handStartInspect.Click += new System.EventHandler(this.handStartInspect_Click);
	        // 
	        // trigger1State
	        // 
	        this.trigger1State.BackColor = System.Drawing.Color.Silver;
	        this.trigger1State.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
	        this.trigger1State.Location = new System.Drawing.Point(747, 259);
	        this.trigger1State.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        this.trigger1State.Name = "trigger1State";
	        this.trigger1State.Size = new System.Drawing.Size(21, 21);
	        this.trigger1State.TabIndex = 39;
	        // 
	        // trigger2State
	        // 
	        this.trigger2State.BackColor = System.Drawing.Color.Silver;
	        this.trigger2State.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
	        this.trigger2State.Location = new System.Drawing.Point(747, 321);
	        this.trigger2State.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        this.trigger2State.Name = "trigger2State";
	        this.trigger2State.Size = new System.Drawing.Size(21, 21);
	        this.trigger2State.TabIndex = 40;
	        // 
	        // label9
	        // 
	        this.label9.AutoSize = true;
	        this.label9.Font = new System.Drawing.Font("宋体", 12F);
	        this.label9.Location = new System.Drawing.Point(609, 261);
	        this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        this.label9.Name = "label9";
	        this.label9.Size = new System.Drawing.Size(136, 16);
	        this.label9.TabIndex = 41;
	        this.label9.Text = "上面相机触发状态";
	        // 
	        // label10
	        // 
	        this.label10.AutoSize = true;
	        this.label10.Font = new System.Drawing.Font("宋体", 12F);
	        this.label10.Location = new System.Drawing.Point(609, 323);
	        this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        this.label10.Name = "label10";
	        this.label10.Size = new System.Drawing.Size(136, 16);
	        this.label10.TabIndex = 42;
	        this.label10.Text = "下面相机触发状态";
	        // 
	        // overTime
	        // 
	        this.overTime.Location = new System.Drawing.Point(723, 36);
	        this.overTime.Name = "overTime";
	        this.overTime.Size = new System.Drawing.Size(23, 21);
	        this.overTime.TabIndex = 44;
	        this.overTime.Text = "2";
	        this.overTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
	        // 
	        // label12
	        // 
	        this.label12.Location = new System.Drawing.Point(748, 35);
	        this.label12.Name = "label12";
	        this.label12.Size = new System.Drawing.Size(20, 23);
	        this.label12.TabIndex = 45;
	        this.label12.Text = "秒";
	        this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        // 
	        // label13
	        // 
	        this.label13.Location = new System.Drawing.Point(645, 35);
	        this.label13.Name = "label13";
	        this.label13.Size = new System.Drawing.Size(78, 23);
	        this.label13.TabIndex = 43;
	        this.label13.Text = "触发超时";
	        this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
	        // 
	        // Form1
	        // 
	        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
	        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	        this.BackColor = System.Drawing.SystemColors.Control;
	        this.ClientSize = new System.Drawing.Size(802, 452);
	        this.Controls.Add(this.overTime);
	        this.Controls.Add(this.label12);
	        this.Controls.Add(this.label13);
	        this.Controls.Add(this.autoStartInspectTime);
	        this.Controls.Add(this.autoConTimeSet);
	        this.Controls.Add(this.label10);
	        this.Controls.Add(this.trigger2State);
	        this.Controls.Add(this.trigger1State);
	        this.Controls.Add(this.panel1);
	        this.Controls.Add(this.label19);
	        this.Controls.Add(this.label20);
	        this.Controls.Add(this.cam2En);
	        this.Controls.Add(this.cam1En);
	        this.Controls.Add(this.label7);
	        this.Controls.Add(this.label6);
	        this.Controls.Add(this.result1);
	        this.Controls.Add(this.trigger1);
	        this.Controls.Add(this.label14);
	        this.Controls.Add(this.label11);
	        this.Controls.Add(this.connectAll);
	        this.Controls.Add(this.plcPort1);
	        this.Controls.Add(this.plcIp1);
	        this.Controls.Add(this.label8);
	        this.Controls.Add(this.label5);
	        this.Controls.Add(this.label2);
	        this.Controls.Add(this.label9);
	        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
	        this.Location = new System.Drawing.Point(15, 15);
	        this.Name = "Form1";
	        this.Text = "检测程序通信工具";
	        this.panel1.ResumeLayout(false);
	        this.panel1.PerformLayout();
	        this.groupBox1.ResumeLayout(false);
	        this.ResumeLayout(false);
	        this.PerformLayout();
        }

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;

        private System.Windows.Forms.Label testMsg;
        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.Button minForm;

        private System.Windows.Forms.Button handStartInspect;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.NotifyIcon notifyIcon1;

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox autoStartInspectTime;
        private System.Windows.Forms.Label label20;

        private System.Windows.Forms.CheckBox cam1En;
        private System.Windows.Forms.CheckBox cam2En;

        private System.Windows.Forms.CheckBox checkBox2;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.Label label7;
        
        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.Button savePath;

        private System.Windows.Forms.Button save;

        private System.Windows.Forms.TextBox result1;

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox trigger1;

        private System.Windows.Forms.Label label11;

        private System.Windows.Forms.Button ExitButton;

        private System.Windows.Forms.Button cmdCam1;

        private System.Windows.Forms.TextBox inspectPort;
        private System.Windows.Forms.TextBox plcPort1;

        private System.Windows.Forms.Button connectAll;

        private System.Windows.Forms.TextBox autoConTimeSet;

        private System.Windows.Forms.TextBox plcIp1;

        private System.Windows.Forms.TextBox inspectIp;

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox overTime;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;
		private CircleLabel trigger1State;
		private CircleLabel trigger2State;

		#endregion

		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
	}
}