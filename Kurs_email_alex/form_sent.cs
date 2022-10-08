using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs_email_alex
{
	public partial class form_sent : Form
	{
		int tab = 0;

		int sms_sent = 0;
		public int flag_foger, UID_del_sms;
		Protokol prot;
		StructureSMS item;
		string part = "";

		public form_sent(Protokol _prot, StructureSMS _item)
		{
			prot = _prot;
			item = _item;
			InitializeComponent();
		}

		private void form_sent_Load(object sender, EventArgs e)
		{
			this.Width = 605;

			switch (flag_foger)
			{
				case 0:
					sms_sent++;
					txt_from.Text = item.from_sms.ToString();
					to_txt.Text = item.to_sms.ToString();
					tem_txt.Text = item.Subjeck.ToString();
					body_tx.Text = item.Body.ToString();
					if (item.Attachments != "0")
						bt_add_file.Text = "сохнаить вложение";
					else
					{
						bt_add_file.Visible = false;
						label4.Text = "Вложений нет в смс";
					}
					this.Text = "Просмотр смс";
					bt_sent.Visible = false;
					to_txt.Enabled = false;
					bt_generator_key_dsa.Visible = false;
					bt_coding_panel.Location = new Point(504, 415);
					bt_code.Visible = false;
					bt_load_param.Dock = DockStyle.Bottom;
					button1.Dock = DockStyle.Bottom;
					bt_coding_panel.Text = "Расскодировать";
					bt_pen_input.Visible = true;
					break;
				case 1:
					sms_sent++;
					txt_from.Text = item.from_sms.ToString();
					to_txt.Text = item.to_sms.ToString();
					tem_txt.Text = item.Subjeck.ToString();
					body_tx.Text = item.Body.ToString();
					if (item.Attachments != "0")
						bt_add_file.Text = "сохнаить вложение";
					else
					{
						bt_add_file.Visible = false;
						label4.Text = "Вложений нет в смс";
					}
					this.Text = "Просмотр смс";
					bt_sent.Visible = false;
					txt_from.Enabled = false;
					bt_generator_key_dsa.Visible = false;
					bt_coding_panel.Location = new Point(504, 415);
					bt_code.Visible = false;
					bt_load_param.Dock = DockStyle.Bottom;
					button1.Dock = DockStyle.Bottom;
					bt_coding_panel.Text = "Расскодировать";
					bt_pen_input.Visible = true;
					break;
				case 2:
					//sms_sent++;
					txt_from.Text = item.from_sms.ToString();
					to_txt.Text = item.to_sms.ToString();
					tem_txt.Text = item.Subjeck.ToString();
					body_tx.Text = item.Body.ToString();
					this.Text = "Просмотр черновика";
					//bt_sent.Visible = false;
					txt_from.Enabled = false;
					bt_load_param.Visible = false;
					button1.Visible = false;
					bt_load_param.Visible = false;

					bt_code.Dock = DockStyle.Bottom;
					//button1.Dock = DockStyle.Bottom;
					bt_generator_key_dsa.Dock = DockStyle.Bottom;

					break;
				case 3:
					sms_sent++;
					txt_from.Text = item.from_sms.ToString();
					to_txt.Text = item.to_sms.ToString();
					tem_txt.Text = item.Subjeck.ToString();
					body_tx.Text = item.Body.ToString();
					this.Text = "Просмотр удаленного смс";
					if (item.Attachments != "0")
						bt_add_file.Text = "сохнаить вложение";
					else
					{
						bt_add_file.Visible = false;
						label4.Text = "Вложений нет в смс";
					}
					bt_sent.Visible = false;
					txt_from.Enabled = false;
					to_txt.Enabled = false;
					tem_txt.Enabled = false;
					body_tx.Enabled = false;
					bt_code.Location = new Point(458, 409);
					break;
				case 999:
					txt_from.Text = prot.UserEmal.ToString();
					txt_from.Enabled = false;
					button1.Visible = false;
					bt_load_param.Visible = false;
					bt_code.Dock = DockStyle.Bottom;
					break;
			}

		}

		private void bt_sent_Click(object sender, EventArgs e)
		{
			StructureSMS sms = new StructureSMS();
			sms.to_sms = to_txt.Text.ToString();
			sms.Subjeck = tem_txt.Text.ToString();
			sms.Body = body_tx.Text.ToString();
			sms.Attachments = part.ToString();

			if (ACT.Sent_sms(sms, prot))
			{
				MessageBox.Show("Сообщение отправлено");
				sms_sent++;
				if (flag_foger == 2)
				{
					ACT.DELETE_sms(UID_del_sms, flag_foger, prot);
				}
				Close();
			}
			else
				MessageBox.Show("Ошибка при отправлении!!!");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				if (coding_sms.Convert_byte_to_sms(body_tx.Text) != "")
				{
					List<struct_sms_coding> ff = coding_sms.decode_sms(coding_sms.Convert_byte_to_sms(body_tx.Text));
					//string key = "alex";
					int D, N;
					D = Convert.ToInt32(d_skech_rsa_txt.Text);
					N = Convert.ToInt32(n_skech_rsa_txt.Text);
					string reee = coding_sms.RSA.Decipher(D, N, ff.ElementAt(0).rsa_code);
					body_tx.Text = coding_sms.DEC.Decipher(ff.ElementAt(0).txt_sms.ToString(), reee).ToString().Replace("#", "");

					int p, q, g, x, y;
					p = Convert.ToInt32(textBoxP.Text);
					q = Convert.ToInt32(textBoxQ.Text);
					y = Convert.ToInt32(textBoxD.Text);

					string[] data = textBoxN.Text.Split(new Char[] { ' ' });
					x = Convert.ToInt32(data[0]);
					g = Convert.ToInt32(data[1]);

					string hash = coding_sms.SHA1.GET_HACH(body_tx.Text);
					string ESD = coding_sms.DSA.Encode(hash, p, q, g, x);
					//MessageBox.Show(ESD);
					if (coding_sms.DSA.DecodeAndEqual(hash, p, q, y, g, ESD))
					{
						this.Width = 605;
						MessageBox.Show("Файл подлинный");
					}
				}
			}
			catch (Exception) {
				MessageBox.Show("Не введены данные для декодинга");
			}
		}

		private void bt_add_file_Click(object sender, EventArgs e)
		{
			if (item != null)
			{
				if (item.Attachments != "0")
				{
					FolderBrowserDialog FBD = new FolderBrowserDialog();

					foreach (var attachment in item.Attachments_file)
					{
						var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
						if (fileName != "")
						{
							if (FBD.ShowDialog() == DialogResult.OK)
							{
								//client_file = FBD.FileName;
							}

							using (var stream = File.Create(Path.Combine(FBD.SelectedPath, fileName)))
							{
								if (attachment is MessagePart)
								{
									var part = (MessagePart)attachment;
									part.Message.WriteTo(stream);
								}
								else
								{
									var part = (MimePart)attachment;
									part.Content.DecodeTo(stream);
								}
							}
						}
					}
				}
			}
			else
			{

				if (open.ShowDialog() == DialogResult.OK)
				{
					string p = open.FileName;
					part = p.Replace('\\', '/');
					//MessageBox.Show(part.ToString());
				}
			}
		}

		private void bt_code_Click(object sender, EventArgs e)
		{
			try
			{
				if (body_tx.Text.Length != 0)
				{
					if (key_dec_txt.Text.Length != 0)
					{
						int p_rsa = Convert.ToInt32(p_rsa_txt.Text.Length);
						int q_rsa = Convert.ToInt32(q_rsa_txt.Text.Length);
						if ((p_rsa > 0) && (q_rsa > 0))
						{
							string txt = body_tx.Text.ToString();
							string key = key_dec_txt.Text.ToString();
							string s2w = coding_sms.DEC.to_str_key_decode(txt, key);
							string s = coding_sms.DEC.Encrypt(txt, key);

							int _p = Convert.ToInt32(p_rsa_txt.Text);
							int _q = Convert.ToInt32(q_rsa_txt.Text);
							if (coding_sms.RSA.IsTheNumberSimple(_p) && coding_sms.RSA.IsTheNumberSimple(_q))
							{

								long nq = _p * _q;
								long m = (_p - 1) * (_q - 1);
								long d = coding_sms.RSA.Calculate_d(m);
								long e_ = coding_sms.RSA.Calculate_e(d, m);

								d_skech_rsa_txt.Text = d.ToString();
								n_skech_rsa_txt.Text = nq.ToString();

								List<string> n = coding_sms.RSA.Encrypt(Convert.ToInt32(p_rsa_txt.Text), Convert.ToInt32(q_rsa_txt.Text), s2w);

								string hash = coding_sms.SHA1.GET_HACH(txt);

								if (Convert.ToInt32(textBoxP.Text.Length) > 0)
								{
									int p, q, g, x, y;
									p = Convert.ToInt32(textBoxP.Text);
									q = Convert.ToInt32(textBoxQ.Text);
									y = Convert.ToInt32(textBoxD.Text);


									string[] data = textBoxN.Text.Split(new Char[] { ' ' });
									x = Convert.ToInt32(data[0]);
									g = Convert.ToInt32(data[1]);

									string ESD = coding_sms.DSA.Encode(hash, p, q, g, x);

									struct_sms_coding qq = new struct_sms_coding();
									qq.txt_sms = s;
									qq.rsa_code = n;
									qq.dsa_metka = ESD;
									List<struct_sms_coding> qwe = new List<struct_sms_coding>();
									qwe.Add(qq);
									string result_txt = coding_sms.Convert_sms_to_byte(coding_sms.create_file_coding(qwe));
									if (result_txt != "")
									{
										body_tx.Text = result_txt;//coding_sms.Convert_sms_to_byte(coding_sms.create_file_coding(qwe));


										DialogResult result = MessageBox.Show(
											"Хотите ли сохранить ключи кодировки ?",
												"Сообщение",
												MessageBoxButtons.YesNo,
												MessageBoxIcon.Information,
												MessageBoxDefaultButton.Button1);
										if (result == DialogResult.Yes)
										{
											save_param prm = new save_param();
											prm.rsa_p = Convert.ToInt32(p_rsa_txt.Text);
											prm.rsa_q = Convert.ToInt32(q_rsa_txt.Text);//
											prm.rsa_d = Convert.ToInt32(d_skech_rsa_txt.Text);
											prm.rsa_n = Convert.ToInt32(n_skech_rsa_txt.Text);
											prm.dsa_p = Convert.ToInt32(textBoxP.Text);
											prm.dsa_q = Convert.ToInt32(textBoxQ.Text);
											prm.dsa_n = Convert.ToString(textBoxN.Text);
											prm.dsa_d = Convert.ToInt32(textBoxD.Text);
											if (coding_sms.save_param_coding(prm))
												MessageBox.Show("Файл сохранился");
											else
												MessageBox.Show("Файл НЕ сохранился");
										}
										this.Width = 605;
										MessageBox.Show("Сообжение успешно закодировано");
										picture_table.Visible = false;
										tab--;
									}
									else
									{
										MessageBox.Show("Не удалось закодирвоать сообщение!!! Не подходит Code_dec");
									}
								}
								else
								{
									MessageBox.Show("Вы не сгенерировали ключи DSA");
								}
							}
							else
							{
								MessageBox.Show("p или q - не простые числа!");
							}
						}
						else
						{
							MessageBox.Show("Введите p и q!");
						}
					}
					else
					{
						MessageBox.Show("Вы не ввели Key_dec");
					}
				}
				else
				{
					MessageBox.Show("Нет тело сообщение!!! По этому кидировать нечего!!! Заполните тело");
				}
			}
			catch (Exception eror)
			{
				MessageBox.Show("Ошибка в кодировании!!! \n" + eror.Message.ToString());
			}
		}
		private void bt_generator_key_dsa_Click(object sender, EventArgs e)
		{
			if (body_tx.Text.Length != 0)
			{
				int p, q, x, y;
				double g;
				q = Convert.ToInt32(textBoxQ.Text);
				try
				{
					if (coding_sms.DSA.IsTheNumberSimple(q)) // P is simple number?
					{
						textBoxP.Visible = true;
						labelP.Visible = true;
						textBoxD.Visible = true;
						labelD.Visible = true;
						textBoxN.Visible = true;
						labelN.Visible = true;

						string hash = coding_sms.SHA1.GET_HACH(body_tx.Text.ToString());

						Random rand = new Random();

						x = rand.Next(0, q);

						p = coding_sms.DSA.CalculateP(q);
						g = coding_sms.DSA.CalculateG(p, q);
						y = coding_sms.DSA.CalculateY(p, g, x);

						string ESD = coding_sms.DSA.Encode(hash, p, q, g, x);
						bool result = coding_sms.DSA.DecodeAndEqual(hash, p, q, y, g, ESD);
						if (!result) // if keys gives wrong results
						{
							throw new Exception();
						}
						textBoxP.Text = p.ToString();
						textBoxD.Text = y.ToString();
						textBoxN.Text = x.ToString() + " " + g.ToString();
					}

					else // Q is not simple number
					{
						MessageBox.Show("Q не простое число(DSA)!!!", "Warning!");
						textBoxP.Text = "";
						textBoxQ.Text = "";
						textBoxD.Text = "";
						textBoxN.Text = "";
					}
				}
				catch (Exception exception)
				{
					bt_generator_key_dsa.PerformClick(); // generate new ESD
					Console.WriteLine(exception + "\n\n" + exception.Message);
				}
			}
			else
			{
				MessageBox.Show("Нет тела сообщение!!! Пока не возможно генерация ключей");
			}
		}

		private void bt_coding_panel_Click(object sender, EventArgs e)
		{
			this.Width = 984;
		}

		private void bt_load_param_Click(object sender, EventArgs e)
		{
			try
			{
				save_param prm = coding_sms.load_param_coding();
				p_rsa_txt.Text = Convert.ToString(prm.rsa_p);
				q_rsa_txt.Text = Convert.ToString(prm.rsa_q);
				d_skech_rsa_txt.Text = Convert.ToString(prm.rsa_d);
				n_skech_rsa_txt.Text = Convert.ToString(prm.rsa_n);
				textBoxP.Text = Convert.ToString(prm.dsa_p);
				textBoxQ.Text = Convert.ToString(prm.dsa_q);
				textBoxN.Text = Convert.ToString(prm.dsa_n);
				textBoxD.Text = Convert.ToString(prm.dsa_d);
			}
			catch { }
		}

		private void bt_pen_input_Click(object sender, EventArgs e)
		{
			textBoxP.Enabled = true;
			textBoxQ.Enabled = true;
			textBoxN.Enabled = true;
			textBoxD.Enabled = true;
			d_skech_rsa_txt.Enabled = true;
			n_skech_rsa_txt.Enabled = true;
			p_rsa_txt.Enabled = true;
			q_rsa_txt.Enabled = true;
		}

		private void p_rsa_txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void q_rsa_txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void d_skech_rsa_txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void n_skech_rsa_txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void textBoxP_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void textBoxQ_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void textBoxD_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void textBoxN_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!Char.IsDigit(number) && number != 8 && !char.IsWhiteSpace(number)) // цифры и клавиша BackSpace
			{
				e.Handled = true;
			}
		}

		private void table_singl_num_Click(object sender, EventArgs e)
		{
			if (tab == 0)
			{
				picture_table.Visible = true;
				tab++;
			}
			else
			{
				picture_table.Visible = false;
				tab--;
			}
		}

		private void form_sent_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (sms_sent == 0)
			{
				StructureSMS sms = new StructureSMS();
				sms.to_sms = to_txt.Text.ToString();
				sms.Subjeck = tem_txt.Text.ToString();
				sms.Body = body_tx.Text.ToString();
				sms.Attachments = part.ToString();
				ACT.DELETE_sms(UID_del_sms, flag_foger, prot);
				if (ACT.add_sms_draft(prot, sms))
				{
					MessageBox.Show("Сообщение сохранено в черновик");
					Close();
				}
			}
		}
	}
}
