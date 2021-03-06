﻿namespace Saving_Accelerator_Tools2.IServices.File
{
    public interface IFileService
    {
        T Read<T>(string folderPath, string fileName);
        string[] Read(string path);

        void Save<T>(string folderPath, string fileName, T content);

        void Delete(string folderPath, string fileName);
    }
}
