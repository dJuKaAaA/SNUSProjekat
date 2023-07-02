using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class CreateUserViewModel : ViewModelBase
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}

		private string _surname;

		public string Surname
		{
			get { return _surname; }
			set { _surname = value; OnPropertyChanged(); }
		}

		private string _username;

		public string Username
		{
			get { return _username; }
			set { _username = value; OnPropertyChanged(); }
		}

		private string _password;

		public string Password
		{
			get { return _password; }
			set { _password = value; OnPropertyChanged(); }
		}

		private string _confirmPassword;

		public string ConfirmPassword
		{
			get { return _confirmPassword; }
			set { _confirmPassword = value; OnPropertyChanged(); }
		}

		private readonly CoreUserRef.UserServiceClient _userServiceClient;

		public ICommand CreateUserCommand { get; }

        public CreateUserViewModel()
        {
			_userServiceClient = new CoreUserRef.UserServiceClient();

			CreateUserCommand = new RelayCommand(OnCreateUser, CanCreateUser);
        }

		private void OnCreateUser(object o)
		{
			if (Password != ConfirmPassword)
			{
				MessageBox.Show("Password not matching", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			CoreUserRef.User newUser = new CoreUserRef.User()
			{
				Name = Name,
				Surname = Surname,
				Username = Username,
				Role = CoreUserRef.Role.Standard
			};

			try
			{
				_userServiceClient.CreateAccount(newUser, Password);
				MessageBox.Show("User created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (FaultException<CoreUserRef.UserDbFault> e)
			{
				MessageBox.Show(e.Detail.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch
			{
				MessageBox.Show("Unknown error has occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}

		private bool CanCreateUser(object o)
		{
			return !string.IsNullOrWhiteSpace(Name) &&
				!string.IsNullOrWhiteSpace(Surname) &&
				!string.IsNullOrWhiteSpace(Username) &&
				!string.IsNullOrWhiteSpace(Password) &&
				!string.IsNullOrWhiteSpace(ConfirmPassword);
		}

    }
}
