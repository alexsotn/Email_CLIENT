using System;
using System.Net; // Подключаем библиотеку для работы с сетью
using System.Net.Mail; // Из библиотеки подключаем класс для работы с почтой


using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenPop.Mime;
using OpenPop.Pop3;
using OpenPop.Common;

using MailKit.Net.Imap;

using Message = OpenPop.Mime.Message;
using MailKit.Security;
using static System.Environment;
using MailKit;
using MailKit.Search;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MimeKit;
using System.Threading;
using System.IO;

using System.Numerics;

namespace Kurs_email_alex
{
	public class StructureSMS
	{
		public int UID { get; set; }
		public string to_sms { get; set; }
		public string from_sms { get; set; }
		public string Subjeck { get; set; } // тема
		public string Body { get; set; } // содержание смс
		public string Data_sms { get; set; } // дата сообщения
		public string Attachments { get; set; } // дата сообщения
		public IEnumerable<MimeEntity> Attachments_file { get; set; }
	}

	public class Protokol
	{
		public string name_service { get; set; }
		public int Port_smtp { get; set; }
		public int Port_pop { get; set; }
		public int Port_imap { get; set; }
		public string UserEmal { get; set; } // тема
		public string USerpass { get; set; } // содержание смс
		public string USerNAME { get; set; } // содержание смс
		public bool SSL { get; set; } // дата сообщения
	}

