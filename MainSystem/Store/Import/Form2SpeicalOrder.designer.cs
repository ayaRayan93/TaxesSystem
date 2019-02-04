namespace MainSystem
{
    partial class Form2SpeicalOrder
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
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comStore = new System.Windows.Forms.ComboBox();
            this.txtStoreID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBillNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.labPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDelegateID = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.txtDelegateName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.labClient = new System.Windows.Forms.Label();
            this.labCustomer = new System.Windows.Forms.Label();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRecivedQuantity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comStorePlace = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecivedQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // comBranch
            // 
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(194, 21);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(140, 24);
            this.comBranch.TabIndex = 197;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(140, 22);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(48, 24);
            this.txtBranchID.TabIndex = 199;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(352, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 17);
            this.label10.TabIndex = 198;
            this.label10.Text = "الفرع";
            // 
            // comStore
            // 
            this.comStore.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comStore.FormattingEnabled = true;
            this.comStore.Location = new System.Drawing.Point(540, 21);
            this.comStore.Name = "comStore";
            this.comStore.Size = new System.Drawing.Size(140, 24);
            this.comStore.TabIndex = 200;
            this.comStore.SelectedValueChanged += new System.EventHandler(this.comStore_SelectedValueChanged);
            // 
            // txtStoreID
            // 
            this.txtStoreID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStoreID.Location = new System.Drawing.Point(486, 22);
            this.txtStoreID.Name = "txtStoreID";
            this.txtStoreID.Size = new System.Drawing.Size(48, 24);
            this.txtStoreID.TabIndex = 202;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(698, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 201;
            this.label1.Text = "المخزن";
            // 
            // txtBillNumber
            // 
            this.txtBillNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBillNumber.Location = new System.Drawing.Point(140, 52);
            this.txtBillNumber.Name = "txtBillNumber";
            this.txtBillNumber.Size = new System.Drawing.Size(140, 24);
            this.txtBillNumber.TabIndex = 203;
            this.txtBillNumber.Visible = false;
            this.txtBillNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNumber_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(298, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 204;
            this.label2.Text = "فاتورة رقم";
            this.label2.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 190);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1053, 167);
            this.dataGridView1.TabIndex = 205;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(486, 57);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(194, 20);
            this.dateTimePicker1.TabIndex = 207;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(698, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 208;
            this.label3.Text = "التاريخ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.labPhoneNumber);
            this.groupBox1.Controls.Add(this.txtPhoneNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDelegateID);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.txtDelegateName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(58, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(961, 99);
            this.groupBox1.TabIndex = 210;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات الفاتورة";
            this.groupBox1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(456, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 216;
            this.label6.Text = "العنوان";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Control;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAddress.Location = new System.Drawing.Point(304, 61);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(137, 17);
            this.txtAddress.TabIndex = 215;
            // 
            // labPhoneNumber
            // 
            this.labPhoneNumber.AutoSize = true;
            this.labPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labPhoneNumber.Location = new System.Drawing.Point(456, 32);
            this.labPhoneNumber.Name = "labPhoneNumber";
            this.labPhoneNumber.Size = new System.Drawing.Size(72, 17);
            this.labPhoneNumber.TabIndex = 214;
            this.labPhoneNumber.Text = "رقم التلفون";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPhoneNumber.Location = new System.Drawing.Point(304, 29);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.ReadOnly = true;
            this.txtPhoneNumber.Size = new System.Drawing.Size(137, 17);
            this.txtPhoneNumber.TabIndex = 213;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(210, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 212;
            this.label5.Text = "تاريخ الاستلام";
            // 
            // txtDelegateID
            // 
            this.txtDelegateID.BackColor = System.Drawing.SystemColors.Control;
            this.txtDelegateID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDelegateID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegateID.Location = new System.Drawing.Point(18, 28);
            this.txtDelegateID.Name = "txtDelegateID";
            this.txtDelegateID.ReadOnly = true;
            this.txtDelegateID.Size = new System.Drawing.Size(48, 17);
            this.txtDelegateID.TabIndex = 164;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(18, 62);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(194, 20);
            this.dateTimePicker2.TabIndex = 211;
            // 
            // txtDelegateName
            // 
            this.txtDelegateName.BackColor = System.Drawing.SystemColors.Control;
            this.txtDelegateName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDelegateName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegateName.Location = new System.Drawing.Point(72, 27);
            this.txtDelegateName.Name = "txtDelegateName";
            this.txtDelegateName.ReadOnly = true;
            this.txtDelegateName.Size = new System.Drawing.Size(140, 17);
            this.txtDelegateName.TabIndex = 163;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(230, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 162;
            this.label4.Text = "المندوب";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtClientName);
            this.groupBox2.Controls.Add(this.txtCustomerName);
            this.groupBox2.Controls.Add(this.txtClientID);
            this.groupBox2.Controls.Add(this.labClient);
            this.groupBox2.Controls.Add(this.labCustomer);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Location = new System.Drawing.Point(550, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 65);
            this.groupBox2.TabIndex = 161;
            this.groupBox2.TabStop = false;
            // 
            // txtClientName
            // 
            this.txtClientName.BackColor = System.Drawing.SystemColors.Control;
            this.txtClientName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClientName.Location = new System.Drawing.Point(84, 38);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.ReadOnly = true;
            this.txtClientName.Size = new System.Drawing.Size(172, 13);
            this.txtClientName.TabIndex = 110;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.SystemColors.Control;
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomerName.Location = new System.Drawing.Point(84, 13);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(172, 13);
            this.txtCustomerName.TabIndex = 109;
            // 
            // txtClientID
            // 
            this.txtClientID.BackColor = System.Drawing.SystemColors.Control;
            this.txtClientID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClientID.Location = new System.Drawing.Point(20, 39);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.ReadOnly = true;
            this.txtClientID.Size = new System.Drawing.Size(48, 13);
            this.txtClientID.TabIndex = 106;
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
            this.txtCustomerID.BackColor = System.Drawing.SystemColors.Control;
            this.txtCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomerID.Location = new System.Drawing.Point(20, 13);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.ReadOnly = true;
            this.txtCustomerID.Size = new System.Drawing.Size(48, 13);
            this.txtCustomerID.TabIndex = 103;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtRecivedQuantity);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comStorePlace);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtCode);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox3.Location = new System.Drawing.Point(23, 358);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox3.Size = new System.Drawing.Size(1032, 117);
            this.groupBox3.TabIndex = 211;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "بيانات البند";
            this.groupBox3.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnAdd.Location = new System.Drawing.Point(35, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 33);
            this.btnAdd.TabIndex = 226;
            this.btnAdd.Text = "اضافة";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label11.Location = new System.Drawing.Point(380, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 19);
            this.label11.TabIndex = 224;
            this.label11.Text = "الكمية المستلمة";
            // 
            // txtRecivedQuantity
            // 
            this.txtRecivedQuantity.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtRecivedQuantity.Location = new System.Drawing.Point(219, 62);
            this.txtRecivedQuantity.Name = "txtRecivedQuantity";
            this.txtRecivedQuantity.Size = new System.Drawing.Size(146, 27);
            this.txtRecivedQuantity.TabIndex = 223;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.Location = new System.Drawing.Point(575, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 19);
            this.label8.TabIndex = 220;
            this.label8.Text = "اماكن التخزين";
            // 
            // comStorePlace
            // 
            this.comStorePlace.Font = new System.Drawing.Font("Tahoma", 12F);
            this.comStorePlace.FormattingEnabled = true;
            this.comStorePlace.Location = new System.Drawing.Point(414, 19);
            this.comStorePlace.Name = "comStorePlace";
            this.comStorePlace.Size = new System.Drawing.Size(146, 27);
            this.comStorePlace.TabIndex = 219;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(865, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 19);
            this.label7.TabIndex = 218;
            this.label7.Text = "الكود";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtCode.Location = new System.Drawing.Point(645, 62);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(205, 27);
            this.txtCode.TabIndex = 217;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(58, 669);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 33);
            this.button1.TabIndex = 225;
            this.button1.Text = "تم التسليم";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(1026, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(17, 13);
            this.panel1.TabIndex = 212;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(1026, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(17, 13);
            this.panel2.TabIndex = 213;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.Location = new System.Drawing.Point(858, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 17);
            this.label9.TabIndex = 214;
            this.label9.Text = "تم تسليم هذا البند بالكامل";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(852, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(168, 17);
            this.label12.TabIndex = 215;
            this.label12.Text = "تم تسليم  جزء من هذا البند";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.RecivedQuantity,
            this.Type_Name,
            this.Factory_Name,
            this.Group_Name,
            this.Product_Name,
            this.Colour,
            this.Size,
            this.Sort,
            this.Classification,
            this.Description,
            this.finished});
            this.dataGridView2.Location = new System.Drawing.Point(23, 483);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.Size = new System.Drawing.Size(1040, 180);
            this.dataGridView2.TabIndex = 226;
            // 
            // Code
            // 
            this.Code.HeaderText = "الكود";
            this.Code.Name = "Code";
            // 
            // RecivedQuantity
            // 
            this.RecivedQuantity.HeaderText = "الكمية المستلمة";
            this.RecivedQuantity.Name = "RecivedQuantity";
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
            // finished
            // 
            this.finished.HeaderText = "تم تسليم الكمية ";
            this.finished.Name = "finished";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(880, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(149, 17);
            this.label13.TabIndex = 228;
            this.label13.Text = "لم يتم تسليم  هذا البند ";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(1026, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(17, 13);
            this.panel3.TabIndex = 227;
            // 
            // Form2SpeicalOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 708);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBillNumber);
            this.Controls.Add(this.comStore);
            this.Controls.Add(this.txtStoreID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comBranch);
            this.Controls.Add(this.txtBranchID);
            this.Controls.Add(this.label10);
            this.MaximizeBox = false;
            this.Name = "Form2SpeicalOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comStore;
        private System.Windows.Forms.TextBox txtStoreID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBillNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDelegateID;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox txtDelegateName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.Label labClient;
        private System.Windows.Forms.Label labCustomer;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label labPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRecivedQuantity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comStorePlace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecivedQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colour;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn finished;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
    }
}

