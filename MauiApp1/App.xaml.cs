namespace MauiApp1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
        var window = new Window(new AppShell());
        window.Width = 430;
        window.Height = 570;

        // Optional: Position the window
        window.X = 100;
        window.Y = 100;
        return window;
	}
}