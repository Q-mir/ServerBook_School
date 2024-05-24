namespace TransportLib;
public enum TypeMessage
{
    Error,

    //GET
    GetBook,
    GetAllBook,
    GetImage,

    //ADD
    AddBook,
    AddImage,

    //Server->Client
    Image,
    Book,
    Token,

    //Client->Server
    Registration,
    Entry,
    EntryToToken,
}
