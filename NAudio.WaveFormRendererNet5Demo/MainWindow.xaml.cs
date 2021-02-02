using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using WaveFormRendererNet5;

namespace NAudio.WaveFormRendererNet5Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); waveFormRenderer = new WaveFormRenderer();

            standardSettings = new StandardWaveFormRendererSettings() { Name = "Standard" };
            var soundcloudOriginalSettings = new SoundCloudOriginalSettings() { Name = "SoundCloud Original" };

            var soundCloudLightBlocks = new SoundCloudBlockWaveFormSettings(System.Drawing.Color.FromArgb(102, 102, 102), System.Drawing.Color.FromArgb(103, 103, 103), System.Drawing.Color.FromArgb(179, 179, 179),
                System.Drawing.Color.FromArgb(218, 218, 218))
            { Name = "SoundCloud Light Blocks" };

            var soundCloudDarkBlocks = new SoundCloudBlockWaveFormSettings(System.Drawing.Color.FromArgb(52, 52, 52), System.Drawing.Color.FromArgb(55, 55, 55), System.Drawing.Color.FromArgb(154, 154, 154),
                System.Drawing.Color.FromArgb(204, 204, 204))
            { Name = "SoundCloud Darker Blocks" };

            var soundCloudOrangeBlocks = new SoundCloudBlockWaveFormSettings(System.Drawing.Color.FromArgb(255, 76, 0), System.Drawing.Color.FromArgb(255, 52, 2), System.Drawing.Color.FromArgb(255, 171, 141),
                System.Drawing.Color.FromArgb(255, 213, 199))
            { Name = "SoundCloud Orange Blocks" };

            var topSpacerColor = System.Drawing.Color.FromArgb(64, 83, 22, 3);
            var soundCloudOrangeTransparentBlocks = new SoundCloudBlockWaveFormSettings(System.Drawing.Color.FromArgb(196, 197, 53, 0), topSpacerColor, System.Drawing.Color.FromArgb(196, 79, 26, 0),
                System.Drawing.Color.FromArgb(64, 79, 79, 79))
            {
                Name = "SoundCloud Orange Transparent Blocks",
                PixelsPerPeak = 2,
                SpacerPixels = 1,
                TopSpacerGradientStartColor = topSpacerColor,
                BackgroundColor = System.Drawing.Color.Transparent
            };

            var topSpacerColor2 = System.Drawing.Color.FromArgb(64, 224, 224, 224);
            var soundCloudGrayTransparentBlocks = new SoundCloudBlockWaveFormSettings(System.Drawing.Color.FromArgb(196, 224, 225, 224), topSpacerColor2, System.Drawing.Color.FromArgb(196, 128, 128, 128),
                System.Drawing.Color.FromArgb(64, 128, 128, 128))
            {
                Name = "SoundCloud Gray Transparent Blocks",
                PixelsPerPeak = 2,
                SpacerPixels = 1,
                TopSpacerGradientStartColor = topSpacerColor2,
                BackgroundColor = System.Drawing.Color.Transparent
            };


       
        }

        private string selectedFile;
        private string imageFile;
        private readonly WaveFormRenderer waveFormRenderer;
        private readonly WaveFormRendererSettings standardSettings;

       
        
        

        private IPeakProvider GetPeakProvider()
        {

            return null;
        }

        private WaveFormRendererSettings GetRendererSettings()
        {
       
            return null;
        }

        private void RenderWaveform()
        {
            if (selectedFile == null) return;
            var settings = GetRendererSettings();
            if (imageFile != null)
            {
                settings.BackgroundImage = new Bitmap(imageFile);
            }
          
            var peakProvider = GetPeakProvider();
            Task.Factory.StartNew(() => RenderThreadFunc(peakProvider, settings));
        }

        private void RenderThreadFunc(IPeakProvider peakProvider, WaveFormRendererSettings settings)
        {
            System.Drawing.Image image = null;
            try
            {
                image = waveFormRenderer.Render(selectedFile, peakProvider, settings);
                hello.Source = helperext.ToWpfImage(image);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    
        private void FinishedRender(System.Drawing.Image image)
        {
       
        }

        private void OnLoadSoundFileClick(object sender, EventArgs e)
        {
    
        }

        private void OnButtonSaveClick(object sender, EventArgs e)
        {
            
        }

        private void OnRefreshImageClick(object sender, EventArgs e)
        {
            RenderWaveform();
        }

        private void OnColorButtonClick(object sender, EventArgs e)
        {
         
        }

        private void OnDecibelsCheckedChanged(object sender, EventArgs e)
        {
            RenderWaveform();
        }

        private void OnButtonLoadImageClick(object sender, EventArgs e)
        {
      
        }


        

    }

    public static class helperext
    {
        public static BitmapImage ToWpfImage(this System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }

    }
}
