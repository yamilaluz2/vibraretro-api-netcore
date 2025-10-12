public class Photo : IFile
{
    public (string,string) GetPath(IFormFile archivo, string rute)
    {
        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
        string pathCompleto = Path.Combine(rute, nombreArchivo);
        return (pathCompleto,nombreArchivo);
    }

    public void SaveFile(IFormFile archivo, string ruteDef)
    {

        using var stream = new FileStream(ruteDef, FileMode.Create);
        archivo.CopyTo(stream);
    }
}