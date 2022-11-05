using Microsoft.Maui.Controls.Foldable;

namespace dotnetConfDemo;

public partial class MainPage : ContentPage
{
    private readonly INavigation navigation;
    private readonly Shell shell;
    private readonly IApplication application;
    int count = 0;

    public MainPage(INavigation navigation, Shell shell, IApplication application)
    {
        InitializeComponent();
        this.navigation = navigation;
        this.shell = shell;
        this.application = application;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    void Button_Clicked(object sender, EventArgs e)
    {
        navigation.PushAsync(new ContentPage()
        {
            Content = new Label()
            {
                Text = "secont page"
            }
        });
    }
}