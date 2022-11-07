using dotnetConfDemo.Data;
using dotnetConfDemo.ViewModel;

namespace dotnetConfDemo.Controls;

public partial class ChatConversationControl : Grid
{
    public ChatConversationViewModel ChatConversationViewModel { get; private set; }
    Window window;

    public ChatConversationControl()
    {
        InitializeComponent();
        this.Unloaded += OnUnloaded;
        this.Loaded += OnLoaded;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (ChatConversationViewModel != null)
            StopWatchingForChanges();

        ChatConversationViewModel = BindingContext as ChatConversationViewModel;

        if (ChatConversationViewModel != null)
            WatchForChanges();

        enterChat.IsVisible = this.BindingContext != null;
    }

    void OnLoaded(object sender, EventArgs e)
    {
        WatchForChanges();
    }

    void OnUnloaded(object sender, EventArgs e)
    {
        StopWatchingForChanges();
    }

    void StopWatchingForChanges()
    {
        if (ChatConversationViewModel != null)
        {
            ChatConversationViewModel?.StopWatchingForChanges();
            ChatConversationViewModel.Messages.CollectionChanged -= MessageCollectionChanged;
        }

        if (window != null)
            window.Destroying -= OnWindowClosing;

        window = null;
    }

    void WatchForChanges()
    {
        if (IsLoaded && ChatConversationViewModel != null)
        {
            ChatConversationViewModel.WatchForChanges();
            DoScroll();
            ChatConversationViewModel.Messages.CollectionChanged += MessageCollectionChanged;
            Window.Destroying += OnWindowClosing;
            window = Window;
        }
    }

    void OnWindowClosing(object sender, EventArgs e)
    {
        StopWatchingForChanges();
    }

    bool processingScroll = false;
    void MessageCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        DoScroll();
    }

    void DoScroll()
    {
        if (ChatConversationViewModel == null)
            return;

        if (processingScroll)
            return;

        processingScroll = true;
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(50), () =>
        {
            processingScroll = false;

            if (IsLoaded && ChatConversationViewModel != null)
                cv.ScrollTo(ChatConversationViewModel.Messages.Count - 1, animate: false);
        });
    }

    void OnCloseWindow(object sender, EventArgs e)
    {
        StopWatchingForChanges();
        Application.Current.CloseWindow(Window);
    }
}