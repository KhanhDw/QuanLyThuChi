namespace QuanLyThuChi.ItemList
{
    partial class ItemPictureBox_ThemDanhMuc
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
            this.pictureBox_Item = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Item)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Item
            // 
            this.pictureBox_Item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Item.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Item.Name = "pictureBox_Item";
            this.pictureBox_Item.Size = new System.Drawing.Size(28, 28);
            this.pictureBox_Item.TabIndex = 0;
            this.pictureBox_Item.TabStop = false;
            this.pictureBox_Item.Click += new System.EventHandler(this.pictureBox_Item_Click);
            // 
            // ItemPictureBox_ThemDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBox_Item);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ItemPictureBox_ThemDanhMuc";
            this.Size = new System.Drawing.Size(28, 28);
            this.Load += new System.EventHandler(this.ItemPictureBox_ThemDanhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Item)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox_Item;
    }
}
