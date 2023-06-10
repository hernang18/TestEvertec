using AppEverTec.MVVM.ViewModels;
using AppEverTec.Services;

namespace AppEverTec.MVVM.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
        InitializeComponent();
        BindingContext = new HomeViewModel(new ChangeColor(),true,new DataService());       
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var viewModel= new HomeViewModel(new ChangeColor(), e.Value,new DataService());
        BindingContext = viewModel;
        
    }
}