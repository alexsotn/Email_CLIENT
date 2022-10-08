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
	public partial class users : Form
	{
		public List<Protokol> user_add;
		public users(List<Protokol> list_)
		{
			InitializeComponent();
			user_add = list_;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				Protokol add_item = new Protokol();
				//add_item.name_service = servis_combo.SelectedText.ToString();
				add_item.name_service = servis_combo.GetItemText(servis_combo.SelectedItem).ToString();
				add_item.UserEmal = login_txt.Text.ToString();
				add_item.USerpass = pass_txt.Text.ToString();
				add_item.USerNAME = user_txt.Text.ToString();
				if (ACT.Test_connection_server(add_item))
				{
					user_add.Add(add_item);
					if (ACT.Registration_user_system(user_add))
					{
						MessageBox.Show("Пользователь доабавелен с систему");
						Close();
					}
					else
						MessageBox.Show("Ошибка добавления");
				}
			}
			catch (Exception es)
			{
				MessageBox.Show(es.Message.ToString());
			}
		}

		
	}
}
