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
            this.label1 = new System.Windows.Forms.Label();
            this.labelTurn = new System.Windows.Forms.Label();
            this.gameBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(130, 41);
            this.btnListen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(78, 37);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // labelPort
            // 
            this.labelPort.Location = new System.Drawing.Point(20, 15);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(51, 21);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port:";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxPort
            // 
            this.txtBoxPort.Location = new System.Drawing.Point(76, 15);
            this.txtBoxPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxPort.Name = "txtBoxPort";
            this.txtBoxPort.Size = new System.Drawing.Size(132, 22);
            this.txtBoxPort.TabIndex = 2;
            this.txtBoxPort.Text = "80";
            // 
            // logs
            // 
            this.logs.HideSelection = false;
            this.logs.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.logs.Location = new System.Drawing.Point(20, 115);
            this.logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(324, 182);
            this.logs.TabIndex = 3;
            this.logs.TabStop = false;
            this.logs.Text = "";
            // 
            // txtBoxPlayers
            // 
            this.txtBoxPlayers.Location = new System.Drawing.Point(565, 114);
            this.txtBoxPlayers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxPlayers.Name = "txtBoxPlayers";
            this.txtBoxPlayers.ReadOnly = true;
            this.txtBoxPlayers.Size = new System.Drawing.Size(118, 133);
            this.txtBoxPlayers.TabIndex = 7;
            this.txtBoxPlayers.TabStop = false;
            this.txtBoxPlayers.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(566, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "Players";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Logs";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStartGame
            // 
            this.btnStartGame.Enabled = false;
            this.btnStartGame.Location = new System.Drawing.Point(566, 251);
            this.btnStartGame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(116, 44);
            this.btnStartGame.TabIndex = 10;
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // board4
            // 
            this.board4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board4.Location = new System.Drawing.Point(20, 68);
            this.board4.Name = "board4";
            this.board4.Size = new System.Drawing.Size(55, 46);
            this.board4.TabIndex = 20;
            this.board4.Text = "4";
            this.board4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board5
            // 
            this.board5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board5.Location = new System.Drawing.Point(80, 68);
            this.board5.Name = "board5";
            this.board5.Size = new System.Drawing.Size(55, 46);
            this.board5.TabIndex = 21;
            this.board5.Text = "5";
            this.board5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board6
            // 
            this.board6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board6.Location = new System.Drawing.Point(140, 68);
            this.board6.Name = "board6";
            this.board6.Size = new System.Drawing.Size(55, 46);
            this.board6.TabIndex = 22;
            this.board6.Text = "6";
            this.board6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board9
            // 
            this.board9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board9.Location = new System.Drawing.Point(140, 114);
            this.board9.Name = "board9";
            this.board9.Size = new System.Drawing.Size(55, 46);
            this.board9.TabIndex = 25;
            this.board9.Text = "9";
            this.board9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board8
            // 
            this.board8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board8.Location = new System.Drawing.Point(80, 114);
            this.board8.Name = "board8";
            this.board8.Size = new System.Drawing.Size(55, 46);
            this.board8.TabIndex = 24;
            this.board8.Text = "8";
            this.board8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board7
            // 
            this.board7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board7.Location = new System.Drawing.Point(20, 114);
            this.board7.Name = "board7";
            this.board7.Size = new System.Drawing.Size(55, 46);
            this.board7.TabIndex = 23;
            this.board7.Text = "7";
            this.board7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board3
            // 
            this.board3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board3.Location = new System.Drawing.Point(140, 22);
            this.board3.Name = "board3";
            this.board3.Size = new System.Drawing.Size(55, 46);
            this.board3.TabIndex = 28;
            this.board3.Text = "3";
            this.board3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board2
            // 
            this.board2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board2.Location = new System.Drawing.Point(80, 22);
            this.board2.Name = "board2";
            this.board2.Size = new System.Drawing.Size(55, 46);
            this.board2.TabIndex = 27;
            this.board2.Text = "2";
            this.board2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // board1
            // 
            this.board1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board1.Location = new System.Drawing.Point(20, 22);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(55, 46);
            this.board1.TabIndex = 26;
            this.board1.Text = "1";
            this.board1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameBoard
            // 
            this.gameBoard.Controls.Add(this.label1);
            this.gameBoard.Controls.Add(this.labelTurn);
            this.gameBoard.Controls.Add(this.board5);
            this.gameBoard.Controls.Add(this.board3);
            this.gameBoard.Controls.Add(this.board4);
            this.gameBoard.Controls.Add(this.board2);
            this.gameBoard.Controls.Add(this.board6);
            this.gameBoard.Controls.Add(this.board1);
            this.gameBoard.Controls.Add(this.board7);
            this.gameBoard.Controls.Add(this.board9);
            this.gameBoard.Controls.Add(this.board8);
            this.gameBoard.Location = new System.Drawing.Point(349, 92);
            this.gameBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoard.Size = new System.Drawing.Size(211, 252);
            this.gameBoard.TabIndex = 29;
            this.gameBoard.TabStop = false;
            this.gameBoard.Text = "Game Board";
            this.gameBoard.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 30;
            this.label1.Text = "X\'s turn";
            // 
            // labelTurn
            // 
            this.labelTurn.AutoSize = true;
            this.labelTurn.Location = new System.Drawing.Point(69, 173);
            this.labelTurn.Name = "labelTurn";
            this.labelTurn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelTurn.Size = new System.Drawing.Size(0, 17);
            this.labelTurn.TabIndex = 29;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(697, 355);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "TicTacToe Server";
            this.gameBoard.ResumeLayout(false);
            this.gameBoard.PerformLayout();
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

        private System.Windows.Forms.Label labelTurn;
        private System.Windows.Forms.Label label1;
    }
}