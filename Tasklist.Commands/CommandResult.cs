namespace Tasklist.Commands
{
    public class CommandResult
    {
        public bool Success { get; private set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
