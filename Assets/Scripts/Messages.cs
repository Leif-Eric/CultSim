using GameEventBus.Events;

public class Messages
{
    public class TestMessage1 : EventBase
    {
        public string Message { get; set; }

        public TestMessage1(string message)
        {
            Message = message;
        }
    }
}
