using System;
using Helpers;

namespace Models
{
    public class Request
    {
        public string id { get; private set; }
        public string clientId { get; private set; }
        public string description { get; set; }
        public Client client { get; set; }
        public DateTime createdAt { get; private set; }

        public Request(string _clientId, string _description)
        {
            id = Functions.UUID();
            clientId = _clientId;
            description = _description;
            createdAt = DateTime.Now;
            client = null;
        }

        public Request(string _row)
        {
            String[] requestFields = _row.Split(Constants.SEPARATOR);
            id = requestFields[0];
            clientId = requestFields[1];
            description = requestFields[2];
            createdAt = Convert.ToDateTime(requestFields[3]);
            client = null;
        }

        public string formatForSave()
        {
            return string.Format(
                "{1}{0}{2}{0}{3}{0}{4}{0}",
                Constants.SEPARATOR,
                id,
                clientId,
                description,
                createdAt
            );
        }
    }
}
