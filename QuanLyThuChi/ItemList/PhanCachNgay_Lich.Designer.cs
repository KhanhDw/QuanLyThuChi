namespace QuanLyThuChi.ItemList
{
    partial class PhanCachNgay_Lich
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbDay = new System.Windows.Forms.Label();
            this.lbtong = new System.Windows.Forms.Label();
            this.lbthu = new System.Windows.Forms.Label();
            this.lbchi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.Location = new System.Drawing.Point(4, 4);
            this.lbDay.Name = "lbDay";
            this.lbDay.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbDay.Size = new System.Drawing.Size(60, 19);
            this.lbDay.TabIndex = 0;
            this.lbDay.Text = "label1";
            // 
            // lbtong
            // 
            this.lbtong.AutoSize = true;
            this.lbtong.BackColor = System.Drawing.Color.FloralWhite;
            this.lbtong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbtong.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbtong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbtong.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtong.Location = new System.Drawing.Point(429, 0);
            this.lbtong.Name = "lbtong";
            this.lbtong.Size = new System.Drawing.Size(70, 21);
            this.lbtong.TabIndex = 1;
            this.lbtong.Text = "Còn lại: ";
            // 
            // lbthu
            // 
            this.lbthu.AutoSize = true;
            this.lbthu.BackColor = System.Drawing.Color.FloralWhite;
            this.lbthu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbthu.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbthu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbthu.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbthu.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbthu.Location = new System.Drawing.Point(359, 0);
            this.lbthu.Name = "lbthu";
            this.lbthu.Size = new System.Drawing.Size(70, 21);
            this.lbthu.TabIndex = 2;
            this.lbthu.Text = "Còn lại: ";
            // 
            // lbchi
            // 
            this.lbchi.AutoSize = true;
            this.lbchi.BackColor = System.Drawing.Color.FloralWhite;
            this.lbchi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbchi.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbchi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbchi.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbchi.ForeColor = System.Drawing.Color.OrangeRed;
            this.lbchi.Location = new System.Drawing.Point(289, 0);
            this.lbchi.Name = "lbchi";
            this.lbchi.Size = new System.Drawing.Size(70, 21);
            this.lbchi.TabIndex = 3;
            this.lbchi.Text = "Còn lại: ";
            // 
            // PhanCachNgay_Lich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbchi);
            this.Controls.Add(this.lbthu);
            this.Controls.Add(this.lbtong);
            this.Controls.Add(this.lbDay);
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.Name = "PhanCachNgay_Lich";
            this.Size = new System.Drawing.Size(499, 26);
            this.Load += new System.EventHandler(this.PhanCachNgay_Lich_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbtong;
        private System.Windows.Forms.Label lbthu;
        private System.Windows.Forms.Label lbchi;
    }
}
