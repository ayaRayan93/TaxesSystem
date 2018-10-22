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
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalMeter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labTotalAD = new System.Windows.Forms.Label();
            this.btnAddToReturnBill = new System.Windows.Forms.Button();
            this.btnCreateReturnBill = new System.Windows.Forms.Button();
            this.labBillDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labBillTotalCostAD = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtPriceAD = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labTotalReturnBillAD = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtClientID);
            this.groupBox2.Controls.Add(this.comCustomer);
            this.groupBox2.Controls.Add(this.comClient);
            this.groupBox2.Controls.Add(this.labClient);
            this.groupBox2.Controls.Add(this.labCustomer);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Location = new System.Drawing.Point(547, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 65);
            this.groupBox2.TabIndex = 160;
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
            this.txtCustomerID.Size = new System.Drawing.Size(48, 20);
            this.txtCustomerID.TabIndex = 103;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radClient
            // 
            this.radClient.AutoSize = true;
            this.radClient.Location = new System.Drawing.Point(613, 7);
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
            this.radEng.AutoSize = true;
            this.radEng.Location = new System.Drawing.Point(806, 8);
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
            this.radCon.AutoSize = true;
            this.radCon.Location = new System.Drawing.Point(715, 8);
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
            this.labBillNumber.AutoSize = true;
            this.labBillNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labBillNumber.Location = new System.Drawing.Point(240, 74);
            this.labBillNumber.Name = "labBillNumber";
            this.labBillNumber.Size = new System.Drawing.Size(65, 17);
            this.labBillNumber.TabIndex = 161;
            this.labBillNumber.Text = "فاتورة رقم";
            // 
            // comBillNumber
            // 
            this.comBillNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBillNumber.FormattingEnabled = true;
            this.comBillNumber.Location = new System.Drawing.Point(150, 72);
            this.comBillNumber.Name = "comBillNumber";
            this.comBillNumber.Size = new System.Drawing.Size(74, 24);
            this.comBillNumber.TabIndex = 162;
            this.comBillNumber.SelectedValueChanged += new System.EventHandler(this.comBillNumber_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(42, 131);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(945, 169);
            this.dataGridView1.TabIndex = 163;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Quantity,
            this.priceAD,
            this.totalAD,
            this.Type_Name,
            this.Factory_Name,
            this.Group_Name,
            this.Product_Name,
            this.Colour,
            this.Size,
            this.Sort,
            this.Classification,
            this.Description});
            this.dataGridView2.Location = new System.Drawing.Point(42, 390);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.Size = new System.Drawing.Size(945, 209);
            this.dataGridView2.TabIndex = 164;
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
            // Product_Name
            // 
            this.Product_Name.HeaderText = "المنتج";
            this.Product_Name.Name = "Product_Name";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label6.Location = new System.Drawing.Point(522, 349);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 18);
            this.label6.TabIndex = 172;
            this.label6.Text = "الاجمالي بعد الخصم";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Location = new System.Drawing.Point(679, 313);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(178, 25);
            this.txtCode.TabIndex = 165;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.Location = new System.Drawing.Point(946, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 166;
            this.label1.Text = "الكود";
            // 
            // txtTotalMeter
            // 
            this.txtTotalMeter.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalMeter.Location = new System.Drawing.Point(362, 313);
            this.txtTotalMeter.Name = "txtTotalMeter";
            this.txtTotalMeter.Size = new System.Drawing.Size(145, 25);
            this.txtTotalMeter.TabIndex = 167;
            this.txtTotalMeter.TextChanged += new System.EventHandler(this.txtTotalMeter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label2.Location = new System.Drawing.Point(522, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 18);
            this.label2.TabIndex = 168;
            this.label2.Text = "اجمالي عدد الامتار";
            // 
            // labTotalAD
            // 
            this.labTotalAD.AutoSize = true;
            this.labTotalAD.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labTotalAD.Location = new System.Drawing.Point(371, 352);
            this.labTotalAD.Name = "labTotalAD";
            this.labTotalAD.Size = new System.Drawing.Size(0, 18);
            this.labTotalAD.TabIndex = 171;
            // 
            // btnAddToReturnBill
            // 
            this.btnAddToReturnBill.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAddToReturnBill.Location = new System.Drawing.Point(42, 335);
            this.btnAddToReturnBill.Name = "btnAddToReturnBill";
            this.btnAddToReturnBill.Size = new System.Drawing.Size(131, 32);
            this.btnAddToReturnBill.TabIndex = 173;
            this.btnAddToReturnBill.Text = "اضف الي فاتورة المرتجع";
            this.btnAddToReturnBill.UseVisualStyleBackColor = true;
            this.btnAddToReturnBill.Click += new System.EventHandler(this.btnAddToReturnBill_Click);
            // 
            // btnCreateReturnBill
            // 
            this.btnCreateReturnBill.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnCreateReturnBill.Location = new System.Drawing.Point(12, 661);
            this.btnCreateReturnBill.Name = "btnCreateReturnBill";
            this.btnCreateReturnBill.Size = new System.Drawing.Size(131, 38);
            this.btnCreateReturnBill.TabIndex = 174;
            this.btnCreateReturnBill.Text = "تسجيل فاتورة مرتجع";
            this.btnCreateReturnBill.UseVisualStyleBackColor = true;
            this.btnCreateReturnBill.Click += new System.EventHandler(this.btnCreateReturnBill_Click);
            // 
            // labBillDate
            // 
            this.labBillDate.AutoSize = true;
            this.labBillDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labBillDate.Location = new System.Drawing.Point(224, 111);
            this.labBillDate.Name = "labBillDate";
            this.labBillDate.Size = new System.Drawing.Size(0, 17);
            this.labBillDate.TabIndex = 178;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(394, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 177;
            this.label4.Text = "التاريخ";
            // 
            // labBillTotalCostAD
            // 
            this.labBillTotalCostAD.AutoSize = true;
            this.labBillTotalCostAD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labBillTotalCostAD.Location = new System.Drawing.Point(515, 111);
            this.labBillTotalCostAD.Name = "labBillTotalCostAD";
            this.labBillTotalCostAD.Size = new System.Drawing.Size(0, 17);
            this.labBillTotalCostAD.TabIndex = 176;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(617, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 17);
            this.label5.TabIndex = 175;
            this.label5.Text = "اجمالي الفاتورة بعد الخصم";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(535, 619);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 17);
            this.label8.TabIndex = 181;
            this.label8.Text = "التاريخ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(311, 619);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 182;
            // 
            // txtPriceAD
            // 
            this.txtPriceAD.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPriceAD.Location = new System.Drawing.Point(679, 342);
            this.txtPriceAD.Name = "txtPriceAD";
            this.txtPriceAD.ReadOnly = true;
            this.txtPriceAD.Size = new System.Drawing.Size(178, 25);
            this.txtPriceAD.TabIndex = 185;
            this.txtPriceAD.TextChanged += new System.EventHandler(this.txtTotalMeter_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label7.Location = new System.Drawing.Point(870, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 18);
            this.label7.TabIndex = 186;
            this.label7.Text = "السعر بعد الخصم";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(787, 622);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(200, 17);
            this.label13.TabIndex = 191;
            this.label13.Text = "اجمالي فاتورة المرتجع بعد الخصم";
            // 
            // labTotalReturnBillAD
            // 
            this.labTotalReturnBillAD.AutoSize = true;
            this.labTotalReturnBillAD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalReturnBillAD.Location = new System.Drawing.Point(668, 622);
            this.labTotalReturnBillAD.Name = "labTotalReturnBillAD";
            this.labTotalReturnBillAD.Size = new System.Drawing.Size(0, 17);
            this.labTotalReturnBillAD.TabIndex = 192;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnBack.Location = new System.Drawing.Point(940, 668);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(98, 31);
            this.btnBack.TabIndex = 193;
            this.btnBack.Text = "رجوع";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // comBranch
            // 
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(150, 31);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(140, 24);
            this.comBranch.TabIndex = 194;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(96, 32);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(48, 24);
            this.txtBranchID.TabIndex = 196;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchID_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(308, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 17);
            this.label10.TabIndex = 195;
            this.label10.Text = "الفرع";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label14.Location = new System.Drawing.Point(716, 657);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 17);
            this.label14.TabIndex = 197;
            this.label14.Text = "سبب الاسترجاع";
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtInfo.Location = new System.Drawing.Point(459, 657);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(244, 42);
            this.txtInfo.TabIndex = 198;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnDelete.Location = new System.Drawing.Point(42, 604);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(131, 32);
            this.btnDelete.TabIndex = 199;
            this.btnDelete.Text = "حذف من فاتورة المرتجع";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CustomerReturnBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 706);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comBranch);
            this.Controls.Add(this.txtBranchID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.labTotalReturnBillAD);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPriceAD);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labBillDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labBillTotalCostAD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCreateReturnBill);
            this.Controls.Add(this.btnAddToReturnBill);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalMeter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labTotalAD);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labBillNumber);
            this.Controls.Add(this.comBillNumber);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radClient);
            this.Controls.Add(this.radEng);
            this.Controls.Add(this.radCon);
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
        private System.Windows.Forms.Button btnCreateReturnBill;
        private System.Windows.Forms.Label labBillDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labBillTotalCostAD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtPriceAD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labTotalReturnBillAD;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colour;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnDelete;
    }
}