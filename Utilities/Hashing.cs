using System;
using System.Security.Cryptography;
using System.Text;

public static class Hashing
{
    //public static string HashUsername(string input)
    //{
    //    using (SHA256 sha = SHA256.Create())
    //    {
    //        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
    //        return Convert.ToBase64String(bytes);
    //    }
    //}

    public static string HashPassword(string password)
    {
        // Use RNG for a secure salt
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // PBKDF2 with HMACSHA256
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        // Combine salt and hash
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        // Return as base64 string
        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100000, HashAlgorithmName.SHA256);
        byte[] enteredHash = pbkdf2.GetBytes(32);

        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 16] != enteredHash[i])
                return false;
        }

        return true;
    }
}
