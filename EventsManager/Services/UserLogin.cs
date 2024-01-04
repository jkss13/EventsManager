using System;
using System.Collections.Generic;
using System.Linq;
using EventsManager.Models;

namespace EventsManager.Services
{
    public class UserLogin
    {
        private readonly UserRegistration userRegistration;

        public UserLogin(UserRegistration userRegistration)
        {
            this.userRegistration = userRegistration;
        }
        
        private string ReadPassword()
        {
            return UserRegistration.ReadPassword();
        }

        public User? Login()
        {
            Console.WriteLine("** LOGIN DE USUÁRIO **");

            string email;
            bool isValidFormat;
            do
            {
                Console.Write("E-mail: ");
                email = Console.ReadLine() ?? "";

                isValidFormat = userRegistration.IsValidEmail(email, out _);

                if (!isValidFormat)
                {
                    Console.WriteLine("E-mail inválido. Forneça um e-mail válido (Ex.: fulano@gmail.com).");
                }
            } while (!isValidFormat);

            Console.Write("Senha: ");
            string password = ReadPassword();

            User? user = FindUserByEmail(email);

            if (user != null)
            {
                if (user.Password == password)
                {
                    Console.Write("Deseja salvar os dados de login? (S para Sim, qualquer outra tecla para Não): ");
                    string saveLoginChoice = Console.ReadLine()?.ToUpper() ?? "";

                    if (saveLoginChoice == "S")
                    {
                        userRegistration.SaveLogin(user);
                        Console.WriteLine($"Dados de login salvos para o usuário {user.Name} ({user.Email}).");
                        ShowUserMenu(user);
                        return user;
                    } 

                    Console.WriteLine("Login realizado com sucesso!");
                    ShowUserMenu(user);
                    return user;
                }
                else
                {
                    Console.WriteLine("Senha incorreta. Tente novamente.");
                    return Login();
                }
            }
            else
            {
                Console.WriteLine("E-mail não encontrado.");
                Console.Write("Deseja realizar cadastro? (S para Sim, qualquer outra tecla para Não): ");
                string registerChoice = Console.ReadLine()?.ToUpper() ?? "";
                if (registerChoice == "S")
                {
                    userRegistration.RegisterUser();
                    Console.WriteLine("Deseja realizar login? (S para Sim, qualquer outra tecla para Não): ");
                    string loginChoice = Console.ReadLine()?.ToUpper() ?? "";
                    if (loginChoice == "S")
                    {
                        return Login();
                    }
                }
            }

            return null;

        }

        private User? FindUserByEmail(string email)
        {
            List<User> users = userRegistration.GetUsers();

            if (users != null)
            {
                return users.FirstOrDefault(u => u.Email == email);
            }

            return null;
        }

        private void ShowUserMenu(User user)
        {
            Console.WriteLine("\nEscolha uma opção:");

            switch (user.ProfileType)
            {
                case Profile.Comum:
                    Console.WriteLine("[1] Procurar eventos");
                    Console.WriteLine("[2] Sair");
                    break;
                case Profile.Produtor:
                    Console.WriteLine("[1] Cadastrar evento");
                    Console.WriteLine("[2] Procurar eventos");
                    Console.WriteLine("[3] Sair");
                    break;
            }

            string userChoice = Console.ReadLine()?.ToUpper() ?? "";

            switch (user.ProfileType)
            {
                case Profile.Comum:
                    if (userChoice == "1")
                    {
                        EventSearch eventSearch = new EventSearch(new EventRegistration());
                        eventSearch.FindEvent();
                    }
                    else if (userChoice == "2")
                    {
                        return;
                    }
                    break;
                case Profile.Produtor:
                    if (userChoice == "1")
                    {
                        EventRegistration eventRegistration = new EventRegistration();
                        eventRegistration.RegisterEvent();
                    }
                    else if (userChoice == "2")
                    {
                        EventSearch eventSearch = new EventSearch(new EventRegistration());
                        eventSearch.FindEvent();
                    }
                    else if (userChoice == "3")
                    {
                        return;
                    }
                    break;
            }

            ShowUserMenu(user);
        }

    }
}
