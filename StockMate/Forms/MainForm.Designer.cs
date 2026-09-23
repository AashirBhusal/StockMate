#nullable disable

namespace StockMate.Forms
{
    partial class MainForm
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
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdItems = new System.Windows.Forms.DataGridView();
            this.colItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReorder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.lblWarnings = new System.Windows.Forms.Label();
            this.lstWarnings = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.SuspendLayout();

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(14, 18);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Text = "Category:";

            // cboCategory
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(84, 14);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(220, 23);
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(316, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // grdItems
            this.grdItems.AllowUserToAddRows = false;
            this.grdItems.AllowUserToDeleteRows = false;
            this.grdItems.ReadOnly = true;
            this.grdItems.MultiSelect = false;
            this.grdItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItemId, this.colName, this.colCategory, this.colType,
                this.colQuantity, this.colUnit, this.colReorder, this.colCost});
            this.grdItems.Location = new System.Drawing.Point(14, 50);
            this.grdItems.Name = "grdItems";
            this.grdItems.RowHeadersVisible = false;
            this.grdItems.Size = new System.Drawing.Size(700, 320);

            // columns
            this.colItemId.HeaderText = "Id";
            this.colItemId.Name = "colItemId";
            this.colItemId.Visible = false;

            this.colName.HeaderText = "Item";
            this.colName.Name = "colName";
            this.colName.Width = 190;

            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.Width = 110;

            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.Width = 90;

            this.colQuantity.HeaderText = "Qty";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 60;

            this.colUnit.HeaderText = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Width = 70;

            this.colReorder.HeaderText = "Reorder at";
            this.colReorder.Name = "colReorder";
            this.colReorder.Width = 80;

            this.colCost.HeaderText = "Unit cost";
            this.colCost.Name = "colCost";
            this.colCost.Width = 80;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(730, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(220, 34);
            this.btnAdd.Text = "Add item";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(730, 94);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(220, 34);
            this.btnEdit.Text = "Edit selected item";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnReceive
            this.btnReceive.Location = new System.Drawing.Point(730, 148);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(220, 34);
            this.btnReceive.Text = "Record delivery (receipt)";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);

            // btnIssue
            this.btnIssue.Location = new System.Drawing.Point(730, 192);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(220, 34);
            this.btnIssue.Text = "Take stock out (issue)";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);

            // btnDeactivate
            this.btnDeactivate.Location = new System.Drawing.Point(730, 300);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(220, 34);
            this.btnDeactivate.Text = "Deactivate item";
            this.btnDeactivate.UseVisualStyleBackColor = true;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);

            // lblWarnings
            this.lblWarnings.AutoSize = true;
            this.lblWarnings.Location = new System.Drawing.Point(14, 384);
            this.lblWarnings.Name = "lblWarnings";
            this.lblWarnings.Text = "Needs attention:";

            // lstWarnings
            this.lstWarnings.FormattingEnabled = true;
            this.lstWarnings.ItemHeight = 15;
            this.lstWarnings.Location = new System.Drawing.Point(14, 404);
            this.lstWarnings.Name = "lstWarnings";
            this.lstWarnings.Size = new System.Drawing.Size(936, 154);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 574);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grdItems);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.btnDeactivate);
            this.Controls.Add(this.lblWarnings);
            this.Controls.Add(this.lstWarnings);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StockMate - Inventory";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView grdItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.ListBox lstWarnings;
    }
}
