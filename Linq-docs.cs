/*
1) What is LINQ?
• LINQ (Language Integrated Query) is uniform query syntax in C# and VB.NET to retrieve data from different sources and formats. It is integrated in C# or VB,
  thereby eliminating the mismatch between programming languages and databases, as well as providing a single querying interface for different types of data sources.
• LINQ queries return results as objects. It enables you to uses object-oriented approach on the result set and not to worry about transforming different formats of results into objects.
*/

//> Example: LINQ Query to Array 
// Data source
string[] names = {"Bill", "Steve", "James", "Mohan" };

// LINQ Query 
var myLinqQuery = from name in names       //> query syntax of LINQ
                where name.Contains('a')
                select name;
    
// Query execution
foreach(var name in myLinqQuery)
    Console.Write(name + " ");

//# it is simple it is just an sql like query but it is not sql it is linq and it is used to query different data sources like arrays, lists, xml, etc.
//! You will not get the result of a LINQ query until you execute it.

/*
2) Why LINQ?
To understand why we should use LINQ, let's look at some examples. Suppose you want to find list of teenage students from an array of Student objects.
Before C# 2.0, we had to use a 'foreach' or a 'for' loop to traverse the collection to find a particular object.
*/

//> Example: Use for loop to find elements from the collection in C# 1.0
class Student
{
    public int StudentID { get; set; }
    public String StudentName { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Student[] studentArray = { 
            new Student() { StudentID = 1, StudentName = "John", Age = 18 },
            new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 },
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 },
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 },
            new Student() { StudentID = 6, StudentName = "Chris",  Age = 17 },
            new Student() { StudentID = 7, StudentName = "Rob",Age = 19  },
        };

        Student[] students = new Student[10];

        int i = 0;

        foreach (Student std in studentArray)
        {
            if (std.Age &gt; 12 && std.Age &lt; 20)
            {
                students[i] = std;
                i++;
            }
        }
    }
}

//# Use of for loop is cumbersome, not maintainable and readable. C# 2.0 introduced delegate, which can be used to handle this kind of a scenario, as shown below.
//> Example: Use delegate to find elements from the collection in C# 2.0
delegate bool FindStudent(Student std);

class StudentExtension
{ 
    public static Student[] where(Student[] stdArray, FindStudent del)
    {
        int i=0;
        Student[] result = new Student[10];
        foreach (Student std in stdArray)
            if (del(std))
            {
                result[i] = std;
                i++;
            }

        return result;
    }
}
    
class Program
{
    static void Main(string[] args)
    {
        Student[] studentArray = { 
            new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 } ,
            new Student() { StudentID = 6, StudentName = "Chris",  Age = 17 } ,
            new Student() { StudentID = 7, StudentName = "Rob",Age = 19  } ,
        };

        Student[] students = StudentExtension.where(studentArray, delegate(Student std){
                return std.Age &gt; 12 && std.Age &lt; 20;
            });
        }
    }
}
/*
3) LINQ API in .NET
We can write LINQ queries for the classes that implement IEnumerable<T> or IQueryable<T> interface.
LINQ queries uses extension methods for classes that implement IEnumerable or IQueryable interface. 
The Enumerable and Queryable are two static classes that contain extension methods to write LINQ queries.
*/

// Enumerable
/*
The Enumerable class includes extension methods for the classes that implement IEnumerable<T> interface, for example all the built-in collection
classes implement IEnumerable<T> interface and so we can write LINQ queries to retrieve data from the built-in collections.
in simple words all the built-in collection classes like:
- List<T>
- Dictionary<TKey, TValue>
- HashSet<T>
- Queue<T>
- SortedDictionary<TKey, TValue>
- SortedList<TKey, TValue>
- SortedSet<T>
- LinkedList<T>
- Stack<T>
- etc...
all of these classes implement IEnumerable<T> interface and so we can write LINQ queries to retrieve data from these collections.
so the Enumerable class contains extension methods for these classes to write LINQ queries:
- Aggregate<TSource> (+ 2 overloads)
- All<TSource>
- Any<TSource> (+ 1 overload)
- AsEnumerable<TSource>
- Average (+ 19 overloads)
- Cast<TResult>
- Concat<TSource>
- Contains<TSource> (+ 1 overload)
- Count<TSource> (+ 1 overload)
- DefaultIfEmpty<TSource> (+ 1 overload)
- Distinct<TSource> (+ 1 overload)
- ElementAt<TSource>
- ElementAtOrDefault<TSource>
- Empty<TSource>
- Except<TSource> (+ 1 overload)
- First<TSource> (+ 1 overload)
- FirstOrDefault<TSource> (+ 1 overload)
- GroupBy<TSource, TKey> (+ 7 overloads)
- GroupJoin<TOuter, TInner, TKey, TResult> (+ 1 overload)
- Intersect<TSource> (+ 1 overload)
- Join<TOuter, TInner, TKey, TResult> (+ 1 overload)
- Last<TSource> (+ 1 overload)
- LastOrDefault<TSource> (+ 1 overload)
- LongCount<TSource> (+ 1 overload)
- Max (+ 21 overloads)
- Min (+ 21 overloads)
- OfType<TResult>
- OrderBy<TSource, TKey> (+ 1 overload)
- OrderByDescending<TSource, TKey> (+ 1 overload)
- Range
- Repeat<TResult>
- Reverse<TSource>
- Select<TSource, TResult> (+ 1 overload)
- SelectMany<TSource, TResult> (+ 3 overloads)
- SequenceEqual<TSource> (+ 1 overload)
- Single<TSource> (+ 1 overload)
- SingleOrDefault<TSource> (+ 1 overload)
- Skip<TSource>
- SkipWhile<TSource> (+ 1 overload)
- Sum (+ 19 overloads)
- Take<TSource>
- TakeWhile<TSource> (+ 1 overload)
- ThenBy<TSource, TKey> (+ 1 overload)
- ThenByDescending<TSource, TKey> (+ 1 overload)
- ToArray<TSource>
- ToDictionary<TSource, TKey> (+ 3 overloads)
- ToList<TSource>
- ToLookup<TSource, TKey> (+ 3 overloads)
- Union<TSource> (+ 1 overload)
- Where<TSource> (+ 1 overload)
- Zip<TFirst, TSecond, TResult>
*/

// Queryable
/*
The Queryable class includes extension methods for classes that implement IQueryable<T> interface.
The IQueryable<T> interface is used to provide querying capabilities against a specific data source where the type of the data is known. 
For example, Entity Framework api implements IQueryable<T> interface to support LINQ queries with underlying databases such as MS SQL Server.
in simple words all the data sources that support LINQ queries implement IQueryable<T> interface and so we can write LINQ queries to retrieve data from these data sources.
things like:
- Entity Framework
- LINQ to SQL
- LINQ to Amazon
- LINQ to LDAP
- Parallel LINQ (PLINQ)
- etc... 
so the Queryable class contains extension methods for these data sources to write LINQ queries:
Queryable
Static Class

Methods
- Aggregate<TSource> (+ 2 overloads)
- All<TSource>
- Any<TSource> (+ 1 overload)
- AsQueryable<TElement> (+ 1 overload)
- Average (+ 19 overloads)
- Cast<TResult>
- Concat<TSource>
- Contains<TSource> (+ 1 overload)
- Count<TSource> (+ 1 overload)
- DefaultIfEmpty<TSource> (+ 1 overload)
- Distinct<TSource> (+ 1 overload)
- ElementAt<TSource>
- ElementAtOrDefault<TSource>
- Except<TSource> (+ 1 overload)
- First<TSource> (+ 1 overload)
- FirstOrDefault<TSource> (+ 1 overload)
- GroupBy<TSource, TKey> (+ 7 overloads)
- GroupJoin<TOuter, TInner, TKey, TResult> (+ 1 overload)
- Intersect<TSource> (+ 1 overload)
- Join<TOuter, TInner, TKey, TResult> (+ 1 overload)
- Last<TSource> (+ 1 overload)
- LastOrDefault<TSource> (+ 1 overload)
- LongCount<TSource> (+ 1 overload)
- Max<TSource> (+ 1 overload)
- Min<TSource> (+ 1 overload)
- OfType<TResult>
- OrderBy<TSource, TKey> (+ 1 overload)
- OrderByDescending<TSource, TKey> (+ 1 overload)
- Reverse<TSource>
- Select<TSource, TResult> (+ 1 overload)
- SelectMany<TSource, TResult> (+ 3 overloads)
- SequenceEqual<TSource> (+ 1 overload)
- Single<TSource> (+ 1 overload)
- SingleOrDefault<TSource> (+ 1 overload)
- Skip<TSource>
- SkipWhile<TSource> (+ 1 overload)
- Sum (+ 19 overloads)
- Take<TSource>
- TakeWhile<TSource> (+ 1 overload)
- ThenBy<TSource, TKey> (+ 1 overload)
- ThenByDescending<TSource, TKey> (+ 1 overload)
- Union<TSource> (+ 1 overload)
- Where<TSource> (+ 1 overload)
- Zip<TFirst, TSecond, TResult>
*/

