﻿namespace TaxesSystem
{
    partial class Driver_Update
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Driver_Update));
            this.panContent = new System.Windows.Forms.Panel();
            this.radCompanyCar = new System.Windows.Forms.RadioButton();
            this.radPrivateCar = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.dTPWorkStartDate = new System.Windows.Forms.DateTimePicker();
            this.dTPBirthDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNationalID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLicese = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panContent.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panContent
            // 
            this.panContent.BackColor = System.Drawing.Color.White;
            this.panContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panContent.Controls.Add(this.radCompanyCar);
            this.panContent.Controls.Add(this.radPrivateCar);
            this.panContent.Controls.Add(this.tableLayoutPanel2);
            this.panContent.Controls.Add(this.dTPWorkStartDate);
            this.panContent.Controls.Add(this.dTPBirthDate);
            this.panContent.Controls.Add(this.label6);
            this.panContent.Controls.Add(this.txtNationalID);
            this.panContent.Controls.Add(this.label1);
            this.panContent.Controls.Add(this.label4);
            this.panContent.Controls.Add(this.txtLicese);
            this.panContent.Controls.Add(this.label5);
            this.panContent.Controls.Add(this.label8);
            this.panContent.Controls.Add(this.txtAddress);
            this.panContent.Controls.Add(this.label3);
            this.panContent.Controls.Add(this.txtDriverName);
            this.panContent.Controls.Add(this.txtPhone);
            this.panContent.Controls.Add(this.label2);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 0);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(830, 417);
            this.panContent.TabIndex = 0;
            // 
            // radCompanyCar
            // 
            this.radCompanyCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radCompanyCar.AutoSize = true;
            this.radCompanyCar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCompanyCar.Location = new System.Drawing.Point(445, 52);
            this.radCompanyCar.Name = "radCompanyCar";
            this.radCompanyCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCompanyCar.Size = new System.Drawing.Size(92, 23);
            this.radCompanyCar.TabIndex = 54;
            this.radCompanyCar.TabStop = true;
            this.radCompanyCar.Text = "سائق الشركة";
            this.radCompanyCar.UseVisualStyleBackColor = true;
            // 
            // radPrivateCar
            // 
            this.radPrivateCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radPrivateCar.AutoSize = true;
            this.radPrivateCar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPrivateCar.Location = new System.Drawing.Point(310, 52);
            this.radPrivateCar.Name = "radPrivateCar";
            this.radPrivateCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radPrivateCar.Size = new System.Drawing.Size(59, 23);
            this.radPrivateCar.TabIndex = 53;
            this.radPrivateCar.TabStop = true;
            this.radPrivateCar.Text = "خاصة";
            this.radPrivateCar.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 367);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(828, 48);
            this.tableLayoutPanel2.TabIndex = 52;
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
            this.btnAdd.Image = global::TaxesSystem.Properties.Resources.Save_32;
            this.btnAdd.ImagePosition = 1;
            this.btnAdd.ImageZoom = 20;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Location = new System.Drawing.Point(366, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(97, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Click += new System.EventHandler(this.btnUpdateDriver_Click);
            // 
            // dTPWorkStartDate
            // 
            this.dTPWorkStartDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dTPWorkStartDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dTPWorkStartDate.Location = new System.Drawing.Point(163, 195);
            this.dTPWorkStartDate.Name = "dTPWorkStartDate";
            this.dTPWorkStartDate.Size = new System.Drawing.Size(176, 24);
            this.dTPWorkStartDate.TabIndex = 6;
            this.dTPWorkStartDate.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            this.dTPWorkStartDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // dTPBirthDate
            // 
            this.dTPBirthDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dTPBirthDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dTPBirthDate.Location = new System.Drawing.Point(163, 156);
            this.dTPBirthDate.Name = "dTPBirthDate";
            this.dTPBirthDate.Size = new System.Drawing.Size(176, 24);
            this.dTPBirthDate.TabIndex = 5;
            this.dTPBirthDate.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            this.dTPBirthDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(652, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 51;
            this.label6.Text = "رقم البطاقة";
            // 
            // txtNationalID
            // 
            this.txtNationalID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNationalID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNationalID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNationalID.Location = new System.Drawing.Point(475, 157);
            this.txtNationalID.Name = "txtNationalID";
            this.txtNationalID.Size = new System.Drawing.Size(176, 24);
            this.txtNationalID.TabIndex = 1;
            this.txtNationalID.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtNationalID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label1.Location = new System.Drawing.Point(340, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 50;
            this.label1.Text = "تاريخ استلام العمل";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label4.Location = new System.Drawing.Point(340, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 49;
            this.label4.Text = "بيان الرخصة";
            // 
            // txtLicese
            // 
            this.txtLicese.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtLicese.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicese.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtLicese.Location = new System.Drawing.Point(163, 117);
            this.txtLicese.Name = "txtLicese";
            this.txtLicese.Size = new System.Drawing.Size(176, 24);
            this.txtLicese.TabIndex = 4;
            this.txtLicese.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtLicese.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label5.Location = new System.Drawing.Point(342, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 48;
            this.label5.Text = "تاريخ الميلاد";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label8.Location = new System.Drawing.Point(652, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 17);
            this.label8.TabIndex = 47;
            this.label8.Text = "العنوان";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAddress.Location = new System.Drawing.Point(475, 233);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(176, 24);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label3.Location = new System.Drawing.Point(652, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 44;
            this.label3.Text = "التلفون";
            // 
            // txtDriverName
            // 
            this.txtDriverName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDriverName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDriverName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDriverName.Location = new System.Drawing.Point(475, 119);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(176, 24);
            this.txtDriverName.TabIndex = 0;
            this.txtDriverName.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtDriverName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPhone.Location = new System.Drawing.Point(475, 195);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(176, 24);
            this.txtPhone.TabIndex = 2;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label2.Location = new System.Drawing.Point(654, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 41;
            this.label2.Text = "أسم السائق";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = null;
            this.bunifuDragControl1.Vertical = true;
            // 
            // Driver_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 417);
            this.Controls.Add(this.panContent);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(68)))), ((int)(((byte)(154)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Driver_Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تعديل بيانات سائق";
            this.Load += new System.EventHandler(this.Driver_Update_Load);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panContent;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.DateTimePicker dTPWorkStartDate;
        private System.Windows.Forms.DateTimePicker dTPBirthDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNationalID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLicese;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private System.Windows.Forms.RadioButton radCompanyCar;
        private System.Windows.Forms.RadioButton radPrivateCar;
    }
}

