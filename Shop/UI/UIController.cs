class UIController
{
    public UIController()
    {
        Context context = new Context();
        Layout next = new StartLayout(context);
        while (next != null)
        {
            next = next.Init();
        }
    }
}
