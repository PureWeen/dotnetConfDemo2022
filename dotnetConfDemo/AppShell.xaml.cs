using Microsoft.Maui.Handlers;

namespace dotnetConfDemo;

public partial class AppShell : Shell
{
    private readonly IApplication application;

    public AppShell(IApplication application)
    {
        InitializeComponent();
        this.application = application;
    }

    void OpenNewChatWindow(object sender, EventArgs e)
    {
        application.OpenWindow(new ChatWindow());
    }

    private void OpenNewChat(object sender, EventArgs e)
    {

    }
}
