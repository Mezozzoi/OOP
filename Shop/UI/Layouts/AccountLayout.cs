using System;
using System.Collections.Generic;
using System.Linq;

class AccountLayout : Layout
{
    public AccountLayout(Context context) : base(context) {
        if (context.customer.IsAdmin)
        {
            actions.Add("Add Product", AddProduct);
            actions.Add("Edit Product Info", EditProduct);
        } else
        {
            actions.Add("History", History);
            actions.Add("Catalog", Catalog);
            actions.Add("Add Balance", Balance);
        }

        if (context.customer == null) Environment.Exit(0);

        exit = new StartLayout(context);
    }
    
    public override Layout Init()
    {
        Console.Clear();
        if (context.customer.IsAdmin)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Admin: {context.customer.Login}");
            Console.ResetColor();
        } else
        {
            Console.WriteLine($"User: {context.customer.Login}");
            Console.WriteLine($"Balance: {context.customer.Balance}₴");
        }
        return base.Init();
    }

    Layout History()
    {
        Console.Clear();

        Console.WriteLine($"|  #  |    Price   |");
        
        for (int i = 0; i < context.customer.History.Count; i++)
        {
            ProductDto product = context.productService.GetById(context.customer.History.ElementAt(i));
            Console.WriteLine($"| {i, 3} | {product.Price, 9}₴ | {product.Title} : {product.Description}");
        }
        
        Console.Write("Press any key to exit history");
        Console.ReadKey();
        return this;
    }

    Layout Catalog()
    {
        Console.Clear();

        List<ProductDto> catalog = context.productService.Catalog();

        while (true)
        {
            Console.WriteLine("Type \"q\" to exit");
            Console.WriteLine($"|  #  |    Price   |");
        
            for (int i = 1; i <= catalog.Count; i++)
            {
                ProductDto product = catalog.ElementAt(i - 1);
                Console.WriteLine($"| {i, 3} | {product.Price, 9}₴ | {product.Title} : {product.Description}");
            }
        
            Console.Write("Enter product number to order: ");
            string input = Console.ReadLine();
            if (input == "q") return this;
            
            int n = 1;
            if (int.TryParse(input, out n) && n > 0 && n <= catalog.Count)
            {
                if (catalog.ElementAt(n - 1).Price > context.customer.Balance)
                {
                    Console.Clear();
                    Utils.PrintError("Not enough money");
                }
                else
                {
                    context.customer = context.customerService.AddToCart(context.customer.Id, catalog.ElementAt(n - 1).Id);
                    Console.Clear();
                    Console.WriteLine($"\"{catalog.ElementAt(n - 1).Title}\" ordered successfully");
                }
            }
            else
            {
                Console.Clear();
                Utils.PrintError("Wrong number");
            }
        }
    }

    Layout Balance()
    {
        Console.Clear();

        while (true)
        {
            Console.WriteLine("Type \"q\" to exit");

            Console.WriteLine($"Balance: {context.customer.Balance}₴");
            Console.Write("Enter amount to add: ");
            string input = Console.ReadLine();
            if (input == "q") return this;

            int amount = 0;
            if (int.TryParse(input, out amount) && amount > 0)
            {
                context.customer = context.customerService.AddToBalance(context.customer.Id, amount);
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Utils.PrintError("Wrong amount");
            }
        }
    }

    Layout AddProduct()
    {
        Console.Clear();

        while (true)
        {
            Console.WriteLine("Type \"q\" to exit");
            Console.Write("Enter product title: ");
            string title = Console.ReadLine();
            if (title == "q") return this;

            Console.Write("Enter product price: ");
            string price = Console.ReadLine();
            if (price == "q") return this;
            
            Console.Write("Enter product description: ");
            string description = Console.ReadLine();
            if (description == "q") return this;
            
            int priceInt = 0;
            if (int.TryParse(price, out priceInt) && priceInt >= 0 && title != "" && context.productService.GetByTitle(title) == null)
            {
                Console.Clear();
                context.productService.Add(title, priceInt, description);
                Console.WriteLine($"Product \"{title}\" added successfully");
            }
            else
            {
                Console.Clear();
                Utils.PrintError("Wrong product info");
            }
        }
    }

    Layout EditProduct()
    {
        Console.Clear();

        List<ProductDto> catalog = context.productService.Catalog();

        while (true)
        {
            Console.WriteLine("Type \"q\" to exit");
            Console.WriteLine($"|  #  |    Price   |");

            for (int i = 1; i <= catalog.Count; i++)
            {
                ProductDto product = catalog.ElementAt(i - 1);
                Console.WriteLine($"| {i,3} | {product.Price,9}₴ | {product.Title} : {product.Description}");
            }

            Console.Write("Enter product number to edit: ");
            string input = Console.ReadLine();
            if (input == "q") return this;

            int n = 1;
            if (int.TryParse(input, out n) && n > 0 && n <= catalog.Count)
            {
                ProductDto product = catalog.ElementAt(n - 1);

                Console.Clear();

                while (true)
                {
                    Console.WriteLine($"You are editing product:\n{product.Price}₴ | {product.Title} : {product.Description}");

                    Console.Write("Enter new title: ");
                    string title = Console.ReadLine();
                    if (title == "q") break;

                    Console.Write("Enter new price: ");
                    string price = Console.ReadLine();
                    if (price == "q") break;

                    Console.Write("Enter new description: ");
                    string description = Console.ReadLine();
                    if (description == "q") break;

                    int priceInt = 0;
                    if (int.TryParse(price, out priceInt) && priceInt >= 0 && title != "" && (title == product.Title || context.productService.GetByTitle(title) == null))
                    {
                        Console.Clear();
                        product.Title = title;
                        product.Price = priceInt;
                        product.Description = description;
                        context.productService.Update(product);
                        Console.WriteLine($"Product \"{title}\" edited successfully");
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Utils.PrintError("Wrong product info");
                    }
                }

                Console.Clear();
            }
            else
            {
                Console.Clear();
                Utils.PrintError("Wrong number");
            }
        }
    }
}