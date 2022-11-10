using dotnetConfDemo.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotnetConfDemo.ViewModel
{
    public class ChatConversationViewModel : INotifyPropertyChanged
    {
        bool isSelected;
        private string newChatMessage;
        private ObservableCollection<ChatMessageData> messages;
        private readonly IDispatcher dispatcher;

        // Uses the resolver
        // IDispatcher is registered for you there's no need to register this
        public ChatConversationViewModel(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            AddChatMessage = new Command(OnAddNewChatMessage, () => !String.IsNullOrWhiteSpace(NewChatMessage));
        }

        public ChatConversationViewModel(ChatConversationData chatConversation, IDispatcher dispatcher) : this(dispatcher)
        {
            SetChatConversation(chatConversation);
        }

        void OnAddNewChatMessage()
        {
            if (String.IsNullOrWhiteSpace(NewChatMessage))
                return;

            ChatConversation.Messages.Add(new ChatMessageData(NewChatMessage, true));
            NewChatMessage = String.Empty;
            OnPropertyChanged(nameof(NewChatMessage));
            AddRandomResponse();
        }

        void MessageAddedToConversation(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Because we don't know what thread the Chat Conversation Messages are coming from 
            dispatcher.Dispatch(() =>
            {
                if (e.NewItems != null)
                    foreach (ChatMessageData message in e.NewItems)
                        Messages.Add(message);
            });
        }

        internal void WatchForChanges()
        {
            StopWatchingForChanges();
            ChatConversation.Messages.CollectionChanged += MessageAddedToConversation;

            dispatcher.Dispatch(() =>
            {
                // sync up messages
                for (int i = Messages.Count; i < ChatConversation.Messages.Count; i++)
                {
                    Messages.Add(ChatConversation.Messages[i]);
                }
            });
        }

        public void SetChatConversation(ChatConversationData chatConversation)
        {
            this.ChatConversation = chatConversation;
            Messages = new ObservableCollection<ChatMessageData>(ChatConversation.Messages);
        }

        async void AddRandomResponse()
        {
            await Task.Delay(Random.Shared.Next(100, 2000));
            ChatConversation.Messages.Add(new ChatMessageData($"Canned Response: {Random.Shared.Next(1, 2000)}", false));
        }

        public ObservableCollection<ChatMessageData> Messages
        {
            get => messages;
            private set
            {
                messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public string NewChatMessage
        {
            get
            {
                return newChatMessage;
            }

            set
            {
                newChatMessage = value;
                AddChatMessage.ChangeCanExecute();
            }
        }

        public string UserName => ChatConversation.UserName;

        public Command AddChatMessage { get; }

        public ChatConversationData ChatConversation { get; private set; }

        public bool IsSelected
        {
            get => isSelected; set
            {
                if (value == isSelected) return;

                isSelected = value;
                OnPropertyChanged(nameof(Background));
                OnPropertyChanged(nameof(PointerOverColor));
            }
        }

        public Brush Background => (isSelected) ? SolidColorBrush.LightBlue : SolidColorBrush.Green;
        public Brush PointerOverColor => (isSelected) ? SolidColorBrush.LightBlue : SolidColorBrush.Purple;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        internal void StopWatchingForChanges()
        {
            ChatConversation.Messages.CollectionChanged -= MessageAddedToConversation;
        }
    }
}
