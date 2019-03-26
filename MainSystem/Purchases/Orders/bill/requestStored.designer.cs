namespace MainSystem
{
    partial class requestStored
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
            this.labBillNumber = new System.Windows.Forms.Label();
            this.txtBillNumber = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnMainForm = new System.Windows.Forms.Button();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // labBillNumber
            // 
            this.labBillNumber.AutoSize = true;
            this.labBillNumber.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labBillNumber.Location = new System.Drawing.Point(724, 62);
            this.labBillNumber.Name = "labBillNumber";
            this.labBillNumber.Size = new System.Drawing.Size(87, 19);
            this.labBillNumber.TabIndex = 5;
            this.labBillNumber.Text = "رقم الطلب";
            this.labBillNumber.Visible = false;
            // 
            // txtBillNumber
            // 
            this.txtBillNumber.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtBillNumber.Location = new System.Drawing.Point(554, 59);
            this.txtBillNumber.Name = "txtBillNumber";
            this.txtBillNumber.Size = new System.Drawing.Size(155, 27);
            this.txtBillNumber.TabIndex = 4;
            this.txtBillNumber.Visible = false;
            this.txtBillNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNumber_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(36, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(901, 195);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnDisplay.Location = new System.Drawing.Point(56, 33);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(132, 33);
            this.btnDisplay.TabIndex = 7;
            this.btnDisplay.Text = "عرض كل الطلابات";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnMainForm
            // 
            this.btnMainForm.Location = new System.Drawing.Point(730, 518);
            this.btnMainForm.Name = "btnMainForm";
            this.btnMainForm.Size = new System.Drawing.Size(183, 33);
            this.btnMainForm.TabIndex = 8;
            this.btnMainForm.Text = "الرجوع للصفحة الرئيسية";
            this.btnMainForm.UseVisualStyleBackColor = true;
            this.btnMainForm.Click += new System.EventHandler(this.btnMainForm_Click);
            // 
            // comBranch
            // 
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 12F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(608, 21);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(140, 27);
            this.comBranch.TabIndex = 203;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtBranchID.Location = new System.Drawing.Point(554, 22);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(48, 27);
            this.txtBranchID.TabIndex = 205;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchID_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(766, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 19);
            this.label16.TabIndex = 204;
            this.label16.Text = "الفرع";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(36, 313);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.Size = new System.Drawing.Size(901, 195);
            this.dataGridView2.TabIndex = 206;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(462, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 214;
            this.label2.Text = "الي";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(273, 62);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(183, 24);
            this.dateTimePicker2.TabIndex = 213;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(461, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 17);
            this.label3.TabIndex = 212;
            this.label3.Text = "من";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(273, 22);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(183, 24);
            this.dateTimePicker1.TabIndex = 211;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(36, 518);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(152, 33);
            this.btnReport.TabIndex = 215;
            this.btnReport.Text = "تقرير";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // requestStored
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 567);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.comBranch);
            this.Controls.Add(this.txtBranchID);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnMainForm);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labBillNumber);
            this.Controls.Add(this.txtBillNumber);
            this.MaximizeBox = false;
            this.Name = "requestStored";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.requestStored_FormClosed);
            this.Load += new System.EventHandler(this.requestStored_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labBillNumber;
        private System.Windows.Forms.TextBox txtBillNumber;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnMainForm;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnReport;
    }
}

