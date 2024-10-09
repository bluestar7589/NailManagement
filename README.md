# NailSalonManagement with Entity Framework

This project is a simple nail salon management system allowing users to manage the salon's supplies, employees, and customers. 
The project is built using Entity Framework and SQL Server.

**Authors:**

[Thien Nguyen](https://github.com/bluestar7589)

[Ivan Vanwoerkom](https://github.com/ScottProgrammer88)


## Getting started

- Visual Studio 2022
- .Net 8

### Generate database from the Models

Click on Tools -> Nuget package manager -> Package manager console
Type the command below to generate the models
```csharp
Update-Database
```

You may need to change the DB connection string located in the ApplicationDbContext class.
By default, it points to mssqllocaldb. You can change it to your own database.
```csharp
optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NailSalon");
```
