# Instructions for maintaining the Sqlite database.

We use [Dotnet EF tooling](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) for managing database changes.

[Detailed instructions on how to use the tooling](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### To create a new migration
- `dotnet ef migrations add MigrationName --project \src\Infrastructure --output-dir Database\Migrations`

The *--project* parameter is necessary so the tooling knows where to find the necessary configuration
to roll out the database. See also the implementation ContextDesignTimeFactory class.
We expect to find a appsettings.json file in the root of the SmartLab.QualityControl.Infrastructure project.
We read the contents of that file to get the connectionstring for the database.

The *--output-dir* parameter is needed so the migrations are created at the desired location in our project.
These locations are relative to the root of the project.

### To update your current database:
- `dotnet ef database update --project \src\Infrastructure`