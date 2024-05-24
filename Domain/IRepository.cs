namespace Domain;
public interface IRepository
{
  bool ExistUser(string login, string password);
  bool RegistrationUser(string login, string password);
}
