using System;
using DataStorage;
using Models;

namespace CRM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string client_db = System.Configuration.ConfigurationSettings.AppSettings["client_db"];
            ClientController clientController = new ClientController(client_db);

            string request_db = System.Configuration.ConfigurationSettings.AppSettings["request_db"];
            RequestController requestController = new RequestController(request_db);

            Client newClient = null;
            int clientCount = 0;
            Client[] clients = clientController.GetClients(out clientCount);

            Request newRequest = null;
            int requestCount = 0;
            Request[] requests = requestController.GetRequests(out requestCount);

            string opt;
            do
            {
                Console.WriteLine("---clients---");
                Console.WriteLine("CC. citire client de la tastatura");
                Console.WriteLine("AC. afisarea ultimului clilent introdus");
                Console.WriteLine("SC. salvare client în fisier");
                Console.WriteLine("FC. afisare clienti din fisier");

                Console.WriteLine("\n---requests---");
                Console.WriteLine("CR. citire cerere de la tastatura");
                Console.WriteLine("AR. afisarea ultimei cereri introduse");
                Console.WriteLine("SR. salvare cerere în fisier");
                Console.WriteLine("FR. afisare cereri din fisier\n");

                Console.WriteLine("X. inchidere program\n");
                Console.Write("alegeti o optiune: ");

                opt = Console.ReadLine();

                /* Whitespace */
                Console.WriteLine();

                switch(opt.ToUpper())
                {
                    /*
                     * Client Cases
                     */
                    case "CC":
                        newClient = ReadClientFromKeyboard();
                        break;

                    case "AC":
                        if (newClient != null) PrintClientToConsole(newClient);
                        else Console.WriteLine("introduceti un client mai intai");
                        break;

                    case "FC":
                        PrintClientsToConsole(clients, clientCount);
                        break;

                    case "SC":
                        clientController.AddClient(newClient);
                        newClient = null;
                        /* Update clients array */
                        clients = clientController.GetClients(out clientCount);
                        Console.WriteLine("clientul a fost adaugat in baza de date");
                        break;

                    /*
                     * Request Cases
                     */
                    case "CR":
                        newRequest = ReadRequestFromKeyboard(clients, clientCount);
                        break;

                    case "AR":
                        if (newRequest != null) PrintRequestToConsole(newRequest);
                        else Console.WriteLine("introduceti o cerere mai intai");
                        break;

                    case "FR":
                        PrintRequestsToConsole(requests, requestCount);
                        break;

                    case "SR":
                        requestController.AddRequest(newRequest);
                        newRequest = null;
                        /* Update requests array */
                        requests = requestController.GetRequests(out requestCount);
                        Console.WriteLine("cererea a fost adaugata in baza de date");
                        break;

                    case "X":
                        return;

                    default:
                        Console.WriteLine("optiune inexistenta");
                        break;
                }

                Console.WriteLine("\n[press any key to continue]");
                Console.ReadKey();
                Console.Clear();
                
            }
            while (opt.ToUpper() != "X");

        }

        public static Client ReadClientFromKeyboard()
        {
            Console.Write("new client name: ");
            string name = Console.ReadLine();

            Console.Write("new client surname: ");
            string surname = Console.ReadLine();

            Console.Write("new client email: ");
            string email = Console.ReadLine();

            Console.Write("new client phone: ");
            string phone = Console.ReadLine();

            return new Client(name, surname, email, phone);
        }

        public static void PrintClientToConsole(Client client)
        {
            Console.WriteLine("client: {0}", client.id);
            Console.WriteLine("{0} {1}", client.name, client.surname);
            Console.WriteLine("contact info: {0} / {1}", client.email, client.phone);
        }

        public static void PrintClientsToConsole(Client[] clients, int clientCount)
        {
            if(clientCount == 0)
            {
                Console.WriteLine("nu exista clienti");
                return;
            }

            for (int i = 0; i < clientCount; i += 1)
            {
                if(i > 0)
                {
                    Console.WriteLine("---");
                }
                PrintClientToConsole(clients[i]);
            }
        }

        public static Request ReadRequestFromKeyboard(Client[] clients, int clientCount)
        {
            Console.Write("new request description: ");
            string description = Console.ReadLine();
            for(int i = 0; i < clientCount; i += 1)
            {
                Console.Write("{0}. {1}   ", i + 1, clients[i].name);
            }

            Console.Write("\nnew request client: ");
            int clientIndex = int.Parse(Console.ReadLine()) - 1;

            return new Request(clients[clientIndex].id, description);
        }

        public static void PrintRequestToConsole(Request request)
        {
            Console.WriteLine("request: {0}", request.id);
            Console.WriteLine("{0}\nfrom: {1}", request.description, request.clientId);
        }

        public static void PrintRequestsToConsole(Request[] requests, int requestCount)
        {
            if (requestCount == 0)
            {
                Console.WriteLine("nu exista cereri");
                return;
            }

            for (int i = 0; i < requestCount; i += 1)
            {
                if (i > 0)
                {
                    Console.WriteLine("---");
                }
                PrintRequestToConsole(requests[i]);
            }
        }
    }
}
