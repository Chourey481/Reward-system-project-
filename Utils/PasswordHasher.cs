using System.Security.Cryptography;
using System.Text;

namespace RewardSystemAPI.Utils;

public static class PasswordHasher
{
    // Simple SHA256 hash for assignment (not for prod)
    public static string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password ?? "");
        return Convert.ToHexString(sha.ComputeHash(bytes));
    }
    public static bool Verify(string password, string hash) => Hash(password) == hash;
}
