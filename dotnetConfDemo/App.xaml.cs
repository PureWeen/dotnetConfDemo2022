namespace dotnetConfDemo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        // These services are scoped to the window
        var scopedService = activationState.Context.Services;
        var window = scopedService.GetRequiredService<Window>();
        return window;
    }
}