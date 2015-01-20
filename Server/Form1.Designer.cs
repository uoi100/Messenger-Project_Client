namespace Server
{
    partial class Form1
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
            this.btn_Server = new System.Windows.Forms.Button();
            this.text_IPAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Server
            // 
            this.btn_Server.Location = new System.Drawing.Point(85, 120);
            this.btn_Server.Name = "btn_Server";
            this.btn_Server.Size = new System.Drawing.Size(75, 23);
            this.btn_Server.TabIndex = 0;
            this.btn_Server.Text = "Start Server";
            this.btn_Server.UseVisualStyleBackColor = true;
            this.btn_Server.Click += new System.EventHandler(this.btn_Server_Click);
            // 
            // text_IPAddress
            // 
            this.text_IPAddress.Location = new System.Drawing.Point(60, 168);
            this.text_IPAddress.MaxLength = 36;
            this.text_IPAddress.Name = "text_IPAddress";
            this.text_IPAddress.Size = new System.Drawing.Size(100, 20);
            this.text_IPAddress.TabIndex = 1;
            this.text_IPAddress.Text = "127.0.0.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.text_IPAddress);
            this.Controls.Add(this.btn_Server);
            this.Name = "Form1";
            this.Text = "Server Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Server;
        private System.Windows.Forms.TextBox text_IPAddress;
    }
}

