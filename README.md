# ASP.NET Core with MSSQL DB Sample Application

## Installing Person Database
 To install person database, run `PersonDirectory.sql` script under `SQLDbScript` folder
 
## To configure environment variable 
After enabling docker support, modify yaml file and add below lines under `environment` section.

 - - SQLDB_SERVER=[SQL_Server_IP]
 - - SQLDB_DATABASE=[SQL_Server_Database]
 - - SQLDB_PORT=[SQL_Database_Port Default is 1433]
 - - SQLDB_USER=[SQL_Server_User]
 - - SQLDB_PASSWORD=[SQL_Server_Password]
