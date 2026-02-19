//╔══════════════════════════════════════════════════════════════════════════════════════════════════════╗
//║                                                                                                      ║
//║   ENTITY FRAMEWORK CORE  —  Personal Study Notes                                                     ║
//║                                                                                                      ║
//║   Table of Contents:                                                                                 ║
//║   ─────────────────────────────────────────────────────────────────────────────────────────────────  ║
//║   FUNDAMENTALS          01. Entities  02. DbContext  03. Working w/ DbContext                        ║
//║                         04. Change Tracking  05. Connection Strings                                  ║
//║   MIGRATIONS            06. Migrations  07. SQL Scripts  08. PMC Commands  09. CLI Commands          ║
//║   CRUD                  10. Saving (Connected)  11. Querying  12. Insert (Disconnected)              ║
//║                         13. Update (Disconnected)  14. Delete (Disconnected)                         ║
//║   CONVENTIONS & CONFIG  15. Conventions  16. One-to-Many  17. One-to-One  18. Configurations         ║
//║                         19. Fluent 1:N  20. Fluent 1:1  21. Fluent N:N                               ║
//║   ADVANCED              22. Shadow Props  23. Disconnected Graphs  24. TrackGraph                    ║
//║                         25. Raw SQL  26. Stored Procedures  27. Database-First                       ║
//║                                                                                                      ║
//╚══════════════════════════════════════════════════════════════════════════════════════════════════════╝


//> Entity Framework Core

/*
Entity Framework is an Object/Relational Mapping (O/RM) framework.
It is an enhancement to ADO.NET that gives developers an automated mechanism for accessing & storing the data in the database.

> EF Core Development Approaches
EF Core supports two development approaches:
1) Code-First
2) Database-First.
EF Core mainly targets the code-first approach and provides little support for the database-first approach because the visual designer or wizard for DB model is not supported
as of EF Core.

# Code-First
In the code-first approach, EF Core API creates the database and tables using migration based on the conventions and configuration provided in your domain classes.
This approach is useful in Domain Driven Design (DDD).
# Database-First
In the database-first approach, EF Core API creates the domain and context classes based on your existing database using EF Core commands.
This has limited support in EF Core as it does not support visual designer or wizard.

> EF Core vs EF 6
Entity Framework Core is the new and improved version of Entity Framework for .NET Core applications. EF Core is new, so still not as mature as EF 6.
EF Core continues to support the following features and concepts, same as EF 6.

1. DbContext & DbSet
2. Data Model
3. Querying using Linq-to-Entities
4. Change Tracking
5. SaveChanges
6. Migrations

> EF Core Database Providers
Entity Framework Core uses a provider model to access many different databases. EF Core includes providers as NuGet packages which you need to install.

The following table lists database providers and NuGet packages for EF Core.
┏━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Database             ┃ NuGet Package                                                        ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ SQL Server           ┃ Microsoft.EntityFrameworkCore.SqlServer                              ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ MySQL                ┃ MySql.Data.EntityFrameworkCore                                       ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ PostgreSQL           ┃ Npgsql.EntityFrameworkCore.PostgreSQL                                ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ SQLite               ┃ Microsoft.EntityFrameworkCore.SQLite                                 ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ SQL Compact          ┃ EntityFrameworkCore.SqlServerCompact40                               ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ In-memory            ┃ Microsoft.EntityFrameworkCore.InMemory                               ┃
┗━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/



//────────────────────────────────────────────────────────────────────────────────────────────────────
//  ▌ FUNDAMENTALS
//────────────────────────────────────────────────────────────────────────────────────────────────────

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [01]  Create Entities
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 1. Create Entities in Entity Framework Core

//> Here we will create domain classes for our demo application and will see the difference between domain classes and entity classes of Entity Framework Core.

//? Domain Classes Vs. Entity Classes
/*
> Domain classes
Domain classes are the classes that represent the business entities in your application. They are designed to model the real-world entities and their relationships.
They are typically used in the business logic layer of your application.

> Entity classes 
Entity classes, on the other hand, are the classes that represent the database tables in Entity Framework Core. 
They are designed to map to the database schema and are typically used in the data access layer of your application.
*/

//> Domain classes 
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int GradeId { get; set; }
    public Grade Grade { get; set; }
}

public class Grade
{
    public Grade()
    {
        Students = new List<Student>();
    }
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public IList<Student> Students { get; set; }
}
/*
! • The above Student class includes student-related properties such as StudentId, FirstName, and LastName.
! • It also includes the GradeId and Grade properties which refer to the GradeId property of the Grade class.
! • This is because each student belongs to any one grade and a grade can contain multiple students.

> • These are our two simple domain classes, but they have not become entities of Entity Framework yet.
> • The terms "entities" and "domain classes" are often used interchangeably, but they are slightly different concepts.
> • Entities in the Entity Framework are mapped to the corresponding tables in the database.
> • Entity Framework keeps track of these entities so that it can perform database CRUD (Create, Read, Update, Delete) operations automatically based on their objects status.
> • Domain classes include the core functionality and business rules of the application, ensuring that the business logic is properly implemented.

# • The Student and Grade classes are domain classes.
# • To treat them as entities, we need to include them as DbSet<T> properties in the DbContext class of Entity Framework so that EF engine can track their status.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [02]  DbContext
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 2. DbContext in Entity Framework Core
/*
> • The DbContext class is an integral part of the Entity Framework.
> • An instance of DbContext represents a session with the database which can be used to query and save instances of your entities to a database.
> • DbContext is a combination of the Unit Of Work and Repository patterns.

! DbContext in EF Core allows us to perform the following tasks:
1. Manage database connection
2. Configure model and relationships
3. Querying database
4. Saving data to the database
5. Configure change tracking
6. Caching
7. Transaction management
To use DbContext in our application, we need to create a class that derives from DbContext.
*/

//> Let's create a context class that derives the DbContext class, as shown below.
//! this class should be implemented in something called ApplicationDbContext.cs file in the Data folder of your project. 
public class SchoolContext : DbContext
{   
    //! empty context class
} 
/*
> The above SchoolContext class derives the DbContext class.
> It can be called a context class. This is an empty context class that does nothing.
> First of all, let's define the entities here.

# We created the Student and Grade domain classes in the Create Entities chapter.
# We can turn domain classes into entities by specifying them as DbSet<TEntity> type properties.
# This will allow Entity Framework to track their changes and perform CRUD operations accordingly.
*/

//> now we have entity classes in our context class, as shown below.
public class SchoolContext : DbContext
{     
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
}
/*
> The DbSet<TEntity> type allows EF Core to query and save instances of the specified entity to the database.
> LINQ queries against a DbSet<TEntity> will be translated into queries against the database.
> EF Core API will create the Student and Grade tables in the underlying SQL Server database where each property of these classes will be a column in the corresponding table.
*/

//> Configure Database Connection
/*
> We have specified entities as DbSet<TEntity> properties, but we haven't specified the database name and database server info yet.
> We can override DbContext's OnConfiguring() method to configure the database and other options to be used for this context.
> This method is called for each instance of the context that is created.
*/
//> Let's override the OnConfiguring() method in the context class, as shown below.

public class SchoolContext : DbContext
{       
    //entities
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
} 

/*
Above, base.OnConfiguring(optionsBuilder) calls the base implementation, which does nothing.
We can remove it and specify a connection string for the database in the OnConfiguring(), as shown below.
*/

public class SchoolContext : DbContext
{       
    //entities
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;");
    }
} 
//> In the above code, the optionsBuilder.UseSqlServer() is an extension method used to configure EF to use SQL Server as the database provider by specifying a database connection string.
//! EF API will create the specified database if it does not exist.



//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [03]  Working with DbContext
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 3. Working with DbContext in EF Core

/*
After creating the context and entity classes, it's time to start interacting with our underlying database using the context class.
However, before we save or retrieve data from the database, we first need to ensure that the database schema is created as per our entities and configurations.
There are two ways using which you can create a database and schema:

1. Using the EnsureCreated() method
2. Using Migration API

• Creating a database using the EnsureCreated() method is commonly used followed by EnsureDeleted() method when testing or prototyping using Entity Framework Core framework.
• Migrations API allows you to create an initial database schema based on your entities and then as and when you add/delete/modify your entities and config, it will sync to the
  corresponding schema changes to your database so that it remains compatible with your EF Core model (entities and other configuration).
Let's use the EnsureCreated() method to create a database and use the context class to save student and grade data in the database.
*/
using EFCoreTutorialsConsole;

using (var context = new SchoolDbContext())
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var grd1 = new Grade() { GradeName = "1st Grade" };
    var std1 = new Student() {  FirstName = "Yash", LastName = "Malhotra", Grade = grd1};

    //add entity to the context
    context.Students.Add(std1);

    //save data to the database tables
    context.SaveChanges();

    //retrieve all the students from the database
    foreach (var s in context.Students) {
        Console.WriteLine($"First Name: {s.FirstName}, Last Name: {s.LastName}");
    }
}

/*
> Let's understand the above example code:
1. The using(){ .. } statement creates a scope for a SchoolDbContext instance called context. It ensures that the context is properly disposed of when it's no longer needed.
2. The context.Database.EnsureCreated(); statement checks if the database exists.
    If it doesn't exist, it creates the database based on the entity classes defined as DbSet<TEntity> properties in the SchoolDbContext class. 
    This is a simple way to create the database schema based on your entity model.
3. Next, two entity objects are created: grd1 of type Grade and std1 of type Student. .
    ! These objects are entity objects that represent records in the database.
4. The std1 student entity is added to the Students property of the context.
    It also assigns grd1 to its Grade property.
    This prepares the Student entity to be saved to the database along with the Grade entity.
5. The context.SaveChanges(); is called to persist the changes to the database.
    It effectively inserts a new student record into the "Students" table and a new grade record into the "Grades" table.
6. After saving changes, a foreach loop retrieves all the students from the "Students" table in the database using the context.Students property.
    It then prints the first name and last name of each student to the console.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [04]  Change Tracking
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 4. Tracking Changes of Entities in EF Core
/*
• The DbContext in Entity Framework Core includes the ChangeTracker property of type ChangeTracker class which is responsible for tracking the state of each
    entity retrieved using the DbContext instance.

! Note that it is not intended to use it directly in your application code. It is just to understand how EF tracks changes of entities.
> The ChangeTracker starts tracking of all the entities as soon as it is retrieved using the object of the DbContext class, until they go out of its scope.
> EF keeps track of all the changes applied to all the entities and their properties, so that it can build and execute appropriate DML statements to the underlying data source.

!!! An entity at any point of time has one of the following states which are represented by the enum Microsoft.EntityFrameworkCore.EntityState in EF Core.
1. Added
2. Modified
3. Deleted
4. Unchanged
5. Detached
*/
//> Let's see how the EntityState is changed automatically based on the action performed on the entity.
//# Unchanged State
//> First, all the entities retrieved using direct SQL query or LINQ-to-Entities queries will have the Unchanged state.

public static void Main()
{
    using (var context = new SchoolContext())
    {
        // retrieve entity 
        var student = context.Students.FirstOrDefault();
        DisplayStates(context.ChangeTracker.Entries()); // Output => Entity: Student, State: Unchanged
    }
}

static void DisplayStates(IEnumerable<EntityEntry> entries)
{
    foreach (var entry in entries)
    {
        Console.WriteLine($"Entity: {entry.Entity.GetType().Name},
                                State: {entry.State.ToString()} ");
    }
}
/*
1. In the above example, we fetch the student entity using context.Students.FirstOrDefault() method.
2. As soon as we retrieve it, the context class starts tracking it.
3. We pass the context.ChangeTracker.Entries() to the DisplayStates() method.
    The context.ChangeTracker.Entries() returns a collection of EntityEntry for each entity being tracked by the context.

4. The DisplayStates() method iterates through each entity entry in the provided EntityEntry collection using a foreach loop.
    It prints the name of the entity type (e.g., Student, Grade, etc.) obtained using entry.Entity.GetType().Name and the state of the entity obtained using entry.State.ToString().

> We don't perform any operation on the student object, so the status will be "Unchanged".
It means when calling the context.SaveChanges() method, nothing will happen. 
No DB query will be executed as no entity has been changed in the scope of the context object.
*/

//# Added State
//> All the new entities without a key property value, added in the DbContext using the Add() or Update() method will be marked as Added.
using (var context = new SchoolContext())
{              
    context.Students.Add(new Student() { FirstName = "Bill", LastName = "Gates" });
    
    DisplayStates(context.ChangeTracker.Entries()); // Output => Entity: Student, State: Added
}

//# Modified State
//> If the value of any property of an entity is changed in the scope of the DbContext, then it will be marked as Modified state.

using (var context = new SchoolContext())
{
    var student = context.Students.FirstOrDefault();
    student.LastName = "Friss";
    DisplayStates(context.ChangeTracker.Entries()); // Output => Entity: Student, State: Modified
}

//# Deleted State
//> If any entity is removed from the DbContext using the DbContext.Remove or DbSet.Remove method, then it will be marked as Deleted.

using (var context = new SchoolContext())
{
    var student = context.Students.FirstOrDefault();
    context.Students.Remove(student);
    
    DisplayStates(context.ChangeTracker.Entries()); // Output => Entity: Student, State: Deleted
}

//# Detached State
//> All the entities which were created or retrieved out of the scope of the current DbContext instance, will have the Detached state.
//> They are also called disconnected entities and are not being tracked by an existing DbContext instance.

var disconnectedEntity = new Student() { StudentId = 1, Name = "Bill" };

using (var context = new SchoolContext())
{              
    Console.Write(context.Entry(disconnectedEntity).State); // Output => Detached
}
//> In the above example, disconnectedEntity is created out of the scope of DbContext instance (context). So, it is in the Detached state for the context.



//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [05]  Connection Strings
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 5. Database Connection String in Entity Framework Core
/*
Database Connection String Formats
The most common format of a connection string in EF Core is:
# Server={server_address};Database={database_name};UserId={username};Password={password};
Replace {server_address}, {database_name}, {username}, and {password} with your specific database credentials.

> In case you have integrated security enabled (Windows authentication), you can use "Trusted_Connection=True;" instead of specifying the username and password, as shown below.
# Server={server_address};Database={database_name}; Trusted_Connection={true or false};

! Another format of the connection string is:
"Data Source={server_address};Initial Catalog={database_name};Integrated Security=True;" 
> This format allows for Windows authentication, which means you do not need to provide the username and password.
> Instead, the connection will be authenticated using the current Windows user credentials.
*/


//> Manage Connection String in EF Core
/*
> Hardcoding Connection String
Use the DbContextOptionsBuilder class and configure the connection string directly in the OnConfiguring method of your DbContext class. 
This allows you to hardcode the connection string within your code, as shown below.
*/

public class SchoolContext : DbContext
{       

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;");
    }
} 

