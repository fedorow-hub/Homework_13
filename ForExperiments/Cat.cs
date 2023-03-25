namespace ForExperiments
{
    public class Cat : IAnimal<Animal>
    {
        public Animal GetValue { get { return new Animal(); } }

        public Animal GetValueMethod()
        {
            return new Animal();
        }
    }
}
