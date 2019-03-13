namespace MainSystem
{
    partial class Delegate_Movement
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Delegate_Movement));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDelegateID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelegate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colTimer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkTimer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttend = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeparture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSound = new Bunifu.Framework.UI.BunifuTileButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.radOldBill = new System.Windows.Forms.RadioButton();
            this.radNewBill = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labBill = new System.Windows.Forms.Label();
            this.txtBill = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.radDealer = new System.Windows.Forms.RadioButton();
            this.comClient = new System.Windows.Forms.ComboBox();
            this.comEngCon = new System.Windows.Forms.ComboBox();
            this.labelEng = new System.Windows.Forms.Label();
            this.radClient = new System.Windows.Forms.RadioButton();
            this.labelClient = new System.Windows.Forms.Label();
            this.radEng = new System.Windows.Forms.RadioButton();
            this.radCon = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtRecomendedBill = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 123);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemColorEdit1,
            this.repositoryItemButtonEdit1});
            this.gridControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gridControl1.Size = new System.Drawing.Size(1037, 414);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Neo Sans Arabic", 12F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDelegateID,
            this.colDelegate,
            this.colStatus,
            this.colButton,
            this.colTimer,
            this.colWorkTimer,
            this.colAttend,
            this.colDeparture});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFind.FindNullPrompt = "بحث";
            this.gridView1.OptionsFind.ShowClearButton = false;
            this.gridView1.OptionsFind.ShowFindButton = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.GridView1_CustomRowCellEdit);
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colDelegateID
            // 
            this.colDelegateID.Caption = "التسلسل";
            this.colDelegateID.FieldName = "DelegateId";
            this.colDelegateID.Name = "colDelegateID";
            this.colDelegateID.Width = 100;
            // 
            // colDelegate
            // 
            this.colDelegate.Caption = "المندوب";
            this.colDelegate.ColumnEdit = this.repositoryItemTextEdit1;
            this.colDelegate.FieldName = "DelegateName";
            this.colDelegate.Name = "colDelegate";
            this.colDelegate.OptionsColumn.AllowEdit = false;
            this.colDelegate.OptionsColumn.ReadOnly = true;
            this.colDelegate.Visible = true;
            this.colDelegate.VisibleIndex = 1;
            this.colDelegate.Width = 200;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colStatus
            // 
            this.colStatus.Caption = "الحالة";
            this.colStatus.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colStatus.FieldName = "StatusID";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 2;
            this.colStatus.Width = 200;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description")});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // colButton
            // 
            this.colButton.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colButton.Name = "colButton";
            this.colButton.OptionsColumn.AllowEdit = false;
            this.colButton.Visible = true;
            this.colButton.VisibleIndex = 3;
            this.colButton.Width = 25;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colTimer
            // 
            this.colTimer.Caption = "المدة";
            this.colTimer.FieldName = "StatusTimer";
            this.colTimer.Name = "colTimer";
            this.colTimer.OptionsColumn.AllowEdit = false;
            this.colTimer.Visible = true;
            this.colTimer.VisibleIndex = 4;
            this.colTimer.Width = 120;
            // 
            // colWorkTimer
            // 
            this.colWorkTimer.Caption = "ساعات العمل";
            this.colWorkTimer.FieldName = "WorkTimer";
            this.colWorkTimer.Name = "colWorkTimer";
            this.colWorkTimer.OptionsColumn.AllowEdit = false;
            this.colWorkTimer.Visible = true;
            this.colWorkTimer.VisibleIndex = 5;
            this.colWorkTimer.Width = 120;
            // 
            // colAttend
            // 
            this.colAttend.Caption = "الحضور";
            this.colAttend.FieldName = "AttendId";
            this.colAttend.Name = "colAttend";
            this.colAttend.OptionsColumn.AllowEdit = false;
            this.colAttend.OptionsColumn.ReadOnly = true;
            this.colAttend.Visible = true;
            this.colAttend.VisibleIndex = 6;
            this.colAttend.Width = 148;
            // 
            // colDeparture
            // 
            this.colDeparture.Caption = "الانصراف";
            this.colDeparture.FieldName = "DepartureId";
            this.colDeparture.Name = "colDeparture";
            this.colDeparture.OptionsColumn.AllowEdit = false;
            this.colDeparture.OptionsColumn.ReadOnly = true;
            this.colDeparture.Visible = true;
            this.colDeparture.VisibleIndex = 7;
            this.colDeparture.Width = 147;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 600);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1043, 600);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnSound, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.axWindowsMediaPlayer1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 543);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1037, 54);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // btnSound
            // 
            this.btnSound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSound.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSound.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSound.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.btnSound.ForeColor = System.Drawing.Color.White;
            this.btnSound.Image = ((System.Drawing.Image)(resources.GetObject("btnSound.Image")));
            this.btnSound.ImagePosition = 1;
            this.btnSound.ImageZoom = 20;
            this.btnSound.LabelPosition = 18;
            this.btnSound.LabelText = "استدعاء";
            this.btnSound.Location = new System.Drawing.Point(523, 4);
            this.btnSound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(97, 46);
            this.btnSound.TabIndex = 1;
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Controls.Add(this.radOldBill, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.radNewBill, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(316, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(201, 48);
            this.tableLayoutPanel3.TabIndex = 25;
            // 
            // radOldBill
            // 
            this.radOldBill.AutoSize = true;
            this.radOldBill.Checked = true;
            this.radOldBill.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radOldBill.ForeColor = System.Drawing.Color.White;
            this.radOldBill.Location = new System.Drawing.Point(115, 3);
            this.radOldBill.Name = "radOldBill";
            this.radOldBill.Size = new System.Drawing.Size(63, 20);
            this.radOldBill.TabIndex = 0;
            this.radOldBill.TabStop = true;
            this.radOldBill.Text = "قديمة";
            this.radOldBill.UseVisualStyleBackColor = true;
            this.radOldBill.Visible = false;
            this.radOldBill.Click += new System.EventHandler(this.radOldBill_Click);
            // 
            // radNewBill
            // 
            this.radNewBill.AutoSize = true;
            this.radNewBill.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNewBill.ForeColor = System.Drawing.Color.White;
            this.radNewBill.Location = new System.Drawing.Point(26, 3);
            this.radNewBill.Name = "radNewBill";
            this.radNewBill.Size = new System.Drawing.Size(62, 20);
            this.radNewBill.TabIndex = 1;
            this.radNewBill.Text = "جديدة";
            this.radNewBill.UseVisualStyleBackColor = true;
            this.radNewBill.Visible = false;
            this.radNewBill.Click += new System.EventHandler(this.radNewBill_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.Controls.Add(this.labBill, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtBill, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnStart, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(307, 48);
            this.tableLayoutPanel4.TabIndex = 26;
            // 
            // labBill
            // 
            this.labBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labBill.Font = new System.Drawing.Font("Neo Sans Arabic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBill.ForeColor = System.Drawing.Color.LightCoral;
            this.labBill.Location = new System.Drawing.Point(157, 0);
            this.labBill.Name = "labBill";
            this.labBill.Size = new System.Drawing.Size(147, 48);
            this.labBill.TabIndex = 0;
            this.labBill.Text = "رقم الفاتورة : ";
            this.labBill.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labBill.Visible = false;
            // 
            // txtBill
            // 
            this.txtBill.Enabled = false;
            this.txtBill.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBill.Location = new System.Drawing.Point(50, 3);
            this.txtBill.Name = "txtBill";
            this.txtBill.Size = new System.Drawing.Size(101, 23);
            this.txtBill.TabIndex = 1;
            this.txtBill.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.Font = new System.Drawing.Font("Neo Sans Arabic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(41, 25);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "بدء";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(626, 3);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(97, 48);
            this.axWindowsMediaPlayer1.TabIndex = 27;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnReload, 5, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1037, 114);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(575, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(356, 108);
            this.panel2.TabIndex = 151;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtClientID);
            this.panel3.Controls.Add(this.txtCustomerID);
            this.panel3.Controls.Add(this.radDealer);
            this.panel3.Controls.Add(this.comClient);
            this.panel3.Controls.Add(this.comEngCon);
            this.panel3.Controls.Add(this.labelEng);
            this.panel3.Controls.Add(this.radClient);
            this.panel3.Controls.Add(this.labelClient);
            this.panel3.Controls.Add(this.radEng);
            this.panel3.Controls.Add(this.radCon);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(353, 102);
            this.panel3.TabIndex = 0;
            // 
            // txtClientID
            // 
            this.txtClientID.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtClientID.Location = new System.Drawing.Point(14, 70);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(56, 23);
            this.txtClientID.TabIndex = 146;
            this.txtClientID.Visible = false;
            this.txtClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtCustomerID.Location = new System.Drawing.Point(14, 40);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(56, 23);
            this.txtCustomerID.TabIndex = 145;
            this.txtCustomerID.Visible = false;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // radDealer
            // 
            this.radDealer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDealer.AutoSize = true;
            this.radDealer.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radDealer.Location = new System.Drawing.Point(47, 9);
            this.radDealer.Name = "radDealer";
            this.radDealer.Size = new System.Drawing.Size(49, 21);
            this.radDealer.TabIndex = 144;
            this.radDealer.TabStop = true;
            this.radDealer.Text = "تاجر";
            this.radDealer.UseVisualStyleBackColor = true;
            this.radDealer.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // comClient
            // 
            this.comClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comClient.FormattingEnabled = true;
            this.comClient.Location = new System.Drawing.Point(76, 69);
            this.comClient.Name = "comClient";
            this.comClient.Size = new System.Drawing.Size(150, 24);
            this.comClient.TabIndex = 135;
            this.comClient.Visible = false;
            this.comClient.SelectedValueChanged += new System.EventHandler(this.comClient_SelectedValueChanged);
            // 
            // comEngCon
            // 
            this.comEngCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comEngCon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comEngCon.FormattingEnabled = true;
            this.comEngCon.Location = new System.Drawing.Point(76, 39);
            this.comEngCon.Name = "comEngCon";
            this.comEngCon.Size = new System.Drawing.Size(150, 24);
            this.comEngCon.TabIndex = 142;
            this.comEngCon.Visible = false;
            this.comEngCon.SelectedValueChanged += new System.EventHandler(this.comEngCon_SelectedValueChanged);
            // 
            // labelEng
            // 
            this.labelEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEng.AutoSize = true;
            this.labelEng.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelEng.Location = new System.Drawing.Point(230, 43);
            this.labelEng.Name = "labelEng";
            this.labelEng.Size = new System.Drawing.Size(119, 17);
            this.labelEng.TabIndex = 143;
            this.labelEng.Text = "مهندس/مقاول/تاجر";
            this.labelEng.Visible = false;
            // 
            // radClient
            // 
            this.radClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radClient.AutoSize = true;
            this.radClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radClient.Location = new System.Drawing.Point(244, 9);
            this.radClient.Name = "radClient";
            this.radClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radClient.Size = new System.Drawing.Size(58, 21);
            this.radClient.TabIndex = 139;
            this.radClient.TabStop = true;
            this.radClient.Text = "عميل";
            this.radClient.UseVisualStyleBackColor = true;
            this.radClient.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // labelClient
            // 
            this.labelClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelClient.Location = new System.Drawing.Point(269, 73);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(40, 17);
            this.labelClient.TabIndex = 136;
            this.labelClient.Text = "عميل";
            this.labelClient.Visible = false;
            // 
            // radEng
            // 
            this.radEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radEng.AutoSize = true;
            this.radEng.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radEng.Location = new System.Drawing.Point(167, 9);
            this.radEng.Name = "radEng";
            this.radEng.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radEng.Size = new System.Drawing.Size(71, 21);
            this.radEng.TabIndex = 138;
            this.radEng.TabStop = true;
            this.radEng.Text = "مهندس";
            this.radEng.UseVisualStyleBackColor = true;
            this.radEng.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // radCon
            // 
            this.radCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radCon.AutoSize = true;
            this.radCon.Font = new System.Drawing.Font("Tahoma", 10F);
            this.radCon.Location = new System.Drawing.Point(102, 9);
            this.radCon.Name = "radCon";
            this.radCon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radCon.Size = new System.Drawing.Size(59, 21);
            this.radCon.TabIndex = 137;
            this.radCon.TabStop = true;
            this.radCon.Text = "مقاول";
            this.radCon.UseVisualStyleBackColor = true;
            this.radCon.CheckedChanged += new System.EventHandler(this.radiotype_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(254, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(305, 108);
            this.panel4.TabIndex = 152;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtRecomendedBill);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(299, 102);
            this.panel5.TabIndex = 0;
            // 
            // txtRecomendedBill
            // 
            this.txtRecomendedBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecomendedBill.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecomendedBill.Location = new System.Drawing.Point(59, 9);
            this.txtRecomendedBill.Name = "txtRecomendedBill";
            this.txtRecomendedBill.ReadOnly = true;
            this.txtRecomendedBill.Size = new System.Drawing.Size(155, 23);
            this.txtRecomendedBill.TabIndex = 138;
            this.txtRecomendedBill.Click += new System.EventHandler(this.txtRecomendedBill_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(220, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 137;
            this.label1.Text = "فاتورة";
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnReload.BackgroundImage = global::MainSystem.Properties.Resources.update;
            this.btnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReload.Location = new System.Drawing.Point(116, 3);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(50, 35);
            this.btnReload.TabIndex = 7;
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // Delegate_Movement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 600);
            this.Controls.Add(this.panel1);
            this.Name = "Delegate_Movement";
            this.Text = "DelegateAttend";
            this.Load += new System.EventHandler(this.DelegateAttend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDelegateID;
        private DevExpress.XtraGrid.Columns.GridColumn colDelegate;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnSound;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton radOldBill;
        private System.Windows.Forms.RadioButton radNewBill;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label labBill;
        private System.Windows.Forms.TextBox txtBill;
        private System.Windows.Forms.Button btnStart;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAttend;
        private DevExpress.XtraGrid.Columns.GridColumn colDeparture;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraGrid.Columns.GridColumn colTimer;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.RadioButton radDealer;
        private System.Windows.Forms.ComboBox comEngCon;
        private System.Windows.Forms.ComboBox comClient;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.RadioButton radCon;
        private System.Windows.Forms.RadioButton radEng;
        private System.Windows.Forms.RadioButton radClient;
        private System.Windows.Forms.Label labelEng;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtRecomendedBill;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkTimer;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.TextBox txtCustomerID;
    }
}

