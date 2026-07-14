using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TragamonedasV2
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer; 
        private int segundos = 0;
        private int puntaje = 0;
        private BitmapImage bitmapImage;
        private Uri uri;

        private DispatcherTimer _clockTimer; 
        private Button _btnIniciarRef; 
        private readonly Random _rand = new Random();
        private readonly string[] _imageFiles = new string[] { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png" };

        public MainWindow()
        {
            InitializeComponent();
            InitializeClock(); 

            uri = new Uri(@"C:\Imagenes\1.png");
            imagen1.Source = new BitmapImage(uri);
            imagen2.Source = new BitmapImage(uri);
            imagen3.Source = new BitmapImage(uri);
        }

        #region Reloj Superior (Formato 1)
        private void InitializeClock()
        {
            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += ClockTimer_Tick;
            UpdateClockText();
            _clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClockText();
        }

        private void UpdateClockText()
        {
            if (l_reloj != null)
            {
                l_reloj.Content = DateTime.Now.ToLongTimeString();
            }
        }
        #endregion

        #region Control de Juego (Formato 1 + Cosas de 2)
        private void iniciar_Click(object sender, RoutedEventArgs e)
        {
            _btnIniciarRef = sender as Button;
            if (_btnIniciarRef != null)
            {
                _btnIniciarRef.IsEnabled = false;
            }
            StartGame();
        }

        private void StartGame()
        {
            segundos = 0;
            puntaje = 0;
            UpdateScoreText();
            l_msj.Content = string.Empty;

            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
            }

            DoGameTick(); 
            timer.Start();
        }

        private void StopGame()
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
            if (_btnIniciarRef != null)
            {
                _btnIniciarRef.IsEnabled = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DoGameTick();
        }

        private void DoGameTick()
        {
            segundos++;

            int a = _rand.Next(0, _imageFiles.Length);
            int b = _rand.Next(0, _imageFiles.Length);
            int c = _rand.Next(0, _imageFiles.Length);

            SetImageSource(imagen1, _imageFiles[a]);
            SetImageSource(imagen2, _imageFiles[b]);
            SetImageSource(imagen3, _imageFiles[c]);

            int added = 0;
            if (a == b && b == c)
            {
                added = 20;
            }
            else if (a == b || a == c || b == c)
            {
                added = 10;
            }

            puntaje += added;
            UpdateScoreText();

            if (puntaje >= 60)
            {
                EndGame(true);
                return;
            }

            if (segundos >= 6)
            {
                EndGame(false);
                return;
            }
        }

        private void EndGame(bool won)
        {
            StopGame();

            if (won)
            {
                l_msj.Content = "GANASTE";
            }
            else
            {
                l_msj.Content = $"PERDISTE, puntaje obtenido: {puntaje}";
            }

            Pregunta(); 
        }

        private void Pregunta()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Deseas seguir jugando?", "Consulta", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Reiniciar();
            }
            else
            {
                this.Close();
            }
        }

        private void b_limpiar_Click(object sender, RoutedEventArgs e)
        {
            StopGame();
            l_msj.Content = "";
            l_puntaje.Content = "";

            uri = new Uri(@"C:\Imagenes\1.png");
            imagen1.Source = new BitmapImage(uri);
            imagen2.Source = new BitmapImage(uri);
            imagen3.Source = new BitmapImage(uri);
        }

        private void Reiniciar()
        {
            l_msj.Content = "";
            l_puntaje.Content = "";
            UpdateClockText();

            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@"C:\Imagenes\1.png");
            bitmapImage.EndInit();

            imagen1.Source = bitmapImage;
            imagen2.Source = bitmapImage;
            imagen3.Source = bitmapImage;

            StartGame(); 
        }
        #endregion

        #region Métodos Auxiliares (Formato 1)
        private void UpdateScoreText()
        {
            if (l_puntaje != null)
            {
                l_puntaje.Content = puntaje.ToString();
            }
        }

        private void SetImageSource(Image imgControl, string fileName)
        {
            try
            {
                if (imgControl == null) return;

                string path = Path.Combine(@"C:\Imagenes\", fileName);

                if (File.Exists(path))
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(path, UriKind.Absolute);
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    imgControl.Source = bmp;
                }
            }
            catch
            { }
        }
        #endregion
    }
}