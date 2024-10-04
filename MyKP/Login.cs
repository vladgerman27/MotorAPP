using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        static public int authIp = 0;

        private void Login_Load(object sender, EventArgs e)
        {
            labelErr.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string user = textBox1.Text;
            var filter = Builders<BsonDocument>.Filter.Eq("логин", user);
            var result = Intro.UsersCollection.Find(filter).FirstOrDefault();

            if (result != null)
            {
                var pass = result.GetValue("пароль").AsString;
                var role = result.GetValue("роль").AsString;

                if (user == textBox1.Text && pass == textBox2.Text)
                {
                    if (role == "админ")
                    {
                        authIp += 3;
                        AdminMenu AdminMenu = new AdminMenu();
                        AdminMenu.Show();
                        this.Close();
                    }
                    if (role == "сотрудник")
                    {
                        authIp += 2;
                        EmployeeMenu EmployeeMenu = new EmployeeMenu();
                        EmployeeMenu.Show();
                        this.Close();
                    }
                    if (role == "клиент")
                    {
                        authIp += 1;
                        this.Hide();
                        ClientMenu ClientMenu = new ClientMenu();
                        ClientMenu.Show();
                    }
                }
            }
            else
            {
                labelErr.Visible = true;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registr Registr = new Registr();
            Registr.Show();
        }
    }
}
