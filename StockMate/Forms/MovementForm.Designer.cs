#nullable disable

namespace StockMate.Forms
{
    partial class MovementForm
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
            this.lblItem = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblStaff = new System.Windows.Forms.Label();
            this.txtStaff = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();

            // lblItem
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(16, 18);
            this.lblItem.Name = "lblItem";
            this.lblItem.Text = "Item";

            // lblAction
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(16, 54);
            this.lblAction.Name = "lblAction";
            this.lblAction.Text = "How many units?";

            // numQuantity
            this.numQuantity.Location = new System.Drawing.Point(244, 52);
            this.numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 23);

            // lblStaff
            this.lblStaff.AutoSize = true;
            this.lblStaff.Location = new System.Drawing.Point(16, 90);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Text = "Your name:";

            // txtStaff
            this.txtStaff.Location = new System.Drawing.Point(244, 87);
            this.txtStaff.Name = "txtStaff";
            this.txtStaff.Size = new System.Drawing.Size(160, 23);

            // btnOk
            this.btnOk.Location = new System.Drawing.Point(204, 132);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 30);
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(308, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // MovementForm
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 180);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.txtStaff);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MovementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Movement";
            this.Load += new System.EventHandler(this.MovementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.TextBox txtStaff;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
