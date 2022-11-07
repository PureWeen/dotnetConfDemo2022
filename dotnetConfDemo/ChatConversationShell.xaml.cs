using dotnetConfDemo.Services;

namespace dotnetConfDemo;

public partial class ChatConversationShell : Shell
{
    private readonly ChatConversationService chatConversationService;

    public ChatConversationShell(ChatConversationService chatConversationService)
    {
        InitializeComponent();
        this.chatConversationService = chatConversationService;
    }


    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        if (this.Window is ChatWindow ct && args.NewHandler != null)
        {
            var data = chatConversationService.GetChatConversation(ct.ChatId);
            this.BindingContext = new ViewModel.ChatConversationViewModel(data, Dispatcher);
        }
    }
}