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
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Login.authIp -= 3;
            this.Hide();
            Registr Registr = new Registr();
            Registr.Show();
        }

        private void employees_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employees Employees = new Employees();
            Employees.Show();
        }

        private void bar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bar Bar = new Bar();
            Bar.Show();
        }

        private void stock_Click(object sender, EventArgs e)
        {
            this.Hide();
            Stock Stock = new Stock();
            Stock.Show();
        }

        private void delivery_Click(object sender, EventArgs e)
        {
            this.Hide();
            Delivery Delivery = new Delivery();
            Delivery.Show();
        }

        private void salary_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary Salary = new Salary();
            Salary.Show();
        }

        private void bands_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bands Bands = new Bands();
            Bands.Show();
        }

        private void tables_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tables Tables = new Tables();
            Tables.Show();
        }

        private void cashbox_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cashbox Cashbox = new Cashbox();
            Cashbox.Show();
        }

        private void roles_Click(object sender, EventArgs e)
        {
            this.Hide();
            Roles Roles = new Roles();
            Roles.Show();
        }

        private void info_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\2\\OneDrive\\Рабочий стол\\KP_C#\\MyKP\\info\\info.html";
            Process.Start(filePath);
        }
    }
}
