using System;
using System.Collections.Generic;
using MongoDB.Bson;


class ProductService
{
    private ProductRepo productRepo = new ProductRepo();

    public ProductDto GetById(ObjectId id)
    {
        return productRepo.GetById(id);
    }
    
    public ProductDto GetByTitle(string title)
    {
        return productRepo.GetByTitle(title);
    }
    
    public ProductDto Add(string title, int price, string description)
    {
        if (title == "" || productRepo.GetByTitle(title) != null)
            throw new ArgumentException($"Product title must be unique and not empty");
        ProductDto product = new ProductDto(title, price, description);
        return productRepo.Add(product);
    }

    public List<ProductDto> Catalog()
    {
        return (List<ProductDto>)productRepo.List();
    }

    public void Delete(ObjectId productId)
    {
        if (productRepo.GetById(productId) == null)
            throw new ArgumentException("Invalid product id");
        productRepo.Delete(productId);
    }

    public ProductDto Update(ProductDto product)
    {
        if (productRepo.GetById(product.Id) == null)
            throw new ArgumentException("Invalid product");
        return productRepo.Update(product);
    }
}
