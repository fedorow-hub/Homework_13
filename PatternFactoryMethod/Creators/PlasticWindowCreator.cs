
using PatternFactoryMethod.Windows;

namespace PatternFactoryMethod.Creators;

public class PlasticWindowCreator : Creator
{
    public override IWindow CreateWindow()
    {
        return new PlasticWindow();
    }
}
