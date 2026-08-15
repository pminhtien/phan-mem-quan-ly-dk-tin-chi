namespace BTL_QLDKTC
{
    partial class frmCourses
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelPhanChia1 = new System.Windows.Forms.Panel();
            this.comCTDT = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSemester = new System.Windows.Forms.Label();
            this.grdRegisteredCourses = new System.Windows.Forms.DataGridView();
            this.col2TT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2TenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2MaLopHP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2STC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2GV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2LichHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2Xoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblRegisteredResult = new System.Windows.Forms.Label();
            this.grdCourses = new System.Windows.Forms.DataGridView();
            this.col1No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col1CourseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col1CourseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col1Credits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col1OpenClasses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelPhanChia2 = new System.Windows.Forms.Panel();
            this.panelBody2 = new System.Windows.Forms.Panel();
            this.toolTip_grdCourses = new System.Windows.Forms.ToolTip(this.components);
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegisteredCourses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCourses)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelBody2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblStatusMessage);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.panelPhanChia1);
            this.panelHeader.Controls.Add(this.comCTDT);
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Controls.Add(this.lblSemester);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1575, 89);
            this.panelHeader.TabIndex = 0;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMessage.Location = new System.Drawing.Point(944, 49);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(0, 25);
            this.lblStatusMessage.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefresh.Location = new System.Drawing.Point(744, 45);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(134, 33);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelPhanChia1
            // 
            this.panelPhanChia1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelPhanChia1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPhanChia1.Location = new System.Drawing.Point(0, 88);
            this.panelPhanChia1.Name = "panelPhanChia1";
            this.panelPhanChia1.Size = new System.Drawing.Size(1575, 1);
            this.panelPhanChia1.TabIndex = 3;
            // 
            // comCTDT
            // 
            this.comCTDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comCTDT.FormattingEnabled = true;
            this.comCTDT.Location = new System.Drawing.Point(327, 46);
            this.comCTDT.Name = "comCTDT";
            this.comCTDT.Size = new System.Drawing.Size(341, 33);
            this.comCTDT.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(109, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chương trình đào tạo";
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemester.Location = new System.Drawing.Point(7, 9);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(201, 29);
            this.lblSemester.TabIndex = 0;
            this.lblSemester.Text = "Năm học - Học kỳ";
            // 
            // grdRegisteredCourses
            // 
            this.grdRegisteredCourses.AllowUserToAddRows = false;
            this.grdRegisteredCourses.AllowUserToDeleteRows = false;
            this.grdRegisteredCourses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdRegisteredCourses.BackgroundColor = System.Drawing.Color.White;
            this.grdRegisteredCourses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdRegisteredCourses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdRegisteredCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRegisteredCourses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col2TT,
            this.col2TenMon,
            this.col2MaLopHP,
            this.col2STC,
            this.col2GV,
            this.col2LichHoc,
            this.col2Xoa});
            this.grdRegisteredCourses.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdRegisteredCourses.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdRegisteredCourses.GridColor = System.Drawing.SystemColors.Control;
            this.grdRegisteredCourses.Location = new System.Drawing.Point(12, 40);
            this.grdRegisteredCourses.Name = "grdRegisteredCourses";
            this.grdRegisteredCourses.ReadOnly = true;
            this.grdRegisteredCourses.RowHeadersWidth = 51;
            this.grdRegisteredCourses.RowTemplate.Height = 24;
            this.grdRegisteredCourses.Size = new System.Drawing.Size(1560, 294);
            this.grdRegisteredCourses.TabIndex = 8;
            this.grdRegisteredCourses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdRegisteredCourses_CellContentClick);
            // 
            // col2TT
            // 
            this.col2TT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col2TT.HeaderText = "STT";
            this.col2TT.MinimumWidth = 6;
            this.col2TT.Name = "col2TT";
            this.col2TT.ReadOnly = true;
            this.col2TT.Width = 81;
            // 
            // col2TenMon
            // 
            this.col2TenMon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col2TenMon.HeaderText = "Tên môn học";
            this.col2TenMon.MinimumWidth = 200;
            this.col2TenMon.Name = "col2TenMon";
            this.col2TenMon.ReadOnly = true;
            this.col2TenMon.Width = 200;
            // 
            // col2MaLopHP
            // 
            this.col2MaLopHP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col2MaLopHP.HeaderText = "Mã LHP";
            this.col2MaLopHP.MinimumWidth = 6;
            this.col2MaLopHP.Name = "col2MaLopHP";
            this.col2MaLopHP.ReadOnly = true;
            this.col2MaLopHP.Width = 112;
            // 
            // col2STC
            // 
            this.col2STC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col2STC.HeaderText = "STC";
            this.col2STC.MinimumWidth = 50;
            this.col2STC.Name = "col2STC";
            this.col2STC.ReadOnly = true;
            this.col2STC.Width = 50;
            // 
            // col2GV
            // 
            this.col2GV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col2GV.HeaderText = "GV";
            this.col2GV.MinimumWidth = 6;
            this.col2GV.Name = "col2GV";
            this.col2GV.ReadOnly = true;
            this.col2GV.Width = 70;
            // 
            // col2LichHoc
            // 
            this.col2LichHoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col2LichHoc.HeaderText = "Lịch học";
            this.col2LichHoc.MinimumWidth = 6;
            this.col2LichHoc.Name = "col2LichHoc";
            this.col2LichHoc.ReadOnly = true;
            this.col2LichHoc.Width = 114;
            // 
            // col2Xoa
            // 
            this.col2Xoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.col2Xoa.DefaultCellStyle = dataGridViewCellStyle2;
            this.col2Xoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.col2Xoa.HeaderText = "";
            this.col2Xoa.MinimumWidth = 6;
            this.col2Xoa.Name = "col2Xoa";
            this.col2Xoa.ReadOnly = true;
            this.col2Xoa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col2Xoa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col2Xoa.Text = "Xóa";
            this.col2Xoa.UseColumnTextForButtonValue = true;
            this.col2Xoa.Width = 23;
            // 
            // lblRegisteredResult
            // 
            this.lblRegisteredResult.AutoSize = true;
            this.lblRegisteredResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegisteredResult.Location = new System.Drawing.Point(3, -1);
            this.lblRegisteredResult.Name = "lblRegisteredResult";
            this.lblRegisteredResult.Size = new System.Drawing.Size(184, 29);
            this.lblRegisteredResult.TabIndex = 0;
            this.lblRegisteredResult.Text = "Kết quả đăng ký";
            // 
            // grdCourses
            // 
            this.grdCourses.AllowUserToAddRows = false;
            this.grdCourses.AllowUserToDeleteRows = false;
            this.grdCourses.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.HighlightText;
            this.grdCourses.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grdCourses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdCourses.BackgroundColor = System.Drawing.Color.White;
            this.grdCourses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdCourses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdCourses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grdCourses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCourses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1No,
            this.col1CourseID,
            this.col1CourseName,
            this.col1Credits,
            this.col1OpenClasses});
            this.grdCourses.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdCourses.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdCourses.Location = new System.Drawing.Point(12, 128);
            this.grdCourses.Name = "grdCourses";
            this.grdCourses.ReadOnly = true;
            this.grdCourses.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdCourses.RowHeadersWidth = 51;
            this.grdCourses.RowTemplate.Height = 24;
            this.grdCourses.Size = new System.Drawing.Size(1560, 322);
            this.grdCourses.TabIndex = 0;
            this.toolTip_grdCourses.SetToolTip(this.grdCourses, "Nhấn chuột 2 lần để xem chi tiết lớp học phần");
            this.grdCourses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCourses_CellDoubleClick);
            // 
            // col1No
            // 
            this.col1No.HeaderText = "STT";
            this.col1No.MinimumWidth = 6;
            this.col1No.Name = "col1No";
            this.col1No.ReadOnly = true;
            this.col1No.Width = 81;
            // 
            // col1CourseID
            // 
            this.col1CourseID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col1CourseID.HeaderText = "Mã môn học";
            this.col1CourseID.MinimumWidth = 6;
            this.col1CourseID.Name = "col1CourseID";
            this.col1CourseID.ReadOnly = true;
            this.col1CourseID.Width = 149;
            // 
            // col1CourseName
            // 
            this.col1CourseName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col1CourseName.HeaderText = "Tên môn học";
            this.col1CourseName.MinimumWidth = 6;
            this.col1CourseName.Name = "col1CourseName";
            this.col1CourseName.ReadOnly = true;
            this.col1CourseName.Width = 156;
            // 
            // col1Credits
            // 
            this.col1Credits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.col1Credits.HeaderText = "STC";
            this.col1Credits.MinimumWidth = 50;
            this.col1Credits.Name = "col1Credits";
            this.col1Credits.ReadOnly = true;
            this.col1Credits.Width = 83;
            // 
            // col1OpenClasses
            // 
            this.col1OpenClasses.HeaderText = "Số LHP";
            this.col1OpenClasses.MinimumWidth = 6;
            this.col1OpenClasses.Name = "col1OpenClasses";
            this.col1OpenClasses.ReadOnly = true;
            this.col1OpenClasses.Width = 109;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 29);
            this.label5.TabIndex = 3;
            this.label5.Text = "Đăng ký môn học";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1575, 32);
            this.panel1.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblRegisteredResult);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1575, 34);
            this.panel3.TabIndex = 9;
            // 
            // panelPhanChia2
            // 
            this.panelPhanChia2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelPhanChia2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPhanChia2.Location = new System.Drawing.Point(0, 455);
            this.panelPhanChia2.Name = "panelPhanChia2";
            this.panelPhanChia2.Size = new System.Drawing.Size(1575, 1);
            this.panelPhanChia2.TabIndex = 11;
            // 
            // panelBody2
            // 
            this.panelBody2.Controls.Add(this.panel3);
            this.panelBody2.Controls.Add(this.grdRegisteredCourses);
            this.panelBody2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBody2.Location = new System.Drawing.Point(0, 456);
            this.panelBody2.Name = "panelBody2";
            this.panelBody2.Size = new System.Drawing.Size(1575, 339);
            this.panelBody2.TabIndex = 10;
            // 
            // frmCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1575, 795);
            this.Controls.Add(this.grdCourses);
            this.Controls.Add(this.panelPhanChia2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBody2);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmCourses";
            this.Load += new System.EventHandler(this.frmCourses_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegisteredCourses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCourses)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelBody2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.ComboBox comCTDT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelPhanChia1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView grdRegisteredCourses;
        private System.Windows.Forms.Label lblRegisteredResult;
        private System.Windows.Forms.DataGridView grdCourses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelPhanChia2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelBody2;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1No;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1CourseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1CourseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1Credits;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1OpenClasses;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2TT;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2TenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2MaLopHP;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2STC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2GV;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2LichHoc;
        private System.Windows.Forms.DataGridViewButtonColumn col2Xoa;
        private System.Windows.Forms.ToolTip toolTip_grdCourses;
    }
}