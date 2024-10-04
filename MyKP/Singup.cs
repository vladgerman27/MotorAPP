using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKP
{
    public partial class Singup : Form
    {
        public Singup()
        {
            InitializeComponent();
        }

        private void Singup_Load(object sender, EventArgs e)
        {
            labelErr.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string username = textBox1.Text;
            string pass = textBox2.Text;

            var document = new BsonDocument
            {
                { "логин", username },
                { "пароль", pass },
                { "роль", "клиент"}
            };

            Intro.UsersCollection.InsertOne(document);

            Login.authIp += 1;
            this.Hide();
            ClientMenu ClientMenu = new ClientMenu();
            ClientMenu.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registr Registr = new Registr();
            Registr.Show();
        }
    }
}
