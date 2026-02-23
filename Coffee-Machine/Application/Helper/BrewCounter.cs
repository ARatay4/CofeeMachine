using Coffee_Machine.Application.Interface;

namespace Coffee_Machine.Application.Helper
{
    public class BrewCounter : IBrewCounter
    {
        private int _brewCnt;

        public int IncrementValue()
        {
            return Interlocked.Increment(ref _brewCnt);
        }
    }
}