/*
> appsettings.json file
Another approach is to store the connection string in the appsettings.json file and retrieve it using the configuration API.
This allows for easy configuration and flexibility, as the connection string can be changed without modifying the code.
If your project doesn't already have it, add Microsoft.Extensions.Configuration NuGet package to your project.

{
    "ConnectionStrings": {
        "SchoolDBLocalConnection": "Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;"
    }
}
Then, you can retrieve the connection string in your DbContext class as shown below.
Now, you need to install Microsoft.Extensions.Configuration and Microsoft.Extensions.Configuration.Json NuGet package to your project.
After installing the package, you need to build the configuration by adding appsettings.json file, as shown below.
*/

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

// You also need to add a constructor which accepts the IConfiguration object, as shown below.

public class SchoolContext : DbContext
{       
    IConfiguration appConfig;
    public SchoolDbContext(IConfiguration config)
    {
        appConfig = config;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(appConfig.GetConnectionString("SchoolDBLocalConnection"));
    }
} 

//> Now, you can pass the configuration when you create an object of DbContext, as shown below:

using (var context = new SchoolDbContext(configuration))
{

}





//────────────────────────────────────────────────────────────────────────────────────────────────────
//  ▌ MIGRATIONS
//────────────────────────────────────────────────────────────────────────────────────────────────────

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [06]  Migrations
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 6. Migrations in Entity Framework Core
/*
Migration is a way to keep the database schema in sync with the EF Core model by preserving data.
EF Core API builds the EF Core model from the entity classes, Data Annotations attributes applied on entity classes and Fluent API configurations in the DbContext class.
EF Core migrations API will create or update the database schema based on the EF Core model.
Whenever you change the domain classes, you need to run migration commands to keep the database schema up to date.
EF Core migrations are a set of commands which you can execute in Package Manager Console or PowerShell or in .NET Core CLI (Command Line Interface).
> Adding a Migration
In the Working with DbContext chapter, we used the context.Database.EnsureCreated() method to create the database and schema for the first time.
! Note that it creates a database the first time only. It cannot change the DB schema after that. For development projects, we must use EF Core Migrations API.

To use EF Core Migrations API, we need to install the NuGet package Microsoft.EntityFrameworkCore.Tools.
*/ 

//> The following are Student and Grade classes.

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int GradeId { get; set; }
    public Grade Grade { get; set; }
}

public class Grade
{
    public Grade()
    {
        Students = new List<Student>();
    }
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public IList<Student> Students { get; set; }
}

//> The following is our context class SchoolDbContext created in the Create DbContext chapter.

public class SchoolContext : DbContext
{       
    //entities
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;");
    }
} 

/*
EF Core provides migrations commands to create, update, or remove tables and other DB objects based on the entities and configurations.
At this point, there is no SchoolDB database. So, we need to create the database from the model (entities and configurations) by adding a migration.
You can execute migration commands using:
> 1. Package Manager Console
> 2. PowerShell 
> 3. .NET Core CLI tools
as per your choice. Microsoft recommends .NET Core CLI tools because they run on all platforms.
Since we are using Visual Studio, we will use Package Manager Console commands (PMC and PowerShell commands are the same).
*/
//# steps using the Package Manager Console
/*
> 1. Open the Package Manager Console in Visual Studio from Tools > NuGet Package Manager > Package Manager Console.
> 2. use this command "add-migration InitialSchoolDB" it works for Package Manager Console or PowerShell.
> 3. use this command "dotnet ef migrations add InitialSchoolDB" If you use .NET Core CLI, then enter the following command.

! This will create a new folder named Migrations in the project and create the ModelSnapshot files, as shown below.
> • <timestamp>_<Migration Name>.cs: The main migration file which includes migration operations in the Up() and Down() methods.
>   The Up() method includes the code for creating DB objects and the Down() method includes code for removing DB objects.
> • <contextclassname>ModelSnapshot.cs: A snapshot of your current model. This is used to determine what changed when creating the next migration.

# Now, to create a database, use the update-database command in the Package Manager Console, as shown below.
> use this command "update-database -verbose" in Package Manager Console or PowerShell
> ues this command "dotnet ef database update" in .Net CLI.

> The following executes the update-database command and creates the database.
> The -verbose option shows the logs while creating the database. 
> It creates a database with the name and location specified in the connection string in the UseSqlServer() method.
> It creates a table for each entity, Student and Grade. It also creates the _EFMigrationHistory table that stores the history of migrations applied over time.
*/

//> Apply Migrations for Modified Entities/Configurations
/*
Suppose we add a new entity or modify an existing entity or change any configuration, then we again need to execute the add-migration and update-database commands to apply changes
to the database schema.
For example, let's modify the Student entity and add some properties, as shown below.
*/

//> For example, let's modify the Student entity and add some properties, as shown below.
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte[] Photo { get; set; }
    public decimal Height { get; set; }
    public float Weight { get; set; }

    public int GradeId { get; set; }
    public Grade Grade { get; set; }
}
//# Now, to sync our "SchoolDB" database with these changes, execute the following commands:
//> 1. use this command "add-migration "ModifiedStudentEntity"" in Package Manager Console or PowerShell
//> 2. Now, to update the database schema, execute the "update-database" command in PMC/PowerShell. This will add the columns in the Student table

//# for the .net CLI, you can use the following command to add migration and update database.
//> 1. use this command "dotnet ef migrations add AddStudentDetails" in .Net CLI
//> 2. use this command "dotnet ef database update" in .Net CLI

//> Reverting Migration
/*
For some reason, if you want to revert the database to any of the previous states then you can do it by using the update-database <migration-name> command.

For example, we modified the Student entity and added some more properties. But now we want to revert it back to the state of the "InitialSchoolDB" migration.
We can do it by using the following command:
> 1. use this command "Update-database "InitialSchoolDB"" in Package Manager Console or PowerShell
> 2. use this command "dotnet ef database update InitialSchoolDB" in .Net CLI

! The above command will revert the database based on a migration named InitialSchoolDB and remove all the changes applied by the second migration ModifiedStudentEntity.
! This will also remove ModifiedStudentEntity entry from the __EFMigrationsHistory table in the database.
*/

//> List All Migrations
//> Use the following migration command to get the list of all migrations.
//> 1. use this command "Get-Migration" in Package Manager Console or PowerShell
//> 2. use this command "dotnet ef migrations list" in .Net CLI

//> Removing a Migration
/*
> Above, we have reverted the second migration named "ModifiedStudentEntity".
> We can remove the last migration if it is not applied to the database. Let's remove the "ModifiedStudentEntity" file using the following command.
> 1. use this command "Remove-Migration" in Package Manager Console or PowerShell
> 2. use this command "dotnet ef migrations remove" in .Net CLI

The above commands will remove the last migration and revert the model snapshot to the previous migration. 
!Please note that if a migration is already applied to the database, then it will throw an exception.
*/



//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [07]  Generate SQL Scripts
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 7. Generate SQL Script in Entity Framework Core
/*
Here you will learn how to generate a SQL script from the EF Core model using a migration which you can use to execute manually or add to the source control.
In the previous Migrations chapter, we added the migration and created the "SchoolDB" database.
! It is recommended to deploy migrations to a production database by generating SQL scripts.
The following table lists PMC/PowerShell commands and .NET Core CLI commands to generate a SQL script from the applied migrations.
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ PMC Command (Visual Studio) ┃ .NET Core CLI Command                    ┃ Usage / Description                                                           ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Script-Migration            ┃ dotnet ef migrations script              ┃ Generates a SQL Script for all migrations                                     ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Script-Migration <From>     ┃ dotnet ef migrations script <From>       ┃ Generates a SQL script from the given migration to the latest migration.      ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Script-Migration <F> <T>    ┃ dotnet ef migrations script <F> <T>      ┃ Generates a SQL script from the specified from migration to the to migration. ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Script-Migration -Idempotent┃ dotnet ef migrations script --idempotent ┃ Generates idempotent scripts which check for existence before applying.       ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Get-Migration               ┃ dotnet ef migrations list                ┃ List all existing migrations.                                                 ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛

! Execute the following migration command to generate a SQL script for the entire database in PMC or PowerShell terminal.
> use this command "Script-Migration" in Package Manager Console or PowerShell
> use this command "dotnet ef migrations script" in .Net CLI
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [08]  PMC / PowerShell Commands
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 8. PMC/PowerShell Commands for Migrations
/*
Migration commands in Entity Framework Core can be executed using the Package Manager Console in Visual Studio. steps:
1. Open the Package Manager Console from menu Tools
2. navigate to NuGet Package Manager
3. go to Package Manager Console in Visual Studio to execute the following commands.

┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ PMC Command                  ┃ Usage / Description                                                    ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Get-Help entityframework     ┃ Displays information about entity framework commands.                  ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Add-Migration <name>         ┃ Creates a migration by adding a migration snapshot.                    ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Remove-Migration             ┃ Removes the last migration snapshot.                                   ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Update-Database              ┃ Updates the database schema based on the last migration snapshot.      ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Script-Migration             ┃ Generates a SQL script using all the migration snapshots.              ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Scaffold-DbContext           ┃ Generates a DbContext and entity type classes for a specified database.┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Get-DbContext                ┃ Gets information about a DbContext type.                               ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Drop-Database                ┃ Drops the database.                                                    ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [09]  CLI Commands (.NET)
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 9. Command Line Interface Commands for Migrations
/*
The .NET Core CLI (Command Line Interface) tools for Entity Framework Core perform design-time development tasks such as migrations, script generation, 
and generating model code from an existing database. It can be used on any platform.
Install the .NET Core CLI tools for EF Core using the following command:
> use this command "dotnet tool install --global dotnet-ef" in the terminal to install the .NET Core CLI tools for EF Core.
After installing the tools, you can execute the following commands in the terminal to perform various migration-related tasks.
! note that you can use this command "dotnet ef --help" to get the list of all available commands and their usage.
there are three main EF commands available:
> 1. database 
> 2. dbcontext 
> 3. migrations.
The following table lists all EF commands and sub-commands.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Command       ┃ Sub-Command       ┃ Usage / Description                                    ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ database      ┃ drop              ┃ Drops the database.                                    ┃
┃               ┃ update            ┃ Updates the database to a specified migration.         ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ dbcontext     ┃ info              ┃ Gets information about a DbContext type.               ┃
┃               ┃ list              ┃ Lists available DbContext types.                       ┃
┃               ┃ scaffold          ┃ Scaffolds a DbContext and entity types for a database. ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ migrations    ┃ add               ┃ Adds a new migration.                                  ┃
┃               ┃ list              ┃ Lists available migrations.                            ┃
┃               ┃ remove            ┃ Removes the last migration.                            ┃
┃               ┃ script            ┃ Generates a SQL script from migrations.                ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
! you can use this command ""dotnet ef <command> <sub-command> --help" to get the usage of a specific command and sub-command.
> For example, you can use this command "dotnet ef migrations add --help" to get the usage of the "add" sub-command of the "migrations" command.
*/



//────────────────────────────────────────────────────────────────────────────────────────────────────
//  ▌ CRUD
//────────────────────────────────────────────────────────────────────────────────────────────────────

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [10]  Saving Data — Connected Scenario
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 10. Entity Framework Core: Saving Data in Connected Scenario
/*
> Entity Framework Core provides different ways to add, update, or delete data in the underlying database.
> An entity contains data in its scalar property will be either inserted or updated or deleted based on its EntityState.

There are two scenarios to save an entity data: 
# 1. connected. 
# 2. disconnected.
• In the connected scenario, the same instance of DbContext is used in retrieving and saving entities.
• whereas this is different in the disconnected scenario. 

• Entity Framework builds and executes INSERT, UPDATE, or DELETE statements for the entities whose EntityState is Added, Modified, or Deleted when the DbContext.SaveChanges() 
    method is called.
• In the connected scenario, an instance of DbContext keeps track of all the entities and so it automatically sets an appropriate EntityState of each entity whenever an entity is 
    created, modified, or deleted.
*/

//> Insert Data
//> The DbSet.Add and DbContext.Add methods add a new entity to a context (instance of DbContext) which will insert a new record in the database when you call the SaveChanges() method.

