#region Usings

using System.Runtime.CompilerServices;
using Hackathon.Core.Exceptions;
using Hackathon.Domain.Entity;
using Microsoft.EntityFrameworkCore;

#endregion


namespace Hackathon.Core.Database;

public class DatabaseController : DbContext
{
    #region Private Members

    private static DatabaseController? _instance;

    /// <summary>
    /// Адрес удаленного сервера с базой данных
    /// </summary>
    private static string? _host;

    /// <summary>
    /// Логин для авторизации в БД
    /// </summary>
    private static string? _login;
    
    /// <summary>
    /// Пароль для авторизации в базе данных
    /// </summary>
    private static string? _password;
    
    /// <summary>
    /// Название к подключаемой базе данных
    /// </summary>
    private static string? _schema;

    #endregion
    
    #region Singleton

    /// <summary>
    /// Получение инстанции класса управления базой данных через Entity
    ///
    /// Класс реализует паттерн Singleton для исключения возможности создавать еще классы данного формата 
    /// </summary>
    /// <returns>Единичная инстанция класса</returns>
    public static DatabaseController GetInstance()
    {
        if (_instance == null)
            throw new DatabaseException("Не была проведена инициализация класса контроллера базы данных. Используйте метод Init()");
        return _instance;
    }

    /// <summary>
    /// Обязательная инциализация полей для подключения контроллера к базе данных
    /// </summary>
    /// <param name="host">Адрес кластера базы данных</param>
    /// <param name="login">Имя пользователя базы данных</param>
    /// <param name="password">Пароль для авторизации в базе данных</param>
    /// <param name="schema">Название схемы таблиц (базы данных)</param>
    public static void Init(string? host, 
        string? login,
        string? password,
        string? schema)
    {
        if (_instance != null)
            throw new DatabaseException("Конфигурационные данные базы данных уже были инициализированы");

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(login) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(schema)
           )
            throw new NullReferenceException("Конфигурационные данные базы данных пустые или с ошибкой");
        
        _host = host;
        _login = login;
        _password = password;
        _schema = schema;
    }
    

    #endregion

    #region Public Fields

    /// <summary>
    /// Пользователи системы
    /// </summary>
    public DbSet<User>? Users { get; set; }

    #endregion
    
    #region Constructor

    /// <summary>
    /// Контроллер используется для управления состоянием кластера реляционной базы MySQL
    ///
    /// Использует паттерн Singleton 
    /// </summary>
    public DatabaseController() : base()
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Содержит строку подключения к базе данных
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            $"server={_host};user={_login};password={_password};database={_schema};", 
            new MySqlServerVersion(new Version(8, 0, 35))
        );
    }

    #endregion
}