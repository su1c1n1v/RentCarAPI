# Rent a car API (.NET)
### The project has the goal to build a API that provide the datas from a store of rent cars.
---
#### The goals
- [x] Build a API in .NET;
- [x] Create a new User;
- [x] Create a new Car;
- [x] Create a new Register;
- [x] Update a Register;
- [x] Update a Car;
- [x] Update a User;
- [x] Provide Registers data (registers of cars rented);
- [x] Provide Users data;
- [x] Provide Cars data;
- [x] Implements Dto using AutoMapper;
- [x] Rent a car;
- [x] SQL Server; 
- [x] Implement Identity; 
- [x] User Authentication (You need to create a login in the SQL Server).

---
#### The technologies
- .NET Core;
- SQL Server (you can use another DB, but you need to change the [1]);
- Visual Studio (but you can you Visual Studio Code);
- Postman (to test the requests).
---
#### Dependencies
- Microsoft.EntityFrameworkCore;
- Microsoft.EntityFrameworkCore.Design;
- Microsoft.EntityFrameworkCore.SqlServer [1];
- Microsoft.AspNetCore.JsonPatch;
- Microsoft.AspNetCore.Mvc.NewtonsoftJson;
- AutoMapper.Extensions.Microsoft.DependencyInjection;
- Microsoft.Extensions.Identity.Core;
- Microsoft.AspNetCore.Identity.EntityFrameworkCore;
---
#### Migrations
- "dotnet toll install --global dotnet-ef" => to install the "dotnet-ef if you dont have installed in your PC";
- "dotnet ef migrations add InitialMigration" => to add a new migration with the name "InitialMigration";
- "dotnet ef migrations remove" => remove all migrations created; 
- "dotnet ef database update" => create the DB or update the tables from the DB.
---
```diff
 This project it was create only to pratice .NET.
 ```
