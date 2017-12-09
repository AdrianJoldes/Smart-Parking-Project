using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;

namespace MyTeamWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private CloudQueue queue;
        private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        LocParcare[] locuriParcare = new LocParcare[96];
        bool dateCorecte;

        public override void Run()
        {
            Trace.TraceInformation("MyTeamWorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("MyTeamWorkerRole has been started");

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            queue = queueClient.GetQueueReference("json-messages");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();


            try
            {
                builder.DataSource = "parkingserver-12345.database.windows.net";
                builder.UserID = "ServerAdmin";
                builder.Password = "Qwerty_12345";
                builder.InitialCatalog = "parking";

                Console.WriteLine("Conexiune realizata cu succes");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            // Create a message and add it to the queue.
            //CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            //queue.AddMessage(message);

            return result;


        }

        public override void OnStop()
        {
            Trace.TraceInformation("MyTeamWorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("MyTeamWorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            //Trace.TraceInformation("-------------------------------------- ASYNC "+ DateTime.Now.ToString());

            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                
                Trace.TraceInformation("Working");


                // Get the next message
                try
                {
                    CloudQueueMessage retrievedMessage = queue.GetMessage();
                    string jsonParking = retrievedMessage.AsString;
                    locuriParcare = JsonConvert.DeserializeObject<LocParcare[]>(jsonParking);


                        dateCorecte = true;
                    foreach (var loc in locuriParcare)
                    {
                        var id = Int64.Parse(loc.Id);
                        var stare = loc.Stare;


                        if (!(((id >= 0 && id <= 95)) && ((stare.Equals("ocupat") || stare.Equals("liber")))))
                        {
                            dateCorecte = false;
                            break;
                        }

                    }

                    if (dateCorecte)
                    {
                        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                        {
                            Trace.TraceInformation("\nInsert into database");
                            Console.WriteLine("\nInsert into database");
                            Console.WriteLine("=========================================\n");

                            StringBuilder sb = new StringBuilder();

                            sb.Append("insert into Table values(jsonString)");


                            String delete = "truncate table parking";
                            using (SqlCommand command = new SqlCommand(delete, connection))
                            {
                                connection.Open();
                                int result = command.ExecuteNonQuery();

                                // Check Error
                                if (result > 0)
                                    Trace.TraceInformation("Succesfully clear Database!");

                            }


                        }


                        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                        {
                            String sql = "insert into parking values(@parcare)";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@parcare", jsonParking);
                                connection.Open();
                                int result = command.ExecuteNonQuery();

                                // Check Error
                                if (result < 0)
                                    Trace.TraceInformation("Error inserting data into Database!");
                                else
                                    Trace.TraceInformation("Succesfully insert into Database");
                            }
                        }
                    }
                    else
                    {
                        Trace.TraceError("Date incorecte");
                    }

                    //Process the message in less than 30 seconds, and then delete the message
                    queue.DeleteMessage(retrievedMessage);

                }
                catch (Exception e)
                {
                    Trace.TraceInformation("No more messages");
                }
                await Task.Delay(1000);
            }
        }
    }
}
