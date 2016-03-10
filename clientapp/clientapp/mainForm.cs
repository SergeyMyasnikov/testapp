using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace clientapp
{
    public partial class mainForm : Form
    {

        public mainForm()
        {
            InitializeComponent();

            dgwClients.ColumnCount = 3;
            dgwClients.Columns[0].Name = "id";
            dgwClients.Columns[1].Name = "name";
            dgwClients.Columns[2].Name = "payment";

            FillDataGridView(ServerConnection.GetAll());
        }

        private void FillDataGridView(List<Client> client_list)
        {
            for (int i = 0; i < client_list.Count; i++)
            {
                DataGridViewRow new_row = new DataGridViewRow();
                new_row.CreateCells(dgwClients);

                new_row.Cells[0].Value = client_list[i].id;
                new_row.Cells[1].Value = client_list[i].name;
                new_row.Cells[2].Value = client_list[i].payment;

                dgwClients.Rows.Add(new_row);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgwClients.SelectedRows.Count == 1)
            {
                editForm ef = new editForm("", "");
                var result = ef.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Client new_client = new Client();
                    
                    new_client.name = ef.client_name;
                    new_client.payment = Convert.ToDouble(ef.client_payment);
                    new_client.creation_date = DateTime.Now;

                    int id = ServerConnection.Add(new_client);
                    if (id != 0)
                    {
                        DataGridViewRow new_row = new DataGridViewRow();
                        new_row.CreateCells(dgwClients);

                        new_row.Cells[0].Value = id;
                        new_row.Cells[1].Value = new_client.name;
                        new_row.Cells[2].Value = new_client.payment;

                        dgwClients.Rows.Add(new_row);
                    }
                    else
                    {
                        MessageBox.Show("Server error");
                    }
                }
            }
            else MessageBox.Show("Fail selection");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgwClients.SelectedRows.Count == 1)
            {
                var name = dgwClients.SelectedRows[0].Cells[1].Value;
                var payment = dgwClients.SelectedRows[0].Cells[2].Value;

                editForm ef = new editForm(name, payment);
                var result = ef.ShowDialog();

                if (result == DialogResult.OK)
                {
                    int selected_index = dgwClients.SelectedRows[0].Index;
                    var id = dgwClients.Rows[selected_index].Cells[0].Value;

                    Client updated_client = new Client();
                    updated_client.id = Convert.ToInt32(id);
                    updated_client.name = ef.client_name;
                    updated_client.payment = Convert.ToDouble(ef.client_payment);

                    List<Client> result_list = ServerConnection.Update(updated_client);
                    if (result_list != null)
                    {
                        dgwClients.Rows.Clear();
                        FillDataGridView(result_list);
                    }
                }
            }
            else MessageBox.Show("Fail selection");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgwClients.SelectedRows.Count == 1)
            {
                int selected_index = dgwClients.SelectedRows[0].Index;
                var id = dgwClients.Rows[selected_index].Cells[0].Value;

                List<Client> result_list = ServerConnection.Delete(id);
                if (result_list != null)
                {
                    dgwClients.Rows.Clear();
                    FillDataGridView(result_list);
                }
            }
            else MessageBox.Show("Fail selection");
        }
    }
}
