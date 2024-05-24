using TransportLib.DTO;

namespace Domain.Commands;

public class EntryCommand : ICommand<EntryUserDto>
{
    private readonly IRepository _repository;

    public EntryCommand(IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public void Execute(EntryUserDto obj)
    {
        if (obj.Login == string.Empty || obj.Password == string.Empty || obj.Login.Length < 3 || obj.Password.Length < 3) throw new ArgumentException("No entry!");
        bool result = _repository.ExistUser(obj.Login, obj.Password);
        if (!result)
        {
            throw new ArgumentException("Login none");
        }
    }
}