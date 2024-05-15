namespace deneme2
{
    partial class Form6
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
            this.txtBoxEngWordName = new System.Windows.Forms.TextBox();
            this.txtBoxTrWordName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.resimSec = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxWordExample = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBoxEngWordName
            // 
            this.txtBoxEngWordName.Location = new System.Drawing.Point(343, 177);
            this.txtBoxEngWordName.Name = "txtBoxEngWordName";
            this.txtBoxEngWordName.Size = new System.Drawing.Size(147, 22);
            this.txtBoxEngWordName.TabIndex = 1;
            // 
            // txtBoxTrWordName
            // 
            this.txtBoxTrWordName.Location = new System.Drawing.Point(343, 245);
            this.txtBoxTrWordName.Name = "txtBoxTrWordName";
            this.txtBoxTrWordName.Size = new System.Drawing.Size(147, 22);
            this.txtBoxTrWordName.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(51, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(439, 55);
            this.button1.TabIndex = 4;
            this.button1.Text = "Kelimeyi Ekle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(47, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kelimenin Türkçe halini giriniz:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(47, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Kelimenin İngilizce halini giriniz:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(496, 177);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(405, 283);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(51, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(439, 55);
            this.button2.TabIndex = 3;
            this.button2.Text = "Resim Ekle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.label3.Location = new System.Drawing.Point(276, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(423, 58);
            this.label3.TabIndex = 8;
            this.label3.Text = "KELİME EKLEME";
            // 
            // resimSec
            // 
            this.resimSec.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label4.Location = new System.Drawing.Point(47, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(266, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Kelimenin Türkçe halini giriniz:";
            // 
            // txtBoxWordExample
            // 
            this.txtBoxWordExample.Location = new System.Drawing.Point(343, 297);
            this.txtBoxWordExample.Name = "txtBoxWordExample";
            this.txtBoxWordExample.Size = new System.Drawing.Size(147, 22);
            this.txtBoxWordExample.TabIndex = 9;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 472);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBoxWordExample);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBoxTrWordName);
            this.Controls.Add(this.txtBoxEngWordName);
            this.Name = "Form6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form6";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxEngWordName;
        private System.Windows.Forms.TextBox txtBoxTrWordName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog resimSec;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxWordExample;
    }
}