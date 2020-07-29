namespace TaxesSystem
{
    partial class customPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panContent = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNewPlus = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDes = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPlus = new System.Windows.Forms.TextBox();
            this.panContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.btnDelete);
            this.panContent.Controls.Add(this.dataGridView1);
            this.panContent.Controls.Add(this.btnNewPlus);
            this.panContent.Controls.Add(this.label18);
            this.panContent.Controls.Add(this.txtDes);
            this.panContent.Controls.Add(this.label17);
            this.panContent.Controls.Add(this.txtPlus);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 0);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(788, 227);
            this.panContent.TabIndex = 43;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(113, 151);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 27);
            this.btnDelete.TabIndex = 140;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value,
            this.Description});
            this.dataGridView1.Location = new System.Drawing.Point(186, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(488, 94);
            this.dataGridView1.TabIndex = 139;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "الزيادة";
            this.Value.Name = "Value";
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "وصف";
            this.Description.Name = "Description";
            // 
            // btnNewPlus
            // 
            this.btnNewPlus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNewPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnNewPlus.FlatAppearance.BorderSize = 0;
            this.btnNewPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPlus.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnNewPlus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNewPlus.Location = new System.Drawing.Point(69, 55);
            this.btnNewPlus.Name = "btnNewPlus";
            this.btnNewPlus.Size = new System.Drawing.Size(92, 27);
            this.btnNewPlus.TabIndex = 138;
            this.btnNewPlus.Text = "اضافة";
            this.btnNewPlus.UseVisualStyleBackColor = false;
            this.btnNewPlus.Click += new System.EventHandler(this.btnNewPlus_Click);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(395, 55);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label18.Size = new System.Drawing.Size(45, 16);
            this.label18.TabIndex = 67;
            this.label18.Text = "الوصف";
            // 
            // txtDes
            // 
            this.txtDes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDes.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDes.Location = new System.Drawing.Point(231, 54);
            this.txtDes.Name = "txtDes";
            this.txtDes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDes.Size = new System.Drawing.Size(158, 24);
            this.txtDes.TabIndex = 66;
            this.txtDes.Text = "0";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(586, 54);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label17.Size = new System.Drawing.Size(39, 16);
            this.label17.TabIndex = 65;
            this.label17.Text = "الزيادة";
            // 
            // txtPlus
            // 
            this.txtPlus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPlus.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPlus.Location = new System.Drawing.Point(482, 53);
            this.txtPlus.Name = "txtPlus";
            this.txtPlus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPlus.Size = new System.Drawing.Size(98, 24);
            this.txtPlus.TabIndex = 64;
            this.txtPlus.Text = "0";
            // 
            // customPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 227);
            this.Controls.Add(this.panContent);
            this.Name = "customPanel";
            this.Text = "customPanel";
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Button btnNewPlus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDes;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPlus;
    }
}