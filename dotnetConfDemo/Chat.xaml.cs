using dotnetConfDemo.Data;
using dotnetConfDemo.Services;
using dotnetConfDemo.ViewModel;
using System.ComponentModel;

namespace dotnetConfDemo;

public partial class Chat : ContentPage
{
    private readonly ChatConversationService chatConversationService;
    private readonly Shell shell;
    private readonly IApplication application;
    private readonly IDispatcher dispatcher;

    public Chat(ChatConversationService chatConversationService, Shell shell, IApplication application, IDispatcher dispatcher)
    {
        InitializeComponent();
        this.chatConversationService = chatConversationService;
        this.shell = shell;
        this.application = application;

        // Dispatcher is also available on base class but I'm just demonstrating that you can also resolve the dispathcer here
        this.dispatcher = dispatcher;

        twoPaneView.Pane1Length = new GridLength(4, GridUnitType.Star);
        twoPaneView.Pane2Length = new GridLength(9, GridUnitType.Star);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // Just load the data the first time
        if (chatLayout.ItemsSource == null)
        {
            var chatData = chatConversationService
                    .GetChatConversationList()
                    .Select(x => new ChatConversationViewModel(x, dispatcher))
                    .ToList();

            chatLayout.ItemsSource = chatData;
        }

        Window.SizeChanged += OnWindowSizeChanged;

        var conversationList =
            chatConversationService.GetChatConversationList();

        conversationList.CollectionChanged += OnConversationChanged;
    }

    void OnConversationChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null && chatLayout.ItemsSource is List<ChatConversationViewModel> chatData)
        {
            foreach (ChatConversationData cc in e.NewItems)
            {
                chatData.Add(new ChatConversationViewModel(cc, Dispatcher));
            }

            chatLayout.ItemsSource = new List<ChatConversationViewModel>(chatData);
            chatLayout.SelectedItem = chatData.Last();
        }
    }

    void OnWindowSizeChanged(object sender, EventArgs e)
    {
        CheckWindowSize();
    }

    async void CheckWindowSize()
    {
        if (Window.Width < 600 && chatLayout.SelectedItem is ChatConversationViewModel selected)
        {
            await shell.GoToAsync($"{nameof(PushedChatConversation)}?Id={selected.ChatConversation.Id}");
        }
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        Window.SizeChanged -= OnWindowSizeChanged;

        chatConversationService
            .GetChatConversationList()
            .CollectionChanged -= OnConversationChanged;
    }

    void OnChatSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.PreviousSelection.FirstOrDefault() is ChatConversationViewModel previous)
            previous.IsSelected = false;

        if (e.CurrentSelection.FirstOrDefault() is ChatConversationViewModel current)
        {
            current.IsSelected = true;
            chatConversation.BindingContext = new ChatConversationViewModel(current.ChatConversation, dispatcher);
        }

        CheckWindowSize();
    }

    void OnOpenChatInNewWindow(object sender, EventArgs e)
    {
        var window = new ChatWindow()
        {
            ChatId = ((sender as BindableObject).BindingContext as ChatConversationData).Id,
            Height = 400,
            Width = 400,
            X = Window.Width / 2,
            Y = Window.Height / 2
        };

        application.OpenWindow(window);
    }
}