using dotnetConfDemo.Services;
using System.ComponentModel;

namespace dotnetConfDemo;

public partial class Chat : ContentPage
{
    private readonly ChatConversationService chatConversationService;

    public Chat(ChatConversationService chatConversationService)
    {
        InitializeComponent();
        this.chatConversationService = chatConversationService;
        twoPaneView.Pane1Length = new GridLength(4, GridUnitType.Star);
        twoPaneView.Pane2Length = new GridLength(9, GridUnitType.Star);

    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        chatLayout.ItemsSource =
            chatConversationService
                .GetChatConversationList()
                .Select(x => new ChatConversationViewModel(x))
                .ToList(); ;
    }

    void OnChatSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.PreviousSelection.FirstOrDefault() is ChatConversationViewModel previous)
            previous.IsSelected = false;

        if (e.CurrentSelection.FirstOrDefault() is ChatConversationViewModel current)
        {
            current.IsSelected = true;
            chatConversation.BindingContext = current.ChatConversation;
        }
    }

    class ChatConversationViewModel : INotifyPropertyChanged
    {
        private bool isSelected;

        public ChatConversationViewModel(ChatConversationData chatConversation)
        {
            this.ChatConversation = chatConversation;
        }

        public ChatConversationData ChatConversation { get; }
        public bool IsSelected
        {
            get => isSelected; set
            {
                if (value == isSelected) return;

                isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Background)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PointerOverColor)));
            }
        }

        public Brush Background => (isSelected) ? SolidColorBrush.LightBlue : SolidColorBrush.Green;
        public Brush PointerOverColor => (isSelected) ? SolidColorBrush.LightBlue : SolidColorBrush.Purple;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}