/*
4) LINQ Query Syntax
There are two basic ways to write a LINQ query to IEnumerable collection or IQueryable data sources.
1. Query Syntax or Query Expression Syntax
2. Method Syntax or Method Extension Syntax or Fluent
*/
// Query Syntax
// Query syntax is similar to SQL (Structured Query Language) for the database. It is defined within the C# code.
// LINQ Query Syntax:
//> from <range variable> in Collection
//> <Standard Query Operators> <lambda expression>
//> <select or groupBy operator> <result formation> 
//# The LINQ query syntax starts with from keyword and ends with select keyword.

//> Example: LINQ Query Syntax in C#
// string collection
IList<string> stringList = new List<string>() { 
    "C# Tutorials",
    "VB.NET Tutorials",
    "Learn C++",
    "MVC Tutorials" ,
    "Java" 
};

// LINQ Query Syntax
var result = from s in stringList
            where s.Contains("Tutorials") 
            select s;

//> var result -> Result variable
//> s -> Range variable
//> stringList -> Sequence (IEnumerable collection or IQueryable data source)
//> where s.Contains("Tutorials") -> Standard Query Operator with lambda expression
//> select s -> Select operator with result formation  

// LINQ Method Syntax
/*
Method syntax (also known as fluent syntax) uses extension methods included in the Enumerable or Queryable static class, similar to how you would call the extension method of any class.
!The compiler converts query syntax into method syntax at compile time.
*/
//> The following is a sample LINQ method syntax query that returns a collection of strings which contains a word "Tutorials".
//> Example: LINQ Method Syntax in C#

// string collection
IList<string> stringList = new List<string>() { 
    "C# Tutorials",
    "VB.NET Tutorials",
    "Learn C++",
    "MVC Tutorials" ,
    "Java" 
};

// LINQ Method Syntax
var result = stringList.Where(s =&gt; s.Contains("Tutorials"));
/*
# As you can see in the above figure, method syntax comprises of extension methods and Lambda expression. The extension method Where() is defined in the Enumerable class.
# If you check the signature of the Where extension method, you will find the Where method accepts a predicate delegate as Func<Student, bool>. 
# This means you can pass any delegate function that accepts a Student object as an input parameter and returns a Boolean value as shown in the below figure.
# The lambda expression works as a delegate passed in the Where clause. Learn lambda expression in the next section.
> in simple words in linq we can write queries in two ways either using query syntax or using method syntax both will give the same results.
> the method syntax is more powerful and flexible than the query syntax and it is also more readable and maintainable than the query syntax. 
*/

/*
5) Anatomy of the Lambda Expression
The lambda expression is a shorter way of representing anonymous method using some special syntax.
*/
//> For example, following anonymous method checks if student is teenager or not:
//> Example: Anonymous Method in C#
delegate(Student s) { return s.Age > 12 && s.Age < 20; };
//! The above anonymous method can be represented using a Lambda Expression in C# as shown below:
//> Example: Lambda Expression in C#
s => s.Age > 12 && s.Age < 20;
//> Thus, we got the lambda expression: 
//> s => s.Age > 12 && s.Age < 20 where:
//> 1. s is a parameter.
//> 2. => is the lambda operator. 
//> 3. s.Age > 12 && s.Age < 20 is the body expression:
//! this little arrow (=>) is called lambda operator and it separates the input parameters on the left side from the body of the lambda expression on the right side.

// Lambda Expression with Multiple Parameters
//> Example: Specify Multiple Parameters in Lambda Expression C#
(s, youngAge) => s.Age >= youngAge;

// Lambda Expression without Parameter
//> It is not necessary to have at least one parameter in a lambda expression. The lambda expression can be specify without any parameter also.
() => Console.WriteLine("Parameter less lambda expression");

// Multiple Statements in Lambda Expression Body
//> Example: Multi Statements Lambda expression C#
(s, youngAge) =>
{
  Console.WriteLine("Lambda expression with multiple statements in the body");
    
  return s.Age >= youngAge;
}

// Declare Local Variable in Lambda Expression Body
//> Example: Local Variable in Lambda expression C#
(s, youngAge) =>
{
  int age = s.Age;
    
  Console.WriteLine("Lambda expression with local variable in the body");
    
  return age >= youngAge;
}

// Assign Lambda Expression to Delegate
//> The lambda expression can be assigned to Func<in T, out TResult> type delegate. The last parameter type in a Func delegate is the return type and rest are input parameters.
//> Example: Lambda Expression Assigned to Func Delegate C#
Func<Student, bool> isStudentTeenAger = s => s.age > 12 && s.age < 20;
Student std = new Student() { age = 21 };
bool isTeen = isStudentTeenAger(std);// returns false

//> this is the delegate as normal method
bool isStudentTeenAger(Student s)
{
    return s.Age > 12 && s.Age < 20;
}

// Action Delegate
//> Unlike the Func delegate, an Action delegate can only have input parameters. Use the Action delegate type when you don't need to return any value from lambda expression.
//> Example: Lamda Expression Assigned to Action Delegate C#
Action<Student> PrintStudentDetail = s => Console.WriteLine("Name: {0}, Age: {1} ", s.StudentName, s.Age);

Student std = new Student(){ StudentName = "Bill", Age=21};

PrintStudentDetail(std);//output: Name: Bill, Age: 21

// Lambda Expression in LINQ Query
/*
Usually lambda expression is used with LINQ query. Enumerable static class includes Where extension method for IEnumerable<T> that accepts Func<TSource,bool>.
So, the Where() extension method for IEnumerable<Student> collection is required to pass Func<Student,bool>
*/

//> So now, you can pass the lambda expression assigned to the Func delegate to the Where() extension method in the method syntax as shown below:
//> Example: Func Delegate in LINQ Method Syntax
IList<Student> studentList = new List<Student>(){...};

Func<Student, bool> isStudentTeenAger = s => s.age > 12 && s.age < 20;
var teenStudents = studentList.Where(isStudentTeenAger).ToList<Student>();
//> Example: Func Delegate in LINQ Query Syntax
IList<Student> studentList = new List<Student>(){...};

Func<Student, bool> isStudentTeenAger = s => s.age > 12 && s.age < 20;

var teenStudents = from s in studentList
                   where isStudentTeenAger(s)
                   select s;

/*
6) Standard Query Operators
Standard Query Operators in LINQ are actually extension methods for the IEnumerable<T> and IQueryable<T> types. 
They are defined in the System.Linq.Enumerable and System.Linq.Queryable classes.
There are over 50 standard query operators available in LINQ that provide different functionalities like filtering, sorting, grouping, aggregation, concatenation, etc.
*/
// Standard Query Operators in Query Syntax
var teenStudents = from s in studentList
                   where s.age > 20 
                   select s;
//> where and select are the standard query operators in the above LINQ query syntax.

// Standard Query Operators in Method Syntax
var teenStudents = studentList.Where(s => s.age > 20).Select(s => s);
//> Where and Select are the standard query operators in the above LINQ method syntax.

