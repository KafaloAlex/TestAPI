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
    public partial class AlbumPage : ContentPage
    {
        private const string url = "http://jsonplaceholder.typicode.com/albums";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Album> _album;

        public AlbumPage()
        {
            InitializeComponent();

            search_bar2.BackgroundColor = Color.White;
            search_bar2.PlaceholderColor = Color.Black;
            search_bar2.TextColor = Color.Black;

            search_bar2.TextChanged += (sender, e) => FilterById(search_bar2.Text);
        }

        protected override async void OnAppearing()
        {
            var result = await _client.GetStringAsync(url);
            var json = JsonConvert.DeserializeObject<List<Album>>(result);
            _album = new ObservableCollection<Album>(json);

            AlbumCollection.ItemsSource = _album;
            base.OnAppearing();
        }

        public void FilterById(string texte)
        {

            if (string.IsNullOrWhiteSpace(texte))
            {
                AlbumCollection.ItemsSource = _album;
            }
            else
            {
                AlbumCollection.ItemsSource = _album.Where(x => x.id.ToString() == texte);
            }
        }

    }
}