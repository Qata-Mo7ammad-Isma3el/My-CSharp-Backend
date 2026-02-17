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






