using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKP
{
    public partial class ClientMenu : Form
    {
        public ClientMenu()
        {
            InitializeComponent();
        }

        private void bands_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bands Bands = new Bands();
            Bands.Show();
        }

        private void bar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bar Bar = new Bar();
            Bar.Show();
        }

        private void cashbox_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cashbox Cashbox = new Cashbox();
            Cashbox.Show();
        }

        private void tables_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tables Tables = new Tables();
            Tables.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Login.authIp -= 1;
            this.Hide();
            Registr Registr = new Registr();
            Registr.Show();
        }

        private void info_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\2\\OneDrive\\Рабочий стол\\KP_C#\\MyKP\\info\\info.html"; 
            Process.Start(filePath);
        }
    }
}
