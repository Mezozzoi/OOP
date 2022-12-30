using System;
using System.Text.RegularExpressions;

class Utils
{
    public static bool ValidateLogin(string login)
    {
        string pat = @"^[A-Za-z\d_]{0,19}$";
        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
        return r.IsMatch(login);
    }
    
    public static bool ValidatePassword(string password)
    {
        string pat = @"^[A-Za-z\d]{0,19}$";
        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
        return r.IsMatch(password);
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}