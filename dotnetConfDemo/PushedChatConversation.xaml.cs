using dotnetConfDemo.Services;

namespace dotnetConfDemo;

[QueryProperty("ChatId", "Id")]
public partial class PushedChatConversation : ContentPage
{
    private readonly ChatConversationService chatConversationService;
    private string chatId;

    public PushedChatConversation(ChatConversationService chatConversationService)
    {
        InitializeComponent();
        this.chatConversationService = chatConversationService;
    }


    public string ChatId
    {
        get => chatId;
        set
        {
            chatId = value;
            var chatData = chatConversationService.GetChatConversation(ChatId);
            chatConversationControl.ChatConversationViewModel = new ViewModel.ChatConversationViewModel(chatData, Dispatcher);
            this.BindingContext = chatData;
        }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Window.SizeChanged += OnWindowSizeChanged;
    }

    async void OnWindowSizeChanged(object sender, EventArgs e)
    {
        if (Window.Width > 700)
        {
            Window.SizeChanged -= OnWindowSizeChanged;
            await Navigation.PopAsync();
        }
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        Window.SizeChanged -= OnWindowSizeChanged;
    }
}