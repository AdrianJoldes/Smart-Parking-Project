using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GenerateData
{
    public partial class Form1 : Form
    {
        string accountName = "mystorage008";
        string accountKey = "VnoP7qCYV4r5N22JhrlVUuPURS+R3+3JioE8XR3e5l9O/bRVN7bPMkMGGU7lpHCN1cIr8DmMuDxyN+TCMTG79g==";
        private CloudQueue queue;
        LocParcare[] locuriParcare = new LocParcare[96];
        Random rnd = new Random();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public Form1()
        {
            InitializeComponent();
            // Retrieve storage account from connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //  CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //DefaultEndpointsProtocol=https;AccountName=mystorage008;AccountKey=VnoP7qCYV4r5N22JhrlVUuPURS+R3+3JioE8XR3e5l9O/bRVN7bPMkMGGU7lpHCN1cIr8DmMuDxyN+TCMTG79g==;EndpointSuffix=core.windows.net
            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(accountName, accountKey), true);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            queue = queueClient.GetQueueReference("json-messages");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();



        }

        private void button_Generate_Data_Click(object sender, EventArgs e)
        {
            string jsonString;
            int r1 = rnd.Next(0, 2);
            if (r1 == 0)
            {
                jsonString = generateGoodData();
                textBox1.Text = "Date bune";

            }
            else
            {
                jsonString = generateBadData();
                textBox1.Text = "Date rele";
            }


            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(jsonString);
            queue.AddMessage(message);

        }

        private string generateBadData()
        {
            for (int i = 0; i < 96; i++)
            {
                int nr = rnd.Next(0, 2);
                locuriParcare[i] = new LocParcare();
                locuriParcare[i].Stare = "state";
                locuriParcare[i].Id ="200";
                string loc = "Locul " + locuriParcare[i].Id + " este " + locuriParcare[i].Stare;
                // Console.WriteLine(locuriParcare[i].Stare);
            }
            return JsonConvert.SerializeObject(locuriParcare);

        }

        private string generateGoodData()
        {
            for (int i = 0; i < 96; i++)
            {
                int nr = rnd.Next(0, 2);
                locuriParcare[i] = new LocParcare();
                locuriParcare[i].Stare = ((Stare)nr).ToString();
                locuriParcare[i].Id = i.ToString();
                string loc = "Locul " + locuriParcare[i].Id + " este " + locuriParcare[i].Stare;
                // Console.WriteLine(locuriParcare[i].Stare);
            }
            return JsonConvert.SerializeObject(locuriParcare);

        }
    }
}
