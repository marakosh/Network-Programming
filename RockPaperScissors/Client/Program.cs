using System.Net.Sockets;
using System.Net;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        try
        {

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8888;

            TcpClient client = new TcpClient();
            client.Connect(ip, port);

            Console.WriteLine("Подключено к серверу");

            byte[] data = Encoding.UTF8.GetBytes("start");
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);


            data = new byte[256];
            StringBuilder response = new StringBuilder();
            int bytes = stream.Read(data, 0, data.Length);
            response.Append(Encoding.UTF8.GetString(data, 0, bytes));


            Console.WriteLine("Сервер: " + response.ToString());


            stream.Close();
            client.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }
}