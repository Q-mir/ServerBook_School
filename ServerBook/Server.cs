using System.Net;
using System.Net.Sockets;
using Data;
using Domain;
using Domain.Commands;
using TransportLib;
using TransportLib.DTO;
using NetworkLib;
using System.Collections.Concurrent;

namespace ServerBook;
public class Server
{
    private ConcurrentDictionary <Guid, User> sessions { get; set; } = new ConcurrentDictionary<Guid, User>();
    private bool _serverStart = false;
    public void Start(string ip, int port)
    {
        _serverStart = true;
        IPAddress ipAddress = IPAddress.Parse(ip);
        IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
        TcpListener listener = new TcpListener(endPoint);
        listener.Start();
        while (_serverStart)
        {
            TcpClient tcpClient = listener.AcceptTcpClient();
            Task.Run(()=>NewClientRoom(tcpClient));
        }
    }

    private void NewClientRoom(TcpClient tcpClient)
    {
        NetworkStream networkStream = tcpClient.GetStream();
        Connection connection = new Connection();
        IRepository repository = new UserRepository(connection);
        Domain.ICommand<RegistrationUserDto> command = new RegistrationCommand(repository);
        Domain.ICommand<EntryUserDto> entrycommand = new EntryCommand(repository);
        ISender sender = new Sender(new StreamWriter(networkStream));
        IReceive receive = new Reader(new StreamReader(networkStream));
        User user = new User(networkStream, sender, receive, command, entrycommand);
        user.Initialization(sessions);
        //тут развилка
        sessions.TryGetValue(Guid.Parse("0000-000-00-0"), out User? oldUser);
        oldUser.Writer.SendMessage(new Message());
    }

    public void Stop()
    {
        _serverStart = false;
    }
}
