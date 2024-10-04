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

namespace MyKP
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Stock_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.StockCollection.Find(filter).ToCursor();
            var documentsDelivery = Intro.DeliveryCollection.Find(filter).ToList();
            var documentsBar = Intro.BarCollection.Find(filter).ToList();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Название", typeof(string));
            dataTable.Columns.Add("Количество, шт", typeof(int));
            dataTable.Columns.Add("Срок годности", typeof(DateTime));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var name = document["Название"].ToString();
                    var count = document["Количество, шт"].ToInt32();
                    var date = Convert.ToDateTime(document["Срок годности"].ToString());

                    var documentDelivery = documentsDelivery.FirstOrDefault(d => d["Товар"].ToString() == name);
                    var documentBar = documentsBar.FirstOrDefault(d => d["Название"].ToString() == name);
                    
                    if (documentDelivery != null)
                    {
                        count = documentDelivery["Количество, шт"].ToInt32();
                        documentsDelivery.Remove(documentDelivery);
                    }

                    if (documentBar != null)
                    {
                        int barCount = documentBar["Количество, шт"].ToInt32();
                        count = count - barCount;
                        documentsBar.Remove(documentBar);

                        if (count < 0)
                        {
                            MessageBox.Show("Ошибка: количество товара не может быть отрицательным.");
                            return;
                        }
                    }


                    var row = dataTable.NewRow();
                    row["ID"] = document["_id"].ToString();
                    row["Название"] = name;
                    row["Количество, шт"] = count;
                    row["Срок годности"] = date;
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string name = textBox2.Text;
            int count = Convert.ToInt32(textBox5.Text);
            DateTime date = dateTimePicker1.Value;

            var document = new BsonDocument
            {
                { "Название", name },
                { "Количество, шт", count },
                { "Срок годности", date },
            };

            var row = dataTable.NewRow();
            row["Название"] = document["Название"].ToString();
            row["Количество, шт"] = count;
            row["Срок годности"] = Convert.ToDateTime(document["Срок годности"].ToString());
            dataTable.Rows.Add(row);

            Intro.StockCollection.InsertOne(document);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string id = textBox6.Text;

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = Intro.StockCollection.DeleteOne(filter);

            if (result.DeletedCount == 1)
            {
                dataTable.Clear();
                var newFilter = Builders<BsonDocument>.Filter.Empty;
                var newDocumentsCursor = Intro.StockCollection.Find(newFilter).ToCursor();
                var documentsDelivery = Intro.DeliveryCollection.Find(filter).ToList();

                while (newDocumentsCursor.MoveNext())
                {
                    var batch = newDocumentsCursor.Current;
                    foreach (var document in batch)
                    {
                        var name = document["Название"].ToString();
                        var count = document["Количество, шт"].ToInt32();
                        var date = Convert.ToDateTime(document["Срок годности"].ToString());

                        var documentDelivery = documentsDelivery.FirstOrDefault(d => d["Товар"].ToString() == name);
                        if (documentDelivery != null)
                        {
                            count = documentDelivery["Количество, шт"].ToInt32();
                            documentsDelivery.Remove(documentDelivery);
                        }

                        var row = dataTable.NewRow();
                        row["ID"] = document["_id"].ToString();
                        row["Название"] = name;
                        row["Количество, шт"] = count;
                        row["Срок годности"] = date;
                        dataTable.Rows.Add(row);
                    }
                }

                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BsonDocument document = Intro.StockCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (document != null && document.Contains(columnName))
            {
                var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                Intro.StockCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Stock Stock = new Stock();
            Stock.Show();
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
            dataTable.DefaultView.RowFilter = string.Format("Название LIKE '%{0}%'", searchText);
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
