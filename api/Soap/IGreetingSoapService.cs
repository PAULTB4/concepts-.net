using CoreWCF;
using System.Runtime.Serialization;

namespace api.Soap
{
    [ServiceContract]
    public interface IGreetingSoapService
    {
        [OperationContract]
        string[] ObtenerLenguajes();

        [OperationContract]
        SaludoResponse ObtenerSaludo(string idioma, string nombre);
    }

    [DataContract]
    public class SaludoResponse
    {
        [DataMember]
        public string Idioma { get; set; } = string.Empty;

        [DataMember]
        public string Nombre { get; set; } = string.Empty;

        [DataMember]
        public string Mensaje { get; set; } = string.Empty;
    }
}