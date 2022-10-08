
namespace Kurs_email_alex
{
	partial class form_sent
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
			this.bt_sent = new System.Windows.Forms.Button();
			this.bt_add_file = new System.Windows.Forms.Button();
			this.body_tx = new System.Windows.Forms.RichTextBox();
			this.tem_txt = new System.Windows.Forms.TextBox();
			this.to_txt = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.open = new System.Windows.Forms.OpenFileDialog();
			this.bt_code = new System.Windows.Forms.Button();
			this.txt_from = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// bt_sent
			// 
			this.bt_sent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bt_sent.Location = new System.Drawing.Point(488, 409);
			this.bt_sent.Name = "bt_sent";
			this.bt_sent.Size = new System.Drawing.Size(91, 34);
			this.bt_sent.TabIndex = 17;
			this.bt_sent.Text = "Отправить";
			this.bt_sent.UseVisualStyleBackColor = true;
			this.bt_sent.Click += new System.EventHandler(this.bt_sent_Click);
			// 
			// bt_add_file
			// 
			this.bt_add_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bt_add_file.Location = new System.Drawing.Point(91, 411);
			this.bt_add_file.Name = "bt_add_file";
			this.bt_add_file.Size = new System.Drawing.Size(135, 23);
			this.bt_add_file.TabIndex = 16;
			this.bt_add_file.Text = "Добавить вложение";
			this.bt_add_file.UseVisualStyleBackColor = true;
			this.bt_add_file.Click += new System.EventHandler(this.bt_add_file_Click);
			// 
			// body_tx
			// 
			this.body_tx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.body_tx.Location = new System.Drawing.Point(7, 137);
			this.body_tx.Name = "body_tx";
			this.body_tx.Size = new System.Drawing.Size(572, 266);
			this.body_tx.TabIndex = 15;
			this.body_tx.Text = "";
			// 
			// tem_txt
			// 
			this.tem_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tem_txt.Location = new System.Drawing.Point(134, 79);
			this.tem_txt.Name = "tem_txt";
			this.tem_txt.Size = new System.Drawing.Size(445, 23);
			this.tem_txt.TabIndex = 14;
			// 
			// to_txt
			// 
			this.to_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.to_txt.Location = new System.Drawing.Point(134, 50);
			this.to_txt.Name = "to_txt";
			this.to_txt.Size = new System.Drawing.Size(445, 23);
			this.to_txt.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(11, 414);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 17);
			this.label4.TabIndex = 12;
			this.label4.Text = "Вложение";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(4, 110);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 17);
			this.label3.TabIndex = 11;
			this.label3.Text = "Сообщение";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 17);
			this.label2.TabIndex = 10;
			this.label2.Text = "Получатель:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(4, 82);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 17);
			this.label1.TabIndex = 9;
			this.label1.Text = "Тема сообщения:";
			// 
			// open
			// 
			this.open.FileName = "openFileDialog1";
			// 
			// bt_code
			// 
			this.bt_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bt_code.Location = new System.Drawing.Point(361, 409);
			this.bt_code.Name = "bt_code";
			this.bt_code.Size = new System.Drawing.Size(121, 34);
			this.bt_code.TabIndex = 18;
			this.bt_code.Text = "Закодировать";
			this.bt_code.UseVisualStyleBackColor = true;
			this.bt_code.Click += new System.EventHandler(this.bt_code_Click);
			// 
			// txt_from
			// 
			this.txt_from.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txt_from.Location = new System.Drawing.Point(134, 12);
			this.txt_from.Name = "txt_from";
			this.txt_from.Size = new System.Drawing.Size(445, 23);
			this.txt_from.TabIndex = 20;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(12, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 17);
			this.label5.TabIndex = 19;
			this.label5.Text = "Отправитель:";
			// 
			// form_sent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(591, 450);
			this.Controls.Add(this.txt_from);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.bt_code);
			this.Controls.Add(this.bt_sent);
			this.Controls.Add(this.bt_add_file);
			this.Controls.Add(this.body_tx);
			this.Controls.Add(this.tem_txt);
			this.Controls.Add(this.to_txt);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "form_sent";
			this.Text = "Отправить сообщение";
			this.Load += new System.EventHandler(this.form_sent_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bt_sent;
		private System.Windows.Forms.Button bt_add_file;
		private System.Windows.Forms.RichTextBox body_tx;
		private System.Windows.Forms.TextBox tem_txt;
		private System.Windows.Forms.TextBox to_txt;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog open;
		private System.Windows.Forms.Button bt_code;
		private System.Windows.Forms.TextBox txt_from;
		private System.Windows.Forms.Label label5;
	}
}