	public class ACT
	{
		public static bool Registration_user_system(List<Protokol> root)
		{
			bool res = false;
			try
			{
				if (root.Count != 0)
				{
					DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
					DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
					dt.TableName = "Setting"; // название таблицы
					dt.Columns.Add("name_service"); // название колонок
					dt.Columns.Add("Port_smtp");
					dt.Columns.Add("Port_pop");
					dt.Columns.Add("Port_imap");
					dt.Columns.Add("UserEmal");
					dt.Columns.Add("USerpass");
					dt.Columns.Add("USerNAME");
					dt.Columns.Add("SSL");

					ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

					for (int i = 0; i < root.Count(); i++)
					{
						DataRow row = ds.Tables["Setting"].NewRow();
						switch (root.ElementAt(i).name_service)
						{
							case "gmail.com":
								row["name_service"] = "gmail.com".ToString();
								row["Port_smtp"] = "587".ToString();
								row["Port_pop"] = "995".ToString();
								row["Port_imap"] = "993".ToString();
								break;
							case "yandex.ru":
								row["name_service"] = "yandex.ru".ToString();
								row["Port_smtp"] = "25".ToString();//25
								row["Port_pop"] = "995".ToString();//110
								row["Port_imap"] = "993".ToString();//110
								break;
							case "mail.ru":
								row["name_service"] = "mail.ru".ToString();
								row["Port_smtp"] = "465".ToString();
								row["Port_pop"] = "995".ToString();
								row["Port_imap"] = "993".ToString();
								break;
						}

						row["UserEmal"] = root.ElementAt(i).UserEmal.ToString();
						row["USerpass"] = root.ElementAt(i).USerpass.ToString();
						row["USerNAME"] = root.ElementAt(i).USerNAME.ToString();
						row["SSL"] = "true".ToString();
						ds.Tables["Setting"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
					}
					ds.WriteXml("setting.xml");
					res = true;
				}
				else
				{
					File.WriteAllText(@"setting.xml", string.Empty);
					res = false;
				}
			}
			catch (Exception es)
			{
				MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.  " + es.Message.ToString());
			}
			return res;
		}
		public static List<Protokol> Find_user_avtorization()
		{
			List<Protokol> result = new List<Protokol>();
			try
			{
				if (File.Exists("setting.xml")) // если существует данный файл
				{
					if (File.ReadAllText("setting.xml") != "")
					{
						DataSet ds = new DataSet(); // создаем новый пустой кэш данных
						ds.ReadXml("setting.xml"); // записываем в него XML-данные из файла
						foreach (DataRow item in ds.Tables["Setting"].Rows)// парсим полученные данные 
						{
							Protokol prt = new Protokol();
							prt.name_service = Convert.ToString(item["name_service"]);
							prt.Port_smtp = Convert.ToInt32(item["Port_smtp"]);

							prt.Port_pop = Convert.ToInt32(item["Port_pop"]);
							prt.Port_imap = Convert.ToInt32(item["Port_imap"]);
							prt.UserEmal = Convert.ToString(item["UserEmal"]);
							prt.USerpass = Convert.ToString(item["USerpass"]);
							prt.USerNAME = Convert.ToString(item["USerNAME"]);
							prt.SSL = Convert.ToBoolean(item["SSL"].ToString());
							result.Add(prt);
						}
					}
				}
				else
				{
					MessageBox.Show("XML файл не найден.", "Ошибка.");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message.ToString());
			}
			return result;
		}
		public static bool Test_connection_server(Protokol prot)
		{
			bool rec = false;
			try
			{
				using (ImapClient client = new ImapClient())
				{
					client.Connect("imap." + prot.name_service, prot.Port_imap, SecureSocketOptions.SslOnConnect);
					client.Authenticate(prot.UserEmal, prot.USerpass);
					IMailFolder inbox = client.Inbox;
					inbox.Open(FolderAccess.ReadOnly);
					inbox.Close();
					rec = true;
				}
			}
			catch (Exception e)
			{
				rec = false;
				MessageBox.Show("Ошибка! Некоректный ввод!");
			}
			return rec;
		}

		public static bool Sent_sms(StructureSMS sms, Protokol prot)
		{
			bool result = false;
			try
			{
				MailAddress from = new MailAddress(prot.UserEmal.ToString(), prot.USerNAME.ToString()); // От кого отправляем Почта и Ник
				MailAddress to = new MailAddress(sms.to_sms.ToString()); // Почта куда отправляем письма
				MailMessage m = new MailMessage(from, to); // Создаем объект сообщения
				m.Subject = sms.Subjeck.ToString(); // Задаем заголовок/тему письма
				m.Body = sms.Body.ToString();
				//m.IsBodyHtml = true; // Если используем HTML теги в сообщении ставим true, если только текст ставим false
				SmtpClient smtp = new SmtpClient("smtp." + prot.name_service.ToString(), prot.Port_smtp); // Указываем хост и порт SMTP сервера с которого отправляем
				smtp.Credentials = new NetworkCredential(prot.UserEmal.ToString(), prot.USerpass.ToString()); // Данные почты с которой будет производится отправка сообщения
				smtp.EnableSsl = Convert.ToBoolean(prot.SSL); // SSL соединение если оно нужно для хоста

				if (sms.Attachments != "")
					m.Attachments.Add(new Attachment(sms.Attachments.ToString()));

				smtp.Send(m); // Отправляем сообщение
				result = true;
			}
			catch (Exception e)
			{
				MessageBox.Show("ERROR: " + e.Message.ToString());
			}
			return result;
		}

		public static List<StructureSMS> folder_sms(Protokol prot, int folder)
		{
			List<StructureSMS> sms = new List<StructureSMS>();
			try
			{
				using (ImapClient client = new ImapClient())
				{
					client.Connect("imap." + prot.name_service, prot.Port_imap, SecureSocketOptions.SslOnConnect);
					client.Authenticate(prot.UserEmal, prot.USerpass);
					IMailFolder inbox = client.Inbox;
					switch (folder)
					{
						case 0: // входящие
							inbox = client.Inbox;
							break;
						case 1: // Отправленные
							inbox = client.GetFolder(MailKit.SpecialFolder.Sent);
							break;
						case 2: // черновики
							inbox = client.GetFolder(MailKit.SpecialFolder.Drafts);
							break;
						case 3: // корзина
							inbox = client.GetFolder(MailKit.SpecialFolder.Trash);
							break;
					}
					inbox.Open(FolderAccess.ReadOnly);
					int max = inbox.Count - 1, _i = 0;
					for (int i = max; 0 <= i; i--, _i++)
					{
						StructureSMS item = new StructureSMS();
						var message = inbox.GetMessage(i);
						item.UID = Convert.ToInt32(i);
						item.to_sms = message.To.ToString();
						item.from_sms = message.From.ToString();
						item.Data_sms = Convert.ToDateTime(message.Date.ToString()).ToShortDateString().ToString();
						item.Subjeck = message.Subject.ToString();
						item.Attachments = message.Attachments.Count().ToString();
						item.Attachments_file = message.Attachments;
						string body = (message.TextBody == null || message.TextBody.Trim().Length == 0) ? message.HtmlBody : message.TextBody;
						item.Body = Regex.Replace(body, "<[^>]+>", string.Empty);
						sms.Add(item);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message.ToString());
			}
			return sms;
		}

		public static bool DELETE_sms(int UID, int folder, Protokol prot)
		{
			bool Result = false;
			try
			{
				using (var client = new ImapClient())
				{
					client.Connect("imap." + prot.name_service, prot.Port_imap, SecureSocketOptions.SslOnConnect);
					client.AuthenticationMechanisms.Remove("XOAUTH");
					client.Authenticate(prot.UserEmal, prot.USerpass);
					IMailFolder inbox = client.Inbox;
					switch (folder)
					{
						case 0: // входящие
							inbox = client.Inbox;
							break;
						case 1: // Отправленные
							inbox = client.GetFolder(MailKit.SpecialFolder.Sent);
							break;
						case 2: // черновики
							inbox = client.GetFolder(MailKit.SpecialFolder.Drafts);
							break;
						case 3: // корзина
							inbox = client.GetFolder(MailKit.SpecialFolder.Trash);
							break;
					}
					inbox.Open(FolderAccess.ReadWrite, CancellationToken.None);
					var message = inbox.GetMessage(UID);
					inbox.AddFlags(UID, MessageFlags.Deleted, true, CancellationToken.None);
					inbox.Close(true, CancellationToken.None);
					//inbox.AddFlagsAsync(new int[] { UID }, MessageFlags.Deleted, true);
					Result = true;
				}
			}
			catch (Exception e)
			{
				Result = false;
				MessageBox.Show("Error: " + e.Message.ToString());
			}
			return Result;
		}
	}
	public class coding_sms
	{
		public class DEC
		{
			private const int sizeOfBlock = 128; //в DES размер блока 64 бит, но поскольку в unicode символ в два раза длинее, то увеличим блок тоже в два раза
			private const int sizeOfChar = 16; //размер одного символа (in Unicode 16 bit)
			private const int shiftKey = 2; //сдвиг ключа 
			private const int quantityOfRounds = 16; //количество раундов
			public static string[] Blocks;
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

			public static string to_str_key_decode(string text, string key)
			{
				string result = "";
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
					result = key;
					//result = StringFromBinaryToNormalFormat(key);
				}
				else
					MessageBox.Show("Введите ключевое слово!");
				return result;
			}
			public static string Encrypt(string txt_body, string key)
			{
				
				string result = "";
				try
				{
					if (key.Length > 0)
					{
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
			public static string Decipher(string txt_body, string key)
			{
				key = StringFromBinaryToNormalFormat(key);
				string result = "";
				try
				{
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
				}
				catch (Exception e)
				{
				}

				return result;
			}
		}
		public class RSA
		{
			//public static char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
			//											'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
			//											'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
			//											'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
			//											'8', '9', '0' };
			public static char[] characters = new char[] { '#', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z','1', '2', '3', '4', '5', '6', '7','8', '9', '0'  };
			//проверка: простое ли число?
			private static bool IsTheNumberSimple(long n)
			{
				if (n > 1)
				{
					// в цикле перебираем числа от 2 до n - 1
					for (int i = 2; i < n; i++)
						if (n % i == 0) // если n делится без остатка на i - возвращаем false (число не простое)
							return false;

					// если программа дошла до данного оператора, то возвращаем true (число простое) - проверка пройдена
					return true;
				}
				else // иначе возвращаем false (число не простое)
					return false;
				// return true;
			}
			//зашифровать
			private static List<string> RSA_Endoce(string s, long e, long n)
			{
				List<string> result = new List<string>();

				BigInteger bi;

				for (int i = 0; i < s.Length; i++)
				{
					int index = Array.IndexOf(characters, s[i]);

					bi = new BigInteger(index);
					bi = BigInteger.Pow(bi, (int)e);

					BigInteger n_ = new BigInteger((int)n);

					bi = bi % n_;

					result.Add(bi.ToString());
				}

				return result;
			}
			//расшифровать
			private static string RSA_Dedoce(List<string> input, long d, long n)
			{
				string result = "";

				BigInteger bi;

				foreach (string item in input)
				{
					bi = new BigInteger(Convert.ToDouble(item));
					bi = BigInteger.Pow(bi, (int)d);

					BigInteger n_ = new BigInteger((int)n);

					bi = bi % n_;

					int index = Convert.ToInt32(bi.ToString());

					result += characters[index].ToString();
				}

				return result;
			}
			//вычисление параметра d. d должно быть взаимно простым с m
			private static long Calculate_d(long m)
			{
				long d = m - 1;

				for (long i = 2; i <= m; i++)
					if ((m % i == 0) && (d % i == 0)) //если имеют общие делители
					{
						d--;
						i = 1;
					}

				return d;
			}
			//вычисление параметра e
			private static long Calculate_e(long d, long m)
			{
				long e = 10;

				while (true)
				{
					if ((e * d) % m == 1)
						break;
					else
						e++;
				}

				return e;
			}


			public static List<string> Encrypt(int _p, int _q, string txt)
			{
				List<string> result = new List<string>();
				if ((_p > 0) && (_q > 0))
				{
					long p = Convert.ToInt64(_p);
					long q = Convert.ToInt64(_q);

					if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
					{
						txt = txt.ToUpper();

						long n = p * q;
						long m = (p - 1) * (q - 1);
						long d = Calculate_d(m);
						long e_ = Calculate_e(d, m);
						result.Clear();
						result = RSA_Endoce(txt, e_, n);
					}
					else
						MessageBox.Show("p или q - не простые числа!");
				}
				else
					MessageBox.Show("Введите p и q!");
				return result;
			}
			public static string Decipher(int _D, int _N, List<string> txt)
			{
				string result = "";
				if ((_D > 0) && (_N > 0))
				{
					long d = Convert.ToInt64(_D);
					long n = Convert.ToInt64(_N);

					//List<string> input = txt;
					result = RSA_Dedoce(txt, d, n);
				}
				else
					MessageBox.Show("Введите p и q!");
				return result;
			}
		}
		public class SHA1
		{

		}
		public class DSA
		{

		}
	}

}
