using dotnetConfDemo.Data;
using System.Collections.ObjectModel;

namespace dotnetConfDemo.Services
{
    public class ChatConversationService
    {
        public ChatConversationService()
        {

        }

        public ChatConversationData GetChatConversation(string Id)
        {
            return chatConversations.FirstOrDefault(x => x.Id == Id);
        }

        public ObservableCollection<ChatConversationData> GetChatConversationList() =>
            chatConversations;

        internal ChatConversationData AddConversation(string userName)
        {
            int newId = chatConversations.Select(x => Convert.ToInt32(x.Id)).Max() + 1;
            var data = new ChatConversationData()
            {
                UserName = userName,
                Id = newId.ToString(),
                Messages = new ObservableCollection<ChatMessageData>()
                {
                    "Hello!"
                }
            };

            chatConversations.Add(data);

            return data;
        }

        ObservableCollection<ChatConversationData> chatConversations = new ObservableCollection<ChatConversationData>()
        {
            new ChatConversationData()
            {
                Id = "1",
                UserName = "Multi McWindow",
                Messages = new ObservableCollection<ChatMessageData>()
                {
                    "Did you open me in a new window?",
                    "When you close me do I continue to exist in The Grid?"
                }
            },
            new ChatConversationData()
            {
                Id = "2",
                UserName = "Connor Text Menu",
                Messages = new ObservableCollection<ChatMessageData>()
                {
                    "I'm flying!!"
                }
            },
            new ChatConversationData()
            {
                Id = "3",
                UserName = "Tammy Tooltips",
                Messages = new ObservableCollection<ChatMessageData>()
                {
                    "Hey!",
                    "Can I give you just a litte more context?"
                }
            },
            new ChatConversationData()
            {
                Id = "4",
                UserName = "Pinter Orville",
                Messages = new ObservableCollection<ChatMessageData>()
                {
                    "When you move",
                    "I change the colors on the things just like that!"
                }
            },
        };
    }
}