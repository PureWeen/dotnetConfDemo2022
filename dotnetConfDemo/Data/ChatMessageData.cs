namespace dotnetConfDemo.Data
{
    public class ChatMessageData
    {
        public string Message { get; }
        public bool FromMe { get; }
        public bool FromYou => !FromMe;
        public DateTimeOffset DateCreated { get; }

        public ChatMessageData(string message, bool fromMe)
        {
            FromMe = fromMe;
            Message = message;
            DateCreated = DateTimeOffset.Now;
        }

        public static implicit operator ChatMessageData(string s) => new ChatMessageData(s, false);
    }
}