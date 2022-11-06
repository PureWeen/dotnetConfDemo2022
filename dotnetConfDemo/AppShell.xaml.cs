using dotnetConfDemo.Services;
using Microsoft.Maui.Handlers;

namespace dotnetConfDemo;

public partial class AppShell : Shell
{
    private readonly IApplication application;
    private readonly ChatConversationService chatConversationService;

    public AppShell(IApplication application, ChatConversationService chatConversationService)
    {
        InitializeComponent();
        this.application = application;
        this.chatConversationService = chatConversationService;
    }


    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);

        if (!chatContent.IsChecked)
        {
            chatContent.FlyoutIcon = "chatunselected.png";
            calendarContent.FlyoutIcon = "calendarselected.png";
        }
        else
        {
            chatContent.FlyoutIcon = "chatselected.png";
            calendarContent.FlyoutIcon = "calendarunselected.png";
        }
    }

    async void OpenNewChat(object sender, EventArgs e)
    {
        string userName = await DisplayPromptAsync("New Chat Message", "Enter Username");

        if (!String.IsNullOrWhiteSpace(userName))
        {
            _ = chatConversationService.AddConversation(userName);
        }
    }

    async void OpenNewChatWindow(object sender, EventArgs e)
    {
        string userName = await DisplayPromptAsync("New Chat Message", "Enter Username");

        if (!String.IsNullOrWhiteSpace(userName))
        {
            var data = chatConversationService.AddConversation(userName);
            application.OpenWindow(new ChatWindow()
            {
                ChatId = data.Id,
                Height = 400,
                Width = 400,
                X = Window.Width / 2,
                Y = Window.Height / 2
            });
        }
    }
}
