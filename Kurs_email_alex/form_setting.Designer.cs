
namespace Kurs_email_alex
{
	partial class form_setting
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txt_host = new System.Windows.Forms.TextBox();
			this.txt_port_smtp = new System.Windows.Forms.TextBox();
			this.txt_port_imap = new System.Windows.Forms.TextBox();
			this.check_ssl = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.txt_port_smtp_pop = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Почтовый хотс";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Порт SMTP";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(12, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 17);
			this.label4.TabIndex = 3;
			this.label4.Text = "Порт IMAP";
			// 
			// txt_host
			// 
			this.txt_host.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txt_host.Location = new System.Drawing.Point(12, 29);
			this.txt_host.MaxLength = 15;
			this.txt_host.Name = "txt_host";
			this.txt_host.Size = new System.Drawing.Size(231, 23);
			this.txt_host.TabIndex = 4;
			// 
			// txt_port_smtp
			// 
			this.txt_port_smtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txt_port_smtp.Location = new System.Drawing.Point(101, 58);
			this.txt_port_smtp.MaxLength = 5;
			this.txt_port_smtp.Name = "txt_port_smtp";
			this.txt_port_smtp.Size = new System.Drawing.Size(142, 23);
			this.txt_port_smtp.TabIndex = 5;
			// 
			// txt_port_imap
			// 
			this.txt_port_imap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txt_port_imap.Location = new System.Drawing.Point(101, 87);
			this.txt_port_imap.MaxLength = 5;
			this.txt_port_imap.Name = "txt_port_imap";
			this.txt_port_imap.Size = new System.Drawing.Size(142, 23);
			this.txt_port_imap.TabIndex = 6;
			// 
			// check_ssl
			// 
			this.check_ssl.AutoSize = true;
			this.check_ssl.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.check_ssl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.check_ssl.Location = new System.Drawing.Point(12, 143);
			this.check_ssl.Name = "check_ssl";
			this.check_ssl.Size = new System.Drawing.Size(83, 21);
			this.check_ssl.TabIndex = 7;
			this.check_ssl.Text = "TLS/SSL";
			this.check_ssl.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.Location = new System.Drawing.Point(124, 170);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(119, 30);
			this.button1.TabIndex = 8;
			this.button1.Text = "Отмена";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.Location = new System.Drawing.Point(12, 170);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(106, 30);
			this.button2.TabIndex = 9;
			this.button2.Text = "Применить";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// txt_port_smtp_pop
			// 
			this.txt_port_smtp_pop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txt_port_smtp_pop.Location = new System.Drawing.Point(101, 116);
			this.txt_port_smtp_pop.MaxLength = 5;
			this.txt_port_smtp_pop.Name = "txt_port_smtp_pop";
			this.txt_port_smtp_pop.Size = new System.Drawing.Size(142, 23);
			this.txt_port_smtp_pop.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(12, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 17);
			this.label3.TabIndex = 10;
			this.label3.Text = "Порт POP ";
			// 
			// form_setting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(255, 210);
			this.Controls.Add(this.txt_port_smtp_pop);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.check_ssl);
			this.Controls.Add(this.txt_port_imap);
			this.Controls.Add(this.txt_port_smtp);
			this.Controls.Add(this.txt_host);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "form_setting";
			this.Text = "Настройка протокола";
			this.Load += new System.EventHandler(this.form_setting_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_host;
		private System.Windows.Forms.TextBox txt_port_smtp;
		private System.Windows.Forms.TextBox txt_port_imap;
		private System.Windows.Forms.CheckBox check_ssl;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txt_port_smtp_pop;
		private System.Windows.Forms.Label label3;
	}
}