namespace MainSystem
{
    partial class StoreReturnBill
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
            this.ReturnItemReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateReturnBill = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNewChooes = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.comType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.labe1 = new System.Windows.Forms.Label();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.comGroup = new System.Windows.Forms.ComboBox();
            this.panBillNumber = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchBillNum = new System.Windows.Forms.TextBox();
            this.labelDelegate = new System.Windows.Forms.Label();
            this.radioButtonWithOutReturnBill = new System.Windows.Forms.RadioButton();
            this.radioButtonReturnBill = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReturnItemReason = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumOfCarton = new System.Windows.Forms.TextBox();
            this.btnPut = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnRemove = new Bunifu.Framework.UI.BunifuImageButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtReturnedQuantity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtReturnReason = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panBillNumber.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridControl2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.54639F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.45361F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 655);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 123);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl1.Size = new System.Drawing.Size(894, 164);
            this.gridControl1.TabIndex = 236;
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
            this.gridControl2.Location = new System.Drawing.Point(3, 373);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl2.Size = new System.Drawing.Size(894, 153);
            this.gridControl2.TabIndex = 235;
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
            this.gridView2.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gridView2.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.AppearancePrint.Row.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.AppearancePrint.Row.Options.UseFont = true;
            this.gridView2.AppearancePrint.Row.Options.UseTextOptions = true;
            this.gridView2.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Data_ID,
            this.Code,
            this.ItemName,
            this.Quantity,
            this.DeliveryQuantity,
            this.Carton,
            this.NumOfCarton,
            this.ReturnItemReason});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsView.ShowGroupPanel = false;
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
            this.Quantity.Caption = "الكمية المستلمة ";
            this.Quantity.FieldName = "TotalQuantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 2;
            // 
            // DeliveryQuantity
            // 
            this.DeliveryQuantity.Caption = "الكمية المسترجعة ب المتر/القطعة";
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
            this.NumOfCarton.Caption = "عدد الكراتين المسترجعة";
            this.NumOfCarton.FieldName = "NumOfCarton";
            this.NumOfCarton.Name = "NumOfCarton";
            this.NumOfCarton.Visible = true;
            this.NumOfCarton.VisibleIndex = 5;
            // 
            // ReturnItemReason
            // 
            this.ReturnItemReason.Caption = "سبب استرجاع البند";
            this.ReturnItemReason.FieldName = "ReturnItemReason";
            this.ReturnItemReason.Name = "ReturnItemReason";
            this.ReturnItemReason.Visible = true;
            this.ReturnItemReason.VisibleIndex = 6;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Controls.Add(this.btnCreateReturnBill, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 612);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(894, 40);
            this.tableLayoutPanel3.TabIndex = 167;
            // 
            // btnCreateReturnBill
            // 
            this.btnCreateReturnBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCreateReturnBill.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCreateReturnBill.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCreateReturnBill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateReturnBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreateReturnBill.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReturnBill.ForeColor = System.Drawing.Color.White;
            this.btnCreateReturnBill.Image = global::MainSystem.Properties.Resources.File_32;
            this.btnCreateReturnBill.ImagePosition = 0;
            this.btnCreateReturnBill.ImageZoom = 0;
            this.btnCreateReturnBill.LabelPosition = 25;
            this.btnCreateReturnBill.LabelText = "حفظ";
            this.btnCreateReturnBill.Location = new System.Drawing.Point(451, 4);
            this.btnCreateReturnBill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateReturnBill.Name = "btnCreateReturnBill";
            this.btnCreateReturnBill.Size = new System.Drawing.Size(83, 32);
            this.btnCreateReturnBill.TabIndex = 6;
            this.btnCreateReturnBill.Click += new System.EventHandler(this.btnCreateReturnBill_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panBillNumber);
            this.panel1.Controls.Add(this.radioButtonWithOutReturnBill);
            this.panel1.Controls.Add(this.radioButtonReturnBill);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 120);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.btnNewChooes);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtFactory);
            this.groupBox1.Controls.Add(this.comType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtProduct);
            this.groupBox1.Controls.Add(this.labe1);
            this.groupBox1.Controls.Add(this.comProduct);
            this.groupBox1.Controls.Add(this.comFactory);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGroup);
            this.groupBox1.Controls.Add(this.comGroup);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox1.Location = new System.Drawing.Point(22, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(721, 117);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "الفلاتر";
            this.groupBox1.Visible = false;
            // 
            // btnNewChooes
            // 
            this.btnNewChooes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnNewChooes.FlatAppearance.BorderSize = 0;
            this.btnNewChooes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewChooes.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnNewChooes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNewChooes.Location = new System.Drawing.Point(14, 23);
            this.btnNewChooes.Name = "btnNewChooes";
            this.btnNewChooes.Size = new System.Drawing.Size(103, 36);
            this.btnNewChooes.TabIndex = 135;
            this.btnNewChooes.Text = "اختيار اخر";
            this.btnNewChooes.UseVisualStyleBackColor = false;
            this.btnNewChooes.Click += new System.EventHandler(this.btnNewChooes_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(14, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 36);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFactory
            // 
            this.txtFactory.Location = new System.Drawing.Point(152, 31);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Size = new System.Drawing.Size(55, 24);
            this.txtFactory.TabIndex = 4;
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comType
            // 
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(492, 34);
            this.comType.Name = "comType";
            this.comType.Size = new System.Drawing.Size(120, 24);
            this.comType.TabIndex = 0;
            this.comType.SelectedValueChanged += new System.EventHandler(this.comType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label1.Location = new System.Drawing.Point(350, 67);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "الصنف";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(431, 34);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(55, 24);
            this.txtType.TabIndex = 1;
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(152, 61);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(55, 24);
            this.txtProduct.TabIndex = 10;
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // labe1
            // 
            this.labe1.AutoSize = true;
            this.labe1.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.labe1.Location = new System.Drawing.Point(629, 40);
            this.labe1.Name = "labe1";
            this.labe1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labe1.Size = new System.Drawing.Size(28, 18);
            this.labe1.TabIndex = 2;
            this.labe1.Text = "نوع";
            // 
            // comProduct
            // 
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(213, 61);
            this.comProduct.Name = "comProduct";
            this.comProduct.Size = new System.Drawing.Size(120, 24);
            this.comProduct.TabIndex = 9;
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comProduct_SelectedValueChanged);
            // 
            // comFactory
            // 
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Location = new System.Drawing.Point(213, 31);
            this.comFactory.Name = "comFactory";
            this.comFactory.Size = new System.Drawing.Size(120, 24);
            this.comFactory.TabIndex = 3;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comFactory_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label4.Location = new System.Drawing.Point(629, 70);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(67, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "المجموعة";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label2.Location = new System.Drawing.Point(350, 37);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "المصنع";
            // 
            // txtGroup
            // 
            this.txtGroup.Location = new System.Drawing.Point(431, 64);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(55, 24);
            this.txtGroup.TabIndex = 7;
            this.txtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comGroup
            // 
            this.comGroup.FormattingEnabled = true;
            this.comGroup.Location = new System.Drawing.Point(492, 64);
            this.comGroup.Name = "comGroup";
            this.comGroup.Size = new System.Drawing.Size(120, 24);
            this.comGroup.TabIndex = 6;
            this.comGroup.SelectedValueChanged += new System.EventHandler(this.comGroup_SelectedValueChanged);
            // 
            // panBillNumber
            // 
            this.panBillNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panBillNumber.Controls.Add(this.label10);
            this.panBillNumber.Controls.Add(this.txtBranchID);
            this.panBillNumber.Controls.Add(this.comBranch);
            this.panBillNumber.Controls.Add(this.txtBranchBillNum);
            this.panBillNumber.Controls.Add(this.labelDelegate);
            this.panBillNumber.Location = new System.Drawing.Point(338, 6);
            this.panBillNumber.Name = "panBillNumber";
            this.panBillNumber.Size = new System.Drawing.Size(286, 112);
            this.panBillNumber.TabIndex = 2;
            this.panBillNumber.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label10.Location = new System.Drawing.Point(220, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 18);
            this.label10.TabIndex = 203;
            this.label10.Text = "الفرع";
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(28, 26);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBranchID.Size = new System.Drawing.Size(50, 24);
            this.txtBranchID.TabIndex = 204;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comBranch
            // 
            this.comBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(84, 26);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(130, 24);
            this.comBranch.TabIndex = 202;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchBillNum
            // 
            this.txtBranchBillNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchBillNum.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchBillNum.Location = new System.Drawing.Point(57, 63);
            this.txtBranchBillNum.Name = "txtBranchBillNum";
            this.txtBranchBillNum.Size = new System.Drawing.Size(124, 24);
            this.txtBranchBillNum.TabIndex = 201;
            this.txtBranchBillNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchBillNum_KeyDown);
            // 
            // labelDelegate
            // 
            this.labelDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDelegate.AutoSize = true;
            this.labelDelegate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelegate.Location = new System.Drawing.Point(187, 65);
            this.labelDelegate.Name = "labelDelegate";
            this.labelDelegate.Size = new System.Drawing.Size(69, 16);
            this.labelDelegate.TabIndex = 200;
            this.labelDelegate.Text = "رقم الفاتورة";
            // 
            // radioButtonWithOutReturnBill
            // 
            this.radioButtonWithOutReturnBill.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonWithOutReturnBill.AutoSize = true;
            this.radioButtonWithOutReturnBill.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWithOutReturnBill.Location = new System.Drawing.Point(756, 64);
            this.radioButtonWithOutReturnBill.Name = "radioButtonWithOutReturnBill";
            this.radioButtonWithOutReturnBill.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonWithOutReturnBill.Size = new System.Drawing.Size(122, 20);
            this.radioButtonWithOutReturnBill.TabIndex = 1;
            this.radioButtonWithOutReturnBill.TabStop = true;
            this.radioButtonWithOutReturnBill.Text = "مرتجع بدون فاتورة";
            this.radioButtonWithOutReturnBill.UseVisualStyleBackColor = true;
            this.radioButtonWithOutReturnBill.CheckedChanged += new System.EventHandler(this.radioButtonWithOutReturnBill_CheckedChanged);
            // 
            // radioButtonReturnBill
            // 
            this.radioButtonReturnBill.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonReturnBill.AutoSize = true;
            this.radioButtonReturnBill.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonReturnBill.Location = new System.Drawing.Point(776, 41);
            this.radioButtonReturnBill.Name = "radioButtonReturnBill";
            this.radioButtonReturnBill.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonReturnBill.Size = new System.Drawing.Size(99, 20);
            this.radioButtonReturnBill.TabIndex = 0;
            this.radioButtonReturnBill.TabStop = true;
            this.radioButtonReturnBill.Text = "مرتجع بفاتورة";
            this.radioButtonReturnBill.UseVisualStyleBackColor = true;
            this.radioButtonReturnBill.CheckedChanged += new System.EventHandler(this.radioButtonReturnBill_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtReturnItemReason);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtCarton);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtNumOfCarton);
            this.panel2.Controls.Add(this.btnPut);
            this.panel2.Controls.Add(this.btnRemove);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtReturnedQuantity);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 290);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 80);
            this.panel2.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.Location = new System.Drawing.Point(365, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 19);
            this.label8.TabIndex = 236;
            this.label8.Text = "سبب استرجاع هذا البند";
            // 
            // txtReturnItemReason
            // 
            this.txtReturnItemReason.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtReturnItemReason.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReturnItemReason.Location = new System.Drawing.Point(181, 45);
            this.txtReturnItemReason.Name = "txtReturnItemReason";
            this.txtReturnItemReason.Size = new System.Drawing.Size(181, 27);
            this.txtReturnItemReason.TabIndex = 235;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.Location = new System.Drawing.Point(542, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 19);
            this.label6.TabIndex = 234;
            this.label6.Text = "الكرتنة";
            // 
            // txtCarton
            // 
            this.txtCarton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCarton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtCarton.Location = new System.Drawing.Point(435, 12);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(104, 27);
            this.txtCarton.TabIndex = 233;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.Location = new System.Drawing.Point(318, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 19);
            this.label5.TabIndex = 232;
            this.label5.Text = "عدد الكراتين";
            // 
            // txtNumOfCarton
            // 
            this.txtNumOfCarton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNumOfCarton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtNumOfCarton.Location = new System.Drawing.Point(211, 12);
            this.txtNumOfCarton.Name = "txtNumOfCarton";
            this.txtNumOfCarton.Size = new System.Drawing.Size(104, 27);
            this.txtNumOfCarton.TabIndex = 231;
            // 
            // btnPut
            // 
            this.btnPut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPut.BackColor = System.Drawing.Color.Transparent;
            this.btnPut.Image = global::MainSystem.Properties.Resources.Expand_Arrow_64px;
            this.btnPut.ImageActive = null;
            this.btnPut.Location = new System.Drawing.Point(10, 14);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(57, 34);
            this.btnPut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPut.TabIndex = 230;
            this.btnPut.TabStop = false;
            this.btnPut.Zoom = 10;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.Image = global::MainSystem.Properties.Resources.closeIcon;
            this.btnRemove.ImageActive = null;
            this.btnRemove.Location = new System.Drawing.Point(76, 15);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(59, 34);
            this.btnRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRemove.TabIndex = 229;
            this.btnRemove.TabStop = false;
            this.btnRemove.Zoom = 10;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label11.Location = new System.Drawing.Point(700, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 19);
            this.label11.TabIndex = 228;
            this.label11.Text = "الاجمالي بالمتر/القطعة";
            // 
            // txtReturnedQuantity
            // 
            this.txtReturnedQuantity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtReturnedQuantity.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReturnedQuantity.Location = new System.Drawing.Point(593, 45);
            this.txtReturnedQuantity.Name = "txtReturnedQuantity";
            this.txtReturnedQuantity.Size = new System.Drawing.Size(104, 27);
            this.txtReturnedQuantity.TabIndex = 227;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(822, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 19);
            this.label7.TabIndex = 226;
            this.label7.Text = "الكود";
            // 
            // txtCode
            // 
            this.txtCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtCode.Location = new System.Drawing.Point(611, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(205, 27);
            this.txtCode.TabIndex = 225;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.txtReturnReason);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.dateTimePicker1);
            this.panel3.Controls.Add(this.txtPhone);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtClientName);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 529);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 80);
            this.panel3.TabIndex = 237;
            // 
            // txtReturnReason
            // 
            this.txtReturnReason.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnReason.Location = new System.Drawing.Point(143, 42);
            this.txtReturnReason.Multiline = true;
            this.txtReturnReason.Name = "txtReturnReason";
            this.txtReturnReason.Size = new System.Drawing.Size(190, 32);
            this.txtReturnReason.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(343, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 19);
            this.label14.TabIndex = 11;
            this.label14.Text = "سبب الاسترجاع";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(343, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 19);
            this.label13.TabIndex = 10;
            this.label13.Text = "تاريخ الاسترجاع";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(142, 10);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(190, 26);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(582, 45);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(190, 26);
            this.txtPhone.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(785, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 19);
            this.label9.TabIndex = 7;
            this.label9.Text = "رقم التلفون";
            // 
            // txtClientName
            // 
            this.txtClientName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientName.Location = new System.Drawing.Point(582, 10);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(190, 26);
            this.txtClientName.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(785, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 19);
            this.label12.TabIndex = 5;
            this.label12.Text = "اسم العميل";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "سبب استرجاع البند";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "سبب استرجاع البند";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "سبب استرجاع البند";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "سبب استرجاع البند";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            // 
            // StoreReturnBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 655);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StoreReturnBill";
            this.Text = "StoreReturnBill";
            this.Load += new System.EventHandler(this.StoreReturnBill_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panBillNumber.ResumeLayout(false);
            this.panBillNumber.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonWithOutReturnBill;
        private System.Windows.Forms.RadioButton radioButtonReturnBill;
        private System.Windows.Forms.Panel panBillNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchBillNum;
        private System.Windows.Forms.Label labelDelegate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNewChooes;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label labe1;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.ComboBox comGroup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Bunifu.Framework.UI.BunifuTileButton btnCreateReturnBill;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtReturnedQuantity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCode;
        private Bunifu.Framework.UI.BunifuImageButton btnPut;
        private Bunifu.Framework.UI.BunifuImageButton btnRemove;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtReturnItemReason;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNumOfCarton;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn Data_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Code;
        private DevExpress.XtraGrid.Columns.GridColumn ItemName;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn DeliveryQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn Carton;
        private DevExpress.XtraGrid.Columns.GridColumn NumOfCarton;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ReturnItemReason;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtReturnReason;
        private System.Windows.Forms.Label label14;
    }
}