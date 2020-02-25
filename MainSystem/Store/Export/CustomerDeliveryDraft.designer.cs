namespace MainSystem
{
    partial class CustomerDeliveryDraft
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
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtDelegate = new System.Windows.Forms.TextBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.labPhoneNumber = new System.Windows.Forms.Label();
            this.labClient = new System.Windows.Forms.Label();
            this.labCustomer = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.labDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPermBillNumber = new System.Windows.Forms.TextBox();
            this.labDescription = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bunifuTileButton1 = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtStore = new System.Windows.Forms.TextBox();
            this.panBranch = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.comStore = new System.Windows.Forms.ComboBox();
            this.labStoreName = new System.Windows.Forms.Label();
            this.radioBtnDriverDelivery = new System.Windows.Forms.RadioButton();
            this.radioBtnCustomerDelivery = new System.Windows.Forms.RadioButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panBranch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(228, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 201;
            this.label1.Text = "المخزن";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(28, 39);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(194, 20);
            this.dateTimePicker1.TabIndex = 207;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(228, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 208;
            this.label3.Text = "التاريخ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Controls.Add(this.txtDelegate);
            this.groupBox1.Controls.Add(this.txtClientID);
            this.groupBox1.Controls.Add(this.labPhoneNumber);
            this.groupBox1.Controls.Add(this.labClient);
            this.groupBox1.Controls.Add(this.labCustomer);
            this.groupBox1.Controls.Add(this.txtPhoneNumber);
            this.groupBox1.Controls.Add(this.txtCustomerID);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(972, 72);
            this.groupBox1.TabIndex = 210;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات الفاتورة";
            // 
            // txtClientName
            // 
            this.txtClientName.BackColor = System.Drawing.SystemColors.Control;
            this.txtClientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientName.Location = new System.Drawing.Point(512, 41);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.ReadOnly = true;
            this.txtClientName.Size = new System.Drawing.Size(172, 20);
            this.txtClientName.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(356, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 218;
            this.label2.Text = "المندوب";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.SystemColors.Control;
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Location = new System.Drawing.Point(512, 16);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(172, 20);
            this.txtCustomerName.TabIndex = 109;
            // 
            // txtDelegate
            // 
            this.txtDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDelegate.BackColor = System.Drawing.SystemColors.Control;
            this.txtDelegate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDelegate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegate.Location = new System.Drawing.Point(178, 41);
            this.txtDelegate.Name = "txtDelegate";
            this.txtDelegate.ReadOnly = true;
            this.txtDelegate.Size = new System.Drawing.Size(174, 24);
            this.txtDelegate.TabIndex = 217;
            // 
            // txtClientID
            // 
            this.txtClientID.BackColor = System.Drawing.SystemColors.Control;
            this.txtClientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClientID.Location = new System.Drawing.Point(448, 41);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.ReadOnly = true;
            this.txtClientID.Size = new System.Drawing.Size(48, 20);
            this.txtClientID.TabIndex = 106;
            // 
            // labPhoneNumber
            // 
            this.labPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labPhoneNumber.AutoSize = true;
            this.labPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labPhoneNumber.Location = new System.Drawing.Point(356, 19);
            this.labPhoneNumber.Name = "labPhoneNumber";
            this.labPhoneNumber.Size = new System.Drawing.Size(72, 17);
            this.labPhoneNumber.TabIndex = 214;
            this.labPhoneNumber.Text = "رقم التلفون";
            // 
            // labClient
            // 
            this.labClient.AutoSize = true;
            this.labClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labClient.Location = new System.Drawing.Point(706, 41);
            this.labClient.Name = "labClient";
            this.labClient.Size = new System.Drawing.Size(40, 17);
            this.labClient.TabIndex = 108;
            this.labClient.Text = "عميل";
            // 
            // labCustomer
            // 
            this.labCustomer.AutoSize = true;
            this.labCustomer.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labCustomer.Location = new System.Drawing.Point(706, 19);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(91, 17);
            this.labCustomer.TabIndex = 105;
            this.labCustomer.Text = "مهندس/مقاول";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPhoneNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPhoneNumber.Location = new System.Drawing.Point(177, 12);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.ReadOnly = true;
            this.txtPhoneNumber.Size = new System.Drawing.Size(174, 24);
            this.txtPhoneNumber.TabIndex = 213;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BackColor = System.Drawing.SystemColors.Control;
            this.txtCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerID.Location = new System.Drawing.Point(448, 16);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.ReadOnly = true;
            this.txtCustomerID.Size = new System.Drawing.Size(48, 20);
            this.txtCustomerID.TabIndex = 103;
            // 
            // labDate
            // 
            this.labDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labDate.AutoSize = true;
            this.labDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labDate.Location = new System.Drawing.Point(353, 48);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(0, 17);
            this.labDate.TabIndex = 220;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(455, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 219;
            this.label4.Text = "تاريخ الفاتورة";
            // 
            // txtPermBillNumber
            // 
            this.txtPermBillNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPermBillNumber.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPermBillNumber.Location = new System.Drawing.Point(566, 42);
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
            this.labDescription.Location = new System.Drawing.Point(718, 48);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(65, 17);
            this.labDescription.TabIndex = 236;
            this.labDescription.Text = "فاتورة رقم";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 452F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.bunifuTileButton1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 738);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(990, 44);
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
            this.bunifuTileButton1.Image = global::MainSystem.Properties.Resources.Print_32;
            this.bunifuTileButton1.ImagePosition = 1;
            this.bunifuTileButton1.ImageZoom = 20;
            this.bunifuTileButton1.LabelPosition = 18;
            this.bunifuTileButton1.LabelText = "طباعة";
            this.bunifuTileButton1.Location = new System.Drawing.Point(453, 0);
            this.bunifuTileButton1.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuTileButton1.Name = "bunifuTileButton1";
            this.bunifuTileButton1.Size = new System.Drawing.Size(94, 44);
            this.bunifuTileButton1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 75);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labDate);
            this.panel2.Controls.Add(this.txtStore);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panBranch);
            this.panel2.Controls.Add(this.comStore);
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
            this.panel2.Size = new System.Drawing.Size(990, 92);
            this.panel2.TabIndex = 230;
            // 
            // txtStore
            // 
            this.txtStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtStore.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStore.Location = new System.Drawing.Point(28, 10);
            this.txtStore.Name = "txtStore";
            this.txtStore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtStore.Size = new System.Drawing.Size(58, 24);
            this.txtStore.TabIndex = 201;
            this.txtStore.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStore_KeyDown);
            // 
            // panBranch
            // 
            this.panBranch.Controls.Add(this.label10);
            this.panBranch.Controls.Add(this.txtBranchID);
            this.panBranch.Controls.Add(this.comBranch);
            this.panBranch.Location = new System.Drawing.Point(423, 6);
            this.panBranch.Name = "panBranch";
            this.panBranch.Size = new System.Drawing.Size(256, 33);
            this.panBranch.TabIndex = 239;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label10.Location = new System.Drawing.Point(205, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 18);
            this.label10.TabIndex = 198;
            this.label10.Text = "الفرع";
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(13, 5);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBranchID.Size = new System.Drawing.Size(50, 24);
            this.txtBranchID.TabIndex = 199;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchID_KeyDown);
            // 
            // comBranch
            // 
            this.comBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(69, 5);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(130, 24);
            this.comBranch.TabIndex = 197;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // comStore
            // 
            this.comStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comStore.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comStore.FormattingEnabled = true;
            this.comStore.Location = new System.Drawing.Point(92, 10);
            this.comStore.Name = "comStore";
            this.comStore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comStore.Size = new System.Drawing.Size(130, 24);
            this.comStore.TabIndex = 200;
            this.comStore.SelectedValueChanged += new System.EventHandler(this.comStore_SelectedValueChanged);
            // 
            // labStoreName
            // 
            this.labStoreName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labStoreName.AutoSize = true;
            this.labStoreName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labStoreName.Location = new System.Drawing.Point(122, 1);
            this.labStoreName.Name = "labStoreName";
            this.labStoreName.Size = new System.Drawing.Size(0, 19);
            this.labStoreName.TabIndex = 229;
            // 
            // radioBtnDriverDelivery
            // 
            this.radioBtnDriverDelivery.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioBtnDriverDelivery.AutoSize = true;
            this.radioBtnDriverDelivery.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnDriverDelivery.Location = new System.Drawing.Point(820, 19);
            this.radioBtnDriverDelivery.Name = "radioBtnDriverDelivery";
            this.radioBtnDriverDelivery.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioBtnDriverDelivery.Size = new System.Drawing.Size(100, 20);
            this.radioBtnDriverDelivery.TabIndex = 238;
            this.radioBtnDriverDelivery.Text = "تسليم سائق";
            this.radioBtnDriverDelivery.UseVisualStyleBackColor = true;
            this.radioBtnDriverDelivery.Visible = false;
            this.radioBtnDriverDelivery.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioBtnCustomerDelivery
            // 
            this.radioBtnCustomerDelivery.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioBtnCustomerDelivery.AutoSize = true;
            this.radioBtnCustomerDelivery.Checked = true;
            this.radioBtnCustomerDelivery.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnCustomerDelivery.Location = new System.Drawing.Point(823, 45);
            this.radioBtnCustomerDelivery.Name = "radioBtnCustomerDelivery";
            this.radioBtnCustomerDelivery.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioBtnCustomerDelivery.Size = new System.Drawing.Size(97, 20);
            this.radioBtnCustomerDelivery.TabIndex = 237;
            this.radioBtnCustomerDelivery.TabStop = true;
            this.radioBtnCustomerDelivery.Text = "تسليم عميل";
            this.radioBtnCustomerDelivery.UseVisualStyleBackColor = true;
            this.radioBtnCustomerDelivery.Visible = false;
            this.radioBtnCustomerDelivery.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 183);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(990, 549);
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
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "عدد الكراتين المستلمة";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // CustomerDeliveryDraft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(996, 785);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "CustomerDeliveryDraft";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panBranch.ResumeLayout(false);
            this.panBranch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.Label labClient;
        private System.Windows.Forms.Label labCustomer;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.Label labPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;
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
        private System.Windows.Forms.Label labStoreName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.Panel panBranch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDelegate;
        private System.Windows.Forms.TextBox txtStore;
        private System.Windows.Forms.ComboBox comStore;
        private System.Windows.Forms.Label labDate;
        private System.Windows.Forms.Label label4;
    }
}

