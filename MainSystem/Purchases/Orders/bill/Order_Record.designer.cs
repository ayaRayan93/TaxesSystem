namespace MainSystem
{
    partial class Order_Record
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.comType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.comGroup = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total_Meter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotalMeters = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comSupplier = new System.Windows.Forms.ComboBox();
            this.btnMainForm = new System.Windows.Forms.Button();
            this.comBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comStore = new System.Windows.Forms.ComboBox();
            this.txtStoreID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtFactory);
            this.groupBox1.Controls.Add(this.comType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtProduct);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comProduct);
            this.groupBox1.Controls.Add(this.comFactory);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtGroup);
            this.groupBox1.Controls.Add(this.comGroup);
            this.groupBox1.Location = new System.Drawing.Point(42, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(817, 98);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "الفلاتر";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSearch.Location = new System.Drawing.Point(50, 36);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFactory
            // 
            this.txtFactory.Location = new System.Drawing.Point(504, 52);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Size = new System.Drawing.Size(55, 20);
            this.txtFactory.TabIndex = 4;
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comType
            // 
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(565, 25);
            this.comType.Name = "comType";
            this.comType.Size = new System.Drawing.Size(120, 21);
            this.comType.TabIndex = 0;
            this.comType.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(423, 58);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "الصنف";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(504, 25);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(55, 20);
            this.txtType.TabIndex = 1;
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(225, 52);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(55, 20);
            this.txtProduct.TabIndex = 10;
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(702, 31);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "نوع";
            // 
            // comProduct
            // 
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(286, 52);
            this.comProduct.Name = "comProduct";
            this.comProduct.Size = new System.Drawing.Size(120, 21);
            this.comProduct.TabIndex = 9;
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comFactory
            // 
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Location = new System.Drawing.Point(565, 52);
            this.comFactory.Name = "comFactory";
            this.comFactory.Size = new System.Drawing.Size(120, 21);
            this.comFactory.TabIndex = 3;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label14.Location = new System.Drawing.Point(423, 31);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(65, 17);
            this.label14.TabIndex = 8;
            this.label14.Text = "المجموعة";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label15.Location = new System.Drawing.Point(702, 58);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(48, 17);
            this.label15.TabIndex = 5;
            this.label15.Text = "المصنع";
            // 
            // txtGroup
            // 
            this.txtGroup.Location = new System.Drawing.Point(225, 25);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(55, 20);
            this.txtGroup.TabIndex = 7;
            this.txtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comGroup
            // 
            this.comGroup.FormattingEnabled = true;
            this.comGroup.Location = new System.Drawing.Point(286, 25);
            this.comGroup.Name = "comGroup";
            this.comGroup.Size = new System.Drawing.Size(120, 21);
            this.comGroup.TabIndex = 6;
            this.comGroup.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 233);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1010, 181);
            this.dataGridView1.TabIndex = 76;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Type,
            this.Factory,
            this.Group,
            this.Product,
            this.Colour,
            this.Size,
            this.Sort,
            this.Classification,
            this.Description,
            this.Total_Meter});
            this.dataGridView2.Location = new System.Drawing.Point(4, 471);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView2.Size = new System.Drawing.Size(1010, 152);
            this.dataGridView2.TabIndex = 109;
            // 
            // Code
            // 
            this.Code.HeaderText = "الكود";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "النوع";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Factory
            // 
            this.Factory.HeaderText = "المصنع";
            this.Factory.Name = "Factory";
            this.Factory.ReadOnly = true;
            // 
            // Group
            // 
            this.Group.HeaderText = "المجموعة";
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            // 
            // Product
            // 
            this.Product.HeaderText = "المنتج";
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            // 
            // Colour
            // 
            this.Colour.HeaderText = "اللون";
            this.Colour.Name = "Colour";
            this.Colour.ReadOnly = true;
            // 
            // Size
            // 
            this.Size.HeaderText = "المقاس";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // Sort
            // 
            this.Sort.HeaderText = "الفرز";
            this.Sort.Name = "Sort";
            this.Sort.ReadOnly = true;
            // 
            // Classification
            // 
            this.Classification.HeaderText = "التصنيف";
            this.Classification.Name = "Classification";
            this.Classification.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "الوصف";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Total_Meter
            // 
            this.Total_Meter.HeaderText = "اجمالي عدد الامتار";
            this.Total_Meter.Name = "Total_Meter";
            this.Total_Meter.ReadOnly = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.Location = new System.Drawing.Point(349, 437);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 17);
            this.label10.TabIndex = 113;
            this.label10.Text = "اجمالي عدد الامتار";
            // 
            // txtTotalMeters
            // 
            this.txtTotalMeters.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTotalMeters.Location = new System.Drawing.Point(201, 431);
            this.txtTotalMeters.Name = "txtTotalMeters";
            this.txtTotalMeters.Size = new System.Drawing.Size(134, 24);
            this.txtTotalMeters.TabIndex = 112;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(719, 440);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 111;
            this.label5.Text = "الكود";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCode.Location = new System.Drawing.Point(562, 434);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(143, 24);
            this.txtCode.TabIndex = 110;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 426);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 28);
            this.btnAdd.TabIndex = 114;
            this.btnAdd.Text = "اضافة";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(10, 636);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(96, 29);
            this.btnConfirm.TabIndex = 130;
            this.btnConfirm.Text = "تاكيد الشراء";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(614, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 132;
            this.label6.Text = "موظف";
            // 
            // txtEmployee
            // 
            this.txtEmployee.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmployee.Location = new System.Drawing.Point(475, 69);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(121, 24);
            this.txtEmployee.TabIndex = 133;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(687, 33);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(141, 24);
            this.dateTimePicker1.TabIndex = 134;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(687, 63);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(141, 24);
            this.dateTimePicker2.TabIndex = 135;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(855, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 17);
            this.label7.TabIndex = 136;
            this.label7.Text = "تاريخ الاستلام";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(855, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 137;
            this.label8.Text = "تاريخ الطلب";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSupplier.Location = new System.Drawing.Point(80, 73);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(48, 24);
            this.txtSupplier.TabIndex = 145;
            this.txtSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(273, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 17);
            this.label13.TabIndex = 144;
            this.label13.Text = "المورد";
            // 
            // comSupplier
            // 
            this.comSupplier.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comSupplier.FormattingEnabled = true;
            this.comSupplier.Location = new System.Drawing.Point(134, 72);
            this.comSupplier.Name = "comSupplier";
            this.comSupplier.Size = new System.Drawing.Size(121, 24);
            this.comSupplier.TabIndex = 143;
            this.comSupplier.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // btnMainForm
            // 
            this.btnMainForm.Location = new System.Drawing.Point(870, 634);
            this.btnMainForm.Name = "btnMainForm";
            this.btnMainForm.Size = new System.Drawing.Size(160, 29);
            this.btnMainForm.TabIndex = 146;
            this.btnMainForm.Text = "رجوع للصفحة الرئيسية";
            this.btnMainForm.UseVisualStyleBackColor = true;
            this.btnMainForm.Click += new System.EventHandler(this.btnMainForm_Click);
            // 
            // comBranch
            // 
            this.comBranch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comBranch.FormattingEnabled = true;
            this.comBranch.Location = new System.Drawing.Point(133, 36);
            this.comBranch.Name = "comBranch";
            this.comBranch.Size = new System.Drawing.Size(140, 24);
            this.comBranch.TabIndex = 197;
            this.comBranch.SelectedValueChanged += new System.EventHandler(this.comBranch_SelectedValueChanged);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBranchID.Location = new System.Drawing.Point(79, 37);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Size = new System.Drawing.Size(48, 24);
            this.txtBranchID.TabIndex = 199;
            this.txtBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label16.Location = new System.Drawing.Point(291, 39);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(36, 17);
            this.label16.TabIndex = 198;
            this.label16.Text = "الفرع";
            // 
            // comStore
            // 
            this.comStore.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comStore.FormattingEnabled = true;
            this.comStore.Location = new System.Drawing.Point(450, 30);
            this.comStore.Name = "comStore";
            this.comStore.Size = new System.Drawing.Size(140, 24);
            this.comStore.TabIndex = 203;
            this.comStore.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // txtStoreID
            // 
            this.txtStoreID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStoreID.Location = new System.Drawing.Point(396, 31);
            this.txtStoreID.Name = "txtStoreID";
            this.txtStoreID.Size = new System.Drawing.Size(48, 24);
            this.txtStoreID.TabIndex = 205;
            this.txtStoreID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label17.Location = new System.Drawing.Point(608, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 17);
            this.label17.TabIndex = 204;
            this.label17.Text = "المخزن";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 675);
            this.Controls.Add(this.comStore);
            this.Controls.Add(this.txtStoreID);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.comBranch);
            this.Controls.Add(this.txtBranchID);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnMainForm);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comSupplier);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTotalMeters);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "طلب بنود";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.ComboBox comGroup;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTotalMeters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comSupplier;
        private System.Windows.Forms.Button btnMainForm;
        private System.Windows.Forms.ComboBox comBranch;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comStore;
        private System.Windows.Forms.TextBox txtStoreID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colour;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total_Meter;
    }
}

