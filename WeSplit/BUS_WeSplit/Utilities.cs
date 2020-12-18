using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BUS_WeSplit
{
    public class Utilities
    {

        /// <summary>
        /// using System.IO;
        /// Copy a file "srcFile" to destDir and change name to "filename"
        /// </summary>
        /// <param name="srcFile">Path of source file (Ex: "C:\User\Public\TestFolder\wjbu.jpg")</param>
        /// <param name="destDir">Directory of destination file (Ex: "C:\User\Public\TestFolder\DestFolder\")</param>
        public static string CopyFile(string srcFile, string destDir)
        {
            // To copy a folder's contents to a new location:
            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
            Directory.CreateDirectory(destDir);


            // Use Path class to manipulate file and directory paths.
            //string sourceFile = System.IO.Path.Combine(srcFile, fileName);
            string fileName = System.IO.Path.GetFileName(srcFile);
            string destinateFile = System.IO.Path.Combine(destDir, fileName);

            // To copy a file to another location and
            // overwrite the destination file if it already exists.
            File.Copy(srcFile, destinateFile, true);

            return destinateFile;
        }

        public static List<BitmapImage> FilePathListToBitmapImageList(List<string> imagePath)
        {
            List<BitmapImage> bitmapImages = new List<BitmapImage>();

            if (imagePath != null)
            {
                foreach (string uri in imagePath)
                {
                    BitmapImage img = new BitmapImage(new Uri(uri, UriKind.Absolute));
                    bitmapImages.Add(img);
                }
                return bitmapImages;
            }
            return null;
        }
    }
}
