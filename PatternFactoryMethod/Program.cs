using PatternFactoryMethod.Creators;
using PatternFactoryMethod.Windows;

PlasticWindowCreator plasticWindowCreator = new PlasticWindowCreator();

IWindow plasticWindow = plasticWindowCreator.CreateWindow();

plasticWindow.Open();

WoodWindowCreator woodWindowCreator = new WoodWindowCreator();

IWindow woodWindow = woodWindowCreator.CreateWindow();

woodWindow.Open();

