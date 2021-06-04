namespace inspectWinformTB
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupBox1.Controls.Add(this.savePath);
            this.groupBox1.Controls.Add(this.save);
            this.groupBox1.Controls.Add(this.ExitButton);
            this.groupBox1.Controls.Add(this.cmdCam1);
            this.groupBox1.Controls.Add(this.inspectPort);
            this.groupBox1.Controls.Add(this.inspectIp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(392, 675);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // savePath
            // 
            this.savePath.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.savePath.Location = new System.Drawing.Point(40, 530);
            this.savePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.savePath.Name = "savePath";
            this.savePath.Size = new System.Drawing.Size(321, 50);
            this.savePath.TabIndex = 11;
            this.savePath.Text = "打开参数保存位置";
            this.savePath.UseVisualStyleBackColor = true;
            this.savePath.Click += new System.EventHandler(this.savePath_Click);
            // 
            // save
            // 
            this.save.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.save.Location = new System.Drawing.Point(40, 478);
            this.save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(321, 50);
            this.save.TabIndex = 10;
            this.save.Text = "保存连接参数";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Red;
            this.ExitButton.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Location = new System.Drawing.Point(40, 588);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(321, 56);
            this.ExitButton.TabIndex = 9;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // cmdCam1
            // 
            this.cmdCam1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.cmdCam1.Location = new System.Drawing.Point(40, 326);
            this.cmdCam1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdCam1.Name = "cmdCam1";
            this.cmdCam1.Size = new System.Drawing.Size(321, 52);
            this.cmdCam1.TabIndex = 6;
            this.cmdCam1.Text = "相机触发测试";
            this.cmdCam1.UseVisualStyleBackColor = true;
            this.cmdCam1.Click += new System.EventHandler(this.cmdCam1_Click);
            // 
            // inspectPort
            // 
            this.inspectPort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.inspectPort.Location = new System.Drawing.Point(160, 237);
            this.inspectPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.inspectPort.Name = "inspectPort";
            this.inspectPort.Size = new System.Drawing.Size(199, 40);
            this.inspectPort.TabIndex = 5;
            this.inspectPort.Text = "5024";
            // 
            // inspectIp
            // 
            this.inspectIp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.inspectIp.Location = new System.Drawing.Point(160, 140);
            this.inspectIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.inspectIp.Name = "inspectIp";
            this.inspectIp.Size = new System.Drawing.Size(199, 40);
            this.inspectIp.TabIndex = 4;
            this.inspectIp.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label4.Location = new System.Drawing.Point(40, 231);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 48);
            this.label4.TabIndex = 3;
            this.label4.Text = "PORT:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label3.Location = new System.Drawing.Point(40, 134);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 48);
            this.label3.TabIndex = 2;
            this.label3.Text = "IP:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label1.Location = new System.Drawing.Point(70, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inspect连接";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label2.Location = new System.Drawing.Point(681, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 48);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLC连接";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label5.Location = new System.Drawing.Point(447, 176);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 48);
            this.label5.TabIndex = 4;
            this.label5.Text = "IP:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label8.Location = new System.Drawing.Point(804, 174);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 48);
            this.label8.TabIndex = 4;
            this.label8.Text = "PORT:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plcIp1
            // 
            this.plcIp1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.plcIp1.Location = new System.Drawing.Point(567, 180);
            this.plcIp1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plcIp1.Name = "plcIp1";
            this.plcIp1.Size = new System.Drawing.Size(226, 40);
            this.plcIp1.TabIndex = 6;
            this.plcIp1.Text = "192.168.0.3";
            // 
            // plcPort1
            // 
            this.plcPort1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.plcPort1.Location = new System.Drawing.Point(924, 180);
            this.plcPort1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plcPort1.Name = "plcPort1";
            this.plcPort1.Size = new System.Drawing.Size(226, 40);
            this.plcPort1.TabIndex = 9;
            this.plcPort1.Text = "12289";
            // 
            // connectAll
            // 
            this.connectAll.BackColor = System.Drawing.Color.Silver;
            this.connectAll.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.connectAll.Location = new System.Drawing.Point(460, 586);
            this.connectAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.connectAll.Name = "connectAll";
            this.connectAll.Size = new System.Drawing.Size(692, 57);
            this.connectAll.TabIndex = 14;
            this.connectAll.Text = "连接";
            this.connectAll.UseVisualStyleBackColor = false;
            this.connectAll.Click += new System.EventHandler(this.connectAll_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label11.Location = new System.Drawing.Point(459, 273);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 48);
            this.label11.TabIndex = 15;
            this.label11.Text = "触发地址：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label14.Location = new System.Drawing.Point(795, 273);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(158, 48);
            this.label14.TabIndex = 18;
            this.label14.Text = "结果地址：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trigger1
            // 
            this.trigger1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.trigger1.Location = new System.Drawing.Point(608, 273);
            this.trigger1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trigger1.Name = "trigger1";
            this.trigger1.Size = new System.Drawing.Size(186, 40);
            this.trigger1.TabIndex = 21;
            this.trigger1.Text = "6030";
            // 
            // result1
            // 
            this.result1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.result1.Location = new System.Drawing.Point(962, 273);
            this.result1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.result1.Name = "result1";
            this.result1.Size = new System.Drawing.Size(188, 40);
            this.result1.TabIndex = 24;
            this.result1.Text = "6032";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(962, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 34);
            this.label6.TabIndex = 25;
            this.label6.Text = "自动连接等待";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoConTimeSet
            // 
            this.autoConTimeSet.Location = new System.Drawing.Point(1078, 94);
            this.autoConTimeSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.autoConTimeSet.Name = "autoConTimeSet";
            this.autoConTimeSet.Size = new System.Drawing.Size(32, 28);
            this.autoConTimeSet.TabIndex = 26;
            this.autoConTimeSet.Text = "10";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1122, 92);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 34);
            this.label7.TabIndex = 27;
            this.label7.Text = "秒";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cam1En
            // 
            this.cam1En.Checked = true;
            this.cam1En.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cam1En.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.cam1En.Location = new System.Drawing.Point(447, 386);
            this.cam1En.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cam1En.Name = "cam1En";
            this.cam1En.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cam1En.Size = new System.Drawing.Size(348, 36);
            this.cam1En.TabIndex = 30;
            this.cam1En.Text = "启用上面胶条检测相机";
            this.cam1En.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cam1En.UseVisualStyleBackColor = true;
            // 
            // cam2En
            // 
            this.cam2En.Checked = true;
            this.cam2En.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cam2En.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.cam2En.Location = new System.Drawing.Point(447, 478);
            this.cam2En.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cam2En.Name = "cam2En";
            this.cam2En.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cam2En.Size = new System.Drawing.Size(348, 36);
            this.cam2En.TabIndex = 31;
            this.cam2En.Text = "启用下面胶条检测相机";
            this.cam2En.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cam2En.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(1122, 130);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 34);
            this.label19.TabIndex = 37;
            this.label19.Text = "秒";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoStartInspectTime
            // 
            this.autoStartInspectTime.Location = new System.Drawing.Point(1078, 135);
            this.autoStartInspectTime.Margin = new System.Windows.Forms.Padding(4);
            this.autoStartInspectTime.Name = "autoStartInspectTime";
            this.autoStartInspectTime.Size = new System.Drawing.Size(36, 28);
            this.autoStartInspectTime.TabIndex = 36;
            this.autoStartInspectTime.Text = "5";
            this.autoStartInspectTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(864, 130);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(222, 34);
            this.label20.TabIndex = 35;
            this.label20.Text = "自动启动Inspect等待时间";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.autoStartInspectTime);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cam2En);
            this.Controls.Add(this.cam1En);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.autoConTimeSet);
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
            this.Controls.Add(this.groupBox1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Inspect连接工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

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
        private System.Windows.Forms.TextBox textBox1;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}