namespace ForExperiments
{
    public interface IAnimal<T>
        where T : Animal
    {
        T GetValue { get; }
        T GetValueMethod();
    }
}
