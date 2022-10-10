using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Libs
{
    public class FileManager
    {
        public static bool Delete(string FileName, string folder)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + folder, FileName);
            var pathThumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + folder + "/thumbs", FileName);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            if (File.Exists(pathThumb))
            {
                File.Delete(pathThumb);
                return true;
            }
            return false;
        }

        public static void FileSave(string filename, IFormFile file, string Folder)
        {
            var stream = file.OpenReadStream();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/" + Folder, filename.Replace(' ', '-'));

            FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(filestream);
        }

        public static string IFormSave(IFormFile file, string folder, string filename = null)
        {
            var list = file.FileName.Split('.');
            if (filename != null)
                filename = filename + "." + list[list.Count() - 1];
            else
                filename = Guid.NewGuid().ToString() + "." + list[list.Count() - 1];



            var stream = file.OpenReadStream();
            var exsitpath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/uploads/" + folder);
            if (!Directory.Exists(exsitpath))
                Directory.CreateDirectory(exsitpath);
            var path = Path.Combine(exsitpath, filename);

            FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(filestream);


            filestream.Close();
            return path;
        }
        public static string IFormSaveLocal(IFormFile file, string folder, string filename = null)
        {
            var list = file.FileName.Split('.');

            if (filename != null)
                filename = filename + "." + list[list.Count() - 1];
            else
                filename = Guid.NewGuid().ToString() + "." + list[list.Count() - 1];


            var stream = file.OpenReadStream();
            var exsitpath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/uploads/" + folder);
            if (!Directory.Exists(exsitpath))
                Directory.CreateDirectory(exsitpath);

            var path = Path.Combine(exsitpath, filename);

            FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(filestream);


            filestream.Close();
            return folder + "/" + filename;
        }
    }
}
