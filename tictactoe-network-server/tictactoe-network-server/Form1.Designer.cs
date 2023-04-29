namespace tictactoe_network_server
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
            this.btnListen = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.txtBoxPort = new System.Windows.Forms.TextBox();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.txtBoxMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(158, 90);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(76, 36);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // labelPort
            // 
            this.labelPort.Location = new System.Drawing.Point(23, 46);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(57, 26);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port:";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBoxPort
            // 
            this.txtBoxPort.Location = new System.Drawing.Point(86, 46);
            this.txtBoxPort.Name = "txtBoxPort";
            this.txtBoxPort.Size = new System.Drawing.Size(148, 26);
            this.txtBoxPort.TabIndex = 2;
            this.txtBoxPort.Text = "80";
            // 
            // logs
            // 
            this.logs.HideSelection = false;
            this.logs.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.logs.Location = new System.Drawing.Point(23, 144);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(211, 342);
            this.logs.TabIndex = 3;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // txtBoxMessage
            // 
            this.txtBoxMessage.Enabled = false;
            this.txtBoxMessage.Location = new System.Drawing.Point(102, 507);
            this.txtBoxMessage.Name = "txtBoxMessage";
            this.txtBoxMessage.Size = new System.Drawing.Size(132, 26);
            this.txtBoxMessage.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 507);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Enabled = false;
            this.btnSendMessage.Location = new System.Drawing.Point(159, 549);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 36);
            this.btnSendMessage.TabIndex = 6;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxMessage);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.txtBoxPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.btnListen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtBoxMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendMessage;

        private System.Windows.Forms.RichTextBox logs;

        private System.Windows.Forms.TextBox txtBoxPort;

        private System.Windows.Forms.Label labelPort;

        private System.Windows.Forms.Button btnListen;

        #endregion
    }
}