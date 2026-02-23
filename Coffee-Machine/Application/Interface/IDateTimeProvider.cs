namespace Coffee_Machine.Application.Interface
{
    public interface IDateTimeProvider
    {
        DateTime Today { get; }
        DateTime Now { get; }
    }
}
