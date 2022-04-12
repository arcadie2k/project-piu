using System.IO;
using Models;
using Helpers;

namespace DataStorage
{
    public class RequestAdmin
    {
        public const int MAX_REQUESTS = 50;
        private string db_filename;

        public RequestAdmin(string _db_filename)
        {
            db_filename = _db_filename;
            Stream FS = File.Open(db_filename, FileMode.OpenOrCreate);
            FS.Close();
        }

        public void AddRequest(Request request)
        {
            using (StreamWriter SW = new StreamWriter(db_filename, true))
            {
                SW.WriteLine(request.formatForSave());
            }
        }

        public Request[] GetRequests(out int requestCount)
        {
            Request[] requests = new Request[MAX_REQUESTS];

            using (StreamReader SR = new StreamReader(db_filename))
            {
                string row;
                requestCount = 0;

                while ((row = SR.ReadLine()) != null)
                {
                    requests[requestCount] = new Request(row);
                    requestCount += 1;
                }
            }

            return requests;
        }
    }
}
