using System.IO.Pipelines;
using Teknokent.Interfaces;

namespace Teknokent.Repositories
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = string.Empty;

            try
            {
                if(file==null && file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory,"AllFiles"));
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (var filestream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }


                }

                return true;
            }catch(Exception ex)
            {
                throw new Exception("Dosya yüklenemedi",ex);
            }
        }

       
    }
}
