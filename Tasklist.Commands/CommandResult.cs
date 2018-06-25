namespace Tasklist.Commands
{
    public class CommandResult
    {
        public bool Success { get; private set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public CommandResult(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
