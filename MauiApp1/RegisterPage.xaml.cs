using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var name = nameEntry.Text; // Add this entry field to XAML
        var phone = phoneEntry.Text; // Add this entry field to XAML
        var password = passwordEntry.Text;
        var confirmPassword = confirmPasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            await DisplayAlert("Error", "Please fill all fields.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        var content = new StringContent($"email={Uri.EscapeDataString(email)}&name={Uri.EscapeDataString(name)}&phone={Uri.EscapeDataString(phone)}&password={Uri.EscapeDataString(password)}&confirm_password={Uri.EscapeDataString(confirmPassword)}&reg_submit=Submit", Encoding.UTF8, "application/x-www-form-urlencoded");
        var client = new HttpClient();
        var response = await client.PostAsync("https://hiatme.com/register.php", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Success", "Registration successful!", "OK");
            await Navigation.PopAsync(); // Go back to login page
        }
        else
        {
            await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
        }
    }
}
