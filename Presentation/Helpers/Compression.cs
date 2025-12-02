using System.IO.Compression;

namespace Presentation.Helpers
{
    public class Compression
    {
        public static void MakeZipFile(List<string> ids, string outputZipPath)
        {
            const string DefaultImagePath = "wwwroot/imgs/default.png";

            using (var fileStream = new FileStream(outputZipPath, FileMode.Create))
            using (var zip = new ZipArchive(fileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                foreach (var id in ids)
                {
                    string imagePath = $"wwwroot/images/{id}.jpg";
                    string fileToAdd = DefaultImagePath;
                    string entryName = $"{id}.jpg";

                    zip.CreateEntryFromFile(fileToAdd, entryName);
                }
            }
        }
    }
}
