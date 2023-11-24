#region Usings

using System.Security.Cryptography;
using System.Text;

#endregion

namespace Hackathon.Core.Helpers;

/// <summary>
/// Специализированный класс для преобразования текста в его хэшированный вариант
/// </summary>
public static class Sha256Helper
{
    #region Public Methods

    /// <summary>
    /// Конвертирование обычного текста в хэшированный формат 
    /// </summary>
    /// <param name="data">Исходный текст</param>
    /// <returns>Захэшированный вариант исходного текста</returns>
    public static string Convert(string data)
    {
        using var sha256 = SHA256.Create();
        
        // Compute the hash of the given string
        var hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
 
        // Convert the byte array to string format
        return BitConverter.ToString(hashValue).Replace("-", "");
    }

    #endregion
    
}