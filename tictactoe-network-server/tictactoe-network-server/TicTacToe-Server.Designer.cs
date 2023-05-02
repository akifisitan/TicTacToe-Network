namespace tictactoe_network_server
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
            this.btnListen = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.txtBoxPort = new System.Windows.Forms.TextBox();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.txtBoxPlayers = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.board4 = new System.Windows.Forms.Label();
            this.board5 = new System.Windows.Forms.Label();
            this.board6 = new System.Windows.Forms.Label();
            this.board9 = new System.Windows.Forms.Label();
            this.board8 = new System.Windows.Forms.Label();
            this.board7 = new System.Windows.Forms.Label();
            this.board3 = new System.Windows.Forms.Label();
            this.board2 = new System.Windows.Forms.Label();
            this.board1 = new System.Windows.Forms.Label();
            this.gameBoard = new System.Windows.Forms.GroupBox();
            this.gameBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(146, 51);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(88, 46);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // labelPort
            // 
            this.labelPort.Location = new System.Drawing.Point(23, 19);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(57, 26);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port:";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxPort
            // 
            this.txtBoxPort.Location = new System.Drawing.Point(86, 19);
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
            this.logs.Size = new System.Drawing.Size(364, 226);
            this.logs.TabIndex = 3;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // txtBoxPlayers
            // 
            this.txtBoxPlayers.Location = new System.Drawing.Point(636, 143);
            this.txtBoxPlayers.Name = "txtBoxPlayers";
            this.txtBoxPlayers.ReadOnly = true;
            this.txtBoxPlayers.Size = new System.Drawing.Size(132, 165);
            this.txtBoxPlayers.TabIndex = 7;
            this.txtBoxPlayers.TabStop = false;
            this.txtBoxPlayers.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(637, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Players";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Logs";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStartGame
            // 
            this.btnStartGame.Enabled = false;
            this.btnStartGame.Location = new System.Drawing.Point(637, 314);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(131, 55);
            this.btnStartGame.TabIndex = 10;
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // board4
            // 
            this.board4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board4.Location = new System.Drawing.Point(22, 85);
            this.board4.Name = "board4";
            this.board4.Size = new System.Drawing.Size(62, 57);
            this.board4.TabIndex = 20;
            this.board4.Text = "-";
            this.board4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board5
            // 
            this.board5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board5.Location = new System.Drawing.Point(90, 85);
            this.board5.Name = "board5";
            this.board5.Size = new System.Drawing.Size(62, 57);
            this.board5.TabIndex = 21;
            this.board5.Text = "-";
            this.board5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board6
            // 
            this.board6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board6.Location = new System.Drawing.Point(158, 85);
            this.board6.Name = "board6";
            this.board6.Size = new System.Drawing.Size(62, 57);
            this.board6.TabIndex = 22;
            this.board6.Text = "-";
            this.board6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board9
            // 
            this.board9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board9.Location = new System.Drawing.Point(158, 142);
            this.board9.Name = "board9";
            this.board9.Size = new System.Drawing.Size(62, 57);
            this.board9.TabIndex = 25;
            this.board9.Text = "-";
            this.board9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board8
            // 
            this.board8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board8.Location = new System.Drawing.Point(90, 142);
            this.board8.Name = "board8";
            this.board8.Size = new System.Drawing.Size(62, 57);
            this.board8.TabIndex = 24;
            this.board8.Text = "-";
            this.board8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board7
            // 
            this.board7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board7.Location = new System.Drawing.Point(22, 142);
            this.board7.Name = "board7";
            this.board7.Size = new System.Drawing.Size(62, 57);
            this.board7.TabIndex = 23;
            this.board7.Text = "-";
            this.board7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board3
            // 
            this.board3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board3.Location = new System.Drawing.Point(158, 28);
            this.board3.Name = "board3";
            this.board3.Size = new System.Drawing.Size(62, 57);
            this.board3.TabIndex = 28;
            this.board3.Text = "-";
            this.board3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board2
            // 
            this.board2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board2.Location = new System.Drawing.Point(90, 28);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(62, 57);
            this.board2.TabIndex = 27;
            this.board2.Text = "-";
            this.board2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board1.Location = new System.Drawing.Point(22, 28);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(62, 57);
            this.board1.TabIndex = 26;
            this.board1.Text = "-";
            this.board1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameBoard
            // 
            this.gameBoard.Controls.Add(this.board5);
            this.gameBoard.Controls.Add(this.board3);
            this.gameBoard.Controls.Add(this.board4);
            this.gameBoard.Controls.Add(this.board2);
            this.gameBoard.Controls.Add(this.board6);
            this.gameBoard.Controls.Add(this.board1);
            this.gameBoard.Controls.Add(this.board7);
            this.gameBoard.Controls.Add(this.board9);
            this.gameBoard.Controls.Add(this.board8);
            this.gameBoard.Location = new System.Drawing.Point(393, 115);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(237, 255);
            this.gameBoard.TabIndex = 29;
            this.gameBoard.TabStop = false;
            this.gameBoard.Text = "Game Board";
            this.gameBoard.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 444);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxPlayers);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.txtBoxPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.btnListen);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "MainWindow";
            this.Text = "TicTacToe Server";
            this.gameBoard.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox gameBoard;

        private System.Windows.Forms.Label board1;
        private System.Windows.Forms.Label board2;
        private System.Windows.Forms.Label board3;
        private System.Windows.Forms.Label board4;
        private System.Windows.Forms.Label board5;
        private System.Windows.Forms.Label board6;
        private System.Windows.Forms.Label board7;
        private System.Windows.Forms.Label board8;
        private System.Windows.Forms.Label board9;
        
        private System.Windows.Forms.Button btnStartGame;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.RichTextBox txtBoxPlayers;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.RichTextBox logs;

        private System.Windows.Forms.TextBox txtBoxPort;

        private System.Windows.Forms.Label labelPort;

        private System.Windows.Forms.Button btnListen;

        #endregion
    }
}