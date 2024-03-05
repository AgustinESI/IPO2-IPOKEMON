using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;
using Windows.Media.Playback;
using Windows.UI.Xaml.Media.Animation;
using Windows.Media.Core;
using Windows.UI.Popups;
using System.ServiceModel.Channels;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace IPO2_IPOKEMON
{
    public sealed partial class MainPage : Page
    {

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private Storyboard sb_normal;

        public MainPage()
        {
            this.InitializeComponent();
            this.IsTabStop = true;


            //this.PlayAudio("ms-appx:///Assets/audio/mewing.mp3");
            //this.StartAnimation("sb_mewing");

            this.sb_normal = (Storyboard)this.Resources["sb_normal"];
            this.sb_normal.RepeatBehavior = RepeatBehavior.Forever;
            this.sb_normal.Begin();


        }

        private void clickNumber(object sender, KeyRoutedEventArgs e)
        {

            this.ShowAlert("CIPOTE");

            this.sb_normal.Stop();

            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1: // Key 1
                    //this.sb_normal.Begin();
                    break;
                case Windows.System.VirtualKey.Number2: // Key 2
                    this.StartAnimation("sb_mega_evolution");
                    break;
                case Windows.System.VirtualKey.Number3: // Key 3
                    this.StartAnimation("sb_garra_dragon");
                    break;
                case Windows.System.VirtualKey.Number4: // Key 4
                    this.StartAnimation("sb_lanza_llamas");
                    break;
                case Windows.System.VirtualKey.Number5: // Key 5
                    this.StartAnimation("sb_derrotado");
                    break;
                case Windows.System.VirtualKey.Number6: // Key 6
                    this.StartAnimation("sb_recuperado");
                    break;
                case Windows.System.VirtualKey.Number7: // Key 7
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

    }

   
}
