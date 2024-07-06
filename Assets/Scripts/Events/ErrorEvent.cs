namespace Events
{
    public class ErrorEvent
    {
        public ErrorEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}