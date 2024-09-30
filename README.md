# NailSalonManagement with Entity Framework

This project is a simple nail salon management system that allows the user to manage the salon's supplies, employees, and customers. 
The project is built using Entity Framework and SQL Server.

**Authors:**

[Thien Nguyen](https://github.com/bluestar7589)


## Getting started

- Visual Studio 2022
- .Net 8
- [Nail Salon Management](NailSalonManagement.sql) installed

### Step one - Run the query to generate the database in the sql server
You can generate the database by using SQL Server Object Explorer in VS 2022 or using SQL Server Management Studio.

### Step two - generate models from the database

Click on Tools -> nuget package manager -> package manager console
Type the command below to generate the models
```csharp
Scaffold-DbContext 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NailSalon' Microsoft.EntityFrameworkCore.SqlServer
```

You may need to change the DB connection string located in the ApplicationDbContext class.
By default, it points to mssqllocaldb. You can change it to your own database.
```csharp
optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NailSalon");
```