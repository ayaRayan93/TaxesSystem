namespace MainSystem
{
    partial class UpdateSupplierTaswaya
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
            this.panContent = new System.Windows.Forms.Panel();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTaswaya = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonDiscount = new System.Windows.Forms.RadioButton();
            this.radioButtonAdd = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.txtSupplierID = new System.Windows.Forms.TextBox();
            this.comSupplier = new System.Windows.Forms.ComboBox();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSupplierAccount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panContent.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panContent, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(817, 571);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.label8);
            this.panContent.Controls.Add(this.label7);
            this.panContent.Controls.Add(this.label6);
            this.panContent.Controls.Add(this.label5);
            this.panContent.Controls.Add(this.txtSupplierAccount);
            this.panContent.Controls.Add(this.dateTimeFrom);
            this.panContent.Controls.Add(this.label4);
            this.panContent.Controls.Add(this.btnTaswaya);
            this.panContent.Controls.Add(this.panel1);
            this.panContent.Controls.Add(this.label3);
            this.panContent.Controls.Add(this.label2);
            this.panContent.Controls.Add(this.txtMoney);
            this.panContent.Controls.Add(this.label1);
            this.panContent.Controls.Add(this.txtInfo);
            this.panContent.Controls.Add(this.txtSupplierID);
            this.panContent.Controls.Add(this.comSupplier);
            this.panContent.Controls.Add(this.labelSupplier);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(3, 3);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(811, 565);
            this.panContent.TabIndex = 0;
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimeFrom.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeFrom.CustomFormat = "yyyy/MM/dd";
            this.dateTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeFrom.Location = new System.Drawing.Point(434, 240);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dateTimeFrom.RightToLeftLayout = true;
            this.dateTimeFrom.Size = new System.Drawing.Size(182, 27);
            this.dateTimeFrom.TabIndex = 192;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(641, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 19);
            this.label4.TabIndex = 193;
            this.label4.Text = "التاريخ";
            // 
            // btnTaswaya
            // 
            this.btnTaswaya.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTaswaya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnTaswaya.FlatAppearance.BorderSize = 0;
            this.btnTaswaya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaswaya.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaswaya.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTaswaya.Location = new System.Drawing.Point(133, 366);
            this.btnTaswaya.Name = "btnTaswaya";
            this.btnTaswaya.Size = new System.Drawing.Size(111, 40);
            this.btnTaswaya.TabIndex = 191;
            this.btnTaswaya.Text = "تسوية";
            this.btnTaswaya.UseVisualStyleBackColor = false;
            this.btnTaswaya.Click += new System.EventHandler(this.btnTaswaya_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.radioButtonDiscount);
            this.panel1.Controls.Add(this.radioButtonAdd);
            this.panel1.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(434, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 45);
            this.panel1.TabIndex = 190;
            // 
            // radioButtonDiscount
            // 
            this.radioButtonDiscount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonDiscount.AutoSize = true;
            this.radioButtonDiscount.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDiscount.Location = new System.Drawing.Point(32, 13);
            this.radioButtonDiscount.Name = "radioButtonDiscount";
            this.radioButtonDiscount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonDiscount.Size = new System.Drawing.Size(52, 20);
            this.radioButtonDiscount.TabIndex = 187;
            this.radioButtonDiscount.TabStop = true;
            this.radioButtonDiscount.Text = "خصم";
            this.radioButtonDiscount.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdd
            // 
            this.radioButtonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonAdd.AutoSize = true;
            this.radioButtonAdd.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAdd.Location = new System.Drawing.Point(109, 13);
            this.radioButtonAdd.Name = "radioButtonAdd";
            this.radioButtonAdd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonAdd.Size = new System.Drawing.Size(56, 20);
            this.radioButtonAdd.TabIndex = 188;
            this.radioButtonAdd.TabStop = true;
            this.radioButtonAdd.Text = "اضافة";
            this.radioButtonAdd.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(622, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 19);
            this.label3.TabIndex = 189;
            this.label3.Text = "نوع التسوية";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(640, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 186;
            this.label2.Text = "المبلغ";
            // 
            // txtMoney
            // 
            this.txtMoney.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMoney.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoney.Location = new System.Drawing.Point(434, 192);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMoney.Size = new System.Drawing.Size(182, 27);
            this.txtMoney.TabIndex = 185;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(646, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 19);
            this.label1.TabIndex = 184;
            this.label1.Text = "بيان";
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInfo.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.Location = new System.Drawing.Point(434, 284);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtInfo.Size = new System.Drawing.Size(182, 47);
            this.txtInfo.TabIndex = 183;
            // 
            // txtSupplierID
            // 
            this.txtSupplierID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSupplierID.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierID.Location = new System.Drawing.Point(361, 82);
            this.txtSupplierID.Name = "txtSupplierID";
            this.txtSupplierID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSupplierID.Size = new System.Drawing.Size(63, 27);
            this.txtSupplierID.TabIndex = 182;
            this.txtSupplierID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comSupplier
            // 
            this.comSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSupplier.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comSupplier.FormattingEnabled = true;
            this.comSupplier.Location = new System.Drawing.Point(434, 82);
            this.comSupplier.Name = "comSupplier";
            this.comSupplier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSupplier.Size = new System.Drawing.Size(182, 27);
            this.comSupplier.TabIndex = 173;
            this.comSupplier.SelectedValueChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // labelSupplier
            // 
            this.labelSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplier.Location = new System.Drawing.Point(639, 86);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(48, 19);
            this.labelSupplier.TabIndex = 174;
            this.labelSupplier.Text = "المورد";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(175, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 19);
            this.label5.TabIndex = 197;
            this.label5.Text = "الحساب الحالى";
            // 
            // txtSupplierAccount
            // 
            this.txtSupplierAccount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSupplierAccount.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierAccount.Location = new System.Drawing.Point(19, 82);
            this.txtSupplierAccount.Name = "txtSupplierAccount";
            this.txtSupplierAccount.ReadOnly = true;
            this.txtSupplierAccount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSupplierAccount.Size = new System.Drawing.Size(150, 27);
            this.txtSupplierAccount.TabIndex = 196;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(337, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 19);
            this.label6.TabIndex = 198;
            this.label6.Text = "*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(410, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 19);
            this.label7.TabIndex = 199;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(410, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 19);
            this.label8.TabIndex = 200;
            this.label8.Text = "*";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpdateSupplierTaswaya
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(817, 571);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UpdateSupplierTaswaya";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonDiscount;
        private System.Windows.Forms.RadioButton radioButtonAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnTaswaya;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSupplierID;
        private System.Windows.Forms.ComboBox comSupplier;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSupplierAccount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}