//! Standard query operators in query syntax is converted into extension methods at compile time. So both are same.
/*
Standard Query Operators can be classified based on the functionality they provide. The following table lists all the classification of Standard Query Operators:
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Classification┃ Standard Query Operators                                       ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Filtering     ┃ Where, OfType                                                  ┃
┃ Sorting       ┃ OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse  ┃
┃ Grouping      ┃ GroupBy, ToLookup                                              ┃
┃ Join          ┃ GroupJoin, Join                                                ┃
┃ Projection    ┃ Select, SelectMany                                             ┃
┃ Aggregation   ┃ Aggregate, Average, Count, LongCount, Max, Min, Sum            ┃
┃ Quantifiers   ┃ All, Any, Contains                                             ┃
┃ Elements      ┃ ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, ...┃
┃ Set           ┃ Distinct, Except, Intersect, Union                             ┃
┃ Partitioning  ┃ Skip, SkipWhile, Take, TakeWhile                               ┃
┃ Concatenation ┃ Concat                                                         ┃
┃ Equality      ┃ SequenceEqual                                                  ┃
┃ Generation    ┃ DefaultIfEmpty, Empty, Range, Repeat                           ┃
┃ Conversion    ┃ AsEnumerable, AsQueryable, Cast, ToArray, ToDictionary, ToList ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/

// 1. Filtering Operator - Where
// Filtering operators in LINQ filter the sequence (collection) based on some given criteria.
/*
The following table lists all the filtering operators available in LINQ.
┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator          ┃ Description                                                        ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Where             ┃ Returns values from the collection based on a predicate function.  ┃
┃                   ┃ (e.g., list.Where(x => x > 10))                                    ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ OfType            ┃ Returns values from the collection based on a specified type.      ┃
┃                   ┃ Filters out elements that cannot be cast to that type.             ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 1.1 Where
The Where operator (Linq extension method) filters the collection based on a given criteria expression and returns a new collection. 
The criteria can be specified as lambda expression or Func delegate type.
The Where extension method has following two overloads. Both overload methods accepts a Func delegate type parameter.
One overload required Func<TSource,bool> input parameter and second overload method required Func<TSource, int, bool> input parameter where int is for index:
> Where method Overloads:
1. public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
2. public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate);
*/

//> Where clause in Query Syntax
//> Example: Where clause - LINQ query syntax C#
IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
        new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 } 
    };

var filteredResult = from s in studentList
                    where s.Age > 12 && s.Age < 20
                    select s.StudentName;

//> Alternatively, you can also use a Func type delegate with an anonymous method to pass as a predicate function as below (output would be the same):
Func<Student,bool> isTeenAger = delegate(Student s) { 
                                    return s.Age > 12 && s.Age < 20; 
                                };

var filteredResult = from s in studentList
                     where isTeenAger(s)
                     select s;

//> You can also call any method that matches with Func parameter with one of Where() method overloads.
//> Example: Where clause with method call in LINQ query syntax C#
public static void Main()
{
    var filteredResult = from s in studentList
                         where isTeenAger(s)
                         select s;
}

public static bool IsTeenAger(Student stud)
{
    return stud.Age > 12 && stud.Age < 20;  
}


//> Where extension method in Method Syntax
//! Unlike the query syntax, you need to pass whole lambda expression as a predicate function instead of just body expression in LINQ method syntax.
var filteredResult = studentList.Where(s => s.Age > 12 && s.Age < 20);

// As mentioned above, the Where extension method also have second overload that includes index of current element in the collection. You can use that index in your logic if you need.
//> Example: Linq - Where extension method in C#

IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

    var filteredResult = studentList.Where((s, i) => { 
            if(i % 2 ==  0) // if it is even element
                return true;
                
        return false;
    });

foreach (var std in filteredResult)
        Console.WriteLine(std.StudentName);

// Multiple Where clause
//> You can call the Where() extension method more than one time in a single LINQ query.
//> this is in query syntax
var filteredResult = from s in studentList
                    where s.Age > 12
                    where s.Age < 20
                    select s;

//> this is in method syntax
var filteredResult = studentList.Where(s => s.Age > 12).Where(s => s.Age < 20);

//> 1.2 OfType
//> The OfType operator filters the collection based on the ability to cast an element in a collection to a specified type.
// OfType in Query Syntax
//> Use OfType operator to filter the above collection based on each element's type
//> Example: OfType operator in C#
IList mixedList = new ArrayList();
mixedList.Add(0);
mixedList.Add("One");
mixedList.Add("Two");
mixedList.Add(3);
mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

var stringResult = from s in mixedList.OfType<string>()
                select s;

var intResult = from s in mixedList.OfType<int>()
                select s;

// OfType in Method Syntax
// You can use OfType<TResult>() extension method in linq method syntax as shown below.
//> Example: OfType in C#
var stringResult = mixedList.OfType<string>();
var intResult = mixedList.OfType<int>();


// 2. Sorting Operators: OrderBy & OrderByDescending
// A sorting operator arranges the elements of the collection in ascending or descending order. 
/*
LINQ includes following sorting operators.
┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator          ┃ Description                                                        ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ OrderBy           ┃ Sorts the elements in the collection based on specified fields     ┃
┃                   ┃ in ascending or descending order.                                  ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ OrderByDescending ┃ Sorts the collection based on specified fields in descending order.┃
┃                   ┃ Only valid in method syntax.                                       ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ThenBy            ┃ Only valid in method syntax. Used for second-level sorting         ┃
┃                   ┃ in ascending order.                                                ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ThenByDescending  ┃ Only valid in method syntax. Used for second-level sorting         ┃
┃                   ┃ in descending order.                                               ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Reverse           ┃ Only valid in method syntax. Reverses the order of the elements    ┃
┃                   ┃ in the collection.                                                 ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 2.1 OrderBy & OrderByDescending
OrderBy sorts the values of a collection in ascending or descending order.
It sorts the collection in ascending order by default because ascending keyword is optional here. Use descending keyword to sort collection in descending order.
*/
//> Example: OrderBy in Query Syntax C#
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
    new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
};

var orderByResult = from s in studentList
                   orderby s.StudentName 
                   select s;

var orderByDescendingResult = from s in studentList
                   orderby s.StudentName descending
                   select s;

/*
> OrderBy in Method Syntax
OrderBy extension method has two overloads. First overload of OrderBy extension method accepts the Func delegate type parameter.
So you need to pass the lambda expression for the field based on which you want to sort the collection.
The second overload method of OrderBy accepts object of IComparer along with Func delegate type to use custom comparison for sorting.

> OrderBy Overload Methods:
1. public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source,Func<TSource, TKey> keySelector);
2. public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source,Func<TSource, TKey> keySelector,IComparer<TKey> comparer);
*/
//> The following example sorts the studentList collection in ascending order of StudentName using OrderBy extension method.
//> Example: OrderBy in Method Syntax C#
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
    new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
};

var studentsInAscOrder = studentList.OrderBy(s => s.StudentName);
//! Method syntax does not allow the descending keyword to sorts the collection in descending order. Use OrderByDescending() method for it.

// Multiple Sorting
//> You can sort the collection on multiple fields seperated by comma. The given collection would be first sorted based on the first field and then if value 
//> of first field would be the same for two elements then it would use second field for sorting and so on.

//> Example: Multiple sorting in Query syntax C#
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
    new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }, 
    new Student() { StudentID = 6, StudentName = "Ram" , Age = 18 }
};

var orderByResult = from s in studentList
                   orderby s.StudentName, s.Age 
                   select new { s.StudentName, s.Age };

//! Multiple sorting in method syntax works differently. Use ThenBy or ThenByDescending extension methods for secondary sorting.

/*
> 2.2 ThenBy & ThenByDescending
The ThenBy and ThenByDescending extension methods are used for sorting on multiple fields.
The OrderBy() method sorts the collection in ascending order based on specified field. Use ThenBy() method after OrderBy to sort the collection on another field in ascending order.
Linq will first sort the collection based on primary field which is specified by OrderBy method and then sort the resulted collection in ascending order again based on secondary field
specified by ThenBy method.
! The same way, use ThenByDescending method to apply secondary sorting in descending order.
*/
//> The following example shows how to use ThenBy and ThenByDescending method for second level sorting:
//! this is in method syntax
//> Example: ThenBy & ThenByDescending
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
    new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }, 
    new Student() { StudentID = 6, StudentName = "Ram" , Age = 18 }
};
var thenByResult = studentList.OrderBy(s => s.StudentName).ThenBy(s => s.Age);
var thenByDescResult = studentList.OrderBy(s => s.StudentName).ThenByDescending(s => s.Age);

// 3. Grouping Operators: GroupBy & ToLookup

/*
The grouping operators do the same thing as the GroupBy clause of SQL query. The grouping operators create a group of elements based on the given key.
This group is contained in a special type of collection that implements an IGrouping<TKey,TSource> interface where TKey is a key value, on which the group has been formed and TSource 
is the collection of elements that matches with the grouping key value.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator      ┃ Description                                                       ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ GroupBy       ┃ Returns groups of elements based on a key value. Each group is an ┃
┃               ┃ IGrouping<TKey, TElement> object. Uses deferred execution.        ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ToLookup      ┃ Similar to GroupBy, but execution is immediate. It creates a      ┃
┃               ┃ 1:N dictionary-like structure that is immutable once created.     ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛


> 3.1 GroupBy
The GroupBy operator returns a group of elements from the given collection based on some key value. Each group is represented by IGrouping<TKey, TElement> object.
Also, the GroupBy method has eight overload methods, so you can use appropriate extension method based on your requirement in method syntax.
! A LINQ query can end with a GroupBy or Select clause.
The result of GroupBy operators is a collection of groups.
*/
//> GroupBy in Query Syntax
// The following example creates a groups of students who have same age. Students of the same age will be in the same collection and each grouped collection will have a key and inner 
// collection, where the key will be the age and the inner collection will include students whose age is matched with a key.
//> Example: GroupBy in Query syntax C#
IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 } 
    };

