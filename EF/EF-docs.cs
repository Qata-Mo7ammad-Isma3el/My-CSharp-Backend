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

