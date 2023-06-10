namespace AppEverTec.MVVM.Views;

public partial class ClimatePage : ContentPage
{
	public ClimatePage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(100);
        climate.IsAnimationPlaying = true;
        DateNow.Text=DateTime.Now.ToString("dd MMMM | HH:mm");
    }
}