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
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Salary_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.SalaryCollection.Find(filter).ToCursor();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("ФИО", typeof(string));
            dataTable.Columns.Add("Оплата/мес", typeof(int));
            dataTable.Columns.Add("Больничный", typeof(int));
            dataTable.Columns.Add("Подоходный", typeof(int));
            dataTable.Columns.Add("Мед страховка", typeof(int));
            dataTable.Columns.Add("Итого", typeof(int));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var row = dataTable.NewRow();
                    int o = document["Оплата/мес"].ToInt32();
                    int b = document["Больничный"].ToInt32();
                    int p = document["Подоходный"].ToInt32();
                    int m = document["Мед страховка"].ToInt32();
                    int sum = document["Итого"].ToInt32();
                    bool sick = document["Болен"].ToBoolean();
                    if (sick == true)
                    {
                        sum = b - p - m;
                        o = 0;
                    } else if (sick == false)
                    {
                        sum = o - p;
                        b = 0;
                        m = 0;
                    }
                    row["ID"] = document["_id"].ToString();
                    row["ФИО"] = document["ФИО"].ToString();
                    row["Оплата/мес"] = o;
                    row["Больничный"] = b;
                    row["Подоходный"] = p;
                    row["Мед страховка"] = m;
                    row["Итого"] = sum;
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string name = textBox2.Text;
            int o = Convert.ToInt32(textBox5.Text);
            int b = o / 100 * 60;
            int p = o / 100 * 10;
            int m = o / 100 * 1;
            int sum = 0;
            bool sick = false;
            if (radioButton1.Checked == true)
            {
                sick = true;
                sum = b - p - m;
                o = 0;
            }else if (radioButton2.Checked == true)
            {
                sick = false;
                sum = o - p;
                b = 0;
                m = 0;
            }

            if (Login.authIp == 3)
            {
                var document = new BsonDocument
                {
                    { "ФИО", name },
                    { "Оплата/мес", o },
                    { "Больничный", b },
                    { "Подоходный", p },
                    { "Мед страховка", m },
                    { "Итого", sum },
                    { "Болен", sick }
                };

                var row = dataTable.NewRow();
                row["ФИО"] = document["ФИО"].ToString();
                row["Оплата/мес"] = o;
                row["Больничный"] = b;
                row["Подоходный"] = p;
                row["Мед страховка"] = m;
                row["Итого"] = sum;
                dataTable.Rows.Add(row);

                Intro.SalaryCollection.InsertOne(document);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3)
            {
                string id = textBox6.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = Intro.SalaryCollection.DeleteOne(filter);

                if (result.DeletedCount == 1)
                {
                    dataTable.Clear();
                    var newFilter = Builders<BsonDocument>.Filter.Empty;
                    var newDocumentsCursor = Intro.SalaryCollection.Find(newFilter).ToCursor();

                    while (newDocumentsCursor.MoveNext())
                    {
                        var batch = newDocumentsCursor.Current;
                        foreach (var document in batch)
                        {
                            var row = dataTable.NewRow();
                            int o = document["Оплата/мес"].ToInt32();
                            int b = o / 100 * 60;
                            int p = o / 100 * 10;
                            int m = o / 100 * 1;
                            int sum = 0;
                            bool sick = document["Болен"].ToBoolean();
                            if (radioButton1.Checked == true)
                            {
                                sick = true;
                                sum = b - p - m;
                                o = 0;
                            }
                            if (radioButton2.Checked == true)
                            {
                                sick = false;
                                sum = o - p;
                                b = 0;
                                m = 0;
                            }
                            row["ID"] = document["_id"].ToString();
                            row["ФИО"] = document["ФИО"].ToString();
                            row["Оплата/мес"] = o;
                            row["Больничный"] = b;
                            row["Подоходный"] = p;
                            row["Мед страховка"] = m;
                            row["Итого"] = sum;
                            dataTable.Rows.Add(row);
                        }
                    }

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BsonDocument document = Intro.SalaryCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (Login.authIp == 3)
            {
                if (document != null && document.Contains(columnName))
                {
                    var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                    Intro.SalaryCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
                }
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary Salary = new Salary();
            Salary.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3)
            {
                this.Hide();
                AdminMenu AdminMenu = new AdminMenu();
                AdminMenu.Show();
            }
            else if (Login.authIp == 2)
            {
                EmployeeMenu EmployeeMenu = new EmployeeMenu();
                EmployeeMenu.Show();
                this.Close();
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text;
            dataTable.DefaultView.RowFilter = string.Format("ФИО LIKE '%{0}%'", searchText);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
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
    }
}
