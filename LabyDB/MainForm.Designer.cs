
namespace LabyDB {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.AbonentsFormButton = new System.Windows.Forms.Button();
            this.PaymentsFormButton = new System.Windows.Forms.Button();
            this.FormExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AbonentsFormButton
            // 
            this.AbonentsFormButton.BackColor = System.Drawing.SystemColors.Window;
            this.AbonentsFormButton.Location = new System.Drawing.Point(12, 12);
            this.AbonentsFormButton.Name = "AbonentsFormButton";
            this.AbonentsFormButton.Size = new System.Drawing.Size(75, 22);
            this.AbonentsFormButton.TabIndex = 0;
            this.AbonentsFormButton.Text = "Абонент";
            this.AbonentsFormButton.UseVisualStyleBackColor = false;
            this.AbonentsFormButton.Click += new System.EventHandler(this.abonentsForm_open_button);
            // 
            // PaymentsFormButton
            // 
            this.PaymentsFormButton.BackColor = System.Drawing.SystemColors.Window;
            this.PaymentsFormButton.Location = new System.Drawing.Point(12, 40);
            this.PaymentsFormButton.Name = "PaymentsFormButton";
            this.PaymentsFormButton.Size = new System.Drawing.Size(75, 22);
            this.PaymentsFormButton.TabIndex = 1;
            this.PaymentsFormButton.Text = "Оплата";
            this.PaymentsFormButton.UseVisualStyleBackColor = false;
            this.PaymentsFormButton.Click += new System.EventHandler(this.paymentsForm_open_button);
            // 
            // FormExitButton
            // 
            this.FormExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FormExitButton.BackColor = System.Drawing.SystemColors.Window;
            this.FormExitButton.Location = new System.Drawing.Point(137, 125);
            this.FormExitButton.Name = "FormExitButton";
            this.FormExitButton.Size = new System.Drawing.Size(75, 22);
            this.FormExitButton.TabIndex = 2;
            this.FormExitButton.Text = "Выход";
            this.FormExitButton.UseVisualStyleBackColor = false;
            this.FormExitButton.Click += new System.EventHandler(this.FormExitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(224, 159);
            this.Controls.Add(this.FormExitButton);
            this.Controls.Add(this.PaymentsFormButton);
            this.Controls.Add(this.AbonentsFormButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AbonentsFormButton;
        private System.Windows.Forms.Button PaymentsFormButton;
        private System.Windows.Forms.Button FormExitButton;
    }
}