namespace MainSystem
{
    partial class checkPaidBillsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labRecivedMoney = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panHeader = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.btnNewChooes = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.radDealer = new System.Windows.Forms.RadioButton();
            this.comEngCon = new System.Windows.Forms.ComboBox();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.radCon = new System.Windows.Forms.RadioButton();
            this.radEng = new System.Windows.Forms.RadioButton();
            this.radClient = new System.Windows.Forms.RadioButton();
            this.labelEng = new System.Windows.Forms.Label();
            this.panFooter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(50, 129);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Size = new System.Drawing.Size(734, 398);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // labRecivedMoney
            // 
            this.labRecivedMoney.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labRecivedMoney.AutoSize = true;
            this.labRecivedMoney.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labRecivedMoney.Location = new System.Drawing.Point(506, 34);
            this.labRecivedMoney.Name = "labRecivedMoney";
            this.labRecivedMoney.Size = new System.Drawing.Size(0, 17);
            this.labRecivedMoney.TabIndex = 1;
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDone.FlatAppearance.BorderSize = 0;
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDone.Location = new System.Drawing.Point(225, 20);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 31);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "تم";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panFooter, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 608);
            this.tableLayoutPanel1.TabIndex = 129;
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panHeader.Controls.Add(this.label10);
            this.panHeader.Controls.Add(this.label5);
            this.panHeader.Controls.Add(this.label6);
            this.panHeader.Controls.Add(this.comBranch);
            this.panHeader.Controls.Add(this.txtBranchID);
            this.panHeader.Controls.Add(this.btnNewChooes);
            this.panHeader.Controls.Add(this.btnSearch);
            this.panHeader.Controls.Add(this.txtClientID);
            this.panHeader.Controls.Add(this.txtCustomerID);
            this.panHeader.Controls.Add(this.radDealer);
            this.panHeader.Controls.Add(this.comEngCon);
            this.panHeader.Controls.Add(this.comClient);
            this.panHeader.Controls.Add(this.labelClient);
            this.panHeader.Controls.Add(this.radCon);
            this.panHeader.Controls.Add(this.radEng);
            this.panHeader.Controls.Add(this.radClient);
            this.panHeader.Controls.Add(this.labelEng);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(834, 129);
            this.panHeader.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.IndianRed;
            this.label10.Location = new System.Drawing.Point(159, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 19);
            this.label10.TabIndex = 213;
            this.label10.Text = "*";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(416, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 18);
            this.label5.TabIndex = 211;
            this.label5.Text = "الفرع";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(416, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 212;
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
            this.comBranch.Location = new System.Drawing.Point(238, 54);
            this.comBranch.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(175, 24);
            this.comBranch.TabIndex = 210;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBranchID.BackColor = System.Drawing.Color.White;
            this.txtBranchID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(183, 54);
            this.txtBranchID.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(49, 24);
            this.txtBranchID.TabIndex = 209;
            this.txtBranchID.TabStop = false;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchID_KeyDown);
            // 
            // btnNewChooes
            // 
            this.btnNewChooes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNewChooes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnNewChooes.FlatAppearance.BorderSize = 0;
            this.btnNewChooes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewChooes.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnNewChooes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNewChooes.Location = new System.Drawing.Point(27, 34);
            this.btnNewChooes.Name = "btnNewChooes";
            this.btnNewChooes.Size = new System.Drawing.Size(103, 36);
            this.btnNewChooes.TabIndex = 184;
            this.btnNewChooes.Text = "اختيار اخر";
            this.btnNewChooes.UseVisualStyleBackColor = false;
            this.btnNewChooes.Click += new System.EventHandler(this.btnNewChooes_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(27, 77);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 36);
            this.btnSearch.TabIndex = 183;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtClientID
            // 
            this.txtClientID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtClientID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClientID.Location = new System.Drawing.Point(463, 85);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(48, 24);
            this.txtClientID.TabIndex = 182;
            this.txtClientID.Visible = false;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCustomerID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCustomerID.Location = new System.Drawing.Point(463, 54);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(48, 24);
            this.txtCustomerID.TabIndex = 181;
            this.txtCustomerID.Visible = false;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radDealer
            // 
            this.radDealer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radDealer.AutoSize = true;
            this.radDealer.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDealer.Location = new System.Drawing.Point(497, 28);
            this.radDealer.Name = "radDealer";
            this.radDealer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radDealer.Size = new System.Drawing.Size(46, 20);
            this.radDealer.TabIndex = 180;
            this.radDealer.TabStop = true;
            this.radDealer.Text = "تاجر";
            this.radDealer.UseVisualStyleBackColor = true;
            this.radDealer.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // comEngCon
            // 
            this.comEngCon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comEngCon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comEngCon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comEngCon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comEngCon.FormattingEnabled = true;
            this.comEngCon.Location = new System.Drawing.Point(518, 54);
            this.comEngCon.Name = "comEngCon";
            this.comEngCon.Size = new System.Drawing.Size(173, 24);
            this.comEngCon.TabIndex = 178;
            this.comEngCon.Visible = false;
            this.comEngCon.SelectedValueChanged += new System.EventHandler(this.comEngCon_SelectedValueChanged);
            // 
            // comClient
            // 
            this.comClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(518, 85);
            this.comClient.Name = "comClient";
            this.comClient.Size = new System.Drawing.Size(173, 24);
            this.comClient.TabIndex = 173;
            this.comClient.Visible = false;
            this.comClient.SelectedValueChanged += new System.EventHandler(this.comClientName_SelectedValueChanged);
            // 
            // labelClient
            // 
            this.labelClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.Location = new System.Drawing.Point(697, 89);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(35, 16);
            this.labelClient.TabIndex = 174;
            this.labelClient.Text = "عميل";
            this.labelClient.Visible = false;
            // 
            // radCon
            // 
            this.radCon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radCon.AutoSize = true;
            this.radCon.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCon.Location = new System.Drawing.Point(552, 28);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(59, 20);
            this.radCon.TabIndex = 175;
            this.radCon.TabStop = true;
            this.radCon.Text = "مقاول";
            this.radCon.UseVisualStyleBackColor = true;
            this.radCon.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radEng
            // 
            this.radEng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radEng.AutoSize = true;
            this.radEng.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEng.Location = new System.Drawing.Point(617, 28);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(65, 20);
            this.radEng.TabIndex = 176;
            this.radEng.TabStop = true;
            this.radEng.Text = "مهندس";
            this.radEng.UseVisualStyleBackColor = true;
            this.radEng.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radClient
            // 
            this.radClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radClient.AutoSize = true;
            this.radClient.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radClient.Location = new System.Drawing.Point(694, 28);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(53, 20);
            this.radClient.TabIndex = 177;
            this.radClient.TabStop = true;
            this.radClient.Text = "عميل";
            this.radClient.UseVisualStyleBackColor = true;
            this.radClient.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // labelEng
            // 
            this.labelEng.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEng.AutoSize = true;
            this.labelEng.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEng.Location = new System.Drawing.Point(695, 58);
            this.labelEng.Name = "labelEng";
            this.labelEng.Size = new System.Drawing.Size(110, 16);
            this.labelEng.TabIndex = 179;
            this.labelEng.Text = "مهندس/مقاول/تاجر";
            this.labelEng.Visible = false;
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panFooter.Controls.Add(this.label1);
            this.panFooter.Controls.Add(this.btnDone);
            this.panFooter.Controls.Add(this.labRecivedMoney);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFooter.Location = new System.Drawing.Point(0, 527);
            this.panFooter.Margin = new System.Windows.Forms.Padding(0);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(834, 81);
            this.panFooter.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(587, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "الاجمالي";
            // 
            // checkPaidBillsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(834, 608);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "checkPaidBillsForm";
            this.Text = "checkPaidBillsForm";
            this.Load += new System.EventHandler(this.checkPaidBillsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labRecivedMoney;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.RadioButton radDealer;
        private System.Windows.Forms.ComboBox comEngCon;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.RadioButton radCon;
        private System.Windows.Forms.RadioButton radEng;
        private System.Windows.Forms.RadioButton radClient;
        private System.Windows.Forms.Label labelEng;
        private System.Windows.Forms.Button btnNewChooes;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
    }
}