var groupedResult = from s in studentList
                    group s by s.Age;

//iterate each group        
foreach (var ageGroup in groupedResult)
{
    Console.WriteLine("Age Group: {0}", ageGroup .Key); //Each group has a key 
             
    foreach(Student s in ageGroup) // Each group has inner collection
        Console.WriteLine("Student Name: {0}", s.StudentName);
}
/*
output:
AgeGroup: 18
StudentName: John
StudentName: Bill
AgeGroup: 21
StudentName: Steve
StudentName: Abram
AgeGroup: 20
StudentName: Ram
*/

//> GroupBy in Method Syntax
// The GroupBy() extension method works the same way in the method syntax. Specify the lambda expression for key selector field name in GroupBy extension method.
//> Example: GroupBy in method syntax C#
IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 } 
    };

var groupedResult = studentList.GroupBy(s =&gt; s.Age);

foreach (var ageGroup in groupedResult)
{
    Console.WriteLine("Age Group: {0}", ageGroup.Key);  //Each group has a key 
    foreach(Student s in ageGroup)  //Each group has a inner collection  
        Console.WriteLine("Student Name: {0}", s.StudentName);
}

//> 3.2 ToLookup
// ToLookup is the same as GroupBy; the only difference is GroupBy execution is deferred, whereas ToLookup execution is immediate. Also, ToLookup is only applicable in Method syntax.
//! ToLookup is not supported in the query syntax.
		// Student collection
		IList<Student> studentList = new List<Student>() { 
				new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
				new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
				new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
				new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
				new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 } 
			};
		
		var lookupResult = studentList.ToLookup(s => s.Age);

		foreach (var group in lookupResult)
		{
			Console.WriteLine("Age Group: {0}", group.Key);  //Each group has a key 
			foreach(Student s in group)  //Each group has a inner collection  
				Console.WriteLine("Student Name: {0}", s.StudentName);
		}


// 4. Join Operators: Join & GroupJoin
// The joining operators joins the two sequences (collections) and produce a result.
/*

┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator          ┃ Usage / Description                                                ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Join              ┃ Joins two sequences based on a key and returns a flat, resulted    ┃
┃                   ┃ sequence. Similar to an Inner Join in SQL.                         ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ GroupJoin         ┃ Joins two sequences based on keys and returns groups of            ┃
┃                   ┃ sequences. Similar to a Left Outer Join in SQL.                    ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 4.1 Join
The Join operator operates on two collections, inner collection & outer collection.
It returns a new collection that contains elements from both the collections which satisfies specified expression. It is the same as inner join of SQL.
> Join in Method Syntax
The Join extension method has two overloads as shown below.
> Join Overload Methods:
1. public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector,Func<TInner, TKey> innerKeySelector,Func<TOuter, TInner, TResult> resultSelector);
2. public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,IEnumerable<TInner> inner,Func<TOuter, TKey> outerKeySelector,Func<TInner, TKey> innerKeySelector,Func<TOuter, TInner, TResult> resultSelector,IEqualityComparer<TKey> comparer);
> As you can see in the first overload method takes five input parameters (except the first 'this' parameter): 
1) outer
2) inner
3) outerKeySelector
4) innerKeySelector
5) resultSelector.
*/
//> Example: Join operator C#
IList<string> strList1 = new List<string>() { 
    "One", 
    "Two", 
    "Three", 
    "Four"
};

IList<string> strList2 = new List<string>() { 
    "One", 
    "Two", 
    "Five", 
    "Six"
};

var innerJoin = strList1.Join( // outer sequence
                        strList2,  // inner sequence
                        str1 => str1, // outer key selector
                        str2 => str2, // inner key selector
                        (str1, str2) => str1 // result selector
                        );

//> there is also query syntax for join operator.

//> 4.2 GroupJoin
/*
We have seen the Join operator in the previous section. The GroupJoin operator performs the same task as Join operator except that GroupJoin returns a result in group based on 
specified group key.
The GroupJoin operator joins two sequences based on key and groups the result by matching key and then returns the collection of grouped result and key.
GroupJoin requires same parameters as Join. GroupJoin has following two overload methods:
> GroupJoin Overload Methods:
1. public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector);
2. public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer);
As you can see in the first overload method takes five input parameters (except the first 'this' parameter):
1) outer
2) inner
3) outerKeySelector 
4) innerKeySelector
5) resultSelector.
! Please notice that resultSelector is of Func delegate type that has second input parameter as IEnumerable type for inner sequence.
*/

public class Program
{
	public static void Main()
	{
		// Student collection
		IList<Student> studentList = new List<Student>() { 
				new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
				new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
				new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
				new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
				new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 } 
			};
		
		IList<Standard> standardList = new List<Standard>() { 
				new Standard(){ StandardID = 1, StandardName="Standard 1"},
				new Standard(){ StandardID = 2, StandardName="Standard 2"},
				new Standard(){ StandardID = 3, StandardName="Standard 3"}
			};
		
		var groupJoin = standardList.GroupJoin(studentList,  //inner sequence
                                std => std.StandardID, //outerKeySelector 
                                s => s.StandardID,     //innerKeySelector
                                (std, studentsGroup) => new // resultSelector 
                                {
                                    Students = studentsGroup,
                                    StandarFulldName = std.StandardName
                                });

		foreach (var item in groupJoin)
		{ 
			Console.WriteLine(item.StandarFulldName );
			
			foreach(var stud in item.Students)
				Console.WriteLine(stud.StudentName);
		}
		
	}
		
}

public class Student{

	public int StudentID { get; set; }
	public string StudentName { get; set; }
	public int Age { get; set; }
	public int StandardID { get; set; }
}

public class Standard{

	public int StandardID { get; set; }
	public string StandardName { get; set; }
}

//> also there is query syntax for group join operator.

//> 5. Projection Operators: Select, SelectMany
/*
There are two projection operators available in LINQ. 
1. Select 
2. SelectMany

> 5.1 Select
The Select operator always returns an IEnumerable collection which contains elements based on a transformation function.
It is similar to the Select clause of SQL that produces a flat result set.
*/
public class Student{ 
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

//> Select in Query Syntax
//> Example: Select in Query Syntax C#
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John" },
    new Student() { StudentID = 2, StudentName = "Moin" },
    new Student() { StudentID = 3, StudentName = "Bill" },
    new Student() { StudentID = 4, StudentName = "Ram" },
    new Student() { StudentID = 5, StudentName = "Ron" } 
};

var selectResult = from s in studentList
                   select s.StudentName;

// The select operator can be used to formulat the result as per our requirement.
// It can be used to return a collection of custom class or anonymous type which includes properties as per our need.
//> Example: Select in Query Syntax C#
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John", Age = 13 } ,
    new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
    new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
    new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 } 
};

// returns collection of anonymous objects with Name and Age property
var selectResult = from s in studentList
                   select new { Name = "Mr. " + s.StudentName, Age = s.Age }; 

// iterate selectResult
foreach (var item in selectResult)
    Console.WriteLine("Student Name: {0}, Age: {1}", item.Name, item.Age);

//> Select in Method Syntax
//> Example: Select in Method Syntax C#
IList<Student> studentList = new List<Student>() { 
    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
    new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
    new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
    new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 } 
};
    
var selectResult = studentList.Select(s => new { Name = s.StudentName , Age = s.Age  });

//> 5.2 Select Many
// The SelectMany operator projects sequences of values that are based on a transform function and then flattens them into one sequence.

//> 6. Quantifier Operators
/*
The quantifier operators evaluate elements of the sequence on some condition and return a boolean value to indicate that some or all elements satisfy the condition.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator      ┃ Description                                                       ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ All           ┃ Checks if all the elements in a sequence satisfy the specified    ┃
┃               ┃ condition. Returns true only if every item matches.               ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Any           ┃ Checks if any of the elements in a sequence satisfy the specified ┃
┃               ┃ condition. Also used to check if a sequence is not empty.         ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Contains      ┃ Checks if the sequence contains a specific element (value).       ┃
┃               ┃ Useful for finding a specific ID or string in a list.             ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 6.1 All
The All operator evaluates each elements in the given collection on a specified condition and returns True if all the elements satisfy a condition.
> Example: All operator C#

*/
IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

