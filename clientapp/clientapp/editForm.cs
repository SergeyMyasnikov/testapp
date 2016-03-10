using System;
using System.Windows.Forms;

namespace clientapp
{
    public partial class editForm : Form
    {
        public string client_name { get; set; }
        public string client_payment { get; set; }

        public editForm(object name, object payment)
        {
            InitializeComponent();

            tbName.Text = name.ToString();
            tbPayment.Text = payment.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.client_name = tbName.Text;
            this.client_payment = tbPayment.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
