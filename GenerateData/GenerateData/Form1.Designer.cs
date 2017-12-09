namespace GenerateData
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
            this.button_Generate_Data = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_Generate_Data
            // 
            this.button_Generate_Data.Location = new System.Drawing.Point(139, 42);
            this.button_Generate_Data.Name = "button_Generate_Data";
            this.button_Generate_Data.Size = new System.Drawing.Size(211, 69);
            this.button_Generate_Data.TabIndex = 0;
            this.button_Generate_Data.Text = "Generate";
            this.button_Generate_Data.UseVisualStyleBackColor = true;
            this.button_Generate_Data.Click += new System.EventHandler(this.button_Generate_Data_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(139, 142);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(211, 53);
            this.textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 248);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_Generate_Data);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Generate_Data;
        private System.Windows.Forms.TextBox textBox1;
    }
}

