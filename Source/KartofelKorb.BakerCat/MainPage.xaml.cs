using Microsoft.Phone.Controls;
using System;
using System.IO;
using System.Windows;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using KartofelKorb.BakerCat.ViewModel;

namespace KartofelKorb.BakerCat
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


        private bool BackgroundMusicPlaying
        {
            get
            {
                return Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl == false;
            }
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFilesToIsoStore();
            Start();
        }

        private void Start()
        {
            webBrowser.Navigate(new Uri("Assets/bakercat.html", UriKind.Relative));
            
            if (BackgroundMusicPlaying == false)
            {
                music.Source = new Uri("/Assets/bakercat.mp3", UriKind.Relative);
            }

            ViewModelLocator.MainStatic.Start();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Start();
        }

        private void OnMediaEnded(object sender, RoutedEventArgs e)
        {
            if (BackgroundMusicPlaying == false)
            {
                music.Position = TimeSpan.Zero;
                music.Play();
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (BackgroundMusicPlaying == false)
            {
                music.Stop();
            }
            ViewModelLocator.MainStatic.Stop();
        }

        private void SaveFilesToIsoStore()
        {
            //These files must match what is included in the application package,
            //or BinaryStream.Dispose below will throw an exception.
            string[] files = 
            {
                "Assets/bakercat.html",
                "Assets/bakercat.gif"
            };

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            if (false == isoStore.FileExists(files[0]) || false == isoStore.FileExists(files[1]))
            {
                foreach (string f in files)
                {
                    StreamResourceInfo sr = Application.GetResourceStream(new Uri(f, UriKind.Relative));
                    using (BinaryReader br = new BinaryReader(sr.Stream))
                    {
                        byte[] data = br.ReadBytes((int)sr.Stream.Length);
                        SaveToIsoStore(f, data);
                    }
                }
            }
        }

        private void SaveToIsoStore(string fileName, byte[] data)
        {
            string strBaseDir = string.Empty;
            string delimStr = "/";
            char[] delimiter = delimStr.ToCharArray();
            string[] dirsPath = fileName.Split(delimiter);

            //Get the IsoStore.
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            //Re-create the directory structure.
            for (int i = 0; i < dirsPath.Length - 1; i++)
            {
                strBaseDir = System.IO.Path.Combine(strBaseDir, dirsPath[i]);
                isoStore.CreateDirectory(strBaseDir);
            }

            //Remove the existing file.
            if (isoStore.FileExists(fileName))
            {
                isoStore.DeleteFile(fileName);
            }

            //Write the file.
            using (BinaryWriter bw = new BinaryWriter(isoStore.CreateFile(fileName)))
            {
                bw.Write(data);
                bw.Close();
            }
        }

        private void OnAboutClicked(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
    }
}
