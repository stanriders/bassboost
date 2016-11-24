namespace basbust
{
    partial class mainForm
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
            this.selectButton = new System.Windows.Forms.Button();
            this.openfile = new System.Windows.Forms.OpenFileDialog();
            this.fileName = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.distAmount = new System.Windows.Forms.TrackBar();
            this.bassAmount = new System.Windows.Forms.TrackBar();
            this.debugInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bassAmountLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.infoButton = new System.Windows.Forms.Button();
            this.encodeProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.distAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bassAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(13, 13);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 0;
            this.selectButton.Text = "Чо бустить";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // openfile
            // 
            this.openfile.FileName = "басы!";
            this.openfile.Filter = "Музяка|*.mp3;*.wav;*.ogg";
            this.openfile.FileOk += new System.ComponentModel.CancelEventHandler(this.openfile_FileOk);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(13, 43);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(239, 32);
            this.fileName.TabIndex = 1;
            this.fileName.Text = "...";
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(95, 13);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Заценить";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // distAmount
            // 
            this.distAmount.LargeChange = 1;
            this.distAmount.Location = new System.Drawing.Point(13, 78);
            this.distAmount.Maximum = 3;
            this.distAmount.Name = "distAmount";
            this.distAmount.Size = new System.Drawing.Size(239, 45);
            this.distAmount.TabIndex = 3;
            this.distAmount.Scroll += new System.EventHandler(this.distAmount_Scroll);
            // 
            // bassAmount
            // 
            this.bassAmount.Location = new System.Drawing.Point(13, 130);
            this.bassAmount.Maximum = 15;
            this.bassAmount.Name = "bassAmount";
            this.bassAmount.Size = new System.Drawing.Size(239, 45);
            this.bassAmount.TabIndex = 4;
            this.bassAmount.Scroll += new System.EventHandler(this.bassAmount_Scroll);
            // 
            // debugInfo
            // 
            this.debugInfo.Location = new System.Drawing.Point(269, 13);
            this.debugInfo.Name = "debugInfo";
            this.debugInfo.Size = new System.Drawing.Size(243, 265);
            this.debugInfo.TabIndex = 5;
            this.debugInfo.Text = "залезь обратно придурок бля!\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Громкасть";
            // 
            // bassAmountLabel
            // 
            this.bassAmountLabel.AutoSize = true;
            this.bassAmountLabel.Location = new System.Drawing.Point(22, 161);
            this.bassAmountLabel.Name = "bassAmountLabel";
            this.bassAmountLabel.Size = new System.Drawing.Size(36, 13);
            this.bassAmountLabel.TabIndex = 7;
            this.bassAmountLabel.Text = "БасЫ";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(177, 13);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "В тачку";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveFile
            // 
            this.saveFile.DefaultExt = "mp3";
            this.saveFile.Filter = "mp3 Files|*.mp3";
            this.saveFile.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFile_FileOk);
            // 
            // infoButton
            // 
            this.infoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.infoButton.Location = new System.Drawing.Point(12, 210);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(240, 20);
            this.infoButton.TabIndex = 9;
            this.infoButton.Text = "чот непонятно нихуя";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // encodeProgress
            // 
            this.encodeProgress.Location = new System.Drawing.Point(12, 181);
            this.encodeProgress.Name = "encodeProgress";
            this.encodeProgress.Size = new System.Drawing.Size(240, 23);
            this.encodeProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.encodeProgress.TabIndex = 10;
            this.encodeProgress.Visible = false;
            // 
            // mainForm
            // 
            this.AcceptButton = this.infoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 242);
            this.Controls.Add(this.encodeProgress);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.bassAmountLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.debugInfo);
            this.Controls.Add(this.bassAmount);
            this.Controls.Add(this.distAmount);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.selectButton);
            this.MaximumSize = new System.Drawing.Size(540, 280);
            this.MinimumSize = new System.Drawing.Size(280, 280);
            this.Name = "mainForm";
            this.ShowIcon = false;
            this.Text = "кач";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.distAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bassAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.OpenFileDialog openfile;
        private System.Windows.Forms.Label fileName;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.TrackBar distAmount;
        private System.Windows.Forms.TrackBar bassAmount;
        private System.Windows.Forms.Label debugInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label bassAmountLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.ProgressBar encodeProgress;
    }
}

