
namespace Kurs_email_alex
{
	partial class users
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
			this.servis_combo = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.login_txt = new System.Windows.Forms.TextBox();
			this.pass_txt = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.user_txt = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "ЛОГИН";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "ПАРОЛЬ";
			// 
			// servis_combo
			// 
			this.servis_combo.FormattingEnabled = true;
			this.servis_combo.Items.AddRange(new object[] {
            "gmail.com",
            "yandex.ru",
            "mail.ru"});
			this.servis_combo.Location = new System.Drawing.Point(97, 15);
			this.servis_combo.Name = "servis_combo";
			this.servis_combo.Size = new System.Drawing.Size(139, 21);
			this.servis_combo.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "КЛИЕНТ";
			// 
			// login_txt
			// 
			this.login_txt.Location = new System.Drawing.Point(70, 65);
			this.login_txt.Name = "login_txt";
			this.login_txt.Size = new System.Drawing.Size(166, 20);
			this.login_txt.TabIndex = 4;
			// 
			// pass_txt
			// 
			this.pass_txt.Location = new System.Drawing.Point(70, 97);
			this.pass_txt.Name = "pass_txt";
			this.pass_txt.Size = new System.Drawing.Size(166, 20);
			this.pass_txt.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.Location = new System.Drawing.Point(15, 120);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(221, 32);
			this.button1.TabIndex = 6;
			this.button1.Text = "Подключиться";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Ник для проги";
			// 
			// user_txt
			// 
			this.user_txt.Location = new System.Drawing.Point(98, 40);
			this.user_txt.Name = "user_txt";
			this.user_txt.Size = new System.Drawing.Size(138, 20);
			this.user_txt.TabIndex = 8;
			// 
			// users
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(241, 164);
			this.Controls.Add(this.user_txt);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pass_txt);
			this.Controls.Add(this.login_txt);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.servis_combo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "users";
			this.ShowIcon = false;
			this.Text = "Подкл. аккаунт";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox servis_combo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox login_txt;
		private System.Windows.Forms.TextBox pass_txt;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox user_txt;
	}
}