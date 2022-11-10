using dotnetConfDemo.Services;
using dotnetConfDemo.ViewModel;

namespace dotnetConfDemo;

public partial class ChatConversationShell : Shell
{
    private readonly ChatConversationService chatConversationService;
    private readonly IApplication application;

    // Just showing injection on Shell if you want to inject things into shell
    public ChatConversationShell(
        ChatConversationService chatConversationService,
        IApplication application)
    {
        InitializeComponent();
        this.chatConversationService = chatConversationService;
        this.application = application;
    }
}