// checks whether all the students are teenagers    
bool areAllStudentsTeenAger = studentList.All(s => s.Age > 12 && s.Age < 20);
Console.WriteLine(areAllStudentsTeenAger);

//> 6.2 Any
//> Any checks whether any element satisfy given condition or not? In the following example, Any operation is used to check whether any student is teen ager or not.
//> Example: Any operator C#
bool isAnyStudentTeenAger = studentList.Any(s => s.Age > 12 && s.Age < 20);
//! Quantifier operators are Not Supported with C# query syntax.

/*
> 6.3 Contains
> The Contains operator checks whether a specified element exists in the collection or not and returns a boolean.
The Contains() extension method has following two overloads. The first overload method requires a value to check in the collection and the second overload 
method requires additional parameter of IEqualityComparer type for custom equalality comparison.
> Contains() Overloads:
1. public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value);
2. public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value,IEqualityComparer<TSource> comparer);
As mentioned above, the Contains() extension method requires a value to check as a input parameter. Type of a value must be same as type of generic collection.
The following example of Contains checks whether 10 exists in the collection or not. Please notice that int is a type of generic collection.
*/
//> Example: Contains operator C#
IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
bool result = intList.Contains(10);  // returns false

//! The above example works well with primitive data types. However, it will not work with a custom class.
//!!! Error!! 
IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

Student std = new Student(){ StudentID =3, StudentName = "Bill"};
bool result = studentList.Contains(std);  //returns false

//# the problem is that Contains() method will only compare the reference in this case even though the values are the same but the reference is different.
//> To solve this problem, you can implement IEqualityComparer interface in your custom class and then pass the object of that class as a second parameter to Contains() method for custom equality comparison as shown below.

class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
        {
            if (x.StudentID == y.StudentID && 
                        x.StudentName.ToLower() == y.StudentName.ToLower())
                return true;

            return false;
        }

        public int GetHashCode(Student obj)
        {
            return obj.GetHashCode();
        }
}

//> Now, you can use the above StudentComparer class in second overload method of Contains extension method that accepts second parameter of IEqualityComparer type, as below:

IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

Student std = new Student(){ StudentID =3, StudentName = "Bill"};
bool result = studentList.Contains(std, new StudentComparer()); //returns true

//>  7. Aggregation Operators
/*
> The aggregation operators perform mathematical operations like Average, Aggregate, Count, Max, Min and Sum, on the numeric property of the elements in the collection.
┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Method            ┃ Description                                                      ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Aggregate         ┃ Performs a custom aggregation operation on the values in the     ┃
┃                   ┃ collection (e.g., building a string or custom math).             ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Average           ┃ Calculates the average of the numeric items in the collection.   ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Count             ┃ Counts the elements in a collection (returns an int).            ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ LongCount         ┃ Counts the elements in a collection (returns a long). Useful for ┃
┃                   ┃ very large datasets.                                             ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Max               ┃ Finds the largest value in the collection.                       ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Min               ┃ Finds the smallest value in the collection.                      ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Sum               ┃ Calculates the total sum of the values in the collection.        ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 7.1 Aggregate
The Aggregate method performs an accumulate operation. Aggregate extension method has the following overload methods:
> Aggregate() Overloads:
1. public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func);
2. public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func);
3. public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector);
*/
//> Example: Aggregate in Method Syntax C#

IList<String> strList = new List<String>() { "One", "Two", "Three", "Four", "Five"};

var commaSeperatedString = strList.Aggregate((s1, s2) => s1 + ", " + s2);

Console.WriteLine(commaSeperatedString);

//> Aggregate Method with Seed Value
// The second overload method of Aggregate requires first parameter for seed value to accumulate. Second parameter is Func type delegate:
// TAccumulate Aggregate<TSource, TAccumulate>(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func);.

//> Example: Aggregate with Seed Value C#
// Student collection
IList<Student> studentList = new List<Student>>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
        new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 } 
    };

string commaSeparatedStudentNames = studentList.Aggregate<Student, string>(
                                        "Student Names: ",  // seed value
                                        (str, s) => str += s.StudentName + "," ); 

Console.WriteLine(commaSeparatedStudentNames);

//> Aggregate Method with Result Selector
// Now, let's see third overload method that required the third parameter of the Func delegate expression for result selector, so that you can formulate the result.
//> Example: Aggregate with Result Selector C#
IList<Student> studentList = new List<Student>>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
        new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 } 
    };

string commaSeparatedStudentNames = studentList.Aggregate<Student, string,string>(
                                            String.Empty, // seed value
                                            (str, s) => str += s.StudentName + ",", // returns result using seed value, String.Empty goes to lambda expression as str
                                            str => str.Substring(0,str.Length - 1 )); // result selector that removes last comma

Console.WriteLine(commaSeparatedStudentNames);

/*
> 7.2 Average
Average extension method calculates the average of the numeric items in the collection. Average method returns nullable or non-nullable decimal, double or float value.
The following example demonstrate Average method that returns average value of all the integers in the collection.
*/
//> Example: Average operator C#
IList<int> intList = new List<int>>() { 10, 20, 30 };
var avg = intList.Average();
Console.WriteLine("Average: {0}", avg);
// You can specify an int, decimal, double or float property of a class as a lambda expression of which you want to get an average value.
//> The following example demonstrates Average method on the complex type.
//> Example: Average in Method Syntax C#
IList<Student> studentList = new List<Student>>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
        new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 } 
    };

var avgAge = studentList.Average(s => s.Age);

Console.WriteLine("Average Age of Student: {0}", avgAge);


/*
> 7.3 Count
The Count operator returns the number of elements in the collection or number of elements that have satisfied the given condition.

The Count() extension method has the following two overloads:
> Count() Overloads:
1. int Count<TSource>();
2. int Count<TSource>(Func<TSource, bool> predicate);
The first overload method of Count returns the number of elements in the specified collection, whereas the second overload method returns the number of
elements which have satisfied the specified condition given as lambda expression/predicate function.
*/
//> The following example demonstrates Count() on primitive collection.
//> Example: Count() - C#
IList<int> intList = new List<int>() { 10, 21, 30, 45, 50 };

var totalElements = intList.Count();

Console.WriteLine("Total Elements: {0}", totalElements);

var evenElements = intList.Count(i => i%2 == 0);

Console.WriteLine("Even Elements: {0}", evenElements);

//> The following example demonstrates Count() method on the complex type collection.
//> Example: Count() in C#

IList<Student> studentList = new List<Student>>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
        new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
        new Student() { StudentID = 5, StudentName = "Mathew" , Age = 15 } 
    };

var totalStudents = studentList.Count();

Console.WriteLine("Total Students: {0}", totalStudents);

var adultStudents = studentList.Count(s => s.Age >= 18);

Console.WriteLine("Number of Adult Students: {0}", adultStudents );


//> 7.4 Max
//> The Max() method returns the largest numeric element from a collection.

//> Example: Max method C#
IList<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };

var largest = intList.Max();

Console.WriteLine("Largest Element: {0}", largest);

var largestEvenElements = intList.Max(i => {
			                        if(i%2 == 0)
				                        return i;
			
			                        return 0;
		                        });

Console.WriteLine("Largest Even Element: {0}", largestEvenElements );

//> The following example demonstrates Max() method on the complex type collection.
//> Example: Max in Method Syntax C#
IList<Student> studentList = new List<Student>>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
        new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 } 
    };

var oldest = studentList.Max(s => s.Age);

Console.WriteLine("Oldest Student Age: {0}", oldest);

//> 7.5 Sum
//> The Sum() method calculates the sum of numeric items in the collection.
//> The following example demonstrates Sum() on primitive collection.

IList<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };

var total = intList.Sum();

Console.WriteLine("Sum: {0}", total);

var sumOfEvenElements = intList.Sum(i => {
			                    if(i%2 == 0)
				                    return i;
			
			                    return 0;
		                        });

Console.WriteLine("Sum of Even Elements: {0}", sumOfEvenElements );


