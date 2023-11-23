#region Usings

using Hackathon.Core.Database;

#endregion


namespace Hackathon.Core.Exceptions;

public class DatabaseException : Exception
{
    #region Constructor

    /// <summary>
    /// Исключение, используемое для обозначения ошибок при использовании <see cref="DatabaseController"/>
    /// </summary>
    /// <param name="message"></param>
    public DatabaseException(string message)
        : base(message) { }

    #endregion
}