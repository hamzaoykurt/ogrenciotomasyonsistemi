namespace denemestaj
{
    partial class Form3
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
            this.girisyapyon = new System.Windows.Forms.Button();
            this.kayitol = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sifrekayit = new System.Windows.Forms.TextBox();
            this.ogrenciadikayit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // girisyapyon
            // 
            this.girisyapyon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.girisyapyon.Location = new System.Drawing.Point(231, 394);
            this.girisyapyon.Name = "girisyapyon";
            this.girisyapyon.Size = new System.Drawing.Size(84, 30);
            this.girisyapyon.TabIndex = 0;
            this.girisyapyon.Text = "GIRIS YAP";
            this.girisyapyon.UseVisualStyleBackColor = true;
            this.girisyapyon.Click += new System.EventHandler(this.girisyapyon_Click);
            // 
            // kayitol
            // 
            this.kayitol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kayitol.Location = new System.Drawing.Point(133, 310);
            this.kayitol.Name = "kayitol";
            this.kayitol.Size = new System.Drawing.Size(161, 57);
            this.kayitol.TabIndex = 1;
            this.kayitol.Text = "KAYIT OL";
            this.kayitol.UseVisualStyleBackColor = true;
            this.kayitol.Click += new System.EventHandler(this.kayitol_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(130, 198);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "SIFRE OLUSTUR:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(130, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "YENI OGRENCI ADI:";
            // 
            // sifrekayit
            // 
            this.sifrekayit.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.sifrekayit.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sifrekayit.ForeColor = System.Drawing.Color.Black;
            this.sifrekayit.Location = new System.Drawing.Point(133, 225);
            this.sifrekayit.Margin = new System.Windows.Forms.Padding(10);
            this.sifrekayit.Multiline = true;
            this.sifrekayit.Name = "sifrekayit";
            this.sifrekayit.Size = new System.Drawing.Size(161, 29);
            this.sifrekayit.TabIndex = 10;
            this.sifrekayit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sifrekayit_KeyDown);
            // 
            // ogrenciadikayit
            // 
            this.ogrenciadikayit.Font = new System.Drawing.Font("Tw Cen MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ogrenciadikayit.Location = new System.Drawing.Point(133, 151);
            this.ogrenciadikayit.Margin = new System.Windows.Forms.Padding(7);
            this.ogrenciadikayit.Multiline = true;
            this.ogrenciadikayit.Name = "ogrenciadikayit";
            this.ogrenciadikayit.Size = new System.Drawing.Size(161, 28);
            this.ogrenciadikayit.TabIndex = 9;
            this.ogrenciadikayit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ogrenciadikayit_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(107, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hesabın var mı?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Perpetua Titling MT", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(115, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 44);
            this.label4.TabIndex = 16;
            this.label4.Text = "KAYIT OL";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(435, 451);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sifrekayit);
            this.Controls.Add(this.ogrenciadikayit);
            this.Controls.Add(this.kayitol);
            this.Controls.Add(this.girisyapyon);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button girisyapyon;
        private System.Windows.Forms.Button kayitol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sifrekayit;
        private System.Windows.Forms.TextBox ogrenciadikayit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}