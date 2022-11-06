using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows.Media.Animation;

namespace POKEMON_HUD
{
    
    public partial class MainWindow : Window
    {
        
        private int puntuacion = 0;
        private int baja_salud = 0;
        private int i;
        private int golpeCuerpo=2;
        private int lanzallamas=3;
        private int ataque_furia=5;
        private string atFuria = "ATAQUE FURIA";
        private string atGC = "ATAQUE GOLPE CUERPO";
        private string atLanzallamas = "ATAQUE LANZALLAMAS";
        DispatcherTimer modificarVida;

        public MainWindow()
        {
            InitializeComponent();
            modificarVida = new DispatcherTimer();
            modificarVida.Interval = TimeSpan.FromMilliseconds(1500);
            modificarVida.Tick += new EventHandler(ModificararBarra);
            modificarVida.Start();
            
        }
        private void Curar_Pokemon(object sender, MouseButtonEventArgs e)
        {
            puntuacion += 1;
            if (this.pbVida.Value >= 90 && this.pbEscudo.Value == 0)
            {
                this.pbVida.Value = 100;
                this.pbEscudo.Value += 25;
            }
            else
            {
                if(this.pbVida.Value < 100 && this.pbEscudo.Value == 0)
                {
                    this.pbVida.Value += 25;
                }
                if (this.pbVida.Value >= 100 && this.pbEscudo.Value <= 100 && this.pbEscudo.Value > 0)
                {
                    this.pbEscudo.Value += 25;
                }
            }
           
            
        }
        private void ModificararBarra(object sender, EventArgs e)
        {
            if (pbVida.Value <= 0 && pbEscudo.Value <= 0)
            {
                modificarVida.Stop();
                this.pocion.Opacity = 0.0;
                FinalJuego(puntuacion);
            }
            else
            {
                
                if (this.pbVida.Value <= 100 && this.pbEscudo.Value <= 0)
                {
                    this.pbVida.Value -= 10;
                    if (this.pbVida.Value <= 25 && this.pbVida.Value >= 0 && this.pbEscudo.Value <= 0 && baja_salud == 0)
                    {
                        MessageBox.Show("¡¡¡CUIDADO!!! \n Tu salud está muy baja");
                        baja_salud++;
                    }
                }
                else if (this.pbVida.Value >= 100 && this.pbEscudo.Value <= 100)
                {
                    this.pbEscudo.Value -= 10;
                }
                
            }
            if (this.pbVida.Value > 0 && this.pbVida.Value < 25)
            {
                this.pbVida.Foreground = Brushes.Red;
            }
            else if (this.pbVida.Value >= 25 && this.pbVida.Value <= 50)
            {
                this.pbVida.Foreground = Brushes.Yellow;
            }else this.pbVida.Foreground = Brushes.Green;

            if (this.pbEscudo.Value > 0 && this.pbEscudo.Value < 40)
            {
                this.pbEscudo.Foreground = Brushes.HotPink;
            }
            else if (this.pbEscudo.Value >= 40 && this.pbEscudo.Value <= 75)
            {
                this.pbEscudo.Foreground = Brushes.Blue;
            }
            else this.pbEscudo.Foreground = Brushes.DarkViolet;

        }
        private void Ataque_Golpe_Cuerpo(object sender, MouseButtonEventArgs e)
        {
            puntuacion += 3;
            
            for(i=0; i < golpeCuerpo; i++)
            {
                ModificararBarra(sender, e);
            }
            this.nombrePokemon.Foreground = Brushes.BlueViolet;
            this.nombrePokemon.Content = atGC;
            
            Desactivar_Botones();
            Storyboard sbaux = (Storyboard)this.Resources["Golpe_Cuerpo"];
            sbaux.Completed += new EventHandler(Fin_Ataque_Golpe_Cuerpo);
            sbaux.Begin();
           
            
        }
        
        private void Ataque_Lanzallamas(object sender, MouseButtonEventArgs e)
        {
            puntuacion += 5;
            for (i = 0; i < lanzallamas; i++)
            {
                ModificararBarra(sender, e);
            }
            this.nombrePokemon.Foreground = Brushes.OrangeRed;
            this.nombrePokemon.Content = atLanzallamas;
            
            Desactivar_Botones();
            
            Storyboard sbaux = (Storyboard)this.Resources["Lanzallamas"];
            sbaux.Completed += new EventHandler(Fin_Ataque_Lanzallamas);
            sbaux.Begin();
            
        }
        
        private void Ataque_Furia(object sender, MouseButtonEventArgs e)
        {
            
            puntuacion += 8;
            for (i = 0; i < ataque_furia; i++)
            {
                ModificararBarra(sender, e);
            }
            this.nombrePokemon.Foreground = Brushes.Brown;
            this.nombrePokemon.Content = atFuria;
            
            Desactivar_Botones();
            Storyboard sbaux = (Storyboard)this.Resources["Ataque_Furia"];
            sbaux.Completed += new EventHandler(Fin_Ataque_Furia);
            sbaux.Begin();
        }

        private void Fin_Ataque_Furia(object sender, EventArgs e)
        {
            if(this.nombrePokemon.Foreground.Equals(Brushes.Brown))
            {
                
                Activar_Botones();
                this.nombrePokemon.Foreground = Brushes.Black;
                this.nombrePokemon.Content = "Charmander";
            }
            

        }
        private void Fin_Ataque_Lanzallamas(object sender, EventArgs e)
        {
            if (this.nombrePokemon.Foreground.Equals(Brushes.OrangeRed))
            {
                
                Activar_Botones();
                this.nombrePokemon.Foreground = Brushes.Black;
                this.nombrePokemon.Content = "Charmander";
            } 
        }
        private void Fin_Ataque_Golpe_Cuerpo(object sender, EventArgs e)
        {
            if (this.nombrePokemon.Foreground.Equals(Brushes.BlueViolet))
            {
                
                Activar_Botones();
                this.nombrePokemon.Foreground = Brushes.Black;
                this.nombrePokemon.Content = "Charmander";
            }
        }
        
        private void Desactivar_Botones()
        {
            this.Golpe_Cuerpo.IsEnabled = false;
            this.Furia.IsEnabled = false;
            this.Lanzallamas.IsEnabled = false;
            this.curar_pokemon.IsEnabled = false;
        }
        private void Activar_Botones()
        {
            this.Golpe_Cuerpo.IsEnabled = true;
            this.Furia.IsEnabled = true;
            this.Lanzallamas.IsEnabled = true;
            this.curar_pokemon.IsEnabled = true;
        }
        private void FinalJuego(int puntuacion)
        {
            MessageBox.Show("Game Over. Tu puntuación es de: " + puntuacion + " puntos");
            Environment.Exit(0);

        }
        private void pbEscudo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        
    }
}
