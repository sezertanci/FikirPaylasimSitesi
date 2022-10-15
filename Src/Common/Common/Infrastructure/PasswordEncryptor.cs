using System.Text;

namespace Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static void Encrypt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));
        }

        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }
    }
}
