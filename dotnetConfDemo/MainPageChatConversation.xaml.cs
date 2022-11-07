namespace dotnetConfDemo;

public partial class MainPageChatConversation : ContentPage
{
    private readonly IApplication application;
    private readonly INavigation navigation;
    private readonly Shell shell;

    public MainPageChatConversation(IApplication application, INavigation navigation, Shell shell)
    {
        InitializeComponent();
        this.application = application;
        this.navigation = navigation;
        this.shell = shell;
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