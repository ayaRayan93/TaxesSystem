namespace MainSystem
{
    partial class Main_SubProperty
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
            this.btnSub = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageSub = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPageMain = new DevExpress.XtraTab.XtraTabPage();
            this.btnDetails = new System.Windows.Forms.Button();
            this.xtraTabPageDetails = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.xtraTabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(959, 723);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnDetails, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMain, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSub, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(953, 37);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnSub
            // 
            this.btnSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSub.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSub.FlatAppearance.BorderSize = 0;
            this.btnSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSub.Location = new System.Drawing.Point(423, 4);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(110, 30);
            this.btnSub.TabIndex = 0;
            this.btnSub.Text = "المصروف الرئيسى";
            this.btnSub.UseVisualStyleBackColor = false;
            this.btnSub.Click += new System.EventHandler(this.btnArea_Click);
            // 
            // btnMain
            // 
            this.btnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMain.BackColor = System.Drawing.Color.White;
            this.btnMain.FlatAppearance.BorderSize = 0;
            this.btnMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnMain.Location = new System.Drawing.Point(539, 4);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(90, 30);
            this.btnMain.TabIndex = 1;
            this.btnMain.Text = "عقار";
            this.btnMain.UseVisualStyleBackColor = false;
            this.btnMain.Click += new System.EventHandler(this.btnZone_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(1, 44);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageSub;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Size = new System.Drawing.Size(957, 678);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageSub,
            this.xtraTabPageMain,
            this.xtraTabPageDetails});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPageSub
            // 
            this.xtraTabPageSub.Name = "xtraTabPageSub";
            this.xtraTabPageSub.Size = new System.Drawing.Size(951, 672);
            // 
            // xtraTabPageMain
            // 
            this.xtraTabPageMain.Name = "xtraTabPageMain";
            this.xtraTabPageMain.Size = new System.Drawing.Size(951, 672);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetails.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDetails.FlatAppearance.BorderSize = 0;
            this.btnDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDetails.Location = new System.Drawing.Point(303, 4);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(110, 30);
            this.btnDetails.TabIndex = 2;
            this.btnDetails.Text = "المصروف الفرعى";
            this.btnDetails.UseVisualStyleBackColor = false;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Name = "xtraTabPageDetails";
            this.xtraTabPageDetails.Size = new System.Drawing.Size(951, 672);
            // 
            // Main_SubProperty
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 723);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Main_SubProperty";
            this.Text = "Zone_Area";
            this.Load += new System.EventHandler(this.Zone_Area_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSub;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageMain;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnDetails;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDetails;
    }
}