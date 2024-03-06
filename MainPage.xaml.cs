using System;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace IPO2_IPOKEMON
{
    public sealed partial class MainPage : Page
    {

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool evolution = false;
        private bool derrotado=false;
        private Storyboard sb_normal;
        private DispatcherTimer dtTime;

        public MainPage()
        {
            this.InitializeComponent();
            this.IsTabStop = true;




            this.sb_normal = (Storyboard)this.Resources["sb_normal"];
            this.sb_normal.RepeatBehavior = RepeatBehavior.Forever;
            this.sb_normal.Begin();


        }

        private void clickNumber(object sender, KeyRoutedEventArgs e)
        {

            //this.ShowAlert("CIPOTE");

            this.sb_normal.Stop();

            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1: // Key 1
                    this.sb_normal.Begin();
                    break;
                case Windows.System.VirtualKey.Number2: // Key 2
                    if (!evolution)
                    {
                        this.PlayAudio("ms-appx:///Assets/audio/evolucion.mp3");
                        this.StartAnimation("sb_mega_evolucion");
                        this.evolution = true;
                    }

                    break;
                case Windows.System.VirtualKey.Number3: // Key 3
                    this.StartAnimation("sb_garra_dragon");
                    this.PlayAudio("ms-appx:///Assets/audio/garra.mp3");
                    break;
                case Windows.System.VirtualKey.Number4: // Key 4
                    this.checkEvolution();
                    this.StartAnimation("sb_lanza_llamas");
                    this.PlayAudio("ms-appx:///Assets/audio/lanzallamas.mp3");
                    break;
                case Windows.System.VirtualKey.Number5: // Key 5
                    this.derrotado = true;
                    this.matar(sender, null);
                    this.StartAnimation("sb_derrotado");
                    break;
                case Windows.System.VirtualKey.Number6: // Key 6
                    this.PlayAudio("ms-appx:///Assets/audio/recuperado.mp3");
                    this.StartAnimation("sb_levantar");
                    derrotado = false;
                    green_potion_PointerReleased(sender, null);
                    break;
                case Windows.System.VirtualKey.Number7: // Key 7
                    this.checkEvolution();
                    this.PlayAudio("ms-appx:///Assets/audio/mewing.mp3");
                    this.StartAnimation("sb_mewing");
                    break;
            }
        }


        private void StartAnimation(string storyboardName)
        {
            Storyboard storyboard = (Storyboard)this.Resources[storyboardName];
            if (storyboard != null)
            {
                storyboard.Begin();
            }
        }


        private void PlayAudio(string audioFilePath)
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer();
            }

            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(audioFilePath));
            mediaPlayer.Play();
        }

        async void ShowAlert(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
        private void checkEvolution()
        {
            if (evolution)
            {
                this.mano_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/mano_mewing_black.png"));
                this.lanzallamas.Source = new BitmapImage(new Uri("ms-appx:///Assets/lanzallamas_azul.png"));
                this.cara_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/mewing_black.png"));
            }
            else
            {
                this.mano_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/mano_mewing.png"));
                this.lanzallamas.Source = new BitmapImage(new Uri("ms-appx:///Assets/lanzallamas.png"));
                this.cara_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/mewing.png"));
            }
        }

        private void green_potion_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (!this.derrotado)
            {
                dtTime = new DispatcherTimer();
                dtTime.Interval = TimeSpan.FromMilliseconds(100);
                dtTime.Tick += increaseHealth;
                dtTime.Start();
                this.green_potion.Opacity = 0.5;
            }
        }

        private void increaseHealth(object sender, object e)
        {
            this.pb_heal.Value += 5;
            if (pb_heal.Value >= 100)
            {
                this.dtTime.Stop();
                this.pb_heal.Opacity = 1;
            }
        }

        private void matar(object sender, PointerRoutedEventArgs e)
        {
            if (this.derrotado)
            {
                dtTime = new DispatcherTimer();
                dtTime.Interval = TimeSpan.FromMilliseconds(100);
                dtTime.Tick += decreaseHealth;
                dtTime.Start();
                this.green_potion.Opacity = 0.5;
            }
        }

        private void decreaseHealth(object sender, object e)
        {
            this.pb_heal.Value -= 5;
            if (pb_heal.Value <= 0)
            {
                this.dtTime.Stop();
                this.pb_heal.Opacity = 1;
            }
        }


    }


}
