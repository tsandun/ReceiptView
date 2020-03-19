using System.IO;
using System.Threading.Tasks;
using pdfjs.Droid.PlatformSpecifics;
using pdfjs.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileProvider))]
namespace pdfjs.Droid.PlatformSpecifics
{
    public class LocalFileProvider : ILocalFileProvider
    {
        private readonly string _rootDir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "z");

        public async Task<string> SaveFileToDisk(Stream pdfStream, string fileName)
        {
            if (!Directory.Exists(_rootDir))
                Directory.CreateDirectory(_rootDir);

            var filePath = Path.Combine(_rootDir, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await pdfStream.CopyToAsync(memoryStream);

                //int read;
                //while ((read = memoryStream.Read(pdfStream, 0, pdfStream.Length)) > 0)
                //{
                //    memoryStream.Write(pdfStream, 0, read);
                //}

                File.WriteAllBytes(filePath, memoryStream.ToArray());
            }

            return filePath;
        }
    }
}