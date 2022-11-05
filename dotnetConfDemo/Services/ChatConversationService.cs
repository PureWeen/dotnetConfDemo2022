using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public List<ChatConversationData> GetChatConversationList() =>
            chatConversations;

        List<ChatConversationData> chatConversations = new List<ChatConversationData>()
        {
            new ChatConversationData()
            {
                Id = "1",
                UserName = "Multi McWindow",
                Messages = new List<string>()
                {
                    "Did you open me in a new window?",
                    "When you close me do I continue to exist in The Grid?"
                }
            },
            new ChatConversationData()
            {
                Id = "2",
                UserName = "Connor Text Menu",
                Messages = new List<string>()
                {
                    "I'm flying!!"
                }
            },
            new ChatConversationData()
            {
                Id = "3",
                UserName = "Tammy Tooltips",
                Messages = new List<string>()
                {
                    "Hey!",
                    "Can I give you just a litte more context?"
                }
            },
            new ChatConversationData()
            {
                Id = "4",
                UserName = "Pinter Orville",
                Messages = new List<string>()
                {
                    "When you move",
                    "I change the colors on the things just like that!"
                }
            },
        };
    }


    public class ChatConversationData
    {
        public string UserName { get; set; }
        public string LastMessage => Messages.LastOrDefault() ?? String.Empty;
        public List<string> Messages { get; set; }

        public string Id { get; set; }

        public ChatConversationData()
        {

        }
    }
}