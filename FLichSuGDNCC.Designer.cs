using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    partial class FLichSuGDNCC
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
System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.tCtlThongTinGD = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_ThongTinGD = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_DSPhieuNhap = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.tCtlThongTinGD.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ThongTinGD)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DSPhieuNhap)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.btnDong);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 498);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(912, 92);
            this.panel4.TabIndex = 7;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(401, 28);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click_2);
            // 
            // tCtlThongTinGD
            // 
this.tCtlThongTinGD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tCtlThongTinGD.Controls.Add(this.tabPage1);
            this.tCtlThongTinGD.Controls.Add(this.tabPage2);
this.tCtlThongTinGD.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tCtlThongTinGD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tCtlThongTinGD.ItemSize = new System.Drawing.Size(180, 35);
            this.tCtlThongTinGD.Location = new System.Drawing.Point(12, 76);
            this.tCtlThongTinGD.Name = "tCtlThongTinGD";
            this.tCtlThongTinGD.SelectedIndex = 0;
            this.tCtlThongTinGD.Size = new System.Drawing.Size(888, 391);
this.tCtlThongTinGD.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tCtlThongTinGD.TabIndex = 6;
this.tCtlThongTinGD.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tCtlThongTinGD_DrawItem);
            // 
            // tabPage1
            // 
this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.dgv_ThongTinGD);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(880, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông tin giao dịch";
                        // 
            // dgv_ThongTinGD
            // 
            this.dgv_ThongTinGD.AllowUserToAddRows = false;
            this.dgv_ThongTinGD.AllowUserToDeleteRows = false;
            this.dgv_ThongTinGD.AllowUserToResizeRows = false;
            this.dgv_ThongTinGD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ThongTinGD.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ThongTinGD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_ThongTinGD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ThongTinGD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ThongTinGD.ColumnHeadersHeight = 40;
            this.dgv_ThongTinGD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ThongTinGD.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ThongTinGD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ThongTinGD.EnableHeadersVisualStyles = false;
            this.dgv_ThongTinGD.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.dgv_ThongTinGD.Location = new System.Drawing.Point(3, 3);
            this.dgv_ThongTinGD.Name = "dgv_ThongTinGD";
this.dgv_ThongTinGD.ReadOnly = true;
            this.dgv_ThongTinGD.RowHeadersVisible = false;
            this.dgv_ThongTinGD.RowHeadersWidth = 51;
            this.dgv_ThongTinGD.RowTemplate.Height = 28;
            this.dgv_ThongTinGD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ThongTinGD.Size = new System.Drawing.Size(874, 342);
            this.dgv_ThongTinGD.TabIndex = 1;
            this.dgv_ThongTinGD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ThongTinGD_CellContentClick);
            // 
            // tabPage2
            // 
this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.dgv_DSPhieuNhap);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(880, 348);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Danh sách phiếu nhập";
                        // 
            // dgv_DSPhieuNhap
            // 
            this.dgv_DSPhieuNhap.AllowUserToAddRows = false;
            this.dgv_DSPhieuNhap.AllowUserToDeleteRows = false;
            this.dgv_DSPhieuNhap.AllowUserToResizeRows = false;
            this.dgv_DSPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DSPhieuNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgv_DSPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_DSPhieuNhap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DSPhieuNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_DSPhieuNhap.ColumnHeadersHeight = 40;
            this.dgv_DSPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DSPhieuNhap.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_DSPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DSPhieuNhap.EnableHeadersVisualStyles = false;
            this.dgv_DSPhieuNhap.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.dgv_DSPhieuNhap.Location = new System.Drawing.Point(3, 3);
            this.dgv_DSPhieuNhap.Name = "dgv_DSPhieuNhap";
this.dgv_DSPhieuNhap.ReadOnly = true;
            this.dgv_DSPhieuNhap.RowHeadersVisible = false;
            this.dgv_DSPhieuNhap.RowHeadersWidth = 51;
            this.dgv_DSPhieuNhap.RowTemplate.Height = 28;
            this.dgv_DSPhieuNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DSPhieuNhap.Size = new System.Drawing.Size(874, 342);
            this.dgv_DSPhieuNhap.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 60);
            this.panel1.TabIndex = 5;
            // 
            // lblTitle
            // 
                        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(912, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LỊCH SỬ GIAO DỊCH NHÀ CUNG CẤP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FLichSuGDNCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(912, 590);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.tCtlThongTinGD);
            this.Controls.Add(this.panel1);
            this.Name = "FLichSuGDNCC";
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch Sử Giao Dịch Nhà Cung Cấp";
            this.Load += new System.EventHandler(this.FLichSuGDNCC_Load);
            this.panel4.ResumeLayout(false);
            this.tCtlThongTinGD.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ThongTinGD)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DSPhieuNhap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TabControl tCtlThongTinGD;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_ThongTinGD;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_DSPhieuNhap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;

        private void tCtlThongTinGD_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Get the TabControl
            TabControl tabControl = sender as TabControl;
            
            // Create font objects
            Font regularFont = new Font("Segoe UI", 9F, FontStyle.Regular);
            Font selectedFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            
            // Define colors
            Color selectedTabColor = Color.FromArgb(33, 150, 243); // Blue color for selected tab
            Color unselectedTabColor = Color.FromArgb(210, 232, 255); // Light blue for unselected tabs
            Color textColor = Color.White; // Text color for selected tab
            Color unselectedTextColor = Color.FromArgb(33, 150, 243); // Text color for unselected tabs
            
            // Get the tab page
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabBounds = tabControl.GetTabRect(e.Index);
            
            // Determine if the tab is selected
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            
            // Create brush for background
            using (SolidBrush brush = new SolidBrush(isSelected ? selectedTabColor : unselectedTabColor))
            {
                // Fill the background
                e.Graphics.FillRectangle(brush, tabBounds);
                
                // Create string format for text
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                
                // Set the appropriate text color and font
                using (SolidBrush textBrush = new SolidBrush(isSelected ? textColor : unselectedTextColor))
                {
                    e.Graphics.DrawString(
                        tabPage.Text,
                        isSelected ? selectedFont : regularFont,
                        textBrush,
                        tabBounds,
                        stringFormat
                    );
                }
            }
        }
    }
}