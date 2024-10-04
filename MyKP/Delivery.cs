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
    public partial class Delivery : Form
    {
        public Delivery()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Delivery_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.DeliveryCollection.Find(filter).ToCursor();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Дата", typeof(DateTime));
            dataTable.Columns.Add("ID поставщика", typeof(string));
            dataTable.Columns.Add("Компания", typeof(string));
            dataTable.Columns.Add("Товар", typeof(string));
            dataTable.Columns.Add("Цена, тг", typeof(int));
            dataTable.Columns.Add("Количество, шт", typeof(int));
            dataTable.Columns.Add("Сумма, тг", typeof(int));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var row = dataTable.NewRow();
                    int cost = document["Цена, тг"].ToInt32();
                    int count = document["Количество, шт"].ToInt32();
                    int sum = cost * count;
                    row["ID"] = document["_id"].ToString();
                    row["Дата"] = Convert.ToDateTime(document["Дата"].ToString());
                    row["ID поставщика"] = document["ID поставщика"].ToString();
                    row["Компания"] = document["Компания"].ToString();
                    row["Товар"] = document["Товар"].ToString();
                    row["Цена, тг"] = cost;
                    row["Количество, шт"] = count;
                    row["Сумма, тг"] = sum;
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            DateTime date = dateTimePicker1.Value;
            string company = textBox2.Text;
            string product = textBox3.Text;
            int cost = Convert.ToInt32(textBox4.Text);
            int count = Convert.ToInt32(textBox5.Text);
            int sum = cost * count;

            if (Login.authIp == 3)
            {
                var document = new BsonDocument
                {
                    { "Дата", date },
                    { "ID поставщика", ObjectId.GenerateNewId() },
                    { "Компания", company },
                    { "Товар", product },
                    { "Цена, тг", cost },
                    { "Количество, шт", count },
                    { "Сумма, тг", sum },
                };

                    var stockDocument = new BsonDocument
                {
                    { "Название", product },
                    { "Количество, шт", count },
                    { "Срок годности", date },
                };

                var row = dataTable.NewRow();
                row["Дата"] = date;
                row["ID поставщика"] = ObjectId.GenerateNewId();
                row["Компания"] = company;
                row["Товар"] = product;
                row["Цена, тг"] = cost;
                row["Количество, шт"] = count;
                row["Сумма, тг"] = sum;
                dataTable.Rows.Add(row);

                Intro.DeliveryCollection.InsertOne(document);
                Intro.StockCollection.InsertOne(stockDocument);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3)
            {
                string id = textBox6.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = Intro.DeliveryCollection.DeleteOne(filter);

                if (result.DeletedCount == 1)
                {
                    dataTable.Clear();
                    var newFilter = Builders<BsonDocument>.Filter.Empty;
                    var newDocumentsCursor = Intro.DeliveryCollection.Find(newFilter).ToCursor();

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
                            row["Дата"] = Convert.ToDateTime(document["Дата"].ToString());
                            row["ID поставщика"] = document["ID поставщика"].ToString();
                            row["Компания"] = document["Компания"].ToString();
                            row["Товар"] = document["Товар"].ToString();
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
            BsonDocument document = Intro.DeliveryCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (Login.authIp == 3)
            {
                if (document != null && document.Contains(columnName))
                {
                    var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                    Intro.DeliveryCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
                }
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Delivery Delivery = new Delivery();
            Delivery.Show();
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
            dataTable.DefaultView.RowFilter = string.Format("Товар LIKE '%{0}%'", searchText);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox9.Text;
            dataTable.DefaultView.RowFilter = string.Format("Компания LIKE '%{0}%'", searchText);
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
