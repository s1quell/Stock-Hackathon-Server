#region Usings

using Hackathon.Core.Models;

#endregion

namespace Hackathon.Core.Exceptions;

public class AuthException : Exception
{
    #region Constructor

    /// <summary>
    /// Исключение, используемое для обозначения ошибок при использовании <see cref="AuthModel"/>
    ///
    /// И все что связано с идентификацией пользователя в системе
    /// </summary>
    /// <param name="message"></param>
    public AuthException(string message)
        : base(message) { }

    #endregion
}