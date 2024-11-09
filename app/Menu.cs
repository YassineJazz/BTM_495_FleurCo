public class Menu
{
    string[] Options { get; set; }

    public Menu(string[] options)
    {
        Options = options;
    }
    public void ShowOptions()
    {
        foreach (string option in Options)
            Console.WriteLine(option);

    }
}