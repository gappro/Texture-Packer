using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using System.Windows.Threading;

namespace TexturePackerApp.ViewModels
{

    public class PackerViewModel : ViewModelBase
    {
        #region CommandsVars
        public ICommand selectfilesCommand { get; set; }
        public ICommand SelectFilesCommand {get{ return selectfilesCommand;} set { selectfilesCommand = value;}}

        public ICommand k1 { get; set; }
        public ICommand K1 { get { return k1; } set { k1 = value; } }

        public ICommand k2 { get; set; }
        public ICommand K2 { get { return k2; } set { k2 = value; } }

        public ICommand k3 { get; set; }
        public ICommand K3 { get { return k3; } set { k3 = value; } }

        public ICommand k4 { get; set; }
        public ICommand K4 { get { return k4; } set { k4 = value; } }
        #endregion


        #region DefaultValues
        public int AtlasResolution = 1024;
        public int HorizontalLimit = 2;
        public string Filenam = "Output"; 
        public System.Windows.Media.ImageSource ImageDisplay;
        #endregion

        #region RuntimeValues
        Dictionary<string, List<Bitmap>> MotherLister = new Dictionary<string, List<Bitmap>>();

        int TexturePixelCount = 819;
        int atlasSizeInTextures = 5;
        int atlases = 0;

        public bool Overriding = false;

        List<Bitmap> sortedTextures = new List<Bitmap>();
        Bitmap atlas;

        List<ImageSource> DisplayList;


        #endregion

        // Initialize RelayCommand of Bindings
        public PackerViewModel()
        {
            SelectFilesCommand = new RelayCommand(new Action<object>(SelectButton));
            K1 = new RelayCommand(new Action<object>(Bk1));
            K2 = new RelayCommand(new Action<object>(Bk2));
            K3 = new RelayCommand(new Action<object>(Bk3));
            K4 = new RelayCommand(new Action<object>(Bk4));
        }


        #region HandleView
        public ImageSource Display
        {
            get
            {
                return ImageDisplay;
            }
            set
            {
                ImageDisplay = value;
                OnPropertyChanged(nameof(ImageDisplay));
            }
        }

        public string AtResolution
        {
            get
            {
                return AtlasResolution.ToString();
            }
            set
            {
                if (int.TryParse(value, out int result))
                {
                    AtlasResolution = result;
                    OnPropertyChanged(nameof(AtResolution));
                }
                else
                {
                    MessageBox.Show("Enter a valid Resolution");
                }



            }
        }

        public bool Override
        {
            get
            {
                return Overriding;
            }
            set
            {
                Overriding = value;
                OnPropertyChanged(nameof(Overriding));

            }
        }

        public string File
        {
            get
            {
                return Filenam.ToString();
            }
            set
            {
                Filenam = value;
                OnPropertyChanged(nameof(File));

            }
        }

        public string RowLimit
        {
            get
            {
                return HorizontalLimit.ToString();
            }
            set
            {
                if (int.TryParse(value, out int result))
                {
                    HorizontalLimit = result;
                    OnPropertyChanged(nameof(RowLimit));
                }
                else
                {
                    MessageBox.Show("Enter a valid Horizontal Limit");
                }

            }
        }
        #endregion

        #region CommandMethods
        public void SelectButton(object obj)
        {
            // Run Method on Worker Thread
            Task.Run(() => ProcessFiles());
        }

        public void Bk1(object obj)
        {
            AtlasResolution = 1024;
            OnPropertyChanged(nameof(AtResolution));
        }

        public void Bk2(object obj)
        {
            AtlasResolution = 2048;
            OnPropertyChanged(nameof(AtResolution));
        }

        public void Bk3(object obj)
        {
            AtlasResolution = 3072;
            OnPropertyChanged(nameof(AtResolution));
        }

        public void Bk4(object obj)
        {
            AtlasResolution = 4069;
            OnPropertyChanged(nameof(AtResolution));
        }

