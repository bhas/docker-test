# docker-test
This is just a small API I developed to get more familiar with Docker. It is a sample API project for importing, storing, and sharing image files. 



## Getting started
1. Install and run docker.
1. Open the solution and set `docker-compose` as your startup project
1. Run the project. This will create the following things for you:
   * Set up a local [Postgres database](https://www.postgresql.org/)
   * Install [pgAdmin](https://www.pgadmin.org/) for managing the local database
   * Run the API project and automatically open its swagger page
   
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


