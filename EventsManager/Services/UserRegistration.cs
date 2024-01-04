using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;
using EventsManager.Models;

namespace EventsManager.Services
{
    public class UserRegistration 
    
    {

        private static readonly List<User> users = new List<User>();
        private User? loggedInUser;

        public User? GetLoggedInUser()
        {
            return loggedInUser;
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }

        public bool UserExists(string email, string password)
        {
            return users.Any(user => user.Email == email && user.Password == password);
        }

        public void RegisterUser()
        {
            Console.WriteLine("** CADASTRO DE USUÁRIO **");

            int profileInput;
            do
            {
                Console.WriteLine("Qual tipo de usuário deseja cadastrar?\n\n[1] Comum\n[2] Produtor");
                string input = GetUserInput();

                if (!int.TryParse(input, out profileInput) || (profileInput != 1 && profileInput != 2))
                {
                    Console.WriteLine("Escolha inválida. Digite 1 para Comum ou 2 para Produtor.");
                }

            } while (profileInput != 1 && profileInput != 2);

            Profile profileType = (Profile)profileInput;

            Console.Write("Nome: ");
            string name = GetUserInput();

            string cpf;
            bool isValidCPF;
            bool cpfExists;
            do
            {
                Console.Write("CPF (somente números): ");
                cpf = GetUserInput();

                if (!IsNumeric(cpf) || cpf.Length != 11 || !IsValidCPF(cpf))
                {
                    Console.WriteLine("CPF inválido. Tente novamente.");
                    isValidCPF = false;
                    cpfExists = true;
                }
                else
                {
                    isValidCPF = true;
                    cpfExists = UserWithCPFExists(cpf);

                    if (cpfExists)
                    {
                        Console.WriteLine("Este CPF já está em uso.");
                    }
                }
            } while (!isValidCPF || cpfExists);
            
            string email;
            bool isValidEmail;
            bool emailExists;
            do
            {
                Console.Write("E-mail: ");
                email = GetUserInput();

                if (!IsValidEmail(email, out isValidEmail))
                {
                    Console.WriteLine("Este email não é válido. Forneça um email válido (Ex.: fulano@gmail.com).");
                    emailExists = true; 
                }
                else
                {
                    emailExists = UserWithEmailExists(email);

                    if (emailExists)
                    {   
                        Console.WriteLine("Este e-mail já está em uso. Escolha um novo e-mail.");
                    }
                }
            } while (!isValidEmail || emailExists);

            string password;
            do
            {
                Console.Write("Senha (8 a 10 caracteres, alfanumérica, pelo menos 1 maiúscula e 1 caractere especial): ");
                password = ReadPassword();

                if (!IsValidPassword(password))
                {
                    Console.WriteLine("A senha não está no formato correto. Tente novamente.");
                }
            } while (!IsValidPassword(password));

            User newUser = new User(profileType, name, long.Parse(cpf), email, password);

            users.Add(newUser);


            Console.WriteLine($"\nUsuário cadastrado com sucesso!");

            Console.Write("Deseja fazer login agora? (S para Sim, qualquer outra tecla para Não): ");
            string loginChoice = Console.ReadLine()?.ToUpper() ?? "";

            if (loginChoice == "S")
            {
                UserLogin userLogin = new UserLogin(this);
                userLogin.Login();
            }
        }

        public void SaveLogin(User user)
        {
            loggedInUser = user;
        }

        public void ClearLoggedInUser()
        {
            loggedInUser = null;
        }

        public void ShowUsers()
        {
            Console.WriteLine("\nLista de Usuários Registrados:");
            foreach (var user in users)
            {
                Console.WriteLine($"Tipo: {user.ProfileType}, Nome: {user.Name}, CPF: {user.CPF}, Email: {user.Email}");
            }
        }

        private bool UserWithEmailExists(string email)
        {
            return users.Any(u => u.Email == email);
        }

        public bool IsValidEmail(string email, out bool isValidFormat)
        {
            try
            {
                var addr = new MailAddress(email);
                isValidFormat = Regex.IsMatch(email, @"@gmail\.com|@outlook\.com|@yahoo\.com");
                return true;
            }
            catch
            {
                isValidFormat = false;
                return false;
            }
        }

        private bool IsNumeric(string input)
        {
            return long.TryParse(input, out _);
        }

        private bool IsValidCPF(string cpf)
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit = digit + remainder.ToString();

            return cpf.EndsWith(digit);
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$");
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        private bool UserWithCPFExists(string cpf)
        {
            return users.Any(u => u.CPF == long.Parse(cpf));
        }

    }
}