using EventsManager.Models;
using EventsManager.Services;

class Program
{
    static void Main()
    {
        UserRegistration userRegistration = new UserRegistration();
        UserLogin userLogin = new UserLogin(userRegistration);
        EventRegistration eventRegistration = new EventRegistration();
        EventSearch eventSearch = new EventSearch(eventRegistration);

        Console.WriteLine("Bem-vindo ao sistema de eventos!");

        User? loggedInUser = null;

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Cadastro");
            Console.WriteLine("[3] Sair");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    loggedInUser = userLogin.Login();
                    if (loggedInUser != null)
                    {
                        if (loggedInUser.ProfileType == Profile.Produtor)
                        {
                            Console.WriteLine("Bem-vindo! Você está logado como usuário Produtor.");
                            HandleProducerMenu(loggedInUser, eventRegistration, eventSearch);
                        }
                        else
                        {
                            Console.WriteLine("Bem-vindo! Você está logado como usuário Comum.");
                            HandleCommonMenu(loggedInUser, eventSearch);

                        }
                    }
                    break;
                case "2":
                    userRegistration.RegisterUser();
                    break;
                case "3":
                    Console.WriteLine("Até logo!");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void HandleProducerMenu(User loggedInUser, EventRegistration eventRegistration, EventSearch eventSearch)
    {
        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("[1] Cadastrar Evento");
            Console.WriteLine("[2] Localizar Eventos");
            Console.WriteLine("[3] Sair");

            string producerChoice = Console.ReadLine() ?? "";

            switch (producerChoice)
            {
                case "1":
                    eventRegistration.RegisterEvent();
                    break;
                case "2":
                    eventSearch.FindEvent();
                    break;
                case "3":
                    Console.WriteLine("Até logo!");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void HandleCommonMenu(User loggedInUser, EventSearch eventSearch)
    {
        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("[1] Localizar Eventos");
            Console.WriteLine("[2] Sair");

            string producerChoice = Console.ReadLine() ?? "";

            switch (producerChoice)
            {
                case "1":
                    eventSearch.FindEvent();
                    break;
                case "2":
                    Console.WriteLine("Até logo!");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

}