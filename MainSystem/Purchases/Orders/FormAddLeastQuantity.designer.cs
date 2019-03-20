namespace MainSystem
{
    partial class FormAddLeastQuantity
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbproduct = new System.Windows.Forms.ComboBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.cmbtype = new System.Windows.Forms.ComboBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtLeastQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(362, 87);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 23);
            this.btnSearch.TabIndex = 35;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(864, 299);
            this.dataGridView1.TabIndex = 34;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cmbproduct
            // 
            this.cmbproduct.FormattingEnabled = true;
            this.cmbproduct.Location = new System.Drawing.Point(226, 59);
            this.cmbproduct.Name = "cmbproduct";
            this.cmbproduct.Size = new System.Drawing.Size(121, 21);
            this.cmbproduct.TabIndex = 33;
            this.cmbproduct.SelectedIndexChanged += new System.EventHandler(this.cmbproduct_SelectedIndexChanged);
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(170, 59);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(50, 20);
            this.txtProduct.TabIndex = 32;
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduct_KeyDown);
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(226, 18);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbGroup.TabIndex = 31;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // txtGroup
            // 
            this.txtGroup.Location = new System.Drawing.Point(170, 18);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(50, 20);
            this.txtGroup.TabIndex = 30;
            this.txtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroup_KeyDown);
            // 
            // cmbFactory
            // 
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(537, 59);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(121, 21);
            this.cmbFactory.TabIndex = 29;
            this.cmbFactory.SelectedIndexChanged += new System.EventHandler(this.cmbFactory_SelectedIndexChanged);
            // 
            // txtFactory
            // 
            this.txtFactory.Location = new System.Drawing.Point(481, 59);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Size = new System.Drawing.Size(50, 20);
            this.txtFactory.TabIndex = 28;
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFactory_KeyDown);
            // 
            // cmbtype
            // 
            this.cmbtype.FormattingEnabled = true;
            this.cmbtype.Location = new System.Drawing.Point(537, 18);
            this.cmbtype.Name = "cmbtype";
            this.cmbtype.Size = new System.Drawing.Size(121, 21);
            this.cmbtype.TabIndex = 27;
            this.cmbtype.SelectedIndexChanged += new System.EventHandler(this.cmbtype_SelectedIndexChanged);
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(481, 18);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(50, 20);
            this.txtType.TabIndex = 26;
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(360, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "الصنف";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(664, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "المورد";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "المجموعة";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(666, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "النوع";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(772, 437);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "الكود";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(537, 434);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(229, 20);
            this.txtCode.TabIndex = 37;
            // 
            // txtLeastQuantity
            // 
            this.txtLeastQuantity.Location = new System.Drawing.Point(170, 434);
            this.txtLeastQuantity.Name = "txtLeastQuantity";
            this.txtLeastQuantity.Size = new System.Drawing.Size(173, 20);
            this.txtLeastQuantity.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(349, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "الحد الادنى";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 432);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(121, 23);
            this.btnAdd.TabIndex = 40;
            this.btnAdd.Text = "اضافة";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 465);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtLeastQuantity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmbproduct);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.txtGroup);
            this.Controls.Add(this.cmbFactory);
            this.Controls.Add(this.txtFactory);
            this.Controls.Add(this.cmbtype);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "اضافة الحد الادنى";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbproduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.ComboBox cmbFactory;
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.ComboBox cmbtype;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtLeastQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAdd;
    }
}

