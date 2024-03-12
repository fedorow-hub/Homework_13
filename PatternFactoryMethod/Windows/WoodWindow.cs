namespace PatternFactoryMethod.Windows;

public class WoodWindow : IWindow
{
    public void Open()
    {
        Console.WriteLine("Открыто деревянное окно");
    }
}