//> 8. Element Operators
/*
> Element operators return a particular element from a sequence (collection).

┏━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Method             ┃ Description                                                            ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ElementAt          ┃ Returns the element at a specified index in a collection.              ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ElementAtOrDefault ┃ Returns the element at an index or a default value if out of range.    ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ First              ┃ Returns the first element of a collection or the first that matches.   ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ FirstOrDefault     ┃ Returns the first element or a default value if no match is found.     ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Last               ┃ Returns the last element of a collection or the last that matches.     ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ LastOrDefault      ┃ Returns the last element or a default value if no match is found.      ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Single             ┃ Returns the only element. Throws if there are 0 or >1 elements.        ┃
┣━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ SingleOrDefault    ┃ Returns the only element or default. Throws if there is more than 1.   ┃
┗━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 8.1 ElementAt and ElementAtOrDefault
> The ElementAt() method returns an element from the specified index from a given collection.
If the specified index is out of the range of a collection then it will throw an Index out of range exception. Please note that index is a zero based index.

> The ElementAtOrDefault() method also returns an element from the specified index from a collection
if the specified index is out of range of a collection then it will return a default value of the data type instead of throwing an error.
*/
// > Example: LINQ ElementAt() and ElementAtOrDefault() - C#
IList<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };
IList<string> strList = new List<string>() { "One", "Two", null, "Four", "Five" };

Console.WriteLine("1st Element in intList: {0}", intList.ElementAt(0));
Console.WriteLine("1st Element in strList: {0}", strList.ElementAt(0));
		
Console.WriteLine("2nd Element in intList: {0}", intList.ElementAt(1));
Console.WriteLine("2nd Element in strList: {0}", strList.ElementAt(1));
		
Console.WriteLine("3rd Element in intList: {0}", intList.ElementAtOrDefault(2));
Console.WriteLine("3rd Element in strList: {0}", strList.ElementAtOrDefault(2));

Console.WriteLine("10th Element in intList: {0} - default int value", 
                intList.ElementAtOrDefault(9));		
Console.WriteLine("10th Element in strList: {0} - default string value (null)",
                 strList.ElementAtOrDefault(9));		
		
		
Console.WriteLine("intList.ElementAt(9) throws an exception: Index out of range");
Console.WriteLine("-------------------------------------------------------------");
Console.WriteLine(intList.ElementAt(9));

//> 8.2 First and FirstOrDefault
/*
The First and FirstOrDefault method returns an element from the zeroth index in the collection i.e. the first element.
Also, it returns an element that satisfies the specified condition
┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator          ┃ Description                                                            ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ First             ┃ Returns the first element of a collection, or the first element that   ┃
┃                   ┃ satisfies a condition. Throws an exception if no match is found.       ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ FirstOrDefault    ┃ Returns the first element that satisfies a condition, or a default     ┃
┃                   ┃ value (like null) if no such element exists.                           ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
First and FirstOrDefault has two overload methods. The first overload method doesn't take any input parameter and returns the first element in the collection. 
The second overload method takes the lambda expression as predicate delegate to specify a condition and returns the first element that satisfies the specified condition.
> First() & FirstOrDefault() Overloads:
1. public static TSource First<TSource>(this IEnumerable<TSource> source);
2. public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

1. public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source);
2. public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

The First method returns the first element of a collection, or the first element that satisfies the specified condition using lambda expression or Func delegate.
If a given collection is empty or does not include any element that satisfied the condition then it will throw InvalidOperation exception.
The FirstOrDefault method does the same thing as First method. The only difference is that it returns default value of the data type of a collection if a collection is
empty or doesn't find any element that satisfies the condition.
*/
//> Example: Example: LINQ First() - C# 
IList<int> intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
IList<string> strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
IList<string> emptyList = new List<string>();
		
Console.WriteLine("1st Element in intList: {0}", intList.First());
Console.WriteLine("1st Even Element in intList: {0}", intList.First(i => i % 2 == 0));

Console.WriteLine("1st Element in strList: {0}", strList.First());

Console.WriteLine("emptyList.First() throws an InvalidOperationException");
Console.WriteLine("-------------------------------------------------------------");
Console.WriteLine(emptyList.First());

//> Example: FirstOrDefault() - C#
IList<int> intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
IList<string> strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
IList<string> emptyList = new List<string>();
		
Console.WriteLine("1st Element in intList: {0}", intList.FirstOrDefault());
Console.WriteLine("1st Even Element in intList: {0}",
                                 intList.FirstOrDefault(i =&gt; i % 2 == 0));

Console.WriteLine("1st Element in strList: {0}", strList.FirstOrDefault());

Console.WriteLine("1st Element in emptyList: {0}", emptyList.FirstOrDefault());

//> 8.3 Last and LastOrDefault
/*
The Last() and LastOrDefault() extension methods returns the last element from the collection.

┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator          ┃ Description                                                            ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Last()            ┃ Returns the last element from a collection, or the last element that   ┃
┃                   ┃ satisfies a condition. Throws exception if no element found.           ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ LastOrDefault()   ┃ Returns the last element from a collection, or the last element that   ┃
┃                   ┃ satisfies a condition. Returns a default value if no element found.    ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛

Method Signature:
1. public static TSource Last<TSource>(this IEnumerable<TSource> source);
2. public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
3. public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source);
4. public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

# it is the same as the First and FirstOrDefault method except that it returns the last element from the collection or last element that satisfies the specified condition.
*/

//> 8.4 Single & SingleOrDefault
/*
┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator          ┃ Description                                                                ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Single            ┃ Returns the only element. Throws InvalidOperationException if the          ┃
┃                   ┃ collection is empty OR contains more than one matching element.            ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ SingleOrDefault   ┃ Returns the only element, or a default value if empty. However, it         ┃
┃                   ┃ still throws if more than one matching element is found.                   ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
The Single and SingleOrDefault have two overload methods. The first overload method doesn't take any input parameter and returns a single element in the collection.
The second overload method takes the lambda expression as a predicate delegate that specifies the condition and returns a single element that satisfies the specified condition.
> Single() & SingleOrDefault() Overloads:
1. public static TSource Single<TSource>(this IEnumerable<TSource> source);
2. public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
3. public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source);
4. public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
> Single
The Single returns the only element from a collection, or the only element that satisfies the specified condition.
If a given collection includes no elements or more than one elements then Single throws InvalidOperationException.
> SingleOrDefault
The SingleOrDefault method does the same thing as Single method.
The only difference is that it returns default value of the data type of a collection if a collection is empty, 
includes more than one element or finds no element or more than one element for the specified condition.
*/

//> Example: Single in method syntax C#
IList<int> oneElementList = new List<int>() { 7 };
IList<int> intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
IList<string> strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
IList<string> emptyList = new List<string>();

Console.WriteLine("The only element in oneElementList: {0}", oneElementList.Single());
Console.WriteLine("The only element in oneElementList: {0}",
             oneElementList.SingleOrDefault());

Console.WriteLine("Element in emptyList: {0}", emptyList.SingleOrDefault());

Console.WriteLine("The only element which is less than 10 in intList: {0}",
             intList.Single(i => i &lt; 10));

//Followings throw an exception
//Console.WriteLine("The only Element in intList: {0}", intList.Single());
//Console.WriteLine("The only Element in intList: {0}", intList.SingleOrDefault());
//Console.WriteLine("The only Element in emptyList: {0}", emptyList.Single());

//> 9. LINQ Equality Operator: SequenceEqual
/*

There is only one equality operator: SequenceEqual. 
The SequenceEqual method checks whether the number of elements, value of each element and order of elements in two collections are equal or not.

If the collection contains elements of primitive data types then it compares the values and number of elements,
whereas collection with complex type elements, checks the references of the objects. So, if the objects have the same reference then they considered as 
equal otherwise they are considered not equal.
*/

//> The following example demonstrates the SequenceEqual method with the collection of primitive data types.
//> Example: SequenceEqual in Method Syntax C#
IList<string> strList1 = new List<string>(){"One", "Two", "Three", "Four", "Three"};

IList<string> strList2 = new List<string>(){"One", "Two", "Three", "Four", "Three"};

bool isEqual = strList1.SequenceEqual(strList2); // returns true
Console.WriteLine(isEqual);

//> The SequenceEqual extension method checks the references of two objects to determine whether two sequences are equal or not.
//> This may give wrong result. Consider following example:
//> Example: SequenceEqual in C#
Student std = new Student() { StudentID = 1, StudentName = "Bill" };

IList<Student> studentList1 = new List<Student>(){ std };

IList<Student> studentList2 = new List<Student>(){ std };
       
bool isEqual = studentList1.SequenceEqual(studentList2); // returns true

Student std1 = new Student() { StudentID = 1, StudentName = "Bill" };
Student std2 = new Student() { StudentID = 1, StudentName = "Bill" };

IList<Student> studentList3 = new List<Student>(){ std1};

IList<Student> studentList4 = new List<Student>(){ std2 };
       
