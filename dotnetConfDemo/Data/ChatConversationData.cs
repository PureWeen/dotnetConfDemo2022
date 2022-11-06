using System.Collections.ObjectModel;

namespace dotnetConfDemo.Data
{
    public class ChatConversationData
    {
        public string UserName { get; set; }
        public ChatMessageData LastMessage => Messages.LastOrDefault(x => x.FromYou) ?? null;
        public ObservableCollection<ChatMessageData> Messages { get; set; }

        public string Id { get; set; }

        public ChatConversationData()
        {

        }
    }
}