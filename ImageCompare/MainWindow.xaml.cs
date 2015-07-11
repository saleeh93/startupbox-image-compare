using Microsoft.Win32;
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
using XnaFan.ImageComparison;
using System.Drawing;
using System.Threading;
using System.Net;

namespace ImageCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string url1, url2;
        private bool img1ready = false, img2ready = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void windo_mouseleftdown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception err)
            {
                Console.WriteLine("exception : " + err.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void img1_mld(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Open Image 1";
            dlg.Filter = "Images |*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                url1 = dlg.FileName;
                imgImg1.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(url1);
                img1ready = true;
                if (img2ready)
                    printPercentage();
            }
        }

        private void printPercentage()
        {
            //Console.WriteLine("src: {0} ; {1}", imgImg1.Source, imgImg2.Source);
            float percentage = Image_Compare(url1, url2);
            if (percentage > 0)
            {
                txtPercentage.Text = percentage.ToString() + "%";
            }
            else
            {
                txtPercentage.Text = "NOT";
            }
            txtblock1.Visibility = Visibility.Visible;
            txtPercentage.Visibility = Visibility.Visible;
            txtblock3.Visibility = Visibility.Visible;
        }

        private void img1_md(object sender, MouseButtonEventArgs e)
        {
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void img2_drop(object sender, DragEventArgs e)
        {
            
        }

        private void img2_mld(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Open Image 1";
            dlg.Filter = "Images |*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                url2 = dlg.FileName;
                imgImg2.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(url2);
                img2ready = true;
                if (img1ready)
                    printPercentage();
            }
        }

        private void btnUrl1_Click(object sender, RoutedEventArgs e)
        {
            string urlin = txtUrl1.Text;
            if (urlin != "")
            {
                try
                {
                    imgImg1.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(urlin);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(imgImg1.Source.ToString(), "tempFile1");
                        
                    }
                    url1 = "tempFile1";
                    img1ready = true;
                    if (img2ready)
                        printPercentage();
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnUrl2_Click(object sender, RoutedEventArgs e)
        {
            string urlin = txtUrl2.Text;
            if (urlin != "")
            {
                try
                {
                    imgImg2.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(urlin);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(imgImg2.Source.ToString(), "tempFile2");
                    }
                    url2 = "tempFile2";                    
                    img2ready = true;
                    if (img1ready)
                        printPercentage();
                }
                catch (Exception)
                {
                    
                }
            }
        }
        private float Image_Compare(string imgurl1, string imgurl2)
        {
            
            try
            {
                int result = (int)((1 - (ImageTool.GetPercentageDifference(imgurl1, imgurl2))) * 10000);
                float similarity = (float)result / 100;
                return similarity;
                
            }
            catch (Exception err)
            {
                Console.WriteLine("compare error : " + err.Message);
                return 0;
            }
        }
        private string SaveBitmapToFile(Bitmap image, string filename)
        {
            string savePath = string.Concat( "\\", filename);

            image.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);

            return savePath;
        }
    }
}
