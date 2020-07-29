﻿namespace TaxesSystem
{
    partial class Gate_Enter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gate_Enter));
            this.txtPermisionNum = new System.Windows.Forms.TextBox();
            this.labelPerNum = new System.Windows.Forms.Label();
            this.txtDriver = new System.Windows.Forms.TextBox();
            this.labelEmp = new System.Windows.Forms.Label();
            this.panContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.labelBranch = new System.Windows.Forms.Label();
            this.treeViewSupIdPerm = new System.Windows.Forms.TreeView();
            this.comStore = new System.Windows.Forms.ComboBox();
            this.labelStore = new System.Windows.Forms.Label();
            this.treeViewSupPerm = new System.Windows.Forms.TreeView();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.labelSupplier = new System.Windows.Forms.Label();
            this.imageListBoxControl1 = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnAddNum = new System.Windows.Forms.Button();
            this.btnDeleteNum = new System.Windows.Forms.Button();
            this.comSupplier = new System.Windows.Forms.ComboBox();
            this.labelType2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCar = new System.Windows.Forms.Label();
            this.comCar = new System.Windows.Forms.ComboBox();
            this.txtCar = new System.Windows.Forms.TextBox();
            this.labelResponsible = new System.Windows.Forms.Label();
            this.comResponsible = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comType = new System.Windows.Forms.ComboBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.labelDriver = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comEmployee = new System.Windows.Forms.ComboBox();
            this.labelLicense = new System.Windows.Forms.Label();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.comDriver = new System.Windows.Forms.ComboBox();
            this.comReason = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.panContent.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPermisionNum
            // 
            this.txtPermisionNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPermisionNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPermisionNum.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtPermisionNum.Location = new System.Drawing.Point(83, 75);
            this.txtPermisionNum.Name = "txtPermisionNum";
            this.txtPermisionNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPermisionNum.Size = new System.Drawing.Size(200, 28);
            this.txtPermisionNum.TabIndex = 2;
            this.txtPermisionNum.Visible = false;
            // 
            // labelPerNum
            // 
            this.labelPerNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPerNum.AutoSize = true;
            this.labelPerNum.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPerNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelPerNum.Location = new System.Drawing.Point(288, 78);
            this.labelPerNum.Name = "labelPerNum";
            this.labelPerNum.Size = new System.Drawing.Size(80, 23);
            this.labelPerNum.TabIndex = 3;
            this.labelPerNum.Text = "رقم الاذن";
            this.labelPerNum.Visible = false;
            // 
            // txtDriver
            // 
            this.txtDriver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDriver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDriver.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtDriver.Location = new System.Drawing.Point(455, 171);
            this.txtDriver.Name = "txtDriver";
            this.txtDriver.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDriver.Size = new System.Drawing.Size(200, 28);
            this.txtDriver.TabIndex = 1;
            this.txtDriver.Visible = false;
            this.txtDriver.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // labelEmp
            // 
            this.labelEmp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEmp.AutoSize = true;
            this.labelEmp.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelEmp.Location = new System.Drawing.Point(325, 208);
            this.labelEmp.Name = "labelEmp";
            this.labelEmp.Size = new System.Drawing.Size(118, 23);
            this.labelEmp.TabIndex = 5;
            this.labelEmp.Text = "مسئول التعتيق";
            this.labelEmp.Visible = false;
            // 
            // panContent
            // 
            this.panContent.BackColor = System.Drawing.Color.White;
            this.panContent.Controls.Add(this.panel1);
            this.panContent.Controls.Add(this.labelType2);
            this.panContent.Controls.Add(this.label2);
            this.panContent.Controls.Add(this.labelCar);
            this.panContent.Controls.Add(this.comCar);
            this.panContent.Controls.Add(this.txtCar);
            this.panContent.Controls.Add(this.labelResponsible);
            this.panContent.Controls.Add(this.comResponsible);
            this.panContent.Controls.Add(this.labelType);
            this.panContent.Controls.Add(this.comType);
            this.panContent.Controls.Add(this.labelDescription);
            this.panContent.Controls.Add(this.txtDescription);
            this.panContent.Controls.Add(this.labelDriver);
            this.panContent.Controls.Add(this.label1);
            this.panContent.Controls.Add(this.comEmployee);
            this.panContent.Controls.Add(this.labelLicense);
            this.panContent.Controls.Add(this.txtLicense);
            this.panContent.Controls.Add(this.comDriver);
            this.panContent.Controls.Add(this.comReason);
            this.panContent.Controls.Add(this.tableLayoutPanel2);
            this.panContent.Controls.Add(this.labelEmp);
            this.panContent.Controls.Add(this.txtDriver);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 0);
            this.panContent.Margin = new System.Windows.Forms.Padding(0);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(781, 562);
            this.panContent.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.comBranch);
            this.panel1.Controls.Add(this.labelBranch);
            this.panel1.Controls.Add(this.treeViewSupIdPerm);
            this.panel1.Controls.Add(this.comStore);
            this.panel1.Controls.Add(this.labelStore);
            this.panel1.Controls.Add(this.treeViewSupPerm);
            this.panel1.Controls.Add(this.comClient);
            this.panel1.Controls.Add(this.labelClient);
            this.panel1.Controls.Add(this.labelSupplier);
            this.panel1.Controls.Add(this.txtPermisionNum);
            this.panel1.Controls.Add(this.imageListBoxControl1);
            this.panel1.Controls.Add(this.labelPerNum);
            this.panel1.Controls.Add(this.btnAddNum);
            this.panel1.Controls.Add(this.btnDeleteNum);
            this.panel1.Controls.Add(this.comSupplier);
            this.panel1.Location = new System.Drawing.Point(37, 242);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 209);
            this.panel1.TabIndex = 78;
            // 
            // comBranch
            // 
            this.comBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(83, 40);
            this.comBranch.Name = "comBranch";
            this.comBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comBranch.Size = new System.Drawing.Size(200, 29);
            this.comBranch.TabIndex = 79;
            this.comBranch.Visible = false;
            // 
            // labelBranch
            // 
            this.labelBranch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBranch.AutoSize = true;
            this.labelBranch.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBranch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelBranch.Location = new System.Drawing.Point(288, 43);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(48, 23);
            this.labelBranch.TabIndex = 80;
            this.labelBranch.Text = "الفرع";
            this.labelBranch.Visible = false;
            // 
            // treeViewSupIdPerm
            // 
            this.treeViewSupIdPerm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.treeViewSupIdPerm.CheckBoxes = true;
            this.treeViewSupIdPerm.Location = new System.Drawing.Point(289, 109);
            this.treeViewSupIdPerm.Name = "treeViewSupIdPerm";
            this.treeViewSupIdPerm.Size = new System.Drawing.Size(49, 95);
            this.treeViewSupIdPerm.TabIndex = 79;
            this.treeViewSupIdPerm.Visible = false;
            // 
            // comStore
            // 
            this.comStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStore.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comStore.FormattingEnabled = true;
            this.comStore.Location = new System.Drawing.Point(83, 37);
            this.comStore.Name = "comStore";
            this.comStore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comStore.Size = new System.Drawing.Size(200, 29);
            this.comStore.TabIndex = 73;
            this.comStore.Visible = false;
            this.comStore.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // labelStore
            // 
            this.labelStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStore.AutoSize = true;
            this.labelStore.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelStore.Location = new System.Drawing.Point(288, 43);
            this.labelStore.Name = "labelStore";
            this.labelStore.Size = new System.Drawing.Size(60, 23);
            this.labelStore.TabIndex = 72;
            this.labelStore.Text = "المخزن";
            this.labelStore.Visible = false;
            // 
            // treeViewSupPerm
            // 
            this.treeViewSupPerm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.treeViewSupPerm.CheckBoxes = true;
            this.treeViewSupPerm.Location = new System.Drawing.Point(83, 109);
            this.treeViewSupPerm.Name = "treeViewSupPerm";
            this.treeViewSupPerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.treeViewSupPerm.RightToLeftLayout = true;
            this.treeViewSupPerm.Size = new System.Drawing.Size(143, 95);
            this.treeViewSupPerm.TabIndex = 78;
            this.treeViewSupPerm.Visible = false;
            this.treeViewSupPerm.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSupPerm_AfterCheck);
            // 
            // comClient
            // 
            this.comClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comClient.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(83, 5);
            this.comClient.Name = "comClient";
            this.comClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comClient.Size = new System.Drawing.Size(200, 29);
            this.comClient.TabIndex = 77;
            this.comClient.Visible = false;
            // 
            // labelClient
            // 
            this.labelClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelClient.Location = new System.Drawing.Point(288, 5);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(59, 23);
            this.labelClient.TabIndex = 76;
            this.labelClient.Text = "العميل";
            this.labelClient.Visible = false;
            // 
            // labelSupplier
            // 
            this.labelSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSupplier.AutoSize = true;
            this.labelSupplier.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelSupplier.Location = new System.Drawing.Point(287, 46);
            this.labelSupplier.Name = "labelSupplier";
            this.labelSupplier.Size = new System.Drawing.Size(55, 23);
            this.labelSupplier.TabIndex = 70;
            this.labelSupplier.Text = "المورد";
            this.labelSupplier.Visible = false;
            // 
            // imageListBoxControl1
            // 
            this.imageListBoxControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imageListBoxControl1.Location = new System.Drawing.Point(232, 109);
            this.imageListBoxControl1.Name = "imageListBoxControl1";
            this.imageListBoxControl1.Size = new System.Drawing.Size(51, 95);
            this.imageListBoxControl1.TabIndex = 74;
            this.imageListBoxControl1.Visible = false;
            // 
            // btnAddNum
            // 
            this.btnAddNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAddNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNum.ForeColor = System.Drawing.Color.White;
            this.btnAddNum.Location = new System.Drawing.Point(19, 79);
            this.btnAddNum.Name = "btnAddNum";
            this.btnAddNum.Size = new System.Drawing.Size(58, 26);
            this.btnAddNum.TabIndex = 58;
            this.btnAddNum.Text = "اضافة";
            this.btnAddNum.UseVisualStyleBackColor = false;
            this.btnAddNum.Visible = false;
            this.btnAddNum.Click += new System.EventHandler(this.btnAddNum_Click);
            // 
            // btnDeleteNum
            // 
            this.btnDeleteNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDeleteNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnDeleteNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteNum.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnDeleteNum.ForeColor = System.Drawing.Color.White;
            this.btnDeleteNum.Location = new System.Drawing.Point(18, 178);
            this.btnDeleteNum.Name = "btnDeleteNum";
            this.btnDeleteNum.Size = new System.Drawing.Size(58, 26);
            this.btnDeleteNum.TabIndex = 59;
            this.btnDeleteNum.Text = "حذف";
            this.btnDeleteNum.UseVisualStyleBackColor = false;
            this.btnDeleteNum.Visible = false;
            this.btnDeleteNum.Click += new System.EventHandler(this.btnDeleteNum_Click);
            // 
            // comSupplier
            // 
            this.comSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSupplier.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comSupplier.FormattingEnabled = true;
            this.comSupplier.Location = new System.Drawing.Point(83, 43);
            this.comSupplier.Name = "comSupplier";
            this.comSupplier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comSupplier.Size = new System.Drawing.Size(200, 29);
            this.comSupplier.TabIndex = 75;
            this.comSupplier.Visible = false;
            // 
            // labelType2
            // 
            this.labelType2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelType2.AutoSize = true;
            this.labelType2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelType2.ForeColor = System.Drawing.Color.Red;
            this.labelType2.Location = new System.Drawing.Point(241, 72);
            this.labelType2.Name = "labelType2";
            this.labelType2.Size = new System.Drawing.Size(18, 19);
            this.labelType2.TabIndex = 68;
            this.labelType2.Text = "*";
            this.labelType2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelType2.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(241, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 19);
            this.label2.TabIndex = 67;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCar
            // 
            this.labelCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelCar.AutoSize = true;
            this.labelCar.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelCar.Location = new System.Drawing.Point(325, 171);
            this.labelCar.Name = "labelCar";
            this.labelCar.Size = new System.Drawing.Size(90, 23);
            this.labelCar.TabIndex = 66;
            this.labelCar.Text = "رقم العربية";
            this.labelCar.Visible = false;
            // 
            // comCar
            // 
            this.comCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCar.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comCar.FormattingEnabled = true;
            this.comCar.Location = new System.Drawing.Point(120, 167);
            this.comCar.Name = "comCar";
            this.comCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comCar.Size = new System.Drawing.Size(200, 29);
            this.comCar.TabIndex = 65;
            this.comCar.Visible = false;
            this.comCar.SelectedValueChanged += new System.EventHandler(this.comCar_SelectedValueChanged);
            this.comCar.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtCar
            // 
            this.txtCar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCar.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtCar.Location = new System.Drawing.Point(120, 169);
            this.txtCar.Name = "txtCar";
            this.txtCar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCar.Size = new System.Drawing.Size(200, 28);
            this.txtCar.TabIndex = 64;
            this.txtCar.Visible = false;
            this.txtCar.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // labelResponsible
            // 
            this.labelResponsible.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelResponsible.AutoSize = true;
            this.labelResponsible.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResponsible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelResponsible.Location = new System.Drawing.Point(470, 118);
            this.labelResponsible.Name = "labelResponsible";
            this.labelResponsible.Size = new System.Drawing.Size(73, 23);
            this.labelResponsible.TabIndex = 63;
            this.labelResponsible.Text = "المسئول";
            this.labelResponsible.Visible = false;
            // 
            // comResponsible
            // 
            this.comResponsible.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comResponsible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comResponsible.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comResponsible.FormattingEnabled = true;
            this.comResponsible.Location = new System.Drawing.Point(265, 115);
            this.comResponsible.Name = "comResponsible";
            this.comResponsible.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comResponsible.Size = new System.Drawing.Size(200, 29);
            this.comResponsible.TabIndex = 62;
            this.comResponsible.Visible = false;
            this.comResponsible.SelectedValueChanged += new System.EventHandler(this.comResponsible_SelectedValueChanged);
            this.comResponsible.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // labelType
            // 
            this.labelType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelType.Location = new System.Drawing.Point(470, 69);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(46, 23);
            this.labelType.TabIndex = 61;
            this.labelType.Text = "النوع";
            this.labelType.Visible = false;
            // 
            // comType
            // 
            this.comType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comType.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(265, 66);
            this.comType.Name = "comType";
            this.comType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comType.Size = new System.Drawing.Size(200, 29);
            this.comType.TabIndex = 60;
            this.comType.Visible = false;
            this.comType.SelectedValueChanged += new System.EventHandler(this.comType_SelectedValueChanged);
            this.comType.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelDescription.Location = new System.Drawing.Point(660, 242);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(48, 23);
            this.labelDescription.TabIndex = 21;
            this.labelDescription.Text = "البيان";
            this.labelDescription.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtDescription.Location = new System.Drawing.Point(455, 239);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescription.Size = new System.Drawing.Size(200, 80);
            this.txtDescription.TabIndex = 20;
            this.txtDescription.Visible = false;
            // 
            // labelDriver
            // 
            this.labelDriver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDriver.AutoSize = true;
            this.labelDriver.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDriver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelDriver.Location = new System.Drawing.Point(660, 173);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(64, 23);
            this.labelDriver.TabIndex = 19;
            this.labelDriver.Text = "السواق";
            this.labelDriver.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label1.Location = new System.Drawing.Point(470, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "سبب الدخول";
            // 
            // comEmployee
            // 
            this.comEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comEmployee.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comEmployee.FormattingEnabled = true;
            this.comEmployee.Location = new System.Drawing.Point(120, 205);
            this.comEmployee.Name = "comEmployee";
            this.comEmployee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comEmployee.Size = new System.Drawing.Size(200, 29);
            this.comEmployee.TabIndex = 17;
            this.comEmployee.Visible = false;
            this.comEmployee.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // labelLicense
            // 
            this.labelLicense.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelLicense.AutoSize = true;
            this.labelLicense.Font = new System.Drawing.Font("Neo Sans Arabic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.labelLicense.Location = new System.Drawing.Point(660, 208);
            this.labelLicense.Name = "labelLicense";
            this.labelLicense.Size = new System.Drawing.Size(92, 23);
            this.labelLicense.TabIndex = 16;
            this.labelLicense.Text = "رقم الرخصة";
            this.labelLicense.Visible = false;
            // 
            // txtLicense
            // 
            this.txtLicense.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicense.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtLicense.Location = new System.Drawing.Point(455, 205);
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLicense.Size = new System.Drawing.Size(200, 28);
            this.txtLicense.TabIndex = 15;
            this.txtLicense.Visible = false;
            this.txtLicense.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // comDriver
            // 
            this.comDriver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDriver.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comDriver.FormattingEnabled = true;
            this.comDriver.Location = new System.Drawing.Point(455, 169);
            this.comDriver.Name = "comDriver";
            this.comDriver.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comDriver.Size = new System.Drawing.Size(200, 29);
            this.comDriver.TabIndex = 14;
            this.comDriver.Visible = false;
            this.comDriver.SelectedValueChanged += new System.EventHandler(this.comDriver_SelectedValueChanged);
            this.comDriver.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // comReason
            // 
            this.comReason.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comReason.Font = new System.Drawing.Font("Tahoma", 13F);
            this.comReason.FormattingEnabled = true;
            this.comReason.Location = new System.Drawing.Point(265, 18);
            this.comReason.Name = "comReason";
            this.comReason.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comReason.Size = new System.Drawing.Size(200, 29);
            this.comReason.TabIndex = 13;
            this.comReason.SelectedValueChanged += new System.EventHandler(this.comReason_SelectedValueChanged);
            this.comReason.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 509);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(781, 53);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::TaxesSystem.Properties.Resources.Save_32;
            this.btnAdd.ImagePosition = 1;
            this.btnAdd.ImageZoom = 25;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Location = new System.Drawing.Point(349, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // Gate_Enter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 562);
            this.Controls.Add(this.panContent);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(68)))), ((int)(((byte)(154)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Gate_Enter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Gate_Enter_Load);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPermisionNum;
        private System.Windows.Forms.Label labelPerNum;
        private System.Windows.Forms.TextBox txtDriver;
        private System.Windows.Forms.Label labelEmp;
        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private System.Windows.Forms.ComboBox comDriver;
        private System.Windows.Forms.ComboBox comReason;
        private System.Windows.Forms.Label labelDriver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comEmployee;
        private System.Windows.Forms.Label labelLicense;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Button btnDeleteNum;
        private System.Windows.Forms.Button btnAddNum;
        private System.Windows.Forms.Label labelResponsible;
        private System.Windows.Forms.ComboBox comResponsible;
        private System.Windows.Forms.Label labelCar;
        private System.Windows.Forms.ComboBox comCar;
        private System.Windows.Forms.TextBox txtCar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelType2;
        private System.Windows.Forms.Label labelSupplier;
        private System.Windows.Forms.Label labelStore;
        private System.Windows.Forms.ComboBox comStore;
        private DevExpress.XtraEditors.ImageListBoxControl imageListBoxControl1;
        private System.Windows.Forms.ComboBox comSupplier;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeViewSupPerm;
        private System.Windows.Forms.TreeView treeViewSupIdPerm;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.Label labelBranch;
    }
}

