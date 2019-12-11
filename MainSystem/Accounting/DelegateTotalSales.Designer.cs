namespace MainSystem
{
    partial class DelegateTotalSales
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReport = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalSafay = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalReturn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalSales = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Delegate_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Delegate_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TotalSales = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TotalReturn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Safaya = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.newChoose = new System.Windows.Forms.Button();
            this.txtDelegateID = new System.Windows.Forms.TextBox();
            this.comDelegate = new System.Windows.Forms.ComboBox();
            this.labelDelegate = new System.Windows.Forms.Label();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 536);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.69869F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.30131F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnReport, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 489);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(907, 44);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnReport.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnReport.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReport.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Image = global::MainSystem.Properties.Resources.Print_32;
            this.btnReport.ImagePosition = 1;
            this.btnReport.ImageZoom = 20;
            this.btnReport.LabelPosition = 18;
            this.btnReport.LabelText = "طباعة تقرير";
            this.btnReport.Location = new System.Drawing.Point(813, 4);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(91, 36);
            this.btnReport.TabIndex = 3;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTotalSafay);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtTotalReturn);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtTotalSales);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(804, 38);
            this.panel2.TabIndex = 4;
            // 
            // txtTotalSafay
            // 
            this.txtTotalSafay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSafay.Location = new System.Drawing.Point(31, 9);
            this.txtTotalSafay.Name = "txtTotalSafay";
            this.txtTotalSafay.ReadOnly = true;
            this.txtTotalSafay.Size = new System.Drawing.Size(114, 26);
            this.txtTotalSafay.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(151, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 19);
            this.label9.TabIndex = 4;
            this.label9.Text = "اجمالي الصافي";
            // 
            // txtTotalReturn
            // 
            this.txtTotalReturn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalReturn.Location = new System.Drawing.Point(335, 9);
            this.txtTotalReturn.Name = "txtTotalReturn";
            this.txtTotalReturn.ReadOnly = true;
            this.txtTotalReturn.Size = new System.Drawing.Size(114, 26);
            this.txtTotalReturn.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(455, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "اجمالي المرتجعات";
            // 
            // txtTotalSales
            // 
            this.txtTotalSales.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSales.Location = new System.Drawing.Point(568, 9);
            this.txtTotalSales.Name = "txtTotalSales";
            this.txtTotalSales.ReadOnly = true;
            this.txtTotalSales.Size = new System.Drawing.Size(114, 26);
            this.txtTotalSales.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(688, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "اجمالي المبيعات";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 100);
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(913, 386);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView1.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.gridView1.AppearancePrint.EvenRow.Options.UseFont = true;
            this.gridView1.AppearancePrint.EvenRow.Options.UseTextOptions = true;
            this.gridView1.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView1.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.FooterPanel.Options.UseBackColor = true;
            this.gridView1.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.gridView1.AppearancePrint.FooterPanel.Options.UseTextOptions = true;
            this.gridView1.AppearancePrint.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Delegate_ID,
            this.Delegate_Name,
            this.TotalSales,
            this.TotalReturn,
            this.Safaya});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // Delegate_ID
            // 
            this.Delegate_ID.Caption = "Delegate_ID";
            this.Delegate_ID.FieldName = "Delegate_ID";
            this.Delegate_ID.Name = "Delegate_ID";
            // 
            // Delegate_Name
            // 
            this.Delegate_Name.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delegate_Name.AppearanceCell.Options.UseFont = true;
            this.Delegate_Name.AppearanceCell.Options.UseTextOptions = true;
            this.Delegate_Name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Delegate_Name.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delegate_Name.AppearanceHeader.Options.UseFont = true;
            this.Delegate_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Delegate_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Delegate_Name.Caption = "المندوب";
            this.Delegate_Name.FieldName = "Delegate_Name";
            this.Delegate_Name.Name = "Delegate_Name";
            this.Delegate_Name.Visible = true;
            this.Delegate_Name.VisibleIndex = 3;
            // 
            // TotalSales
            // 
            this.TotalSales.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalSales.AppearanceCell.ForeColor = System.Drawing.Color.Green;
            this.TotalSales.AppearanceCell.Options.UseFont = true;
            this.TotalSales.AppearanceCell.Options.UseForeColor = true;
            this.TotalSales.AppearanceCell.Options.UseTextOptions = true;
            this.TotalSales.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TotalSales.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalSales.AppearanceHeader.Options.UseFont = true;
            this.TotalSales.AppearanceHeader.Options.UseTextOptions = true;
            this.TotalSales.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TotalSales.Caption = "اجمالي المبيعات";
            this.TotalSales.DisplayFormat.FormatString = "{0:n2}";
            this.TotalSales.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TotalSales.FieldName = "TotalSales";
            this.TotalSales.Name = "TotalSales";
            this.TotalSales.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalSales", "{0:0.##}")});
            this.TotalSales.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.TotalSales.Visible = true;
            this.TotalSales.VisibleIndex = 2;
            // 
            // TotalReturn
            // 
            this.TotalReturn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalReturn.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TotalReturn.AppearanceCell.Options.UseFont = true;
            this.TotalReturn.AppearanceCell.Options.UseForeColor = true;
            this.TotalReturn.AppearanceCell.Options.UseTextOptions = true;
            this.TotalReturn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TotalReturn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalReturn.AppearanceHeader.Options.UseFont = true;
            this.TotalReturn.AppearanceHeader.Options.UseTextOptions = true;
            this.TotalReturn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TotalReturn.Caption = "اجمالي المرتجعات";
            this.TotalReturn.DisplayFormat.FormatString = "{0:n2}";
            this.TotalReturn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TotalReturn.FieldName = "TotalReturn";
            this.TotalReturn.Name = "TotalReturn";
            this.TotalReturn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalReturn", "{0:0.##}")});
            this.TotalReturn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.TotalReturn.Visible = true;
            this.TotalReturn.VisibleIndex = 1;
            // 
            // Safaya
            // 
            this.Safaya.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Safaya.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Safaya.AppearanceCell.Options.UseFont = true;
            this.Safaya.AppearanceCell.Options.UseForeColor = true;
            this.Safaya.AppearanceCell.Options.UseTextOptions = true;
            this.Safaya.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Safaya.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Safaya.AppearanceHeader.Options.UseFont = true;
            this.Safaya.AppearanceHeader.Options.UseTextOptions = true;
            this.Safaya.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Safaya.Caption = "الصافي";
            this.Safaya.DisplayFormat.FormatString = "{0:n2}";
            this.Safaya.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Safaya.FieldName = "Safaya";
            this.Safaya.Name = "Safaya";
            this.Safaya.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Safaya", "{0:0.##}")});
            this.Safaya.UnboundExpression = "[TotalSales] - [TotalReturn]";
            this.Safaya.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.Safaya.Visible = true;
            this.Safaya.VisibleIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comBranch);
            this.panel1.Controls.Add(this.txtBranchID);
            this.panel1.Controls.Add(this.newChoose);
            this.panel1.Controls.Add(this.txtDelegateID);
            this.panel1.Controls.Add(this.comDelegate);
            this.panel1.Controls.Add(this.labelDelegate);
            this.panel1.Controls.Add(this.dateTimeTo);
            this.panel1.Controls.Add(this.dateTimeFrom);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 100);
            this.panel1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.IndianRed;
            this.label10.Location = new System.Drawing.Point(219, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 19);
            this.label10.TabIndex = 208;
            this.label10.Text = "*";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(476, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 18);
            this.label5.TabIndex = 206;
            this.label5.Text = "الفرع";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(476, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 207;
            this.label6.Text = "الصنف";
            // 
            // comBranch
            // 
            this.comBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comBranch.BackColor = System.Drawing.Color.White;
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(298, 16);
            this.comBranch.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(175, 24);
            this.comBranch.TabIndex = 205;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchID.BackColor = System.Drawing.Color.White;
            this.txtBranchID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(243, 16);
            this.txtBranchID.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(49, 24);
            this.txtBranchID.TabIndex = 204;
            this.txtBranchID.TabStop = false;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchID_KeyDown);
            // 
            // newChoose
            // 
            this.newChoose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.newChoose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.newChoose.FlatAppearance.BorderSize = 0;
            this.newChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newChoose.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newChoose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.newChoose.Location = new System.Drawing.Point(85, 14);
            this.newChoose.Margin = new System.Windows.Forms.Padding(0);
            this.newChoose.Name = "newChoose";
            this.newChoose.Size = new System.Drawing.Size(93, 28);
            this.newChoose.TabIndex = 195;
            this.newChoose.Text = "اختيار اخر";
            this.newChoose.UseVisualStyleBackColor = true;
            this.newChoose.Click += new System.EventHandler(this.newChoose_Click);
            // 
            // txtDelegateID
            // 
            this.txtDelegateID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDelegateID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegateID.Location = new System.Drawing.Point(243, 50);
            this.txtDelegateID.Name = "txtDelegateID";
            this.txtDelegateID.Size = new System.Drawing.Size(48, 24);
            this.txtDelegateID.TabIndex = 194;
            this.txtDelegateID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDelegateID_KeyDown);
            // 
            // comDelegate
            // 
            this.comDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comDelegate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comDelegate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comDelegate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comDelegate.FormattingEnabled = true;
            this.comDelegate.Location = new System.Drawing.Point(298, 50);
            this.comDelegate.Name = "comDelegate";
            this.comDelegate.Size = new System.Drawing.Size(173, 24);
            this.comDelegate.TabIndex = 192;
            this.comDelegate.SelectedValueChanged += new System.EventHandler(this.comDelegate_SelectedValueChanged);
            // 
            // labelDelegate
            // 
            this.labelDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDelegate.AutoSize = true;
            this.labelDelegate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelegate.Location = new System.Drawing.Point(477, 54);
            this.labelDelegate.Name = "labelDelegate";
            this.labelDelegate.Size = new System.Drawing.Size(45, 16);
            this.labelDelegate.TabIndex = 193;
            this.labelDelegate.Text = "مندوب";
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeTo.CustomFormat = "yyyy/MM/dd";
            this.dateTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeTo.Location = new System.Drawing.Point(567, 54);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimeTo.TabIndex = 188;
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeFrom.CustomFormat = "yyyy/MM/dd";
            this.dateTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeFrom.Location = new System.Drawing.Point(567, 18);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFrom.TabIndex = 189;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(782, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 190;
            this.label2.Text = "من";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(781, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 191;
            this.label3.Text = "الي";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(85, 50);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 28);
            this.btnSearch.TabIndex = 187;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // DelegateTotalSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 536);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DelegateTotalSales";
            this.Text = "DelegateTotalSales";
            this.Load += new System.EventHandler(this.DelegateTotalSales_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtDelegateID;
        private System.Windows.Forms.ComboBox comDelegate;
        private System.Windows.Forms.Label labelDelegate;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn Delegate_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Delegate_Name;
        private DevExpress.XtraGrid.Columns.GridColumn TotalSales;
        private DevExpress.XtraGrid.Columns.GridColumn TotalReturn;
        private DevExpress.XtraGrid.Columns.GridColumn Safaya;
        private System.Windows.Forms.Button newChoose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnReport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTotalSafay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalReturn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalSales;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
    }
}