using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TareaBingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cartilla cartilla;
        private Bingo bingo;
        private DispatcherTimer autoJuego;
        private bool juegoProceso = false;

        private Border[,] matrizBorder = new Border[5, 5];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerarCartilla_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cartilla = new Cartilla();
                cartilla.Generar();
                RenderizarCartilla();

                btnIniciarJuego.IsEnabled = true;
                btnSacarBolilla.IsEnabled = false;
                btnAutoJuego.IsEnabled = false;
                juegoProceso = false;

                txtLetraBolilla.Text = "-";
                txtNumeroBolilla.Text = "--";
                lstHistorial.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la cartilla: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            autoJuego = new DispatcherTimer();
            autoJuego.Interval = TimeSpan.FromMilliseconds(600);
            autoJuego.Tick += timer_Tick;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            EjecutarPasoJuego();
        }

        private void EjecutarPasoJuego()
        {
            try
            {
                if (!juegoProceso) return;

                int bolilla = bingo.SacarBolilla();
                string letra = bingo.ObtenerLetra(bolilla);

                txtLetraBolilla.Text = letra;
                txtNumeroBolilla.Text = bolilla.ToString();
                lstHistorial.Items.Insert(0, "[ " + letra + " ] - " + bolilla);

                cartilla.MarcarNumero(bolilla);

                if (cartilla.EsLineaCompleta())
                {
                    cartilla.MarcarCasillasGanadoras();
                    ActualizarColores();
                    FinalizarJuego(true);
                }
                else
                {
                    ActualizarColores();
                }
            }
            catch (InvalidOperationException ex)
            {
                autoJuego.Stop();
                MessageBox.Show(ex.Message, "Fin de Bolillas", MessageBoxButton.OK, MessageBoxImage.Information);
                FinalizarJuego(false);
            }
            catch (Exception ex)
            {
                autoJuego.Stop();
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void btnIniciarJuego_Click(object sender, RoutedEventArgs e)
        {
            if (cartilla == null)
            {
                MessageBox.Show("Debes generar una cartilla primero.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bingo = new Bingo();
            juegoProceso = true;

            btnGenerarCartilla.IsEnabled = false;
            btnIniciarJuego.IsEnabled = false;
            btnSacarBolilla.IsEnabled = true;
            btnAutoJuego.IsEnabled = true;
        }

        private void btnSacarBolilla_Click(object sender, RoutedEventArgs e)
        {
            EjecutarPasoJuego();
        }

        private void btnAutoJuego_Click(object sender, RoutedEventArgs e)
        {
            if (autoJuego.IsEnabled)
            {
                autoJuego.Stop();
                btnAutoJuego.Content = "Modo Automático";
                btnSacarBolilla.IsEnabled = true;
            }
            else
            {
                autoJuego.Start();
                btnAutoJuego.Content = "Pausar Automático";
                btnSacarBolilla.IsEnabled = false;
            }
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            if (autoJuego != null)
            {
                autoJuego.Stop();
            }

            juegoProceso = false;
            cartilla = null;
            bingo = null;

            bingoCartilla.Children.Clear();
            txtLetraBolilla.Text = "-";
            txtNumeroBolilla.Text = "--";
            lstHistorial.Items.Clear();

            btnGenerarCartilla.IsEnabled = true;
            btnIniciarJuego.IsEnabled = false;
            btnSacarBolilla.IsEnabled = false;
            btnAutoJuego.IsEnabled = false;
            btnAutoJuego.Content = "Modo Automático";
        }

        private void RenderizarCartilla()
        {
            bingoCartilla.Children.Clear();

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Casilla casilla = cartilla.Matriz[f, c];

                    Border border = new Border
                    {
                        Margin = new Thickness(2),
                        Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC99CC"))
                    };

                    TextBlock txt = new TextBlock
                    {
                        Text = casilla.EsEspacioLibre ? "*" : casilla.Numero.ToString(),
                        FontSize = casilla.EsEspacioLibre ? 20 : 16,
                        FontWeight = FontWeights.Bold,
                        Foreground = Brushes.White,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    if (casilla.EsEspacioLibre)
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9E2AF"));
                        txt.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#11111B"));
                    }

                    border.Child = txt;

                    matrizBorder[f, c] = border;
                    bingoCartilla.Children.Add(border);
                }
            }
        }
        private void ActualizarColores()
        {
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Casilla casilla = cartilla.Matriz[f, c];
                    Border border = matrizBorder[f, c];
                    TextBlock txt = (TextBlock)border.Child;

                    if (casilla.EsGanadora)
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2563EB"));
                        border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#60A5FA"));
                        border.BorderThickness = new Thickness(2);
                        txt.Foreground = Brushes.White;
                    }
                    else if (casilla.Marcado && !casilla.EsEspacioLibre)
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6E3A1"));
                        txt.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#11111B"));
                    }
                }
            }
        }

        private void FinalizarJuego(bool victoria)
        {
            juegoProceso = false;
            autoJuego.Stop();

            btnGenerarCartilla.IsEnabled = false;
            btnIniciarJuego.IsEnabled = false;
            btnSacarBolilla.IsEnabled = false;
            btnAutoJuego.IsEnabled = false;

            if (victoria)
            {
                MessageBox.Show("Has completado una línea de 5 casillas y ganado la partida!\n",
                                "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("El juego ha terminado. Se agotaron las bolillas.",
                                "Fin del Juego", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
    }
}