using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetConfDemo
{
    public class ChatWindow : ScopedWindow<ChatConversationShell>
    {
        public ChatWindow() : base()
        {
        }

        public string ChatId { get; internal set; }


        protected async override void OnCreated()
        {
            base.OnCreated();

            // .NET 8 possibly we can route at the Shell level so you can open a new window
            // through the use of GoToAsync
            await (Page as Shell).GoToAsync($"///{nameof(MainPageChatConversation)}?Id={ChatId}");
        }
    }
}
