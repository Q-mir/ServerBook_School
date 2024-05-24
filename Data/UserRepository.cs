using Domain;
namespace Data;
public class UserRepository : IRepository
{
    private readonly Connection _connect;
    public UserRepository(Connection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);
        _connect = connection;
    }

    public bool ExistUser(string login, string password)
    =>  _connect.Clients
                .Any(row => row.Login.Equals(login) && 
                            row.Password.Equals(password));
    
    public bool RegistrationUser(string login, string password)
    {
        bool result = _connect.Clients.Any(row => row.Login.Equals(login));
        if (!result)
        {
            try
            {
                Client client = new Client();
                client.Login = login;
                client.Password = password;
                _connect.Add(client);
                _connect.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }
}