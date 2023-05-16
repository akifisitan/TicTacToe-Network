namespace tictactoe_network_client
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gameBoard.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.txtBoxPort.Location = new System.Drawing.Point(108, 58);
            this.txtBoxPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxPort.Name = "txtBoxPort";
            this.txtBoxPort.Size = new System.Drawing.Size(130, 26);
            this.txtBoxPort.TabIndex = 4;
            this.txtBoxPort.Text = "80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
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
            this.label2.Location = new System.Drawing.Point(11, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(134, 136);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 34);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.TabStop = false;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // txtBoxChoice
            // 
            this.txtBoxChoice.Enabled = false;
            this.txtBoxChoice.Location = new System.Drawing.Point(64, 207);
            this.txtBoxChoice.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxChoice.Name = "txtBoxChoice";
            this.txtBoxChoice.Size = new System.Drawing.Size(144, 26);
            this.txtBoxChoice.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 207);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Move:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logs
            // 
            this.logs.HideSelection = false;
            this.logs.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.logs.Location = new System.Drawing.Point(10, 22);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(329, 266);
            this.logs.TabIndex = 11;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxUsername
            // 
            this.txtBoxUsername.Location = new System.Drawing.Point(108, 96);
            this.txtBoxUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxUsername.Name = "txtBoxUsername";
            this.txtBoxUsername.Size = new System.Drawing.Size(130, 26);
            this.txtBoxUsername.TabIndex = 5;
            this.txtBoxUsername.Text = "test";
            // 
            // board3
            // 
            this.board3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board3.Location = new System.Drawing.Point(145, 18);
            this.board3.Name = "board3";
            this.board3.Size = new System.Drawing.Size(62, 58);
            this.board3.TabIndex = 37;
            this.board3.Text = "3";
            this.board3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board2
            // 
            this.board2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board2.Location = new System.Drawing.Point(76, 18);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(62, 58);
            this.board2.TabIndex = 36;
            this.board2.Text = "2";
            this.board2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board1.Location = new System.Drawing.Point(9, 18);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(62, 58);
            this.board1.TabIndex = 35;
            this.board1.Text = "1";
            this.board1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board9
            // 
            this.board9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board9.Location = new System.Drawing.Point(145, 131);
            this.board9.Name = "board9";
            this.board9.Size = new System.Drawing.Size(62, 58);
            this.board9.TabIndex = 34;
            this.board9.Text = "9";
            this.board9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board8
            // 
            this.board8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board8.Location = new System.Drawing.Point(76, 131);
            this.board8.Name = "board8";
            this.board8.Size = new System.Drawing.Size(62, 58);
            this.board8.TabIndex = 33;
            this.board8.Text = "8";
            this.board8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board7
            // 
            this.board7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board7.Location = new System.Drawing.Point(9, 131);
            this.board7.Name = "board7";
            this.board7.Size = new System.Drawing.Size(62, 58);
            this.board7.TabIndex = 32;
            this.board7.Text = "7";
            this.board7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board6
            // 
            this.board6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board6.Location = new System.Drawing.Point(145, 74);
            this.board6.Name = "board6";
            this.board6.Size = new System.Drawing.Size(62, 58);
            this.board6.TabIndex = 31;
            this.board6.Text = "6";
            this.board6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board5
            // 
            this.board5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board5.Location = new System.Drawing.Point(76, 74);
            this.board5.Name = "board5";
            this.board5.Size = new System.Drawing.Size(62, 58);
            this.board5.TabIndex = 30;
            this.board5.Text = "5";
            this.board5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board4
            // 
            this.board4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board4.Location = new System.Drawing.Point(9, 74);
            this.board4.Name = "board4";
            this.board4.Size = new System.Drawing.Size(62, 58);
            this.board4.TabIndex = 29;
            this.board4.Text = "4";
            this.board4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameBoard
            // 
            this.gameBoard.Controls.Add(this.btnPlay);
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
            this.gameBoard.Location = new System.Drawing.Point(385, 182);
            this.gameBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoard.Size = new System.Drawing.Size(220, 301);
            this.gameBoard.TabIndex = 38;
            this.gameBoard.TabStop = false;
            this.gameBoard.Text = "Game Board";
            this.gameBoard.Visible = false;
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(110, 246);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(98, 42);
            this.btnPlay.TabIndex = 8;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logs);
            this.groupBox1.Location = new System.Drawing.Point(11, 182);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(352, 301);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Logs";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(873, 522);
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

        private System.Windows.Forms.Button btnPlay;

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