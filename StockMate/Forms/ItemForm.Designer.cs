#nullable disable

namespace StockMate.Forms
{
    partial class ItemForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblReorder = new System.Windows.Forms.Label();
            this.numReorder = new System.Windows.Forms.NumericUpDown();
            this.lblCost = new System.Windows.Forms.Label();
            this.numCost = new System.Windows.Forms.NumericUpDown();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.lblMonths = new System.Windows.Forms.Label();
            this.numMonths = new System.Windows.Forms.NumericUpDown();
            this.lblLastReplaced = new System.Windows.Forms.Label();
            this.dtpLastReplaced = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonths)).BeginInit();
            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 20);
            this.lblName.Name = "lblName";
            this.lblName.Text = "Name:";

            // txtName
            this.txtName.Location = new System.Drawing.Point(140, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(240, 23);

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(16, 52);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Text = "Category:";

            // txtCategory
            this.txtCategory.Location = new System.Drawing.Point(140, 49);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(240, 23);

            // lblType
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(16, 84);
            this.lblType.Name = "lblType";
            this.lblType.Text = "Type:";

            // cboType
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Location = new System.Drawing.Point(140, 81);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(240, 23);
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);

            // lblUnit
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(16, 116);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Text = "Unit (each, box):";

            // txtUnit
            this.txtUnit.Location = new System.Drawing.Point(140, 113);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(240, 23);
            this.txtUnit.Text = "each";

            // lblQuantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(16, 148);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Text = "Starting quantity:";

            // numQuantity
            this.numQuantity.Location = new System.Drawing.Point(140, 146);
            this.numQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 23);

            // lblReorder
            this.lblReorder.AutoSize = true;
            this.lblReorder.Location = new System.Drawing.Point(16, 180);
            this.lblReorder.Name = "lblReorder";
            this.lblReorder.Text = "Reorder level:";

            // numReorder
            this.numReorder.Location = new System.Drawing.Point(140, 178);
            this.numReorder.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numReorder.Name = "numReorder";
            this.numReorder.Size = new System.Drawing.Size(120, 23);

            // lblCost
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(16, 212);
            this.lblCost.Name = "lblCost";
            this.lblCost.Text = "Unit cost:";

            // numCost
            this.numCost.DecimalPlaces = 2;
            this.numCost.Location = new System.Drawing.Point(140, 210);
            this.numCost.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numCost.Name = "numCost";
            this.numCost.Size = new System.Drawing.Size(120, 23);

            // lblExpiry
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Location = new System.Drawing.Point(16, 248);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Text = "Use-by date:";

            // dtpExpiry
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(140, 244);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(240, 23);

            // lblMonths
            this.lblMonths.AutoSize = true;
            this.lblMonths.Location = new System.Drawing.Point(16, 248);
            this.lblMonths.Name = "lblMonths";
            this.lblMonths.Text = "Replace after (months):";
            this.lblMonths.Visible = false;

            // numMonths
            this.numMonths.Location = new System.Drawing.Point(180, 244);
            this.numMonths.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMonths.Maximum = new decimal(new int[] { 240, 0, 0, 0 });
            this.numMonths.Value = new decimal(new int[] { 12, 0, 0, 0 });
            this.numMonths.Name = "numMonths";
            this.numMonths.Size = new System.Drawing.Size(120, 23);
            this.numMonths.Visible = false;

            // lblLastReplaced
            this.lblLastReplaced.AutoSize = true;
            this.lblLastReplaced.Location = new System.Drawing.Point(16, 280);
            this.lblLastReplaced.Name = "lblLastReplaced";
            this.lblLastReplaced.Text = "Last replaced:";
            this.lblLastReplaced.Visible = false;

            // dtpLastReplaced
            this.dtpLastReplaced.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLastReplaced.Location = new System.Drawing.Point(180, 276);
            this.dtpLastReplaced.Name = "dtpLastReplaced";
            this.dtpLastReplaced.Size = new System.Drawing.Size(200, 23);
            this.dtpLastReplaced.Visible = false;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(180, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 30);
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(284, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ItemForm
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 368);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblReorder);
            this.Controls.Add(this.numReorder);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.numCost);
            this.Controls.Add(this.lblExpiry);
            this.Controls.Add(this.dtpExpiry);
            this.Controls.Add(this.lblMonths);
            this.Controls.Add(this.numMonths);
            this.Controls.Add(this.lblLastReplaced);
            this.Controls.Add(this.dtpLastReplaced);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item";
            this.Load += new System.EventHandler(this.ItemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonths)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblReorder;
        private System.Windows.Forms.NumericUpDown numReorder;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.NumericUpDown numCost;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Label lblMonths;
        private System.Windows.Forms.NumericUpDown numMonths;
        private System.Windows.Forms.Label lblLastReplaced;
        private System.Windows.Forms.DateTimePicker dtpLastReplaced;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
