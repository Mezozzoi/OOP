using System;
using BC = BCrypt.Net.BCrypt;
using MongoDB.Bson;

public class CustomerService
{
    private CustomerRepo customerRepo = new();
    private ProductRepo productRepo = new();

    public CustomerDto Login(string login, string password)
    {
        CustomerDto customer = customerRepo.GetByLogin(login);
        if (customer == null || !BC.Verify(password, customer.Password)) return null;
        return customer;
    }

    public void Register(string login, string password)
    {
        if (customerRepo.GetByLogin(login) != null)
            throw new ArgumentException($"Customer with specified name already exists: {login}");
        CustomerDto customerData = new CustomerDto(login, BC.HashPassword(password));
        customerRepo.Add(customerData);
    }

    public bool IsLoginAvailable(string login)
    {
        return customerRepo.GetByLogin(login) == null;
    }

    public CustomerDto AddToCart(ObjectId customerId, ObjectId productId)
    {
        CustomerDto customer = customerRepo.GetById(customerId);
        ProductDto product = productRepo.GetById(productId);

        if (customer.Balance < product.Price)
            throw new ArgumentOutOfRangeException(nameof(product.Price));
        customer.History.Add(product.Id);
        customer.Balance -= product.Price;
        return customerRepo.Update(customer);
    }

    public CustomerDto AddToBalance(ObjectId customerId, int amount)
    {
        CustomerDto customer = customerRepo.GetById(customerId);

        if (customer == null)
            throw new ArgumentException($"No customer with such id: {customerId}");
        customer.Balance += amount;
        return customerRepo.Update(customer);
    }
}