﻿namespace tictactoe_network_client
{
    partial class MainWindow
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
            this.txtBoxChoice = new System.Windows.Forms.TextBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gameBoard.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxIp
            // 
            this.txtBoxIp.Location = new System.Drawing.Point(96, 17);
            this.txtBoxIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxIp.Name = "txtBoxIp";
            this.txtBoxIp.Size = new System.Drawing.Size(116, 22);
            this.txtBoxIp.TabIndex = 3;
            this.txtBoxIp.Text = "127.0.0.1";
            // 
            // txtBoxPort
            // 
            this.txtBoxPort.Location = new System.Drawing.Point(96, 46);
            this.txtBoxPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxPort.Name = "txtBoxPort";
            this.txtBoxPort.Size = new System.Drawing.Size(116, 22);
            this.txtBoxPort.TabIndex = 4;
            this.txtBoxPort.Text = "80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(119, 109);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(92, 27);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.TabStop = false;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // txtBoxChoice
            // 
            this.txtBoxChoice.Enabled = false;
            this.txtBoxChoice.Location = new System.Drawing.Point(57, 150);
            this.txtBoxChoice.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxChoice.Name = "txtBoxChoice";
            this.txtBoxChoice.Size = new System.Drawing.Size(128, 22);
            this.txtBoxChoice.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Move:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(97, 174);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(87, 25);
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
            this.logs.Location = new System.Drawing.Point(9, 18);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(293, 203);
            this.logs.TabIndex = 11;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxUsername
            // 
            this.txtBoxUsername.Location = new System.Drawing.Point(96, 77);
            this.txtBoxUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxUsername.Name = "txtBoxUsername";
            this.txtBoxUsername.Size = new System.Drawing.Size(116, 22);
            this.txtBoxUsername.TabIndex = 5;
            this.txtBoxUsername.Text = "Michael Jordan";
            // 
            // board3
            // 
            this.board3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board3.Location = new System.Drawing.Point(129, 14);
            this.board3.Name = "board3";
            this.board3.Size = new System.Drawing.Size(55, 46);
            this.board3.TabIndex = 37;
            this.board3.Text = "3";
            this.board3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board2
            // 
            this.board2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board2.Location = new System.Drawing.Point(68, 14);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(55, 46);
            this.board2.TabIndex = 36;
            this.board2.Text = "2";
            this.board2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board1.Location = new System.Drawing.Point(8, 14);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(55, 46);
            this.board1.TabIndex = 35;
            this.board1.Text = "1";
            this.board1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board9
            // 
            this.board9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board9.Location = new System.Drawing.Point(129, 105);
            this.board9.Name = "board9";
            this.board9.Size = new System.Drawing.Size(55, 46);
            this.board9.TabIndex = 34;
            this.board9.Text = "9";
            this.board9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board8
            // 
            this.board8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board8.Location = new System.Drawing.Point(68, 105);
            this.board8.Name = "board8";
            this.board8.Size = new System.Drawing.Size(55, 46);
            this.board8.TabIndex = 33;
            this.board8.Text = "8";
            this.board8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board7
            // 
            this.board7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board7.Location = new System.Drawing.Point(8, 105);
            this.board7.Name = "board7";
            this.board7.Size = new System.Drawing.Size(55, 46);
            this.board7.TabIndex = 32;
            this.board7.Text = "7";
            this.board7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board6
            // 
            this.board6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board6.Location = new System.Drawing.Point(129, 59);
            this.board6.Name = "board6";
            this.board6.Size = new System.Drawing.Size(55, 46);
            this.board6.TabIndex = 31;
            this.board6.Text = "6";
            this.board6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board5
            // 
            this.board5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board5.Location = new System.Drawing.Point(68, 59);
            this.board5.Name = "board5";
            this.board5.Size = new System.Drawing.Size(55, 46);
            this.board5.TabIndex = 30;
            this.board5.Text = "5";
            this.board5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board4
            // 
            this.board4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board4.Location = new System.Drawing.Point(8, 59);
            this.board4.Name = "board4";
            this.board4.Size = new System.Drawing.Size(55, 46);
            this.board4.TabIndex = 29;
            this.board4.Text = "4";
            this.board4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameBoard
            // 
            this.gameBoard.Controls.Add(this.btnSend);
            this.gameBoard.Controls.Add(this.txtBoxChoice);
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
            this.gameBoard.Location = new System.Drawing.Point(342, 146);
            this.gameBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoard.Size = new System.Drawing.Size(196, 216);
            this.gameBoard.TabIndex = 38;
            this.gameBoard.TabStop = false;
            this.gameBoard.Text = "Game Board";
            this.gameBoard.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logs);
            this.groupBox1.Location = new System.Drawing.Point(10, 146);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(313, 216);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Logs";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(557, 418);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.txtBoxUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxPort);
            this.Controls.Add(this.txtBoxIp);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "TicTacToe Client";
            this.gameBoard.ResumeLayout(false);
            this.gameBoard.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBox1;

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

        private System.Windows.Forms.TextBox txtBoxChoice;

        private System.Windows.Forms.Button btnConnect;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TextBox txtBoxPort;

        private System.Windows.Forms.TextBox txtBoxIp;

        #endregion
    }
}