namespace MainSystem
{
    partial class MainExpenses_Report
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
            this.comMain = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewMain = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelUpdateMain = new System.Windows.Forms.Panel();
            this.checkedListBoxAddSub = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxDeleteSub = new System.Windows.Forms.CheckedListBox();
            this.btnUpdateMain = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMainNameUpdate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelAddMain = new System.Windows.Forms.Panel();
            this.checkedListBoxSub = new System.Windows.Forms.CheckedListBox();
            this.btnAddMain = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMainNameAdd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelUpdateMain.SuspendLayout();
            this.panelAddMain.SuspendLayout();
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
            this.btndisplayAll.Location = new System.Drawing.Point(273, 3);
            this.btndisplayAll.Name = "btndisplayAll";
            this.btndisplayAll.Size = new System.Drawing.Size(80, 32);
            this.btndisplayAll.TabIndex = 0;
            this.btndisplayAll.Text = "عرض الكل";
            this.btndisplayAll.UseVisualStyleBackColor = false;
            this.btndisplayAll.Click += new System.EventHandler(this.btndisplayAll_Click);
            // 
            // comMain
            // 
            this.comMain.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comMain.FormattingEnabled = true;
            this.comMain.Location = new System.Drawing.Point(379, 3);
            this.comMain.Name = "comMain";
            this.comMain.Size = new System.Drawing.Size(174, 24);
            this.comMain.TabIndex = 2;
            this.comMain.SelectedValueChanged += new System.EventHandler(this.comMain_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(559, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "المصروف الرئيسى";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddNewMain
            // 
            this.btnAddNewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNewMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewMain.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnAddNewMain.ForeColor = System.Drawing.Color.White;
            this.btnAddNewMain.Location = new System.Drawing.Point(469, 4);
            this.btnAddNewMain.Name = "btnAddNewMain";
            this.btnAddNewMain.Size = new System.Drawing.Size(171, 32);
            this.btnAddNewMain.TabIndex = 12;
            this.btnAddNewMain.Text = "اضافة مصروف رئيسى جديد ";
            this.btnAddNewMain.UseVisualStyleBackColor = false;
            this.btnAddNewMain.Click += new System.EventHandler(this.btnAddNewMain_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdate.BackgroundImage = global::MainSystem.Properties.Resources.update;
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
            this.tableLayoutPanel2.Controls.Add(this.panelUpdateMain, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelAddMain, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 442);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(931, 304);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // panelUpdateMain
            // 
            this.panelUpdateMain.AutoScroll = true;
            this.panelUpdateMain.Controls.Add(this.checkedListBoxAddSub);
            this.panelUpdateMain.Controls.Add(this.checkedListBoxDeleteSub);
            this.panelUpdateMain.Controls.Add(this.btnUpdateMain);
            this.panelUpdateMain.Controls.Add(this.label3);
            this.panelUpdateMain.Controls.Add(this.txtMainNameUpdate);
            this.panelUpdateMain.Controls.Add(this.label4);
            this.panelUpdateMain.Controls.Add(this.label2);
            this.panelUpdateMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUpdateMain.Location = new System.Drawing.Point(3, 3);
            this.panelUpdateMain.Name = "panelUpdateMain";
            this.panelUpdateMain.Size = new System.Drawing.Size(460, 298);
            this.panelUpdateMain.TabIndex = 11;
            // 
            // checkedListBoxAddSub
            // 
            this.checkedListBoxAddSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAddSub.CheckOnClick = true;
            this.checkedListBoxAddSub.FormattingEnabled = true;
            this.checkedListBoxAddSub.Location = new System.Drawing.Point(241, 55);
            this.checkedListBoxAddSub.Name = "checkedListBoxAddSub";
            this.checkedListBoxAddSub.Size = new System.Drawing.Size(120, 199);
            this.checkedListBoxAddSub.TabIndex = 7;
            // 
            // checkedListBoxDeleteSub
            // 
            this.checkedListBoxDeleteSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxDeleteSub.CheckOnClick = true;
            this.checkedListBoxDeleteSub.FormattingEnabled = true;
            this.checkedListBoxDeleteSub.Location = new System.Drawing.Point(21, 56);
            this.checkedListBoxDeleteSub.Name = "checkedListBoxDeleteSub";
            this.checkedListBoxDeleteSub.Size = new System.Drawing.Size(120, 199);
            this.checkedListBoxDeleteSub.TabIndex = 9;
            // 
            // btnUpdateMain
            // 
            this.btnUpdateMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdateMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateMain.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnUpdateMain.ForeColor = System.Drawing.Color.White;
            this.btnUpdateMain.Location = new System.Drawing.Point(150, 261);
            this.btnUpdateMain.Name = "btnUpdateMain";
            this.btnUpdateMain.Size = new System.Drawing.Size(80, 30);
            this.btnUpdateMain.TabIndex = 10;
            this.btnUpdateMain.Text = "تعديل";
            this.btnUpdateMain.UseVisualStyleBackColor = false;
            this.btnUpdateMain.Click += new System.EventHandler(this.btnUpdateMain_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(365, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 40);
            this.label3.TabIndex = 6;
            this.label3.Text = "اضافة مصروف فرعى";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMainNameUpdate
            // 
            this.txtMainNameUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMainNameUpdate.Enabled = false;
            this.txtMainNameUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMainNameUpdate.Location = new System.Drawing.Point(103, 15);
            this.txtMainNameUpdate.Name = "txtMainNameUpdate";
            this.txtMainNameUpdate.Size = new System.Drawing.Size(150, 24);
            this.txtMainNameUpdate.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(145, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 40);
            this.label4.TabIndex = 8;
            this.label4.Text = "حذف مصروف فرعى";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(259, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "اسم المصروف الرئيسى";
            // 
            // panelAddMain
            // 
            this.panelAddMain.AutoScroll = true;
            this.panelAddMain.Controls.Add(this.checkedListBoxSub);
            this.panelAddMain.Controls.Add(this.btnAddMain);
            this.panelAddMain.Controls.Add(this.label7);
            this.panelAddMain.Controls.Add(this.txtMainNameAdd);
            this.panelAddMain.Controls.Add(this.label5);
            this.panelAddMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddMain.Location = new System.Drawing.Point(469, 3);
            this.panelAddMain.Name = "panelAddMain";
            this.panelAddMain.Size = new System.Drawing.Size(459, 298);
            this.panelAddMain.TabIndex = 0;
            // 
            // checkedListBoxSub
            // 
            this.checkedListBoxSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxSub.CheckOnClick = true;
            this.checkedListBoxSub.FormattingEnabled = true;
            this.checkedListBoxSub.Location = new System.Drawing.Point(22, 55);
            this.checkedListBoxSub.Name = "checkedListBoxSub";
            this.checkedListBoxSub.Size = new System.Drawing.Size(120, 199);
            this.checkedListBoxSub.TabIndex = 7;
            // 
            // btnAddMain
            // 
            this.btnAddMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMain.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnAddMain.ForeColor = System.Drawing.Color.White;
            this.btnAddMain.Location = new System.Drawing.Point(200, 261);
            this.btnAddMain.Name = "btnAddMain";
            this.btnAddMain.Size = new System.Drawing.Size(80, 30);
            this.btnAddMain.TabIndex = 10;
            this.btnAddMain.Text = "اضافه";
            this.btnAddMain.UseVisualStyleBackColor = false;
            this.btnAddMain.Click += new System.EventHandler(this.btnAddMain_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(20, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "اضافة مصروف فرعى";
            // 
            // txtMainNameAdd
            // 
            this.txtMainNameAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMainNameAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMainNameAdd.Location = new System.Drawing.Point(152, 106);
            this.txtMainNameAdd.Name = "txtMainNameAdd";
            this.txtMainNameAdd.Size = new System.Drawing.Size(150, 24);
            this.txtMainNameAdd.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(308, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "اسم المصروف الرئيسى";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.comMain, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btndisplayAll, 4, 0);
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
            this.tableLayoutPanel5.Controls.Add(this.btnAddNewMain, 0, 0);
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
            // MainExpenses_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainExpenses_Report";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelUpdateMain.ResumeLayout(false);
            this.panelUpdateMain.PerformLayout();
            this.panelAddMain.ResumeLayout(false);
            this.panelAddMain.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btndisplayAll;
        private System.Windows.Forms.ComboBox comMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewMain;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckedListBox checkedListBoxSub;
        private System.Windows.Forms.Button btnAddMain;
        private System.Windows.Forms.TextBox txtMainNameAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox checkedListBoxAddSub;
        private System.Windows.Forms.Button btnUpdateMain;
        private System.Windows.Forms.TextBox txtMainNameUpdate;
        private System.Windows.Forms.CheckedListBox checkedListBoxDeleteSub;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panelUpdateMain;
        private System.Windows.Forms.Panel panelAddMain;
    }
}

