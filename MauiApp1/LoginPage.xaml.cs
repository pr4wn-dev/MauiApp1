using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginPageViewModel();
    }



    

}