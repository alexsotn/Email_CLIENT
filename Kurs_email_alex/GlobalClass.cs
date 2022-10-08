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

using MailKit.Net.Imap;

//using Message = OpenPop.Mime.Message;
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
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Text.Json;

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
	public class struct_sms_coding
	{
		public string txt_sms { get; set; }
		public List<string> rsa_code { get; set; }
		public string dsa_metka { get; set; }
	}
	public class save_param
	{
		public int rsa_p { get; set; }
		public int rsa_q { get; set; }
		public int rsa_d { get; set; }
		public int rsa_n { get; set; }
		public int dsa_p { get; set; }
		public int dsa_q { get; set; }
		public int dsa_d { get; set; }
		public string dsa_n { get; set; }
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
								row["Port_smtp"] = "25".ToString();
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
		public static bool setting_user_system(List<Protokol> root)
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
						row["name_service"] = root.ElementAt(i).name_service.ToString();
						row["Port_smtp"] = root.ElementAt(i).Port_smtp.ToString();
						row["Port_pop"] = root.ElementAt(i).Port_pop.ToString();
						row["Port_imap"] = root.ElementAt(i).Port_imap.ToString();
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
				//else
				//{
				//	MessageBox.Show("XML файл не найден.", "Ошибка.");
				//}
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
							  //smtp.(m); // Отправляем сообщение
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
					//client.
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
					//inbox.appe(FolderAccess.ReadOnly);
					int max = inbox.Count - 1, _i = 0;
					for (int i = max; 0 <= i; i--, _i++)
					{
						StructureSMS item = new StructureSMS();
						var message = inbox.GetMessage(i);
						item.UID = Convert.ToInt32(i);
						item.to_sms = message.To.ToString();
						item.from_sms = message.From.ToString();
						item.Data_sms =  Convert.ToDateTime(message.Date.ToString()).ToShortDateString().ToString();
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
		public static bool add_sms_draft(Protokol prot, StructureSMS sms)
		{
			bool result = false;
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress(prot.USerNAME.ToString(), prot.UserEmal.ToString()));
				message.To.Add(new MailboxAddress("", sms.to_sms.ToString()));
				message.Subject = sms.Subjeck.ToString();
				message.Body = new TextPart("plain")
				{
					Text = sms.Body.ToString()
				};
				using (var client = new ImapClient())
				{
					try
					{
						client.Connect("imap."+prot.name_service.ToString(), prot.Port_imap, SecureSocketOptions.SslOnConnect);
						client.AuthenticationMechanisms.Remove("XOAUTH2");
						client.Authenticate(prot.UserEmal.ToString(), prot.USerpass.ToString());
						var draftFolder = client.GetFolder(MailKit.SpecialFolder.Drafts);
						if (draftFolder != null)
						{
							draftFolder.Open(FolderAccess.ReadWrite);
							draftFolder.Append(message, MessageFlags.Draft);
							draftFolder.Expunge();
						}
						else
						{
							var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
							var DraftFolder = toplevel.Create(MailKit.SpecialFolder.Drafts.ToString(), true);
							DraftFolder.Open(FolderAccess.ReadWrite);
							DraftFolder.Append(message, MessageFlags.Draft);
							DraftFolder.Expunge();
						}
						result = true;
					}
					catch (Exception ex)
					{
						throw new ApplicationException("IMAPException has occured: " + ex.Message);
					}

					client.Disconnect(true);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
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
					//inbox.Append(message, MessageFlags.Deleted);
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
		public static List<struct_sms_coding> decode_sms(string xml)
		{
			List<struct_sms_coding> SSC = null;
			try
			{
				File.WriteAllText("sms.xml", xml);
				if (File.Exists("sms.xml")) // если существует данный файл
				{
					if (File.ReadAllText("sms.xml") != "")
					{
						DataSet ds = new DataSet(); // создаем новый пустой кэш данных
						ds.ReadXml("sms.xml"); // записываем в него XML-данные из файла
						foreach (DataRow item in ds.Tables["SMS_STRUCT_code"].Rows)// парсим полученные данные 
						{
							SSC = new List<struct_sms_coding>();
							struct_sms_coding prt = new struct_sms_coding();
							prt.txt_sms = Convert.ToString(item["txt_sms"]);
							List<string> rsa_list = new List<string>();
							for (int i = 0; i < coutn_imet_list_find(File.ReadAllText("sms.xml")); i++)
							{
								string rsa_sts = item["rsa_code_" + i].ToString();
								rsa_list.Add(rsa_sts);
							}
							prt.rsa_code = rsa_list;
							prt.dsa_metka = Convert.ToString(item["dsa_summa"]);
							SSC.Add(prt);
						}
					}
					FileInfo fileInf = new FileInfo("sms.xml");
					if (fileInf.Exists)
					{
						fileInf.Delete();
					}
				}
				else
				{
					FileInfo fileInf = new FileInfo("sms.xml");
					if (fileInf.Exists)
					{
						fileInf.Delete();
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Сообщение битое и не подлежит декодированию!!!");
			}
			return SSC;
		}
		public static string create_file_coding(List<struct_sms_coding> sms)
		{
			string result = "";
			try
			{
				DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
				DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
				dt.TableName = "SMS_STRUCT_code"; // название таблицы
				dt.Columns.Add("txt_sms"); // название колонок
				for (int i = 0; i < sms.ElementAt(0).rsa_code.Count(); i++)
				{
					dt.Columns.Add("rsa_code_" + i);
				}

				dt.Columns.Add("dsa_summa");
				ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

				for (int i = 0; i < sms.Count(); i++)
				{
					DataRow row = ds.Tables["SMS_STRUCT_code"].NewRow();
					row["txt_sms"] = sms.ElementAt(i).txt_sms.ToString();
					if (sms.ElementAt(i).rsa_code.Count() > 0)
					{
						for (int q = 0; q < sms.ElementAt(i).rsa_code.Count(); q++)
						{
							row["rsa_code_" + q] = sms.ElementAt(i).rsa_code.ElementAt(q).ToString();
							//dt.Columns.Add("rsa_code");
						}
					}
					row["dsa_summa"] = sms.ElementAt(i).dsa_metka.ToString();
					ds.Tables["SMS_STRUCT_code"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
				}
				File.WriteAllText("sms.xml", "");
				ds.WriteXml("sms.xml");
				if (File.Exists("sms.xml")) // если существует данный файл
				{
					result = File.ReadAllText("sms.xml").ToString();
					FileInfo fileInf = new FileInfo("sms.xml");
					if (fileInf.Exists)
					{
						fileInf.Delete();
					}
				}
			}
			catch (Exception e)
			{
				result = "";
			}
			return result;
		}
		public static int coutn_imet_list_find(string str)
		{
			string find = "rsa_code_";
			int count = 0; int index = -1; // да, индекс изначально устанавливаем в -1
			int textLength = str.Length, findLength = find.Length;

			while ((textLength - index) >= findLength)
			{
				index = str.IndexOf(find, index + 1); // вот тут на первой итерации -1 превращается в 0

				if (index != -1)
				{
					count++;
				}
				else break;

				if ((textLength - index) == findLength) break;
			}
			return count / 2;
		}
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
			public static char[] characters = new char[] { '#', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
			//проверка: простое ли число?
			public static bool IsTheNumberSimple(long n)
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
			public static long Calculate_d(long m)
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
			public static long Calculate_e(long d, long m)
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
					result = RSA_Dedoce(txt, d, n);
				}
				else
					MessageBox.Show("Введите p и q!");
				return result;
			}
		}
		public class SHA1
		{
			public static string GET_HACH(string input)
			{
				using (SHA1Managed sha1 = new SHA1Managed())
				{
					var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
					var sb = new StringBuilder(hash.Length * 2);
					foreach (byte b in hash)
					{
						sb.Append(b.ToString("x2"));
					}
					return sb.ToString();
				}
			}
			public static string GET_HACH2(string text)
			{
				SHA256 SHA256Hash = SHA256.Create();

				byte[] data = SHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

				StringBuilder sBuilder = new StringBuilder();

				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				return sBuilder.ToString();
			}
		}
		public class DSA
		{
			static char[] characters = new char[] {
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
			'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'w', 'x', 'y', 'z'};

			// check: is it a simple number?
			public static bool IsTheNumberSimple(int n)
			{
				if (n < 2)
					return false;

				if (n == 2)
					return true;

				for (long i = 2; i < n; i++)
					if (n % i == 0)
						return false;

				return true;
			}

			// the message hash code is calculated
			private static int Hash(string hash, int q)
			{
				int k = 0;
				int f = 0;

				for (int i = 0; i < hash.Length; i++)
				{
					for (int j = 0; j < characters.Length; j++)
					{
						if (characters[j] == hash[i])
						{
							f = j + 1;
							break;
						}
					}

					k = Convert.ToInt32(Math.Pow(k + f, 2) % q);
				}
				return k;
			}

			// a fast algorithm for raising a number to a power
			// and then finding the remainder from division by another number
			private static int Fast(int a, int r, int n)
			{
				int a1 = a;
				int z1 = r;
				int x = 1;

				while (z1 != 0)
				{
					while (z1 % 2 == 0)
					{
						z1 /= 2;
						a1 = (a1 * a1) % n;
					}

					z1 -= 1;
					x = (x * a1) % n;
				}

				return x;
			}

			// alculate G
			public static int CalculateG(int p, int q)
			{
				Random rand = new Random();
				double g = 0;

				while (g < 1)
				{
					g = Math.Pow(rand.Next(1, p - 1), (p - 1) / q) % p;
				}

				return Convert.ToInt32(g);
			}

			// calculate simple P
			public static int CalculateP(int q)
			{
				int p = q + 1;

				while (true) // in order that q is a divisor of p-1
				{
					bool f = false;

					for (int i = 2; i < p - 1; i++)
					{
						if (p % i == 0)
						{
							f = true;
							break;
						}
					}

					if (!f && (p - 1) % q == 0)
					{
						break;
					}
					else
					{
						p++;
					}
				}
				return p;
			}

			// calculate 
			public static int CalculateY(int p, double g, int x)
			{
				return Fast(Convert.ToInt32(g), x, p);
			}

			// encode
			public static string Encode(string hash, int p, int q, double g, int x)
			{
				try
				{
					int r = 0;
					int s = 0;
					int s1 = 0;
					int k = 0;
					int h = Hash(hash, q);

					while (k < q)
					{
						k++;

						int k1 = 0;

						r = Fast(Convert.ToInt32(g), k, p) % q;

						while ((k1 * k) % q != 1 && k1 < 10)
						{
							k1++;
						}

						if (k1 == 10) continue; // did not find the appropriate K1

						s = Convert.ToInt32(k1 * (h + x * r)) % q;

						while ((s1 * s) % q != 1 && s1 < 10)
						{
							s1++;
						}

						if (s1 == 10) continue; // did not find the appropriate S1

						if (r != 0 || s != 0) break;
					}
					if (k == q) // did not find the appropriate K1 < Q
						throw new Exception();

					return r + "-" + s;
				}
				catch (Exception)
				{
					throw new Exception();
				}
			}

			// decode and compares the values of the signature and the result and returns the result
			public static bool DecodeAndEqual(string hash, int p, int q, int y, double g, string ESD)
			{
				try
				{
					string[] data = ESD.Split(new Char[] { '-' });
					int r = Convert.ToInt32(data[0]);
					int s = Convert.ToInt32(data[1]);

					int s1 = 0;

					while ((s1 * s) % q != 1)
					{
						s1++;
					}

					int w = s1 % q;
					int u1 = (Hash(hash, q) * w) % q;
					int u2 = (r * w) % q;

					double mp1 = Fast(Convert.ToInt32(g), u1, p);
					double mp2 = Fast(y, u2, p);
					double res = mp1 * mp2;

					res %= p;
					res %= q;
					int v = Convert.ToInt32(res);

					return v == r;
				}
				catch (Exception)
				{
					throw new Exception();
				}
			}

		}

		// так для удобства прописал сохрание кодов  после их генирации которые на форме!!!

		public static bool save_param_coding(save_param P)
		{
			bool res = false;
			try
			{
				if (P.rsa_d != 0)
				{
					DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
					DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
					dt.TableName = "param"; // название таблицы
					dt.Columns.Add("rsa_p"); // название колонок
					dt.Columns.Add("rsa_q");
					dt.Columns.Add("rsa_n");
					dt.Columns.Add("rsa_d");
					dt.Columns.Add("dsa_p");
					dt.Columns.Add("dsa_q");
					dt.Columns.Add("dsa_n");
					dt.Columns.Add("dsa_d");
					ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше
					try
					{
						DataRow row = ds.Tables["param"].NewRow();

						row["rsa_p"] = P.rsa_p.ToString();
						row["rsa_q"] = P.rsa_q.ToString();
						row["rsa_n"] = P.rsa_n.ToString();
						row["rsa_d"] = P.rsa_d.ToString();
						row["dsa_p"] = P.dsa_p.ToString();
						row["dsa_q"] = P.dsa_q.ToString();
						row["dsa_n"] = P.dsa_n.ToString();
						row["dsa_d"] = P.dsa_d.ToString();
						//row["SSL"] = "true".ToString();
						ds.Tables["param"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
					}
					catch (Exception es)
					{
						MessageBox.Show(es.Message.ToString());
					}
					SaveFileDialog SaveFiles = new SaveFileDialog();
					SaveFiles.Filter = "XML-File|*.xml";
					string filename = SaveFiles.FileName;
					if (SaveFiles.ShowDialog() == DialogResult.OK)
					{
						filename = SaveFiles.FileName;
						ds.WriteXml(filename);
						//MessageBox.Show("Файл сохранен");
						res = true;
					}
				}
			}
			catch (Exception es)
			{
				MessageBox.Show(es.Message.ToString());
			}
			return res;
		}
		public static save_param load_param_coding()
		{
			save_param prm = new save_param();
			OpenFileDialog LoadFiles = new OpenFileDialog();
			try
			{
				var filePath = string.Empty;
				LoadFiles.Filter = "XML-File|*.xml";
				if (LoadFiles.ShowDialog() == DialogResult.OK)
				{
					filePath = LoadFiles.FileName;
					if (File.Exists(filePath)) // если существует данный файл
					{
						DataSet ds = new DataSet(); // создаем новый пустой кэш данных
						ds.ReadXml(filePath); // записываем в него XML-данные из файла
						foreach (DataRow item in ds.Tables["param"].Rows)// парсим полученные данные 
						{
							prm = new save_param();
							prm.rsa_p = Convert.ToInt32(item["rsa_p"]);
							prm.rsa_q = Convert.ToInt32(item["rsa_q"]);//
							prm.rsa_n = Convert.ToInt32(item["rsa_n"]);
							prm.rsa_d = Convert.ToInt32(item["rsa_d"]);

							prm.dsa_p = Convert.ToInt32(item["dsa_p"]);
							prm.dsa_q = Convert.ToInt32(item["dsa_q"]);
							prm.dsa_n = Convert.ToString(item["dsa_n"]);
							prm.dsa_d = Convert.ToInt32(item["dsa_d"]);
						}
						MessageBox.Show("Параметры загрузились");
					}
				}
			}
			catch
			{
				MessageBox.Show("XML файл содержит битую разметку(параметров)!!!");
			}
			return prm;
		}

		public static string Convert_sms_to_byte(string str)
		{
			var bytes = Encoding.UTF8.GetBytes(str.ToString());
			for (int i = 0; i < bytes.Length; i++) bytes[i] ^= 0x5a;
			return Convert.ToBase64String(bytes);
		}
		public static string Convert_byte_to_sms(string str)
		{
			try
			{
				var bytes = Convert.FromBase64String(str);
				for (int i = 0; i < bytes.Length; i++) bytes[i] ^= 0x5a;
				return Encoding.UTF8.GetString(bytes);
			}
			catch (Exception)
			{
				MessageBox.Show("Сообщение не подлежит востановлению!!! Нарушена стуруктура сообщения!!!");
				return "";
			}
		}
	}
}
