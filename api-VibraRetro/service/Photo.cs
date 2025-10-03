public class Photo : IFile
{
    public string Save(IFormFile file, string path)
    {
        string fullPath = getPath(file, path);
        saveFile(file, fullPath);
        return fullPath;
    }

    private string getPath(IFormFile archivo, string rute)
    {
        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
        string pathCompleto = Path.Combine(rute, nombreArchivo);
        return pathCompleto;
    }

    private void saveFile(IFormFile archivo, string ruteDef)
    {
        using var stream = new FileStream(ruteDef, FileMode.Create);
        archivo.CopyTo(stream);
    }
}