namespace TaxesSystem
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
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.labelBranch = new System.Windows.Forms.Label();
            this.treeViewSupIdPerm = new System.Windows.Forms.TreeView();
            this.treeViewSupPerm = new System.Windows.Forms.TreeView();
            this.imageListBoxControl1 = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnAddNum = new System.Windows.Forms.Button();
            this.txtPermisionNum = new System.Windows.Forms.TextBox();
            this.labelPerNum = new System.Windows.Forms.Label();
            this.comSupplier = new System.Windows.Forms.ComboBox();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.comStore = new System.Windows.Forms.ComboBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.labelStore = new System.Windows.Forms.Label();
            this.treeViewGatSupplierId = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeViewGatSupplierId);
            this.panel1.Controls.Add(this.comBranch);
            this.panel1.Controls.Add(this.labelBranch);
            this.panel1.Controls.Add(this.comStore);
            this.panel1.Controls.Add(this.labelStore);
            this.panel1.Controls.Add(this.comClient);
            this.panel1.Controls.Add(this.labelClient);
            this.panel1.Controls.Add(this.labelSupplier);
            this.panel1.Controls.Add(this.comSupplier);
            this.panel1.Controls.Add(this.treeViewSupIdPerm);
            this.panel1.Controls.Add(this.treeViewSupPerm);
            this.panel1.Controls.Add(this.imageListBoxControl1);
            this.panel1.Controls.Add(this.btnAddNum);
            this.panel1.Controls.Add(this.txtPermisionNum);
            this.panel1.Controls.Add(this.labelPerNum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 264);
            this.panel1.TabIndex = 1;
            // 
            // comBranch
            // 
            this.comBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(21, 47);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(200, 29);
            this.comBranch.TabIndex = 88;
            this.comBranch.Visible = false;
            // 
            // labelBranch
            // 
            this.labelBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBranch.AutoSize = true;
            this.labelBranch.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBranch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelBranch.Location = new System.Drawing.Point(226, 50);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(48, 23);
            this.labelBranch.TabIndex = 89;
            this.labelBranch.Text = "الفرع";
            this.labelBranch.Visible = false;
            // 
            // treeViewSupIdPerm
            // 
            this.treeViewSupIdPerm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.treeViewSupIdPerm.CheckBoxes = true;
            this.treeViewSupIdPerm.Location = new System.Drawing.Point(269, 158);
            this.treeViewSupIdPerm.Name = "treeViewSupIdPerm";
            this.treeViewSupIdPerm.Size = new System.Drawing.Size(49, 95);
            this.treeViewSupIdPerm.TabIndex = 81;
            this.treeViewSupIdPerm.Visible = false;
            // 
            // treeViewSupPerm
            // 
            this.treeViewSupPerm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.treeViewSupPerm.CheckBoxes = true;
            this.treeViewSupPerm.Location = new System.Drawing.Point(28, 158);
            this.treeViewSupPerm.Name = "treeViewSupPerm";
            this.treeViewSupPerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.treeViewSupPerm.RightToLeftLayout = true;
            this.treeViewSupPerm.Size = new System.Drawing.Size(143, 95);
            this.treeViewSupPerm.TabIndex = 80;
            this.treeViewSupPerm.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSupPerm_AfterCheck);
            // 
            // imageListBoxControl1
            // 
            this.imageListBoxControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imageListBoxControl1.Location = new System.Drawing.Point(177, 158);
            this.imageListBoxControl1.Name = "imageListBoxControl1";
            this.imageListBoxControl1.Size = new System.Drawing.Size(31, 96);
            this.imageListBoxControl1.TabIndex = 75;
            this.imageListBoxControl1.Visible = false;
            // 
            // btnAddNum
            // 
            this.btnAddNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNum.ForeColor = System.Drawing.Color.White;
            this.btnAddNum.Location = new System.Drawing.Point(92, 126);
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
            this.txtPermisionNum.Location = new System.Drawing.Point(21, 91);
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
            this.labelPerNum.Location = new System.Drawing.Point(226, 94);
            this.labelPerNum.Name = "labelPerNum";
            this.labelPerNum.Size = new System.Drawing.Size(80, 23);
            this.labelPerNum.TabIndex = 3;
            this.labelPerNum.Text = "رقم الاذن";
            // 
            // comSupplier
            // 
            this.comSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSupplier.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comSupplier.FormattingEnabled = true;
            this.comSupplier.Location = new System.Drawing.Point(21, 50);
            this.comSupplier.Name = "comSupplier";
            this.comSupplier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSupplier.Size = new System.Drawing.Size(200, 29);
            this.comSupplier.TabIndex = 85;
            this.comSupplier.Visible = false;
            // 
            // labelSupplier
            // 
            this.labelSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelSupplier.Location = new System.Drawing.Point(225, 53);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(55, 23);
            this.labelSupplier.TabIndex = 82;
            this.labelSupplier.Text = "المورد";
            this.labelSupplier.Visible = false;
            // 
            // comStore
            // 
            this.comStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStore.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comStore.FormattingEnabled = true;
            this.comStore.Location = new System.Drawing.Point(21, 44);
            this.comStore.Name = "comStore";
            this.comStore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comStore.Size = new System.Drawing.Size(200, 29);
            this.comStore.TabIndex = 84;
            this.comStore.Visible = false;
            // 
            // labelClient
            // 
            this.labelClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelClient.Location = new System.Drawing.Point(226, 12);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(59, 23);
            this.labelClient.TabIndex = 86;
            this.labelClient.Text = "العميل";
            this.labelClient.Visible = false;
            // 
            // comClient
            // 
            this.comClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comClient.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(21, 12);
            this.comClient.Name = "comClient";
            this.comClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comClient.Size = new System.Drawing.Size(200, 29);
            this.comClient.TabIndex = 87;
            this.comClient.Visible = false;
            // 
            // labelStore
            // 
            this.labelStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStore.AutoSize = true;
            this.labelStore.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelStore.Location = new System.Drawing.Point(226, 50);
            this.labelStore.Name = "labelStore";
            this.labelStore.Size = new System.Drawing.Size(78, 23);
            this.labelStore.TabIndex = 83;
            this.labelStore.Text = "من مخزن";
            this.labelStore.Visible = false;
            // 
            // treeViewGatSupplierId
            // 
            this.treeViewGatSupplierId.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.treeViewGatSupplierId.CheckBoxes = true;
            this.treeViewGatSupplierId.Location = new System.Drawing.Point(214, 157);
            this.treeViewGatSupplierId.Name = "treeViewGatSupplierId";
            this.treeViewGatSupplierId.Size = new System.Drawing.Size(49, 95);
            this.treeViewGatSupplierId.TabIndex = 90;
            this.treeViewGatSupplierId.Visible = false;
            // 
            // Permissions_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(327, 264);
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
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddNum;
        private System.Windows.Forms.TextBox txtPermisionNum;
        private System.Windows.Forms.Label labelPerNum;
        private DevExpress.XtraEditors.ImageListBoxControl imageListBoxControl1;
        private System.Windows.Forms.TreeView treeViewSupIdPerm;
        private System.Windows.Forms.TreeView treeViewSupPerm;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.Label labelBranch;
        private System.Windows.Forms.ComboBox comStore;
        private System.Windows.Forms.Label labelStore;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.ComboBox comSupplier;
        private System.Windows.Forms.TreeView treeViewGatSupplierId;
    }
}