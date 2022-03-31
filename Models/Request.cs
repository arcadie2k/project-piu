using System;
using Helpers;

namespace Models
{
    public class Request
    {
        public string id;
        public string clientId;
        public string description;
        public Client client = null;
        public DateTime createdAt = DateTime.Now;

        public Request(string _clientId, string _description)
        {
            clientId = _clientId;
            description = _description;
        }

        public Request(string _row)
        {
            String[] requestFields = _row.Split(Constants.SEPARATOR);
            id = requestFields[0];
            clientId = requestFields[1];
            description = requestFields[2];
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
