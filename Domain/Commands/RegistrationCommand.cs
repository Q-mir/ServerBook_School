using TransportLib.DTO;

namespace Domain.Commands;
public class RegistrationCommand : ICommand<RegistrationUserDto>
{
    private readonly IRepository _repository;

    public RegistrationCommand(IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public void Execute(RegistrationUserDto obj)
    {
        if(obj.Password1 != obj.Password2)
        {
            throw new ArgumentException("Password no equal!!!");
        }
        bool result=  _repository.RegistrationUser(obj.Login, obj.Password1);
        if (!result)
        {
            throw new ArgumentException("Login exist!");
        }
    }
}