using (var context = new SchoolContext()) //> this is the connected scenario as we are using the same context instance to add and save the entity.
{
    var std = new Student()
    {
        FirstName = "Bill",
        LastName = "Gates"
    };
    context.Students.Add(std);
    // or
    // context.Add<Student>(std);
    context.SaveChanges();
}
/*
> what is happening in the above code:
1. context.Students.Add(std) adds a newly created instance of the Student entity to a context with Added EntityState.
2. EF Core introduced the new DbContext.Add method, which does the same thing as the DbSet.Add method.
3. After this, the SaveChanges() method builds and executes the following INSERT statement to the database.

> '''
> exec sp_executesql N'SET NOCOUNT ON;
> INSERT INTO [Students] ( [FirstName], [LastName])
> VALUES (@p0, @p1);
> SELECT [StudentId]
> FROM [Students]
> WHERE @@ROWCOUNT = 1 AND [StudentId] = scope_identity();',N
> '@p0 nvarchar(4000), @p1 nvarchar(4000) ',@p0=N'Bill',@p1=N'Gates'
> go
> '''
# lets break down the above SQL statement:
> 1. exec sp_executesql: This is SQL Server's built-in stored procedure for executing a parameterized SQL string safely
> 2. SET NOCOUNT ON; This prevents SQL Server from sending messages about the number of rows affected by the query, which can improve performance.
> 3. The actual insert. 
> INSERT INTO [Students] ([FirstName], [LastName]) 
> VALUES (@p0, @p1);  

!  Notice EF excludes StudentId from the column list because it's an identity (auto-increment) column — SQL Server will generate it automatically.
>4. After inserting, EF immediately retrieves the newly generated primary key 
> SELECT [StudentId]
> FROM [Students]
> WHERE @@ROWCOUNT = 1 AND [StudentId] = scope_identity();
> @@ROWCOUNT = 1 — confirms exactly one row was inserted successfully
> scope_identity() — gets the last identity value generated in the current scope, which is the StudentId just created
! EF uses this returned StudentId to update your std object in memory, so after SaveChanges() your object automatically has the new ID populated. 

> 5. The parameter declaration:
> '@p0 nvarchar(4000), @p1 nvarchar(4000)'
> Declares the types of the two parameters. 
> EF uses nvarchar(4000) by default for strings (Unicode), and uses nvarchar(max) for strings longer than 4000 characters.

> 6. The parameter values:
@p0=N'Bill', @p1=N'Gates'
! The actual values from your C# std object — the N prefix means they're Unicode strings, matching the nvarchar type.

# In summary
> 1. when you call SaveChanges(), EF translates your C# object into this safe, parameterized SQL that inserts the row
> 2. hands the new database-generated ID back to your application automatically.
*/

//> Updating Data
/*
> In the connected scenario, EF Core API keeps track of all the entities retrieved using a context.
> Therefore, when you edit entity data, EF automatically marks EntityState to Modified, which results in an updated statement in the database when you call the SaveChanges() method.

*/
using (var context = new SchoolContext())
{
    var std = context.Students.First<Student>(); 
    std.FirstName = "Steve";
    context.SaveChanges();
}

/*
> In the above example, we retrieve the first student from the database using context.Students.First<student>().
> As soon as we modify the FirstName, the context sets its EntityState to Modified because of the modification performed in the scope of the DbContext instance (context).
> So, when we call the SaveChanges() method, it builds and executes the following Update statement in the database.
> '''
> exec sp_executesql N'SET NOCOUNT ON;
> UPDATE [Students] SET [FirstName] = @p0
> WHERE [StudentId] = @p1;
> SELECT @@ROWCOUNT;
> ',N'@p1 int,@p0 nvarchar(4000)',@p1=1,@p0=N'Steve'
> Go
> '''
In an update statement, EF Core API includes the properties with modified values, the rest being ignored.
In the above example, only the FirstName property was edited, so an update statement includes only the FirstName column.
*/

//> Deleting Data
//> Use the DbSet.Remove() or DbContext.Remove methods to delete a record in the database table.
using (var context = new SchoolContext())
{
    var std = context.Students.First<Student>();
    context.Students.Remove(std);

    // or
    // context.Remove<Student>(std);

    context.SaveChanges();
}

/*
> In the above example, context.Students.Remove(std) or context.Remove<Student>(std) marks the std entity object as Deleted.
Therefore, EF Core will build and execute the following DELETE statement in the database.
> '''
> exec sp_executesql N'SET NOCOUNT ON;
> DELETE FROM [Students]
> WHERE [StudentId] = @p0;
> SELECT @@ROWCOUNT;
> ',N'@p0 int',@p0=1
> Go
> '''

! Thus, it is very easy to add, update, or delete data in Entity Framework Core in the connected scenario.
*/

//> some advanced topics related to saving data, deleting data, and updating data ill bit them here to just now that they are exist:
//# 1. EF-Core Bulk Insert
//# 2. EF-Core Execute Delete
//# 3. EF-Core Execute Update 
//> ill not study them now because they are advanced and now i won't need them, but i will study them later when i need them.




//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [11]  Querying
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 11. Querying in Entity Framework Core
/*
> Querying in Entity Framework Core remains the same as in EF 6.x, with more optimized SQL queries and the ability to include C# functions into LINQ-to-Entities queries.
> C# Functions in Queries
! EF Core has a new feature in LINQ-to-Entities where we can include C# functions in the query. This was not possible in EF 6.

! Note that linq works with IEnumerable and IQueryable:
> 1. IEnumerable:
It is used for in-memory collections and does not support deferred execution.
When you execute a LINQ query on an IEnumerable collection, the entire collection is loaded into memory, and then the query is executed against that in-memory data.
> 2. IQueryable: 
It is used for querying data from external sources, such as databases or remote services. 
It supports deferred execution, which means that the query is not executed until you iterate over the results.
When you execute a LINQ query on an IQueryable collection, the query is translated into a format that the underlying data source can understand (e.g., SQL for databases) and executed
on the server side, returning only the relevant data to the client.
! Deferred execution in LINQ means that a query is not executed when it is declared or created;
! instead, the query logic is stored, and the execution is delayed until the actual results are needed or requested.
*/
private static void Main(string[] args)
{
    var context = new SchoolContext();
    var studentsWithSameName = context.Students
                                    .Where(s => s.FirstName == GetName())
                                    .ToList();
}
public static string GetName() {
    return "Bill";
}

/*
In the above L2E query, we have included the GetName() C# function in the Where clause. This will execute the following query in the database:
> '''
> exec sp_executesql N'SELECT [s].[StudentId], [s].[DoB], [s].[FirstName], 
>     [s].[GradeId], [s].[LastName], [s].[MiddleName]
> FROM [Students] AS [s]
> WHERE [s].[FirstName] = @__GetName_0',N'@__GetName_0 nvarchar(4000)',
>     @__GetName_0=N'Bill'
> Go
> '''
*/

//> Eager Loading
/*
Entity Framework Core supports eager loading of related entities, same as EF 6, using the Include() extension method and projection query. 
In addition to this, it also provides the ThenInclude() extension method to load multiple levels of related entities. (EF 6 does not support the ThenInclude() method.)
*/
//> Include
//> Unlike EF 6, we can specify a lambda expression as a parameter in the Include() method to specify a navigation property as shown below.
var context = new SchoolContext();

var studentWithGrade = context.Students
                           .Where(s => s.FirstName == "Bill")
                           .Include(s => s.Grade)
                           .FirstOrDefault();

/*
In the above example, .Include(s => s.Grade) passes the lambda expression s => s.Grade to specify a reference property to be loaded with Student entity data from the database 
in a single SQL query. The above query executes the following SQL query in the database.

> '''
> SELECT TOP(1) [s].[StudentId], [s].[DoB], [s].[FirstName], [s].[GradeId],[s].[LastName], 
>         [s].[MiddleName], [s.Grade].[GradeId], [s.Grade].[GradeName], [s.Grade].[Section]
> FROM [Students] AS [s]
> LEFT JOIN [Grades] AS [s.Grade] ON [s].[GradeId] = [s.Grade].[GradeId]
> WHERE [s].[FirstName] = N'Bill'
> '''
! The Include() extension method can also be used after the FromSql() method. 
! The Include() extension method cannot be used after the DbSet.Find() method. 
! Use the Include() method multiple times to load multiple navigation properties of the same entity.
! EF Core introduced the new ThenInclude() extension method to load multiple levels of related entities.
! We can also load multiple related entities by using the projection query instead of Include() or ThenInclude() methods.
> projection Query Methods like Select() and SelectMany() can be used to load related entities by projecting the query results into a new form.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [12]  Insert — Disconnected Scenario
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 12. Insert Data in a Disconnected Scenario in Entity Framework Core
/*
Saving data in the disconnected scenario is a little bit different than in the connected scenario.
In the disconnected scenario, the DbContext is not aware of disconnected entities because entities were added or modified out of the scope of the current DbContext instance.
So, you need to attach the disconnected entities to a context with appropriate EntityState in order to perform CUD (Create, Update, Delete) operations to the database.

disconnected entities (entities which are not being tracked by the DbContext) need to be attached to the DbContext with an appropriate EntityState.
For example, Added state for new entities, Modified state for the edited entities and Deleted state for the deleted entities, which will result in an INSERT, UPDATE, or DELETE command
in the database when the SaveChanges() method is called.
The following steps must be performed in order to insert, update or delete records into the DB table using Entity Framework Core in a disconnected scenario:
> 1. Attach an entity to DbContext with an appropriate EntityState e.g. Added, Modified, or Deleted
> 2. Call SaveChanges() method

in the disconnected scenario it could be an error the entity is'nt been tracked by the context.
The following example demonstrates inserting a new record into the database using the above steps:
*/
//Disconnected entity
var std = new Student(){ Name = "Bill" };

using (var context = new SchoolContext())
{
    //1. Attach an entity to context with Added EntityState
    context.Add<Student>(std);
    
    //or the following are also valid
    // context.Students.Add(std);
    // context.Entry<Student>(std).State = EntityState.Added;
    // context.Attach<Student>(std);
    //2. Calling SaveChanges to insert a new record into Students table
    context.SaveChanges();
}

/*
In the example above, std is a disconnected instance of the Student entity.
> 1. The context.Add<Student>() method attaches a Student entity to a context with an Added state.
> 2. The SaveChanges() method builds and executes the following INSERT statement:
> '''
> exec sp_executesql N'SET NOCOUNT ON;
> INSERT INTO [Students] ([Name])
> VALUES (@p0);
> SELECT [StudentId]
> FROM [Students]
> WHERE @@ROWCOUNT = 1 AND [StudentId] = scope_identity();',N'@p0 nvarchar(4000), 
> @p1 nvarchar(4000) ',@p0=N'Bill'
> go
> '''

> EF Core provides multiple ways to add entities with Added state. In the above example, 
1. context.Students.Add(std);
2. context.Entry<Student>(std).State = EntityState.Added; 
3. context.Attach<Student>(std); 
! will result in the same INSERT statement as above.

Entity Framework Core provides the following DbContext and DbSet methods which attach disconnected entities with Added EntityState, which in turn will execute
INSERT statements in the database.
┏━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ DbContext Method     ┃ DbSet Method             ┃ Description / State Behavior                                         ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Attach               ┃ Attach                   ┃ Key exists: Unchanged state. No Key: Added state.                    ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Add                  ┃ Add                      ┃ Attaches entity with Added state.                                    ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ AddRange             ┃ AddRange                 ┃ Attaches multiple entities with Added state.                         ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Entry                ┃ (N/A)                    ┃ Provides access to Change Tracking info and manual state control.    ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ AddAsync             ┃ AddAsync                 ┃ Async add; starts tracking immediately.                              ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ AddRangeAsync        ┃ AddRangeAsync            ┃ Async add for multiple entities in one operation.                    ┃
┗━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
! Both DbContext and DbSet methods perform the same operation. Which one you use depends on your coding pattern and preference.
*/
//> Insert Relational Data
/*
Use the DbContext.Add or DbSet.Add method to add related entities to the database. 
The Add method attaches entities to a context and sets the Added state to all the entities in an entity graph whose Id (Key) properties are empty, null or the default value of data type.
Consider the following example.
*/

var stdAddress = new StudentAddress()
{
    City = "SFO",
    State = "CA",
    Country = "USA"
};

var std = new Student()
{
    Name = "Steve",
    Address = stdAddress
};
using (var context = new SchoolContext())
{
    // Attach an entity to DbContext with Added state
    context.Add<Student>(std);

    // Calling SaveChanges to insert a new record into Students table
    context.SaveChanges();
}

/*
In the example above, context.Add<Student>(std) adds an instance of Student entity.
EF Core API reaches the StudentAddress instance through the reference navigation property of Student and marks EntityState of both the entities to Added, which will build and
execute the following two INSERT commands on SaveChanges().
> '''
> exec sp_executesql N'SET NOCOUNT ON;
> INSERT INTO [Students] ([Name])
> VALUES (@p0);
> SELECT [StudentId]
> FROM [Students]
> WHERE @@ROWCOUNT = 1 AND [StudentId] = scope_identity();',N'@p0 nvarchar(4000), 
> @p1 nvarchar(4000) ',@p0=N'Steve'
> go
> 
> exec sp_executesql N'SET NOCOUNT ON;
> INSERT INTO [StudentAddresses] ([Address], [City], [Country], [State], [StudentId])
> VALUES (@p5, @p6, @p7, @p8, @p9);
> SELECT [StudentAddressId]
> FROM [StudentAddresses]
> WHERE @@ROWCOUNT = 1 AND [StudentAddressId] = scope_identity();
> ',N'@p5 nvarchar(4000),@p6 nvarchar(4000),@p7 nvarchar(4000),@p8 nvarchar(4000),
> @p9 int',@p5=NULL,@p6=N'SFO',@p7=N'USA',@p8=N'CA',@p9=1
> Go
> '''

there is some additional topics related to inserting data in a disconnected scenario, such as:
# 1. Insert Multiple Records
# 2. Insert Data Using DbSet
i don't want study them now, but i will study them later when i need them. 
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [13]  Update — Disconnected Scenario
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 13. Update Data in Disconnected Scenario in Entity Framework Core

/*
EF Core API builds and executes UPDATE statement in the database for the entities whose EntityState is Modified.
In the connected scenario, the DbContext keeps track of all entities so it knows which are modified and hence automatically sets EntityState to Modified.

In the disconnected scenario such as in a web application, the DbContext is not aware of the entities because entities were modified out of the scope of the current DbContext instance.
So, first, we need to attach the disconnected entities to a DbContext instance with Modified EntityState.
The following table lists the DbContext and DbSet methods to update entities:
┏━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ DbContext Method     ┃ DbSet Method             ┃ Description / State Behavior                              ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Update               ┃ Update                   ┃ Attaches entity with Modified state. Triggers SQL UPDATE. ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ UpdateRange          ┃ UpdateRange              ┃ Attaches multiple entities with Modified state in one go. ┃
┗━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛

*/
//> The following example demonstrates updating a disconnected entity.
// Disconnected Student entity
var stud = new Student(){ StudentId = 1, Name = "Bill" };

