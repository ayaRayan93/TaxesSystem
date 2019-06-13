namespace MainSystem
{
    partial class CustomerDelivery
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
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.labPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.labClient = new System.Windows.Forms.Label();
            this.labCustomer = new System.Windows.Forms.Label();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPut = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnRemove = new Bunifu.Framework.UI.BunifuImageButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRecivedQuantity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comStorePlace = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtPermBillNumber = new System.Windows.Forms.TextBox();
            this.labDescription = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bunifuTileButton1 = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnPrint = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labStoreName = new System.Windows.Forms.Label();
            this.radioBtnDriverDelivery = new System.Windows.Forms.RadioButton();
            this.radioBtnCustomerDelivery = new System.Windows.Forms.RadioButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Data_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DeliveryQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Carton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NumOfCarton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(228, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 201;
            this.label1.Text = "المخزن";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePicker1.Location = new System.Drawing.Point(28, 43);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(194, 20);
            this.dateTimePicker1.TabIndex = 207;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(228, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 208;
            this.label3.Text = "التاريخ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.labPhoneNumber);
            this.groupBox1.Controls.Add(this.txtPhoneNumber);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(9, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(972, 94);
            this.groupBox1.TabIndex = 210;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات الفاتورة";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(301, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 216;
            this.label6.Text = "العنوان";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAddress.BackColor = System.Drawing.SystemColors.Control;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAddress.Location = new System.Drawing.Point(123, 55);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(174, 24);
            this.txtAddress.TabIndex = 215;
            // 
            // labPhoneNumber
            // 
            this.labPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labPhoneNumber.AutoSize = true;
            this.labPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labPhoneNumber.Location = new System.Drawing.Point(301, 23);
            this.labPhoneNumber.Name = "labPhoneNumber";
            this.labPhoneNumber.Size = new System.Drawing.Size(72, 17);
            this.labPhoneNumber.TabIndex = 214;
            this.labPhoneNumber.Text = "رقم التلفون";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPhoneNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPhoneNumber.Location = new System.Drawing.Point(123, 22);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.ReadOnly = true;
            this.txtPhoneNumber.Size = new System.Drawing.Size(174, 24);
            this.txtPhoneNumber.TabIndex = 213;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.txtClientName);
            this.groupBox2.Controls.Add(this.txtCustomerName);
            this.groupBox2.Controls.Add(this.txtClientID);
            this.groupBox2.Controls.Add(this.labClient);
            this.groupBox2.Controls.Add(this.labCustomer);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Location = new System.Drawing.Point(409, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 65);
            this.groupBox2.TabIndex = 161;
            this.groupBox2.TabStop = false;
            // 
            // txtClientName
            // 
            this.txtClientName.BackColor = System.Drawing.SystemColors.Control;
            this.txtClientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientName.Location = new System.Drawing.Point(84, 38);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.ReadOnly = true;
            this.txtClientName.Size = new System.Drawing.Size(172, 20);
            this.txtClientName.TabIndex = 110;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.SystemColors.Control;
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Location = new System.Drawing.Point(84, 13);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(172, 20);
            this.txtCustomerName.TabIndex = 109;
            // 
            // txtClientID
            // 
            this.txtClientID.BackColor = System.Drawing.SystemColors.Control;
            this.txtClientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientID.Location = new System.Drawing.Point(20, 38);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.ReadOnly = true;
            this.txtClientID.Size = new System.Drawing.Size(48, 20);
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
            this.txtCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerID.Location = new System.Drawing.Point(20, 13);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.ReadOnly = true;
            this.txtCustomerID.Size = new System.Drawing.Size(48, 20);
            this.txtCustomerID.TabIndex = 103;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.btnPut);
            this.groupBox3.Controls.Add(this.btnPrint);
            this.groupBox3.Controls.Add(this.btnRemove);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtRecivedQuantity);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comStorePlace);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtCode);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox3.Location = new System.Drawing.Point(3, 415);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox3.Size = new System.Drawing.Size(990, 84);
            this.groupBox3.TabIndex = 227;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "بيانات البند";
            // 
            // btnPut
            // 
            this.btnPut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPut.BackColor = System.Drawing.Color.Transparent;
            this.btnPut.Image = global::MainSystem.Properties.Resources.Expand_Arrow_64px;
            this.btnPut.ImageActive = null;
            this.btnPut.Location = new System.Drawing.Point(146, 39);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(57, 34);
            this.btnPut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPut.TabIndex = 228;
            this.btnPut.TabStop = false;
            this.btnPut.Zoom = 10;
            this.btnPut.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.Image = global::MainSystem.Properties.Resources.closeIcon;
            this.btnRemove.ImageActive = null;
            this.btnRemove.Location = new System.Drawing.Point(212, 40);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(59, 34);
            this.btnRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRemove.TabIndex = 227;
            this.btnRemove.TabStop = false;
            this.btnRemove.Zoom = 10;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label11.Location = new System.Drawing.Point(754, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 19);
            this.label11.TabIndex = 224;
            this.label11.Text = "الكمية المستلمة";
            // 
            // txtRecivedQuantity
            // 
            this.txtRecivedQuantity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtRecivedQuantity.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtRecivedQuantity.Location = new System.Drawing.Point(644, 49);
            this.txtRecivedQuantity.Name = "txtRecivedQuantity";
            this.txtRecivedQuantity.Size = new System.Drawing.Size(104, 27);
            this.txtRecivedQuantity.TabIndex = 223;
            this.txtRecivedQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecivedQuantity_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.Location = new System.Drawing.Point(438, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 19);
            this.label8.TabIndex = 220;
            this.label8.Text = "اماكن التخزين";
            // 
            // comStorePlace
            // 
            this.comStorePlace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comStorePlace.Font = new System.Drawing.Font("Tahoma", 12F);
            this.comStorePlace.FormattingEnabled = true;
            this.comStorePlace.Location = new System.Drawing.Point(289, 42);
            this.comStorePlace.Name = "comStorePlace";
            this.comStorePlace.Size = new System.Drawing.Size(146, 27);
            this.comStorePlace.TabIndex = 219;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(754, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 19);
            this.label7.TabIndex = 218;
            this.label7.Text = "الكود";
            // 
            // txtCode
            // 
            this.txtCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtCode.Location = new System.Drawing.Point(543, 17);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(205, 27);
            this.txtCode.TabIndex = 217;
            // 
            // txtPermBillNumber
            // 
            this.txtPermBillNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPermBillNumber.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPermBillNumber.Location = new System.Drawing.Point(417, 28);
            this.txtPermBillNumber.Name = "txtPermBillNumber";
            this.txtPermBillNumber.Size = new System.Drawing.Size(146, 27);
            this.txtPermBillNumber.TabIndex = 227;
            this.txtPermBillNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNumber_KeyDown);
            // 
            // labDescription
            // 
            this.labDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labDescription.AutoSize = true;
            this.labDescription.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labDescription.Location = new System.Drawing.Point(569, 34);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(56, 17);
            this.labDescription.TabIndex = 236;
            this.labDescription.Text = "اذن رقم ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControl2, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 785);
            this.tableLayoutPanel1.TabIndex = 237;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.6085F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.84746F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.45763F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 414F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.bunifuTileButton1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 737);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(990, 45);
            this.tableLayoutPanel2.TabIndex = 211;
            // 
            // bunifuTileButton1
            // 
            this.bunifuTileButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTileButton1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuTileButton1.ForeColor = System.Drawing.Color.White;
            this.bunifuTileButton1.Image = global::MainSystem.Properties.Resources.Save_32;
            this.bunifuTileButton1.ImagePosition = 1;
            this.bunifuTileButton1.ImageZoom = 20;
            this.bunifuTileButton1.LabelPosition = 18;
            this.bunifuTileButton1.LabelText = "حفظ";
            this.bunifuTileButton1.Location = new System.Drawing.Point(415, 0);
            this.bunifuTileButton1.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuTileButton1.Name = "bunifuTileButton1";
            this.bunifuTileButton1.Size = new System.Drawing.Size(100, 45);
            this.bunifuTileButton1.TabIndex = 1;
            this.bunifuTileButton1.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnPrint.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnPrint.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = global::MainSystem.Properties.Resources.Print_32;
            this.btnPrint.ImagePosition = 1;
            this.btnPrint.ImageZoom = 25;
            this.btnPrint.LabelPosition = 18;
            this.btnPrint.LabelText = "طباعة تقرير";
            this.btnPrint.Location = new System.Drawing.Point(27, 26);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(83, 45);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 94);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labStoreName);
            this.panel2.Controls.Add(this.radioBtnDriverDelivery);
            this.panel2.Controls.Add(this.radioBtnCustomerDelivery);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.labDescription);
            this.panel2.Controls.Add(this.txtPermBillNumber);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(990, 74);
            this.panel2.TabIndex = 230;
            // 
            // labStoreName
            // 
            this.labStoreName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labStoreName.AutoSize = true;
            this.labStoreName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labStoreName.Location = new System.Drawing.Point(122, 18);
            this.labStoreName.Name = "labStoreName";
            this.labStoreName.Size = new System.Drawing.Size(0, 19);
            this.labStoreName.TabIndex = 229;
            // 
            // radioBtnDriverDelivery
            // 
            this.radioBtnDriverDelivery.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioBtnDriverDelivery.AutoSize = true;
            this.radioBtnDriverDelivery.Checked = true;
            this.radioBtnDriverDelivery.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnDriverDelivery.Location = new System.Drawing.Point(820, 19);
            this.radioBtnDriverDelivery.Name = "radioBtnDriverDelivery";
            this.radioBtnDriverDelivery.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioBtnDriverDelivery.Size = new System.Drawing.Size(100, 20);
            this.radioBtnDriverDelivery.TabIndex = 238;
            this.radioBtnDriverDelivery.TabStop = true;
            this.radioBtnDriverDelivery.Text = "تسليم سائق";
            this.radioBtnDriverDelivery.UseVisualStyleBackColor = true;
            this.radioBtnDriverDelivery.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioBtnCustomerDelivery
            // 
            this.radioBtnCustomerDelivery.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioBtnCustomerDelivery.AutoSize = true;
            this.radioBtnCustomerDelivery.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnCustomerDelivery.Location = new System.Drawing.Point(823, 45);
            this.radioBtnCustomerDelivery.Name = "radioBtnCustomerDelivery";
            this.radioBtnCustomerDelivery.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioBtnCustomerDelivery.Size = new System.Drawing.Size(97, 20);
            this.radioBtnCustomerDelivery.TabIndex = 237;
            this.radioBtnCustomerDelivery.Text = "تسليم عميل";
            this.radioBtnCustomerDelivery.UseVisualStyleBackColor = true;
            this.radioBtnCustomerDelivery.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 183);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(990, 226);
            this.gridControl1.TabIndex = 231;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(3, 505);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(990, 226);
            this.gridControl2.TabIndex = 232;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Appearance.Row.Options.UseTextOptions = true;
            this.gridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Data_ID,
            this.Code,
            this.ItemName,
            this.Quantity,
            this.DeliveryQuantity,
            this.Carton,
            this.NumOfCarton});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView2_CellValueChanged);
            // 
            // Data_ID
            // 
            this.Data_ID.Caption = "Data_ID";
            this.Data_ID.FieldName = "Data_ID";
            this.Data_ID.Name = "Data_ID";
            // 
            // Code
            // 
            this.Code.Caption = "الكود";
            this.Code.FieldName = "Code";
            this.Code.Name = "Code";
            this.Code.OptionsColumn.AllowEdit = false;
            this.Code.Visible = true;
            this.Code.VisibleIndex = 0;
            // 
            // ItemName
            // 
            this.ItemName.Caption = "البند";
            this.ItemName.FieldName = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.OptionsColumn.AllowEdit = false;
            this.ItemName.Visible = true;
            this.ItemName.VisibleIndex = 1;
            // 
            // Quantity
            // 
            this.Quantity.Caption = "الكمية المطلوبة";
            this.Quantity.FieldName = "TotalQuantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 2;
            // 
            // DeliveryQuantity
            // 
            this.DeliveryQuantity.Caption = "الكمية المستلمة ب المتر/القطعة";
            this.DeliveryQuantity.FieldName = "DeliveryQuantity";
            this.DeliveryQuantity.Name = "DeliveryQuantity";
            this.DeliveryQuantity.Visible = true;
            this.DeliveryQuantity.VisibleIndex = 3;
            // 
            // Carton
            // 
            this.Carton.Caption = "الكرتنة";
            this.Carton.FieldName = "Carton";
            this.Carton.Name = "Carton";
            this.Carton.Visible = true;
            this.Carton.VisibleIndex = 4;
            // 
            // NumOfCarton
            // 
            this.NumOfCarton.Caption = "عدد الكراتين المستلمة";
            this.NumOfCarton.FieldName = "NumOfCarton";
            this.NumOfCarton.Name = "NumOfCarton";
            this.NumOfCarton.Visible = true;
            this.NumOfCarton.VisibleIndex = 5;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "عدد الكراتين المستلمة";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // CustomerDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(996, 785);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "CustomerDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.TextBox txtPermBillNumber;
        private System.Windows.Forms.Label labDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioBtnDriverDelivery;
        private System.Windows.Forms.RadioButton radioBtnCustomerDelivery;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton bunifuTileButton1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private Bunifu.Framework.UI.BunifuImageButton btnPut;
        private Bunifu.Framework.UI.BunifuImageButton btnRemove;
        private System.Windows.Forms.Label labStoreName;
        private DevExpress.XtraGrid.Columns.GridColumn Data_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Code;
        private DevExpress.XtraGrid.Columns.GridColumn ItemName;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn Carton;
        private Bunifu.Framework.UI.BunifuTileButton btnPrint;
        private DevExpress.XtraGrid.Columns.GridColumn DeliveryQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn NumOfCarton;
    }
}

