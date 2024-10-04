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

using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace MyKP
{
    public partial class Cashbox : Form
    {
        public Cashbox()
        {
            InitializeComponent();
        }


        public DataTable dataTable = new DataTable();

        private void Cashbox_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.CashboxCollection.Find(filter).ToCursor();
            var documentsBands = Intro.BandsCollection.Find(filter).ToList();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("ФИО", typeof(string));
            dataTable.Columns.Add("Группа", typeof(string));
            dataTable.Columns.Add("Цена", typeof(int));
            dataTable.Columns.Add("Количество", typeof(int));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var bandName = document["Группа"].ToString();
                    var cost = document["Цена"].ToInt32();
                    var documentBands = documentsBands.FirstOrDefault(d => d["Название"].ToString() == bandName);

                    if (documentBands != null)
                    {
                        document["Цена"] = documentBands["Стоимость билета"].ToInt32(); 
                    }

                    var row = dataTable.NewRow();
                    row["ID"] = document["_id"].ToString();
                    row["ФИО"] = document["ФИО"].ToString();
                    row["Группа"] = document["Группа"].ToString();
                    row["Цена"] = document["Цена"].ToInt32();
                    row["Количество"] = document["Количество"].ToInt32();
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string name = textBox1.Text;
            string band = textBox3.Text;
            int cost = 0;
            int count = Convert.ToInt32(textBox2.Text);

            var document = new BsonDocument
            {
                { "ФИО", name },
                { "Группа", band },
                { "Цена", cost },
                { "Количество", count },
            };

            var row = dataTable.NewRow();
            row["ФИО"] = name;
            row["Группа"] = band;
            row["Цена"] = cost;
            row["Количество"] = count;
            dataTable.Rows.Add(row);

            Intro.CashboxCollection.InsertOne(document);
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cashbox Cashbox = new Cashbox();
            Cashbox.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3) { 
                this.Hide();
                AdminMenu AdminMenu = new AdminMenu();
                AdminMenu.Show(); 
            } else if (Login.authIp == 1)
            {
                this.Hide();
                ClientMenu ClientMenu = new ClientMenu();
                ClientMenu.Show();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string id = textBox2.Text;

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = Intro.CashboxCollection.DeleteOne(filter);

            if (result.DeletedCount == 1)
            {
                dataTable.Clear();
                var newFilter = Builders<BsonDocument>.Filter.Empty;
                var newDocumentsCursor = Intro.CashboxCollection.Find(newFilter).ToCursor();

                while (newDocumentsCursor.MoveNext())
                {
                    var batch = newDocumentsCursor.Current;
                    foreach (var document in batch)
                    {
                        var row = dataTable.NewRow();
                        row["ID"] = document["_id"].ToString();
                        row["ФИО"] = document["ФИО"].ToString();
                        row["Группа"] = document["Группа"].ToString();
                        row["Цена"] = document["Цена"].ToInt32();
                        row["Количество"] = document["Количество"].ToInt32();
                        dataTable.Rows.Add(row);
                    }
                }

                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BsonDocument document = Intro.CashboxCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (document != null && document.Contains(columnName))
            {
                var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                Intro.CashboxCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text;
            dataTable.DefaultView.RowFilter = string.Format("ФИО LIKE '%{0}%'", searchText);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
