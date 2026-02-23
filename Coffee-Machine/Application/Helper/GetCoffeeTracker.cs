using Coffee_Machine.Application.Interface;

namespace Coffee_Machine.Application.Helper
{
    public class GetCoffeeTracker : IGetCoffeeTracker
    {
        private int counter = 0;

        public int IncrementAndGet()
        {
            counter++;

            if (counter > 5)
                counter = 1;

            return counter;
        }
    }
}
