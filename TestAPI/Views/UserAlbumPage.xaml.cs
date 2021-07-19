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

namespace TestAPI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAlbumPage : ContentPage
    {

        private const string url = "http://jsonplaceholder.typicode.com/albums";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Album> _album;

        public UserAlbumPage()
        {
            InitializeComponent();

            search_bar3.BackgroundColor = Color.White;
            search_bar3.PlaceholderColor = Color.Black;
            search_bar3.TextColor = Color.Black;

            search_bar3.TextChanged += (sender, e) => FilterByUserId(search_bar3.Text);
        }

        protected override async void OnAppearing()
        {
            var result = await _client.GetStringAsync(url);
            var json = JsonConvert.DeserializeObject<List<Album>>(result);
            _album = new ObservableCollection<Album>(json);

            base.OnAppearing();
        }

        public void FilterByUserId(string UID)
        {

            if (string.IsNullOrWhiteSpace(UID))
            {
                UserAlbumCollection.ItemsSource = _album;
            }
            else
            {
                UserAlbumCollection.ItemsSource = _album.Where(x => x.userId.ToString() == UID);
            }
        }
    }
}