using dotnetConfDemo.Data;
using dotnetConfDemo.ViewModel;

namespace dotnetConfDemo.Controls;

public partial class ChatConversationControl : Grid
{
    public ChatConversationControl()
    {
        InitializeComponent();
        this.Unloaded += OnUnloaded;
        this.Loaded += OnLoaded;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        enterChat.IsVisible = this.BindingContext != null;
    }

    void OnLoaded(object sender, EventArgs e)
    {
        WatchForChanges();
    }

    void OnUnloaded(object sender, EventArgs e)
    {
        ChatConversationViewModel?.StopWatchingForChanges();


        if (ChatConversationViewModel != null)
            ChatConversationViewModel.Messages.CollectionChanged -= MessageCollectionChanged;
    }

    public ChatConversationViewModel ChatConversationViewModel
    {
        get => this.BindingContext as ChatConversationViewModel;
        set
        {
            this.BindingContext = value;
            WatchForChanges();
        }
    }

    void WatchForChanges()
    {
        if (IsLoaded && ChatConversationViewModel != null)
        {
            ChatConversationViewModel.WatchForChanges();
            DoScroll();
            ChatConversationViewModel.Messages.CollectionChanged -= MessageCollectionChanged;
            ChatConversationViewModel.Messages.CollectionChanged += MessageCollectionChanged;
        }
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
        Dispatcher.Dispatch(async () =>
        {
            await Task.Delay(50);
            processingScroll = false;

            if (IsLoaded && ChatConversationViewModel != null)
                cv.ScrollTo(ChatConversationViewModel.Messages.Count - 1);
        });
    }
}