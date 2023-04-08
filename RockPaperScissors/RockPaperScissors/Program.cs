using System.Net.Sockets;
using System.Net;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        try
        {
            // Устанавливаем IP-адрес и порт для прослушивания
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8888;

            // Создаем объект TcpListener и запускаем прослушивание
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            // Принимаем клиентские подключения и запускаем обработку
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Подключен клиент. Ожидание запроса...");

                // Обработка запроса
                byte[] data = new byte[256];
                NetworkStream stream = client.GetStream();
                int bytes = stream.Read(data, 0, data.Length);
                string request = Encoding.UTF8.GetString(data, 0, bytes);

                // Выбираем случайный вариант из "Камень-Ножницы-Бумага"
                string[] choices = { "Камень", "Ножницы", "Бумага" };
                Random rand = new Random();
                int index = rand.Next(choices.Length);
                string serverChoice = choices[index];

                // Определяем результат игры
                string result = "";
                if (request == "Камень")
                {
                    if (serverChoice == "Камень")
                    {
                        result = "Ничья";
                    }
                    else if (serverChoice == "Ножницы")
                    {
                        result = "Вы выиграли!";
                    }
                    else
                    {
                        result = "Вы проиграли...";
                    }
                }
                else if (request == "Ножницы")
                {
                    if (serverChoice == "Камень")
                    {
                        result = "Вы проиграли...";
                    }
                    else if (serverChoice == "Ножницы")
                    {
                        result = "Ничья";
                    }
                    else
                    {
                        result = "Вы выиграли!";
                    }
                }
                else if (request == "Бумага")
                {
                    if (serverChoice == "Камень")
                    {
                        result = "Вы выиграли!";
                    }
                    else if (serverChoice == "Ножницы")
                    {
                        result = "Вы проиграли...";
                    }
                    else
                    {
                        result = "Ничья";
                    }
                }
                // Отправляем ответ клиенту
                data = Encoding.UTF8.GetBytes(serverChoice + ", " + result);
                stream.Write(data, 0, data.Length);

                // Закрываем соединение
                stream.Close();
                client.Close();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }
}