namespace LEARNING_EF_CODE_FIRST
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

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
			this.countryNameTextBox = new System.Windows.Forms.TextBox();
			this.countryCodeFromTextBox = new System.Windows.Forms.TextBox();
			this.countryCodeToTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// countryNameTextBox
			// 
			this.countryNameTextBox.Location = new System.Drawing.Point(12, 12);
			this.countryNameTextBox.Name = "countryNameTextBox";
			this.countryNameTextBox.Size = new System.Drawing.Size(266, 20);
			this.countryNameTextBox.TabIndex = 0;
			// 
			// countryCodeFromTextBox
			// 
			this.countryCodeFromTextBox.Location = new System.Drawing.Point(12, 38);
			this.countryCodeFromTextBox.Name = "countryCodeFromTextBox";
			this.countryCodeFromTextBox.Size = new System.Drawing.Size(266, 20);
			this.countryCodeFromTextBox.TabIndex = 1;
			// 
			// countryCodeToTextBox
			// 
			this.countryCodeToTextBox.Location = new System.Drawing.Point(12, 64);
			this.countryCodeToTextBox.Name = "countryCodeToTextBox";
			this.countryCodeToTextBox.Size = new System.Drawing.Size(266, 20);
			this.countryCodeToTextBox.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 262);
			this.Controls.Add(this.countryCodeToTextBox);
			this.Controls.Add(this.countryCodeFromTextBox);
			this.Controls.Add(this.countryNameTextBox);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Main";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox countryNameTextBox;
		private System.Windows.Forms.TextBox countryCodeFromTextBox;
		private System.Windows.Forms.TextBox countryCodeToTextBox;
	}
}
