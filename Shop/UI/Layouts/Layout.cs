using System;
using System.Collections.Generic;
using System.Linq;

abstract class Layout
{
    protected Layout exit = null;
    protected Context context;
    protected Dictionary<string, Func<Layout>> actions = new();

    protected Layout(Context context)
    {
        this.context = context;
    }

    public virtual Layout Init()
    {
        while (true)
        {
            DisplayActions();

            Console.Write("Enter action number: ");
            string input = Console.ReadLine();
            if (input == "q") return exit;
            int i;

            if (int.TryParse(input, out i) && i >= 1 && i <= actions.Count)
            {
                Console.Clear();
                return TriggerAction(i - 1);
            }
            Console.Clear();
            Utils.PrintError("Wrong action number");
        }
    }

    private void DisplayActions()
    {
        Console.WriteLine("Available actions:");

        for (int i = 1; i <= actions.Count; i++)
            Console.WriteLine($"{i}. {actions.ElementAt(i-1).Key}");
    }

    private Layout TriggerAction(int action)
    {
        return actions.ElementAt(action).Value.Invoke();
    }
}
