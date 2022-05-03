using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace SChallengeAPI.Services;

class PasswordHasher : IDisposable
{
    private readonly RandomNumberGenerator rng;

    public PasswordHasher()
    {
        rng = RandomNumberGenerator.Create();
    }

    public static bool IsEquals(string password, string hash, string salt)
    {
        var hashedPassword = Hash(password, Convert.FromBase64String(salt));
        return hashedPassword.Equals(hash);
    }

    public (string hash, string salt) Hash(string password)
    {
        byte[] saltBytes = new byte[128 / 8];
        rng.GetBytes(saltBytes);

        string hash = Hash(password, saltBytes);
        string salt = Convert.ToBase64String(saltBytes);

        return (hash, salt);
    }

    public static string Hash(string password, byte[] salt)
        => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));


    public void Dispose() => rng?.Dispose();
}