stud.Name = "Steve"; 

using (var context = new SchoolContext())
{
    context.Update<Student>(stud);
    // or the following are also valid
    // context.Students.Update(stud);
    // context.Attach<Student>(stud).State = EntityState.Modified;
    // context.Entry<Student>(stud).State = EntityState.Modified; 
    context.SaveChanges(); 
}
/*
In the above example, consider the stud is an existing Student entity object because it has a valid Key property value (StudentId = 1).
Entity Framework Core introduced the DbContext.Update() method which attaches the specified entity to a context and sets its EntityState to Modified.
Alternatively, you can also use the DbSet.Update() method (context.Students.Update(stud)) to do the same thing.
The above example executes the following UPDATE statement in the database.
> '''
> exec sp_executesql N'SET NOCOUNT ON;
> UPDATE [Students] SET [Name] = @p0
> WHERE [StudentId] = @p1;
> SELECT @@ROWCOUNT;
> ',N'@p1 int,@p0 nvarchar(4000)',@p1=1,@p0=N'Steve'
> go
> '''

The Update method sets the EntityState based on the value of the key property.
If the root or child entity's key property is empty, null or default value of the specified data type then the Update() method considers it a new entity and sets 
its EntityState to Added in Entity Framework Core 
*/
public static void Main()
{
    var newStudent = new Student()
    {
        Name = "Bill"
    };

    var modifiedStudent = new Student()
    {
        StudentId = 1,
        Name = "Steve"
    };

    using (var context = new SchoolContext())
    {
        context.Update<Student>(newStudent);
        context.Update<Student>(modifiedStudent);

        DisplayStates(context.ChangeTracker.Entries());
    }
}
private static void DisplayStates(IEnumerable<EntityEntry> entries)
{
    foreach (var entry in entries)
    {
        Console.WriteLine($"Entity: {entry.Entity.GetType().Name},
                State: {entry.State.ToString()} ");
    }
}
/*
In the above example, newStudent does not have a Key property value (StudentId).
So, the Update() method will mark it as Added, whereas modifiedStudent has a value, so it will be marked as Modified.

*/
//! Exception:
// The Update and UpdateRange methods throw an InvalidOperationException if an instance of DbContext is already tracking an entity with the same key property value.
// Consider the following example:
var student = new Student()
{
    StudentId = 1,
    Name = "Steve"
};
using (var context = new SchoolContext())
{
    // loads entity in a context whose StudentId is 1
    context.Students.First<Student>(s => s.StudentId == 1); 
    // throws an exception as it is already tracking entity with StudentId=1
    context.Update<Student>(student); 
    context.SaveChanges();
}
/*
In the above example, a context object loads the Student entity whose StudentId is 1 and starts tracking it.
So, attaching an entity with the same key value will throw the following exception:
! The instance of entity type 'Student' cannot be tracked because another instance with the same key value for {'StudentId'} is already being tracked.
! When attaching existing entities, ensure that only one entity instance with a given key value is attached. 
! Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [14]  Delete — Disconnected Scenario
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 15. Delete Data in Disconnected Scenario in Entity Framework Core
/*
EF Core API builds and executes a DELETE statement in the database for the entities whose EntityState is Deleted.
There is no difference in deleting an entity in a connected or a disconnected scenario in EF Core.
EF Core made it easy to delete an entity from a context, which in turn will delete a record in the database using the following methods.
┏━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ DbContext Method     ┃ DbSet Method             ┃ Description / State Behavior                              ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Remove               ┃ Remove                   ┃ Attaches entity with Deleted state. Triggers SQL DELETE.  ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ RemoveRange          ┃ RemoveRange              ┃ Attaches multiple entities with Deleted state in one go.  ┃
┗━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛

*/
//> The following example demonstrates the different ways of deleting an entity in the disconnected scenario.
// entity to be deleted
var student = new Student() {
        StudentId = 1
};

using (var context = new SchoolContext()) 
{
    context.Remove<Student>(student);
    // or the following are also valid
    // context.RemoveRange(student);
    //context.Students.Remove(student);
    //context.Students.RemoveRange(student);
    //context.Attach<Student>(student).State = EntityState.Deleted;
    //context.Entry<Student>(student).State = EntityState.Deleted;
    
    context.SaveChanges();
}
/*
In the above example, a Student entity with a valid StudentId is removed from a context using the Remove() or RemoveRange() method.
The data will be deleted from the database on SaveChanges(). The above example executes the following delete command in the database:
> '''
> exec sp_executesql N'SET NOCOUNT ON;
> DELETE FROM [Students]
> WHERE [StudentId] = @p0;
> SELECT @@ROWCOUNT;
> ',N'@p0 int',@p0=1
> go
> '''
Note: The DbContext.Remove() and DbContext.RemoveRange() methods are newly introduced in EF Core to make the delete operation easy.
! if the entity to be deleted does not exist in the database, then the above code will not throw an exception. It will execute the DELETE statement and return 0 rows affected.
> some additional topics related to deleting data in a disconnected scenario are:
# 1. Delete Multiple Records
# 2. Delete Data Using DbSet 
*/



//────────────────────────────────────────────────────────────────────────────────────────────────────
//  ▌ CONVENTIONS & CONFIG
//────────────────────────────────────────────────────────────────────────────────────────────────────

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [15]  EF Core Conventions
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 15. Conventions in Entity Framework Core
/*
Conventions are default rules using which Entity Framework builds a model based on your domain (entity) classes.
In the First EF Core Application chapter, EF Core API creates a database schema based on domain and context classes, without any additional configurations because
domain classes were following the conventions.
*/
//> Consider the following sample entities and context class to understand the default conventions.
public class Student // dependant entity
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte[] Photo { get; set; }
    public decimal Height { get; set; }
    public float Weight { get; set; }

    public int GradeId { get; set; } // foreign key property
    public Grade Grade { get; set; } // reference navigation property
}

public class Grade // principal entity
{
    public int Id { get; set; } // primary key property
    public string GradeName { get; set; }
    public string Section { get; set; }

    public IList<Student> Students { get; set; }
}

public class SchoolContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
    }

    public DbSet<Student> Students { get; set; }
}
//> Let's understand the EF Core conventions and how EF Core API will create a database for the above entities.
//> 15.1 Schema
// EF Core will create all the database objects in the dbo schema by default.
//> 15.2 Table
/*
# EF Core will create database tables for all DbSet<TEntity> properties in a context class with the same name as the property.
# It will also create tables for entities which are not included as DbSet properties but are reachable through reference properties in other DbSet entities.
# For the above example, EF Core will create the Students table for DbSet<Student> property in the SchoolContext class and the Grade table for a Grade property in the
# Student entity class, even though the SchoolContext class does not include the DbSet<Grade> property.
*/

//> 15.3 Column
/*
EF Core will create columns for all the scalar properties of an entity class with the same name as the property, by default.
It uses the reference and collection properties in building relationships among corresponding tables in the database.
> Column Data Type
The data type for columns in the database table is depending on how the provider for the database has mapped C# data type to the data type of a selected database.
The following table lists mapping between C# data type to SQL Server column data type.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ C# Type       ┃ SQL Server Type              ┃ Notes                                    ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ int           ┃ int                          ┃ Standard 32-bit integer.                 ┃
┃ string        ┃ nvarchar(Max)                ┃ Default. Use [MaxLength] to optimize.    ┃
┃ decimal       ┃ decimal(18,2)                ┃ Precision can be customized.             ┃
┃ float         ┃ real                         ┃ 7-digit precision.                       ┃
┃ byte[]        ┃ varbinary(Max)               ┃ Binary data storage.                     ┃
┃ DateTime      ┃ datetime                     ┃ Maps to datetime2 in modern EF versions. ┃
┃ bool          ┃ bit                          ┃ 0 or 1 logic.                            ┃
┃ byte          ┃ tinyint                      ┃ Unsigned 8-bit.                          ┃
┃ short         ┃ smallint                     ┃ 16-bit signed.                           ┃
┃ long          ┃ bigint                       ┃ 64-bit signed.                           ┃
┃ double        ┃ float                        ┃ 15-digit precision.                      ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> Nullable Column
EF Core creates null columns for all reference data type and nullable primitive type properties e.g. string, Nullable<int>, decimal?.
> NotNull Column
EF Core creates NotNull columns in the database for all primary key properties, and primitive type properties e.g. int, float, decimal, DateTime etc..
> Primary Key
EF Core will create the primary key column for the property named Id or <Entity Class Name>Id (case insensitive).
For example, EF Core will create a column as Primary Key in the Students table if the Student class includes a property named:
id,
1. ID
2. iD
3. Id
4. studentid
5. StudentId
6. STUDENTID
7. sTUdentID

> Foreign Key
As per the foreign key convention, EF Core API will create a foreign key column for each reference navigation property in an entity with one of the following naming patterns.
> <Reference Navigation Property Name>Id
> <Reference Navigation Property Name><Principal Primary Key Property Name>
In our example (Student and Grade entities), EF Core will create a foreign key column GradeId in the Students table, as depicted in the following figure.
The following table lists foreign key column names for different reference property names and primary key property names.
┏━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━┓
┃ Navigation Property  ┃ FK Property (C#)  ┃ Principal PK Name ┃ Resulting DB Column ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━┫
┃ Grade                ┃ GradeId           ┃ GradeId           ┃ GradeId             ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━┫
┃ Grade                ┃ GradeId           ┃ Id                ┃ GradeId             ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━┫
┃ CurrentGrade         ┃ CurrentGradeId    ┃ GradeId           ┃ CurrentGradeId      ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━┫
┃ CurrentGrade         ┃ CurrentGradeId    ┃ Id                ┃ CurrentGradeId      ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━┫
┃ CurrentGrade         ┃ GradeId           ┃ Id                ┃ GradeId             ┃
┗━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━┛
> Index
EF Core creates a clustered index on the Primary Key columns and a non-clustered index on the Foreign Key columns, by default.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [16]  One-to-Many Conventions
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 16. One-to-Many Relationship Conventions in Entity Framework Core
/*
Entity Framework Core follows the same convention as Entity Framework 6.x conventions for one-to-many relationship.
The only difference is that EF Core creates a foreign key column with the same name as navigation property name and not as <NavigationPropertyName>_<PrimaryKeyPropertyName>
*/
//> Let's look at the different conventions which automatically configure a one-to-many relationship between the following Student and Grade entities.
public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
}
public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }
}

//> Convention 1
/*
We want to establish a one-to-many relationship where many students are associated with one grade.
This can be achieved by including a reference navigation property in the dependent entity as shown below.
(here, the Student entity is the dependent entity and the Grade entity is the principal entity).
*/
public class Student // dependant entity
{
    public int Id { get; set; } // primary key property
    public string Name { get; set; }
    public Grade Grade { get; set; } // reference navigation property
}

public class Grade // principal entity
{
    public int GradeId { get; set; } // primary key property
    public string GradeName { get; set; }
    public string Section { get; set; }
}

/*
In the example above, the Student entity class includes a reference navigation property of Grade type.
This allows us to link the same Grade to many different Student entities, which creates a one-to-many relation en them.
This will produce a one-to-many relationship between the Students and Grades tables in the database, where Students table includes a nullable foreign key GradeId, as shown below.
EF Core will create a shadow property for the foreign key named GradeId in the conceptual model, which will be mapped to the GradeId foreign key column in the Students table.
!!! important note 
! Note: The reference property Grade is nullable, so it creates a nullable ForeignKey GradeId in the Students table. You can configure non null foreign keys using fluent API.
*/


//> Convention 2
//> Another convention is to include a collection navigation property in the principal entity as shown below.
public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
}
public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }
    public ICollection<Student> Students { get; set; } 
}
/*
In the example above, the Grade entity includes a collection navigation property of type ICollection<Student>.
This will allow us to add multiple Student entities to a Grade entity, which results in a one-to-many relationship between the Students and Grades tables in the database,
just like in convention 1.
*/

//> Convention 3
//> Another EF convention for the one-to-many relationship is to include navigation property at both ends, which will also result in a one-to-many relationship (convention 1 + convention 2).
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Grade Grade { get; set; }
}

public class Grade
{
    public int GradeID { get; set; }
    public string GradeName { get; set; }
    
    public ICollection<Student> Students { get; set; }
}

/*
In the example above, the Student entity includes a reference navigation property of Grade type and the Grade entity class includes a collection navigation property 
ICollection<Student>, which results in a one-to-many relationship between corresponding database tables Students and Grades, same as in convention 1.
*/


//> Convention 4
//> Defining the relationship fully at both ends with the foreign key property in the dependent entity creates a one-to-many relationships.

public class Student // dependant entity
{ 
    public int Id { get; set; } // primary key property
    public string Name { get; set; }
    public int GradeId { get; set; } // foreign key property
    public Grade Grade { get; set; } // reference navigation property
}
public class Grade // principal entity
{
    public int GradeId { get; set; } // primary key property
    public string GradeName { get; set; }
    public ICollection<Student> Students { get; set; } // collection navigation property
}

/*
In the above example, the Student entity includes a foreign key property GradeId of type int and its reference navigation property Grade.
At the other end, the Grade entity also includes a collection navigation property ICollection<Student>.
This will create a one-to-many relationship with the non null foreign key column in the Students table.
*/
//!!!! thats an important note about the foreign key property GradeId in the above example. It is of type int, which is a non-nullable value type in C#. 
//!!!! If you want to make the foreign key GradeId as nullable, then use nullable int data type (Nullable<int> or int?), as shown below.

