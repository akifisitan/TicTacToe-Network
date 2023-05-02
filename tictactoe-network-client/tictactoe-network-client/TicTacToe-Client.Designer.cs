namespace tictactoe_network_client
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
            this.txtBoxIp = new System.Windows.Forms.TextBox();
            this.txtBoxPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtBoxMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxUsername = new System.Windows.Forms.TextBox();
            this.board3 = new System.Windows.Forms.Label();
            this.board2 = new System.Windows.Forms.Label();
            this.board1 = new System.Windows.Forms.Label();
            this.board9 = new System.Windows.Forms.Label();
            this.board8 = new System.Windows.Forms.Label();
            this.board7 = new System.Windows.Forms.Label();
            this.board6 = new System.Windows.Forms.Label();
            this.board5 = new System.Windows.Forms.Label();
            this.board4 = new System.Windows.Forms.Label();
            this.gameBoard = new System.Windows.Forms.GroupBox();
            this.gameBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxIp
            // 
            this.txtBoxIp.Location = new System.Drawing.Point(108, 21);
            this.txtBoxIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxIp.Name = "txtBoxIp";
            this.txtBoxIp.Size = new System.Drawing.Size(130, 26);
            this.txtBoxIp.TabIndex = 3;
            this.txtBoxIp.Text = "127.0.0.1";
            // 
            // txtBoxPort
            // 
            this.txtBoxPort.Location = new System.Drawing.Point(108, 54);
            this.txtBoxPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxPort.Name = "txtBoxPort";
            this.txtBoxPort.Size = new System.Drawing.Size(130, 26);
            this.txtBoxPort.TabIndex = 4;
            this.txtBoxPort.Text = "80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(134, 116);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 34);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.TabStop = false;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // txtBoxMessage
            // 
            this.txtBoxMessage.Enabled = false;
            this.txtBoxMessage.Location = new System.Drawing.Point(84, 188);
            this.txtBoxMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxMessage.Name = "txtBoxMessage";
            this.txtBoxMessage.Size = new System.Drawing.Size(123, 26);
            this.txtBoxMessage.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Choice:";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(109, 218);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(98, 31);
            this.btnSend.TabIndex = 8;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "Play";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button_send_Click);
            // 
            // logs
            // 
            this.logs.HideSelection = false;
            this.logs.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.logs.Location = new System.Drawing.Point(11, 163);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(329, 243);
            this.logs.TabIndex = 11;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxUsername
            // 
            this.txtBoxUsername.Location = new System.Drawing.Point(108, 85);
            this.txtBoxUsername.Name = "txtBoxUsername";
            this.txtBoxUsername.Size = new System.Drawing.Size(130, 26);
            this.txtBoxUsername.TabIndex = 5;
            this.txtBoxUsername.Text = "Michael Jordan";
            // 
            // board3
            // 
            this.board3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board3.Location = new System.Drawing.Point(145, 17);
            this.board3.Name = "board3";
            this.board3.Size = new System.Drawing.Size(62, 57);
            this.board3.TabIndex = 37;
            this.board3.Text = "-";
            this.board3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board2
            // 
            this.board2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board2.Location = new System.Drawing.Point(77, 17);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(62, 57);
            this.board2.TabIndex = 36;
            this.board2.Text = "-";
            this.board2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board1.Location = new System.Drawing.Point(9, 17);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(62, 57);
            this.board1.TabIndex = 35;
            this.board1.Text = "-";
            this.board1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board9
            // 
            this.board9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board9.Location = new System.Drawing.Point(145, 131);
            this.board9.Name = "board9";
            this.board9.Size = new System.Drawing.Size(62, 57);
            this.board9.TabIndex = 34;
            this.board9.Text = "-";
            this.board9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board8
            // 
            this.board8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board8.Location = new System.Drawing.Point(77, 131);
            this.board8.Name = "board8";
            this.board8.Size = new System.Drawing.Size(62, 57);
            this.board8.TabIndex = 33;
            this.board8.Text = "-";
            this.board8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board7
            // 
            this.board7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board7.Location = new System.Drawing.Point(9, 131);
            this.board7.Name = "board7";
            this.board7.Size = new System.Drawing.Size(62, 57);
            this.board7.TabIndex = 32;
            this.board7.Text = "-";
            this.board7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board6
            // 
            this.board6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board6.Location = new System.Drawing.Point(145, 74);
            this.board6.Name = "board6";
            this.board6.Size = new System.Drawing.Size(62, 57);
            this.board6.TabIndex = 31;
            this.board6.Text = "-";
            this.board6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board5
            // 
            this.board5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board5.Location = new System.Drawing.Point(77, 74);
            this.board5.Name = "board5";
            this.board5.Size = new System.Drawing.Size(62, 57);
            this.board5.TabIndex = 30;
            this.board5.Text = "-";
            this.board5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board4
            // 
            this.board4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board4.Location = new System.Drawing.Point(9, 74);
            this.board4.Name = "board4";
            this.board4.Size = new System.Drawing.Size(62, 57);
            this.board4.TabIndex = 29;
            this.board4.Text = "-";
            this.board4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameBoard
            // 
            this.gameBoard.Controls.Add(this.btnSend);
            this.gameBoard.Controls.Add(this.txtBoxMessage);
            this.gameBoard.Controls.Add(this.board9);
            this.gameBoard.Controls.Add(this.board3);
            this.gameBoard.Controls.Add(this.label3);
            this.gameBoard.Controls.Add(this.board2);
            this.gameBoard.Controls.Add(this.board4);
            this.gameBoard.Controls.Add(this.board1);
            this.gameBoard.Controls.Add(this.board5);
            this.gameBoard.Controls.Add(this.board6);
            this.gameBoard.Controls.Add(this.board8);
            this.gameBoard.Controls.Add(this.board7);
            this.gameBoard.Location = new System.Drawing.Point(532, 136);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(220, 270);
            this.gameBoard.TabIndex = 38;
            this.gameBoard.TabStop = false;
            this.gameBoard.Text = "Game Board";
            this.gameBoard.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.txtBoxUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxPort);
            this.Controls.Add(this.txtBoxIp);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.gameBoard.ResumeLayout(false);
            this.gameBoard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox gameBoard;

        private System.Windows.Forms.Label board3;
        private System.Windows.Forms.Label board2;
        private System.Windows.Forms.Label board1;
        private System.Windows.Forms.Label board9;
        private System.Windows.Forms.Label board8;
        private System.Windows.Forms.Label board7;
        private System.Windows.Forms.Label board6;
        private System.Windows.Forms.Label board5;
        private System.Windows.Forms.Label board4;

        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxUsername;

        private System.Windows.Forms.Button btnSend;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.TextBox txtBoxMessage;

        private System.Windows.Forms.Button btnConnect;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TextBox txtBoxPort;

        private System.Windows.Forms.TextBox txtBoxIp;

        #endregion
    }
}