isEqual = studentList3.SequenceEqual(studentList4);// returns false
/*
# In the above example, the studentList1 and studentList2 contains the same student object, std. 
# So studentList1.SequenceEqual(studentList2) returns true. But, stdList1 and stdList2 contains two seperate student object, std1 and std2.
# So now, stdList1.SequenceEqual(stdList2) will return false even if std1 and std2 contain the same value.

To compare the values of two collection of complex type (reference type or object), you need to implement IEqualityComparer<T> interface as shown below.

*/
//> Example: IEqualityComparer C#:
class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID && x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.GetHashCode();
    }
}
//! Now, you can use above StudentComparer class in SequenceEqual extension method as a second parameter to compare the values:
//> Example: Compare object type elements using SequenceEqual C#
IList<Student> studentList1 = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

IList<Student> studentList2 = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };
// following returns true
bool isEqual = studentList1.SequenceEqual(studentList2, new StudentComparer());

//> 10. Concatenation Operator: Concat
//> The Concat() method appends two sequences of the same type and returns a new sequence (collection).
//> Example: Concat in C#
IList<string> collection1 = new List<string>() { "One", "Two", "Three" };
IList<string> collection2 = new List<string>() { "Five", "Six"};

var collection3 = collection1.Concat(collection2);

foreach (string str in collection3)
    Console.WriteLine(str);

//> 11. Generation Operator: 
//> 11.1 DefaultIfEmpty
// The DefaultIfEmpty() method returns a new collection with the default value if the given collection on which DefaultIfEmpty() is invoked is empty.
// Another overload method of DefaultIfEmpty() takes a value parameter that should be replaced with default value.
// Consider the following example.

//> Example: DefaultIfEmpty in C#
IList<string> emptyList = new List<string>();

var newList1 = emptyList.DefaultIfEmpty(); 
var newList2 = emptyList.DefaultIfEmpty("None"); 

Console.WriteLine("Count: {0}" , newList1.Count());
Console.WriteLine("Value: {0}" , newList1.ElementAt(0));

Console.WriteLine("Count: {0}" , newList2.Count());
Console.WriteLine("Value: {0}" , newList2.ElementAt(0));

/*
Output:
Count: 1
Value:
Count: 1
Value: None
> In the above example, emptyList.DefaultIfEmpty() returns a new string collection with one element whose value is null because null is a default value of string. 
> Another method emptyList.DefaultIfEmpty("None") returns a string collection with one element whose value is "None" instead of null.
*/

//> 11.2 Empty, Range, Repeat
/*
LINQ includes generation operators DefaultIfEmpty, Empty, Range & Repeat.
! The Empty, Range & Repeat methods are not extension methods for IEnumerable or IQueryable but they are simply static methods defined in a static class Enumerable.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Method        ┃ Description                                                          ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Empty         ┃ Returns an empty collection of the specified type. Useful for        ┃
┃               ┃ returning a "none" result without dealing with null references.      ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Range         ┃ Generates a sequence of integral numbers within a specified range.   ┃
┃               ┃ (e.g., Enumerable.Range(1, 5) creates {1, 2, 3, 4, 5}).              ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Repeat        ┃ Generates a collection that contains one repeated value a specified  ┃
┃               ┃ number of times. (e.g., Repeat("Hi", 3) creates {"Hi", "Hi", "Hi"}). ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 11.2.1 Empty
The Empty() method is not an extension method of IEnumerable or IQueryable like other LINQ methods.
It is a static method included in Enumerable static class. So, you can call it the same way as other static methods like Enumerable.Empty<TResult>().
The Empty() method returns an empty collection of a specified type as shown below.
*/
//> Example: Enumerable.Empty()
var emptyCollection1 = Enumerable.Empty<string>();
var emptyCollection2 = Enumerable.Empty<Student>();

Console.WriteLine("Count: {0} ", emptyCollection1.Count());
Console.WriteLine("Type: {0} ", emptyCollection1.GetType().Name );

Console.WriteLine("Count: {0} ",emptyCollection2.Count());
Console.WriteLine("Type: {0} ", emptyCollection2.GetType().Name );

//> 11.2.2 Range
// The Range() method returns a collection of IEnumerable<T> type with specified number of elements and sequential values starting from the first element.
//> Example: Enumerable.Range()
                            //starting value, number of elements
var intCollection = Enumerable.Range(10, 10);
Console.WriteLine("Total Count: {0} ", intCollection.Count());

for(int i = 0; i < intCollection.Count(); i++)
    Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

//> 11.2.3 Repeat
// The Repeat() method generates a collection of IEnumerable<T> type with specified number of elements and each element contains same specified value.

//>  The Repeat() method generates a collection of IEnumerable<T> type with specified number of elements and each element contains same specified value.

//> Example: Repeat
var intCollection = Enumerable.Repeat<int>(10, 10);
Console.WriteLine("Total Count: {0} ", intCollection.Count());

for(int i = 0; i < intCollection.Count(); i++)
    Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

//# it is the same as range but it returns the same value for each element in the collection whereas Range() returns sequential values for each element in the collection.

//> 12. Set Operator:
/*
The following table lists all Set operators available in LINQ.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Operator      ┃ Usage / Description                                                  ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Distinct      ┃ Removes all duplicate values from a single collection, leaving only  ┃
┃               ┃ unique elements.                                                     ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Except        ┃ Returns the difference between two sequences. It picks elements from ┃
┃               ┃ the first list that do not exist in the second list.                 ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Intersect     ┃ Returns the intersection of two sequences. It only picks elements    ┃
┃               ┃ that are present in both collections.                                ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Union         ┃ Combines two sequences into one and removes duplicates. It returns   ┃
┃               ┃ unique elements that appear in either list.                          ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 12.1 Distinct
The Distinct extension method returns a new collection of unique elements from the given collection.
*/
//> Example: Distinct C#
IList<string> strList = new List<string>(){ "One", "Two", "Three", "Two", "Three" };

IList<int> intList = new List<int>(){ 1, 2, 3, 2, 4, 4, 3, 5 };

var distinctList1 = strList.Distinct();

foreach(var str in distinctList1)
    Console.WriteLine(str);

var distinctList2 = intList.Distinct();

foreach(var i in distinctList2)
    Console.WriteLine(i);

/*
! important 
he Distinct extension method doesn't compare values of complex type objects.
You need to implement IEqualityComparer<T> interface in order to compare the values of complex types.
In the following example, StudentComparer class implements IEqualityComparer<Student> to compare Student objects.
*/
//> Example: Implement IEqualityComparer in C#
public class Student 
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID 
                && x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}
//> Now, you can pass an object of the above StudentComparer class in the Distinct() method as a parameter to compare the Student objects as shown below.
//> Example: Distinct in C#
IList<Student> studentList = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };


var distinctStudents = studentList.Distinct(new StudentComparer()); 

foreach(Student std in distinctStudents)
    Console.WriteLine(std.StudentName);

//> 12.2 Except
/*
The Except() method requires two collections.
It returns a new collection with elements from the first collection which do not exist in the second collection (parameter collection).
*/
//> Example: Except in method syntax C#
IList<string> strList1 = new List<string>(){"One", "Two", "Three", "Four", "Five" };
IList<string> strList2 = new List<string>(){"Four", "Five", "Six", "Seven", "Eight"};

var result = strList1.Except(strList2);

foreach(string str in result)
        Console.WriteLine(str);

/*
! important 
The Except extension method doesn't return the correct result for the collection of complex types.
You need to implement IEqualityComparer interface in order to get the correct result from Except method.
Implement IEqualityComparer interface for Student class as shown below:
*/
//> Example: IEqualityComparer with Except method C#
public class Student 
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID && x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}

IList<Student> studentList1 = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

IList<Student> studentList2 = new List<Student>() { 
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

var resultedCol = studentList1.Except(studentList2,new StudentComparer()); 

foreach(Student std in resultedCol)
    Console.WriteLine(std.StudentName);

//> 12.3 Intersect
/*
The Intersect extension method requires two collections.
It returns a new collection that includes common elements that exists in both the collection. Consider the following example.
*/
//> Example: Intersect in method syntax C#
IList<string> strList1 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
IList<string> strList2 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight"};

var result = strList1.Intersect(strList2);

foreach(string str in result)
        Console.WriteLine(str);

/*
! important 
The Intersect extension method doesn't return the correct result for the collection of complex types.
You need to implement IEqualityComparer interface in order to get the correct result from Intersect method.
Implement IEqualityComparer interface for Student class as shown below:
*/
//> Example: Use IEqualityComparer with Intersect in C#

public class Student 
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID && 
                        x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}
