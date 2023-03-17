namespace Dependency_Injection.Services
{
    public interface ITimeStamping
    {
        string TimeStamp { get; }
    }


    public class DefaultTimeStamper : ITimeStamping
    {
        public string TimeStamp { get => DateTime.Now.ToShortTimeString(); }
    }
}
