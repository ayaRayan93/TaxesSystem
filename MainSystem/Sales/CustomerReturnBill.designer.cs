namespace MainSystem
{
    partial class CustomerReturnBill
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
            this.labBillNumber = new System.Windows.Forms.Label();
            this.comBillNumber = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalMeter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labTotalAD = new System.Windows.Forms.Label();
            this.btnAddToReturnBill = new System.Windows.Forms.Button();
            this.labBillDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labBillTotalCostAD = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPriceAD = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labTotalReturnBillAD = new System.Windows.Forms.Label();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateReturnBill = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnDelete = new Bunifu.Framework.UI.BunifuTileButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.Data_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtClientID);
            this.groupBox2.Controls.Add(this.comCustomer);
            this.groupBox2.Controls.Add(this.comClient);
            this.groupBox2.Controls.Add(this.labClient);
            this.groupBox2.Controls.Add(this.labCustomer);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Location = new System.Drawing.Point(62, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 67);
            this.groupBox2.TabIndex = 160;
            this.groupBox2.TabStop = false;
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(20, 39);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtClientID.Size = new System.Drawing.Size(48, 20);
            this.txtClientID.TabIndex = 106;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comCustomer
            // 
            this.comCustomer.FormattingEnabled = true;
            this.comCustomer.Location = new System.Drawing.Point(89, 13);
            this.comCustomer.Name = "comCustomer";
            this.comCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comCustomer.Size = new System.Drawing.Size(159, 21);
            this.comCustomer.TabIndex = 104;
            this.comCustomer.SelectedValueChanged += new System.EventHandler(this.comCustomer_SelectedValueChanged);
            // 
            // comClient
            // 
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(89, 39);
            this.comClient.Name = "comClient";
            this.comClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comClient.Size = new System.Drawing.Size(159, 21);
            this.comClient.TabIndex = 107;
            this.comClient.SelectedValueChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // labClient
            // 
            this.labClient.AutoSize = true;
            this.labClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labClient.Location = new System.Drawing.Point(278, 39);
            this.labClient.Name = "labClient";
            this.labClient.Size = new System.Drawing.Size(40, 17);
            this.labClient.TabIndex = 108;
            this.labClient.Text = "عميل";
            // 
            // labCustomer
            // 
            this.labCustomer.AutoSize = true;
            this.labCustomer.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labCustomer.Location = new System.Drawing.Point(278, 13);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(91, 17);
            this.labCustomer.TabIndex = 105;
            this.labCustomer.Text = "مهندس/مقاول";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(20, 13);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCustomerID.Size = new System.Drawing.Size(48, 20);
            this.txtCustomerID.TabIndex = 103;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radClient
            // 
            this.radClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radClient.AutoSize = true;
            this.radClient.Location = new System.Drawing.Point(183, 3);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(51, 17);
            this.radClient.TabIndex = 159;
            this.radClient.Text = "عميل";
            this.radClient.UseVisualStyleBackColor = true;
            this.radClient.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radEng
            // 
            this.radEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radEng.AutoSize = true;
            this.radEng.Location = new System.Drawing.Point(366, 3);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(60, 17);
            this.radEng.TabIndex = 158;
            this.radEng.Text = "مهندس";
            this.radEng.UseVisualStyleBackColor = true;
            this.radEng.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radCon
            // 
            this.radCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radCon.AutoSize = true;
            this.radCon.Location = new System.Drawing.Point(275, 3);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(52, 17);
            this.radCon.TabIndex = 157;
            this.radCon.Text = "مقاول";
            this.radCon.UseVisualStyleBackColor = true;
            this.radCon.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // labBillNumber
            // 
            this.labBillNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labBillNumber.AutoSize = true;
            this.labBillNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labBillNumber.Location = new System.Drawing.Point(259, 56);
            this.labBillNumber.Name = "labBillNumber";
            this.labBillNumber.Size = new System.Drawing.Size(65, 17);
            this.labBillNumber.TabIndex = 161;
            this.labBillNumber.Text = "فاتورة رقم";
            // 
            // comBillNumber
            // 
            this.comBillNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comBillNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBillNumber.FormattingEnabled = true;
            this.comBillNumber.Location = new System.Drawing.Point(169, 54);
            this.comBillNumber.Name = "comBillNumber";
            this.comBillNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBillNumber.Size = new System.Drawing.Size(74, 24);
            this.comBillNumber.TabIndex = 162;
            this.comBillNumber.SelectedValueChanged += new System.EventHandler(this.comBillNumber_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1044, 166);
            this.dataGridView1.TabIndex = 163;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data_ID,
            this.Code,
            this.Quantity,
            this.priceAD,
            this.totalAD,
            this.Product_Name,
            this.Type_Name,
            this.Factory_Name,
            this.Group_Name,
            this.Colour,
            this.Size,
            this.Sort,
            this.Classification,
            this.Description});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 405);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.Size = new System.Drawing.Size(1044, 166);
            this.dataGridView2.TabIndex = 164;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label6.Location = new System.Drawing.Point(515, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 18);
            this.label6.TabIndex = 172;
            this.label6.Text = "الاجمالي بعد الخصم";
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Location = new System.Drawing.Point(672, 14);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCode.Size = new System.Drawing.Size(178, 25);
            this.txtCode.TabIndex = 165;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.Location = new System.Drawing.Point(939, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 166;
            this.label1.Text = "الكود";
            // 
            // txtTotalMeter
            // 
            this.txtTotalMeter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalMeter.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalMeter.Location = new System.Drawing.Point(355, 14);
            this.txtTotalMeter.Name = "txtTotalMeter";
            this.txtTotalMeter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalMeter.Size = new System.Drawing.Size(145, 25);
            this.txtTotalMeter.TabIndex = 167;
            this.txtTotalMeter.TextChanged += new System.EventHandler(this.txtTotalMeter_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label2.Location = new System.Drawing.Point(515, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 18);
            this.label2.TabIndex = 168;
            this.label2.Text = "اجمالي عدد الامتار";
            // 
            // labTotalAD
            // 
            this.labTotalAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labTotalAD.AutoSize = true;
            this.labTotalAD.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labTotalAD.Location = new System.Drawing.Point(364, 53);
            this.labTotalAD.Name = "labTotalAD";
            this.labTotalAD.Size = new System.Drawing.Size(0, 18);
            this.labTotalAD.TabIndex = 171;
            // 
            // btnAddToReturnBill
            // 
            this.btnAddToReturnBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToReturnBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddToReturnBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToReturnBill.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAddToReturnBill.ForeColor = System.Drawing.Color.White;
            this.btnAddToReturnBill.Location = new System.Drawing.Point(35, 27);
            this.btnAddToReturnBill.Name = "btnAddToReturnBill";
            this.btnAddToReturnBill.Size = new System.Drawing.Size(157, 41);
            this.btnAddToReturnBill.TabIndex = 173;
            this.btnAddToReturnBill.Text = "اضف الي فاتورة المرتجع";
            this.btnAddToReturnBill.UseVisualStyleBackColor = false;
            this.btnAddToReturnBill.Click += new System.EventHandler(this.btnAddToReturnBill_Click);
            // 
            // labBillDate
            // 
            this.labBillDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labBillDate.AutoSize = true;
            this.labBillDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labBillDate.Location = new System.Drawing.Point(265, 6);
            this.labBillDate.Name = "labBillDate";
            this.labBillDate.Size = new System.Drawing.Size(0, 17);
            this.labBillDate.TabIndex = 178;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(412, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 177;
            this.label4.Text = "التاريخ";
            // 
            // labBillTotalCostAD
            // 
            this.labBillTotalCostAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labBillTotalCostAD.AutoSize = true;
            this.labBillTotalCostAD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labBillTotalCostAD.Location = new System.Drawing.Point(533, 8);
            this.labBillTotalCostAD.Name = "labBillTotalCostAD";
            this.labBillTotalCostAD.Size = new System.Drawing.Size(0, 17);
            this.labBillTotalCostAD.TabIndex = 176;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(635, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 17);
            this.label5.TabIndex = 175;
            this.label5.Text = "اجمالي الفاتورة بعد الخصم";
            // 
            // txtPriceAD
            // 
            this.txtPriceAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPriceAD.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPriceAD.Location = new System.Drawing.Point(672, 43);
            this.txtPriceAD.Name = "txtPriceAD";
            this.txtPriceAD.ReadOnly = true;
            this.txtPriceAD.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPriceAD.Size = new System.Drawing.Size(178, 25);
            this.txtPriceAD.TabIndex = 185;
            this.txtPriceAD.TextChanged += new System.EventHandler(this.txtTotalMeter_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label7.Location = new System.Drawing.Point(863, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 18);
            this.label7.TabIndex = 186;
            this.label7.Text = "السعر بعد الخصم";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(695, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(200, 17);
            this.label13.TabIndex = 191;
            this.label13.Text = "اجمالي فاتورة المرتجع بعد الخصم";
            // 
            // labTotalReturnBillAD
            // 
            this.labTotalReturnBillAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labTotalReturnBillAD.AutoSize = true;
            this.labTotalReturnBillAD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalReturnBillAD.Location = new System.Drawing.Point(576, 12);
            this.labTotalReturnBillAD.Name = "labTotalReturnBillAD";
            this.labTotalReturnBillAD.Size = new System.Drawing.Size(0, 17);
            this.labTotalReturnBillAD.TabIndex = 192;
            // 
            // comBranch
            // 
            this.comBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(169, 13);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(140, 24);
            this.comBranch.TabIndex = 194;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(115, 14);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBranchID.Size = new System.Drawing.Size(48, 24);
            this.txtBranchID.TabIndex = 196;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchID_KeyDown);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(327, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 17);
            this.label10.TabIndex = 195;
            this.label10.Text = "الفرع";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label14.Location = new System.Drawing.Point(406, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 17);
            this.label14.TabIndex = 197;
            this.label14.Text = "سبب الاسترجاع";
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfo.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtInfo.Location = new System.Drawing.Point(149, 12);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtInfo.Size = new System.Drawing.Size(244, 42);
            this.txtInfo.TabIndex = 198;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1050, 706);
            this.tableLayoutPanel1.TabIndex = 200;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1044, 94);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.radCon);
            this.panel1.Controls.Add(this.radEng);
            this.panel1.Controls.Add(this.radClient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(525, 3);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(516, 88);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comBillNumber);
            this.panel2.Controls.Add(this.labBillNumber);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtBranchID);
            this.panel2.Controls.Add(this.comBranch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(516, 88);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.labBillTotalCostAD);
            this.panel3.Controls.Add(this.labBillDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 103);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1044, 34);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnAddToReturnBill);
            this.panel4.Controls.Add(this.labTotalAD);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtTotalMeter);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txtCode);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.txtPriceAD);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 315);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1044, 84);
            this.panel4.TabIndex = 164;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtInfo);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.labTotalReturnBillAD);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 577);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1044, 64);
            this.panel5.TabIndex = 165;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCreateReturnBill, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 647);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1044, 56);
            this.tableLayoutPanel3.TabIndex = 166;
            // 
            // btnCreateReturnBill
            // 
            this.btnCreateReturnBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCreateReturnBill.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCreateReturnBill.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCreateReturnBill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateReturnBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreateReturnBill.Font = new System.Drawing.Font("Neo Sans Arabic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReturnBill.ForeColor = System.Drawing.Color.White;
            this.btnCreateReturnBill.Image = global::MainSystem.Properties.Resources.File_32;
            this.btnCreateReturnBill.ImagePosition = 2;
            this.btnCreateReturnBill.ImageZoom = 25;
            this.btnCreateReturnBill.LabelPosition = 20;
            this.btnCreateReturnBill.LabelText = "حفظ";
            this.btnCreateReturnBill.Location = new System.Drawing.Point(526, 4);
            this.btnCreateReturnBill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateReturnBill.Name = "btnCreateReturnBill";
            this.btnCreateReturnBill.Size = new System.Drawing.Size(98, 48);
            this.btnCreateReturnBill.TabIndex = 6;
            this.btnCreateReturnBill.Click += new System.EventHandler(this.btnCreateReturnBill_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Neo Sans Arabic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = global::MainSystem.Properties.Resources.File_32;
            this.btnDelete.ImagePosition = 2;
            this.btnDelete.ImageZoom = 25;
            this.btnDelete.LabelPosition = 20;
            this.btnDelete.LabelText = "حذف";
            this.btnDelete.Location = new System.Drawing.Point(422, 4);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(98, 48);
            this.btnDelete.TabIndex = 200;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(100, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButton1.Size = new System.Drawing.Size(43, 17);
            this.radioButton1.TabIndex = 161;
            this.radioButton1.Text = "تاجر";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // Data_ID
            // 
            this.Data_ID.HeaderText = "Data_ID";
            this.Data_ID.Name = "Data_ID";
            this.Data_ID.Visible = false;
            // 
            // Code
            // 
            this.Code.HeaderText = "الكود";
            this.Code.Name = "Code";
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "اجمالي عدد الامتار";
            this.Quantity.Name = "Quantity";
            // 
            // priceAD
            // 
            this.priceAD.HeaderText = "السعر";
            this.priceAD.Name = "priceAD";
            // 
            // totalAD
            // 
            this.totalAD.HeaderText = "الاجمالي";
            this.totalAD.Name = "totalAD";
            // 
            // Product_Name
            // 
            this.Product_Name.HeaderText = "الصنف";
            this.Product_Name.Name = "Product_Name";
            // 
            // Type_Name
            // 
            this.Type_Name.HeaderText = "النوع";
            this.Type_Name.Name = "Type_Name";
            // 
            // Factory_Name
            // 
            this.Factory_Name.HeaderText = "المصنع";
            this.Factory_Name.Name = "Factory_Name";
            // 
            // Group_Name
            // 
            this.Group_Name.HeaderText = "المجموعة";
            this.Group_Name.Name = "Group_Name";
            // 
            // Colour
            // 
            this.Colour.HeaderText = "اللون";
            this.Colour.Name = "Colour";
            // 
            // Size
            // 
            this.Size.HeaderText = "المقاس";
            this.Size.Name = "Size";
            // 
            // Sort
            // 
            this.Sort.HeaderText = "الفرز";
            this.Sort.Name = "Sort";
            // 
            // Classification
            // 
            this.Classification.HeaderText = "التصنيف";
            this.Classification.Name = "Classification";
            // 
            // Description
            // 
            this.Description.HeaderText = "الوصف";
            this.Description.Name = "Description";
            // 
            // CustomerReturnBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 706);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "CustomerReturnBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerReturnBill";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CustomerReturnBill_FormClosed);
            this.Load += new System.EventHandler(this.CustomerReturnBill_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label labBillNumber;
        private System.Windows.Forms.ComboBox comBillNumber;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalMeter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labTotalAD;
        private System.Windows.Forms.Button btnAddToReturnBill;
        private System.Windows.Forms.Label labBillDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labBillTotalCostAD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPriceAD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labTotalReturnBillAD;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Bunifu.Framework.UI.BunifuTileButton btnCreateReturnBill;
        private Bunifu.Framework.UI.BunifuTileButton btnDelete;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colour;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}