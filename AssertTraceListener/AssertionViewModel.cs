namespace AssertTraceListener
{
    internal class AssertionViewModel
    {
        public AssertionViewModel(string message)
        : this(message, string.Empty, string.Empty)
        {
        }

        public AssertionViewModel(string message, string detailedMessage, string stackTrace)
        {
            Message = message;
            DetailedMessage = detailedMessage;
            StackTrace = stackTrace;
        }

        public string Message { get; }
        public string DetailedMessage { get; }
        public string StackTrace { get; set; }
    }
}
