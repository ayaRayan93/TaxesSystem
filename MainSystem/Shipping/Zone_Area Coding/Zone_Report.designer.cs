namespace TaxesSystem
{
    partial class Zone_Report
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
            this.btndisplayAll = new System.Windows.Forms.Button();
            this.comZone = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewZone = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelUpdateZone = new System.Windows.Forms.Panel();
            this.checkedListBoxAddArea = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxDeleteArea = new System.Windows.Forms.CheckedListBox();
            this.btnUpdateZone = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtZoneNameUpdate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelAddZone = new System.Windows.Forms.Panel();
            this.checkedListBoxAreas = new System.Windows.Forms.CheckedListBox();
            this.btnAddZone = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtZoneNameAdd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelUpdateZone.SuspendLayout();
            this.panelAddZone.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btndisplayAll
            // 
            this.btndisplayAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btndisplayAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndisplayAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndisplayAll.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btndisplayAll.ForeColor = System.Drawing.Color.White;
            this.btndisplayAll.Location = new System.Drawing.Point(383, 3);
            this.btndisplayAll.Name = "btndisplayAll";
            this.btndisplayAll.Size = new System.Drawing.Size(80, 32);
            this.btndisplayAll.TabIndex = 0;
            this.btndisplayAll.Text = "عرض الكل";
            this.btndisplayAll.UseVisualStyleBackColor = false;
            this.btndisplayAll.Click += new System.EventHandler(this.btndisplayAll_Click);
            // 
            // comZone
            // 
            this.comZone.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comZone.FormattingEnabled = true;
            this.comZone.Location = new System.Drawing.Point(542, 3);
            this.comZone.Name = "comZone";
            this.comZone.Size = new System.Drawing.Size(200, 24);
            this.comZone.TabIndex = 2;
            this.comZone.SelectedValueChanged += new System.EventHandler(this.comZone_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(748, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "اسم الزون";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddNewZone
            // 
            this.btnAddNewZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNewZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewZone.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnAddNewZone.ForeColor = System.Drawing.Color.White;
            this.btnAddNewZone.Location = new System.Drawing.Point(469, 4);
            this.btnAddNewZone.Name = "btnAddNewZone";
            this.btnAddNewZone.Size = new System.Drawing.Size(120, 32);
            this.btnAddNewZone.TabIndex = 12;
            this.btnAddNewZone.Text = "اضافة زون جديدة ";
            this.btnAddNewZone.UseVisualStyleBackColor = false;
            this.btnAddNewZone.Click += new System.EventHandler(this.btnAddNewZone_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdate.BackgroundImage = global::TaxesSystem.Properties.Resources.update;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(413, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(50, 32);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 749);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panelUpdateZone, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelAddZone, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 442);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(931, 304);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // panelUpdateZone
            // 
            this.panelUpdateZone.AutoScroll = true;
            this.panelUpdateZone.Controls.Add(this.checkedListBoxAddArea);
            this.panelUpdateZone.Controls.Add(this.checkedListBoxDeleteArea);
            this.panelUpdateZone.Controls.Add(this.btnUpdateZone);
            this.panelUpdateZone.Controls.Add(this.label3);
            this.panelUpdateZone.Controls.Add(this.txtZoneNameUpdate);
            this.panelUpdateZone.Controls.Add(this.label4);
            this.panelUpdateZone.Controls.Add(this.label2);
            this.panelUpdateZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUpdateZone.Location = new System.Drawing.Point(3, 3);
            this.panelUpdateZone.Name = "panelUpdateZone";
            this.panelUpdateZone.Size = new System.Drawing.Size(460, 298);
            this.panelUpdateZone.TabIndex = 11;
            // 
            // checkedListBoxAddArea
            // 
            this.checkedListBoxAddArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAddArea.CheckOnClick = true;
            this.checkedListBoxAddArea.FormattingEnabled = true;
            this.checkedListBoxAddArea.Location = new System.Drawing.Point(241, 55);
            this.checkedListBoxAddArea.Name = "checkedListBoxAddArea";
            this.checkedListBoxAddArea.Size = new System.Drawing.Size(120, 199);
            this.checkedListBoxAddArea.TabIndex = 7;
            // 
            // checkedListBoxDeleteArea
            // 
            this.checkedListBoxDeleteArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxDeleteArea.CheckOnClick = true;
            this.checkedListBoxDeleteArea.FormattingEnabled = true;
            this.checkedListBoxDeleteArea.Location = new System.Drawing.Point(21, 56);
            this.checkedListBoxDeleteArea.Name = "checkedListBoxDeleteArea";
            this.checkedListBoxDeleteArea.Size = new System.Drawing.Size(120, 199);
            this.checkedListBoxDeleteArea.TabIndex = 9;
            // 
            // btnUpdateZone
            // 
            this.btnUpdateZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdateZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateZone.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnUpdateZone.ForeColor = System.Drawing.Color.White;
            this.btnUpdateZone.Location = new System.Drawing.Point(150, 261);
            this.btnUpdateZone.Name = "btnUpdateZone";
            this.btnUpdateZone.Size = new System.Drawing.Size(80, 30);
            this.btnUpdateZone.TabIndex = 10;
            this.btnUpdateZone.Text = "تعديل";
            this.btnUpdateZone.UseVisualStyleBackColor = false;
            this.btnUpdateZone.Click += new System.EventHandler(this.btnUpdateZone_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(367, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "اضافة منطقة";
            // 
            // txtZoneNameUpdate
            // 
            this.txtZoneNameUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZoneNameUpdate.Enabled = false;
            this.txtZoneNameUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtZoneNameUpdate.Location = new System.Drawing.Point(103, 15);
            this.txtZoneNameUpdate.Name = "txtZoneNameUpdate";
            this.txtZoneNameUpdate.Size = new System.Drawing.Size(150, 24);
            this.txtZoneNameUpdate.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(147, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "حذف منطقه";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(259, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "اسم الزون";
            // 
            // panelAddZone
            // 
            this.panelAddZone.AutoScroll = true;
            this.panelAddZone.Controls.Add(this.checkedListBoxAreas);
            this.panelAddZone.Controls.Add(this.btnAddZone);
            this.panelAddZone.Controls.Add(this.label7);
            this.panelAddZone.Controls.Add(this.txtZoneNameAdd);
            this.panelAddZone.Controls.Add(this.label5);
            this.panelAddZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddZone.Location = new System.Drawing.Point(469, 3);
            this.panelAddZone.Name = "panelAddZone";
            this.panelAddZone.Size = new System.Drawing.Size(459, 298);
            this.panelAddZone.TabIndex = 0;
            // 
            // checkedListBoxAreas
            // 
            this.checkedListBoxAreas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAreas.CheckOnClick = true;
            this.checkedListBoxAreas.FormattingEnabled = true;
            this.checkedListBoxAreas.Location = new System.Drawing.Point(51, 55);
            this.checkedListBoxAreas.Name = "checkedListBoxAreas";
            this.checkedListBoxAreas.Size = new System.Drawing.Size(120, 199);
            this.checkedListBoxAreas.TabIndex = 7;
            // 
            // btnAddZone
            // 
            this.btnAddZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddZone.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnAddZone.ForeColor = System.Drawing.Color.White;
            this.btnAddZone.Location = new System.Drawing.Point(200, 261);
            this.btnAddZone.Name = "btnAddZone";
            this.btnAddZone.Size = new System.Drawing.Size(80, 30);
            this.btnAddZone.TabIndex = 10;
            this.btnAddZone.Text = "اضافه";
            this.btnAddZone.UseVisualStyleBackColor = false;
            this.btnAddZone.Click += new System.EventHandler(this.btnAddZone_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(71, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "اضافة مناطق";
            // 
            // txtZoneNameAdd
            // 
            this.txtZoneNameAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZoneNameAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtZoneNameAdd.Location = new System.Drawing.Point(200, 106);
            this.txtZoneNameAdd.Name = "txtZoneNameAdd";
            this.txtZoneNameAdd.Size = new System.Drawing.Size(150, 24);
            this.txtZoneNameAdd.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(356, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "اسم الزون";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btndisplayAll, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.comZone, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(931, 39);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnUpdate, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnAddNewZone, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 397);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(931, 39);
            this.tableLayoutPanel5.TabIndex = 16;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 48);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(931, 343);
            this.gridControl1.TabIndex = 17;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // Zone_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Zone_Report";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelUpdateZone.ResumeLayout(false);
            this.panelUpdateZone.PerformLayout();
            this.panelAddZone.ResumeLayout(false);
            this.panelAddZone.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btndisplayAll;
        private System.Windows.Forms.ComboBox comZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewZone;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckedListBox checkedListBoxAreas;
        private System.Windows.Forms.Button btnAddZone;
        private System.Windows.Forms.TextBox txtZoneNameAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox checkedListBoxAddArea;
        private System.Windows.Forms.Button btnUpdateZone;
        private System.Windows.Forms.TextBox txtZoneNameUpdate;
        private System.Windows.Forms.CheckedListBox checkedListBoxDeleteArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panelUpdateZone;
        private System.Windows.Forms.Panel panelAddZone;
    }
}

