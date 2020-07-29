﻿namespace TaxesSystem
{
    partial class Bill_Confirm
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
            this.labTotalBillPriceAD = new System.Windows.Forms.Label();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbCash = new System.Windows.Forms.RadioButton();
            this.rdbSoon = new System.Windows.Forms.RadioButton();
            this.labelEng = new System.Windows.Forms.Label();
            this.comEngCon = new System.Windows.Forms.ComboBox();
            this.radClient = new System.Windows.Forms.RadioButton();
            this.radEng = new System.Windows.Forms.RadioButton();
            this.radCon = new System.Windows.Forms.RadioButton();
            this.labelClient = new System.Windows.Forms.Label();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDetails = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnConfirm = new Bunifu.Framework.UI.BunifuTileButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labTotalDiscount = new System.Windows.Forms.Label();
            this.labTotalBillPriceBD = new System.Windows.Forms.Label();
            this.btnAddItem = new Bunifu.Framework.UI.BunifuTileButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDelegate = new System.Windows.Forms.TextBox();
            this.listBoxControlBranchID = new DevExpress.XtraEditors.ListBoxControl();
            this.checkBoxAdd = new System.Windows.Forms.CheckBox();
            this.listBoxControlBills = new DevExpress.XtraEditors.ListBoxControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.radDealer = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtClientPhone = new System.Windows.Forms.TextBox();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlBranchID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlBills)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTotalBillPriceAD
            // 
            this.labTotalBillPriceAD.AutoSize = true;
            this.labTotalBillPriceAD.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalBillPriceAD.Location = new System.Drawing.Point(230, 0);
            this.labTotalBillPriceAD.Name = "labTotalBillPriceAD";
            this.labTotalBillPriceAD.Size = new System.Drawing.Size(0, 17);
            this.labTotalBillPriceAD.TabIndex = 93;
            this.labTotalBillPriceAD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comBranch
            // 
            this.comBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(148, 13);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(150, 24);
            this.comBranch.TabIndex = 147;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(304, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 17);
            this.label6.TabIndex = 151;
            this.label6.Text = "الفرع";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.rdbCash);
            this.groupBox1.Controls.Add(this.rdbSoon);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(99, 128);
            this.groupBox1.TabIndex = 146;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "طريقة الدفع";
            // 
            // rdbCash
            // 
            this.rdbCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbCash.AutoSize = true;
            this.rdbCash.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCash.Location = new System.Drawing.Point(26, 33);
            this.rdbCash.Name = "rdbCash";
            this.rdbCash.Size = new System.Drawing.Size(52, 20);
            this.rdbCash.TabIndex = 98;
            this.rdbCash.Text = "كاش";
            this.rdbCash.UseVisualStyleBackColor = true;
            this.rdbCash.CheckedChanged += new System.EventHandler(this.rdbCash_CheckedChanged);
            // 
            // rdbSoon
            // 
            this.rdbSoon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbSoon.AutoSize = true;
            this.rdbSoon.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSoon.Location = new System.Drawing.Point(29, 69);
            this.rdbSoon.Name = "rdbSoon";
            this.rdbSoon.Size = new System.Drawing.Size(47, 20);
            this.rdbSoon.TabIndex = 99;
            this.rdbSoon.Text = "آجل";
            this.rdbSoon.UseVisualStyleBackColor = true;
            this.rdbSoon.CheckedChanged += new System.EventHandler(this.rdbSoon_CheckedChanged);
            // 
            // labelEng
            // 
            this.labelEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEng.AutoSize = true;
            this.labelEng.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelEng.Location = new System.Drawing.Point(229, 28);
            this.labelEng.Name = "labelEng";
            this.labelEng.Size = new System.Drawing.Size(119, 17);
            this.labelEng.TabIndex = 143;
            this.labelEng.Text = "مهندس/مقاول/تاجر";
            this.labelEng.Visible = false;
            // 
            // comEngCon
            // 
            this.comEngCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comEngCon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comEngCon.FormattingEnabled = true;
            this.comEngCon.Location = new System.Drawing.Point(75, 24);
            this.comEngCon.Name = "comEngCon";
            this.comEngCon.Size = new System.Drawing.Size(150, 24);
            this.comEngCon.TabIndex = 142;
            this.comEngCon.Visible = false;
            this.comEngCon.SelectedValueChanged += new System.EventHandler(this.comEngCon_SelectedValueChanged);
            // 
            // radClient
            // 
            this.radClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radClient.AutoSize = true;
            this.radClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radClient.Location = new System.Drawing.Point(242, 3);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(58, 21);
            this.radClient.TabIndex = 139;
            this.radClient.TabStop = true;
            this.radClient.Text = "عميل";
            this.radClient.UseVisualStyleBackColor = true;
            this.radClient.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radEng
            // 
            this.radEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radEng.AutoSize = true;
            this.radEng.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radEng.Location = new System.Drawing.Point(165, 3);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(71, 21);
            this.radEng.TabIndex = 138;
            this.radEng.TabStop = true;
            this.radEng.Text = "مهندس";
            this.radEng.UseVisualStyleBackColor = true;
            this.radEng.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radCon
            // 
            this.radCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radCon.AutoSize = true;
            this.radCon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radCon.Location = new System.Drawing.Point(100, 3);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(59, 21);
            this.radCon.TabIndex = 137;
            this.radCon.TabStop = true;
            this.radCon.Text = "مقاول";
            this.radCon.UseVisualStyleBackColor = true;
            this.radCon.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // labelClient
            // 
            this.labelClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelClient.Location = new System.Drawing.Point(268, 80);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(40, 17);
            this.labelClient.TabIndex = 136;
            this.labelClient.Text = "عميل";
            this.labelClient.Visible = false;
            // 
            // comClient
            // 
            this.comClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(75, 76);
            this.comClient.Name = "comClient";
            this.comClient.Size = new System.Drawing.Size(150, 24);
            this.comClient.TabIndex = 135;
            this.comClient.Visible = false;
            this.comClient.SelectedValueChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(280, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 133;
            this.label2.Text = "رقم الفاتورة";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillNo.Enabled = false;
            this.txtBillNo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBillNo.Location = new System.Drawing.Point(194, 43);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(80, 24);
            this.txtBillNo.TabIndex = 132;
            this.txtBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNo_KeyDown);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 143);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl1.Size = new System.Drawing.Size(1037, 450);
            this.gridControl1.TabIndex = 153;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.FindNullPrompt = "بحث";
            this.gridView1.OptionsFind.ShowClearButton = false;
            this.gridView1.OptionsFind.ShowFindButton = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 661);
            this.panel1.TabIndex = 154;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1043, 661);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Controls.Add(this.btnDetails, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnConfirm, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAddItem, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 599);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1037, 59);
            this.tableLayoutPanel3.TabIndex = 154;
            // 
            // btnDetails
            // 
            this.btnDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDetails.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDetails.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetails.ForeColor = System.Drawing.Color.White;
            this.btnDetails.Image = global::TaxesSystem.Properties.Resources.Delete_32;
            this.btnDetails.ImagePosition = 1;
            this.btnDetails.ImageZoom = 25;
            this.btnDetails.LabelPosition = 18;
            this.btnDetails.LabelText = "حذف عنصر";
            this.btnDetails.Location = new System.Drawing.Point(484, 4);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(97, 51);
            this.btnDetails.TabIndex = 0;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnConfirm.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnConfirm.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Image = global::TaxesSystem.Properties.Resources.Save_32;
            this.btnConfirm.ImagePosition = 1;
            this.btnConfirm.ImageZoom = 25;
            this.btnConfirm.LabelPosition = 18;
            this.btnConfirm.LabelText = "تاكيد";
            this.btnConfirm.Location = new System.Drawing.Point(690, 4);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(97, 51);
            this.btnConfirm.TabIndex = 94;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.labTotalDiscount, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labTotalBillPriceAD, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.labTotalBillPriceBD, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(465, 53);
            this.tableLayoutPanel4.TabIndex = 95;
            // 
            // labTotalDiscount
            // 
            this.labTotalDiscount.AutoSize = true;
            this.labTotalDiscount.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labTotalDiscount.Location = new System.Drawing.Point(462, 26);
            this.labTotalDiscount.Name = "labTotalDiscount";
            this.labTotalDiscount.Size = new System.Drawing.Size(0, 17);
            this.labTotalDiscount.TabIndex = 94;
            this.labTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalBillPriceBD
            // 
            this.labTotalBillPriceBD.AutoSize = true;
            this.labTotalBillPriceBD.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotalBillPriceBD.Location = new System.Drawing.Point(462, 0);
            this.labTotalBillPriceBD.Name = "labTotalBillPriceBD";
            this.labTotalBillPriceBD.Size = new System.Drawing.Size(0, 16);
            this.labTotalBillPriceBD.TabIndex = 95;
            this.labTotalBillPriceBD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddItem.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddItem.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Image = global::TaxesSystem.Properties.Resources.File_32;
            this.btnAddItem.ImagePosition = 1;
            this.btnAddItem.ImageZoom = 25;
            this.btnAddItem.LabelPosition = 18;
            this.btnAddItem.LabelText = "اضافة عنصر";
            this.btnAddItem.Location = new System.Drawing.Point(587, 4);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(97, 51);
            this.btnAddItem.TabIndex = 96;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.MaximumSize = new System.Drawing.Size(0, 258);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1037, 134);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(574, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 128);
            this.panel2.TabIndex = 150;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtDelegate);
            this.panel3.Controls.Add(this.listBoxControlBranchID);
            this.panel3.Controls.Add(this.checkBoxAdd);
            this.panel3.Controls.Add(this.listBoxControlBills);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtBillNo);
            this.panel3.Controls.Add(this.comBranch);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(48, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(363, 121);
            this.panel3.TabIndex = 157;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(280, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 158;
            this.label1.Text = "المندوب";
            // 
            // txtDelegate
            // 
            this.txtDelegate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDelegate.Enabled = false;
            this.txtDelegate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegate.Location = new System.Drawing.Point(128, 73);
            this.txtDelegate.Name = "txtDelegate";
            this.txtDelegate.Size = new System.Drawing.Size(146, 24);
            this.txtDelegate.TabIndex = 157;
            // 
            // listBoxControlBranchID
            // 
            this.listBoxControlBranchID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxControlBranchID.Location = new System.Drawing.Point(121, 13);
            this.listBoxControlBranchID.Name = "listBoxControlBranchID";
            this.listBoxControlBranchID.Size = new System.Drawing.Size(20, 28);
            this.listBoxControlBranchID.TabIndex = 156;
            this.listBoxControlBranchID.Visible = false;
            // 
            // checkBoxAdd
            // 
            this.checkBoxAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAdd.AutoSize = true;
            this.checkBoxAdd.BackColor = System.Drawing.Color.Gainsboro;
            this.checkBoxAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAdd.Location = new System.Drawing.Point(129, 45);
            this.checkBoxAdd.Name = "checkBoxAdd";
            this.checkBoxAdd.Size = new System.Drawing.Size(61, 20);
            this.checkBoxAdd.TabIndex = 153;
            this.checkBoxAdd.Text = "+اخرى";
            this.checkBoxAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxAdd.UseVisualStyleBackColor = false;
            // 
            // listBoxControlBills
            // 
            this.listBoxControlBills.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxControlBills.HorizontalScrollbar = true;
            this.listBoxControlBills.Location = new System.Drawing.Point(13, 13);
            this.listBoxControlBills.Name = "listBoxControlBills";
            this.listBoxControlBills.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listBoxControlBills.Size = new System.Drawing.Size(100, 84);
            this.listBoxControlBills.TabIndex = 155;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(108, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(460, 128);
            this.panel4.TabIndex = 147;
            // 
            // txtClientID
            // 
            this.txtClientID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClientID.Location = new System.Drawing.Point(19, 76);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtClientID.Size = new System.Drawing.Size(50, 24);
            this.txtClientID.TabIndex = 146;
            this.txtClientID.Visible = false;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCustomerID.Location = new System.Drawing.Point(19, 24);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCustomerID.Size = new System.Drawing.Size(50, 24);
            this.txtCustomerID.TabIndex = 145;
            this.txtCustomerID.Visible = false;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radDealer
            // 
            this.radDealer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDealer.AutoSize = true;
            this.radDealer.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radDealer.Location = new System.Drawing.Point(45, 3);
            this.radDealer.Name = "radDealer";
            this.radDealer.Size = new System.Drawing.Size(49, 21);
            this.radDealer.TabIndex = 144;
            this.radDealer.TabStop = true;
            this.radDealer.Text = "تاجر";
            this.radDealer.UseVisualStyleBackColor = true;
            this.radDealer.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.radClient);
            this.panel5.Controls.Add(this.txtClientID);
            this.panel5.Controls.Add(this.txtCustomerID);
            this.panel5.Controls.Add(this.labelEng);
            this.panel5.Controls.Add(this.labelClient);
            this.panel5.Controls.Add(this.radDealer);
            this.panel5.Controls.Add(this.radCon);
            this.panel5.Controls.Add(this.comEngCon);
            this.panel5.Controls.Add(this.comClient);
            this.panel5.Controls.Add(this.radEng);
            this.panel5.Controls.Add(this.txtClientPhone);
            this.panel5.Controls.Add(this.txtCustomerPhone);
            this.panel5.Location = new System.Drawing.Point(47, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(358, 128);
            this.panel5.TabIndex = 147;
            // 
            // txtClientPhone
            // 
            this.txtClientPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientPhone.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClientPhone.Location = new System.Drawing.Point(19, 101);
            this.txtClientPhone.Name = "txtClientPhone";
            this.txtClientPhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtClientPhone.Size = new System.Drawing.Size(140, 24);
            this.txtClientPhone.TabIndex = 147;
            this.txtClientPhone.Visible = false;
            // 
            // txtCustomerPhone
            // 
            this.txtCustomerPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerPhone.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCustomerPhone.Location = new System.Drawing.Point(19, 49);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCustomerPhone.Size = new System.Drawing.Size(140, 24);
            this.txtCustomerPhone.TabIndex = 146;
            this.txtCustomerPhone.Visible = false;
            // 
            // Bill_Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 661);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Bill_Confirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlBranchID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlBills)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labTotalBillPriceAD;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbCash;
        private System.Windows.Forms.RadioButton rdbSoon;
        private System.Windows.Forms.Label labelEng;
        private System.Windows.Forms.ComboBox comEngCon;
        private System.Windows.Forms.RadioButton radClient;
        private System.Windows.Forms.RadioButton radEng;
        private System.Windows.Forms.RadioButton radCon;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBillNo;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Bunifu.Framework.UI.BunifuTileButton btnDetails;
        private System.Windows.Forms.RadioButton radDealer;
        private Bunifu.Framework.UI.BunifuTileButton btnConfirm;
        private System.Windows.Forms.CheckBox checkBoxAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label labTotalDiscount;
        private System.Windows.Forms.Label labTotalBillPriceBD;
        private Bunifu.Framework.UI.BunifuTileButton btnAddItem;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlBills;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlBranchID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtClientPhone;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDelegate;
    }
}

