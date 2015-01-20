namespace Client
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.text_msg = new System.Windows.Forms.TextBox();
            this.text_IPAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(96, 119);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start Client";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // text_msg
            // 
            this.text_msg.Location = new System.Drawing.Point(76, 70);
            this.text_msg.Name = "text_msg";
            this.text_msg.Size = new System.Drawing.Size(100, 20);
            this.text_msg.TabIndex = 1;
            this.text_msg.Text = "Send Message";
            // 
            // text_IPAddress
            // 
            this.text_IPAddress.Location = new System.Drawing.Point(76, 213);
            this.text_IPAddress.Name = "text_IPAddress";
            this.text_IPAddress.Size = new System.Drawing.Size(100, 20);
            this.text_IPAddress.TabIndex = 2;
            this.text_IPAddress.Text = "127.0.0.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.text_IPAddress);
            this.Controls.Add(this.text_msg);
            this.Controls.Add(this.btn_Start);
            this.Name = "Form1";
            this.Text = "Client Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox text_msg;
        private System.Windows.Forms.TextBox text_IPAddress;
    }
}

