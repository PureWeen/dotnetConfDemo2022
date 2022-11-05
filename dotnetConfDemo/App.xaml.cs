namespace dotnetConfDemo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell())
        {
            Width = 1920,
            Height = 1080
        };
    }
}
