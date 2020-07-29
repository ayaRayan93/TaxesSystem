namespace TaxesSystem
{
    partial class DetailsProperty_Report
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
            this.comDetails = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewDetails = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelUpdateSub = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comSubUpdate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateDetails = new System.Windows.Forms.Button();
            this.txtDetailsNameUpdate = new System.Windows.Forms.TextBox();
            this.panelAddSub = new System.Windows.Forms.Panel();
            this.comSubAdd = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddDetails = new System.Windows.Forms.Button();
            this.txtDetailsNameAdd = new System.Windows.Forms.TextBox();
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
            this.btndisplayAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btndisplayAll.ForeColor = System.Drawing.Color.White;
            this.btndisplayAll.Location = new System.Drawing.Point(258, 3);
            this.btndisplayAll.Name = "btndisplayAll";
            this.btndisplayAll.Size = new System.Drawing.Size(80, 32);
            this.btndisplayAll.TabIndex = 0;
            this.btndisplayAll.Text = "عرض الكل";
            this.btndisplayAll.UseVisualStyleBackColor = false;
            this.btndisplayAll.Click += new System.EventHandler(this.btndisplayAll_Click);
            // 
            // comDetails
            // 
            this.comDetails.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comDetails.FormattingEnabled = true;
            this.comDetails.Location = new System.Drawing.Point(364, 3);
            this.comDetails.Name = "comDetails";
            this.comDetails.Size = new System.Drawing.Size(174, 24);
            this.comDetails.TabIndex = 2;
            this.comDetails.SelectedValueChanged += new System.EventHandler(this.comMain_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(544, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "المصروف الفرعى";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddNewDetails
            // 
            this.btnAddNewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAddNewDetails.ForeColor = System.Drawing.Color.White;
            this.btnAddNewDetails.Location = new System.Drawing.Point(469, 4);
            this.btnAddNewDetails.Name = "btnAddNewDetails";
            this.btnAddNewDetails.Size = new System.Drawing.Size(171, 32);
            this.btnAddNewDetails.TabIndex = 12;
            this.btnAddNewDetails.Text = "اضافة مصروف فرعى جديد ";
            this.btnAddNewDetails.UseVisualStyleBackColor = false;
            this.btnAddNewDetails.Click += new System.EventHandler(this.btnAddNewMain_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdate.BackgroundImage = global::TaxesSystem.Properties.Resources.update;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
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
            this.panelUpdateSub.BackColor = System.Drawing.Color.White;
            this.panelUpdateSub.Controls.Add(this.label2);
            this.panelUpdateSub.Controls.Add(this.comSubUpdate);
            this.panelUpdateSub.Controls.Add(this.label3);
            this.panelUpdateSub.Controls.Add(this.btnUpdateDetails);
            this.panelUpdateSub.Controls.Add(this.txtDetailsNameUpdate);
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
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "المصروف الفرعى";
            // 
            // comSubUpdate
            // 
            this.comSubUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSubUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comSubUpdate.FormattingEnabled = true;
            this.comSubUpdate.Location = new System.Drawing.Point(103, 48);
            this.comSubUpdate.Name = "comSubUpdate";
            this.comSubUpdate.Size = new System.Drawing.Size(150, 24);
            this.comSubUpdate.TabIndex = 13;
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
            // btnUpdateDetails
            // 
            this.btnUpdateDetails.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdateDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnUpdateDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnUpdateDetails.ForeColor = System.Drawing.Color.White;
            this.btnUpdateDetails.Location = new System.Drawing.Point(138, 87);
            this.btnUpdateDetails.Name = "btnUpdateDetails";
            this.btnUpdateDetails.Size = new System.Drawing.Size(80, 30);
            this.btnUpdateDetails.TabIndex = 10;
            this.btnUpdateDetails.Text = "تعديل";
            this.btnUpdateDetails.UseVisualStyleBackColor = false;
            this.btnUpdateDetails.Click += new System.EventHandler(this.btnUpdateMain_Click);
            // 
            // txtDetailsNameUpdate
            // 
            this.txtDetailsNameUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDetailsNameUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDetailsNameUpdate.Location = new System.Drawing.Point(103, 18);
            this.txtDetailsNameUpdate.Name = "txtDetailsNameUpdate";
            this.txtDetailsNameUpdate.Size = new System.Drawing.Size(150, 24);
            this.txtDetailsNameUpdate.TabIndex = 4;
            // 
            // panelAddSub
            // 
            this.panelAddSub.AutoScroll = true;
            this.panelAddSub.BackColor = System.Drawing.Color.White;
            this.panelAddSub.Controls.Add(this.comSubAdd);
            this.panelAddSub.Controls.Add(this.label6);
            this.panelAddSub.Controls.Add(this.btnAddDetails);
            this.panelAddSub.Controls.Add(this.txtDetailsNameAdd);
            this.panelAddSub.Controls.Add(this.label5);
            this.panelAddSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddSub.Location = new System.Drawing.Point(469, 3);
            this.panelAddSub.Name = "panelAddSub";
            this.panelAddSub.Size = new System.Drawing.Size(459, 138);
            this.panelAddSub.TabIndex = 0;
            // 
            // comSubAdd
            // 
            this.comSubAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSubAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comSubAdd.FormattingEnabled = true;
            this.comSubAdd.Location = new System.Drawing.Point(92, 48);
            this.comSubAdd.Name = "comSubAdd";
            this.comSubAdd.Size = new System.Drawing.Size(150, 24);
            this.comSubAdd.TabIndex = 11;
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
            // btnAddDetails
            // 
            this.btnAddDetails.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAddDetails.ForeColor = System.Drawing.Color.White;
            this.btnAddDetails.Location = new System.Drawing.Point(127, 87);
            this.btnAddDetails.Name = "btnAddDetails";
            this.btnAddDetails.Size = new System.Drawing.Size(80, 30);
            this.btnAddDetails.TabIndex = 10;
            this.btnAddDetails.Text = "اضافه";
            this.btnAddDetails.UseVisualStyleBackColor = false;
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddMain_Click);
            // 
            // txtDetailsNameAdd
            // 
            this.txtDetailsNameAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDetailsNameAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDetailsNameAdd.Location = new System.Drawing.Point(92, 18);
            this.txtDetailsNameAdd.Name = "txtDetailsNameAdd";
            this.txtDetailsNameAdd.Size = new System.Drawing.Size(150, 24);
            this.txtDetailsNameAdd.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(248, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "المصروف الفرعى";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.comDetails, 2, 0);
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
            this.tableLayoutPanel5.Controls.Add(this.btnAddNewDetails, 0, 0);
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
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // DetailsProperty_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DetailsProperty_Report";
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
        private System.Windows.Forms.ComboBox comDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewDetails;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnAddDetails;
        private System.Windows.Forms.TextBox txtDetailsNameAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUpdateDetails;
        private System.Windows.Forms.TextBox txtDetailsNameUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panelUpdateSub;
        private System.Windows.Forms.Panel panelAddSub;
        private System.Windows.Forms.ComboBox comSubAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comSubUpdate;
        private System.Windows.Forms.Label label3;
    }
}

