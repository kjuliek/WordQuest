using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    // Configuration
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000; // Nombre d'itérations pour le hash

    public static string HashPassword(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
        {
            var salt = algorithm.Salt;
            var key = algorithm.GetBytes(KeySize);
            var hash = new byte[SaltSize + KeySize];
            
            // Copy salt and key into hash array
            Array.Copy(salt, 0, hash, 0, SaltSize);
            Array.Copy(key, 0, hash, SaltSize, KeySize);

            // Convert to Base64 for storage
            return Convert.ToBase64String(hash);
        }
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Decode the base64 encoded hash
        var hashBytes = Convert.FromBase64String(hashedPassword);

        // Extract salt and key from the hash bytes
        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        var storedKey = new byte[KeySize];
        Array.Copy(hashBytes, SaltSize, storedKey, 0, KeySize);

        // Hash the provided password with the same salt
        using (var algorithm = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            var key = algorithm.GetBytes(KeySize);
            // Compare the computed key with the stored key
            return key.SequenceEqual(storedKey);
        }
    }
}