﻿namespace Teknokent.Interfaces
{
    public interface IFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
