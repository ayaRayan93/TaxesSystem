namespace TaxesSystem
{
    partial class SubExpenses_Report
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
            this.comSub = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewSub = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelUpdateSub = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comMainUpdate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateSub = new System.Windows.Forms.Button();
            this.txtSubNameUpdate = new System.Windows.Forms.TextBox();
            this.panelAddSub = new System.Windows.Forms.Panel();
            this.comMainAdd = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddSub = new System.Windows.Forms.Button();
            this.txtSubNameAdd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelUpdateSub.SuspendLayout();
            this.panelAddSub.SuspendLayout();
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
            // comSub
            // 
            this.comSub.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comSub.FormattingEnabled = true;
            this.comSub.Location = new System.Drawing.Point(379, 3);
            this.comSub.Name = "comSub";
            this.comSub.Size = new System.Drawing.Size(174, 24);
            this.comSub.TabIndex = 2;
            this.comSub.SelectedValueChanged += new System.EventHandler(this.comMain_SelectedValueChanged);
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
            this.label1.Text = "المصروف الفرعى";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddNewSub
            // 
            this.btnAddNewSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNewSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewSub.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnAddNewSub.ForeColor = System.Drawing.Color.White;
            this.btnAddNewSub.Location = new System.Drawing.Point(469, 4);
            this.btnAddNewSub.Name = "btnAddNewSub";
            this.btnAddNewSub.Size = new System.Drawing.Size(171, 32);
            this.btnAddNewSub.TabIndex = 12;
            this.btnAddNewSub.Text = "اضافة مصروف فرعى جديد ";
            this.btnAddNewSub.UseVisualStyleBackColor = false;
            this.btnAddNewSub.Click += new System.EventHandler(this.btnAddNewMain_Click);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 749);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panelUpdateSub, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelAddSub, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 602);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(931, 144);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // panelUpdateSub
            // 
            this.panelUpdateSub.AutoScroll = true;
            this.panelUpdateSub.Controls.Add(this.label2);
            this.panelUpdateSub.Controls.Add(this.comMainUpdate);
            this.panelUpdateSub.Controls.Add(this.label3);
            this.panelUpdateSub.Controls.Add(this.btnUpdateSub);
            this.panelUpdateSub.Controls.Add(this.txtSubNameUpdate);
            this.panelUpdateSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUpdateSub.Location = new System.Drawing.Point(3, 3);
            this.panelUpdateSub.Name = "panelUpdateSub";
            this.panelUpdateSub.Size = new System.Drawing.Size(460, 138);
            this.panelUpdateSub.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(259, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "اسم المصروف الفرعى";
            // 
            // comMainUpdate
            // 
            this.comMainUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comMainUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comMainUpdate.FormattingEnabled = true;
            this.comMainUpdate.Location = new System.Drawing.Point(103, 48);
            this.comMainUpdate.Name = "comMainUpdate";
            this.comMainUpdate.Size = new System.Drawing.Size(150, 24);
            this.comMainUpdate.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(259, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "المصروف الرئيسى";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUpdateSub
            // 
            this.btnUpdateSub.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdateSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdateSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateSub.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnUpdateSub.ForeColor = System.Drawing.Color.White;
            this.btnUpdateSub.Location = new System.Drawing.Point(138, 87);
            this.btnUpdateSub.Name = "btnUpdateSub";
            this.btnUpdateSub.Size = new System.Drawing.Size(80, 30);
            this.btnUpdateSub.TabIndex = 10;
            this.btnUpdateSub.Text = "تعديل";
            this.btnUpdateSub.UseVisualStyleBackColor = false;
            this.btnUpdateSub.Click += new System.EventHandler(this.btnUpdateMain_Click);
            // 
            // txtSubNameUpdate
            // 
            this.txtSubNameUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSubNameUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSubNameUpdate.Location = new System.Drawing.Point(103, 18);
            this.txtSubNameUpdate.Name = "txtSubNameUpdate";
            this.txtSubNameUpdate.Size = new System.Drawing.Size(150, 24);
            this.txtSubNameUpdate.TabIndex = 4;
            // 
            // panelAddSub
            // 
            this.panelAddSub.AutoScroll = true;
            this.panelAddSub.Controls.Add(this.comMainAdd);
            this.panelAddSub.Controls.Add(this.label6);
            this.panelAddSub.Controls.Add(this.btnAddSub);
            this.panelAddSub.Controls.Add(this.txtSubNameAdd);
            this.panelAddSub.Controls.Add(this.label5);
            this.panelAddSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddSub.Location = new System.Drawing.Point(469, 3);
            this.panelAddSub.Name = "panelAddSub";
            this.panelAddSub.Size = new System.Drawing.Size(459, 138);
            this.panelAddSub.TabIndex = 0;
            // 
            // comMainAdd
            // 
            this.comMainAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comMainAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comMainAdd.FormattingEnabled = true;
            this.comMainAdd.Location = new System.Drawing.Point(92, 48);
            this.comMainAdd.Name = "comMainAdd";
            this.comMainAdd.Size = new System.Drawing.Size(150, 24);
            this.comMainAdd.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(248, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "المصروف الرئيسى";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddSub
            // 
            this.btnAddSub.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSub.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnAddSub.ForeColor = System.Drawing.Color.White;
            this.btnAddSub.Location = new System.Drawing.Point(127, 87);
            this.btnAddSub.Name = "btnAddSub";
            this.btnAddSub.Size = new System.Drawing.Size(80, 30);
            this.btnAddSub.TabIndex = 10;
            this.btnAddSub.Text = "اضافه";
            this.btnAddSub.UseVisualStyleBackColor = false;
            this.btnAddSub.Click += new System.EventHandler(this.btnAddMain_Click);
            // 
            // txtSubNameAdd
            // 
            this.txtSubNameAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSubNameAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSubNameAdd.Location = new System.Drawing.Point(92, 18);
            this.txtSubNameAdd.Name = "txtSubNameAdd";
            this.txtSubNameAdd.Size = new System.Drawing.Size(150, 24);
            this.txtSubNameAdd.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(248, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "اسم المصروف الفرعى";
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
            this.tableLayoutPanel4.Controls.Add(this.comSub, 2, 0);
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
            this.tableLayoutPanel5.Controls.Add(this.btnAddNewSub, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 557);
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
            this.gridControl1.Size = new System.Drawing.Size(931, 503);
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
            // SubExpenses_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SubExpenses_Report";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelUpdateSub.ResumeLayout(false);
            this.panelUpdateSub.PerformLayout();
            this.panelAddSub.ResumeLayout(false);
            this.panelAddSub.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btndisplayAll;
        private System.Windows.Forms.ComboBox comSub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewSub;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnAddSub;
        private System.Windows.Forms.TextBox txtSubNameAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUpdateSub;
        private System.Windows.Forms.TextBox txtSubNameUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panelUpdateSub;
        private System.Windows.Forms.Panel panelAddSub;
        private System.Windows.Forms.ComboBox comMainAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comMainUpdate;
        private System.Windows.Forms.Label label3;
    }
}

