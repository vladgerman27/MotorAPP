using Microsoft.Office.Interop.Excel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DataTable = System.Data.DataTable;

namespace MyKP
{
    public partial class Roles : Form
    {
        public Roles()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Roles_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.UsersCollection.Find(filter).ToCursor();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Логин", typeof(string));
            dataTable.Columns.Add("Пароль", typeof(string));
            dataTable.Columns.Add("Роль", typeof(string));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var row = dataTable.NewRow();
                    row["ID"] = document["_id"].ToString();
                    row["Логин"] = document["логин"].ToString();
                    row["Пароль"] = document["пароль"].ToString();
                    row["Роль"] = document["роль"].ToString();
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string login = textBox1.Text;  
            string password = textBox3.Text;
            string role = textBox4.Text;

            var document = new BsonDocument
            {
                { "логин", login },
                { "пароль", password },
                { "роль", role },
            };

            var row = dataTable.NewRow();
            row["Логин"] = login;
            row["Пароль"] = password;
            row["Роль"] = role;
            dataTable.Rows.Add(row);

            Intro.UsersCollection.InsertOne(document);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string id = textBox2.Text;

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = Intro.UsersCollection.DeleteOne(filter);

            if (result.DeletedCount == 1)
            {
                dataTable.Clear();
                var newFilter = Builders<BsonDocument>.Filter.Empty;
                var newDocumentsCursor = Intro.UsersCollection.Find(newFilter).ToCursor();

                while (newDocumentsCursor.MoveNext())
                {
                    var batch = newDocumentsCursor.Current;
                    foreach (var document in batch)
                    {
                        var row = dataTable.NewRow();
                        row["ID"] = document["_id"].ToString();
                        row["Логин"] = document["логин"].ToString();
                        row["Пароль"] = document["пароль"].ToString();
                        row["Роль"] = document["роль"].ToString();
                        dataTable.Rows.Add(row);
                    }
                }

                dataGridView1.DataSource = dataTable;
            }
        }

        private void exelBtn_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();

            ExcelApp.Application.Workbooks.Add(Type.Missing);

            ExcelApp.Columns.ColumnWidth = 15;

            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                ExcelApp.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 2, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }

            ExcelApp.Visible = true;
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Roles Roles = new Roles();
            Roles.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminMenu AdminMenu = new AdminMenu();
            AdminMenu.Show();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BsonDocument document = Intro.UsersCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (document != null && document.Contains(columnName))
            {
                var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                Intro.UsersCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text;
            dataTable.DefaultView.RowFilter = string.Format("логин LIKE '%{0}%'", searchText);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox9.Text;
            dataTable.DefaultView.RowFilter = string.Format("роль LIKE '%{0}%'", searchText);
        }
    }
}
