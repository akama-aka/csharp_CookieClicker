/*
 *  (C) by Akama Aka
 *  LICENSE: ASPL 1.0 | https://licenses.akami-solutions.cc/
 * 
 */

using System.Text.Json;

namespace MauMau
{
    public partial class MainPage : ContentPage
    {
        private int _count = 1;
        private HttpClient _client;
        private string apiEndpoint = "https://localhost:7096/api/Counter";
        private string apiEndpointTwo = "https://localhost:7096/api/Counter/1";
        public MainPage()
        {
            _count = 0;
            _client = new HttpClient();
            LoadCurrentCount();
            InitializeComponent();
            
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {

            _count++;
            await UpdateCounterOnApi();
        }

        private async Task UpdateCounterOnApi()
        {
            Counter counterDto = new()
            {
                id = 1,
                count = Convert.ToInt32(_count)
            };
            var json = JsonSerializer.Serialize(counterDto);
            HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(apiEndpoint + $"/{counterDto.id}", content);
            if (response.IsSuccessStatusCode)
            {
                CounterBtn.Text = $"Cookies earned: {_count}";
                SemanticScreenReader.Announce(CounterBtn.Text);
            }
        }
        private async void LoadCurrentCount()
        {
            var response = await _client.GetAsync(apiEndpoint);
        
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Counter>(jsonString);
                _count = data.count;
                CounterBtn.Text = _count > 0
                    ? $"Clicked {_count} times"
                    : "Click me to continue collecting Cookies";
            }
        }
        private async void LicenseClicked(object sender, EventArgs e)
        {

           await Browser.OpenAsync("https://licenses.akami-solutions.cc");
        }
        private async void OnResetClicked(object sender, EventArgs e)
        {
            Counter counterDto = new()
            {
                id = 1,
                count = 0
            };
            var json = JsonSerializer.Serialize(counterDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(apiEndpoint + $"/{counterDto.id}", content);
            if (response.IsSuccessStatusCode)
            {
                _count = 0;
                CounterBtn.Text = $"Click me to continue collecting Cookies";
                SemanticScreenReader.Announce(CounterBtn.Text);
            }
        }
        public class Counter 
        {
            public int id { get; set; }
            public int count { get; set; }
        }
    }

}


/**
⣿⣿⣿⣿⣿⣿⣿⣿⡿⢻⣿⣿⣿⣿⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⢻⣿⣿⡏⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠈⣿⣿⠃⢰⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⡇⢹⣿⠀⢸⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⢹⢸⡏⠀⣸⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣇⠀⢸⢸⣿⠀⡿⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠈⢸⣿⠀⠁⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠘⠁⠀⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⣿⣿⣿⣿⣿⣿⠟⠋⠁⣀⣤⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⣠⠀⠀⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⣿⣿⣿⣿⠋⠁⠀⠀⠺⠿⢿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠻⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣾⣿⣿⡯⢰⣤⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣤⣤⣤⣤⠀⠀⠀⠀⠀⣤⣦⣄⠀⠀
⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢹⣿⣿⣧⣀⣹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⣶⣿⠏⣿⣿⣿⣿⣿⣁⠀⠀⠀⠛⠙⠛⠋⠀⠀
⣿⣿⣿⣿⣿⣿⣿⠟⠋⠀⣼⣿⣿⣿⡿⣿⣿⣿⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⡿⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⣰⣿⣿⣿⣿⡄⠘⣿⣿⣿⣿⣷⠄⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⣿⣿⡿⠛⠁⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣆⠈⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⡇⠀⠀⠀⠀⠀⠀⠀⠸⠇⣼⣿⣿⣿⣿⣿⣷⣄⠘⢿⣿⣿⣿⣅⠀⠀⠀⠀⠀⠀
⣿⣿⣿⠟⠁⠀⠀⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠙⣿⣿⣿⣿⣿⣿⣿⣿  ⠁⠀⠀⠀⣴⣿⠀⣐⣣⣸⣿⣿⣿⣿⣿⠟⠛⠛⠀⠌⠻⣿⣿⣿⡄⠀⠀⠀⠀
⣿⣿⡏⠀⠀⠀⠀⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠘⣿⣿⣿⣿⣿⣿⣿  ⠀⠀⠀⣶⣮⣽⣰⣿⡿⢿⣿⣿⣿⣿⣿⡀⢿⣤⠄⢠⣄⢹⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀
⣿⣿⢇⣿⣷⡀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣾⣿⣽⣿⣿⣿⣿⣿⣿  ⠀⠀⠀⣿⣿⣿⣿⣿⡘⣿⣿⣿⣿⣿⣿⠿⣶⣶⣾⣿⣿⡆⢻⣿⣿⠃⢠⠖⠛⣛⣷⠀  
⣿⣿⢸⣿⣿⣟⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠿⠿⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⠀⠀⢸⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⣿⣮⣝⡻⠿⠿⢃⣄⣭⡟⢀⡎⣰⡶⣪⣿⠀  
⣿⣿⣯⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣶⣶⣶⣶⣤⣍⣉⣛⠛⠻⠛⠻⣿⣿  ⠀⠀⠘⣿⣿⣿⠟⣛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣿⡿⢁⣾⣿⢿⣿⣿⠏⠀  
⣿⣿⣿⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⡟⢿⣿⡿⡿⠿⢿⣿⣿⣿⣿⣿⣿⢸⣷⡌⢻  ⠀⠀⠀⣻⣿⡟⠘⠿⠿⠎⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣵⣿⣿⠧⣷⠟⠁⠀⠀    
⣿⣿⣿⣇⠀⠀⠀⠻⢿⣿⣿⣿⣿⡇⢸⣿⡅⡇⠰⣶⣤⣭⣭⣭⣭⣉⣘⣛⣋⣼  ⡇⠀⠀⢹⣿⡧⠀⡀⠀⣀⠀⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠋⢰⣿⠀⠀⠀⠀
⣿⣿⣿⣿⣧⡀⠀⠀⠀⢉⠙⠛⠛⠁⣿⣿⠁⠇⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⡇⠀⠀⠀⢻⢰⣿⣶⣿⡿⠿⢂⣿⣿⣿⣿⣿⣿⣿⢿⣻⣿⣿⣿⡏⠀⠀⠁⠀⠀⠀⠀ 
⣿⣿⣿⣿⣦⣀⡀⢀⣀⣠⣴⣾⣶⣶⣦⣤⣴⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿  ⣷⠀⠀⠀⠀⠈⠿⠟⣁⣴⣾⣿⣿⠿⠿⣛⣋⣥⣶⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀ ⣿⡀⠀
⣿⣿⣻⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀
⠀⠀⠀⠀
***/