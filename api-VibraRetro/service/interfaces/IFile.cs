public interface IFile
{
    (string,string) GetPath(IFormFile archivo, string rute);
    void SaveFile(IFormFile archivo, string ruteDef);

}

