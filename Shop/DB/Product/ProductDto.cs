using MongoDB.Bson;

public class ProductDto
{
    public ObjectId Id { get; set; }
    public int Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public ProductDto(string title, int price, string description)
    {
        Price = price;
        Title = title;
        Description = description;
    }
}