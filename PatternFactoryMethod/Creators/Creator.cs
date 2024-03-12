using PatternFactoryMethod.Windows;

namespace PatternFactoryMethod.Creators;

public abstract class Creator
{
    public abstract IWindow CreateWindow();
}
