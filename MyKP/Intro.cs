using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace MyKP
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();
        }

        static public MongoClientSettings settings = MongoClientSettings.FromConnectionString("mongodb+srv://VladGerman:26417108@clubcluster.df11w4f.mongodb.net/?retryWrites=true&w=majority");
        static public MongoClient client = new MongoClient(settings);
        static public IMongoDatabase database = client.GetDatabase("ClubDB");
        static public IMongoCollection<BsonDocument> UsersCollection = database.GetCollection<BsonDocument>("Users");
        static public IMongoCollection<BsonDocument> EmployeesCollection = database.GetCollection<BsonDocument>("Employees");
        static public IMongoCollection<BsonDocument> BarCollection = database.GetCollection<BsonDocument>("Bar");
        static public IMongoCollection<BsonDocument> DeliveryCollection = database.GetCollection<BsonDocument>("Delivery");
        static public IMongoCollection<BsonDocument> StockCollection = database.GetCollection<BsonDocument>("Stock");
        static public IMongoCollection<BsonDocument> SalaryCollection = database.GetCollection<BsonDocument>("Salary");
        static public IMongoCollection<BsonDocument> BandsCollection = database.GetCollection<BsonDocument>("Bands");
        static public IMongoCollection<BsonDocument> TablesCollection = database.GetCollection<BsonDocument>("Tables");
        static public IMongoCollection<BsonDocument> CashboxCollection = database.GetCollection<BsonDocument>("Cashbox");

        private void Inrto_Load(object sender, EventArgs e)
        {
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            //var users = new List<BsonDocument>
            //{
            //    new BsonDocument
            //    {
            //        {"логин", "Admi" },
            //        {"пароль", "12" },
            //        {"роль", "админ" }
            //    },
            //};

            //UsersCollection.InsertMany(users);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registr Registr = new Registr();
            Registr.Show();
        }
    }
}
