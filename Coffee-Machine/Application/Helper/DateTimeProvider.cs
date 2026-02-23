using Coffee_Machine.Application.Interface;

namespace Coffee_Machine.Application.Helper
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Today => DateTime.Today;
        public DateTime Now => DateTime.Now;
    }
}
