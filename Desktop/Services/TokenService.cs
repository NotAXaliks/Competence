using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Desktop.Services;

public class TokenService
{
    public static string Path = "token";

    public static string GetToken()
    {
        if (!File.Exists(Path)) return string.Empty;

        var encrypted = File.ReadAllText(Path);

        return Encoding.UTF8.GetString(Convert.FromBase64String(encrypted));
    }

    public static void SetToken(string token)
    {
        var encrypted = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

        File.WriteAllText(Path, encrypted);
    }

    public static void Clear() => File.Delete(Path);
}
