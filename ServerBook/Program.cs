namespace ServerBook;

internal class Program
{
    static void Main(string[] args)
    {
       new Server().Start("127.0.0.1", 405);
    }
}
