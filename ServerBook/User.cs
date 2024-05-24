using Domain;
using Microsoft.Identity.Client;
using NetworkLib;
using System.Collections.Concurrent;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.Json;
using TransportLib;
using TransportLib.DTO;

namespace ServerBook;
internal class User
{
    private readonly ICommand<RegistrationUserDto> _registration;
    private readonly ICommand<EntryUserDto> _entry;
    private readonly NetworkStream _stream; 
    public IReceive Read { get; set; }
    public ISender Writer { get; set; }
    public Guid UserId { get; set; }

    public User(NetworkStream stream, ISender sender, IReceive receive, ICommand<RegistrationUserDto> registration, ICommand<EntryUserDto> entry)
    {
        ArgumentNullException.ThrowIfNull(stream); 
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(receive);
        ArgumentNullException.ThrowIfNull(registration);
        _stream = stream;
        Writer= sender;
        Read = receive;
        _registration = registration;
        _entry = entry;
    }

    public void Initialization(ConcurrentDictionary<Guid, User> sessions)
    {
        Message message = Read.ReadMessage();
        switch (message.Type)
        {
            case TypeMessage.Registration:
            {
                    try
                    {
                        RegistrationUserDto? registration = JsonSerializer.Deserialize<RegistrationUserDto>(message.Data.ToString());
                        _registration.Execute(registration);
                        SendToken();
                        
                    }
                    catch (Exception ex)
                    {
                        Error(ex.Message, "Ошибка регистрации");
                    }
            }

            break;
            case TypeMessage.Entry:
                {
                    try
                    {
                        EntryUserDto user = JsonSerializer.Deserialize<EntryUserDto>(message.Data.ToString());
                        _entry.Execute(user);
                        SendToken();
                        
                    }
                    catch (Exception ex) 
                    {
                        Error(ex.Message, "Ошибка входа");
                    }
                }

            break;
            case TypeMessage.EntryToToken:
                {
                    if(message.Guid != null)
                    {
                        UserId = message.Guid.Value;
                        if(sessions.ContainsKey(UserId)) //добавление в сессию
                        {
                            Message result = new() { Type = TypeMessage.EntryToToken };
                            Writer.SendMessage(result);
                        }
                        else
                        {
                            Error($"{UserId} нет", "Ошибка входа");
                        }
                    }
                }
            break;
        }
    }

    private void SendToken()
    {
        UserId = Guid.NewGuid();
        Message answer = new Message();
        answer.Type = TypeMessage.Token;
        answer.Guid = UserId;
        Writer.SendMessage(answer);
    }
    public void Error(string message, string messageClient)
    {
        Console.WriteLine(message);
        Message error = new Message() { Type= TypeMessage.Error, Data= messageClient };
        Writer.SendMessage(error);
    }

}
