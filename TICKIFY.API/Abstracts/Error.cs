namespace TICKIFY.API.Abstracts
{
    public class Error(String Code, String Message)
    {
        public string Code { get; } = Code;
        public string Message { get; } = Message;

        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
