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
	public partial class main : Form
	{
		List<Protokol> user_data_client = new List<Protokol>();
		List<StructureSMS> ID_sms;
		int UID_del = -1;
		public main()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//user_data_client = new List<Protokol>();
			if (0 != ACT.Find_user_avtorization().Count())
			{
				user_data_client = ACT.Find_user_avtorization();
				for (int i = 0; i < user_data_client.Count(); i++)
				{
					list_user.Items.Add(user_data_client.ElementAt(i).UserEmal.ToString());
				}
				list_user.SelectedIndex = 0;
				user_id.Text = "Пользователь: " + user_data_client.ElementAt(list_user.SelectedIndex).USerNAME;
				grid_inbox.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 0);
			}
			else
			{
				MessageBox.Show("В системе нет авторизированых пользователей");
				users form = new users(user_data_client);
				form.ShowDialog();
				up_user();
			}
			panel1.Width = 15;



			grid_basket.AllowUserToAddRows = false;
			grid_basket.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			grid_basket.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			grid_basket.ReadOnly = true;
			grid_basket.RowHeadersVisible = false;

			grid_draft.AllowUserToAddRows = false;
			grid_draft.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			grid_draft.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			grid_draft.ReadOnly = true;
			grid_draft.RowHeadersVisible = false;

			grid_inbox.AllowUserToAddRows = false;
			grid_inbox.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			grid_inbox.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			grid_inbox.ReadOnly = true;
			grid_inbox.RowHeadersVisible = false;

			grid_send.AllowUserToAddRows = false;
			grid_send.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			grid_send.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			grid_send.ReadOnly = true;
			grid_send.RowHeadersVisible = false;
		}

		private void bt_menu_Click(object sender, EventArgs e)
		{
			if (bt_menu.Text == ">")
			{
				bt_menu.Text = "<";
				panel1.Width = 265;
			}
			else
			{
				bt_menu.Text = ">";
				panel1.Width = 15;
			}

		}

		private void bt_sent_Click(object sender, EventArgs e)
		{
			form_sent form = new form_sent(user_data_client.ElementAt(list_user.SelectedIndex), null);
			form.flag_foger = 999;
			form.ShowDialog();
		}

		private void Connect_server_Click(object sender, EventArgs e)
		{
			users form = new users(user_data_client);
			form.ShowDialog();
			if (0 != ACT.Find_user_avtorization().Count())
				up_user();
			else
			{
				up_user();
			}
		}

		private void bt_setting_Click(object sender, EventArgs e)
		{
			int flag = list_user.SelectedIndex;
			form_setting form = new form_setting(user_data_client, list_user.SelectedIndex);
			form.ShowDialog();
			up_user();
			list_user.SelectedIndex = flag;
			UpDate(flag);
		}

		private void Del_sms_Click(object sender, EventArgs e)
		{
			if (UID_del != -1)
			{

				DialogResult result = MessageBox.Show(
					"Вы уверенны что хотите удалить это сообщение ?",
						"Сообщение",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Information,
						MessageBoxDefaultButton.Button1);
				if (result == DialogResult.Yes)
				{
					if (ACT.DELETE_sms(UID_del, tab_control.SelectedIndex, user_data_client.ElementAt(list_user.SelectedIndex)))
					{
						MessageBox.Show("Сообщение удалено");
						UpDate(this.tab_control.SelectedIndex);
					}
					else
						MessageBox.Show("Ошибка при удалении!");
				}
			}
			else
				MessageBox.Show("Невыбранно сообжение для удаления!");

		}

		private void bt_input_Click(object sender, EventArgs e)
		{
			grid_inbox.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 0);
			tab_control.SelectedIndex = 0;
		}

		private void bt_sented_Click(object sender, EventArgs e)
		{
			grid_send.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 1);
			tab_control.SelectedIndex = 1;
		}

		private void bt_draft_Click(object sender, EventArgs e)
		{
			grid_draft.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 2);
			tab_control.SelectedIndex = 2;
		}

		private void bt_basket_Click(object sender, EventArgs e)
		{
			grid_basket.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 3);
			tab_control.SelectedIndex = 3;
		}


		private void tab_control_Selected(object sender, TabControlEventArgs e)
		{
			UpDate(this.tab_control.SelectedIndex);
		}

		private void bt_refrech_Click(object sender, EventArgs e)
		{
			UpDate(this.tab_control.SelectedIndex);
		}

		private void grid_inbox_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			UID_del = Convert.ToInt32(this.grid_inbox.CurrentRow.Cells[0].Value);
		}

		private void grid_send_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			UID_del = Convert.ToInt32(this.grid_send.CurrentRow.Cells[0].Value);
		}

		private void grid_draft_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			UID_del = Convert.ToInt32(this.grid_draft.CurrentRow.Cells[0].Value);
		}

		private void grid_basket_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			UID_del = Convert.ToInt32(this.grid_basket.CurrentRow.Cells[0].Value);
		}

		public void UpDate(int Q)
		{
			switch (Q)
			{
				case 0:
					grid_inbox.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 0);
					break;
				case 1:
					grid_send.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 1);
					break;
				case 2:
					grid_draft.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 2);
					break;
				case 3:
					grid_basket.DataSource = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), 3);
					break;
			}
		}

		private void grid_inbox_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//UID_del = Convert.ToInt32(this.grid_inbox.CurrentRow.Cells[0].Value);
			ID_sms = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), tab_control.SelectedIndex);
			form_sent form = new form_sent(user_data_client.ElementAt(list_user.SelectedIndex), ID_sms.ElementAt(e.RowIndex));
			form.flag_foger = tab_control.SelectedIndex;
			form.ShowDialog();
			UpDate(tab_control.SelectedIndex);
		}

		private void grid_send_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//UID_del = Convert.ToInt32(this.grid_send.CurrentRow.Cells[0].Value);
			ID_sms = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), tab_control.SelectedIndex);
			form_sent form = new form_sent(user_data_client.ElementAt(list_user.SelectedIndex), ID_sms.ElementAt(e.RowIndex));
			form.flag_foger = tab_control.SelectedIndex;
			form.ShowDialog();
			UpDate(tab_control.SelectedIndex);
		}

		private void grid_draft_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//UID_del = Convert.ToInt32(this.grid_draft.CurrentRow.Cells[0].Value);
			ID_sms = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), tab_control.SelectedIndex);
			form_sent form = new form_sent(user_data_client.ElementAt(list_user.SelectedIndex), ID_sms.ElementAt(e.RowIndex));
			form.flag_foger = tab_control.SelectedIndex;
			form.UID_del_sms = UID_del;
			form.ShowDialog();
			UpDate(tab_control.SelectedIndex);
		}

		private void grid_basket_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//UID_del = Convert.ToInt32(this.grid_basket.CurrentRow.Cells[0].Value);
			ID_sms = ACT.folder_sms(user_data_client.ElementAt(list_user.SelectedIndex), tab_control.SelectedIndex);
			form_sent form = new form_sent(user_data_client.ElementAt(list_user.SelectedIndex), ID_sms.ElementAt(e.RowIndex));
			form.flag_foger = tab_control.SelectedIndex;
			form.ShowDialog();
			UpDate(tab_control.SelectedIndex);
		}

		private void list_user_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpDate(this.tab_control.SelectedIndex);
			user_id.Text = "Пользователь: " + user_data_client.ElementAt(list_user.SelectedIndex).USerNAME;
		}

		private void Disconnect_user_Click(object sender, EventArgs e)
		{

			DialogResult result = MessageBox.Show(
					"Вы уверенны что хотите выйти из системы этим пользователем ?",
						"Сообщение",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Information,
						MessageBoxDefaultButton.Button1);
			if (result == DialogResult.Yes)
			{
				user_data_client.RemoveAt(list_user.SelectedIndex);

				if (ACT.Registration_user_system(user_data_client))
				{
					up_user();
				}
				else
					up_user();
			}
		}
		public void up_user()
		{
			if (0 != ACT.Find_user_avtorization().Count())
			{
				user_data_client.Clear();
				user_data_client = ACT.Find_user_avtorization();
				list_user.Items.Clear();
				for (int i = 0; i < user_data_client.Count(); i++)
				{
					list_user.Items.Add(user_data_client.ElementAt(i).UserEmal.ToString());
				}
				list_user.SelectedIndex = user_data_client.Count - 1;
				user_id.Text = "Пользователь: " + user_data_client.ElementAt(list_user.SelectedIndex).USerNAME;
				Show();
			}
			else
			{
				Hide();
				MessageBox.Show("ВЫ не авторизировались!!! Повторите попытку");
				users form = new users(user_data_client);
				form.ShowDialog();
				up_user();
			}
		}
	}
}
