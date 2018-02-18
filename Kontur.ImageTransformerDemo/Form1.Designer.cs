namespace Kontur.ImageTransformerDemo
{
    partial class frmMain
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
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.prpRequest = new System.Windows.Forms.PropertyGrid();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblServerState = new System.Windows.Forms.Label();
            this.lblRequest = new System.Windows.Forms.Label();
            this.cmbRequests = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSelectPixel = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.lblRequestImg = new System.Windows.Forms.Label();
            this.lblResponseImg = new System.Windows.Forms.Label();
            this.lblRequestImgA = new System.Windows.Forms.Label();
            this.lblRequestImgR = new System.Windows.Forms.Label();
            this.lblRequestImgG = new System.Windows.Forms.Label();
            this.lblbRequestImgB = new System.Windows.Forms.Label();
            this.lblResponseImgA = new System.Windows.Forms.Label();
            this.lblResponseImgR = new System.Windows.Forms.Label();
            this.lblResponseImgG = new System.Windows.Forms.Label();
            this.lblResponseImgB = new System.Windows.Forms.Label();
            this.imgRequest = new System.Windows.Forms.PictureBox();
            this.imgResponse = new System.Windows.Forms.PictureBox();
            this.lblCheckResponse = new System.Windows.Forms.Label();
            this.cmbCheck = new System.Windows.Forms.ComboBox();
            this.lblCheckState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgRequest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgResponse)).BeginInit();
            this.SuspendLayout();
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splMain.Location = new System.Drawing.Point(0, 0);
            this.splMain.Margin = new System.Windows.Forms.Padding(0);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splMain.Panel1MinSize = 250;
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splMain.Panel2MinSize = 530;
            this.splMain.Size = new System.Drawing.Size(784, 561);
            this.splMain.SplitterDistance = 250;
            this.splMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.prpRequest, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCheck, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblServerState, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRequest, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbRequests, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.btnSend, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid2, 0, 14);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 20;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(250, 561);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // prpRequest
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.prpRequest, 4);
            this.prpRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prpRequest.LineColor = System.Drawing.SystemColors.ControlDark;
            this.prpRequest.Location = new System.Drawing.Point(3, 57);
            this.prpRequest.Name = "prpRequest";
            this.tableLayoutPanel1.SetRowSpan(this.prpRequest, 9);
            this.prpRequest.Size = new System.Drawing.Size(244, 237);
            this.prpRequest.TabIndex = 1;
            // 
            // btnCheck
            // 
            this.btnCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheck.Location = new System.Drawing.Point(189, 3);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(58, 21);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblServerState
            // 
            this.lblServerState.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblServerState, 3);
            this.lblServerState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServerState.Location = new System.Drawing.Point(3, 0);
            this.lblServerState.Name = "lblServerState";
            this.lblServerState.Size = new System.Drawing.Size(180, 27);
            this.lblServerState.TabIndex = 4;
            this.lblServerState.Text = "Server state: not available";
            this.lblServerState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRequest
            // 
            this.lblRequest.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRequest, 4);
            this.lblRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequest.Location = new System.Drawing.Point(3, 27);
            this.lblRequest.Name = "lblRequest";
            this.lblRequest.Size = new System.Drawing.Size(244, 27);
            this.lblRequest.TabIndex = 4;
            this.lblRequest.Text = "Request propeties:";
            this.lblRequest.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cmbRequests
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbRequests, 4);
            this.cmbRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRequests.FormattingEnabled = true;
            this.cmbRequests.Location = new System.Drawing.Point(3, 300);
            this.cmbRequests.Name = "cmbRequests";
            this.cmbRequests.Size = new System.Drawing.Size(244, 21);
            this.cmbRequests.TabIndex = 3;
            // 
            // btnSend
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnSend, 4);
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(3, 327);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(244, 21);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send request";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 4);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "Response propeties:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // propertyGrid2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.propertyGrid2, 6);
            this.propertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid2.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid2.Location = new System.Drawing.Point(3, 381);
            this.propertyGrid2.Name = "propertyGrid2";
            this.tableLayoutPanel1.SetRowSpan(this.propertyGrid2, 6);
            this.propertyGrid2.Size = new System.Drawing.Size(244, 177);
            this.propertyGrid2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 12;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 6, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblRequestImg, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblResponseImg, 7, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblRequestImgA, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblRequestImgR, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblRequestImgG, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblbRequestImgB, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblResponseImgA, 7, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblResponseImgR, 8, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblResponseImgG, 9, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblResponseImgB, 10, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(530, 561);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 9;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel3, 12);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.lblSelectPixel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.numX, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.numY, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblCheckResponse, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbCheck, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblCheckState, 7, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(530, 27);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 6);
            this.panel1.Controls.Add(this.imgRequest);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 474);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 6);
            this.panel2.Controls.Add(this.imgResponse);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(267, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 474);
            this.panel2.TabIndex = 1;
            // 
            // lblSelectPixel
            // 
            this.lblSelectPixel.AutoSize = true;
            this.lblSelectPixel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectPixel.Location = new System.Drawing.Point(78, 0);
            this.lblSelectPixel.Name = "lblSelectPixel";
            this.lblSelectPixel.Size = new System.Drawing.Size(74, 27);
            this.lblSelectPixel.TabIndex = 0;
            this.lblSelectPixel.Text = "Select pixel:";
            this.lblSelectPixel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numX
            // 
            this.numX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numX.Location = new System.Drawing.Point(158, 3);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(54, 20);
            this.numX.TabIndex = 1;
            // 
            // numY
            // 
            this.numY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numY.Location = new System.Drawing.Point(218, 3);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(54, 20);
            this.numY.TabIndex = 1;
            // 
            // lblRequestImg
            // 
            this.lblRequestImg.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblRequestImg, 4);
            this.lblRequestImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestImg.Location = new System.Drawing.Point(45, 27);
            this.lblRequestImg.Name = "lblRequestImg";
            this.lblRequestImg.Size = new System.Drawing.Size(174, 27);
            this.lblRequestImg.TabIndex = 2;
            this.lblRequestImg.Text = "Request";
            this.lblRequestImg.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblResponseImg
            // 
            this.lblResponseImg.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblResponseImg, 4);
            this.lblResponseImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResponseImg.Location = new System.Drawing.Point(309, 27);
            this.lblResponseImg.Name = "lblResponseImg";
            this.lblResponseImg.Size = new System.Drawing.Size(174, 27);
            this.lblResponseImg.TabIndex = 2;
            this.lblResponseImg.Text = "Response";
            this.lblResponseImg.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblRequestImgA
            // 
            this.lblRequestImgA.AutoSize = true;
            this.lblRequestImgA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestImgA.Location = new System.Drawing.Point(45, 54);
            this.lblRequestImgA.Name = "lblRequestImgA";
            this.lblRequestImgA.Size = new System.Drawing.Size(39, 27);
            this.lblRequestImgA.TabIndex = 3;
            this.lblRequestImgA.Text = "A: 255";
            this.lblRequestImgA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRequestImgR
            // 
            this.lblRequestImgR.AutoSize = true;
            this.lblRequestImgR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestImgR.Location = new System.Drawing.Point(90, 54);
            this.lblRequestImgR.Name = "lblRequestImgR";
            this.lblRequestImgR.Size = new System.Drawing.Size(39, 27);
            this.lblRequestImgR.TabIndex = 3;
            this.lblRequestImgR.Text = "R: 255";
            this.lblRequestImgR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRequestImgG
            // 
            this.lblRequestImgG.AutoSize = true;
            this.lblRequestImgG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestImgG.Location = new System.Drawing.Point(135, 54);
            this.lblRequestImgG.Name = "lblRequestImgG";
            this.lblRequestImgG.Size = new System.Drawing.Size(39, 27);
            this.lblRequestImgG.TabIndex = 3;
            this.lblRequestImgG.Text = "G: 255";
            this.lblRequestImgG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblbRequestImgB
            // 
            this.lblbRequestImgB.AutoSize = true;
            this.lblbRequestImgB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblbRequestImgB.Location = new System.Drawing.Point(180, 54);
            this.lblbRequestImgB.Name = "lblbRequestImgB";
            this.lblbRequestImgB.Size = new System.Drawing.Size(39, 27);
            this.lblbRequestImgB.TabIndex = 3;
            this.lblbRequestImgB.Text = "B: 255";
            this.lblbRequestImgB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResponseImgA
            // 
            this.lblResponseImgA.AutoSize = true;
            this.lblResponseImgA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResponseImgA.Location = new System.Drawing.Point(309, 54);
            this.lblResponseImgA.Name = "lblResponseImgA";
            this.lblResponseImgA.Size = new System.Drawing.Size(39, 27);
            this.lblResponseImgA.TabIndex = 3;
            this.lblResponseImgA.Text = "A: 255";
            this.lblResponseImgA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResponseImgR
            // 
            this.lblResponseImgR.AutoSize = true;
            this.lblResponseImgR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResponseImgR.Location = new System.Drawing.Point(354, 54);
            this.lblResponseImgR.Name = "lblResponseImgR";
            this.lblResponseImgR.Size = new System.Drawing.Size(39, 27);
            this.lblResponseImgR.TabIndex = 3;
            this.lblResponseImgR.Text = "R: 255";
            this.lblResponseImgR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResponseImgG
            // 
            this.lblResponseImgG.AutoSize = true;
            this.lblResponseImgG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResponseImgG.Location = new System.Drawing.Point(399, 54);
            this.lblResponseImgG.Name = "lblResponseImgG";
            this.lblResponseImgG.Size = new System.Drawing.Size(39, 27);
            this.lblResponseImgG.TabIndex = 3;
            this.lblResponseImgG.Text = "G: 255";
            this.lblResponseImgG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResponseImgB
            // 
            this.lblResponseImgB.AutoSize = true;
            this.lblResponseImgB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResponseImgB.Location = new System.Drawing.Point(444, 54);
            this.lblResponseImgB.Name = "lblResponseImgB";
            this.lblResponseImgB.Size = new System.Drawing.Size(39, 27);
            this.lblResponseImgB.TabIndex = 3;
            this.lblResponseImgB.Text = "B: 255";
            this.lblResponseImgB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgRequest
            // 
            this.imgRequest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgRequest.Location = new System.Drawing.Point(45, 59);
            this.imgRequest.Name = "imgRequest";
            this.imgRequest.Size = new System.Drawing.Size(100, 50);
            this.imgRequest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgRequest.TabIndex = 0;
            this.imgRequest.TabStop = false;
            // 
            // imgResponse
            // 
            this.imgResponse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgResponse.Location = new System.Drawing.Point(55, 59);
            this.imgResponse.Name = "imgResponse";
            this.imgResponse.Size = new System.Drawing.Size(100, 50);
            this.imgResponse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgResponse.TabIndex = 0;
            this.imgResponse.TabStop = false;
            // 
            // lblCheckResponse
            // 
            this.lblCheckResponse.AutoSize = true;
            this.lblCheckResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckResponse.Location = new System.Drawing.Point(315, 0);
            this.lblCheckResponse.Name = "lblCheckResponse";
            this.lblCheckResponse.Size = new System.Drawing.Size(74, 27);
            this.lblCheckResponse.TabIndex = 0;
            this.lblCheckResponse.Text = "Check:";
            this.lblCheckResponse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCheck
            // 
            this.cmbCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCheck.FormattingEnabled = true;
            this.cmbCheck.Location = new System.Drawing.Point(309, 1);
            this.cmbCheck.Name = "cmbCheck";
            this.cmbCheck.Size = new System.Drawing.Size(121, 21);
            this.cmbCheck.TabIndex = 3;
            // 
            // lblCheckState
            // 
            this.lblCheckState.AutoSize = true;
            this.lblCheckState.BackColor = System.Drawing.Color.ForestGreen;
            this.lblCheckState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckState.Location = new System.Drawing.Point(475, 0);
            this.lblCheckState.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lblCheckState.Name = "lblCheckState";
            this.lblCheckState.Size = new System.Drawing.Size(14, 27);
            this.lblCheckState.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splMain);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Kontur.ImageTransformer Demo";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgRequest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgResponse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PropertyGrid prpRequest;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblServerState;
        private System.Windows.Forms.Label lblRequest;
        private System.Windows.Forms.ComboBox cmbRequests;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblSelectPixel;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRequestImg;
        private System.Windows.Forms.Label lblResponseImg;
        private System.Windows.Forms.Label lblRequestImgA;
        private System.Windows.Forms.Label lblRequestImgR;
        private System.Windows.Forms.Label lblRequestImgG;
        private System.Windows.Forms.Label lblbRequestImgB;
        private System.Windows.Forms.Label lblResponseImgA;
        private System.Windows.Forms.Label lblResponseImgR;
        private System.Windows.Forms.Label lblResponseImgG;
        private System.Windows.Forms.Label lblResponseImgB;
        private System.Windows.Forms.PictureBox imgRequest;
        private System.Windows.Forms.PictureBox imgResponse;
        private System.Windows.Forms.Label lblCheckResponse;
        private System.Windows.Forms.ComboBox cmbCheck;
        private System.Windows.Forms.Label lblCheckState;
    }
}