        #endregion

        // Bitmap>BitmapImage Conversion
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }


        private void ProcessFiles()
        {
            var TempList  = new List<ImageSource>();

            // Check if AtlasResolution is factor of 2
            while (AtlasResolution % 2 == 1) {
                AtlasResolution += 1;
                OnPropertyChanged(nameof(AtResolution));
            }


            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "";
            ofd.Multiselect = true;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            // Get all image extensions
            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, "All Files", "*.*");

            ofd.DefaultExt = ".png"; // Default file extension 

            bool? response = ofd.ShowDialog();
            if (response == true)
            {
                //ImageDisplay = null;
                OnPropertyChanged(nameof(Display));
                if (atlas != null) { atlas.Dispose(); }
                if (sortedTextures != null)
                {
                    foreach (Bitmap Text in sortedTextures)
                    {
                        Text.Dispose();
                    }
                    sortedTextures.Clear();
                }

                double temp = (ofd.FileNames.Length / 2.0);
                temp = Math.Round(temp, MidpointRounding.AwayFromZero);
                temp = Math.Round((AtlasResolution / temp), MidpointRounding.AwayFromZero);
                while (temp % 2 == 1)
                    temp -= 1;

                atlasSizeInTextures = ((int)(AtlasResolution / ((float)temp)));

                if (atlasSizeInTextures >= HorizontalLimit){atlasSizeInTextures = HorizontalLimit; }

                TexturePixelCount = AtlasResolution / atlasSizeInTextures;
                DisplayList=Pack(ofd, Filenam, TempList);
            }
        }


        private List<ImageSource> Pack(Microsoft.Win32.OpenFileDialog ofd, string FileName, List<ImageSource> TempList)
        {
            if (!Directory.Exists(@"textures")){ Directory.CreateDirectory(@"textures"); }
            if (Override){if (!Directory.Exists(@"textures" + "\\" + FileName)){Directory.CreateDirectory(@"textures" + "\\" + FileName);} }


            string[] path = ofd.FileNames;



            // Loop through each one, resize single img and add to img array
            foreach (string element in path)
            {
                var destRect = new System.Drawing.Rectangle(0, 0, TexturePixelCount, TexturePixelCount);
                var destImage = new Bitmap(TexturePixelCount, TexturePixelCount);


                System.Drawing.Image img = System.Drawing.Image.FromFile(element);
                if (img.Width == TexturePixelCount && img.Height == TexturePixelCount)
                    sortedTextures.Add((Bitmap)img);
                else
                    // Resize
                    destImage.SetResolution(TexturePixelCount * atlasSizeInTextures, TexturePixelCount * atlasSizeInTextures);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.Clamp);
                        graphics.DrawImage(img, 0, 0, destImage.Width, destImage.Height);
                    }
                }
                sortedTextures.Add((Bitmap)destImage);

                // Tap into UI Thread and Update ImageBrush
                Application.Current.Dispatcher.Invoke(() => { ImageDisplay = BitmapToImageSource((Bitmap)destImage); });
                
                TempList.Add(BitmapToImageSource((Bitmap)destImage));
                OnPropertyChanged(nameof(Display));

            }

            if (Overriding)
            {
                MotherLister.Clear();
                int imagesinatlas = atlasSizeInTextures * atlasSizeInTextures;

                if (sortedTextures.Count > atlasSizeInTextures)
                {
                    atlases = Convert.ToInt32(Math.Ceiling(Convert.ToDouble((sortedTextures.Count / imagesinatlas))));

                    for (int i = 0; i < (int)(atlases + 1); i++)
                    {
                        if (i >= atlases) {  break; }
                        List<Bitmap> templist = new List<Bitmap>();
                        MotherLister.Add("Lister" + i, new List<Bitmap>());
                        for (int j = imagesinatlas * i; j < (imagesinatlas + imagesinatlas * i); j++)
                        {
                            if (j == sortedTextures.Count) { break; }
                            templist.Add(sortedTextures[j]);
                        }
                        MotherLister["Lister" + i] = templist;

                        // Create Image Bundle after resizing and adding all textures to a Dictionary of lists
                        CreateImageBundle(MotherLister["Lister" + i], path, FileName, i);
                        if (i + 1 > atlases){ break; }

                        // Tap into UI Thread and Update ImageBrush
                        //Application.Current.Dispatcher.Invoke(() =>
                        //{
                        //ImageDisplay = null;
                        //});

                        OnPropertyChanged(nameof(Display));

                    }
                }
            }
            if (!Overriding)
            {
                if (!(sortedTextures.Count > (atlasSizeInTextures * atlasSizeInTextures)))
                {
                    // Create Image Bundle after resizing and adding all textures to a list
                    CreateImageBundle(sortedTextures, path, FileName, 0);
                }
                else
                {
                    MessageBox.Show("Horizontal Limit small for texture count, try a higher one. Minimum limit => " + Math.Ceiling(Math.Sqrt(sortedTextures.Count)));
                    return null;
                }

            }
            MessageBox.Show("Finished Successfully");
            return DisplayList;
        }

        private void CreateImageBundle(List<Bitmap> img, string[] imglist, string Filename, int indexvalue)
        {
            Bitmap bitmap = new Bitmap(AtlasResolution, AtlasResolution, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            // Create and initialize Graphics
            Graphics graphics = Graphics.FromImage(bitmap);

            if (!Overriding)
            {
                System.IO.File.Delete("textures" + "\\"+ Filename + @".png");
                bitmap.Save("textures" + "\\" + Filename + @".png");
                atlas = (Bitmap)System.Drawing.Image.FromFile("textures" + "\\" + Filename + @".png");
            }
            
            if (Overriding)
            {
                bitmap.Save("textures" + "\\" + Filename + "\\" + Filename + indexvalue + @".png");
                atlas = (Bitmap)System.Drawing.Image.FromFile("textures" + "\\" + Filename + "\\" + Filename + indexvalue + @".png");
            }
            
            System.Drawing.Color[] pixels = new System.Drawing.Color[AtlasResolution * AtlasResolution];

            // for loop while x is less than AtlasResolution => x++
            for (int x = 0; x < AtlasResolution; x++)
            {
                // for loop while y is less than AtlasResolution => y++
                for (int y = 0; y < AtlasResolution; y++)
                {
                    // Get the current block that we're looking at.
                    int currentBlockX = x / TexturePixelCount;
                    int currentBlockY = y / TexturePixelCount;

                    int index = currentBlockY * atlasSizeInTextures + currentBlockX;

                    // Get the pixel in the current block.
                    Int32 currentPixelX = x - (currentBlockX * TexturePixelCount);
                    Int32 currentPixelY = y - (currentBlockY * TexturePixelCount);

                    if (index < img.Count) { pixels[(AtlasResolution - y - 1) * AtlasResolution + x] = img[index].GetPixel(currentPixelX, currentPixelY);}
                    else{pixels[(AtlasResolution - y - 1) * AtlasResolution + x] = System.Drawing.Color.FromArgb(0, 0, 0, 0);}
                    atlas.SetPixel(x, y, pixels[((AtlasResolution - y - 1) * AtlasResolution + x)]);

                }
            }

            // Tap into UI Thread and Update ImageBrush
            Application.Current.Dispatcher.Invoke(() =>
            {
                ImageDisplay = BitmapToImageSource(atlas);
            });
            
            OnPropertyChanged(nameof(Display));
            Bitmap temp = new Bitmap(atlas);
            atlas.Dispose();
            if (Override) { temp.Save("textures" + "\\" + Filename + "\\" + Filename + indexvalue + @".png", ImageFormat.Png); }
            if (!Override) {temp.Save("textures" + "\\"+ Filename + @".png", ImageFormat.Png);}          
        }

    }
}