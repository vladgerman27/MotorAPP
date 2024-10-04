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
    public partial class Bands : Form
    {
        public Bands()
        {
            InitializeComponent();
        }

        public DataTable dataTable = new DataTable();

        private void Bands_Load(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var documentsCursor = Intro.BandsCollection.Find(filter).ToCursor();

            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Название", typeof(string));
            dataTable.Columns.Add("Дата", typeof(DateTime));
            dataTable.Columns.Add("Длительность, мин", typeof(int));
            dataTable.Columns.Add("Оплата", typeof(int));
            dataTable.Columns.Add("Описание", typeof(string));
            dataTable.Columns.Add("Стоимость билета", typeof(int));

            while (documentsCursor.MoveNext())
            {
                var batch = documentsCursor.Current;
                foreach (var document in batch)
                {
                    var row = dataTable.NewRow();
                    row["ID"] = document["_id"].ToString();
                    row["Название"] = document["Название"].ToString();
                    row["Дата"] = Convert.ToDateTime(document["Дата"]);
                    row["Длительность, мин"] = document["Длительность, мин"].ToInt32();
                    row["Оплата"] = document["Оплата"].ToInt32();
                    row["Описание"] = document["Описание"].ToString();
                    row["Стоимость билета"] = document["Стоимость билета"].ToInt32();
                    dataTable.Rows.Add(row);
                }
            }

            dataGridView1.DataSource = dataTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Intro.settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            string name = textBox1.Text;
            DateTime date = dateTimePicker1.Value;
            int time = Convert.ToInt32(textBox3.Text);
            int salary = Convert.ToInt32(textBox4.Text);
            string description = textBox5.Text;
            int cost = Convert.ToInt32(textBox6.Text);

            if (Login.authIp == 3)
            {
                var document = new BsonDocument
                {
                    { "Название", name },
                    { "Дата", date },
                    { "Длительность, мин", time },
                    { "Оплата", salary },
                    { "Описание", description },
                    { "Стоимость билета", cost },
                };

                var row = dataTable.NewRow();
                row["Название"] = name;
                row["Дата"] = date;
                row["Длительность, мин"] = time;
                row["Оплата"] = salary;
                row["Описание"] = description;
                row["Стоимость билета"] = cost;
                dataTable.Rows.Add(row);

                Intro.BandsCollection.InsertOne(document);
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bands Bands = new Bands();
            Bands.Show();
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
            else if (Login.authIp == 1)
            {
                this.Hide();
                ClientMenu ClientMenu = new ClientMenu();
                ClientMenu.Show();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (Login.authIp == 3)
            {
                string id = textBox2.Text;

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = Intro.BandsCollection.DeleteOne(filter);

                if (result.DeletedCount == 1)
                {
                    dataTable.Clear();
                    var newFilter = Builders<BsonDocument>.Filter.Empty;
                    var newDocumentsCursor = Intro.BandsCollection.Find(newFilter).ToCursor();

                    while (newDocumentsCursor.MoveNext())
                    {
                        var batch = newDocumentsCursor.Current;
                        foreach (var document in batch)
                        {
                            var row = dataTable.NewRow();
                            row["ID"] = document["_id"].ToString();
                            row["Название"] = document["Название"].ToString();
                            row["Дата"] = Convert.ToDateTime(document["Дата"]);
                            row["Длительность, мин"] = document["Длительность, мин"].ToInt32();
                            row["Оплата"] = document["Оплата"].ToInt32();
                            row["Описание"] = document["Описание"].ToString();
                            row["Стоимость билета"] = document["Стоимость билета"].ToInt32();
                            dataTable.Rows.Add(row);
                        }
                    }

                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            BsonDocument document = Intro.BandsCollection.Find(new BsonDocument()).Skip(e.RowIndex).FirstOrDefault();
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            BsonValue newValue = BsonValue.Create(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            if (Login.authIp == 3)
            {
                if (document != null && document.Contains(columnName))
                {
                    var update = Builders<BsonDocument>.Update.Set(columnName, newValue);
                    Intro.BandsCollection.UpdateOne(new BsonDocument("_id", document["_id"]), update);
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox8.Text;
            dataTable.DefaultView.RowFilter = string.Format("Название LIKE '%{0}%'", searchText);
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
