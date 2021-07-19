using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private const string url = "http://jsonplaceholder.typicode.com/users";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<User> _user;

        public UserPage()
        {
            InitializeComponent();

            search_bar.BackgroundColor = Color.White;
            search_bar.PlaceholderColor = Color.Black;
            search_bar.TextColor = Color.Black;

            search_bar.TextChanged += (sender, e) => FilterById(search_bar.Text);
        }

        protected override async void OnAppearing()
        {
            var result = await _client.GetStringAsync(url);
            var json = JsonConvert.DeserializeObject<List<User>>(result);
            _user = new ObservableCollection<User>(json);
            UserCollection.ItemsSource = _user;
            base.OnAppearing();
        }

        public void FilterById(string texte)
        {

            if (string.IsNullOrWhiteSpace(texte))
            {
                UserCollection.ItemsSource = _user;
            }
            else
            {
                UserCollection.ItemsSource = _user.Where(x => x.id.ToString() == texte);
            }
        }
    }
}