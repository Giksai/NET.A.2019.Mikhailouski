# Northwind Data Services

#### Выполнение

1. Используя диаграмму, выясните, какие данные хранят в себе сущности модели.
2. Выясните кардинальность для связей между таблицами PK Table (таблица, которая содержит первичный ключ сущности) и FK Table (таблица, содержащая внешний ключ сущности). Заполните колонки Cardinality в таблице:

| PK Table      | Cardinality PK Table | FK Table             | Cardinality FK Table | Relationship |
| ------------- | -------------------- | -------------------- | -------------------- | ------------ |
| shippers      | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| employees     | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| employees     | Zero-or-One          | employees            |  One-or-Many         | One-to-Many  |
| employees     | -                    | territories          | -                    | Many-to-Many |
| customers     | Zero-or-One          | orders               |  One-or-Many         | One-to-Many  |
| customers     | -                    | customerdemographics | -                    | Many-to-Many |
| orders        | One-and-only-One     | orderdetails         |  One-or-Many         | One-to-Many  |
| products      | One-and-only-One     | orderdetails         |  One-or-Many         | One-to-Many  |
| suppliers     | Zero-or-One          | products             |  One-or-Many         | One-to-Many  |
| categories    | Zero-or-One          | products             |  One-or-Many         | One-to-Many  |
| region        | One-and-only-One     | territories          |  One-or-Many         | One-to-Many  |


3. Создайте в Postman новую коллекцию с именем Northwind, в этой коллекции создайте такие запросы к [Northwind OData Service](https://services.odata.org/V2/Northwind/Northwind.svc/), которые будут удовлетворять описанию из таблицы ниже. После проверки запроса, занесите необходимые параметры в таблицу:

| Query Description                                                 | HTTP Verb | Url                        |
| ----------------------------------------------------------------- | --------- | -------------------------- |
| Get service metadata.                                             | GET       | /$metadata                 |
| Get all customers.                                                | GET       | /Customers                 |
| Get a customer with "ALFKI" id.                                   | GET       | /Customers('ALFKI')        |
| Get all orders.                                                   | GET       | /Orders                    |
| Get an order with "10248" id.                                     | GET       | /Orders('10248')           |
| Get all orders for a customer with "ANATR" id.                    | GET       | /Customers('ANATR')/Orders |
| Get a customer for an order with "10248" id.                      | GET       | /Orders(10248)/Customer    |
| Get products with the price bigger than 10000 and smaller than 30000| GET     | /Products?$filter = UnitPrice lt 30.000 and UnitPrice gt 10.000                           |
| Get a country with an eployee id equals to 1.	                    | GET       | /Employees(1)/Country      |
| Get all customers from London.            		       	    | GET       | /Customers?$filter=City eq 'London' |
| Get orders with shipped year 2000	                            | GET       | /Orders?$filter = year(ShippedDate) eq 1998 |
| Get product name where its quantity more than 5                   | GET       | /Products?$select=ProductName&filter=Order_Details?filter =Order/Quantity gh 5 |

Создайте самостоятельно еще минимум 5 сложных запросов и запишите их в таблицу.


8. 
    *Найдите базовый класс, от которого унаследован _NorthwindModel.NorthwindEntities_ :  DataServiceContext
    * В какой сборке находится базовый класс?    :   System.Data.Services.Client
    * По какому пути лежит эта сборка?    :     В NuGet пакете
    * Какая версия у сборки, в которой находится базовый класс?   :  5.6.3


Запустите приложение и, используя окно _Threads_, запишите параметры ID, Managed ID и Name для текущего потока в каждой точке останова.

3. Заполните таблицу:

| Breakpoint | Thread ID   | Location          | Thread Name |
| ---------- | ----------- | ----------------- | ----------- |
| #1.1       | 16704212    | 0x0 in ODataEntity.Program.Main(string[] args) | <No Name>   |
| #1.2       | 16729035    | 0x0 in ODataEntity.Program.Main.AnonymousMethod__0(System.IAsyncResult ar) | Worker Thread |
| #1.3       | 16739801    | 0x0 in ODataEntity.Program.Main(string[] args) | <No Name>   |
| #2.1       | 16755512    | 0x0 in ODataEntity.Program.Main(string[] args) | <No Name>   |
| #2.2       | 16760928    | ODataEntity.Program.Main.AnonymousMethod__0(System.IAsyncResult iar) | Worker Thread |
| #2.3       | 16760928    | ODataEntity.Program.Main(string[] args) |  Worker Thread |


