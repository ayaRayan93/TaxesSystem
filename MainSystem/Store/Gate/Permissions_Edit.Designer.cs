namespace MainSystem
{
    partial class Permissions_Edit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteNum = new System.Windows.Forms.Button();
            this.checkedListBoxControlNum = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnAddNum = new System.Windows.Forms.Button();
            this.txtPermisionNum = new System.Windows.Forms.TextBox();
            this.labelPerNum = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.imageListBoxControl1 = new DevExpress.XtraEditors.ImageListBoxControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlNum)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageListBoxControl1);
            this.panel1.Controls.Add(this.btnDeleteNum);
            this.panel1.Controls.Add(this.checkedListBoxControlNum);
            this.panel1.Controls.Add(this.btnAddNum);
            this.panel1.Controls.Add(this.txtPermisionNum);
            this.panel1.Controls.Add(this.labelPerNum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 288);
            this.panel1.TabIndex = 1;
            // 
            // btnDeleteNum
            // 
            this.btnDeleteNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDeleteNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDeleteNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteNum.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnDeleteNum.ForeColor = System.Drawing.Color.White;
            this.btnDeleteNum.Location = new System.Drawing.Point(92, 184);
            this.btnDeleteNum.Name = "btnDeleteNum";
            this.btnDeleteNum.Size = new System.Drawing.Size(58, 26);
            this.btnDeleteNum.TabIndex = 59;
            this.btnDeleteNum.Text = "حذف";
            this.btnDeleteNum.UseVisualStyleBackColor = false;
            this.btnDeleteNum.Click += new System.EventHandler(this.btnDeleteNum_Click);
            // 
            // checkedListBoxControlNum
            // 
            this.checkedListBoxControlNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkedListBoxControlNum.Appearance.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxControlNum.Appearance.Options.UseFont = true;
            this.checkedListBoxControlNum.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxControlNum.Location = new System.Drawing.Point(71, 82);
            this.checkedListBoxControlNum.Name = "checkedListBoxControlNum";
            this.checkedListBoxControlNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkedListBoxControlNum.Size = new System.Drawing.Size(100, 96);
            this.checkedListBoxControlNum.TabIndex = 57;
            // 
            // btnAddNum
            // 
            this.btnAddNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNum.ForeColor = System.Drawing.Color.White;
            this.btnAddNum.Location = new System.Drawing.Point(92, 50);
            this.btnAddNum.Name = "btnAddNum";
            this.btnAddNum.Size = new System.Drawing.Size(58, 26);
            this.btnAddNum.TabIndex = 58;
            this.btnAddNum.Text = "اضافة";
            this.btnAddNum.UseVisualStyleBackColor = false;
            this.btnAddNum.Click += new System.EventHandler(this.btnAddNum_Click);
            // 
            // txtPermisionNum
            // 
            this.txtPermisionNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPermisionNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPermisionNum.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtPermisionNum.Location = new System.Drawing.Point(21, 15);
            this.txtPermisionNum.Name = "txtPermisionNum";
            this.txtPermisionNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPermisionNum.Size = new System.Drawing.Size(200, 28);
            this.txtPermisionNum.TabIndex = 2;
            // 
            // labelPerNum
            // 
            this.labelPerNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPerNum.AutoSize = true;
            this.labelPerNum.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPerNum.ForeColor = System.Drawing.Color.Firebrick;
            this.labelPerNum.Location = new System.Drawing.Point(226, 18);
            this.labelPerNum.Name = "labelPerNum";
            this.labelPerNum.Size = new System.Drawing.Size(80, 23);
            this.labelPerNum.TabIndex = 3;
            this.labelPerNum.Text = "رقم الاذن";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 235);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(327, 53);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::MainSystem.Properties.Resources.Save_321;
            this.btnAdd.ImagePosition = 1;
            this.btnAdd.ImageZoom = 35;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Location = new System.Drawing.Point(135, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(59, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imageListBoxControl1
            // 
            this.imageListBoxControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imageListBoxControl1.Location = new System.Drawing.Point(177, 82);
            this.imageListBoxControl1.Name = "imageListBoxControl1";
            this.imageListBoxControl1.Size = new System.Drawing.Size(86, 96);
            this.imageListBoxControl1.TabIndex = 75;
            this.imageListBoxControl1.Visible = false;
            // 
            // Permissions_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(327, 288);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Permissions_Edit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Permissions_Edit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlNum)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteNum;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlNum;
        private System.Windows.Forms.Button btnAddNum;
        private System.Windows.Forms.TextBox txtPermisionNum;
        private System.Windows.Forms.Label labelPerNum;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private DevExpress.XtraEditors.ImageListBoxControl imageListBoxControl1;
    }
}