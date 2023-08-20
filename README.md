# Test Task
Web_api Test Task

## Prerequisites
Before you begin, make sure your development environment includes dotnet core SDK v3.0+.

Used NuGet packages:
"Microsoft.EntityFrameworkCore" Version="7.0.2" 
"Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.2"
"Microsoft.EntityFrameworkCore.Tools" Version="7.0.2"


## Build
Restore Data base from backup file "TestDb.bak", the backup is in folder DBbac in project.
Create user in SQL server: "Web_api"/"password".
Then use that in ConnectionStrings, where change please Data source, login and password.