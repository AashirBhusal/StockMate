#nullable disable

namespace StockMate.Forms
{
    partial class HistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSummary = new System.Windows.Forms.Label();
            this.grdHistory = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStaff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).BeginInit();
            this.SuspendLayout();

            // lblSummary
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(14, 14);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Text = "Summary";

            // grdHistory
            this.grdHistory.AllowUserToAddRows = false;
            this.grdHistory.AllowUserToDeleteRows = false;
            this.grdHistory.ReadOnly = true;
            this.grdHistory.MultiSelect = false;
            this.grdHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colDate, this.colType, this.colQuantity, this.colStaff});
            this.grdHistory.Location = new System.Drawing.Point(14, 44);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.RowHeadersVisible = false;
            this.grdHistory.Size = new System.Drawing.Size(592, 296);

            // columns
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.Width = 150;

            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.Width = 90;

            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 90;

            this.colStaff.HeaderText = "Staff";
            this.colStaff.Name = "colStaff";
            this.colStaff.Width = 240;

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(510, 352);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 30);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // HistoryForm
            this.CancelButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 396);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.grdHistory);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "History";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataGridView grdHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStaff;
        private System.Windows.Forms.Button btnClose;
    }
}
