using AppEverTec.MVVM.ViewModels;
using AppEverTec.Services;

namespace AppEverTec.MVVM.Views;

public partial class ContacsPage : ContentPage
{
	public ContacsPage()
	{
		InitializeComponent();
		BindingContext = new ContactsViewModel(new ApiService());
	}
}