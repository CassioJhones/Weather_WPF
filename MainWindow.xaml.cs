using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TimeVersion.Deserializacao;

namespace TimeVersion
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        #region Propriedades e Constantes

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly HttpClient _httpClient;
        private const string ApiKey = "f24e3b3b88dc280e84e540c4500113d5";
        private string _apiStatus = "";
        public string ApiStatus
        {
            get { return _apiStatus; }
            set
            {
                if (_apiStatus != value)
                {
                    _apiStatus = value;
                    OnPropertyChanged(nameof(ApiStatus));
                }
            }
        }
        private string _resultadoPesquisa = "";
        public string ResultadoPesquisa
        {
            get { return _resultadoPesquisa; }
            set
            {
                if (_resultadoPesquisa != value)
                {
                    _resultadoPesquisa = value;
                    OnPropertyChanged(nameof(ResultadoPesquisa));
                }
            }
        }
        private string _tituloResultadoPesquisa = "";
        public string TituloResultadoPesquisa
        {
            get { return _tituloResultadoPesquisa; }
            set
            {
                if (_tituloResultadoPesquisa != value)
                {
                    _tituloResultadoPesquisa = value;
                    OnPropertyChanged(nameof(TituloResultadoPesquisa));
                }
            }
        }
        private string _corResultado = "SlateBlue";
        public string CorResultado
        {
            get { return _corResultado; }
            set
            {
                if (_corResultado != value)
                {
                    _corResultado = value;
                    OnPropertyChanged(nameof(CorResultado));
                }
            }
        }

        #endregion Propriedades e Constantes
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            DataContext = this;

            if (string.IsNullOrWhiteSpace(ApiKey) || ApiKey.Length < 32)
                ApiStatus = "Offline";
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
            => await CheckApiStatus();
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public async void BuscarPrevisaoClick(object sender, RoutedEventArgs evento)
        {
            string cidade = txtCidade.Text.Trim();
            try
            {
                string previsao = await ObterPrevisaoDoTempo(cidade);
                string previsaoDescricao = await ObterDescricaoDoCeu(cidade);
                ResultadoPesquisa = previsao;
                TituloResultadoPesquisa = previsaoDescricao;
                CorResultado = "SlateBlue";
            }
            catch (HttpRequestException ex)
            {
                string mensagemErro = ex.Message.Contains("Unauthorized")
                    ? "Falha na API" : ex.Message.Contains("Not Found") ? "Cidade Inexistente" : "Campo Vazio";
                TituloResultadoPesquisa = mensagemErro;
                CorResultado = "Red";
            }
        }

        public async Task<string> ObterPrevisaoDoTempo(string cidade)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={ApiKey}&units=metric&lang=pt";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Root cidadeEscolhida = JsonSerializer.Deserialize<Root>(json);

            double velocidadeVento = cidadeEscolhida.Vento.Velocidade;
            int umidade = cidadeEscolhida.Main.Umidade;

            string frase = $"{cidadeEscolhida.Nome} - {cidadeEscolhida.Sys.Pais}\nTemperatura: {cidadeEscolhida.Main.Temperatura}°C\nSensação: {cidadeEscolhida.Main.SensacaoTermica}°C\nVento: {velocidadeVento} Km/h\nUmidade: {umidade}%";
            return frase;
        }
        public async Task<string> ObterDescricaoDoCeu(string cidade)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={ApiKey}&units=metric&lang=pt";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Root cidadeEscolhida = JsonSerializer.Deserialize<Root>(json);

            string titulo = cidadeEscolhida.Clima[0].Descricao.ToUpperInvariant();

            if (titulo.Contains("NUVENS QUEBRADAS"))
                titulo = "PARCIALMENTE NUBLADO";

            return titulo;
        }
        public async Task CheckApiStatus()
        {
            HttpResponseMessage response = await GetApi();
            ApiStatus = !response.IsSuccessStatusCode ? "Offline" : "Online";
        }
        public async Task<HttpResponseMessage> GetApi()
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q=brasil&appid={ApiKey}&units=metric&lang=pt";
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            { return response; }

            return response;
        }
        public void Limpar_Click(object sender, RoutedEventArgs e)
        {
            ResultadoPesquisa = "";
            TituloResultadoPesquisa = "";
        }

        public void Pesquisar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ResultadoPesquisa)) return;

            string cortaCidade = ResultadoPesquisa.Replace("-", "|").Split('|')[0].Trim();
            Process.Start($"https://pt.wikipedia.org/wiki/{cortaCidade}");
            Process.Start($"https://www.google.com.br/maps/place/{cortaCidade}");
        }
    }
}