/*
Therefore, these are the conventions which automatically create a one-to-many relationships in the corresponding database tables. 
If entities do not follow the above conventions, then you can use Fluent API to configure the one-to-many relationships.
*/

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [17]  One-to-One Conventions
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 17. One-to-One Relationship Conventions in Entity Framework Core
//> Entity Framework Core introduced default conventions which automatically configure a One-to-One relationship between two entities

//> In EF Core, a one-to-one relationship requires a reference navigation property at both sides.
//> The following Student and StudentAddress entities follow the convention for the one-to-one relationship.


public class Student // principal entity
{
    public int Id { get; set; } // primary key property
    public string Name { get; set; }
    public int StudentAddressId { get; set; } // foreign key property
    public StudentAddress Address { get; set; } // reference navigation property
}

public class StudentAddress // principal entity
{
    public int StudentAddressId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public int StudentId { get; set; } // foreign key property
    public Student Student { get; set; } // reference navigation property
}
/*
In the example above, the Student entity includes a reference navigation property of type StudentAddress and the StudentAddress entity includes a 
foreign key property StudentId and its corresponding reference property Student.
This will result in a one-to-one relationship in corresponding tables Students and StudentAddresses in the database.
EF Core creates a unique index on the NotNull foreign key column StudentId in the StudentAddresses table, as shown above.
This ensures that the value of the foreign key column StudentId must be unique in the StudentAddress table, which is necessary for a one-to-one relationship.
*/
//! ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
//! ┃ Use Fluent API to configure one-to-one relationships if entities do not follow the conventions. ┃
//! ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [18]  Configurations (Data Annotations & Fluent API)
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 18. Configurations in Entity Framework Core
/*
You learned about default Conventions in EF Core in the previous chapter. Many times we want to customize the entity to table mapping and do not want to follow default conventions.
EF Core allows us to configure domain classes in order to customize the EF model to database mappings.
This programming pattern is referred to as Convention over Configuration.
There are two ways to configure domain classes in EF Core.
> 1. By using Data Annotation Attributes
> 2. By using Fluent API
*/

//> Data Annotation Attributes
/*
Data Annotations are a simple attribute-based configuration method where different .NET attributes can be applied to domain classes and properties to configure the model.
Data annotation attributes are not dedicated to Entity Framework, as they are also used in ASP.NET MVC. This is why these attributes are included in a separate namespace
!! System.ComponentModel.DataAnnotations.
*/
//> The following example demonstrates how the data annotation attributes can be applied to a domain class and properties to override conventions.
[Table("StudentInfo")]
public class Student
{
    public Student() { }
    [Key]
    public int SID { get; set; }
    [Column("Name", TypeName="ntext")]
    [MaxLength(20)]
    public string StudentName { get; set; }
    [NotMapped]
    public int? Age { get; set; }
    public int StdId { get; set; }
    [ForeignKey("StdId")]
    public virtual Standard Standard { get; set; }
}
//> Data annotation attributes
/*
! Data Annotation attributes are .NET attributes which can be applied on an entity class or properties to override default conventions in EF 6 and EF Core.
Data Annotation attributes are included in the System.ComponentModel.DataAnnotations and System.ComponentModel.DataAnnotations.Schema namespaces in EF 6 as well as in EF Core.
These attributes are not only used in Entity Framework but they can also be used with ASP.NET MVC or data controls.
These data annotation attributes work in the same way in EF 6 and EF Core and are valid in both.
# Note: Data annotations only give you a subset of configuration options.
# Fluent API provides a full set of configuration options available in Code-First.
> System.ComponentModel.DataAnnotations Attributes
┏━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Attribute           ┃ Description / Database Impact                        ┃
┣━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ [Key]               ┃ Defines the Primary Key column.                      ┃
┃ [Timestamp]         ┃ Defines a row version column for tracking changes.   ┃
┃ [ConcurrencyCheck]  ┃ Includes column in optimistic concurrency checks.    ┃
┃ [Required]          ┃ Sets the database column to NOT NULL.                ┃
┃ [MinLength]         ┃ Validates minimum string length (Application level). ┃
┃ [MaxLength]         ┃ Sets maximum column size (Database level).           ┃
┃ [StringLength]      ┃ Sets maximum (and optionally minimum) string length. ┃
┗━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> System.ComponentModel.DataAnnotations.Schema Attributes
┏━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Attribute            ┃ Description / Database Impact                                   ┃
┣━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ [Table]              ┃ Sets custom Table Name and Schema.                              ┃
┃ [Column]             ┃ Sets custom Column Name, Order, and Data Type.                  ┃
┃ [Index]              ┃ Creates a database index on the property.                       ┃
┃ [ForeignKey]         ┃ Manually specifies the FK property for a relationship.          ┃
┃ [NotMapped]          ┃ Prevents the property from being created as a column in the DB. ┃
┃ [DatabaseGenerated]  ┃ Configures Identity, Computed, or None for value generation.    ┃
┃ [InverseProperty]    ┃ Links two ends of a relationship when naming is ambiguous.      ┃
┗━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
/*
! the important thing to know now is the fluent API:
> Fluent API
Another way to configure domain classes is by using Entity Framework Fluent API.
EF Fluent API is based on a Fluent API design pattern (a.k.a Fluent Interface) where the result is formulated by method chaining.

! In Entity Framework Core, the ModelBuilder class acts as a Fluent API. 
> By using it, we can configure many different things, as it provides more configuration options than data annotation attributes.
Entity Framework Core Fluent API configures the following aspects of a model:
> 1. Model Configuration: 
    • Configures an EF model to database mappings.
    • Configures the default Schema, DB functions, additional data annotation attributes and entities to be excluded from mapping.
> 2. Entity Configuration:
    • Configures entity to table and relationships mapping e.g. PrimaryKey, AlternateKey, Index, table name, one-to-one, one-to-many, many-to-many relationships etc.
> 3. Property Configuration: 
Configures property to column mapping e.g. column name, default value, nullability, foreign key, data type, concurrency column etc.
The following table lists important methods for each type of configuration.
┏━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Configurations ┃ Fluent API Method            ┃ Usage / Description                                                                                                                                                     ┃
┣━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Model (Global) ┃ HasDbFunction()              ┃ Configures a database function when targeting a relational database.                                                                                                    ┃  
┃                ┃ HasDefaultSchema()           ┃ Specifies the database schema.                                                                                                                                          ┃  
┃                ┃ HasAnnotation()              ┃ Adds or updates data annotation attributes on the entity.                                                                                                               ┃  
┃                ┃ HasSequence()                ┃ Configures a database sequence when targeting a relational database.                                                                                                    ┃  
┣━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Entity (Table) ┃ HasAlternateKey()            ┃ Configures an alternate key in the EF model for the entity.                                                                                                             ┃
┃                ┃ HasIndex()                   ┃ Configures an index on the specified properties.                                                                                                                        ┃  
┃                ┃ HasKey()                     ┃ Configures the property or list of properties as Primary Key.                                                                                                           ┃
┃                ┃ HasMany()                    ┃ Configures the Many part of the relationship, where an entity contains the reference collection property of another type for one-to-Many or many-to-many relationships. ┃
┃                ┃ HasOne()                     ┃ Configures the One part of the relationship, where an entity contains the reference property of another type for one-to-one or one-to-many relationships.               ┃
┃                ┃ Ignore()                     ┃ Configures that the class or property should not be mapped to a table or column.                                                                                        ┃
┃                ┃ OwnsOne()                    ┃ Configures a relationship where the target entity is owned by this entity. The target entity key value is propagated from the entity it belongs to.                     ┃
┃                ┃ ToTable()                    ┃ Configures the database table that the entity maps to.                                                                                                                  ┃
┣━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Property       ┃ HasColumnName()              ┃ Configures the corresponding column name in the database for the property.                                                                                              ┃
┃ (Column)       ┃ HasColumnType()              ┃ Configures the data type of the corresponding column in the database for the property.                                                                                  ┃
┃                ┃ HasComputedColumnSql()       ┃ Configures the property to map to a computed column in the database when targeting a relational database.                                                               ┃          
┃                ┃ HasDefaultValue()            ┃ Configures the default value for the column that the property maps to when targeting a relational database..                                                            ┃          
┃                ┃ HasDefaultValueSql()         ┃ Configures the default value expression for the column that the property maps to when targeting relational database.                                                    ┃                  
┃                ┃ HasField()                   ┃ Specifies the backing field to be used with a property.                                                                                                                 ┃
┃                ┃ HasMaxLength()               ┃ Configures the maximum length of data that can be stored in a property.                                                                                                 ┃
┃                ┃ IsConcurrencyToken()         ┃ Configures the property to be used as an optimistic concurrency token.                                                                                                  ┃
┃                ┃ IsRequired()                 ┃ Configures whether the valid value of the property is required or whether null is a valid value.                                                                        ┃      
┃                ┃ IsRowVersion()               ┃ Configures the property to be used in optimistic concurrency detection.                                                                                                 ┃
┃                ┃ IsUnicode()                  ┃ Configures the string property which can contain Unicode characters or not.                                                                                             ┃
┃                ┃ ValueGeneratedNever()        ┃ Configures a property which cannot have a generated value when an entity is saved.                                                                                      ┃
┃                ┃ ValueGeneratedOnAdd()        ┃ Configures that the property has a generated value when saving a new entity.                                                                                            ┃
┃                ┃ ValueGeneratedOnAddOrUpdate()┃ Configures that the property has a generated value when saving a new or existing entity.                                                                                ┃
┃                ┃ ValueGeneratedOnUpdate()     ┃ Configures that a property has a generated value when saving an existing entity.                                                                                        ┃
┗━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
>simpler version of the above table:
┏━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Configurations ┃ Fluent API Method            ┃ Usage / Description                                                ┃
┣━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Model (Global) ┃ HasDbFunction()              ┃ Configures a database function when targeting a relational db.     ┃
┃                ┃ HasDefaultSchema()           ┃ Specifies the database schema.                                     ┃
┃                ┃ HasAnnotation()              ┃ Adds or updates data annotation attributes on the entity.          ┃
┃                ┃ HasSequence()                ┃ Configures a database sequence when targeting a relational db.     ┃
┣━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Entity (Table) ┃ HasAlternateKey()            ┃ Configures an alternate key in the EF model for the entity.        ┃
┃                ┃ HasIndex()                   ┃ Configures an index on the specified properties.                   ┃
┃                ┃ HasKey()                     ┃ Configures the property or list of properties as Primary Key.      ┃
┃                ┃ HasMany()                    ┃ Configures the Many part of the relationship (1:N or N:N).         ┃
┃                ┃ HasOne()                     ┃ Configures the One part of the relationship (1:1 or 1:N).          ┃
┃                ┃ Ignore()                     ┃ Configures that the class or property should not be mapped.        ┃
┃                ┃ OwnsOne()                    ┃ Configures a relationship where the target entity is owned.        ┃
┃                ┃ ToTable()                    ┃ Configures the database table that the entity maps to.             ┃
┣━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Property       ┃ HasColumnName()              ┃ Configures the corresponding column name in the database.          ┃
┃ (Column)       ┃ HasColumnType()              ┃ Configures the data type of the corresponding column.              ┃
┃                ┃ HasComputedColumnSql()       ┃ Configures the property to map to a computed column.               ┃
┃                ┃ HasDefaultValue()            ┃ Configures the default value for the column.                       ┃
┃                ┃ HasDefaultValueSql()         ┃ Configures the default value expression (SQL) for the column.      ┃
┃                ┃ HasField()                   ┃ Specifies the backing field to be used with a property.            ┃
┃                ┃ HasMaxLength()               ┃ Configures the maximum length of data stored in a property.        ┃
┃                ┃ IsConcurrencyToken()         ┃ Configures the property as an optimistic concurrency token.        ┃
┃                ┃ IsRequired()                 ┃ Configures whether the value is required (NotNull).                ┃
┃                ┃ IsRowVersion()               ┃ Configures property for optimistic concurrency detection.          ┃
┃                ┃ IsUnicode()                  ┃ Configures if the string property can contain Unicode.             ┃
┃                ┃ ValueGeneratedNever()        ┃ Configures a property which cannot have a generated value.         ┃
┃                ┃ ValueGeneratedOnAdd()        ┃ Generated value when saving a new entity.                          ┃
┃                ┃ ValueGeneratedOnAddOrUpdate()┃ Generated value when saving a new or existing entity.              ┃
┃                ┃ ValueGeneratedOnUpdate()     ┃ Generated value when saving an existing entity.                    ┃
┗━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ 
*/
//> Fluent API Configurations
//> Override the OnModelCreating method and use a parameter modelBuilder of type ModelBuilder to configure domain classes, as shown below.
public class SchoolDBContext: DbContext 
{
    public DbSet<Student> Students { get; set; }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Write Fluent API configurations here

        //Property Configurations
        modelBuilder.Entity<Student>()
                .Property(s => s.StudentId) // specify the property to be configured
                .HasColumnName("Id") // configure the column name in the database for the property
                .HasDefaultValue(0) // configure the default value for the column that the property maps to when targeting a relational database
                .IsRequired(); // configure whether the valid value of the property is required or whether null is a valid value
    }
}

/*
> In the above example, the ModelBuilder Fluent API instance is used to configure a property by calling multiple methods in a chain.
> It configures the StudentId property of the Student entity;
> it configures the name using HasColumnName, the default value using HasDefaultValue and nullability using IsRequired method in a single statement instead of multiple statements.
> This increases the readability and also takes less time to write compared to multiple statements, as shown below.
*/
//Fluent API method chained calls
modelBuilder.Entity<Student>()
        .Property(s => s.StudentId)
        .HasColumnName("Id")
        .HasDefaultValue(0)
        .IsRequired();

