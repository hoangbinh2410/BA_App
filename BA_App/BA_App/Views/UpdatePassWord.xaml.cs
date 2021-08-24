using BA_App.DataAPI;
using BA_App.Model;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace BA_App.Views
{
    public partial class UpdatePassWord : ContentPage
    {
        private INavigationService _navigationService;
        private User _objUser { get; set; }
        public UpdatePassWord()
        {
            MessagingCenter.Subscribe<UpdatePassWord, User>(this, "ChangePW", (sender, objUser) =>
            {
                _objUser = objUser;
            });
            MessagingCenter.Subscribe<UpdatePassWord, INavigationService>(this, "ChangePW", (sender, navigationService) =>
            {
                _navigationService = navigationService;
            });
            InitializeComponent();
        }        
    }
}
