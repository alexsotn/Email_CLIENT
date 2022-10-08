using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs_email_alex
{
	public partial class form_setting : Form
	{
		List<Protokol> update_setting;
		int flag_item;
		public form_setting(List<Protokol> user_data_client, int F)
		{
			update_setting = user_data_client;
			flag_item = F;
			InitializeComponent();
		}

		private void form_setting_Load(object sender, EventArgs e)
		{
			txt_host.Text = update_setting.ElementAt(flag_item).name_service.ToString();
			txt_port_imap.Text = update_setting.ElementAt(flag_item).Port_imap.ToString();
			txt_port_smtp.Text = update_setting.ElementAt(flag_item).Port_smtp.ToString();
			txt_port_smtp_pop.Text = update_setting.ElementAt(flag_item).Port_pop.ToString();
			check_ssl.Checked = update_setting.ElementAt(flag_item).SSL;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				update_setting.ElementAt(flag_item).name_service = txt_host.Text;
				update_setting.ElementAt(flag_item).Port_imap = Convert.ToInt32(txt_port_imap.Text);
				update_setting.ElementAt(flag_item).Port_smtp = Convert.ToInt32(txt_port_smtp.Text);
				update_setting.ElementAt(flag_item).Port_pop = Convert.ToInt32(txt_port_smtp_pop.Text);
				update_setting.ElementAt(flag_item).SSL = check_ssl.Checked;
				if (ACT.Test_connection_server(update_setting.ElementAt(flag_item)))
				{
					if (ACT.setting_user_system(update_setting))
					{
						MessageBox.Show("Данные изменены)))");
						Close();
					}
					else
						MessageBox.Show("Ошибка Измениния данных");
				}
			}
			catch (Exception es)
			{
				MessageBox.Show(es.Message.ToString());
			}
		}
	}
}
