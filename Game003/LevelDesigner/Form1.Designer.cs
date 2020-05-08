namespace Maparameter
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.OfdPic = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenPic = new System.Windows.Forms.Button();
            this.NudBlockSize = new System.Windows.Forms.NumericUpDown();
            this.PbPic = new System.Windows.Forms.PictureBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.BtnWriteLevels = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnSpecial = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NudBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbPic)).BeginInit();
            this.SuspendLayout();
            // 
            // OfdPic
            // 
            this.OfdPic.FileName = "openFileDialog1";
            // 
            // btnOpenPic
            // 
            this.btnOpenPic.Location = new System.Drawing.Point(12, 9);
            this.btnOpenPic.Name = "btnOpenPic";
            this.btnOpenPic.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPic.TabIndex = 0;
            this.btnOpenPic.Text = "Open File";
            this.btnOpenPic.UseVisualStyleBackColor = true;
            this.btnOpenPic.Click += new System.EventHandler(this.btnOpenPic_Click);
            // 
            // NudBlockSize
            // 
            this.NudBlockSize.Location = new System.Drawing.Point(304, 12);
            this.NudBlockSize.Name = "NudBlockSize";
            this.NudBlockSize.Size = new System.Drawing.Size(55, 20);
            this.NudBlockSize.TabIndex = 1;
            this.NudBlockSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // PbPic
            // 
            this.PbPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbPic.Location = new System.Drawing.Point(12, 73);
            this.PbPic.Name = "PbPic";
            this.PbPic.Size = new System.Drawing.Size(1033, 488);
            this.PbPic.TabIndex = 2;
            this.PbPic.TabStop = false;
            this.PbPic.Click += new System.EventHandler(this.PbPic_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(93, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(205, 20);
            this.txtPath.TabIndex = 4;
            // 
            // BtnWriteLevels
            // 
            this.BtnWriteLevels.Location = new System.Drawing.Point(365, 9);
            this.BtnWriteLevels.Name = "BtnWriteLevels";
            this.BtnWriteLevels.Size = new System.Drawing.Size(84, 23);
            this.BtnWriteLevels.TabIndex = 7;
            this.BtnWriteLevels.Text = "Write Level";
            this.BtnWriteLevels.UseVisualStyleBackColor = true;
            this.BtnWriteLevels.Click += new System.EventHandler(this.BtnWrite_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(365, 47);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(75, 23);
            this.BtnReset.TabIndex = 8;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ctrl + LeftClick => Special Block";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "LeftClick => Level Block";
            // 
            // BtnSpecial
            // 
            this.BtnSpecial.Location = new System.Drawing.Point(455, 9);
            this.BtnSpecial.Name = "BtnSpecial";
            this.BtnSpecial.Size = new System.Drawing.Size(84, 23);
            this.BtnSpecial.TabIndex = 12;
            this.BtnSpecial.Text = "Write Special";
            this.BtnSpecial.UseVisualStyleBackColor = true;
            this.BtnSpecial.Click += new System.EventHandler(this.BtnSpecial_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1057, 573);
            this.Controls.Add(this.BtnSpecial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.BtnWriteLevels);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.PbPic);
            this.Controls.Add(this.NudBlockSize);
            this.Controls.Add(this.btnOpenPic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.Text = "Mapping";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NudBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OfdPic;
        private System.Windows.Forms.Button btnOpenPic;
        private System.Windows.Forms.NumericUpDown NudBlockSize;
        private System.Windows.Forms.PictureBox PbPic;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button BtnWriteLevels;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSpecial;
    }
}

