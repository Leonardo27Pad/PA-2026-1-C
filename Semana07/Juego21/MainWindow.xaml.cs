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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Juego21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string imagesPath = "C:\\Imagenes";

        private List<string> deck;
        private int p1Total;
        private int p2Total;
        private bool p1Plantado;
        private bool p2Plantado;

        public MainWindow()
        {
            InitializeComponent();
            // preparar estado inicial: mazo construido pero sin repartir
            BuildDeck();
            ResetHands();
            // desactivar controles de jugadores hasta que presione Nuevo Juego
            btnP1Pedir.IsEnabled = false;
            btnP1Plantarse.IsEnabled = false;
            btnP2Pedir.IsEnabled = false;
            btnP2Plantarse.IsEnabled = false;
            if (btnNuevoJuego != null) btnNuevoJuego.IsEnabled = true;
        }

        private void BuildDeck()
        {
            // inicializar mazo con nombres de archivos esperados
            deck = new List<string>();
            string[] suits = new[] { "C", "D", "E", "T" };
            for (int r = 1; r <= 13; r++)
            {
                foreach (var s in suits)
                {
                    deck.Add($"{r}{s}");
                }
            }
            deck.Add("black_jocker");
            deck.Add("red_jocker");
        }

        private void InitGame()
        {
            // barajar y comenzar juego
            ShuffleDeck();
            // limpiar manos
            ResetHands();

            // Deshabilitar nuevo juego mientras se juega
            if (btnNuevoJuego != null) btnNuevoJuego.IsEnabled = false;

            // Repartir 2 cartas obligatorias a cada jugador
            for (int i = 0; i < 2; i++)
            {
                // Jugador 1
                var c1 = DrawCard();
                p1CardsPanel.Children.Add(LoadCardImage(c1));
                p1Total += CardValue(c1);

                // Jugador 2
                var c2 = DrawCard();
                p2CardsPanel.Children.Add(LoadCardImage(c2));
                p2Total += CardValue(c2);
            }

            lblJugador1.Text = $"Jugador 1 Puntos: {p1Total}";
            lblJugador2.Text = $"Jugador 2 Puntos: {p2Total}";
            txtStatus.Text = "Se repartieron 2 cartas a cada jugador. Turno: Jugador 1.";

            // Verificar si alguien ya se pasó
            if (p1Total > 21)
            {
                txtStatus.Text = "Jugador 1 se pasó al repartir. Jugador 2 gana.";
                EndGame();
            }
            else if (p2Total > 21)
            {
                txtStatus.Text = "Jugador 2 se pasó al repartir. Jugador 1 gana.";
                EndGame();
            }
        }

        private void ShuffleDeck()
        {
            var rnd = new Random();
            deck = deck.OrderBy(x => rnd.Next()).ToList();
        }

        private void ResetHands()
        {
            p1Total = 0;
            p2Total = 0;
            p1Plantado = false;
            p2Plantado = false;
            lblJugador1.Text = "Jugador 1 Puntos: 0";
            lblJugador2.Text = "Jugador 2 Puntos: 0";
            p1CardsPanel.Children.Clear();
            p2CardsPanel.Children.Clear();
            txtStatus.Text = "Nuevo juego. Jugador 1 comienza.";
            btnP1Pedir.IsEnabled = true;
            btnP1Plantarse.IsEnabled = true;
            btnP2Pedir.IsEnabled = true;
            btnP2Plantarse.IsEnabled = true;
        }

        private int CardValue(string cardName)
        {
            // formato: number + suit, ex: 1C .. 13T
            if (cardName.StartsWith("black") || cardName.StartsWith("red")) return 0;
            if (!int.TryParse(new string(cardName.TakeWhile(char.IsDigit).ToArray()), out int number)) return 0;
            if (number == 1) return 1; // As = 1 según petición
            if (number >= 11 && number <= 13) return 10;
            return number;
        }

        private Image LoadCardImage(string cardName)
        {
            var img = new Image { Width = 60, Height = 90, Margin = new Thickness(3) };
            string[] exts = new[] { ".png", ".jpg", ".jpeg", ".bmp" };
            foreach (var ext in exts)
            {
                var path = System.IO.Path.Combine(imagesPath, cardName + ext);
                if (File.Exists(path))
                {
                    try
                    {
                        var bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.UriSource = new Uri(path, UriKind.Absolute);
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        bmp.EndInit();
                        img.Source = bmp;
                        return img;
                    }
                    catch { }
                }
            }
            // si no existe, devolver un placeholder
            img.Source = null;
            return img;
        }

        private string DrawCard()
        {
            if (deck.Count == 0) InitGame();
            var card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

        private void NuevoJuego_Click(object sender, RoutedEventArgs e)
        {
            InitGame();
        }

        private void BtnP1Pedir_Click(object sender, RoutedEventArgs e)
        {
            if (p1Plantado) return;
            var card = DrawCard();
            var img = LoadCardImage(card);
            p1CardsPanel.Children.Add(img);
            p1Total += CardValue(card);
            lblJugador1.Text = $"Jugador 1 Puntos: {p1Total}";
            if (p1Total > 21)
            {
                txtStatus.Text = "Jugador 1 se pasó de 21. Jugador 2 gana.";
                EndGame();
            }
        }

        private void BtnP1Plantarse_Click(object sender, RoutedEventArgs e)
        {
            p1Plantado = true;
            btnP1Pedir.IsEnabled = false;
            btnP1Plantarse.IsEnabled = false;
            if (p2Plantado)
            {
                txtStatus.Text = "Jugador 1 se plantó. Ambos jugadores plantados. Comparando resultados...";
                CompareResults();
            }
            else
            {
                txtStatus.Text = "Jugador 1 se plantó. Turno Jugador 2.";
            }
        }

        private void BtnP2Pedir_Click(object sender, RoutedEventArgs e)
        {
            if (p2Plantado) return;
            var card = DrawCard();
            var img = LoadCardImage(card);
            p2CardsPanel.Children.Add(img);
            p2Total += CardValue(card);
            lblJugador2.Text = $"Jugador 2 Puntos: {p2Total}";
            if (p2Total > 21)
            {
                txtStatus.Text = "Jugador 2 se pasó de 21. Jugador 1 gana.";
                EndGame();
            }
        }

        private void BtnP2Plantarse_Click(object sender, RoutedEventArgs e)
        {
            p2Plantado = true;
            btnP2Pedir.IsEnabled = false;
            btnP2Plantarse.IsEnabled = false;
            if (p1Plantado)
            {
                txtStatus.Text = "Jugador 2 se plantó. Ambos jugadores plantados. Comparando resultados...";
                CompareResults();
            }
            else
            {
                txtStatus.Text = "Jugador 2 se plantó. Jugador 1 aún puede jugar.";
            }
        }

        private void CompareResults()
        {
            string result;
            if (p1Total > 21 && p2Total > 21) result = "Ambos se pasaron. Empate.";
            else if (p1Total > 21) result = $"¡GANADOR JUGADOR 2! Obtuvo {p2Total} puntos frente a {p1Total} del J1.";
            else if (p2Total > 21) result = $"¡GANADOR JUGADOR 1! Obtuvo {p1Total} puntos frente a {p2Total} del J2.";
            else if (p1Total == p2Total) result = "Empate.";
            else if (p1Total > p2Total) result = $"¡GANADOR JUGADOR 1! Obtuvo {p1Total} puntos frente a {p2Total} del J2.";
            else result = $"¡GANADOR JUGADOR 2! Obtuvo {p2Total} puntos frente a {p1Total} del J1.";

            txtStatus.Text = result;
            EndGame();
        }

        private void EndGame()
        {
            btnP1Pedir.IsEnabled = false;
            btnP1Plantarse.IsEnabled = false;
            btnP2Pedir.IsEnabled = false;
            btnP2Plantarse.IsEnabled = false;
            if (btnNuevoJuego != null) btnNuevoJuego.IsEnabled = true;
        }
    }
}