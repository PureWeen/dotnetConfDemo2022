using dotnetConfDemo.Services;

namespace dotnetConfDemo;

public partial class ChatConversationShell : Shell
{
    private readonly ChatConversationService chatConversationService;
    private readonly IApplication application;

    public ChatConversationShell(ChatConversationService chatConversationService, IApplication application)
    {
        InitializeComponent();
        this.chatConversationService = chatConversationService;
        this.application = application;
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