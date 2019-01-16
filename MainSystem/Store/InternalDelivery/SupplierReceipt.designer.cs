namespace MainSystem
{
    partial class SupplierReceipt
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.comSupplier = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBalat = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPermissionNum = new System.Windows.Forms.TextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNewChosen = new System.Windows.Forms.Button();
            this.comColor = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comSize = new System.Windows.Forms.ComboBox();
            this.comGroup = new System.Windows.Forms.ComboBox();
            this.comSort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comType = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCoding = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalMeter = new System.Windows.Forms.TextBox();
            this.comStorePlace = new System.Windows.Forms.ComboBox();
            this.txtSupPermissionNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(35, 80);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 34);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "اضافه";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label9.Location = new System.Drawing.Point(684, 8);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(48, 19);
            this.label9.TabIndex = 23;
            this.label9.Text = "المورد";
            // 
            // comSupplier
            // 
            this.comSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSupplier.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comSupplier.FormattingEnabled = true;
            this.comSupplier.Location = new System.Drawing.Point(528, 5);
            this.comSupplier.Name = "comSupplier";
            this.comSupplier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSupplier.Size = new System.Drawing.Size(150, 24);
            this.comSupplier.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label7.Location = new System.Drawing.Point(634, 13);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 29;
            this.label7.Text = "كود المنتج";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label8.Location = new System.Drawing.Point(330, 53);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(89, 19);
            this.label8.TabIndex = 27;
            this.label8.Text = "عدد الكراتين ";
            // 
            // txtCarton
            // 
            this.txtCarton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCarton.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCarton.Location = new System.Drawing.Point(137, 50);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCarton.Size = new System.Drawing.Size(171, 24);
            this.txtCarton.TabIndex = 26;
            this.txtCarton.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label10.Location = new System.Drawing.Point(335, 13);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(79, 19);
            this.label10.TabIndex = 25;
            this.label10.Text = "عدد البلتات";
            // 
            // txtBalat
            // 
            this.txtBalat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBalat.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBalat.Location = new System.Drawing.Point(137, 10);
            this.txtBalat.Name = "txtBalat";
            this.txtBalat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBalat.Size = new System.Drawing.Size(171, 24);
            this.txtBalat.TabIndex = 24;
            this.txtBalat.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label11.Location = new System.Drawing.Point(628, 53);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label11.Size = new System.Drawing.Size(90, 19);
            this.label11.TabIndex = 31;
            this.label11.Text = "مكان التخزين";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label13.Location = new System.Drawing.Point(312, 93);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label13.Size = new System.Drawing.Size(124, 19);
            this.label13.TabIndex = 32;
            this.label13.Text = "اجمالي عدد الامتار";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label14.Location = new System.Drawing.Point(641, 93);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(62, 19);
            this.label14.TabIndex = 35;
            this.label14.Text = "ملحوظة";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDescription.Location = new System.Drawing.Point(453, 90);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescription.Size = new System.Drawing.Size(171, 24);
            this.txtDescription.TabIndex = 34;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCode.Location = new System.Drawing.Point(453, 10);
            this.txtCode.Name = "txtCode";
            this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCode.Size = new System.Drawing.Size(171, 24);
            this.txtCode.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label15.Location = new System.Drawing.Point(445, 8);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(77, 19);
            this.label15.TabIndex = 39;
            this.label15.Text = "اذن المخزن";
            // 
            // txtPermissionNum
            // 
            this.txtPermissionNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPermissionNum.Enabled = false;
            this.txtPermissionNum.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPermissionNum.Location = new System.Drawing.Point(339, 5);
            this.txtPermissionNum.Name = "txtPermissionNum";
            this.txtPermissionNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPermissionNum.Size = new System.Drawing.Size(100, 24);
            this.txtPermissionNum.TabIndex = 38;
            this.txtPermissionNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPermissionNum_KeyDown);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(3, 113);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl1.Size = new System.Drawing.Size(772, 189);
            this.gridControl1.TabIndex = 40;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.FindNullPrompt = "بحث";
            this.gridView1.OptionsFind.ShowClearButton = false;
            this.gridView1.OptionsFind.ShowFindButton = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnNewChosen);
            this.panel3.Controls.Add(this.comColor);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.comSize);
            this.panel3.Controls.Add(this.comGroup);
            this.panel3.Controls.Add(this.comSort);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.comFactory);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.comProduct);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.comType);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(772, 64);
            this.panel3.TabIndex = 41;
            // 
            // btnNewChosen
            // 
            this.btnNewChosen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNewChosen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnNewChosen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewChosen.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewChosen.ForeColor = System.Drawing.Color.White;
            this.btnNewChosen.Location = new System.Drawing.Point(28, 5);
            this.btnNewChosen.Name = "btnNewChosen";
            this.btnNewChosen.Size = new System.Drawing.Size(57, 56);
            this.btnNewChosen.TabIndex = 21;
            this.btnNewChosen.Text = "اختيار اخر";
            this.btnNewChosen.UseVisualStyleBackColor = false;
            this.btnNewChosen.Click += new System.EventHandler(this.btnNewChosen_Click);
            // 
            // comColor
            // 
            this.comColor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comColor.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comColor.FormattingEnabled = true;
            this.comColor.Location = new System.Drawing.Point(189, 34);
            this.comColor.Name = "comColor";
            this.comColor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comColor.Size = new System.Drawing.Size(100, 24);
            this.comColor.TabIndex = 19;
            this.comColor.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(90, 31);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 30);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comSize
            // 
            this.comSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSize.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comSize.FormattingEnabled = true;
            this.comSize.Location = new System.Drawing.Point(189, 5);
            this.comSize.Name = "comSize";
            this.comSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSize.Size = new System.Drawing.Size(100, 24);
            this.comSize.TabIndex = 18;
            this.comSize.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comGroup
            // 
            this.comGroup.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comGroup.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comGroup.FormattingEnabled = true;
            this.comGroup.Location = new System.Drawing.Point(357, 5);
            this.comGroup.Name = "comGroup";
            this.comGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comGroup.Size = new System.Drawing.Size(120, 24);
            this.comGroup.TabIndex = 6;
            this.comGroup.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comSort
            // 
            this.comSort.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSort.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comSort.FormattingEnabled = true;
            this.comSort.Location = new System.Drawing.Point(91, 5);
            this.comSort.Name = "comSort";
            this.comSort.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSort.Size = new System.Drawing.Size(50, 24);
            this.comSort.TabIndex = 17;
            this.comSort.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label1.Location = new System.Drawing.Point(684, 37);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "المصنع";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label2.Location = new System.Drawing.Point(301, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "اللون";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label3.Location = new System.Drawing.Point(481, 8);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(71, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "المجموعة";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label4.Location = new System.Drawing.Point(293, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "المقاس";
            // 
            // comFactory
            // 
            this.comFactory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comFactory.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Location = new System.Drawing.Point(558, 34);
            this.comFactory.Name = "comFactory";
            this.comFactory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comFactory.Size = new System.Drawing.Size(120, 24);
            this.comFactory.TabIndex = 3;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label16.Location = new System.Drawing.Point(146, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(36, 19);
            this.label16.TabIndex = 14;
            this.label16.Text = "الفرز";
            // 
            // comProduct
            // 
            this.comProduct.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comProduct.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(357, 34);
            this.comProduct.Name = "comProduct";
            this.comProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comProduct.Size = new System.Drawing.Size(120, 24);
            this.comProduct.TabIndex = 9;
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label17.Location = new System.Drawing.Point(691, 8);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label17.Size = new System.Drawing.Size(41, 19);
            this.label17.TabIndex = 2;
            this.label17.Text = "النوع";
            // 
            // comType
            // 
            this.comType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comType.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(558, 5);
            this.comType.Name = "comType";
            this.comType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comType.Size = new System.Drawing.Size(120, 24);
            this.comType.TabIndex = 0;
            this.comType.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label18.Location = new System.Drawing.Point(491, 37);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label18.Size = new System.Drawing.Size(51, 19);
            this.label18.TabIndex = 11;
            this.label18.Text = "الصنف";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl2.Location = new System.Drawing.Point(3, 438);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl2.Size = new System.Drawing.Size(772, 190);
            this.gridControl2.TabIndex = 42;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Appearance.Row.Options.UseForeColor = true;
            this.gridView2.Appearance.Row.Options.UseTextOptions = true;
            this.gridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsDetail.EnableMasterViewMode = false;
            this.gridView2.OptionsFind.AlwaysVisible = true;
            this.gridView2.OptionsFind.FindNullPrompt = "بحث";
            this.gridView2.OptionsFind.ShowClearButton = false;
            this.gridView2.OptionsFind.ShowFindButton = false;
            this.gridView2.OptionsSelection.MultiSelect = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControl2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 631);
            this.tableLayoutPanel1.TabIndex = 43;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSupPermissionNum);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnCoding);
            this.panel1.Controls.Add(this.txtPermissionNum);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.comSupplier);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 34);
            this.panel1.TabIndex = 43;
            // 
            // btnCoding
            // 
            this.btnCoding.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCoding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCoding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCoding.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnCoding.ForeColor = System.Drawing.Color.White;
            this.btnCoding.Location = new System.Drawing.Point(28, 3);
            this.btnCoding.Name = "btnCoding";
            this.btnCoding.Size = new System.Drawing.Size(87, 28);
            this.btnCoding.TabIndex = 40;
            this.btnCoding.Text = "تكويد";
            this.btnCoding.UseVisualStyleBackColor = false;
            this.btnCoding.Click += new System.EventHandler(this.btnCoding_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTotalMeter);
            this.panel2.Controls.Add(this.comStorePlace);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.txtBalat);
            this.panel2.Controls.Add(this.txtDescription);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtCarton);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 308);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(772, 124);
            this.panel2.TabIndex = 44;
            // 
            // txtTotalMeter
            // 
            this.txtTotalMeter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTotalMeter.Enabled = false;
            this.txtTotalMeter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalMeter.Location = new System.Drawing.Point(137, 91);
            this.txtTotalMeter.Name = "txtTotalMeter";
            this.txtTotalMeter.Size = new System.Drawing.Size(171, 23);
            this.txtTotalMeter.TabIndex = 38;
            // 
            // comStorePlace
            // 
            this.comStorePlace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comStorePlace.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comStorePlace.FormattingEnabled = true;
            this.comStorePlace.Location = new System.Drawing.Point(453, 50);
            this.comStorePlace.Name = "comStorePlace";
            this.comStorePlace.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comStorePlace.Size = new System.Drawing.Size(171, 24);
            this.comStorePlace.TabIndex = 37;
            // 
            // txtSupPermissionNum
            // 
            this.txtSupPermissionNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSupPermissionNum.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSupPermissionNum.Location = new System.Drawing.Point(141, 5);
            this.txtSupPermissionNum.Name = "txtSupPermissionNum";
            this.txtSupPermissionNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSupPermissionNum.Size = new System.Drawing.Size(100, 24);
            this.txtSupPermissionNum.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label5.Location = new System.Drawing.Point(247, 8);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(86, 19);
            this.label5.TabIndex = 42;
            this.label5.Text = "اذن الاستلام";
            // 
            // SupplierReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(778, 631);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SupplierReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SupplierReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comSupplier;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBalat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPermissionNum;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNewChosen;
        private System.Windows.Forms.ComboBox comColor;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox comSize;
        private System.Windows.Forms.ComboBox comGroup;
        private System.Windows.Forms.ComboBox comSort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Label label18;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comStorePlace;
        private System.Windows.Forms.TextBox txtTotalMeter;
        private System.Windows.Forms.Button btnCoding;
        private System.Windows.Forms.TextBox txtSupPermissionNum;
        private System.Windows.Forms.Label label5;
    }
}

