using System;
using Helpers;

namespace Models
{
    public class Client
    {
        public string id { get; private set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime createdAt { get; private set; }

        public Client(string _name, string _surname, string _email, string _phone)
        {
            id = Functions.UUID();
            name = _name;
            surname = _surname;
            email = _email;
            phone = _phone;
            createdAt = DateTime.Now;
        }

        public Client(string _row)
        {
            String[] clientFields = _row.Split(Constants.SEPARATOR);
            id = clientFields[0];
            name = clientFields[1];
            surname = clientFields[2];
            email = clientFields[3];
            phone = clientFields[4];
            createdAt = Convert.ToDateTime(clientFields[5]);
        }

        public string formatForSave()
        {
            return string.Format(
                "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}",
                Constants.SEPARATOR,
                id, 
                name, 
                surname, 
                email, 
                phone, 
                createdAt
            );
        }
    }
}
