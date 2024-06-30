namespace Test
{
    public class Bird
    {
        public virtual void Eat() { }
    }

    public class FlyBird : Bird
    {
        public virtual void Fly() { }
    }

    public class Sparrow : FlyBird
    {
        public override void Eat() {
            // ok
        }

        public override void Fly() {
            // ok
        }
    }

    public class Ostrich : Bird
    {
        public override void Eat() {
            // ok
        }
    }

    public class Main
    {
        public void Test() {
            FlyBird sparrow = new Sparrow();
            sparrow.Eat();
            sparrow.Fly();

            Bird ostrich = new Ostrich();
            ostrich.Eat();
        }
    }
}