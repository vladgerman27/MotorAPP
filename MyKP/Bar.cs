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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyKP
{
    public partial class Bar : Form
    {
        public Bar()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Bar_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.BarCollection.Find(filter).ToCursor();
            var documentsStock = Intro.StockCollection.Find(filter).ToList();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Название", typeof(string));
            dataTable.Columns.Add("Категория", typeof(string));
            dataTable.Columns.Add("Цена, тг", typeof(int));
            dataTable.Columns.Add("Количество, шт", typeof(int));
            dataTable.Columns.Add("Сумма, тг", typeof(int));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var name = document["Название"].ToString();
                    var countB = document["Количество, шт"].ToInt32();
                    var documentStock = documentsStock.FirstOrDefault(d => d["Название"].ToString() == name);

                    if (documentStock != null)
                    {
                        int stockCount = documentStock["Количество, шт"].ToInt32();

                        if (stockCount < 0)
                        {
                            MessageBox.Show("Ошибка: количество товара не может быть отрицательным.");
                            return;
                        }
                    }

                    var row = dataTable.NewRow();
                    int cost = document["Цена, тг"].ToInt32();
                    int count = document["Количество, шт"].ToInt32();
                    int sum = cost*count;
                    row["ID"] = document["_id"].ToString();
                    row["Название"] = document["Название"].ToString();
                    row["Категория"] = document["Категория"].ToString();
                    row["Цена, тг"] = cost;
                    row["Количество, шт"] = count;
                    row["Сумма, тг"] = sum;
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
            }
            else if (Login.authIp == 2)
            {
                this.Hide();
                EmployeeMenu EmployeeMenu = new EmployeeMenu();
                EmployeeMenu.Show();
            } else if (Login.authIp == 1)
            {
                this.Hide();
                ClientMenu ClientMenu = new ClientMenu();
                ClientMenu.Show();
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string name = textBox1.Text;
            string category = textBox2.Text;
            int cost = Convert.ToInt32(textBox3.Text);
            int count = Convert.ToInt32(textBox4.Text);
            int sum = cost * count;

            if (Login.authIp == 3 || Login.authIp == 2)
            {
                var document = new BsonDocument
                {
                    { "Название", name },
                    { "Категория", category },
                    { "Цена, тг", cost },
                    { "Количество, шт", count },
                    { "Сумма, тг", sum },
                };

                var row = dataTable.NewRow();
                row["Название"] = name;
                row["Категория"] = category;
                row["Цена, тг"] = cost;
                row["Количество, шт"] = count;
                row["Сумма, тг"] = sum;
                dataTable.Rows.Add(row);

                Intro.BarCollection.InsertOne(document);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3 || Login.authIp == 2)
            {
                string id = textBox6.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = Intro.BarCollection.DeleteOne(filter);

                if (result.DeletedCount == 1)
                {
                    dataTable.Clear();
                    var newFilter = Builders<BsonDocument>.Filter.Empty;
                    var newDocumentsCursor = Intro.BarCollection.Find(newFilter).ToCursor();

                    while (newDocumentsCursor.MoveNext())
                    {
                        var batch = newDocumentsCursor.Current;
                        foreach (var document in batch)
                        {
                            var row = dataTable.NewRow();
                            int cost = document["Цена, тг"].ToInt32();
                            int count = document["Количество, шт"].ToInt32();
                            int sum = cost * count;
                            row["ID"] = document["_id"].ToString();
                            row["Название"] = document["Название"].ToString();
                            row["Категория"] = document["Категория"].ToString();
                            row["Цена, тг"] = cost;
                            row["Количество, шт"] = count;
                            row["Сумма, тг"] = sum;
                            dataTable.Rows.Add(row);
                        }
                    }

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Login.authIp == 3 || Login.authIp == 2)
            {
                BsonDocument document = Intro.BarCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
                string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (document != null && document.Contains(columnName))
                {
                    var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                    Intro.BarCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
                }
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bar Bar = new Bar();
            Bar.Show();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text;
            dataTable.DefaultView.RowFilter = string.Format("Название LIKE '%{0}%'", searchText);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox9.Text;
            dataTable.DefaultView.RowFilter = string.Format("Категория LIKE '%{0}%'", searchText);
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
