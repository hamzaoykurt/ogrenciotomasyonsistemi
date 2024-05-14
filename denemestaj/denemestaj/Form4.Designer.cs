namespace denemestaj
{
    partial class Form4
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
            this.yeniderseklebaslik = new System.Windows.Forms.Label();
            this.yenidersadi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zorunluders = new System.Windows.Forms.CheckBox();
            this.yenidersekle = new System.Windows.Forms.Button();
            this.ogrencisecim = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.secilenders = new System.Windows.Forms.CheckedListBox();
            this.derslerikaydet = new System.Windows.Forms.Button();
            this.derslercomboBox = new System.Windows.Forms.ComboBox();
            this.derslerlbl = new System.Windows.Forms.Label();
            this.seciliderssil = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // yeniderseklebaslik
            // 
            this.yeniderseklebaslik.AutoSize = true;
            this.yeniderseklebaslik.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.yeniderseklebaslik.ForeColor = System.Drawing.Color.White;
            this.yeniderseklebaslik.Location = new System.Drawing.Point(624, 378);
            this.yeniderseklebaslik.Name = "yeniderseklebaslik";
            this.yeniderseklebaslik.Size = new System.Drawing.Size(94, 18);
            this.yeniderseklebaslik.TabIndex = 0;
            this.yeniderseklebaslik.Text = "Yeni Ders Adı:";
            // 
            // yenidersadi
            // 
            this.yenidersadi.Location = new System.Drawing.Point(627, 399);
            this.yenidersadi.Multiline = true;
            this.yenidersadi.Name = "yenidersadi";
            this.yenidersadi.Size = new System.Drawing.Size(115, 21);
            this.yenidersadi.TabIndex = 1;
            this.yenidersadi.TextChanged += new System.EventHandler(this.yenidersadi_TextChanged);
            this.yenidersadi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.yenidersadi_KeyDown);
            this.yenidersadi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.yenidersadi_KeyPress);
            this.yenidersadi.MouseDown += new System.Windows.Forms.MouseEventHandler(this.yenidersadi_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Perpetua Titling MT", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(89, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(324, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "DERS SEÇME, SEÇİM KALDIRMA";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // zorunluders
            // 
            this.zorunluders.AutoSize = true;
            this.zorunluders.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.zorunluders.ForeColor = System.Drawing.Color.White;
            this.zorunluders.Location = new System.Drawing.Point(31, 453);
            this.zorunluders.Name = "zorunluders";
            this.zorunluders.Size = new System.Drawing.Size(94, 17);
            this.zorunluders.TabIndex = 5;
            this.zorunluders.Text = "Zorunlu Ders";
            this.zorunluders.UseVisualStyleBackColor = true;
            // 
            // yenidersekle
            // 
            this.yenidersekle.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.yenidersekle.Location = new System.Drawing.Point(180, 420);
            this.yenidersekle.Name = "yenidersekle";
            this.yenidersekle.Size = new System.Drawing.Size(142, 50);
            this.yenidersekle.TabIndex = 6;
            this.yenidersekle.Text = "Yeni Ekle / Zorunluluğu Değiş";
            this.yenidersekle.UseVisualStyleBackColor = true;
            this.yenidersekle.Click += new System.EventHandler(this.yenidersekle_Click);
            // 
            // ogrencisecim
            // 
            this.ogrencisecim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ogrencisecim.FormattingEnabled = true;
            this.ogrencisecim.Location = new System.Drawing.Point(124, 140);
            this.ogrencisecim.Name = "ogrencisecim";
            this.ogrencisecim.Size = new System.Drawing.Size(120, 21);
            this.ogrencisecim.TabIndex = 7;
            this.ogrencisecim.SelectedIndexChanged += new System.EventHandler(this.ogrencisecim_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(121, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Öğrenci Adi:";
            // 
            // secilenders
            // 
            this.secilenders.BackColor = System.Drawing.SystemColors.Highlight;
            this.secilenders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secilenders.CheckOnClick = true;
            this.secilenders.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secilenders.ForeColor = System.Drawing.SystemColors.Window;
            this.secilenders.FormattingEnabled = true;
            this.secilenders.Location = new System.Drawing.Point(124, 204);
            this.secilenders.MultiColumn = true;
            this.secilenders.Name = "secilenders";
            this.secilenders.Size = new System.Drawing.Size(244, 152);
            this.secilenders.TabIndex = 9;
            this.secilenders.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.secilenders_ItemCheck);
            this.secilenders.SelectedIndexChanged += new System.EventHandler(this.secilenders_SelectedIndexChanged);
            // 
            // derslerikaydet
            // 
            this.derslerikaydet.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.derslerikaydet.Location = new System.Drawing.Point(263, 103);
            this.derslerikaydet.Name = "derslerikaydet";
            this.derslerikaydet.Size = new System.Drawing.Size(105, 67);
            this.derslerikaydet.TabIndex = 10;
            this.derslerikaydet.Text = "DERSLERİ KAYDET";
            this.derslerikaydet.UseVisualStyleBackColor = true;
            this.derslerikaydet.Click += new System.EventHandler(this.derslerikaydet_Click);
            // 
            // derslercomboBox
            // 
            this.derslercomboBox.FormattingEnabled = true;
            this.derslercomboBox.Location = new System.Drawing.Point(31, 420);
            this.derslercomboBox.Name = "derslercomboBox";
            this.derslercomboBox.Size = new System.Drawing.Size(110, 21);
            this.derslercomboBox.TabIndex = 12;
            this.derslercomboBox.SelectedIndexChanged += new System.EventHandler(this.derslercomboBox_SelectedIndexChanged);
            this.derslercomboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.derslercomboBox_KeyPress);
            // 
            // derslerlbl
            // 
            this.derslerlbl.AutoSize = true;
            this.derslerlbl.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.derslerlbl.ForeColor = System.Drawing.Color.White;
            this.derslerlbl.Location = new System.Drawing.Point(28, 399);
            this.derslerlbl.Name = "derslerlbl";
            this.derslerlbl.Size = new System.Drawing.Size(57, 18);
            this.derslerlbl.TabIndex = 13;
            this.derslerlbl.Text = "Dersler:";
            // 
            // seciliderssil
            // 
            this.seciliderssil.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.seciliderssil.Location = new System.Drawing.Point(350, 420);
            this.seciliderssil.Name = "seciliderssil";
            this.seciliderssil.Size = new System.Drawing.Size(110, 50);
            this.seciliderssil.TabIndex = 16;
            this.seciliderssil.Text = "DERSİ SİL";
            this.seciliderssil.UseVisualStyleBackColor = true;
            this.seciliderssil.Click += new System.EventHandler(this.seciliderssil_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(486, 494);
            this.Controls.Add(this.seciliderssil);
            this.Controls.Add(this.derslerlbl);
            this.Controls.Add(this.derslercomboBox);
            this.Controls.Add(this.derslerikaydet);
            this.Controls.Add(this.secilenders);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ogrencisecim);
            this.Controls.Add(this.yenidersekle);
            this.Controls.Add(this.zorunluders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.yenidersadi);
            this.Controls.Add(this.yeniderseklebaslik);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label yeniderseklebaslik;
        private System.Windows.Forms.TextBox yenidersadi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox zorunluders;
        private System.Windows.Forms.Button yenidersekle;
        private System.Windows.Forms.ComboBox ogrencisecim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox secilenders;
        private System.Windows.Forms.Button derslerikaydet;
        private System.Windows.Forms.ComboBox derslercomboBox;
        private System.Windows.Forms.Label derslerlbl;
        private System.Windows.Forms.Button seciliderssil;
    }
}