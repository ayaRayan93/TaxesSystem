namespace MainSystem
{
    partial class DelegateSalesForCompany
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReport = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.newChoose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.txtDelegateID = new System.Windows.Forms.TextBox();
            this.comDelegate = new System.Windows.Forms.ComboBox();
            this.labelDelegate = new System.Windows.Forms.Label();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Delegate_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Delegate_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TotalSales = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TotalReturn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Safaya = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Factory_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalSales = new System.Windows.Forms.TextBox();
            this.txtTotalReturn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalSafay = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(57, 78);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 32);
            this.btnSearch.TabIndex = 187;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.GridControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(922, 551);
            this.tableLayoutPanel1.TabIndex = 1;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 504);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(916, 44);
            this.tableLayoutPanel2.TabIndex = 8;
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
            this.btnReport.Location = new System.Drawing.Point(821, 4);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(92, 36);
            this.btnReport.TabIndex = 3;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comBranch);
            this.panel1.Controls.Add(this.txtBranchID);
            this.panel1.Controls.Add(this.newChoose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comFactory);
            this.panel1.Controls.Add(this.txtFactory);
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
            this.panel1.Size = new System.Drawing.Size(922, 120);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(583, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 18);
            this.label5.TabIndex = 202;
            this.label5.Text = "الفرع";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(583, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 203;
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
            this.comBranch.Location = new System.Drawing.Point(405, 11);
            this.comBranch.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(175, 24);
            this.comBranch.TabIndex = 201;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchID.BackColor = System.Drawing.Color.White;
            this.txtBranchID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(346, 11);
            this.txtBranchID.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(53, 24);
            this.txtBranchID.TabIndex = 200;
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
            this.newChoose.Location = new System.Drawing.Point(57, 40);
            this.newChoose.Margin = new System.Windows.Forms.Padding(0);
            this.newChoose.Name = "newChoose";
            this.newChoose.Size = new System.Drawing.Size(93, 32);
            this.newChoose.TabIndex = 199;
            this.newChoose.Text = "اختيار اخر";
            this.newChoose.UseVisualStyleBackColor = true;
            this.newChoose.Click += new System.EventHandler(this.newChoose_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(449, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 18);
            this.label1.TabIndex = 197;
            this.label1.Text = "الشركة";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label4.Location = new System.Drawing.Point(449, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 198;
            this.label4.Text = "الصنف";
            // 
            // comFactory
            // 
            this.comFactory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comFactory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comFactory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comFactory.BackColor = System.Drawing.Color.White;
            this.comFactory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Location = new System.Drawing.Point(271, 48);
            this.comFactory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.comFactory.Name = "comFactory";
            this.comFactory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comFactory.Size = new System.Drawing.Size(175, 24);
            this.comFactory.TabIndex = 196;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comFactory_SelectedValueChanged);
            // 
            // txtFactory
            // 
            this.txtFactory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFactory.BackColor = System.Drawing.Color.White;
            this.txtFactory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFactory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFactory.Location = new System.Drawing.Point(212, 48);
            this.txtFactory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Size = new System.Drawing.Size(53, 24);
            this.txtFactory.TabIndex = 195;
            this.txtFactory.TabStop = false;
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFactory_KeyDown);
            // 
            // txtDelegateID
            // 
            this.txtDelegateID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDelegateID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegateID.Location = new System.Drawing.Point(212, 78);
            this.txtDelegateID.Name = "txtDelegateID";
            this.txtDelegateID.Size = new System.Drawing.Size(53, 24);
            this.txtDelegateID.TabIndex = 194;
            this.txtDelegateID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDelegateID_KeyDown);
            // 
            // comDelegate
            // 
            this.comDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comDelegate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comDelegate.FormattingEnabled = true;
            this.comDelegate.Location = new System.Drawing.Point(271, 78);
            this.comDelegate.Name = "comDelegate";
            this.comDelegate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comDelegate.Size = new System.Drawing.Size(175, 24);
            this.comDelegate.TabIndex = 192;
            this.comDelegate.SelectedValueChanged += new System.EventHandler(this.comDelegate_SelectedValueChanged);
            // 
            // labelDelegate
            // 
            this.labelDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDelegate.AutoSize = true;
            this.labelDelegate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelegate.Location = new System.Drawing.Point(450, 78);
            this.labelDelegate.Name = "labelDelegate";
            this.labelDelegate.Size = new System.Drawing.Size(45, 16);
            this.labelDelegate.TabIndex = 193;
            this.labelDelegate.Text = "مندوب";
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeTo.Location = new System.Drawing.Point(602, 78);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimeTo.TabIndex = 188;
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeFrom.Location = new System.Drawing.Point(602, 48);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFrom.TabIndex = 189;
            this.dateTimeFrom.ValueChanged += new System.EventHandler(this.dateTimeFrom_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(817, 53);
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
            this.label3.Location = new System.Drawing.Point(816, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 191;
            this.label3.Text = "الي";
            // 
            // GridControl1
            // 
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridControl1.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.GridControl1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridControl1.Location = new System.Drawing.Point(0, 120);
            this.GridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridControl1.MainView = this.gridView1;
            this.GridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(922, 381);
            this.GridControl1.TabIndex = 1;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Delegate_ID,
            this.Delegate_Name,
            this.TotalSales,
            this.TotalReturn,
            this.Safaya,
            this.Factory_Name});
            this.gridView1.GridControl = this.GridControl1;
            this.gridView1.Name = "gridView1";
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
            this.Delegate_Name.OptionsColumn.AllowEdit = false;
            this.Delegate_Name.Visible = true;
            this.Delegate_Name.VisibleIndex = 4;
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
            this.TotalSales.FieldName = "TotalSales";
            this.TotalSales.Name = "TotalSales";
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
            this.TotalReturn.FieldName = "TotalReturn";
            this.TotalReturn.Name = "TotalReturn";
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
            this.Safaya.FieldName = "Safaya";
            this.Safaya.Name = "Safaya";
            this.Safaya.Visible = true;
            this.Safaya.VisibleIndex = 0;
            // 
            // Factory_Name
            // 
            this.Factory_Name.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Factory_Name.AppearanceCell.Options.UseFont = true;
            this.Factory_Name.AppearanceCell.Options.UseTextOptions = true;
            this.Factory_Name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Factory_Name.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Factory_Name.AppearanceHeader.Options.UseFont = true;
            this.Factory_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Factory_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Factory_Name.Caption = "الشركة";
            this.Factory_Name.FieldName = "Factory_Name";
            this.Factory_Name.Name = "Factory_Name";
            this.Factory_Name.Visible = true;
            this.Factory_Name.VisibleIndex = 3;
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
            this.panel2.Size = new System.Drawing.Size(812, 38);
            this.panel2.TabIndex = 4;
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
            // txtTotalSales
            // 
            this.txtTotalSales.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSales.Location = new System.Drawing.Point(568, 9);
            this.txtTotalSales.Name = "txtTotalSales";
            this.txtTotalSales.ReadOnly = true;
            this.txtTotalSales.Size = new System.Drawing.Size(114, 26);
            this.txtTotalSales.TabIndex = 1;
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
            // DelegateSalesForCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 551);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DelegateSalesForCompany";
            this.Text = "DelegateSalesForCompany";
            this.Load += new System.EventHandler(this.DelegateSalesForCompany_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDelegateID;
        private System.Windows.Forms.ComboBox comDelegate;
        private System.Windows.Forms.Label labelDelegate;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl GridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.TextBox txtFactory;
        private DevExpress.XtraGrid.Columns.GridColumn Delegate_ID;
        private DevExpress.XtraGrid.Columns.GridColumn Delegate_Name;
        private DevExpress.XtraGrid.Columns.GridColumn TotalSales;
        private DevExpress.XtraGrid.Columns.GridColumn TotalReturn;
        private DevExpress.XtraGrid.Columns.GridColumn Safaya;
        private DevExpress.XtraGrid.Columns.GridColumn Factory_Name;
        private System.Windows.Forms.Button newChoose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTotalSafay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalReturn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalSales;
        private System.Windows.Forms.Label label7;
    }
}