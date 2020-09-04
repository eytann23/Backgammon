namespace BackgammonWorld
{
    partial class BackgammonGameUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgammonGameUI));
            this.moveBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.rollButton = new System.Windows.Forms.Button();
            this.dicePictureBox1 = new System.Windows.Forms.PictureBox();
            this.dicePictureBox2 = new System.Windows.Forms.PictureBox();
            this.dicePictureBox3 = new System.Windows.Forms.PictureBox();
            this.dicePictureBox4 = new System.Windows.Forms.PictureBox();
            this.blackStartLabel = new System.Windows.Forms.Label();
            this.whiteStartLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // moveBackgroundWorker
            // 
            this.moveBackgroundWorker.WorkerReportsProgress = true;
            this.moveBackgroundWorker.WorkerSupportsCancellation = true;
            this.moveBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.moveBackgroundWorker_DoWork);
            this.moveBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.moveBackgroundWorker_ProgressChanged);
            this.moveBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.moveBackgroundWorker_RunWorkerCompleted);
            // 
            // rollButton
            // 
            this.rollButton.BackColor = System.Drawing.Color.Tan;
            this.rollButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rollButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rollButton.Location = new System.Drawing.Point(152, 342);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(140, 65);
            this.rollButton.TabIndex = 20;
            this.rollButton.Text = "Roll Dices";
            this.rollButton.UseVisualStyleBackColor = false;
            this.rollButton.Visible = false;
            this.rollButton.Click += new System.EventHandler(this.rollButton_Click);
            // 
            // dicePictureBox1
            // 
            this.dicePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.dicePictureBox1.Enabled = false;
            this.dicePictureBox1.Location = new System.Drawing.Point(539, 331);
            this.dicePictureBox1.Name = "dicePictureBox1";
            this.dicePictureBox1.Size = new System.Drawing.Size(78, 76);
            this.dicePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dicePictureBox1.TabIndex = 25;
            this.dicePictureBox1.TabStop = false;
            // 
            // dicePictureBox2
            // 
            this.dicePictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.dicePictureBox2.Enabled = false;
            this.dicePictureBox2.Location = new System.Drawing.Point(623, 331);
            this.dicePictureBox2.Name = "dicePictureBox2";
            this.dicePictureBox2.Size = new System.Drawing.Size(78, 76);
            this.dicePictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dicePictureBox2.TabIndex = 26;
            this.dicePictureBox2.TabStop = false;
            // 
            // dicePictureBox3
            // 
            this.dicePictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.dicePictureBox3.Enabled = false;
            this.dicePictureBox3.Location = new System.Drawing.Point(454, 331);
            this.dicePictureBox3.Name = "dicePictureBox3";
            this.dicePictureBox3.Size = new System.Drawing.Size(78, 76);
            this.dicePictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dicePictureBox3.TabIndex = 27;
            this.dicePictureBox3.TabStop = false;
            // 
            // dicePictureBox4
            // 
            this.dicePictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.dicePictureBox4.Enabled = false;
            this.dicePictureBox4.Location = new System.Drawing.Point(707, 331);
            this.dicePictureBox4.Name = "dicePictureBox4";
            this.dicePictureBox4.Size = new System.Drawing.Size(78, 76);
            this.dicePictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dicePictureBox4.TabIndex = 28;
            this.dicePictureBox4.TabStop = false;
            // 
            // blackStartLabel
            // 
            this.blackStartLabel.AutoSize = true;
            this.blackStartLabel.BackColor = System.Drawing.Color.Transparent;
            this.blackStartLabel.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackStartLabel.Location = new System.Drawing.Point(552, 314);
            this.blackStartLabel.Name = "blackStartLabel";
            this.blackStartLabel.Size = new System.Drawing.Size(54, 23);
            this.blackStartLabel.TabIndex = 29;
            this.blackStartLabel.Text = "Black:";
            // 
            // whiteStartLabel
            // 
            this.whiteStartLabel.AutoSize = true;
            this.whiteStartLabel.BackColor = System.Drawing.Color.Transparent;
            this.whiteStartLabel.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteStartLabel.Location = new System.Drawing.Point(633, 312);
            this.whiteStartLabel.Name = "whiteStartLabel";
            this.whiteStartLabel.Size = new System.Drawing.Size(59, 23);
            this.whiteStartLabel.TabIndex = 30;
            this.whiteStartLabel.Text = "White:";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Tan;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startButton.Location = new System.Drawing.Point(556, 222);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(140, 65);
            this.startButton.TabIndex = 31;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Game
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(930, 735);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.whiteStartLabel);
            this.Controls.Add(this.blackStartLabel);
            this.Controls.Add(this.dicePictureBox4);
            this.Controls.Add(this.dicePictureBox3);
            this.Controls.Add(this.dicePictureBox2);
            this.Controls.Add(this.dicePictureBox1);
            this.Controls.Add(this.rollButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "Game";
            this.Click += new System.EventHandler(this.Game_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dicePictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker moveBackgroundWorker;
        private System.Windows.Forms.Button rollButton;
        private System.Windows.Forms.PictureBox dicePictureBox1;
        private System.Windows.Forms.PictureBox dicePictureBox2;
        private System.Windows.Forms.PictureBox dicePictureBox3;
        private System.Windows.Forms.PictureBox dicePictureBox4;
        private System.Windows.Forms.Label blackStartLabel;
        private System.Windows.Forms.Label whiteStartLabel;
        private System.Windows.Forms.Button startButton;
    }
}

