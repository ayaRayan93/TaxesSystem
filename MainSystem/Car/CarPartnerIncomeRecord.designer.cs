﻿namespace TaxesSystem
{
    partial class CarPartnerIncomeRecord
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.panContent = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoComp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNoSets = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNoColumns = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNoDocks = new System.Windows.Forms.TextBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNoPanio = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNoCarton = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddPermission = new System.Windows.Forms.Button();
            this.txtPermissionNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comCarNumber = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.panContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 592);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(817, 48);
            this.tableLayoutPanel2.TabIndex = 51;
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
            this.btnAdd.Location = new System.Drawing.Point(361, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(96, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panContent
            // 
            this.panContent.AutoScroll = true;
            this.panContent.AutoScrollMargin = new System.Drawing.Size(1, 1);
            this.panContent.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.panContent.AutoSize = true;
            this.panContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panContent.Controls.Add(this.btnDelete);
            this.panContent.Controls.Add(this.dataGridView1);
            this.panContent.Controls.Add(this.groupBox1);
            this.panContent.Controls.Add(this.txtNote);
            this.panContent.Controls.Add(this.label10);
            this.panContent.Controls.Add(this.txtNoPanio);
            this.panContent.Controls.Add(this.label7);
            this.panContent.Controls.Add(this.txtNoCarton);
            this.panContent.Controls.Add(this.label4);
            this.panContent.Controls.Add(this.txtAddress);
            this.panContent.Controls.Add(this.label3);
            this.panContent.Controls.Add(this.btnAddPermission);
            this.panContent.Controls.Add(this.txtPermissionNumber);
            this.panContent.Controls.Add(this.label2);
            this.panContent.Controls.Add(this.label1);
            this.panContent.Controls.Add(this.dateTimePicker1);
            this.panContent.Controls.Add(this.comCarNumber);
            this.panContent.Controls.Add(this.label5);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 0);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(817, 592);
            this.panContent.TabIndex = 52;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(561, 148);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(58, 23);
            this.btnDelete.TabIndex = 68;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(281, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(193, 117);
            this.dataGridView1.TabIndex = 67;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "رقم الاذن";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.txtNoComp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtNoSets);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNoColumns);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtNoDocks);
            this.groupBox1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(132, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(645, 100);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "صحي";
            // 
            // txtNoComp
            // 
            this.txtNoComp.Location = new System.Drawing.Point(24, 65);
            this.txtNoComp.Name = "txtNoComp";
            this.txtNoComp.Size = new System.Drawing.Size(147, 23);
            this.txtNoComp.TabIndex = 35;
            this.txtNoComp.Text = "0";
            this.txtNoComp.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNoComp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(526, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 18);
            this.label6.TabIndex = 30;
            this.label6.Text = "عدد الاطقم";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label13.Location = new System.Drawing.Point(178, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 18);
            this.label13.TabIndex = 44;
            this.label13.Text = "عدد الكومبنيشن";
            // 
            // txtNoSets
            // 
            this.txtNoSets.Location = new System.Drawing.Point(368, 30);
            this.txtNoSets.Name = "txtNoSets";
            this.txtNoSets.Size = new System.Drawing.Size(147, 23);
            this.txtNoSets.TabIndex = 31;
            this.txtNoSets.Text = "0";
            this.txtNoSets.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNoSets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label9.Location = new System.Drawing.Point(182, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 18);
            this.label9.TabIndex = 32;
            this.label9.Text = "عدد العواميد";
            // 
            // txtNoColumns
            // 
            this.txtNoColumns.Location = new System.Drawing.Point(24, 33);
            this.txtNoColumns.Name = "txtNoColumns";
            this.txtNoColumns.Size = new System.Drawing.Size(147, 23);
            this.txtNoColumns.TabIndex = 33;
            this.txtNoColumns.Text = "0";
            this.txtNoColumns.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNoColumns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(193, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 16);
            this.label8.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label12.Location = new System.Drawing.Point(526, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 18);
            this.label12.TabIndex = 38;
            this.label12.Text = "عدد الاحواض";
            // 
            // txtNoDocks
            // 
            this.txtNoDocks.Location = new System.Drawing.Point(368, 65);
            this.txtNoDocks.Name = "txtNoDocks";
            this.txtNoDocks.Size = new System.Drawing.Size(147, 23);
            this.txtNoDocks.TabIndex = 39;
            this.txtNoDocks.Text = "0";
            this.txtNoDocks.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNoDocks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtNote
            // 
            this.txtNote.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNote.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNote.Location = new System.Drawing.Point(445, 484);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(254, 56);
            this.txtNote.TabIndex = 65;
            this.txtNote.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label10.Location = new System.Drawing.Point(721, 484);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 18);
            this.label10.TabIndex = 64;
            this.label10.Text = "ملاحظات";
            // 
            // txtNoPanio
            // 
            this.txtNoPanio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNoPanio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNoPanio.Location = new System.Drawing.Point(152, 294);
            this.txtNoPanio.Name = "txtNoPanio";
            this.txtNoPanio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNoPanio.Size = new System.Drawing.Size(147, 24);
            this.txtNoPanio.TabIndex = 61;
            this.txtNoPanio.Text = "0";
            this.txtNoPanio.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNoPanio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label7.Location = new System.Drawing.Point(321, 294);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 18);
            this.label7.TabIndex = 60;
            this.label7.Text = "عدد البانيوهات";
            // 
            // txtNoCarton
            // 
            this.txtNoCarton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNoCarton.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNoCarton.Location = new System.Drawing.Point(494, 296);
            this.txtNoCarton.Name = "txtNoCarton";
            this.txtNoCarton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNoCarton.Size = new System.Drawing.Size(147, 24);
            this.txtNoCarton.TabIndex = 59;
            this.txtNoCarton.Text = "0";
            this.txtNoCarton.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtNoCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label4.Location = new System.Drawing.Point(663, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 58;
            this.label4.Text = "عدد الكراتين";
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAddress.Location = new System.Drawing.Point(333, 219);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(366, 24);
            this.txtAddress.TabIndex = 57;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label3.Location = new System.Drawing.Point(705, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 56;
            this.label3.Text = "اسم العميل";
            // 
            // btnAddPermission
            // 
            this.btnAddPermission.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddPermission.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btnAddPermission.FlatAppearance.BorderSize = 0;
            this.btnAddPermission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPermission.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPermission.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddPermission.Location = new System.Drawing.Point(625, 148);
            this.btnAddPermission.Name = "btnAddPermission";
            this.btnAddPermission.Size = new System.Drawing.Size(58, 23);
            this.btnAddPermission.TabIndex = 55;
            this.btnAddPermission.Text = "اضافه";
            this.btnAddPermission.UseVisualStyleBackColor = false;
            this.btnAddPermission.Click += new System.EventHandler(this.btnAddPermission_Click);
            // 
            // txtPermissionNumber
            // 
            this.txtPermissionNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPermissionNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPermissionNumber.Location = new System.Drawing.Point(552, 108);
            this.txtPermissionNumber.Name = "txtPermissionNumber";
            this.txtPermissionNumber.Size = new System.Drawing.Size(147, 24);
            this.txtPermissionNumber.TabIndex = 54;
            this.txtPermissionNumber.TextChanged += new System.EventHandler(this.txtPermissionNumber_TextChanged);
            this.txtPermissionNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPermissionNumber_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label2.Location = new System.Drawing.Point(705, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 53;
            this.label2.Text = "رقم الاذن";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label1.Location = new System.Drawing.Point(230, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 18);
            this.label1.TabIndex = 52;
            this.label1.Text = "التاريخ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(37, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(180, 24);
            this.dateTimePicker1.TabIndex = 51;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // comCarNumber
            // 
            this.comCarNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comCarNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comCarNumber.FormattingEnabled = true;
            this.comCarNumber.Location = new System.Drawing.Point(552, 56);
            this.comCarNumber.Name = "comCarNumber";
            this.comCarNumber.Size = new System.Drawing.Size(147, 24);
            this.comCarNumber.TabIndex = 50;
            this.comCarNumber.SelectedValueChanged += new System.EventHandler(this.comCarNumber_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label5.Location = new System.Drawing.Point(705, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 18);
            this.label5.TabIndex = 49;
            this.label5.Text = "رقم السيارة";
            // 
            // CarPartnerIncomeRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(817, 640);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "CarPartnerIncomeRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الايرادات";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNoComp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNoSets;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNoColumns;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNoDocks;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNoPanio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNoCarton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddPermission;
        private System.Windows.Forms.TextBox txtPermissionNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comCarNumber;
        private System.Windows.Forms.Label label5;
    }
}

