using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;
using EventsManager.Models;
using SkiaSharp;


namespace EventsManager.Services
{
    public class EventRegistration
    {
        private static readonly List<Event> events = new List<Event>();

        public List<Event> GetEvents()
        {
            return events;
        }

        public void RegisterEvent()
        {
            Console.WriteLine("** CADASTRO DE EVENTO **");

            string title;
            do
            {
                Console.Write("Título: ");
                title = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("O título é um campo obrigatório. Por favor, preencha-o.");
                }
                else if (TitleExists(title))
                {
                    Console.WriteLine("Já existe um evento com esse título. Por favor, escolha um título único.");
                }
            } while (string.IsNullOrEmpty(title) || TitleExists(title));

            Console.Write("Categoria do Evento: ");
            string category = Console.ReadLine() ?? "";

            DateTime date;
            do
            {
                Console.Write("Data do evento (dd/mm/yyyy): ");
                string dateInput = Console.ReadLine() ?? "";

                if (DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Formato de data inválido. Tente novamente informando um formato válido (Ex.: 01/01/2024).");
                }
            } while (true);

            TimeSpan time;
            do
            {
                Console.Write("Hora do evento (HH:mm): ");
                string timeInput = Console.ReadLine() ?? "";

                if (IsValidTimeFormat(timeInput))
                {
                    if (DateTime.TryParseExact(timeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTimeResult))
                    {
                        time = dateTimeResult.TimeOfDay;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Formato de hora inválido. Tente novamente informando um formato válido (Ex.: 12:45).");
                    }
                } 
                else
                {
                    Console.WriteLine("Formato de hora inválido. Tente novamente informando um formato válido (Ex.: 12:45).");
                }
            } while (true);

            string place;
            do
            {
                Console.Write("Local do Evento: ");
                place = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(place))
                {
                    Console.WriteLine("O local é um campo obrigatório. Por favor, preencha-o.");
                }
            } while (string.IsNullOrEmpty(place));

            Console.Write("Descrição do Evento: ");
            string description = Console.ReadLine() ?? "";

            SKBitmap cover = new SKBitmap();
            do
            {
                Console.Write("URL da imagem de capa do evento: ");
                string coverUrl = Console.ReadLine() ?? "";

                if (IsImageUrl(coverUrl) || IsLocalFilePath(coverUrl))
                {
                    cover = LoadBitmapFromUrl(coverUrl);
                    break;
                }
                else
                {
                    Console.WriteLine("Caminho da imagem inválido. Forneça uma URL válida ou um caminho de arquivo local.");
                }
            } while (true);

            Event newEvent = new Event(title, category, date, time, place, description, cover);
            events.Add(newEvent);

            Console.WriteLine("\nEvento cadastrado com sucesso!");
        }


        private SKBitmap LoadBitmapFromUrl(string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    return SKBitmap.Decode(url);
                }
                else
                {
                    Console.WriteLine("Caminho da imagem não pode ser vazio.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar a imagem: {ex.Message}");
            }

            return new SKBitmap();
        }

        private bool IsImageUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult))
            {
                return uriResult != null && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }
    
        private bool IsLocalFilePath(string path)
        {
            if (Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out Uri? uriResult) &&
                uriResult != null)
            {
                if (uriResult.IsAbsoluteUri)
                {
                    return uriResult.IsFile;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private bool TitleExists(string title)
        {
            return events.Any(e => e.EventTitle.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        bool IsValidTimeFormat(string timeInput)
        {
            if (timeInput.Contains(":"))
            {
                string[] timeParts = timeInput.Split(':');
                if (timeParts.Length == 2 && int.TryParse(timeParts[0], out _) && int.TryParse(timeParts[1], out _))
                {
                    return true;
                }
            }

            return false;
        }

    }
}