using System.Security.Cryptography;
using System.Text;
namespace ApartadoAulas.Recurso
{
    public class Utilidad
    {
        public static string EncriptarClave(string clave)
        {
            StringBuilder sb = new StringBuilder();
            //Crear HASH para encriptar
            using (SHA256 hash = SHA256Managed.Create())
            {
                //codificacion UTF8

                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(clave));
                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();

        }


    }
}
