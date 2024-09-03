namespace Japhet.Vues.forms
{
    partial class Frmbackup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnrestore = new System.Windows.Forms.Button();
            this.lblTitreBackUp = new System.Windows.Forms.Label();
            this.btnStartBackUp = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnSaveDialog = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnrestore);
            this.groupBox1.Controls.Add(this.lblTitreBackUp);
            this.groupBox1.Controls.Add(this.btnStartBackUp);
            this.groupBox1.Controls.Add(this.lblPath);
            this.groupBox1.Controls.Add(this.btnSaveDialog);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnrestore
            // 
            this.btnrestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnrestore.BackColor = System.Drawing.Color.Transparent;
            this.btnrestore.Enabled = false;
            this.btnrestore.FlatAppearance.BorderSize = 0;
            this.btnrestore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnrestore.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 24F);
            this.btnrestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnrestore.Location = new System.Drawing.Point(46, 311);
            this.btnrestore.Name = "btnrestore";
            this.btnrestore.Size = new System.Drawing.Size(182, 56);
            this.btnrestore.TabIndex = 18;
            this.btnrestore.Text = "RESTORE";
            this.btnrestore.UseVisualStyleBackColor = false;
            this.btnrestore.Click += new System.EventHandler(this.btnrestore_Click);
            // 
            // lblTitreBackUp
            // 
            this.lblTitreBackUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitreBackUp.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitreBackUp.Location = new System.Drawing.Point(48, 15);
            this.lblTitreBackUp.Name = "lblTitreBackUp";
            this.lblTitreBackUp.Size = new System.Drawing.Size(186, 30);
            this.lblTitreBackUp.TabIndex = 17;
            this.lblTitreBackUp.Text = "Backup MySql BD";
            // 
            // btnStartBackUp
            // 
            this.btnStartBackUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartBackUp.BackColor = System.Drawing.Color.Transparent;
            this.btnStartBackUp.Enabled = false;
            this.btnStartBackUp.FlatAppearance.BorderSize = 0;
            this.btnStartBackUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartBackUp.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 24F);
            this.btnStartBackUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStartBackUp.Location = new System.Drawing.Point(46, 240);
            this.btnStartBackUp.Name = "btnStartBackUp";
            this.btnStartBackUp.Size = new System.Drawing.Size(182, 56);
            this.btnStartBackUp.TabIndex = 16;
            this.btnStartBackUp.Text = "DEBUTER";
            this.btnStartBackUp.UseVisualStyleBackColor = false;
            this.btnStartBackUp.Click += new System.EventHandler(this.btnStartBackUp_Click);
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPath.Location = new System.Drawing.Point(4, 139);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(173, 48);
            this.lblPath.TabIndex = 15;
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveDialog
            // 
            this.btnSaveDialog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDialog.FlatAppearance.BorderSize = 0;
            this.btnSaveDialog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveDialog.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDialog.Location = new System.Drawing.Point(46, 69);
            this.btnSaveDialog.Name = "btnSaveDialog";
            this.btnSaveDialog.Size = new System.Drawing.Size(188, 28);
            this.btnSaveDialog.TabIndex = 14;
            this.btnSaveDialog.Text = "Choisir un emplacement d\'enregistrement";
            this.btnSaveDialog.UseVisualStyleBackColor = true;
            this.btnSaveDialog.Click += new System.EventHandler(this.btnSaveDialog_Click);
            // 
            // Frmbackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frmbackup";
            this.Text = "Frmbackup";
            this.Load += new System.EventHandler(this.Frmbackup_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnrestore;
        private System.Windows.Forms.Label lblTitreBackUp;
        protected System.Windows.Forms.Button btnStartBackUp;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnSaveDialog;
    }
}