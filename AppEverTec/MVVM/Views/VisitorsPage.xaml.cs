using AppEverTec.MVVM.ViewModels;
using AppEverTec.Services;

namespace AppEverTec.MVVM.Views;

public partial class VisitorsPage : ContentPage
{
	public VisitorsPage()
	{
		InitializeComponent();
		BindingContext = new VisitorViewModel(new DataService());
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AddVisitorPage(new DataService(),0));
    }
}