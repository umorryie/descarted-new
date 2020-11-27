# HOW TO START APPLICATION

- Start local SQL server. Needed configurations are in `appsetting.json`. (You can customize your database configurations). This application is configured to use:
    - Server = `.`
    - Database = `Differences`
- After your server is setup and database is manually created you need to create tables in database. To create them execute these commands in Package Manager Console of Descartes in Visual Studio: 
    - `EntityFrameworkCore\Add-Migration nameOfMigration`
    - `EntityFrameworkCore\Update-Database`
- Then run aplication and go to: `https://localhost:44387/` where is Swagger. In order for everything to work make sure that you have data base configured!

- Tests are written in project: `Tests`.