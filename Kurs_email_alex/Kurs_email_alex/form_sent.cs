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
		private const int sizeOfBlock = 128; //в DES размер блока 64 бит, но поскольку в unicode символ в два раза длинее, то увеличим блок тоже в два раза
		private const int sizeOfChar = 16; //размер одного символа (in Unicode 16 bit)
		private const int shiftKey = 2; //сдвиг ключа 
		private const int quantityOfRounds = 16; //количество раундов
		public static string[] Blocks; //сами блоки в двоичном формате

		public static string key_1 = "";


		public int flag_foger;
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
			if (item != null)
			{

			}

			switch (flag_foger)
			{
				case 0:
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
					bt_code.Location = new Point(458, 409);
					break;
				case 1:
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
					bt_code.Location = new Point(458, 409);
					break;
				case 2:
					txt_from.Text = item.from_sms.ToString();
					to_txt.Text = item.to_sms.ToString();
					tem_txt.Text = item.Subjeck.ToString();
					body_tx.Text = item.Body.ToString();
					this.Text = "Просмотр черновика";
					//bt_sent.Visible = false;
					txt_from.Enabled = false;
					//bt_code.Location = new Point(458, 409);
					break;
				case 3:
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
			}
			else
				MessageBox.Show("Ошибка при отправлении!!!");
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
					MessageBox.Show(part.ToString());

				}
			}
		}

		private void bt_code_Click(object sender, EventArgs e)
		{
			string txt = body_tx.Text.ToString();
			string key = "alex";
			//string s2 = Encrypt(txt, key);
			string s2w = coding_sms.DEC.to_str_key_decode(txt, key);
			string s = coding_sms.DEC.Encrypt(txt, key);

			//MessageBox.Show(s2.ToString());
			//MessageBox.Show(key_1.ToString());

			//string ress = coding_sms.DEC.Decipher(s, s2w).ToString().Replace("#", "");
			//MessageBox.Show(Decipher(s2, key_1).ToString());
			//MessageBox.Show(coding_sms.DEC.Decipher(s, s2w).ToString());

			

			List<string> n = coding_sms.RSA.Encrypt(101, 103, s2w);
			string reee = coding_sms.RSA.Decipher(10199,10403, n);
			MessageBox.Show(reee);
			//MessageBox.Show(coding_sms.DEC.Decipher(s, reee).ToString());
			string ress = coding_sms.DEC.Decipher(s, reee).ToString().Replace("#", "");
			if (ress.Equals(txt))
				MessageBox.Show("Равны");
		}





		//доводим строку до размера, чтобы делилась на sizeOfBlock
		private static string StringToRightLength(string input)
		{
			while (((input.Length * sizeOfChar) % sizeOfBlock) != 0)
				input += "#";

			return input;
		}

		//разбиение обычной строки на блоки
		private static void CutStringIntoBlocks(string input)
		{
			Blocks = new string[(input.Length * sizeOfChar) / sizeOfBlock];

			int lengthOfBlock = input.Length / Blocks.Length;

			for (int i = 0; i < Blocks.Length; i++)
			{
				Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
				Blocks[i] = StringToBinaryFormat(Blocks[i]);
			}
		}

		//разбиение двоичной строки на блоки
		private static void CutBinaryStringIntoBlocks(string input)
		{
			Blocks = new string[input.Length / sizeOfBlock];

			int lengthOfBlock = input.Length / Blocks.Length;

			for (int i = 0; i < Blocks.Length; i++)
				Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
		}

		//перевод строки в двоичный формат
		private static string StringToBinaryFormat(string input)
		{
			string output = "";

			for (int i = 0; i < input.Length; i++)
			{
				string char_binary = Convert.ToString(input[i], 2);

				while (char_binary.Length < sizeOfChar)
					char_binary = "0" + char_binary;

				output += char_binary;
			}

			return output;
		}

		//доводим длину ключа до нужной
		private static string CorrectKeyWord(string input, int lengthKey)
		{
			if (input.Length > lengthKey)
				input = input.Substring(0, lengthKey);
			else
				while (input.Length < lengthKey)
					input = "0" + input;

			return input;
		}

		//шифрование DES один раунд
		private static string EncodeDES_One_Round(string input, string key)
		{
			string L = input.Substring(0, input.Length / 2);
			string R = input.Substring(input.Length / 2, input.Length / 2);

			return (R + XOR(L, f(R, key)));
		}

		//расшифровка DES один раунд
		private static string DecodeDES_One_Round(string input, string key)
		{
			string L = input.Substring(0, input.Length / 2);
			string R = input.Substring(input.Length / 2, input.Length / 2);

			return (XOR(f(L, key), R) + L);
		}

		//XOR двух строк с двоичными данными
		private static string XOR(string s1, string s2)
		{
			string result = "";

			for (int i = 0; i < s1.Length; i++)
			{
				bool a = Convert.ToBoolean(Convert.ToInt32(s1[i].ToString()));
				bool b = Convert.ToBoolean(Convert.ToInt32(s2[i].ToString()));

				if (a ^ b)
					result += "1";
				else
					result += "0";
			}
			return result;
		}

		//шифрующая функция f. в данном случае это XOR
		private static string f(string s1, string s2)
		{
			return XOR(s1, s2);
		}

		//вычисление ключа для следующего раунда шифрования. циклический сдвиг >> 2
		private static string KeyToNextRound(string key)
		{
			for (int i = 0; i < shiftKey; i++)
			{
				key = key[key.Length - 1] + key;
				key = key.Remove(key.Length - 1);
			}

			return key;
		}

		//вычисление ключа для следующего раунда расшифровки. циклический сдвиг << 2
		private static string KeyToPrevRound(string key)
		{
			for (int i = 0; i < shiftKey; i++)
			{
				key = key + key[0];
				key = key.Remove(0, 1);
			}

			return key;
		}

		//переводим строку с двоичными данными в символьный формат
		private static string StringFromBinaryToNormalFormat(string input)
		{
			string output = "";

			while (input.Length > 0)
			{
				string char_binary = input.Substring(0, sizeOfChar);
				input = input.Remove(0, sizeOfChar);

				int a = 0;
				int degree = char_binary.Length - 1;

				foreach (char c in char_binary)
					a += Convert.ToInt32(c.ToString()) * (int)Math.Pow(2, degree--);

				output += ((char)a).ToString();
			}

			return output;
		}




		public static string Decipher(string txt_body, string key)
		{
			string result = "";
			if (key.Length > 0)
			{
				string s = "";

				key = StringToBinaryFormat(key);
				s = StringToBinaryFormat(txt_body);

				CutBinaryStringIntoBlocks(s);

				for (int j = 0; j < quantityOfRounds; j++)
				{
					for (int i = 0; i < Blocks.Length; i++)
						Blocks[i] = DecodeDES_One_Round(Blocks[i], key);

					key = KeyToPrevRound(key);
				}

				key = KeyToNextRound(key);

				//textBoxEncodeKeyWord.Text = StringFromBinaryToNormalFormat(key);

				for (int i = 0; i < Blocks.Length; i++)
					result += Blocks[i];

				//treamWriter sw = new StreamWriter("out2.txt");
				result = StringFromBinaryToNormalFormat(result);
			}
			else
				MessageBox.Show("Введите ключевое слово!");
			return result;
		}
		public static string Encrypt(string text, string key)
		{
			string result = "";
			try
			{
				if (key.Length > 0)
				{
					string s = "";
					s = StringToRightLength(text);

					CutStringIntoBlocks(s);

					key = CorrectKeyWord(key, s.Length / (2 * Blocks.Length));
					key = StringToBinaryFormat(key);

					for (int j = 0; j < quantityOfRounds; j++)
					{
						for (int i = 0; i < Blocks.Length; i++)
							Blocks[i] = EncodeDES_One_Round(Blocks[i], key);

						key = KeyToNextRound(key);
					}

					key = KeyToPrevRound(key);

					key_1 = StringFromBinaryToNormalFormat(key);
					for (int i = 0; i < Blocks.Length; i++)
						result += Blocks[i];

					result = StringFromBinaryToNormalFormat(result);
				}
				else
					MessageBox.Show("Введите ключевое слово!");
			}
			catch (Exception e)
			{
			}

			return result;
		}

	}
}
