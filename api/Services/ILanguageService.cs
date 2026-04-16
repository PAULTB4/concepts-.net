namespace api.Services
{
    public interface ILanguageService
    {
        string[] ObtenerLenguajes();
        bool ExisteIdioma(string idioma);
    }
}