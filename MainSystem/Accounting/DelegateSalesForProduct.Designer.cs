namespace MainSystem
{
    partial class DelegateSalesForProduct
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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Data_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.QuantitySaled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.QuantityReturned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Cost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReport = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalSafay = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalReturn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalSales = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(901, 130);
            this.panel1.TabIndex = 0;
            // 
            // newChoose
            // 
            this.newChoose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.newChoose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.newChoose.FlatAppearance.BorderSize = 0;
            this.newChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newChoose.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newChoose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.newChoose.Location = new System.Drawing.Point(62, 43);
            this.newChoose.Margin = new System.Windows.Forms.Padding(0);
            this.newChoose.Name = "newChoose";
            this.newChoose.Size = new System.Drawing.Size(93, 32);
            this.newChoose.TabIndex = 203;
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
            this.label1.Location = new System.Drawing.Point(449, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 18);
            this.label1.TabIndex = 201;
            this.label1.Text = "الشركة";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label4.Location = new System.Drawing.Point(449, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 202;
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
            this.comFactory.Location = new System.Drawing.Point(271, 50);
            this.comFactory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.comFactory.Name = "comFactory";
            this.comFactory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comFactory.Size = new System.Drawing.Size(175, 24);
            this.comFactory.TabIndex = 200;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comFactory_SelectedValueChanged);
            // 
            // txtFactory
            // 
            this.txtFactory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFactory.BackColor = System.Drawing.Color.White;
            this.txtFactory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFactory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFactory.Location = new System.Drawing.Point(212, 50);
            this.txtFactory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Size = new System.Drawing.Size(53, 24);
            this.txtFactory.TabIndex = 199;
            this.txtFactory.TabStop = false;
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFactory_KeyDown);
            // 
            // txtDelegateID
            // 
            this.txtDelegateID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDelegateID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDelegateID.Location = new System.Drawing.Point(212, 83);
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
            this.comDelegate.Location = new System.Drawing.Point(271, 83);
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
            this.labelDelegate.Location = new System.Drawing.Point(450, 83);
            this.labelDelegate.Name = "labelDelegate";
            this.labelDelegate.Size = new System.Drawing.Size(45, 16);
            this.labelDelegate.TabIndex = 193;
            this.labelDelegate.Text = "مندوب";
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeTo.Location = new System.Drawing.Point(567, 83);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimeTo.TabIndex = 188;
            this.dateTimeTo.ValueChanged += new System.EventHandler(this.dateTimeFrom_ValueChanged);
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeFrom.Location = new System.Drawing.Point(567, 50);
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
            this.label2.Location = new System.Drawing.Point(782, 50);
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
            this.label3.Location = new System.Drawing.Point(781, 83);
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
            this.btnSearch.Location = new System.Drawing.Point(62, 81);
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
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(901, 594);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.gridControl1.EmbeddedNavigator.Appearance.Options.UseTextOptions = true;
            this.gridControl1.EmbeddedNavigator.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(0, 130);
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(901, 414);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
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
            this.gridView1.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Data_ID,
            this.CodeName,
            this.QuantitySaled,
            this.QuantityReturned,
            this.Quantity,
            this.Cost});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(584, 396, 260, 282);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // Data_ID
            // 
            this.Data_ID.Caption = "gridColumn1";
            this.Data_ID.FieldName = "ff";
            this.Data_ID.Name = "Data_ID";
            // 
            // CodeName
            // 
            this.CodeName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeName.AppearanceCell.Options.UseFont = true;
            this.CodeName.AppearanceCell.Options.UseTextOptions = true;
            this.CodeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CodeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeName.AppearanceHeader.Options.UseFont = true;
            this.CodeName.AppearanceHeader.Options.UseTextOptions = true;
            this.CodeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CodeName.Caption = "البند";
            this.CodeName.FieldName = "CodeName";
            this.CodeName.Name = "CodeName";
            this.CodeName.Visible = true;
            this.CodeName.VisibleIndex = 4;
            this.CodeName.Width = 150;
            // 
            // QuantitySaled
            // 
            this.QuantitySaled.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantitySaled.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.QuantitySaled.AppearanceCell.Options.UseFont = true;
            this.QuantitySaled.AppearanceCell.Options.UseForeColor = true;
            this.QuantitySaled.AppearanceCell.Options.UseTextOptions = true;
            this.QuantitySaled.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.QuantitySaled.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantitySaled.AppearanceHeader.Options.UseFont = true;
            this.QuantitySaled.AppearanceHeader.Options.UseTextOptions = true;
            this.QuantitySaled.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.QuantitySaled.Caption = "الكمية المباعة";
            this.QuantitySaled.FieldName = "QuantitySaled";
            this.QuantitySaled.Name = "QuantitySaled";
            this.QuantitySaled.Visible = true;
            this.QuantitySaled.VisibleIndex = 3;
            this.QuantitySaled.Width = 173;
            // 
            // QuantityReturned
            // 
            this.QuantityReturned.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityReturned.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.QuantityReturned.AppearanceCell.Options.UseFont = true;
            this.QuantityReturned.AppearanceCell.Options.UseForeColor = true;
            this.QuantityReturned.AppearanceCell.Options.UseTextOptions = true;
            this.QuantityReturned.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.QuantityReturned.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityReturned.AppearanceHeader.Options.UseFont = true;
            this.QuantityReturned.AppearanceHeader.Options.UseTextOptions = true;
            this.QuantityReturned.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.QuantityReturned.Caption = "الكمية المرتجعة";
            this.QuantityReturned.FieldName = "QuantityReturned";
            this.QuantityReturned.Name = "QuantityReturned";
            this.QuantityReturned.Visible = true;
            this.QuantityReturned.VisibleIndex = 2;
            this.QuantityReturned.Width = 173;
            // 
            // Quantity
            // 
            this.Quantity.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantity.AppearanceCell.ForeColor = System.Drawing.Color.Green;
            this.Quantity.AppearanceCell.Options.UseFont = true;
            this.Quantity.AppearanceCell.Options.UseForeColor = true;
            this.Quantity.AppearanceCell.Options.UseTextOptions = true;
            this.Quantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Quantity.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantity.AppearanceHeader.Options.UseFont = true;
            this.Quantity.AppearanceHeader.Options.UseTextOptions = true;
            this.Quantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Quantity.Caption = "صافي الكمية";
            this.Quantity.FieldName = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 1;
            this.Quantity.Width = 173;
            // 
            // Cost
            // 
            this.Cost.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cost.AppearanceCell.Options.UseFont = true;
            this.Cost.AppearanceCell.Options.UseTextOptions = true;
            this.Cost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cost.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cost.AppearanceHeader.Options.UseFont = true;
            this.Cost.AppearanceHeader.Options.UseTextOptions = true;
            this.Cost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Cost.Caption = "قيمة المبيعات";
            this.Cost.FieldName = "Cost";
            this.Cost.Name = "Cost";
            this.Cost.Visible = true;
            this.Cost.VisibleIndex = 0;
            this.Cost.Width = 173;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "الكمية المرتجعة";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "الكمية المرتجعة";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "الكمية المرتجعة";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "الكمية المباعة";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "الكمية المباعة";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "البند";
            this.gridColumn6.FieldName = "f";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "البند";
            this.gridColumn7.FieldName = "f";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "البند";
            this.gridColumn8.FieldName = "f";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(585, 14);
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
            this.label6.Location = new System.Drawing.Point(585, 14);
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
            this.comBranch.Location = new System.Drawing.Point(407, 14);
            this.comBranch.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(175, 24);
            this.comBranch.TabIndex = 205;
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchID.BackColor = System.Drawing.Color.White;
            this.txtBranchID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(348, 14);
            this.txtBranchID.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(53, 24);
            this.txtBranchID.TabIndex = 204;
            this.txtBranchID.TabStop = false;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 547);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(895, 44);
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
            this.btnReport.Location = new System.Drawing.Point(803, 4);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(89, 36);
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
            this.panel2.Size = new System.Drawing.Size(794, 38);
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
            // DelegateSalesForProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 594);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DelegateSalesForProduct";
            this.Text = "DelegateSalesForProduct";
            this.Load += new System.EventHandler(this.DelegateSalesForProduct_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDelegateID;
        private System.Windows.Forms.ComboBox comDelegate;
        private System.Windows.Forms.Label labelDelegate;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.TextBox txtFactory;
        private DevExpress.XtraGrid.Columns.GridColumn Cost;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn QuantityReturned;
        private DevExpress.XtraGrid.Columns.GridColumn QuantitySaled;
        private DevExpress.XtraGrid.Columns.GridColumn CodeName;
        private DevExpress.XtraGrid.Columns.GridColumn Data_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
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
    }
}