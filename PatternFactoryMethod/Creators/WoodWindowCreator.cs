using PatternFactoryMethod.Windows;

namespace PatternFactoryMethod.Creators;

public class WoodWindowCreator : Creator
{
    public override IWindow CreateWindow()
    {
        return new WoodWindow();
    }
}
