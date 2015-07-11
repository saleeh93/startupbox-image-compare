using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using XnaFan.ImageComparison;

namespace ImageCompareConsole
{
    class Program
    {
        private static string url1, url2;
        private static bool img1ready = false, img2ready = false;
        private static string match;
        static void Main(string[] args)
        {
            try
            {
                url1 = args[0];
                url2 = args[1];
                if (url1 != ""&&url2!="")
                {
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(url1, "tempFile1");
                            url1 = "tempFile1";
                            client.DownloadFile(url2, "tempFile2");
                            url2 = "tempFile2";
                            float percentage = Image_Compare(url1, url2);
                            if (percentage > 0)
                            {
                                match = percentage.ToString() + "%";
                            }
                            else
                            {
                                match = "NOT";
                            }
                            Console.WriteLine("\n  The first image is {0} similar to the second image. \n", match);
                            Console.ReadLine();
                        }                   
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("ERROR : "+err.Message);
                    }

                }

            }
            catch (Exception)
            {
                Console.WriteLine("ERROR :invalid arguments");
            }
        }
        private static float Image_Compare(string imgurl1, string imgurl2)
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
    }
}
