namespace Maparameter
{
    partial class FrmPlaceSpecialBlock
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
            this.BtnOK = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.CboDir = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(12, 65);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 0;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(93, 65);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.btnAnnuler.TabIndex = 1;
            this.btnAnnuler.Text = "Cancel";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 39);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(156, 20);
            this.txtName.TabIndex = 2;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "ChangeStageNow",
            "ChangeStageUp",
            "ChangeStageDown",
            "ChangeStageLeft",
            "ChangeStageRight",
            "StartPoint",
            "Interact"});
            this.cboType.Location = new System.Drawing.Point(12, 12);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(156, 21);
            this.cboType.TabIndex = 3;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(174, 65);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 4;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            // 
            // CboDir
            // 
            this.CboDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDir.FormattingEnabled = true;
            this.CboDir.Items.AddRange(new object[] {
            "NONE",
            "UP",
            "DOWN",
            "LEFT",
            "RIGHT"});
            this.CboDir.Location = new System.Drawing.Point(174, 12);
            this.CboDir.Name = "CboDir";
            this.CboDir.Size = new System.Drawing.Size(78, 21);
            this.CboDir.TabIndex = 5;
            // 
            // FrmPlaceSpecialBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 100);
            this.Controls.Add(this.CboDir);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPlaceSpecialBlock";
            this.Text = "Special Block";
            this.Load += new System.EventHandler(this.FrmPlaceSpecialBlock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.ComboBox CboDir;
    }
}