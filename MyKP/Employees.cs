using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyKP
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        public void Employees_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.EmployeesCollection.Find(filter).ToCursor();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("ФИО", typeof(string));
            dataTable.Columns.Add("Дата рождения", typeof(DateTime));
            dataTable.Columns.Add("ИИН", typeof(string));
            dataTable.Columns.Add("Семейное положение", typeof(string));
            dataTable.Columns.Add("Должность", typeof(string));
            dataTable.Columns.Add("Адрес", typeof(string));
            dataTable.Columns.Add("Телефон", typeof(string));
            dataTable.Columns.Add("Дата начала работы", typeof(DateTime));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var row = dataTable.NewRow();
                    row["ID"] = document["_id"].ToString();
                    row["ФИО"] = document["ФИО"].ToString();
                    row["Дата рождения"] = Convert.ToDateTime(document["Дата рождения"]);
                    row["ИИН"] = document["ИИН"].ToString();
                    row["Семейное положение"] = document["Семейное положение"].ToString();
                    row["Должность"] = document["Должность"].ToString();
                    row["Адрес"] = document["Адрес"].ToString();
                    row["Телефон"] = document["Телефон"].ToString();
                    row["Дата начала работы"] = Convert.ToDateTime(document["Дата начала работы"]);
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3)
            {
                this.Hide();
                AdminMenu AdminMenu = new AdminMenu();
                AdminMenu.Show();
            } else if (Login.authIp == 2)
            {
                EmployeeMenu EmployeeMenu = new EmployeeMenu();
                EmployeeMenu.Show();
                this.Close();
            }
            
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string name = textBox1.Text;
            DateTime age = dateTimePicker1.Value;
            string code = textBox3.Text;
            string status = textBox4.Text;
            string job = textBox5.Text;
            string adress = textBox6.Text;
            string phone = textBox7.Text;
            DateTime start = dateTimePicker2.Value;


            if (Login.authIp == 3)
            {
                var document = new BsonDocument
                {
                    { "ФИО", name },
                    { "Дата рождения", age },
                    { "ИИН", code },
                    { "Семейное положение", status },
                    { "Должность", job },
                    { "Адрес", adress },
                    { "Телефон", phone },
                    { "Дата начала работы", start },
                };

                var row = dataTable.NewRow();
                row["ФИО"] = name;
                row["Дата рождения"] = age;
                row["ИИН"] = code;
                row["Семейное положение"] = status;
                row["Должность"] = job;
                row["Адрес"] = adress;
                row["Телефон"] = phone;
                row["Дата начала работы"] = start;
                dataTable.Rows.Add(row);

                Intro.EmployeesCollection.InsertOne(document);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3) { 
                string id = textBox2.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = Intro.EmployeesCollection.DeleteOne(filter);

                if (result.DeletedCount == 1)
                {
                    dataTable.Clear();
                    var newFilter = Builders<BsonDocument>.Filter.Empty;
                    var newDocumentsCursor = Intro.EmployeesCollection.Find(newFilter).ToCursor();

                    while (newDocumentsCursor.MoveNext())
                    {
                        var batch = newDocumentsCursor.Current;
                        foreach (var document in batch)
                        {
                            var row = dataTable.NewRow();
                            row["ID"] = document["_id"].ToString();
                            row["ФИО"] = document["ФИО"].ToString();
                            row["Дата рождения"] = Convert.ToDateTime(document["Дата рождения"]);
                            row["ИИН"] = document["ИИН"].ToString();
                            row["Семейное положение"] = document["Семейное положение"].ToString();
                            row["Должность"] = document["Должность"].ToString();
                            row["Адрес"] = document["Адрес"].ToString();
                            row["Телефон"] = document["Телефон"].ToString();
                            row["Дата начала работы"] = Convert.ToDateTime(document["Дата начала работы"]);
                            dataTable.Rows.Add(row);
                        }
                    }

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BsonDocument document = Intro.EmployeesCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);


            if (Login.authIp == 3)
            {
                if (document != null && document.Contains(columnName))
                {
                    var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                    Intro.EmployeesCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
                }
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employees Employees = new Employees();
            Employees.Show();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text;
            dataTable.DefaultView.RowFilter = string.Format("ФИО LIKE '%{0}%'", searchText);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox9.Text;
            dataTable.DefaultView.RowFilter = string.Format("Должность LIKE '%{0}%'", searchText);
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
    }
}
