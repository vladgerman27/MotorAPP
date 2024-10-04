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
    public partial class Tables : Form
    {
        public Tables()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Tabels_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.TablesCollection.Find(filter).ToCursor();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Количество стульев", typeof(int));
            dataTable.Columns.Add("Цена", typeof(int));
            dataTable.Columns.Add("Дата", typeof(DateTime));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var row = dataTable.NewRow();
                    row["ID"] = document["_id"].ToString();
                    row["Количество стульев"] = document["Количество стульев"].ToInt32();
                    row["Цена"] = document["Цена"].ToInt32();
                    row["Дата"] = Convert.ToDateTime(document["Дата"]);
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string count = textBox1.Text;
            int cost = Convert.ToInt32(textBox3.Text);
            DateTime date = dateTimePicker1.Value;

            var document = new BsonDocument
            {
                { "Количество стульев", count },
                { "Цена", cost },
                { "Дата", date },
            };

            var row = dataTable.NewRow();
            row["Количество стульев"] = count;
            row["Цена"] = cost;
            row["Дата"] = date;
            dataTable.Rows.Add(row);

            Intro.TablesCollection.InsertOne(document);
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tables Tables = new Tables();
            Tables.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3)
            {
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
            var result = Intro.TablesCollection.DeleteOne(filter);

            if (result.DeletedCount == 1)
            {
                dataTable.Clear();
                var newFilter = Builders<BsonDocument>.Filter.Empty;
                var newDocumentsCursor = Intro.TablesCollection.Find(newFilter).ToCursor();

                while (newDocumentsCursor.MoveNext())
                {
                    var batch = newDocumentsCursor.Current;
                    foreach (var document in batch)
                    {
                        var row = dataTable.NewRow();
                        row["ID"] = document["_id"].ToString();
                        row["Количество стульев"] = document["Количество стульев"].ToInt32();
                        row["Цена"] = document["Цена"].ToInt32();
                        row["Дата"] = Convert.ToDateTime(document["Дата"]);
                        dataTable.Rows.Add(row);
                    }
                }

                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BsonDocument document = Intro.TablesCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (document != null && document.Contains(columnName))
            {
                var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                Intro.TablesCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
