using KCMS.AppConfigs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;

namespace KCMS.Ultitlies
{
    public static class FileUlti
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName).Replace(" ", "_");
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public static string UploadFile(IFormFile file, IHostingEnvironment _hostingEnvironment, bool isDeleteExistedFile = false)
        {
            var time = DateTime.UtcNow;
            var folder = Path.Combine(time.Year.ToString(), time.Month.ToString());
            folder = Path.Combine(folder, time.Day.ToString());
            var fileName = FileUlti.GetUniqueFileName(file.FileName);

            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, Path.Combine(Constant.UploadFolder, folder));
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(folder, fileName);
        }

        public static void DeleteFile(string fileName, IHostingEnvironment _hostingEnvironment)
        {
            var NumberOfRetries = 3;
            var DelayOnRetry = 1000;

            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    if (File.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, Path.Combine(Constant.UploadFolder, fileName))))
                    {
                        File.Delete(Path.Combine(_hostingEnvironment.ContentRootPath, Path.Combine(Constant.UploadFolder, fileName)));
                    }
                    break;
                }
                catch (IOException) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
            }
        }
    }
}
