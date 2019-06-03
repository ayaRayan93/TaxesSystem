namespace MainSystem
{
    partial class ChangeIP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeIP));
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labOldIP = new System.Windows.Forms.Label();
            this.txtNewIP = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.pictureBoxCheckConnection = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comBranchName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(229, 196);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 36);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Current IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "New IP";
            // 
            // labOldIP
            // 
            this.labOldIP.AutoSize = true;
            this.labOldIP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labOldIP.Location = new System.Drawing.Point(119, 95);
            this.labOldIP.Name = "labOldIP";
            this.labOldIP.Size = new System.Drawing.Size(0, 19);
            this.labOldIP.TabIndex = 17;
            // 
            // txtNewIP
            // 
            this.txtNewIP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewIP.Location = new System.Drawing.Point(123, 138);
            this.txtNewIP.Name = "txtNewIP";
            this.txtNewIP.Size = new System.Drawing.Size(163, 26);
            this.txtNewIP.TabIndex = 18;
            this.txtNewIP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Click);
            this.txtNewIP.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnTestConnection.FlatAppearance.BorderSize = 0;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnTestConnection.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTestConnection.Location = new System.Drawing.Point(63, 196);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(123, 36);
            this.btnTestConnection.TabIndex = 19;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // pictureBoxCheckConnection
            // 
            this.pictureBoxCheckConnection.InitialImage = null;
            this.pictureBoxCheckConnection.Location = new System.Drawing.Point(291, 129);
            this.pictureBoxCheckConnection.Name = "pictureBoxCheckConnection";
            this.pictureBoxCheckConnection.Size = new System.Drawing.Size(41, 35);
            this.pictureBoxCheckConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCheckConnection.TabIndex = 20;
            this.pictureBoxCheckConnection.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 19);
            this.label3.TabIndex = 21;
            this.label3.Text = "Branch";
            // 
            // comBranchName
            // 
            this.comBranchName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comBranchName.FormattingEnabled = true;
            this.comBranchName.Location = new System.Drawing.Point(123, 45);
            this.comBranchName.Name = "comBranchName";
            this.comBranchName.Size = new System.Drawing.Size(163, 27);
            this.comBranchName.TabIndex = 22;
            // 
            // ChangeIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(399, 265);
            this.Controls.Add(this.comBranchName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBoxCheckConnection);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.txtNewIP);
            this.Controls.Add(this.labOldIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeIP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change IP Address";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChangeIP_FormClosed);
            this.Load += new System.EventHandler(this.ChangeIP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckConnection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labOldIP;
        private System.Windows.Forms.TextBox txtNewIP;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.PictureBox pictureBoxCheckConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comBranchName;
    }
}