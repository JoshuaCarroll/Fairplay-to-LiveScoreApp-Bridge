namespace FairplayLivescoreBridge
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
            this.txtComRcvd = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ddlComPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.simulatorTimer = new System.Windows.Forms.Timer(this.components);
            this.chkSimulator = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.chkReceiving = new System.Windows.Forms.CheckBox();
            this.chkSending = new System.Windows.Forms.CheckBox();
            this.chkMonitorInput = new System.Windows.Forms.CheckBox();
            this.chkMonitorOutput = new System.Windows.Forms.CheckBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtComRcvd
            // 
            this.txtComRcvd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtComRcvd.Location = new System.Drawing.Point(17, 99);
            this.txtComRcvd.Multiline = true;
            this.txtComRcvd.Name = "txtComRcvd";
            this.txtComRcvd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComRcvd.Size = new System.Drawing.Size(698, 440);
            this.txtComRcvd.TabIndex = 6;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(728, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output";
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutput.Location = new System.Drawing.Point(733, 99);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(698, 440);
            this.txtOutput.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 599);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1443, 42);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(238, 32);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ddlComPort
            // 
            this.ddlComPort.FormattingEnabled = true;
            this.ddlComPort.Location = new System.Drawing.Point(175, 19);
            this.ddlComPort.Name = "ddlComPort";
            this.ddlComPort.Size = new System.Drawing.Size(252, 33);
            this.ddlComPort.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 25);
            this.label1.TabIndex = 13;
            this.label1.Text = "Input COM port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(728, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "IP address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(854, 19);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(174, 31);
            this.txtIpAddress.TabIndex = 15;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(1107, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 31);
            this.txtPort.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1044, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Port:";
            // 
            // simulatorTimer
            // 
            this.simulatorTimer.Tick += new System.EventHandler(this.simulatorTimer_Tick);
            // 
            // chkSimulator
            // 
            this.chkSimulator.AutoSize = true;
            this.chkSimulator.Enabled = false;
            this.chkSimulator.Location = new System.Drawing.Point(214, 554);
            this.chkSimulator.Name = "chkSimulator";
            this.chkSimulator.Size = new System.Drawing.Size(193, 29);
            this.chkSimulator.TabIndex = 18;
            this.chkSimulator.Text = "Simulator mode";
            this.chkSimulator.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(1238, 15);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(119, 39);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // chkReceiving
            // 
            this.chkReceiving.AutoSize = true;
            this.chkReceiving.Enabled = false;
            this.chkReceiving.Location = new System.Drawing.Point(17, 554);
            this.chkReceiving.Name = "chkReceiving";
            this.chkReceiving.Size = new System.Drawing.Size(139, 29);
            this.chkReceiving.TabIndex = 20;
            this.chkReceiving.Text = "Receiving";
            this.chkReceiving.UseVisualStyleBackColor = true;
            // 
            // chkSending
            // 
            this.chkSending.AutoSize = true;
            this.chkSending.Enabled = false;
            this.chkSending.Location = new System.Drawing.Point(733, 554);
            this.chkSending.Name = "chkSending";
            this.chkSending.Size = new System.Drawing.Size(123, 29);
            this.chkSending.TabIndex = 21;
            this.chkSending.Text = "Sending";
            this.chkSending.UseVisualStyleBackColor = true;
            // 
            // chkMonitorInput
            // 
            this.chkMonitorInput.AutoSize = true;
            this.chkMonitorInput.Location = new System.Drawing.Point(599, 67);
            this.chkMonitorInput.Name = "chkMonitorInput";
            this.chkMonitorInput.Size = new System.Drawing.Size(116, 29);
            this.chkMonitorInput.TabIndex = 22;
            this.chkMonitorInput.Text = "Monitor";
            this.chkMonitorInput.UseVisualStyleBackColor = true;
            this.chkMonitorInput.CheckedChanged += new System.EventHandler(this.chkMonitorInput_CheckedChanged);
            // 
            // chkMonitorOutput
            // 
            this.chkMonitorOutput.AutoSize = true;
            this.chkMonitorOutput.Location = new System.Drawing.Point(1315, 70);
            this.chkMonitorOutput.Name = "chkMonitorOutput";
            this.chkMonitorOutput.Size = new System.Drawing.Size(116, 29);
            this.chkMonitorOutput.TabIndex = 23;
            this.chkMonitorOutput.Text = "Monitor";
            this.chkMonitorOutput.UseVisualStyleBackColor = true;
            this.chkMonitorOutput.CheckedChanged += new System.EventHandler(this.chkMonitorOutput_CheckedChanged);
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(457, 15);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(110, 39);
            this.btnListen.TabIndex = 24;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 641);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.chkMonitorOutput);
            this.Controls.Add(this.chkMonitorInput);
            this.Controls.Add(this.chkSending);
            this.Controls.Add(this.chkReceiving);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.chkSimulator);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlComPort);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComRcvd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fair-Play to LiveScoreApp Bridge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtComRcvd;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox ddlComPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer simulatorTimer;
        private System.Windows.Forms.CheckBox chkSimulator;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox chkReceiving;
        private System.Windows.Forms.CheckBox chkSending;
        private System.Windows.Forms.CheckBox chkMonitorInput;
        private System.Windows.Forms.CheckBox chkMonitorOutput;
        private System.Windows.Forms.Button btnListen;
    }
}

