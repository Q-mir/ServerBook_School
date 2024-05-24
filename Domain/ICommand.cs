namespace Domain;
public interface ICommand<T>
{
    void Execute(T obj);
}




