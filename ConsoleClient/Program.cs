using NetworkLib;
using System.Net.Sockets;
using TransportLib;
using TransportLib.DTO;

namespace ConsoleClient;

internal class Program //top
{
    static void Main(string[] args)
    {
        string login = Console.ReadLine();
        string password = Console.ReadLine();

        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect("127.0.0.1", 405);
        if (tcpClient.Connected)
        {
            NetworkStream network = tcpClient.GetStream();
            ISender sender = new Sender(new StreamWriter(network));
            IReceive receive = new Reader(new StreamReader(network));

            Message message = new Message();
            message.Type = TypeMessage.EntryToToken;
            message.Guid = Guid.Parse("c3dda11c-ae42-4b9e-81b7-c7f5a9999e8c");// вход по токену
            

            sender.SendMessage(message);

            Message result = receive.ReadMessage();
            if(result.Type != TypeMessage.Error)
            {
                Console.WriteLine(result.Guid);
                Console.WriteLine("good");
            }
            else
            {
                Console.WriteLine(result.Data.ToString());
            }


            Console.ReadLine();
        }

    }
}
