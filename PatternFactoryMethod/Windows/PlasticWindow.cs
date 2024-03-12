namespace PatternFactoryMethod.Windows;

public class PlasticWindow : IWindow
{
    public void Open()
    {
        Console.WriteLine("Открыто пластиковое окно");
    }
}
