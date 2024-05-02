# docker-test
This started as a small API I developed to get more familiar with Docker. It later turned into a proof of concept for a platform that can distribute digital assets.

I use the following core concepts:
 * **Distribution configs** dictate how assets should be distributed. For instance when should it happen, which assets should be shared and to whom.
 * **Distributions** are added each times assets for a distribution config get distributed. I holds information about what was shared, when and how it was shared.

The solution consists of the following core parts:
* An API from where the user can configure how assets should be distributed. This will be consumed by a UI.
* Background services (workers) responsible for reacting to triggers, aggregating the needed data and distribute the content through the appropriate channel.

**Areas of interest**
* The API is designed using FastEndpoints to ensure good performance while utilizing the native code separation and slim setup
* The solution uses Postgres as a database
* The code is designed for to run in Docker and to support easy change of implementations by relying on interfaces
* The DistributionManager is the orchestrator of the distribution. The concrete implementation for the distribution depends largely on the chosen channel and method of distribution. Each implementation is added as a new IDistributor to reduce coupling and making easy to add future implementations.

**NB** This code focuses on the area of Distributions and therefore all other parts such as Asset management are mocked for now.




## Getting started
1. Install Docker.
1. Open the solution and set `docker-compose` as your startup project
1. Run the project. This will create the following things for you:
   * Set up a local [Postgres database](https://www.postgresql.org/)
   * Install [pgAdmin](https://www.pgadmin.org/) for managing the local database
   * Run the API project and automatically open its swagger page
   * Run the Workers which listens for triggers and distribute assets
   
**OBS** if you get a `Connection is not private` error you may need to restart your browser. 

### Using pgAdmin
1. Make sure the database is running in docker. If not, run the docker-compose project in visual studio.
1. navigate to http://localhost:5050/ in your browser
1. Log in as `local@test.com` and password = `postgres`
1. Under _Servers_ create a new server with the details:
   * Name = Local
   * Host = postgres-db
   * Port = 5432
   * Username = postgres
   * Password = postgres
1. You can now expand the server _Local_ to find the database `docker_test`. This will be your local database containing all the data for the application. 


### Using another database management tool
You are free to use another datbase management tool instead of pgAdmin if you prefer. 

1. First make sure the database is running in docker. If not, run the docker-compose project in visual studio.
2. Now you can connect from your preferred tool with the following details:
   * Host = localhost
   * Port = 8001
   * Username = postgres
   * Password = postgres

### Making database schema changes
For changes to the database schema we use [Entity Framework with the .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) to add and apply database migrations.

First install the .NET Core CLI globally by running: `dotnet tool install --global dotnet-ef`

Now you can add a new migration by running the below command. Replace `<YourMigrationName>` with the name of your migration, i.e. _AddUsersTable_. Remember to review that the migration file looks correct.
```shell
dotnet ef migrations add <YourMigrationName> --context DockerTestContext --project src\Infrastructure --startup-project src\Api --output-dir Database/Migrations --configuration Development
```
**OBS** If you encounter an error: `Your startup project 'Api' doesn't reference...` try rebuilding the entire solution.

All migrations are automatically applied to your database when you run the project.


