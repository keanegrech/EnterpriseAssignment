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

        public static Dictionary<string, Guid> SaveAndRetrieveImageMap(IFormFile zipFile)
        {
            Dictionary<string, Guid> idToGuid = new Dictionary<string, Guid>();
            using (var zipArchive = new ZipArchive(zipFile.OpenReadStream()))
            {
                foreach (var entry in zipArchive.Entries)
                {
                    using (var entryStream = entry.Open())
                    {
                        Guid gen = Guid.NewGuid();

                        idToGuid[entry.Name.Split(".")[0]] = gen;

                        string outputPath = $"wwwroot\\imgs\\{gen}.jpg";

                        using (var fileStream = new FileStream(outputPath, FileMode.Create))
                        {
                            entryStream.CopyTo(fileStream);
                        }
                    }
                }
            }

            return idToGuid;
        }
    }
}
