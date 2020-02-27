WebApiTest

Решение состоит из 4 компонентов:

- DLL библиотека
- БД (code first)
- ASP.NET WebAPI сервер
- Клиентское консольное приложение


## ASP.Net WebApiTest

Для написания этого приложения был использован ASP.NET MVC Framework от Microsoft. Для подключения к SQL Server используется Entity Framework. Стандартная строка подключения прописывается  в файле Web.config. Ее вид следующий:

<connectionStrings>
<add name="WebApiContext"
connectionString="Data Source=(LocalDb)\MSSQLLocalDB;
AttachDBFilename=|DataDirectory|\WebApiTest1.mdf 
Initial Catalog=WebApiTest;Integrated Security=SSPI;" providerName="System.Data.SqlClient"/>
</connectionStrings>

Путь для доступа к API : /api/values

Поддерживаются GET, POST, PUT, DELETE, запросы. Ответы и запросы приводятся в формате JSON


## DLL

К данной структуре была добавлена подключаемая библиотека DLL (Data Logic Layer) которая позволяет обрабатывать первичные данные полученные из БД. Для работы с БД через Entity Framework, необходим контекст данных, для взаимодействия с БД (WebApiContext) и набор сущностей DbSet<T>, хранящихся в БД.

## БД

Для создания БД был использован подход CodeFirst. При работе с Code First требуется определения ключа элемента для создания первичного ключа в таблице в БД. По умолчанию при генерации БД Entity Framework в качестве первичных ключей будет рассматривать свойства с именами ID.
БД состоит из двух таблиц Interval и LogInterval
Interval
	ID (PK, int, not null) – первичный ключ
	DateBegin (DateTime, not null) – начальная дата
	EndDate (DateTime, not null) – конечная дата

LogInterval

	ID (PK, int, not null) – первичный ключ
	DateChange (DateTime, not null) – дата/время создания записи
	TypeChange (nvarchar(max), not null) – тип выполненного действия
	ID_Interval (FK, int, null) – внешний ключ (табл Interval)
    DateBegin (DateTime, null) – начальная дата? (данные до/после изменений)
	EndDate (DateTime, null) – конечная дата? (данные до/после изменений)

## Консольное приложение

Консольное приложение позволять ввести данные, вызвать методы и отобразить результат. Навигация по меню реализуется ответом в виде цифры.

1 – запрос на создание экземпляра объекта; POST запрос
2 – запрос на получение всех данных из БД из таблицы Interval; GET запрос
3 – запрос на выполнение фильтрации пересечений периодов; GET запрос
4 – запрос на получение всех данных из БД из таблицы LogInterval; GET запрос
0 – завершение работы приложения.

Адрес по умолчанию: http://localhost:50644/api/values Изменить его можно изменив константу baseUrl в файлеProgram.cs

Пример POST запроса для добавления нового элемента

$ dotnet run
{
    "args": {},
    "data": "{\"ID\":\"0\",\"BeginDate\":\"2020-01-15\",\"EndDate\":\"2020-02-20\"}",
    "files": {},
    "form": {},
    "headers": {
    "Content-Length": "53",
    "Content-Type": "application/json; charset=utf-8",
    "Host": " http://localhost:50644"
    },
    "json": {
    "ID": "0",
    "BeginDate": "2020-01-15",
    "EndDate": "2020-02-20"
    },
    ...
    "url": "http://localhost:50644/api/values"
}

Запросы на добавление и фильтрацию осуществляются после введения пользователе начальной и конечной даты периода в формате “yyyy-mm-dd”
