using dotnetConfDemo.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotnetConfDemo.ViewModel
{
    public class ChatConversationViewModel : INotifyPropertyChanged
    {
        bool isSelected;
        private string newChatMessage;
        private readonly IDispatcher dispatcher;

        public ChatConversationViewModel(ChatConversationData chatConversation, IDispatcher dispatcher)
        {
            this.ChatConversation = chatConversation;
            this.dispatcher = dispatcher;
            Messages = new ObservableCollection<ChatMessageData>(ChatConversation.Messages);
            AddChatMessage = new Command(() =>
            {
                if (String.IsNullOrWhiteSpace(NewChatMessage))
                    return;

                ChatConversation.Messages.Add(new ChatMessageData(NewChatMessage, true));
                NewChatMessage = String.Empty;
                OnPropertyChanged(nameof(NewChatMessage));
                AddRandomResponse();

            }, () => !String.IsNullOrWhiteSpace(NewChatMessage));
        }

        async void AddRandomResponse()
        {
            await Task.Delay(Random.Shared.Next(100, 2000));
            ChatConversation.Messages.Add(new ChatMessageData($"Canned Response: {Random.Shared.Next(1, 2000)}", false));
        }

        public ObservableCollection<ChatMessageData> Messages { get; set; }

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

        public ChatConversationData ChatConversation { get; }

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

        internal void WatchForChanges()
        {
            StopWatchingForChanges();
            ChatConversation.Messages.CollectionChanged += OnMessagesAdded;

            dispatcher.Dispatch(() =>
            {
                // sync up messages
                for (int i = Messages.Count; i < ChatConversation.Messages.Count; i++)
                {
                    Messages.Add(ChatConversation.Messages[i]);
                }
            });
        }

        internal void StopWatchingForChanges()
        {
            ChatConversation.Messages.CollectionChanged -= OnMessagesAdded;
        }

        void OnMessagesAdded(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Because we don't know what thread the Chat Conversation Messages are coming from 
            dispatcher.Dispatch(() =>
            {
                if (e.NewItems != null)
                    foreach (ChatMessageData message in e.NewItems)
                        Messages.Add(message);
            });
        }
    }
}
