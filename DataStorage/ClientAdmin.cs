using System.IO;
using Models;
using Helpers;

namespace DataStorage
{
    public class ClientAdmin
    {
        public const int MAX_CLIENTS = 50;
        private string db_filename;

        public ClientAdmin(string _db_filename)
        {
            db_filename = _db_filename;
            Stream FS = File.Open(db_filename, FileMode.OpenOrCreate);
            FS.Close();
        }

        public void AddClient(Client client)
        {
            using (StreamWriter SW = new StreamWriter(db_filename, true))
            {
                SW.WriteLine(client.formatForSave());
            }
        }

        public Client[] GetClients(out int clientCount)
        {
            Client[] clients = new Client[MAX_CLIENTS];

            using (StreamReader SR = new StreamReader(db_filename))
            {
                string row;
                clientCount = 0;

                while ((row = SR.ReadLine()) != null)
                {
                    clients[clientCount] = new Client(row);
                    clientCount += 1;
                }
            }

            return clients;
        }
    }
}