//Separate method calls
modelBuilder.Entity<Student>().Property(s => s.StudentId).HasColumnName("Id");
modelBuilder.Entity<Student>().Property(s => s.StudentId).HasDefaultValue(0);
modelBuilder.Entity<Student>().Property(s => s.StudentId).IsRequired();

//! Note: Fluent API configurations have higher precedence than data annotation attributes.


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [19]  Fluent API — One-to-Many
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 19. Configure One-to-Many Relationships using Fluent API in Entity Framework Core
/*
You learned about the Conventions for One-to-Many Relationship.
Generally, you don't need to configure one-to-many relationships because EF Core includes enough conventions which will automatically configure them.
However, you can use Fluent API to configure the one-to-many relationship if you decide to have all the EF configurations in Fluent API for easy maintenance.
Entity Framework Core made it easy to configure relationships using Fluent API.
*/

//> Consider the following Student and Grade classes where the Grade entity includes many Student entities.

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrentGradeId { get; set; }
    public Grade Grade { get; set; }
}

public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }
    public ICollection<Student> Students { get; set; }
}
//> Configure the one-to-many relationship for the above entities using Fluent API by overriding the OnModelCreating method in the context class

public class SchoolContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EFCore-SchoolDB;Trusted_Connection=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne<Grade>(s => s.Grade)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.CurrentGradeId);
    }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Student> Students { get; set; }
}

//>  In the example above, the following code snippet configures the one-to-many relationship:
modelBuilder.Entity<Student>() // specify the entity to be configured
    .HasOne<Grade>(s => s.Grade) // configure the reference navigation property of the dependent entity (Student) which points to the principal entity (Grade)
    .WithMany(g => g.Students) // configure the collection navigation property of the principal entity (Grade) which points to the dependent entity (Student)
    .HasForeignKey(s => s.CurrentGradeId); // configure the foreign key property in the dependent entity (Student) which will be used for the relationship
//> Now, to reflect this in the database, execute migration commands, add-migration <name> and update-database.
//> The database will include two tables with One-to-Many relationship as shown below.

/*
# Let's understand the above code step by step.
1. we need to start configuring with one entity class, either Student or Grade. So, modelBuilder.Entity<Student>() starts with the Student entity.
2. Then .HasOne<Grade>(s => s.Grade) specifies that the Student entity includes a Grade type property named Grade.
3. Now, we need to configure the other end of the relationship, the Grade entity. 
    The .WithMany(g => g.Students) specifies that the Grade entity class includes many Student entities. Here, WithMany infers collection navigation property.
4. The .HasForeignKey<int>(s => s.CurrentGradeId); specifies the name of the foreign key property CurrentGradeId. 
    This is optional. Use it only when you have the foreign key Id property in the dependent class.

> lets draw the illustration of the above code snippet to understand it better. 

   ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
   ┃                            ┃
   ┃     modelBuilder.Entity<Student>()
   ┃         .HasOne<Grade>(s => s.Grade) ━━━━━━━━━━━━━━━━━┓
   ┃         .WithMany(g => g.Students) ━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━┓                             
   ┃         .HasForeignKey(s => s.CurrentGradeId); ━━━━━┓ ┃              ┃    
   ┃                                                     ┃ ┃              ┃
   ┗━━━━━━━━━━━━┓                                        ┃ ┃              ┃     
                ┃                                        ┃ ┃              ┃ 
                ┃                                        ┃ ┃              ┃     
                ▼                                        ┃ ┃              ┃ 
public class Student                                     ┃ ┃              ┃ 
{                                                        ┃ ┃              ┃ 
    public int Id { get; set; }                          ┃ ┃              ┃         
    public string Name { get; set; }                     ┃ ┃              ┃
    public int CurrentGradeId { get; set; } <━━━━━━━━━━━━┛ ┃              ┃                                    
    public Grade Grade { get; set; } <━━━━━━━━━━━━━━━━━━━━━┛              ┃
}                                                                         ┃
public class Grade                                                        ┃
{                                                                         ┃
    public int GradeId { get; set; }                                      ┃
    public string GradeName { get; set; }                                 ┃
    public string Section { get; set; }                                   ┃
    public ICollection<Student> Students { get; set; } <━━━━━━━━━━━━━━━━━━┛
}

Alternatively, you can start configuring the relationship with the Grade entity instead of the Student entity, as shown below.
modelBuilder.Entity<Grade>()
    .HasMany<Student>(g => g.Students)
    .WithOne(s => s.Grade)
    .HasForeignKey(s => s.CurrentGradeId);

! very important note about method chaining in Fluent API:

# in simple words the method chaining will work on the name it self for example here:
> 1-  modelBuilder.Entity<Grade>() - starts with the Grade entity
> 2- .HasMany<Student>(g => g.Students) - we are in the Grade entity and we are saying that the Grade entity has many Student entities
> 3- .WithOne(s => s.Grade) - now we are in the Student entity and we are saying that the Student entity has one Grade entity
> 4- .HasForeignKey(s => s.CurrentGradeId); - we are still in the Student entity and we are saying that the foreign key property in the Student entity is CurrentGradeId.
*/

//> Configure Cascade Delete using Fluent API
/*
Cascade delete automatically deletes the child row when the related parent row is deleted.
For example, if a Grade is deleted, then all the Students in that grade should also be deleted from the database automatically.
Use the OnDelete method to configure the cascade delete between Student and Grade entities, as shown below.
*/
modelBuilder.Entity<Grade>()
    .HasMany<Student>(g => g.Students)
    .WithOne(s => s.Grade)
    .HasForeignKey(s => s.CurrentGradeId)
    .OnDelete(DeleteBehavior.Cascade);

/*
The OnDelete() method cascade delete behavior uses the DeleteBehavior parameter. You can specify any of the following DeleteBehavior values, based on your requirement.
> 1. Cascade : Dependent entities will be deleted when the principal entity is deleted.
> 2. ClientSetNull: The values of foreign key properties in the dependent entities will be set to null.
> 3. Restrict: Prevents Cascade delete.
> 4. SetNull: The values of foreign key properties in the dependent entities will be set to null.
*/



//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [20]  Fluent API — One-to-One
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 20. Configure One-to-One Relationships using Fluent API in Entity Framework Core
/*
Here you will learn how to configure one-to-one relationships between two entities using Fluent API, if they do not follow EF Core conventions.
Generally, you don't need to configure one-to-one relationships manually because EF Core includes Conventions for One-to-One Relationships.
However, if the key or foreign key properties do not follow the convention, then you can use data annotation attributes or Fluent API to configure a one-to-one 
relationship between the two entities.
Let's configure a one-to-one relationship between the following Student and StudentAddress entities, which do not follow the foreign key convention.
*/

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StudentAddress Address { get; set; }
}
public class StudentAddress
{
    public int StudentAddressId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public int AddressOfStudentId { get; set; }
    public Student Student { get; set; }
}

//! To configure a one-to-one relationship using Fluent API in EF Core, use the HasOne, WithOne and HasForeignKey methods, as shown below.
public class SchoolContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EFCore-SchoolDB;Trusted_Connection=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne<StudentAddress>(s => s.Address)
            .WithOne(ad => ad.Student)
            .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentAddress> StudentAddresses { get; set; }
}
/*
In the above example, the following code snippet configures the one-to-one relationship.

modelBuilder.Entity<Student>()
    .HasOne<StudentAddress>(s => s.Address)
    .WithOne(ad => ad.Student)
    .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);

> Let's understand it step by step.
1. modelBuilder.Entity<Student>() starts configuring the Student entity.
2. The .HasOne<StudentAddress>(s => s.Address) method specifies that the Student entity includes one StudentAddress reference property using a lambda expression.
3. .WithOne(ad => ad.Student) configures the other end of the relationship, the StudentAddress entity.
    It specifies that the StudentAddress entity includes a reference navigation property of Student type.
4. .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId) specifies the foreign key property name.
Now, to reflect this in the database, execute migration commands, add-migration <name> and update-database. 
The database will include two tables with one-to-one relationship as shown below.

The following figure illustrates the Fluent API configuration for a one-to-one relationship.

   ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
   ┃                            ┃
   ┃     modelBuilder.Entity<Student>()
   ┃         .HasOne<StudentAddress>(s => s.Address) ━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
   ┃         .WithOne(ad => ad.Student) ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━┓                             
   ┃         .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId); ━━━━┓ ┃  ┃    
   ┃                                                                          ┃ ┃  ┃
   ┗━━━━━━━━━━━━┓                                                             ┃ ┃  ┃     
                ┃                                                             ┃ ┃  ┃ 
                ┃                                                             ┃ ┃  ┃     
                ▼                                                             ┃ ┃  ┃ 
public class Student                                                          ┃ ┃  ┃ 
{                                                                             ┃ ┃  ┃ 
    public int Id { get; set; }                                               ┃ ┃  ┃         
    public string Name { get; set; }                                          ┃ ┃  ┃       
    public StudentAddress Address { get; set; } <━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━┛  ┃
}                                                                             ┃    ┃
public class StudentAddress                                                   ┃    ┃
{                                                                             ┃    ┃
    public int StudentAddressId { get; set; }                                 ┃    ┃ 
    public string Address { get; set; }                                       ┃    ┃     
    public string City { get; set; }                                          ┃    ┃ 
    public string State { get; set; }                                         ┃    ┃ 
    public string Country { get; set; }                                       ┃    ┃     
    public int AddressOfStudentId { get; set; } <━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛    ┃             
    public Student Student { get; set; } <━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
}
You can start configuring with the StudentAddress entity in the same way, as below.
modelBuilder.Entity<StudentAddress>()
    .HasOne<Student>(ad => ad.Student)
    .WithOne(s => s.Address)
    .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);
//! Thus, you can configure a one-to-one relationship in Entity Framework Core.
*/

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [21]  Fluent API — Many-to-Many
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 21. Configure Many-to-Many Relationships in Entity Framework Core
/*
Here you will learn how to configure many-to-many relationships between two entities using Fluent API in Entity Framework Core.
Let's implement a many-to-many relationship between the following Student and Course entities, where one student can enroll for many courses and, 
in the same way, one course can be joined by many students.
*/

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
}
/*
The many-to-many relationship in the database is represented by a joining table which includes the foreign keys of both tables. Also, these foreign keys are composite primary keys.
! Convention
There are no default conventions available in Entity Framework Core which automatically configure a many-to-many relationship. You must configure it using Fluent API.
> Fluent API
In the Entity Framework 6.x or earlier, EF API used to create the joining table for many-to-many relationships. We do not need to create a joining entity for a joining table (however, we can of course create a joining entity explicitly in EF 6).

In Entity Framework Core, this has not been implemented yet. We must create a joining entity class for a joining table.
The joining entity for the above Student and Course entities should include a foreign key property and a reference navigation property for each entity.
> The steps for configuring many-to-many relationships would be the following:
1. Define a new joining entity class which includes the foreign key property and the reference navigation property for each entity.
2. Define a one-to-many relationship between the other two entities and the joining entity, by including a collection navigation property in entities
    at both sides (Student and Course, in this case).
3. Configure both the foreign keys in the joining entity as a composite key using Fluent API.
*/
//> So, first of all, define the joining entity StudentCourse, as shown below.

public class StudentCourse
{
    public int StudentId { get; set; } //> foreign key property for Student entity
    public Student Student { get; set; } //> reference navigation property for Student entity
    public int CourseId { get; set; } //# foreign key property for Course entity
    public Course Course { get; set; } //# reference navigation property for Course entity
}

/*
The above joining entity StudentCourse includes reference navigation properties Student and Course and their foreign key properties StudentId and CourseId respectively
(foreign key properties follow the convention).

Now, we also need to configure two separate one-to-many relationships between 
1. Student -> StudentCourse 
2. Course -> StudentCourse 
We can do it by just following the convention for one-to-many relationships, as shown below.
*/
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public IList<StudentCourse> StudentCourses { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
    public IList<StudentCourse> StudentCourses { get; set; }
}

/*
As you can see above, the Student and Course entities now include a collection navigation property of StudentCourse type. 
The StudentCourse entity already includes the foreign key property and navigation property for both, Student and Course.
This makes it a fully defined one-to-many relationship between Student & StudentCourse and Course & StudentCourse.
Now, the foreign keys must be the composite primary key in the joining table. This can only be configured using Fluent API, as below.
*/

public class SchoolContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EFCore-SchoolDB;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
}
/*
In the above code, modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId }) configures StudentId and CourseId as the composite key.
This is how you can configure many-to-many relationships if entities follow the conventions for one-to-many relationships with the joining entity.
Suppose that the foreign key property names do not follow the convention (e.g. SID instead of StudentId and CID instead of CourseId), then you can configure it using Fluent API,
as shown below.
*/
modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.SId, sc.CId });

modelBuilder.Entity<StudentCourse>()
    .HasOne<Student>(sc => sc.Student)
    .WithMany(s => s.StudentCourses)
    .HasForeignKey(sc => sc.SId);


modelBuilder.Entity<StudentCourse>()
    .HasOne<Course>(sc => sc.Course)
    .WithMany(s => s.StudentCourses)
    .HasForeignKey(sc => sc.CId);
//> Note: EF team will include a feature where we don't need to create a joining entity for many-to-many relationships in the future.



//────────────────────────────────────────────────────────────────────────────────────────────────────
//  ▌ ADVANCED
//────────────────────────────────────────────────────────────────────────────────────────────────────