IList<Student> studentList1 = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

IList<Student> studentList2 = new List<Student>() { 
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

var resultedCol = studentList1.Intersect(studentList2, new StudentComparer()); 

foreach(Student std in resultedCol)
    Console.WriteLine(std.StudentName);

//> 12.4 Union
/*
The Union extension method requires two collections and returns a new collection that includes distinct elements from both the collections. Consider the following example.
*/
//> Example: Union in method syntax C#
IList<string> strList1 = new List<string>() { "One", "Two", "three", "Four" };
IList<string> strList2 = new List<string>() { "Two", "THREE", "Four", "Five" };

var result = strList1.Union(strList2);

foreach(string str in result)
        Console.WriteLine(str);

/*
! important 
The Union extension method doesn't return the correct result for the collection of complex types.
You need to implement IEqualityComparer interface in order to get the correct result from Union method.
Implement IEqualityComparer interface for Student class as below:
*/
//> Example: Union operator with IEqualityComparer:
public class Student 
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID && x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}
IList<Student> studentList1 = new List<Student>() { 
        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

IList<Student> studentList2 = new List<Student>() { 
        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 } 
    };

var resultedCol = studentList1.Union(studentList2, new StudentComparer()); 

foreach(Student std in resultedCol)
    Console.WriteLine(std.StudentName);


//> 13. Partitioning Operators

/*
The partitioning operators split the sequence (collection) into two parts and return one of the parts.
┏━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Method        ┃ Description                                                                ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Skip          ┃ Skips elements up to a specified position starting from the first element. ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ SkipWhile     ┃ Skips elements based on a condition until an element does not satisfy it.  ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Take          ┃ Takes elements up to a specified position starting from the first element. ┃
┣━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ TakeWhile     ┃ Returns elements until an element does not satisfy the condition.          ┃
┗━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
*/

//> 13.1 Skip and SkipWhile
// The Skip() method skips the specified number of element starting from first element and returns rest of the elements.
//> Example: Skip() - C#
IList<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var skipResult = numbers.Skip(3); // Skip first three elements

foreach(int num in skipResult)
    Console.WriteLine(num);
// Output: 4,5,6,7,8,9,10

//> the SkipWhile 
/*
As the name suggests, the SkipWhile() extension method in LINQ skip elements in the collection till the specified condition is true..
It returns a new collection that includes all the remaining elements once the specified condition becomes false for any element.
The SkipWhile() method has two overload methods. 
One method accepts the predicate of Func<TSource, bool> type and other overload method accepts the predicate Func<TSource, int, bool> type that pass the index of an element.
In the following example, SkipWhile() method skips all elements till it finds a string whose length is equal or more than 4 characters.
*/

IList<string> strList = new List<string>() { 
                                            "One", 
                                            "Two", 
                                            "Three", 
                                            "Four", 
                                            "Five", 
                                            "Six"  };

var resultList = strList.SkipWhile(s => s.Length < 4);

foreach(string str in resultList)
        Console.WriteLine(str); // output => Three Four Five Six


//> 13.2 Take and TakeWhile
/*
> Take
The partitioning operators split the sequence (collection) into two parts and returns one of the parts.
The Take() extension method returns the specified number of elements starting from the first element.
The Take & TakeWhile operator is Not Supported in C# query syntax. 
However, you can use Take/TakeWhile method on query variable or wrap whole query into brackets and then call Take/TakeWhile.
*/
//> Example: Take() in C#
IList<string> strList = new List<string>(){ "One", "Two", "Three", "Four", "Five" };

var newList = strList.Take(2);

foreach(var str in newList)
    Console.WriteLine(str); // output => One Two

//> TakeWhile
/*
The TakeWhile() extension method returns elements from the given collection until the specified condition is true.
If the first element itself doesn't satisfy the condition then returns an empty collection.
The TakeWhile method has two overload methods.
One method accepts the predicate of Func<TSource, bool> type and the other overload method accepts the predicate Func<TSource, int, bool> type that passes the index of element.
In the following example, TakeWhile() method returns a new collection that includes all the elements till it finds a string whose length less than 4 characters.
*/
//> Example: TakeWhile in C#
IList<string> strList = new List<string>() { 
                                            "Three", 
                                            "Four", 
                                            "Five", 
                                            "Hundred"  };

var result = strList.TakeWhile(s => s.Length > 4);

foreach(string str in result)
        Console.WriteLine(str);

//> 14. Conversion Operators
/*
The Conversion operators in LINQ are useful in converting the type of the elements in a sequence (collection).
There are three types of conversion operators: 
1. As operators (AsEnumerable and AsQueryable).
2. To operators (ToArray, ToDictionary, ToList and ToLookup).
3. Casting operators (Cast and OfType).
The following table lists all the conversion operators.
┏━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃ Method            ┃ Description                                                      ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ AsEnumerable      ┃ Returns the sequence as IEnumerable<T>. Forces local evaluation. ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ AsQueryable       ┃ Converts IEnumerable to IQueryable for remote query providers.   ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ Cast              ┃ Converts non-generic to generic collection. Throws on failure.   ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ OfType            ┃ Filters a collection based on type; skips incompatible elements. ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ToArray           ┃ Executes query immediately and converts to an Array.             ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ToDictionary      ┃ Creates a Dictionary based on key selector functions.            ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ToList            ┃ Executes query immediately and converts to a List<T>.            ┃
┣━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┫
┃ ToLookup          ┃ Groups elements into a 1:N Lookup; execution is immediate.       ┃
┗━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
> 14.1 AsEnumerable & AsQueryable
The AsEnumerable and AsQueryable methods cast or convert a source object to IEnumerable<T> or IQueryable<T> respectively.
*/
//> Example: AsEnumerable & AsQueryable operator in C#:
class Program
{

    static void ReportTypeProperties<T>(T obj)
    {
        Console.WriteLine("Compile-time type: {0}", typeof(T).Name);
        Console.WriteLine("Actual type: {0}", obj.GetType().Name);
    }

    static void Main(string[] args)
    {
        Student[] studentArray = { 
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 } ,
            };   
            
        ReportTypeProperties( studentArray);
        ReportTypeProperties(studentArray.AsEnumerable());
        ReportTypeProperties(studentArray.AsQueryable());   
    }
}
//! As you can see in the above example AsEnumerable and AsQueryable methods convert compile time type to IEnumerable and IQueryable respectively

//> 14.2 Cast
// Cast does the same thing as AsEnumerable<T>. It cast the source object into IEnumerable<T>.
//> Example: Cast operator in C#
class Program
{

    static void ReportTypeProperties<T>(T obj)
    {
        Console.WriteLine("Compile-time type: {0}", typeof(T).Name);
        Console.WriteLine("Actual type: {0}", obj.GetType().Name);
    }

    static void Main(string[] args)
    {
        Student[] studentArray = { 
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 } ,
            };   

        ReportTypeProperties(studentArray);
        ReportTypeProperties(studentArray.Cast<Student>());
    }
}
//! As you can see in the above example, studentArray.Cast<Student>() is the same as (IEnumerable<Student>)studentArray but Cast<Student>() is more readable.

//> 14.3 To Operators: ToArray(), ToList(), ToDictionary()
/*
As the name suggests, ToArray(), ToList(), ToDictionary() method converts a source object into an array, List or Dictionary respectively.
To operators force the execution of the query. It forces the remote query provider to execute a query and get the result from the underlying data source e.g. SQL Server database.
*/
IList<string> strList = new List<string>() { 
                                            "One", 
                                            "Two", 
                                            "Three", 
                                            "Four", 
                                            "Three" 
                                            };

string[] strArray = strList.ToArray<string>();// converts List to Array

IList<string> list = strArray.ToList<string>(); // converts array into list


//> ToDictionary - Converts a Generic list to a generic dictionary:
//> Example: ToDictionary in C#:
IList<Student> studentList = new List<Student>() { 
                    new Student() { StudentID = 1, StudentName = "John", age = 18 } ,
                    new Student() { StudentID = 2, StudentName = "Steve",  age = 21 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  age = 18 } ,
                    new Student() { StudentID = 4, StudentName = "Ram" , age = 20 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , age = 21 } 
                };

//following converts list into dictionary where StudentId is a key
IDictionary<int, Student> studentDict = 
                                studentList.ToDictionary<Student, int>(s => s.StudentID); 

foreach(var key in studentDict.Keys)
	Console.WriteLine("Key: {0}, Value: {1}", 
                                key, (studentDict[key] as Student).StudentName);