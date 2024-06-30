namespace Test2
{
    public interface Animal
    {
        void Eat();
    }

    public interface RunnableAnimal : Animal
    {
        void Run();
    }

    public interface FlyableAnimal : Animal
    {
        void Fly();
    }

    public class Dog : RunnableAnimal
    {
        public void Eat() {
            // ok
        }

        public void Run() {
            // ok
        }
    }

    public class Bird : FlyableAnimal
    {
        public void Eat() {
            // ok
        }

        public void Fly() {
            // ok
        }
    }
}