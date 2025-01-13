using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;


namespace MauiApp1.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set => SetProperty(ref _userEmail, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            RegisterCommand = new Command(ExecuteRegisterCommand);
        }
        private async void ExecuteRegisterCommand()
        {
            // Handle the registration logic here
            // For example:
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
        private async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(UserEmail) || string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "Please enter both username and password.";
                return;
            }

            StatusMessage = "Logging in...";

            var client = new HttpClient();
            var content = new StringContent($"email={Uri.EscapeDataString(UserEmail)}&password={Password}&login_submit=1", Encoding.UTF8, "application/x-www-form-urlencoded");

            try
            {
                var response = await client.PostAsync("https://hiatme.com/login.php", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Adjust this based on what your server returns for success/failure
                    if (result.Contains("Logout"))
                    {
                        StatusMessage = "Login successful!";
                        // You might want to navigate to another page or update app state here
                    }
                    else
                    {
                        StatusMessage = "Login failed. Please try again.";
                    }
                }
                else
                {
                    StatusMessage = "Error: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "An error occurred: " + ex.Message;
            }
        }
    }
}