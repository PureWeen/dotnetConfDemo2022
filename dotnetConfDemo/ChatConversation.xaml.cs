using dotnetConfDemo.Services;

namespace dotnetConfDemo;

public partial class ChatConversation : ContentPage
{
    private readonly ChatConversationService chatConversationService;

    public ChatConversation(ChatConversationService chatConversationService)
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
            chatControl.ChatConversationViewModel = new ViewModel.ChatConversationViewModel(data, Dispatcher);
            this.BindingContext = data;
        }
    }
}