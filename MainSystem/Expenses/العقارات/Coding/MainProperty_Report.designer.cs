namespace MainSystem
{
    partial class MainProperty_Report
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
            this.txtMainAdd = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddMain = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panelAddMain = new System.Windows.Forms.Panel();
            this.panelUpdateMain = new System.Windows.Forms.Panel();
            this.btnMainUpdate = new System.Windows.Forms.Button();
            this.txtMainUpdate = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panelAddMain.SuspendLayout();
            this.panelUpdateMain.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
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
            this.btndisplayAll.Location = new System.Drawing.Point(273, 3);
            this.btndisplayAll.Name = "btndisplayAll";
            this.btndisplayAll.Size = new System.Drawing.Size(80, 30);
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
            this.comMain.SelectedValueChanged += new System.EventHandler(this.comSub_SelectedValueChanged);
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
            this.label1.Text = "العقار";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddNewMain
            // 
            this.btnAddNewMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddNewMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNewMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAddNewMain.ForeColor = System.Drawing.Color.White;
            this.btnAddNewMain.Location = new System.Drawing.Point(191, 48);
            this.btnAddNewMain.Name = "btnAddNewMain";
            this.btnAddNewMain.Size = new System.Drawing.Size(80, 30);
            this.btnAddNewMain.TabIndex = 13;
            this.btnAddNewMain.Text = "اضافة";
            this.btnAddNewMain.UseVisualStyleBackColor = false;
            this.btnAddNewMain.Click += new System.EventHandler(this.btnAddNewSub_Click);
            // 
            // txtMainAdd
            // 
            this.txtMainAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMainAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMainAdd.Location = new System.Drawing.Point(156, 16);
            this.txtMainAdd.Name = "txtMainAdd";
            this.txtMainAdd.Size = new System.Drawing.Size(150, 24);
            this.txtMainAdd.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddMain, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 749);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btnAddMain
            // 
            this.btnAddMain.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAddMain.ForeColor = System.Drawing.Color.White;
            this.btnAddMain.Location = new System.Drawing.Point(383, 614);
            this.btnAddMain.Name = "btnAddMain";
            this.btnAddMain.Size = new System.Drawing.Size(171, 32);
            this.btnAddMain.TabIndex = 18;
            this.btnAddMain.Text = "اضافة عقار جديد ";
            this.btnAddMain.UseVisualStyleBackColor = false;
            this.btnAddMain.Click += new System.EventHandler(this.btnAddMain_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panelAddMain, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panelUpdateMain, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 652);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(931, 94);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // panelAddMain
            // 
            this.panelAddMain.BackColor = System.Drawing.Color.White;
            this.panelAddMain.Controls.Add(this.btnAddNewMain);
            this.panelAddMain.Controls.Add(this.txtMainAdd);
            this.panelAddMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddMain.Location = new System.Drawing.Point(469, 3);
            this.panelAddMain.Name = "panelAddMain";
            this.panelAddMain.Size = new System.Drawing.Size(459, 88);
            this.panelAddMain.TabIndex = 4;
            // 
            // panelUpdateMain
            // 
            this.panelUpdateMain.BackColor = System.Drawing.Color.White;
            this.panelUpdateMain.Controls.Add(this.btnMainUpdate);
            this.panelUpdateMain.Controls.Add(this.txtMainUpdate);
            this.panelUpdateMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUpdateMain.Location = new System.Drawing.Point(3, 3);
            this.panelUpdateMain.Name = "panelUpdateMain";
            this.panelUpdateMain.Size = new System.Drawing.Size(460, 88);
            this.panelUpdateMain.TabIndex = 5;
            // 
            // btnMainUpdate
            // 
            this.btnMainUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnMainUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnMainUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnMainUpdate.ForeColor = System.Drawing.Color.White;
            this.btnMainUpdate.Location = new System.Drawing.Point(190, 45);
            this.btnMainUpdate.Name = "btnMainUpdate";
            this.btnMainUpdate.Size = new System.Drawing.Size(80, 30);
            this.btnMainUpdate.TabIndex = 15;
            this.btnMainUpdate.Text = "تعديل";
            this.btnMainUpdate.UseVisualStyleBackColor = false;
            this.btnMainUpdate.Click += new System.EventHandler(this.btnMainUpdate_Click);
            // 
            // txtMainUpdate
            // 
            this.txtMainUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMainUpdate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMainUpdate.Location = new System.Drawing.Point(155, 13);
            this.txtMainUpdate.Name = "txtMainUpdate";
            this.txtMainUpdate.Size = new System.Drawing.Size(150, 24);
            this.txtMainUpdate.TabIndex = 16;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.White;
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(931, 36);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 45);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(931, 561);
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
            // MainProperty_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainProperty_Report";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panelAddMain.ResumeLayout(false);
            this.panelAddMain.PerformLayout();
            this.panelUpdateMain.ResumeLayout(false);
            this.panelUpdateMain.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btndisplayAll;
        private System.Windows.Forms.ComboBox comMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewMain;
        private System.Windows.Forms.TextBox txtMainAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panelAddMain;
        private System.Windows.Forms.Button btnAddMain;
        private System.Windows.Forms.Panel panelUpdateMain;
        private System.Windows.Forms.Button btnMainUpdate;
        private System.Windows.Forms.TextBox txtMainUpdate;
    }
}

