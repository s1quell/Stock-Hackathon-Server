


# Сервер сервиса автоматизации логистики в торговле на маркетплейсах
[![Build Status](https://raw.githubusercontent.com/s1quell/git-buttons/a9f17ed13d6de3174d39a6009c51ee3c41216e2a/version.svg)](#)

## О проекте

Сервер автоматизации логистики в торговле на маркетплейсах. Проект написан на языке C# с использованием фрейморка ASP.NET Core

## Установка

### Зависимости

Для работы сервера необходимо наличие следующих зависимостей:

- DotNet Core версии 7.0 или выше

- MySQL

  

### Установка

1. Склонируйте репозиторий на свой компьютер:

```

https://github.com/s1quell/Stock-Hackathon-Server.git

```

  

2. Перейдите в каталог проекта:

```

cd project

```

  

3. Запустите сервер:

```

dotnet run

```


  

## Использование

Процесс использования будет описан в документации по API

### Авторизация

Аудентификация пользователей происходит с помощью логина и пароля и сверки в базе данный. Авторизация и предоставление доступа к API происходит через JWT токен.

### База данных

Сервер использует MySQL для хранения данных о товарах, клиентах и поставщиках. 

Помимо основной базы данных, сервер использует Redis для кеширования частых запросов клиентов


### Логирование

Сервер осуществляет логирование событий в файл `server.log`.

    

## Лицензия

Лицензия отсутствует.

  

