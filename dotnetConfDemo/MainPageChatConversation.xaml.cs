using dotnetConfDemo.Services;

namespace dotnetConfDemo;

[QueryProperty("ChatId", "Id")]
public partial class MainPageChatConversation : ContentPage
{
    private readonly IApplication application;
    private readonly INavigation navigation;
    private readonly Shell shell;
    private string chatId;
    private readonly ViewModel.ChatConversationViewModel chatConversationViewModel;
    private readonly ChatConversationService chatConversationService;

    public MainPageChatConversation(
        ChatConversationService chatConversationService,  // Singleton
        IApplication application, // Singleton
        INavigation navigation,   // ScopedService
        Shell shell,              // ScopedService
        IDispatcher dispatcher,   // ScopedService
        ViewModel.ChatConversationViewModel chatConversationViewModel) //Transient
    {
        InitializeComponent();
        this.application = application;
        this.navigation = navigation;
        this.shell = shell;
        this.chatConversationViewModel= chatConversationViewModel;
        this.chatConversationService = chatConversationService;
    }

    public string ChatId
    {
        get => chatId;
        set
        {
            chatId = value;
            chatConversationViewModel.SetChatConversation(chatConversationService.GetChatConversation(ChatId));
            this.BindingContext = chatConversationViewModel;
        }
    }

    private void OnMenuFlyoutItem(object sender, EventArgs e)
    {
        application.CloseWindow(Window);
    }

    async void OnPushSomePage(object sender, EventArgs e)
    {
        await navigation.PushAsync(new ContentPage());
    }
}