//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [22]  Shadow Properties
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 22. Shadow Property in Entity Framework Core 
/*
Shadow properties are the properties that are not defined in your .NET entity class directly;
instead, you configure them for the particular entity type in the entity data model. They can be configured in the OnModelCreating() method of the context class.

shadow properties are not part of your entity class. So, you cannot access them as you access other properties of an entity.
Shadow properties can only be configured for an entity type while building an Entity Data Model and they are also mapped to a database column.
The value and state of the shadow properties are maintained purely in the Change Tracker.

Let's understand the practical aspect of the shadow property.
Assume that we need to maintain the created and updated date of each record in the database table.
You learned how to set created and modified date of entities in EF Core by defining CreatedDate and UpdatedDate properties in entity classes.
Here, we will see how to achieve the same result by using shadow properties without including them in entity classes.
*/

//> Consider the following Student entity class.
public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public decimal Height { get; set; }
    public float Weight { get; set; }
}
/*
> The above Student class does not include CreatedDate and UpdatedDate properties to maintain created or updated time.
> We will configure them as shadow properties on the Student entity.
> Defining Shadow Property
You can define the shadow properties for an entity type using the Fluent API in the OnModelCreating() using the Property() method.
The following configures two shadow properties CreatedDate and UpdatedDate on the Student entity.
*/

public class SchoolContext : DbContext
{
    public SchoolContext() : base()
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Student>().Property<DateTime>("CreatedDate");
        modelBuilder.Entity<Student>().Property<DateTime>("UpdatedDate");
    }
    public DbSet<Student> Students { get; set; }
}
/*
As you can see, the Property() method is used to configure a shadow property. Specify the name of the shadow property as a string and the type as a generic parameter.
If the name specified in the Property() method matches the name of an existing property, then the EF Core will configure that existing property as a shadow property rather 
than introducing a new shadow property.

> Shadow Properties in the Database
Once we define shadow properties, we need to update the database schema because shadow properties will be mapped to the corresponding database column.
To do this, add database migration using the following command in Package Manager Console in Visual Studio.
1. PM> add-migration <migration-name>
2. PM> update-database
Now, the Student table will include two columns, CreatedDate and UpdatedDate in SQL Server.
Thus, the database will have corresponding columns even if we haven't included these properties in the Student class and configured them as shadow properties.
*/
//> Access Shadow Property
//> You can get or set the values of the shadow properties using the Property() method of EntityEntry. The following code accesses the value of the shadow property.

using (var context = new SchoolContext())
{
    var std = new Student(){ StudentName = "Bill"  };
    
    // sets the value to the shadow property
    context.Entry(std).Property("CreatedDate").CurrentValue = DateTime.Now;

    // gets the value of the shadow property
    var createdDate = context.Entry(std).Property("CreatedDate").CurrentValue; 
}
/*
However, in our scenario, we want to set the value to these shadow properties automatically on the SaveChanges() method, so that we don't have to set them manually on 
each entity object. So, override the SaveChanges() method in the context class, as shown below.
*/
public override int SaveChanges()
{
    var entries = ChangeTracker
        .Entries()
        .Where(e =>
                e.State == EntityState.Added
                || e.State == EntityState.Modified);
    foreach (var entityEntry in entries)
    {
        entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;
        if (entityEntry.State == EntityState.Added)
        {
            entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
        }
    }
    return base.SaveChanges();
}
//! This will automatically set values to CreatedDate and UpdatedDate shadow properties.
//> Now, execute the following code and check the record in the database.
using (var context = new SchoolContext())
{
    var std = new Student(){ StudentName = "Bill"  };
    context.Add(std);
    context.SaveChanges();
}

//> Configuring Shadow Properties on All Entities
//> You can configure shadow properties on all entities at once, rather than configuring them manually for each.
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    var allEntities = modelBuilder.Model.GetEntityTypes();

    foreach (var entity in allEntities)
    {
        entity.AddProperty("CreatedDate",typeof(DateTime));
        entity.AddProperty("UpdatedDate",typeof(DateTime));
    }
}

/*
When to use shadow properties?
Shadow properties can be used in two scenarios:

1. When you don't want to expose database columns on the mapped entities, such as the scenario discussed above.
2. When you don't want to expose foreign key properties and want to manage relationships only using navigation properties. 
    The foreign key property will be a shadow property and mapped to the database column but will not be exposed as a property of an entity.
    (In EF Core, if you don't define foreign key property in entity classes then it will automatically generate shadow property for that. You don't need to configure foreign key
        property manually.)
*/



//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [23]  Disconnected Entity Graphs
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 23. Working with Disconnected Entity Graph in Entity Framework Core
/*
In the previous chapter, you learned how the ChangeTracker automatically changes the EntityState of each entity in the connected scenario.
Here, you will learn about the behaviors of different methods on the root entity and child entities of the disconnected entity graph in Entity Framework Core.

Entity Framework Core provides the following different methods, which not only attach an entity to a context, but also change the EntityState of each entity in a
disconnected entity graph:
1. Attach()
2. Entry()
3. Add()
4. Update()
5. Remove()

Let's see how the above methods change the EntityState of each entity in an entity graph in Entity Framework Core 2.x.
> 1. Attach()
The DbContext.Attach() and DbSet.Attach() methods attach the specified disconnected entity graph and start tracking it.
They return an instance of EntityEntry, which is used to assign the appropriate EntityState.
*/
//> The following example demonstrates the behavior of the DbContext.Attach() method on the EntityState of each entity in a graph.
public static void Main()
{
    var stud = new Student() { //Root entity (empty key)
        Name = "Bill",
        Address = new StudentAddress()  //Child entity (with key value)
        {
            StudentAddressId = 1,
            City = "Seattle",
            Country = "USA"
        },
        StudentCourses = new List<StudentCourse>() {
            new StudentCourse(){  Course = new Course(){ CourseName = "Machine Language" } },//Child entity (empty key)
            new StudentCourse(){  Course = new Course(){  CourseId = 2 } } //Child entity (with key value)
        }
    };

    var context = new SchoolContext();
    context.Attach(stud).State = EntityState.Added;  

    DisplayStates(context.ChangeTracker.Entries());
}

private static void DisplayStates(IEnumerable<EntityEntry> entries)
{
    foreach (var entry in entries)
    {
        Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()} ");
    }
}

//> Output:
//> Entity: Student, State: Added   => Root entity with empty key value is marked as Added
//> Entity: StudentAddress, State: Unchanged => Child entity with key value is marked as Unchanged
//> Entity: StudentCourse, State: Added => Child entity with empty key value is marked as Added
//> Entity: StudentCourse, State: Added => Child entity with empty key value is marked as Added
//> Entity: Course, State: Added
//> Entity: Course, State: Unchanged


/*
In the above example, stud is an instance of the Student entity graph which includes references of StudentAddress and StudentCourse entities.
context.Attach(stud).State = EntityState.Added attaches the stud entity graph to a context and sets Added state to it.
The Attach() method sets Added EntityState to the root entity (in this case Student) irrespective of whether it contains the Key value or not.
If a child entity contains the key value, then it will be marked as Unchanged, otherwise it will be marked as Added.
The output of the above example shows that the Student entity has Added EntityState, the child entities with non-empty key values have Unchanged EntityState and the
ones with empty key values have Added state.


The following table lists the behavior of the Attach() method when setting a different EntityState to a disconnected entity graph.
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┓
┃ Method / State Configuration         ┃ Root (with Key) ┃ Root (No Key)   ┃ Child (with Key)┃ Child (No Key)  ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ context.Attach(graph)                ┃ Unchanged       ┃ Added           ┃ Unchanged       ┃ Added           ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ .State = EntityState.Added           ┃ Added           ┃ Added           ┃ Added           ┃ Added           ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ .State = EntityState.Modified        ┃ Modified        ┃ Exception       ┃ Unchanged       ┃ Added           ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ .State = EntityState.Deleted         ┃ Deleted         ┃ Exception       ┃ Unchanged       ┃ Added           ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┛

*/


//> 2. Entry()
//> The DbContext.Entry() method behaves differently in Entity Framework Core compared with the previous EF 6.x. Consider the following example:
var student = new Student() { //Root entity (empty key)
    Name = "Bill",
    Address = new StudentAddress()  //Child entity (with key value)
    {
        StudentAddressId = 1,
        City = "Seattle",
        Country = "USA"
    },
    StudentCourses = new List<StudentCourse>() {
            new StudentCourse(){  Course = new Course(){ CourseName="Machine Language" } },//Child entity (empty key)
            new StudentCourse(){  Course = new Course(){  CourseId=2 } } //Child entity (with key value)
        }
};
var context = new SchoolContext();
context.Entry(student).State = EntityState.Modified;
DisplayStates(context.ChangeTracker.Entries());
//> Output:
//> Entity: Student, State: Modified 

/*
In the above example, context.Entry(student).State = EntityState.Modified attaches an entity to a context and applies the specified EntityState (in this case, Modified) 
to the root entity, irrespective of whether it contains a Key property value or not. It ignores all the child entities in a graph and does not attach or set their EntityState.
The following table lists different behaviors of the DbContext.Entry() method.
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━┓
┃ Method / State Configuration               ┃ Root (with Key) ┃ Root (No Key)   ┃ Child Entities ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━┫
┃ .Entry(graph).State = EntityState.Added    ┃ Added           ┃ Added           ┃ Ignored        ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━┫
┃ .Entry(graph).State = EntityState.Modified ┃ Modified        ┃ Modified        ┃ Ignored        ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━┫
┃ .Entry(graph).State = EntityState.Deleted  ┃ Deleted         ┃ Deleted         ┃ Ignored        ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━┛
*/

//> 3. Add()
/*
The DbContext.Add and DbSet.Add methods attach an entity graph to a context and set Added EntityState to a root and child entities, irrespective of whether they have key values or not.
*/
var student = new Student() { //Root entity (with key value)
    StudentId = 1,
    Name = "Bill",
    Address = new StudentAddress()  //Child entity (with key value)
    {
        StudentAddressId = 1,
        City = "Seattle",
        Country = "USA"
    },
    StudentCourses = new List<StudentCourse>() {
            new StudentCourse(){  Course = new Course(){ CourseName="Machine Language" } },//Child entity (empty key)
            new StudentCourse(){  Course = new Course(){  CourseId=2 } } //Child entity (with key value)
        }
};

var context = new SchoolContext();
context.Students.Add(student);

DisplayStates(context.ChangeTracker.Entries());
//> Output:
//> Entity: Student, State: Added
//> Entity: StudentAddress, State: Added
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Added
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Added

/*
The following table lists possible EntityState of each entity in a graph using the DbContext.Add or DbSet.Add methods.
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Method                     ┃ Root (with/out Key)      ┃ Children (with/out Key)  ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ DbContext.Add(entityGraph) ┃ Added                    ┃ Added                    ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ DbSet.Add(entityGraph)     ┃ Added                    ┃ Added                    ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/

//> 4. Update()
/*
The DbContext.Update() and DbSet.Update() methods attach an entity graph to a context and set the EntityState of each entity in a graph depending on whether 
it contains a key property value or not. 
*/
//> Consider the following example.
var student = new Student() { //Root entity (with key value)
    StudentId = 1,
    Name = "Bill",
    Address = new StudentAddress()  //Child entity (with key value)
    {
        StudentAddressId = 1,
        City = "Seattle",
        Country = "USA"
    },
    StudentCourses = new List<StudentCourse>() {
            new StudentCourse(){  Course = new Course(){ CourseName="Machine Language" } },//Child entity (empty key)
            new StudentCourse(){  Course = new Course(){  CourseId=2 } } //Child entity (with key value)
        }
};

var context = new SchoolContext();
context.Update(student);

DisplayStates(context.ChangeTracker.Entries());
//> Output:
//> Entity: Student, State: Modified
//> Entity: StudentAddress, State: Modified
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Added
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Modified

/*
In the above example, the Update() method applies the Modified state to the entities which contain non-empty key property values and the Added state to those which contain
empty or default CLR key values, irrespective of whether they are a root entity or a child entity.
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┓
┃ Method                         ┃ Root (with Key) ┃ Root (No Key)   ┃ Child (with Key)┃ Child (No Key)  ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ DbContext.Update(entityGraph)  ┃ Modified        ┃ Added           ┃ Modified        ┃ Added           ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ DbSet.Update(entityGraph)      ┃ Modified        ┃ Added           ┃ Modified        ┃ Added           ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┛
*/

//> 5. Remove()
//> The DbContext.Remove() and DbSet.Remove() methods set the Deleted EntityState to the root entity.
var student = new Student() { //Root entity (with key value)
    StudentId = 1,
    Name = "Bill",
    Address = new StudentAddress()  //Child entity (with key value)
    {
        StudentAddressId = 1,
        City = "Seattle",
        Country = "USA"
    },
    StudentCourses = new List<StudentCourse>() {
            new StudentCourse(){  Course = new Course(){ CourseName="Machine Language" } },//Child entity (empty key)
            new StudentCourse(){  Course = new Course(){  CourseId=2 } } //Child entity (with key value)
        }
};

var context = new SchoolContext();
context.Remove(student);

DisplayStates(context.ChangeTracker.Entries());
//> Output:
//> Entity: Student, State: Deleted
//> Entity: StudentAddress, State: Unchanged
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Added
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Unchanged

/*
The following table lists the behavior of the Remove() method on the EntityState of each entity.
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━┓
┃ Method                         ┃ Root (with Key) ┃ Root (No Key)   ┃ Child (with Key)┃ Child (No Key)  ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ DbContext.Remove(entityGraph)  ┃ Deleted         ┃ Exception       ┃ Unchanged       ┃ Added           ┃
┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┫
┃ DbSet.Remove(entityGraph)      ┃ Deleted         ┃ Exception       ┃ Unchanged       ┃ Added           ┃
┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━┛
*/
//! Thus, be careful while using the above methods in EF Core.


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [24]  ChangeTracker.TrackGraph()
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 24. ChangeTracker.TrackGraph() in Entity Framework Core
//> The ChangeTracker.TrackGraph() method was introduced in Entity Framework Core to track the entire entity graph and set custom entity states to each entity in a graph.

