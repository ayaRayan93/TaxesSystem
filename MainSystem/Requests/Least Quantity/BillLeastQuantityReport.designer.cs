namespace MainSystem
{
    partial class BillLeastQuantityReport
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOpenBill = new System.Windows.Forms.Button();
            this.btnPrint = new Bunifu.Framework.UI.BunifuTileButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNewChosen = new System.Windows.Forms.Button();
            this.comColor = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comSize = new System.Windows.Forms.ComboBox();
            this.comGroup = new System.Windows.Forms.ComboBox();
            this.comSort = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comType = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(3, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl1.Size = new System.Drawing.Size(878, 528);
            this.gridControl1.TabIndex = 83;
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
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsFind.FindNullPrompt = "بحث";
            this.gridView1.OptionsFind.ShowClearButton = false;
            this.gridView1.OptionsFind.ShowFindButton = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnOpenBill, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnPrint, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 617);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(878, 54);
            this.tableLayoutPanel3.TabIndex = 174;
            // 
            // btnOpenBill
            // 
            this.btnOpenBill.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOpenBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenBill.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnOpenBill.ForeColor = System.Drawing.Color.White;
            this.btnOpenBill.Location = new System.Drawing.Point(442, 3);
            this.btnOpenBill.Name = "btnOpenBill";
            this.btnOpenBill.Size = new System.Drawing.Size(94, 47);
            this.btnOpenBill.TabIndex = 21;
            this.btnOpenBill.Text = "اذن طلب";
            this.btnOpenBill.UseVisualStyleBackColor = true;
            this.btnOpenBill.Click += new System.EventHandler(this.btnOpenBill_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnPrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnPrint.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnPrint.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = global::MainSystem.Properties.Resources.Print_32;
            this.btnPrint.ImagePosition = 1;
            this.btnPrint.ImageZoom = 33;
            this.btnPrint.LabelPosition = 18;
            this.btnPrint.LabelText = "طباعة";
            this.btnPrint.Location = new System.Drawing.Point(351, 4);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(76, 46);
            this.btnPrint.TabIndex = 22;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 674);
            this.tableLayoutPanel1.TabIndex = 175;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel3.Controls.Add(this.btnNewChosen);
            this.panel3.Controls.Add(this.comColor);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.comSize);
            this.panel3.Controls.Add(this.comGroup);
            this.panel3.Controls.Add(this.comSort);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.comFactory);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.comProduct);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.comType);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Location = new System.Drawing.Point(74, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(736, 74);
            this.panel3.TabIndex = 175;
            // 
            // btnNewChosen
            // 
            this.btnNewChosen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewChosen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnNewChosen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewChosen.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewChosen.ForeColor = System.Drawing.Color.White;
            this.btnNewChosen.Location = new System.Drawing.Point(8, 5);
            this.btnNewChosen.Name = "btnNewChosen";
            this.btnNewChosen.Size = new System.Drawing.Size(53, 56);
            this.btnNewChosen.TabIndex = 21;
            this.btnNewChosen.Text = "اختيار اخر";
            this.btnNewChosen.UseVisualStyleBackColor = false;
            this.btnNewChosen.Click += new System.EventHandler(this.btnNewChosen_Click);
            // 
            // comColor
            // 
            this.comColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comColor.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comColor.FormattingEnabled = true;
            this.comColor.Location = new System.Drawing.Point(184, 34);
            this.comColor.Name = "comColor";
            this.comColor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comColor.Size = new System.Drawing.Size(100, 24);
            this.comColor.TabIndex = 19;
            this.comColor.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(67, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 30);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comSize
            // 
            this.comSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comSize.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comSize.FormattingEnabled = true;
            this.comSize.Location = new System.Drawing.Point(183, 5);
            this.comSize.Name = "comSize";
            this.comSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSize.Size = new System.Drawing.Size(100, 24);
            this.comSize.TabIndex = 18;
            this.comSize.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comGroup
            // 
            this.comGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comGroup.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comGroup.FormattingEnabled = true;
            this.comGroup.Location = new System.Drawing.Point(351, 5);
            this.comGroup.Name = "comGroup";
            this.comGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comGroup.Size = new System.Drawing.Size(120, 24);
            this.comGroup.TabIndex = 6;
            this.comGroup.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comSort
            // 
            this.comSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comSort.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comSort.FormattingEnabled = true;
            this.comSort.Location = new System.Drawing.Point(67, 5);
            this.comSort.Name = "comSort";
            this.comSort.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSort.Size = new System.Drawing.Size(70, 24);
            this.comSort.TabIndex = 17;
            this.comSort.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label15.Location = new System.Drawing.Point(676, 37);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(54, 19);
            this.label15.TabIndex = 5;
            this.label15.Text = "المصنع";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label4.Location = new System.Drawing.Point(288, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "اللون";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label14.Location = new System.Drawing.Point(475, 8);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(71, 19);
            this.label14.TabIndex = 8;
            this.label14.Text = "المجموعة";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label7.Location = new System.Drawing.Point(287, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "المقاس";
            // 
            // comFactory
            // 
            this.comFactory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comFactory.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Location = new System.Drawing.Point(552, 34);
            this.comFactory.Name = "comFactory";
            this.comFactory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comFactory.Size = new System.Drawing.Size(120, 24);
            this.comFactory.TabIndex = 3;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label16.Location = new System.Drawing.Point(141, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(36, 19);
            this.label16.TabIndex = 14;
            this.label16.Text = "الفرز";
            // 
            // comProduct
            // 
            this.comProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comProduct.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(351, 34);
            this.comProduct.Name = "comProduct";
            this.comProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comProduct.Size = new System.Drawing.Size(120, 24);
            this.comProduct.TabIndex = 9;
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label17.Location = new System.Drawing.Point(683, 8);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label17.Size = new System.Drawing.Size(41, 19);
            this.label17.TabIndex = 2;
            this.label17.Text = "النوع";
            // 
            // comType
            // 
            this.comType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comType.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(552, 5);
            this.comType.Name = "comType";
            this.comType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comType.Size = new System.Drawing.Size(120, 24);
            this.comType.TabIndex = 0;
            this.comType.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Neo Sans Arabic", 12F);
            this.label18.Location = new System.Drawing.Point(485, 37);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label18.Size = new System.Drawing.Size(51, 19);
            this.label18.TabIndex = 11;
            this.label18.Text = "الصنف";
            // 
            // LeastQuantityReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 674);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LeastQuantityReport";
            this.Text = "ادارة المشتريات";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnOpenBill;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNewChosen;
        private System.Windows.Forms.ComboBox comColor;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox comSize;
        private System.Windows.Forms.ComboBox comGroup;
        private System.Windows.Forms.ComboBox comSort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Label label18;
        private Bunifu.Framework.UI.BunifuTileButton btnPrint;
    }
}

