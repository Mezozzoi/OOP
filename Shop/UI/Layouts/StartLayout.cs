using System;
class StartLayout : Layout
{
    public StartLayout(Context context) : base(context)
    {
        actions.Add("Login", Login);
        actions.Add("Register", Register);
        
        exit = null;
    }

    public override Layout Init()
    {
        Console.Clear();
        return base.Init();
    }

    Layout Login()
    {
        Console.Clear();

        while (true)
        {
            Console.Write("Enter your login: ");
            string login = Console.ReadLine();
            if (login == "q") return this;

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            if (password == "q") return this;

            CustomerDto customer = context.customerService.Login(login, password);
            if (customer != null)
            {
                context.customer = customer;
                return new AccountLayout(context);
            }
            Console.Clear();
            Utils.PrintError("Wrong login or password");
        }
    }

    Layout Register()
    {
        Console.Clear();

        string login, password;

        while (true)
        {
            Console.Write("Enter your login: ");
            login = Console.ReadLine();

            if (login == "q") return this;

            if (!Utils.ValidateLogin(login))
            {
                Console.Clear();
                Utils.PrintError("Invalid login");
                continue;
            } else if (!context.customerService.IsLoginAvailable(login))
            {
                Console.Clear();
                Utils.PrintError("This login is already in usage");
                continue;
            } else break;
        }

        while (true)
        {
            Console.Write("Enter your password: ");
            password = Console.ReadLine();

            if (password == "q") return this;

            if (Utils.ValidatePassword(password))
                break;

            Utils.PrintError("Invalid password");
        }

        context.customerService.Register(login, password);

        return this;
    }
}