/*
# Signature: 
# public virtual void TrackGraph(object rootEntity, Action<EntityEntry> callback);
• The ChangeTracker.TrackGraph() method begins tracking an entity and any entities that are reachable by traversing its navigation properties. 
• The specified callback is called for each discovered entity and an appropriate EntityState must be set for each entity. 
• The callback function allows us to implement custom logic to set the appropriate state. If no state is set, the entity remains untracked.
*/

//> The following example demonstrates the TrackGraph method.
var student = new Student() { //Root entity (with key value)
    StudentId = 1,
    Name = "Bill",
    Address = new StudentAddress()  //Child entity (with key value)
    {
        StudentAddressId = 1,
        City = "Seattle",
        Country = "USA"
    },
    StudentCourses = new List<StudentCourse>() {
            new StudentCourse(){  Course = new Course(){ CourseName="Machine Language" } },//Child entity (empty key)
            new StudentCourse(){  Course = new Course(){  CourseId=2 } } //Child entity (with key value)
        }
};
var context = new SchoolContext();
            
context.ChangeTracker.TrackGraph(student, e => {
                                                if (e.Entry.IsKeySet)
                                                {
                                                    e.Entry.State = EntityState.Unchanged;
                                                }
                                                else
                                                {
                                                    e.Entry.State = EntityState.Added;
                                                }
                                            });

foreach (var entry in context.ChangeTracker.Entries())
{
    Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, 
                        State: {entry.State.ToString()} ");
}

//> Output:
//> Entity: Student, State: Added
//> Entity: StudentAddress, State: Unchanged
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Added
//> Entity: StudentCourse, State: Added
//> Entity: Course, State: Unchanged
//! it is just manually way to do things which we can do by using the DbContext.Add() or DbContext.Update() methods.
//! even though but it gives us more control to set the state of each entity in a graph.

/*
In the above example, the ChangeTracker.TrackGraph() method is used to set the state for each entity in a Student entity graph.
The first parameter is an entity graph and the second parameter is a function which sets the state of each entity.
We used a lambda expression to set the Unchanged state for entities that have valid key values and the Added state for entities that have empty key values.
The IsKeySet becomes true when an entity has a valid key property value.
Thus, we can use the ChangeTracker.TrackGraph() method to set different EntityState for each entity in a graph.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [25]  Raw SQL Queries (FromSql)
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 25. Execute Raw SQL Queries in Entity Framework Core
//> Entity Framework Core provides the DbSet.FromSql() method to execute raw SQL queries for the underlying database and get the results as entity objects.
//> The following example demonstrates executing a raw SQL query to MS SQL Server database.
var context = new SchoolContext();

var students = context.Students
                    .FromSql("SELECT * FROM Students WHERE Name = 'Bill'")
                    .ToList();

//> Parameterized Query
//> The FromSql method allows parameterized queries using string interpolation syntax in C#, as shown below.
string name = "Bill";
var context = new SchoolContext();
var students = context.Students
                    .FromSql($"SELECT * FROM Students WHERE Name = '{name}'")
                    .ToList();

//> The following is also valid.
string name = "Bill";
var context = new SchoolContext();
var students = context.Students
                    .FromSql("SELECT * FROM Students WHERE Name = '{0}'", name)
                    .ToList();

/*
> The examples above will execute the following SQL query to the SQL Server database:
> '''
> exec sp_executesql N'SELECT * FROM Students WHERE Name = ''@p0''
> ',N'@p0 nvarchar(4000)',@p0=N'Bill'
> go
> '''
*/

//> LINQ Operators
//> You can also use LINQ Operators after a raw query using FromSql method.
string name = "Bill";
var context = new SchoolContext();
var students = context.Students
                    .FromSql("SELECT * FROM Students WHERE Name = '{0}'", name)
                    .OrderBy(s => s.StudentId)
                    .ToList();

//> In the above example, EF Core executes the following query by combining FromSql method and OrderBy operator.
/*
> '''
> exec sp_executesql N'SELECT [s].[StudentId], [s].[Name]
> FROM (
>     SELECT * FROM Students WHERE Name = ''@p0''
> ) AS [s]
> ORDER BY [s].[StudentId]',N'@p0 nvarchar(4000)',@p0=N'Bill'
> go
> '''
*/

//> FromSql Limitations
/*
> 1. SQL queries must return entities of the same type as DbSet<T> type. e.g. the specified query cannot return the Course entities if FromSql is used after Students.
>     Returning ad-hoc types from FromSql() method is in the backlog.
> 2. The SQL query must return all the columns of the table. e.g. context.Students.FromSql("SELECT StudentId, LastName FROM Students").ToList() will throw an exception.
> 3. The SQL query cannot include JOIN queries to get related data. Use Include method to load related entities after FromSql() method.
*/


//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [26]  Stored Procedures
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 26. Working with Stored Procedures in Entity Framework Core
/*
EF Core provides the following methods to execute a stored procedure:
> 1. DbSet<TEntity>.FromSql()
> 2. DbContext.Database.ExecuteSqlCommand()
There are some limitations on the execution of database stored procedures using FromSql or ExecuteSqlCommand methods in EF Core 2:
> 1. Result must be an entity type. This means that a stored procedure must return all the columns of the corresponding table of an entity.
> 2. Result cannot contain related data. This means that a stored procedure cannot perform JOINs to formulate the result.
> 3. Insert, Update and Delete procedures cannot be mapped with the entity, so the SaveChanges method cannot call stored procedures for CUD operations.
*/

//> Let's create our stored procedure in MS SQL Server before we execute it in EF Core.
//# If you follow the database-first approach, then execute the following script in your local SQL Server database:
/*
USE [SchoolDB] => database name
GO

SET ANSI_NULLS ON => it is required to set ANSI_NULLS ON to create a stored procedure in SQL Server
GO

SET QUOTED_IDENTIFIER ON => it is required to set QUOTED_IDENTIFIER ON to create a stored procedure in SQL Server
GO

CREATE PROCEDURE [dbo].[GetStudents]
            @FirstName varchar(50)
        AS
        BEGIN
            SET NOCOUNT ON;
            select * from Students where FirstName like @FirstName +'%'
        END
GO
*/

//# If you are following the code-first approach, then follow the steps below:
//> 1. Add an empty migration by executing the following command in NPM (NuGet Package Manager):
//>     PM> Add-migration sp-GetStudents
//> 2. Write the following code in the Up method of the empty migration class in <DateTime>_sp-GetStudents.cs:
public partial class spGetStudents : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var sp = @"CREATE PROCEDURE [dbo].[GetStudents]
                    @FirstName varchar(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select * from Students where FirstName like @FirstName +'%'
                END";
        migrationBuilder.Sql(sp);
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
    }
}
//> 3. Now, create the above stored procedure in the database by executing the following command in NPM:
//>     PM> Update-database
//> This will create the GetStudents stored procedure in the SQL Server database.

//> Execute Stored Procedures using FromSql
//> As mentioned in the previous chapter, the FromSql method of DbSet can be used to execute the raw SQL queries to the underlying database. 
//> In the same way, it can be used to execute the stored procedure which returns entity data, but with some limitations.

//# In the database, we can execute the GetStudents stored procedure with an INPUT parameter value as shown below:
//> GetStudents "Bill"
//> -- or
//> exec GetStudents "Bill"

//# You can execute a stored procedure using the FromSql method in EF Core in the same way as above, as shown below.
var context = new SchoolContext(); 
var students = context.Students.FromSql("GetStudents 'Bill'").ToList();

//> You can also pass a parameter value using C# string interpolation syntax, as shown below.
var name = "Bill";
var context = new SchoolContext(); 
var students = context.Students
                        .FromSql($"GetStudents {name}")
                        .ToList();
//or
//var students = context.Students.FromSql($"exec GetStudents {name}").ToList();

//> Use a SqlParameter instance to specify the value of IN or OUT parameters as shown below:
var context = new SchoolContext(); 
var param = new SqlParameter("@FirstName", "Bill");
//or
/*var param = new SqlParameter() {
                    ParameterName = "@FirstName",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = "Bill"
};*/
var students = context.Students.FromSql("GetStudents @FirstName", param).ToList();

//> You can also specify @p0 for the first parameter, @p1 for the second, and so on.
var context = new SchoolContext(); 
var students = context.Students.FromSql("GetStudents @p0","Bill").ToList();
//> In the above example, @p0 is used for the first parameter because named parameters are not supported yet in EF Core.
/*
! Note: All entities in the result will be tracked by the DbContext by default. If you execute the same stored procedure with the same parameters multiple times,
! then it will execute the same SQL statement each time, but it will only track one result set. 
For example, the following example will execute the GetStudents stored procedure three times, but it will cache and track only one copy of the result.
*/

var context = new SchoolContext(); 

var list1 = context.Students.FromSql("GetStudents 'Bill'").ToList();
var list2 = context.Students.FromSql("GetStudents 'Bill'").ToList();
var list3 = context.Students.FromSql("GetStudents 'Bill'").ToList();

//> Execute Stored Procedure using ExecuteSqlCommand()
//> The ExecuteSqlCommand() method is used to execute database commands as a string. It returns an integer for the number of rows that were affected through the specified command.

var context = new SchoolContext(); 
var rowsAffected = context.Database.ExecuteSqlCommand("Update Students set FirstName = 'Bill' where StudentId = 1;");
/*
In the above example, the update command is passed in the ExecuteSqlCommand method.
The value of rowsAffected will be 1 because only 1 row was affected with the specified update command.
In the same way, we can execute stored procedures for Create, Update and Delete commands.
Consider the following stored procedure which inserts a record in the Students table in the database:
> '''
> CREATE PROCEDURE CreateStudent
>     @FirstName Varchar(50),
>     @LastName Varchar(50)
> AS
> BEGIN
>     SET NOCOUNT ON;
>     Insert into Students(
>            [FirstName]
>            ,[LastName]
>            )
>  Values (@FirstName, @LastName)
> END
> GO
> '''
*/
//> Now, you can execute the above stored procedure as shown below.
var context = new SchoolContext(); 
context.Database.ExecuteSqlCommand("CreateStudent @p0, @p1", parameters: new[] { "Bill", "Gates" });
//> In the same way, you can execute stored procedures for Update and Delete commands.




//════════════════════════════════════════════════════════════════════════════════════════════════════
//  [27]  Database-First (Scaffold)
//════════════════════════════════════════════════════════════════════════════════════════════════════
//> 27. Creating a Model for an Existing Database in Entity Framework Core
/*
Here you will learn how to create the context and entity classes for an existing database in Entity Framework Core.
Creating entity and context classes for an existing database is called the Database-First approach.

EF Core does not support a visual designer for the DB model or a wizard to create the entity and context classes similar to EF 6. So, 
we need to do reverse engineering using the Scaffold-DbContext command. This reverse engineering command creates entity and context classes (by deriving DbContext) 
based on the schema of the existing database.
> Scaffold-DbContext Command
Use Scaffold-DbContext to create a model based on your existing database. The following parameters can be specified with Scaffold-DbContext in Package Manager Console:
Scaffold-DbContext [-Connection] [-Provider] [-OutputDir] [-Context] [-Schemas] [-Tables] 
                    [-DataAnnotations] [-Force] [-Project] [-StartupProject] [<CommonParameters>]

> In Visual Studio, select menu Tools -> NuGet Package Manager -> Package Manager Console and run the following command:
PM> Scaffold-DbContext "Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

In the above command, the first parameter is a connection string which includes three parts:
1. DB Server. => Server=.\SQLExpress; refers to the local SQLEXPRESS database server.
2. database name. => Database=SchoolDB; specifies the name of the database for which we want to create the model classes. In this case, it is SchoolDB.
3. security info. => Trusted_Connection=True; specifies the Windows authentication. It will use Windows credentials to connect to the SQL Server. 
The second parameter is the provider name. We use the provider for the SQL Server, so it is Microsoft.EntityFrameworkCore.SqlServer. 
The -OutputDir parameter specifies the directory where we want to generate all the classes which is the Models folder in this case.

> Use the following command to get detailed help on the Scaffold-DbContext command:
PM> get-help scaffold-dbcontext -detailed
The above Scaffold-DbContext command creates entity classes for each table in the SchoolDB database and context class (by deriving DbContext) with Fluent API
configurations for all the entities in the Models folder.
*/

//> The following is the generated Student entity class for the Student table.
using System;
using System.Collections.Generic;

namespace EFCoreTutorials.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? StandardId { get; set; }
        public Standard Standard { get; set; }
        public StudentAddress StudentAddress { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}

//> The following is the SchoolDBContext class which you can use to save or retrieve data.
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCoreTutorials.Models
{
    public partial class SchoolDBContext : DbContext
    {
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Standard> Standard { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentAddress> StudentAddress { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Course_Teacher");
            });

            modelBuilder.Entity<Standard>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.StandardName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.Standard)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.StandardId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Student_Standard");
            });

            modelBuilder.Entity<StudentAddress>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .ValueGeneratedNever();
                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.Student)
                    .WithOne(p => p.StudentAddress)
                    .HasForeignKey<StudentAddress>(d => d.StudentId)
                    .HasConstraintName("FK_StudentAddress_Student");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");
                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentCourse_Student");
            });
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.StandardId).HasDefaultValueSql("((0))");
                entity.Property(e => e.TeacherName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.Standard)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.StandardId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Teacher_Standard");
            });
        }
    }
}

//! Note: EF Core creates entity classes only for tables and not for stored procedures or views.

//> DotNet CLI
//> If you use the .NET command-line interface, then open the command prompt and navigate to the root folder and execute the following dotnet ef dbcontext scaffold command:

//> dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models 
// Thus, you can create an EF Core model for an existing database.
// Note: Once you have created the model, you must use the Migration commands whenever you change the model to keep the database up to date with the model.