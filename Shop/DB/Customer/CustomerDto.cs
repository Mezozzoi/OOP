using System.Collections.Generic;
using MongoDB.Bson;

public class CustomerDto
{
    public ObjectId Id { get; set; }
    public string Login { get; set; }
    public List<ObjectId> History { get; set; } = new ();
    public string Password { get; set; }
    public int Balance { get; set; }
    public bool IsAdmin { get; set; } = false;

    public CustomerDto(string login, string password)
    {
        Login = login;
        Password = password;
    }
}