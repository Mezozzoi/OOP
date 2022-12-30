using System.Collections.Generic;
using System.Text.Json;
using MongoDB.Bson;

public class CustomerDto
{
    public ObjectId Id { get; set; }
    public string Login { get; set; }
    public List<ObjectId> History { get; set; } = new ();
    public string Password { get; set; }
    public int Balance { get; set; }

    public CustomerDto(string login, string password)
    {
        this.Login = login;
        Password = password;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}