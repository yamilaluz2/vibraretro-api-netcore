public interface Ifile
{
    string GetPath(IFormFile archivo, string rute);
    void SaveFile(IFormFile archivo, string ruteDef);

}

