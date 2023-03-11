using System.Security.Cryptography;
using System.Text;

namespace Hairdressers.Helpers {
    public class HelperCryptography {

        public static string Salt { get; set; }

        public static string GenerateSalt() {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++) {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static byte[] EncryptContent(string password, string salt) {
            string contenido = password + salt;
            SHA512 sHA = SHA512.Create();
            byte[] salida = Encoding.UTF8.GetBytes(contenido);

            for (int i = 1; i <= 31; i++) {
                salida = sHA.ComputeHash(salida);
            }
            sHA.Clear();
            return salida;
        }

        public static bool CompareArrays(byte[] a, byte[] b) {
            bool iguales = true;
            if (a.Length != b.Length) {
                iguales = false;
            } else {
                for (int i = 0; i < a.Length; i++) {
                    if (a[i].Equals(b[i]) == false) {
                        iguales = false;
                        break;
                    }
                }
            }
            return iguales;
        }

    }
}
