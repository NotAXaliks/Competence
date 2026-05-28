using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Desktop.Services;

public class TokenService
{
    public static string Path = "token";

    public static bool Has() => File.Exists(Path);

    public static void Save() => File.WriteAllText(Path, "123");

    public static void Clear() => File.Delete(Path);
}
