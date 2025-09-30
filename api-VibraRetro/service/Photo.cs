public class Photo : Ifile
{
    public string GetPath(IFormFile archivo, string rute)
    {
        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
        string pathCompleto = Path.Combine(rute, nombreArchivo);
        return pathCompleto;
    }

    public void SaveFile(IFormFile archivo, string ruteDef)
    {

        using var stream = new FileStream(ruteDef, FileMode.Create);
        archivo.CopyTo(stream);
    }
}