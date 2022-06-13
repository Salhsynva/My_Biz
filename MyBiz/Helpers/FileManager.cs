using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Helpers
{
    public static class FileManager
    {
        public static string Save(string root, string folder, IFormFile formFile)
        {
            string newFileImage = Guid.NewGuid().ToString() + (formFile.FileName.Length>64?formFile.FileName.Substring(formFile.FileName.Length-64,64):formFile.FileName);
            string path = Path.Combine(root, folder, newFileImage);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return newFileImage;
        }

        public static void Delete(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
