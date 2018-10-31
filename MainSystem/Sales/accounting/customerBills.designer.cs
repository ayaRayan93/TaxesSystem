namespace MainSystem
{
    partial class customerBills
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.comCustomer = new System.Windows.Forms.ComboBox();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.labClient = new System.Windows.Forms.Label();
            this.labCustomer = new System.Windows.Forms.Label();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.radClient = new System.Windows.Forms.RadioButton();
            this.radEng = new System.Windows.Forms.RadioButton();
            this.radCon = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labTotalBillCost = new System.Windows.Forms.Label();
            this.labTotalReturnCost = new System.Windows.Forms.Label();
            this.labRest = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panHeader = new System.Windows.Forms.Panel();
            this.panFooter = new System.Windows.Forms.Panel();
            this.Bill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.txtClientID);
            this.groupBox2.Controls.Add(this.comCustomer);
            this.groupBox2.Controls.Add(this.comClient);
            this.groupBox2.Controls.Add(this.labClient);
            this.groupBox2.Controls.Add(this.labCustomer);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Location = new System.Drawing.Point(479, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 65);
            this.groupBox2.TabIndex = 156;
            this.groupBox2.TabStop = false;
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(20, 39);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(48, 20);
            this.txtClientID.TabIndex = 106;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comCustomer
            // 
            this.comCustomer.FormattingEnabled = true;
            this.comCustomer.Location = new System.Drawing.Point(89, 13);
            this.comCustomer.Name = "comCustomer";
            this.comCustomer.Size = new System.Drawing.Size(159, 21);
            this.comCustomer.TabIndex = 104;
            this.comCustomer.SelectedValueChanged += new System.EventHandler(this.comCustomer_SelectedValueChanged);
            // 
            // comClient
            // 
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(89, 39);
            this.comClient.Name = "comClient";
            this.comClient.Size = new System.Drawing.Size(159, 21);
            this.comClient.TabIndex = 107;
            this.comClient.SelectedValueChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // labClient
            // 
            this.labClient.AutoSize = true;
            this.labClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labClient.Location = new System.Drawing.Point(278, 39);
            this.labClient.Name = "labClient";
            this.labClient.Size = new System.Drawing.Size(35, 16);
            this.labClient.TabIndex = 108;
            this.labClient.Text = "عميل";
            // 
            // labCustomer
            // 
            this.labCustomer.AutoSize = true;
            this.labCustomer.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCustomer.Location = new System.Drawing.Point(278, 13);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(85, 16);
            this.labCustomer.TabIndex = 105;
            this.labCustomer.Text = "مهندس/مقاول";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(20, 13);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(48, 20);
            this.txtCustomerID.TabIndex = 103;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radClient
            // 
            this.radClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radClient.AutoSize = true;
            this.radClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radClient.Location = new System.Drawing.Point(545, 27);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(53, 20);
            this.radClient.TabIndex = 155;
            this.radClient.Text = "عميل";
            this.radClient.UseVisualStyleBackColor = true;
            this.radClient.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radEng
            // 
            this.radEng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radEng.AutoSize = true;
            this.radEng.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEng.Location = new System.Drawing.Point(738, 28);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(65, 20);
            this.radEng.TabIndex = 154;
            this.radEng.Text = "مهندس";
            this.radEng.UseVisualStyleBackColor = true;
            this.radEng.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radCon
            // 
            this.radCon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radCon.AutoSize = true;
            this.radCon.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCon.Location = new System.Drawing.Point(647, 28);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(59, 20);
            this.radCon.TabIndex = 153;
            this.radCon.Text = "مقاول";
            this.radCon.UseVisualStyleBackColor = true;
            this.radCon.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(405, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 160;
            this.label3.Text = "الي";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(406, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 159;
            this.label2.Text = "من";
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeFrom.Location = new System.Drawing.Point(198, 56);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFrom.TabIndex = 158;
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeTo.Location = new System.Drawing.Point(198, 92);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimeTo.TabIndex = 157;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Bill,
            this.ReturnBill,
            this.BillNumber,
            this.Customer,
            this.Client,
            this.Date});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 153);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.Size = new System.Drawing.Size(956, 349);
            this.dataGridView1.TabIndex = 161;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(60, 72);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 26);
            this.btnSearch.TabIndex = 162;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labTotalBillCost
            // 
            this.labTotalBillCost.AutoSize = true;
            this.labTotalBillCost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalBillCost.Location = new System.Drawing.Point(796, 463);
            this.labTotalBillCost.Name = "labTotalBillCost";
            this.labTotalBillCost.Size = new System.Drawing.Size(0, 17);
            this.labTotalBillCost.TabIndex = 163;
            // 
            // labTotalReturnCost
            // 
            this.labTotalReturnCost.AutoSize = true;
            this.labTotalReturnCost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalReturnCost.Location = new System.Drawing.Point(704, 463);
            this.labTotalReturnCost.Name = "labTotalReturnCost";
            this.labTotalReturnCost.Size = new System.Drawing.Size(0, 17);
            this.labTotalReturnCost.TabIndex = 164;
            // 
            // labRest
            // 
            this.labRest.AutoSize = true;
            this.labRest.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labRest.Location = new System.Drawing.Point(205, 14);
            this.labRest.Name = "labRest";
            this.labRest.Size = new System.Drawing.Size(0, 17);
            this.labRest.TabIndex = 165;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(295, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 166;
            this.label6.Text = "الباقي";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panFooter, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(962, 565);
            this.tableLayoutPanel1.TabIndex = 167;
            // 
            // panHeader
            // 
            this.panHeader.AutoScroll = true;
            this.panHeader.Controls.Add(this.label3);
            this.panHeader.Controls.Add(this.radCon);
            this.panHeader.Controls.Add(this.radEng);
            this.panHeader.Controls.Add(this.radClient);
            this.panHeader.Controls.Add(this.groupBox2);
            this.panHeader.Controls.Add(this.btnSearch);
            this.panHeader.Controls.Add(this.dateTimeTo);
            this.panHeader.Controls.Add(this.dateTimeFrom);
            this.panHeader.Controls.Add(this.label2);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHeader.Location = new System.Drawing.Point(3, 3);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(956, 144);
            this.panHeader.TabIndex = 162;
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.label6);
            this.panFooter.Controls.Add(this.labRest);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFooter.Location = new System.Drawing.Point(3, 508);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(956, 54);
            this.panFooter.TabIndex = 163;
            // 
            // Bill
            // 
            this.Bill.HeaderText = "فواتير";
            this.Bill.Name = "Bill";
            // 
            // ReturnBill
            // 
            this.ReturnBill.HeaderText = "المرتجعات";
            this.ReturnBill.Name = "ReturnBill";
            // 
            // BillNumber
            // 
            this.BillNumber.HeaderText = "رقم الفاتورة";
            this.BillNumber.Name = "BillNumber";
            this.BillNumber.Width = 120;
            // 
            // Customer
            // 
            this.Customer.HeaderText = "مهندس/مقاول";
            this.Customer.Name = "Customer";
            // 
            // Client
            // 
            this.Client.HeaderText = "عميل";
            this.Client.Name = "Client";
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Date.HeaderText = "تاريخ";
            this.Date.Name = "Date";
            // 
            // customerBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(962, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labTotalReturnCost);
            this.Controls.Add(this.labTotalBillCost);
            this.MaximizeBox = false;
            this.Name = "customerBills";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "customerBills";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.ComboBox comCustomer;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label labClient;
        private System.Windows.Forms.Label labCustomer;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.RadioButton radClient;
        private System.Windows.Forms.RadioButton radEng;
        private System.Windows.Forms.RadioButton radCon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labTotalBillCost;
        private System.Windows.Forms.Label labTotalReturnCost;
        private System.Windows.Forms.Label labRest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
    }
}