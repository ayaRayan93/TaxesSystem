namespace MainSystem
{
    partial class Cars
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
            this.gridControlStores = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnUpdate = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnDelete = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnReport = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnCarPaper = new Bunifu.Framework.UI.BunifuTileButton();
            this.bunifuTileButton1 = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnCarDrivers = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnSpareParts = new Bunifu.Framework.UI.BunifuTileButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radCompanyCar = new System.Windows.Forms.RadioButton();
            this.radPrivateCar = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlStores
            // 
            this.gridControlStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlStores.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControlStores.Location = new System.Drawing.Point(0, 60);
            this.gridControlStores.MainView = this.gridView1;
            this.gridControlStores.Margin = new System.Windows.Forms.Padding(0);
            this.gridControlStores.Name = "gridControlStores";
            this.gridControlStores.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControlStores.Size = new System.Drawing.Size(881, 363);
            this.gridControlStores.TabIndex = 6;
            this.gridControlStores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gridControlStores;
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
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 10;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUpdate, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnReport, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCarPaper, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.bunifuTileButton1, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCarDrivers, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSpareParts, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 423);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(881, 47);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::MainSystem.Properties.Resources.File_32;
            this.btnAdd.ImagePosition = 1;
            this.btnAdd.ImageZoom = 20;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "اضافة";
            this.btnAdd.Location = new System.Drawing.Point(708, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 39);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdate.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdate.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = global::MainSystem.Properties.Resources.Edit_32;
            this.btnUpdate.ImagePosition = 1;
            this.btnUpdate.ImageZoom = 20;
            this.btnUpdate.LabelPosition = 18;
            this.btnUpdate.LabelText = "تعديل";
            this.btnUpdate.Location = new System.Drawing.Point(620, 4);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(82, 39);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = global::MainSystem.Properties.Resources.Delete_32;
            this.btnDelete.ImagePosition = 1;
            this.btnDelete.ImageZoom = 20;
            this.btnDelete.LabelPosition = 18;
            this.btnDelete.LabelText = "حذف";
            this.btnDelete.Location = new System.Drawing.Point(532, 4);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(82, 39);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnReport.LabelText = "تقرير";
            this.btnReport.Location = new System.Drawing.Point(92, 4);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(82, 39);
            this.btnReport.TabIndex = 3;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnCarPaper
            // 
            this.btnCarPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCarPaper.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCarPaper.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCarPaper.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarPaper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCarPaper.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarPaper.ForeColor = System.Drawing.Color.White;
            this.btnCarPaper.Image = null;
            this.btnCarPaper.ImagePosition = 1;
            this.btnCarPaper.ImageZoom = 25;
            this.btnCarPaper.LabelPosition = 18;
            this.btnCarPaper.LabelText = "اوراق السيارة";
            this.btnCarPaper.Location = new System.Drawing.Point(356, 4);
            this.btnCarPaper.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCarPaper.Name = "btnCarPaper";
            this.btnCarPaper.Size = new System.Drawing.Size(82, 39);
            this.btnCarPaper.TabIndex = 4;
            this.btnCarPaper.Click += new System.EventHandler(this.btnCarPapers_Click);
            // 
            // bunifuTileButton1
            // 
            this.bunifuTileButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTileButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bunifuTileButton1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuTileButton1.ForeColor = System.Drawing.Color.White;
            this.bunifuTileButton1.Image = null;
            this.bunifuTileButton1.ImagePosition = 1;
            this.bunifuTileButton1.ImageZoom = 25;
            this.bunifuTileButton1.LabelPosition = 18;
            this.bunifuTileButton1.LabelText = "الرخصة";
            this.bunifuTileButton1.Location = new System.Drawing.Point(268, 4);
            this.bunifuTileButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bunifuTileButton1.Name = "bunifuTileButton1";
            this.bunifuTileButton1.Size = new System.Drawing.Size(82, 39);
            this.bunifuTileButton1.TabIndex = 5;
            this.bunifuTileButton1.Click += new System.EventHandler(this.btnCarLicense_Click);
            // 
            // btnCarDrivers
            // 
            this.btnCarDrivers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCarDrivers.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCarDrivers.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnCarDrivers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCarDrivers.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarDrivers.ForeColor = System.Drawing.Color.White;
            this.btnCarDrivers.Image = null;
            this.btnCarDrivers.ImagePosition = 1;
            this.btnCarDrivers.ImageZoom = 0;
            this.btnCarDrivers.LabelPosition = 18;
            this.btnCarDrivers.LabelText = "السائقين";
            this.btnCarDrivers.Location = new System.Drawing.Point(444, 4);
            this.btnCarDrivers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCarDrivers.Name = "btnCarDrivers";
            this.btnCarDrivers.Size = new System.Drawing.Size(82, 39);
            this.btnCarDrivers.TabIndex = 6;
            this.btnCarDrivers.Click += new System.EventHandler(this.btnCarDrivers_Click);
            // 
            // btnSpareParts
            // 
            this.btnSpareParts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSpareParts.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSpareParts.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSpareParts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpareParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSpareParts.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpareParts.ForeColor = System.Drawing.Color.White;
            this.btnSpareParts.Image = null;
            this.btnSpareParts.ImagePosition = 1;
            this.btnSpareParts.ImageZoom = 25;
            this.btnSpareParts.LabelPosition = 18;
            this.btnSpareParts.LabelText = "قطع الغيار";
            this.btnSpareParts.Location = new System.Drawing.Point(180, 4);
            this.btnSpareParts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSpareParts.Name = "btnSpareParts";
            this.btnSpareParts.Size = new System.Drawing.Size(82, 39);
            this.btnSpareParts.TabIndex = 7;
            this.btnSpareParts.Click += new System.EventHandler(this.btnSpareParts_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControlStores, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 423);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.radCompanyCar);
            this.panel1.Controls.Add(this.radPrivateCar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 60);
            this.panel1.TabIndex = 7;
            // 
            // radCompanyCar
            // 
            this.radCompanyCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radCompanyCar.AutoSize = true;
            this.radCompanyCar.Checked = true;
            this.radCompanyCar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCompanyCar.Location = new System.Drawing.Point(494, 22);
            this.radCompanyCar.Name = "radCompanyCar";
            this.radCompanyCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCompanyCar.Size = new System.Drawing.Size(105, 23);
            this.radCompanyCar.TabIndex = 45;
            this.radCompanyCar.TabStop = true;
            this.radCompanyCar.Text = "سيارات الشركة";
            this.radCompanyCar.UseVisualStyleBackColor = true;
            this.radCompanyCar.CheckedChanged += new System.EventHandler(this.radCompanyCar_CheckedChanged);
            // 
            // radPrivateCar
            // 
            this.radPrivateCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radPrivateCar.AutoSize = true;
            this.radPrivateCar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPrivateCar.Location = new System.Drawing.Point(359, 22);
            this.radPrivateCar.Name = "radPrivateCar";
            this.radPrivateCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPrivateCar.Size = new System.Drawing.Size(102, 23);
            this.radPrivateCar.TabIndex = 44;
            this.radPrivateCar.Text = "سيارات خاصة";
            this.radPrivateCar.UseVisualStyleBackColor = true;
            // 
            // Cars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 470);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "Cars";
            this.Text = "Cars";
            this.Load += new System.EventHandler(this.Cars_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlStores;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private Bunifu.Framework.UI.BunifuTileButton btnUpdate;
        private Bunifu.Framework.UI.BunifuTileButton btnDelete;
        private Bunifu.Framework.UI.BunifuTileButton btnReport;
        private Bunifu.Framework.UI.BunifuTileButton btnCarPaper;
        private Bunifu.Framework.UI.BunifuTileButton bunifuTileButton1;
        private Bunifu.Framework.UI.BunifuTileButton btnCarDrivers;
        private Bunifu.Framework.UI.BunifuTileButton btnSpareParts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radCompanyCar;
        private System.Windows.Forms.RadioButton radPrivateCar;
    }
}