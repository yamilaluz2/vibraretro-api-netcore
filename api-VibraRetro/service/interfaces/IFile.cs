public interface IFile
{
    string GetPath(IFormFile archivo, string rute);
    void SaveFile(IFormFile archivo, string ruteDef);

}

