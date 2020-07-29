﻿namespace TaxesSystem
{
    partial class UserRecord
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
            this.label1 = new System.Windows.Forms.Label();
            this.comEmployee = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rDelegate = new System.Windows.Forms.RadioButton();
            this.rEmployee = new System.Windows.Forms.RadioButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.comDepartment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.label1.Location = new System.Drawing.Point(516, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 28;
            this.label1.Text = "الموظف";
            // 
            // comEmployee
            // 
            this.comEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comEmployee.FormattingEnabled = true;
            this.comEmployee.Location = new System.Drawing.Point(327, 136);
            this.comEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comEmployee.Name = "comEmployee";
            this.comEmployee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comEmployee.Size = new System.Drawing.Size(185, 24);
            this.comEmployee.TabIndex = 2;
            this.comEmployee.SelectedValueChanged += new System.EventHandler(this.comEmployee_SelectedValueChanged);
            this.comEmployee.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtName.Location = new System.Drawing.Point(489, 244);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtName.Size = new System.Drawing.Size(166, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.label2.Location = new System.Drawing.Point(335, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = "الرقم السرى";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPassword.Location = new System.Drawing.Point(162, 244);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPassword.Size = new System.Drawing.Size(166, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(97, 191);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1, 1);
            this.dataGridView1.TabIndex = 45;
            // 
            // rDelegate
            // 
            this.rDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rDelegate.AutoSize = true;
            this.rDelegate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rDelegate.ForeColor = System.Drawing.Color.DarkBlue;
            this.rDelegate.Location = new System.Drawing.Point(362, 78);
            this.rDelegate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rDelegate.Name = "rDelegate";
            this.rDelegate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rDelegate.Size = new System.Drawing.Size(67, 20);
            this.rDelegate.TabIndex = 62;
            this.rDelegate.Text = "مندوب";
            this.rDelegate.UseVisualStyleBackColor = true;
            this.rDelegate.CheckedChanged += new System.EventHandler(this.rDelegate_CheckedChanged);
            // 
            // rEmployee
            // 
            this.rEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rEmployee.AutoSize = true;
            this.rEmployee.Checked = true;
            this.rEmployee.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rEmployee.ForeColor = System.Drawing.Color.DarkBlue;
            this.rEmployee.Location = new System.Drawing.Point(457, 78);
            this.rEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rEmployee.Name = "rEmployee";
            this.rEmployee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rEmployee.Size = new System.Drawing.Size(67, 20);
            this.rEmployee.TabIndex = 61;
            this.rEmployee.TabStop = true;
            this.rEmployee.Text = "موظف";
            this.rEmployee.UseVisualStyleBackColor = true;
            this.rEmployee.CheckedChanged += new System.EventHandler(this.rEmployee_CheckedChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(38, 73);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1, 1);
            this.dataGridView2.TabIndex = 63;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.label9.Location = new System.Drawing.Point(660, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 18);
            this.label9.TabIndex = 64;
            this.label9.Text = "الاسم";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 486);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(870, 53);
            this.tableLayoutPanel2.TabIndex = 70;
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
            this.btnAdd.ImageZoom = 25;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Location = new System.Drawing.Point(395, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 45);
            this.btnAdd.TabIndex = 71;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // comDepartment
            // 
            this.comDepartment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comDepartment.Enabled = false;
            this.comDepartment.FormattingEnabled = true;
            this.comDepartment.Location = new System.Drawing.Point(324, 183);
            this.comDepartment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comDepartment.Name = "comDepartment";
            this.comDepartment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comDepartment.Size = new System.Drawing.Size(185, 24);
            this.comDepartment.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.label3.Location = new System.Drawing.Point(513, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 72;
            this.label3.Text = "القسم";
            // 
            // UserRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(870, 539);
            this.Controls.Add(this.comDepartment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.rDelegate);
            this.Controls.Add(this.rEmployee);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.comEmployee);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UserRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comEmployee;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rDelegate;
        private System.Windows.Forms.RadioButton rEmployee;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private System.Windows.Forms.ComboBox comDepartment;
        private System.Windows.Forms.Label label3;
    }
}