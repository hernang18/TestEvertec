using AppEverTec.MVVM.ViewModels;
using AppEverTec.Services;

namespace AppEverTec.MVVM.Views;

public partial class AddVisitorPage : ContentPage
{
	public AddVisitorPage(DataService dataService,int id)
	{
		InitializeComponent();
		BindingContext = new AddVisitorViewModel(dataService, id);
	}
}