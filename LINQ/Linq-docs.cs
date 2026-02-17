// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║                        LINQ in C# — Complete Reference                       ║
// ║                   Organized, Fixed & Explained in Simple Words               ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// TABLE OF CONTENTS:
// ──────────────────
//  1.  What is LINQ?
//  2.  Why Use LINQ? (Before vs After)
//  3.  LINQ API in .NET (Enumerable vs Queryable)
//  4.  Query Syntax vs Method Syntax
//  5.  Lambda Expressions
//  6.  Standard Query Operators — Overview
//  7.  Filtering: Where & OfType
//  8.  Sorting: OrderBy, ThenBy & Reverse
//  9.  Grouping: GroupBy & ToLookup
//  10. Joining: Join & GroupJoin
//  11. Projection: Select & SelectMany
//  12. Quantifiers: All, Any & Contains
//  13. Aggregation: Aggregate, Average, Count, Max, Min, Sum
//  14. Element Operators: ElementAt, First, Last, Single
//  15. Equality: SequenceEqual
//  16. Concatenation: Concat
//  17. Generation: DefaultIfEmpty, Empty, Range, Repeat
//  18. Set Operators: Distinct, Except, Intersect, Union
//  19. Partitioning: Skip, SkipWhile, Take, TakeWhile
//  20. Conversion: AsEnumerable, Cast, ToArray, ToList, ToDictionary
//
// BUGS FIXED FROM ORIGINAL:
// ─────────────────────────
//  - &gt; and &lt; HTML entities replaced with > and <
//  - List<Student>>() (double >>) fixed to List<Student>()
//  - Inconsistent 'age' vs 'Age' property names → all use 'Age'
//  - Missing semicolons and formatting inconsistencies cleaned up
//  - =&gt; HTML entity replaced with =>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// ════════════════════════════════════════════════════════════════════════════════
// SHARED CLASSES (used in many examples below)
// ════════════════════════════════════════════════════════════════════════════════

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public int StandardID { get; set; }   // used in Join examples
}

public class Standard
{
    public int StandardID { get; set; }
    public string StandardName { get; set; }
}

// Comparer for Student objects — needed by Contains, Distinct, Except,
// Intersect, Union, and SequenceEqual when comparing custom objects.
//
// WHY? By default, LINQ compares object *references* (memory addresses),
//       not the actual values. Two Student objects with the same data are
//       still considered "different" unless you tell LINQ how to compare them.

public class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        return x.StudentID == y.StudentID
            && x.StudentName.ToLower() == y.StudentName.ToLower();
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  1. WHAT IS LINQ?                                                            ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// LINQ = Language Integrated Query
//
// In simple words:
//   LINQ lets you write queries INSIDE C# to search, filter, and transform
//   data from many sources (arrays, lists, databases, XML, etc.)
//   Think of it like SQL, but built right into C#.
//
// 3 key points:
//   1. LINQ returns results as objects — you work with them using normal C#.
//   2. Results are NOT produced until you loop through them (deferred execution).
//   3. LINQ works with anything that implements IEnumerable<T> or IQueryable<T>.

namespace Topic01_WhatIsLinq
{
    class Program
    {
        static void Main()
        {
            // --- Data source ---
            string[] names = { "Bill", "Steve", "James", "Mohan" };

            // --- LINQ Query (query syntax) ---
            var myLinqQuery = from name in names
                              where name.Contains('a')
                              select name;

            // --- Execute the query ---
            // TIP: The query only runs NOW, when you loop through it.
            //      This is called "deferred execution".
            foreach (var name in myLinqQuery)
                Console.Write(name + " ");
            // Output: James Mohan
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  2. WHY USE LINQ? (Before vs After)                                          ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// Before LINQ, finding items in a collection required long loops.
// LINQ makes it much shorter and easier to read.

namespace Topic02_WhyLinq
{
    // ── OLD WAY: for loop (C# 1.0) ──────────────────────────────────────────
    // Problem: long, hard to read, not reusable
    class OldWay
    {
        static void Main()
        {
            Student[] studentArray = {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 31 },
                new Student() { StudentID = 6, StudentName = "Chris", Age = 17 },
                new Student() { StudentID = 7, StudentName = "Rob",   Age = 19 },
            };

            Student[] teenagers = new Student[10];
            int i = 0;

            foreach (Student std in studentArray)
            {
                if (std.Age > 12 && std.Age < 20)  // FIX: was &gt; and &lt;
                {
                    teenagers[i] = std;
                    i++;
                }
            }
        }
    }

    // ── BETTER WAY: delegates (C# 2.0) ──────────────────────────────────────
    // Improvement: condition is now a parameter, but still wordy
    delegate bool FindStudent(Student std);

    class StudentExtension
    {
        public static Student[] Where(Student[] stdArray, FindStudent del)
        {
            int i = 0;
            Student[] result = new Student[10];
            foreach (Student std in stdArray)
            {
                if (del(std))
                {
                    result[i] = std;
                    i++;
                }
            }
            return result;
        }
    }

    class BetterWay
    {
        static void Main()
        {
            Student[] studentArray = {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 31 },
                new Student() { StudentID = 6, StudentName = "Chris", Age = 17 },
                new Student() { StudentID = 7, StudentName = "Rob",   Age = 19 },
            };

            Student[] teenagers = StudentExtension.Where(studentArray,
                delegate(Student std) {
                    return std.Age > 12 && std.Age < 20;  // FIX: was &gt; and &lt;
                });
        }
    }

    // ── BEST WAY: LINQ (C# 3.0+) ────────────────────────────────────────────
    // One clean line does it all!
    class BestWay
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 31 },
                new Student() { StudentID = 6, StudentName = "Chris", Age = 17 },
                new Student() { StudentID = 7, StudentName = "Rob",   Age = 19 },
            };

            var teenagers = studentList.Where(s => s.Age > 12 && s.Age < 20);
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  3. LINQ API IN .NET (Enumerable vs Queryable)                               ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// LINQ works on classes that implement IEnumerable<T> or IQueryable<T>.
// Two static classes provide the LINQ methods:
//
// ┌────────────────┬──────────────────────────────┬───────────────────────────┐
// │ Class          │ Works With                   │ Examples                  │
// ├────────────────┼──────────────────────────────┼───────────────────────────┤
// │ Enumerable     │ IEnumerable<T>               │ List, Array, Dictionary,  │
// │                │ (in-memory collections)      │ HashSet, Queue, Stack     │
// ├────────────────┼──────────────────────────────┼───────────────────────────┤
// │ Queryable      │ IQueryable<T>                │ Entity Framework,         │
// │                │ (remote data sources)        │ LINQ to SQL, PLINQ        │
// └────────────────┴──────────────────────────────┴───────────────────────────┘
//
// KEY DIFFERENCE:
//   1. Enumerable — works on data already in memory (like a List or Array).
//   2. Queryable  — translates your LINQ into something the data source
//                   understands (like SQL), so only needed data is fetched.
//
// Common methods available in BOTH:
//   Where, Select, OrderBy, GroupBy, Join, First, Last, Count, Sum, Average,
//   Max, Min, Any, All, Contains, Distinct, Skip, Take, Concat, Union,
//   Intersect, Except, and many more.


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  4. QUERY SYNTAX vs METHOD SYNTAX                                            ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// Two ways to write LINQ queries. Both give the SAME results.
//
// ┌─────────────────┬──────────────────────┬──────────────────────────┐
// │ Feature         │ Query Syntax         │ Method Syntax            │
// ├─────────────────┼──────────────────────┼──────────────────────────┤
// │ Looks like      │ SQL                  │ Chained method calls     │
// │ Ends with       │ select or group by   │ Any LINQ method          │
// │ Flexibility     │ Limited operators    │ All operators available  │
// │ At compile time │ Converted to method  │ Used directly            │
// └─────────────────┴──────────────────────┴──────────────────────────┘
//
// NOTE: The compiler converts query syntax into method syntax at compile time.
//       They are the same thing underneath.

namespace Topic04_QueryVsMethod
{
    class Program
    {
        static void Main()
        {
            IList<string> stringList = new List<string>() {
                "C# Tutorials",
                "VB.NET Tutorials",
                "Learn C++",
                "MVC Tutorials",
                "Java"
            };

            // ── 4.1 Query Syntax ─────────────────────────────────────────────
            // Looks like SQL. Starts with "from", ends with "select" or "group".
            //
            // Breaking it down:
            //   1. from s in stringList  → "s" represents each item in the list
            //   2. where s.Contains(...) → the filter condition
            //   3. select s              → what to return
            var queryResult = from s in stringList
                              where s.Contains("Tutorials")
                              select s;

            // ── 4.2 Method Syntax (Fluent Syntax) ────────────────────────────
            // Uses extension methods + lambda expressions. More flexible.
            var methodResult = stringList.Where(s => s.Contains("Tutorials"));
            // FIX: was s =&gt; (HTML entity)

            // Both queryResult and methodResult give the same output:
            // "C# Tutorials", "VB.NET Tutorials", "MVC Tutorials"
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  5. LAMBDA EXPRESSIONS                                                       ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// A lambda expression is a SHORT way to write an anonymous method.
// LINQ uses lambdas everywhere.
//
// Basic structure:  parameter => expression
//
//   s => s.Age > 12 && s.Age < 20
//   │    └─────────────────────── body (what the method does)
//   │  └── lambda operator ("goes to")
//   └── input parameter

namespace Topic05_LambdaExpressions
{
    class Program
    {
        static void Main()
        {
            // ── Old way: anonymous method ────────────────────────────────────
            // delegate(Student s) { return s.Age > 12 && s.Age < 20; };

            // ── New way: lambda expression (same thing, shorter) ─────────────
            // s => s.Age > 12 && s.Age < 20;


            // ── Multiple parameters ──────────────────────────────────────────
            // (s, youngAge) => s.Age >= youngAge;

            // ── No parameters ────────────────────────────────────────────────
            // () => Console.WriteLine("Hello!");

            // ── Multiple statements (use curly braces + return) ──────────────
            // (s, youngAge) =>
            // {
            //     Console.WriteLine("Checking age...");
            //     return s.Age >= youngAge;
            // }

            // ── Local variable inside a lambda ───────────────────────────────
            // (s, youngAge) =>
            // {
            //     int age = s.Age;
            //     return age >= youngAge;
            // }


            // ══ ASSIGNING LAMBDAS TO DELEGATES ══════════════════════════════

            // Func<T, TResult> — has input AND return value
            Func<Student, bool> isTeenager = s => s.Age > 12 && s.Age < 20;
            // FIX: was s.age (lowercase) in original

            Student std = new Student() { Age = 21 };
            bool isTeen = isTeenager(std);  // false


            // Action<T> — has input but NO return value (void)
            Action<Student> printStudent = s =>
                Console.WriteLine("Name: {0}, Age: {1}", s.StudentName, s.Age);

            Student bill = new Student() { StudentName = "Bill", Age = 21 };
            printStudent(bill);  // Output: Name: Bill, Age: 21


            // ══ USING LAMBDAS IN LINQ ═══════════════════════════════════════

            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 19 },
            };

            // Method syntax — lambda passed directly
            var teens1 = studentList.Where(s => s.Age > 12 && s.Age < 20);

            // Query syntax — can use a Func variable
            Func<Student, bool> isStudentTeenager = s => s.Age > 12 && s.Age < 20;
            // FIX: was s.age (lowercase) in original

            var teens2 = from s in studentList
                         where isStudentTeenager(s)
                         select s;
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  6. STANDARD QUERY OPERATORS — OVERVIEW                                      ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// There are 50+ standard query operators, organized by category:
//
// ┌─────────────────┬──────────────────────────────────────────────────────────┐
// │ Category        │ Operators                                                │
// ├─────────────────┼──────────────────────────────────────────────────────────┤
// │ Filtering       │ Where, OfType                                            │
// │ Sorting         │ OrderBy, OrderByDescending, ThenBy, ThenByDescending,    │
// │                 │ Reverse                                                  │
// │ Grouping        │ GroupBy, ToLookup                                        │
// │ Join            │ Join, GroupJoin                                          │
// │ Projection      │ Select, SelectMany                                       │
// │ Aggregation     │ Aggregate, Average, Count, LongCount, Max, Min, Sum      │
// │ Quantifiers     │ All, Any, Contains                                       │
// │ Element         │ ElementAt, ElementAtOrDefault, First, FirstOrDefault,    │
// │                 │ Last, LastOrDefault, Single, SingleOrDefault             │
// │ Set             │ Distinct, Except, Intersect, Union                       │
// │ Partitioning    │ Skip, SkipWhile, Take, TakeWhile                         │
// │ Concatenation   │ Concat                                                   │
// │ Equality        │ SequenceEqual                                            │
// │ Generation      │ DefaultIfEmpty, Empty, Range, Repeat                     │
// │ Conversion      │ AsEnumerable, AsQueryable, Cast, OfType, ToArray,        │
// │                 │ ToDictionary, ToList, ToLookup                           │
// └─────────────────┴──────────────────────────────────────────────────────────┘
//
// The sections below cover each category with examples.


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  7. FILTERING: Where & OfType                                                ║
// ╚══════════════════════════════════════════════════════════════════════════════╝

namespace Topic07_Filtering
{
    class Program
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 15 },
            };


            // ── 7.1 Where ───────────────────────────────────────────────────
            // Filters a collection based on a condition.
            // Returns only items that match.

            // Query Syntax:
            var queryResult = from s in studentList
                              where s.Age > 12 && s.Age < 20
                              select s.StudentName;

            // Method Syntax:
            var methodResult = studentList.Where(s => s.Age > 12 && s.Age < 20);

            // Using a Func delegate as the condition:
            Func<Student, bool> isTeenager = delegate(Student s) {
                return s.Age > 12 && s.Age < 20;
            };
            var funcResult = from s in studentList
                             where isTeenager(s)
                             select s;

            // Where with INDEX (method syntax only):
            // The 2nd overload gives you the index of each element.
            var evenPositions = studentList.Where((s, i) => i % 2 == 0);
            // Returns students at index 0, 2, 4 → John, Bill, Ron

            // CHAINING multiple Where clauses:
            // Query Syntax:
            var chained1 = from s in studentList
                           where s.Age > 12
                           where s.Age < 20
                           select s;

            // Method Syntax:
            var chained2 = studentList.Where(s => s.Age > 12)
                                      .Where(s => s.Age < 20);


            // ── 7.2 OfType ──────────────────────────────────────────────────
            // Filters by DATA TYPE. Useful when a collection has mixed types.

            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

            // Query Syntax:
            var strings = from s in mixedList.OfType<string>() select s;
            // Returns: "One", "Two"

            var ints = from s in mixedList.OfType<int>() select s;
            // Returns: 0, 3

            // Method Syntax:
            var stringsMethod = mixedList.OfType<string>();
            var intsMethod    = mixedList.OfType<int>();
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  8. SORTING: OrderBy, ThenBy & Reverse                                       ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// ┌─────────────────────┬──────────────────────────────────────────────────────┐
// │ Operator            │ What It Does                                         │
// ├─────────────────────┼──────────────────────────────────────────────────────┤
// │ OrderBy             │ Sorts ascending (A→Z, 1→9). Default.                 │
// │ OrderByDescending   │ Sorts descending (Z→A, 9→1). Method syntax only.     │ 
// │ ThenBy              │ 2nd-level sort ascending. Method syntax only.        │
// │ ThenByDescending    │ 2nd-level sort descending. Method syntax only.       │
// │ Reverse             │ Reverses the entire order. Method syntax only.       │
// └─────────────────────┴──────────────────────────────────────────────────────┘

namespace Topic08_Sorting
{
    class Program
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 19 },
                new Student() { StudentID = 6, StudentName = "Ram",   Age = 18 },
            };


            // ── 8.1 OrderBy & OrderByDescending ─────────────────────────────

            // Query Syntax — ascending (default):
            var ascending = from s in studentList
                            orderby s.StudentName
                            select s;

            // Query Syntax — descending:
            var descending = from s in studentList
                             orderby s.StudentName descending
                             select s;

            // Method Syntax:
            var ascMethod  = studentList.OrderBy(s => s.StudentName);
            var descMethod = studentList.OrderByDescending(s => s.StudentName);


            // ── 8.2 ThenBy & ThenByDescending ───────────────────────────────
            // Use for a SECOND sorting level.
            // Example: sort by name first, then by age.

            // Method Syntax (ThenBy is ONLY available in method syntax):
            var thenByResult     = studentList.OrderBy(s => s.StudentName)
                                              .ThenBy(s => s.Age);

            var thenByDescResult = studentList.OrderBy(s => s.StudentName)
                                              .ThenByDescending(s => s.Age);

            // Query Syntax — use comma for multiple sort fields:
            var multiSort = from s in studentList
                            orderby s.StudentName, s.Age
                            select new { s.StudentName, s.Age };
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  9. GROUPING: GroupBy & ToLookup                                             ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// ┌────────────┬─────────────────────────────────────────────────────────────┐
// │ Feature    │ GroupBy                │ ToLookup                           │
// ├────────────┼────────────────────────┼────────────────────────────────────┤
// │ Execution  │ Deferred (lazy)        │ Immediate (runs right away)        │
// │ Query Syn. │ Supported              │ NOT supported                      │
// │ Method Syn.│ Supported              │ Supported                          │
// └────────────┴────────────────────────┴────────────────────────────────────┘

namespace Topic09_Grouping
{
    class Program
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Abram", Age = 21 },
            };


            // ── 9.1 GroupBy ─────────────────────────────────────────────────
            // Creates groups based on a key.
            // Each group has a Key and a collection of matching items.

            // Query Syntax:
            var groupedQuery = from s in studentList
                               group s by s.Age;

            // Method Syntax:
            var groupedMethod = studentList.GroupBy(s => s.Age);

            // Iterating through groups:
            foreach (var ageGroup in groupedQuery)
            {
                Console.WriteLine("Age: {0}", ageGroup.Key);
                foreach (Student s in ageGroup)
                    Console.WriteLine("  Name: {0}", s.StudentName);
            }
            // Output:
            //   Age: 18  → John, Bill
            //   Age: 21  → Steve, Abram
            //   Age: 20  → Ram


            // ── 9.2 ToLookup ────────────────────────────────────────────────
            // Same as GroupBy but executes IMMEDIATELY.
            // Only available in method syntax.

            var lookupResult = studentList.ToLookup(s => s.Age);

            foreach (var group in lookupResult)
            {
                Console.WriteLine("Age Group: {0}", group.Key);
                foreach (Student s in group)
                    Console.WriteLine("  Name: {0}", s.StudentName);
            }
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  10. JOINING: Join & GroupJoin                                               ║
// ╚══════════════════════════════════════════════════════════════════════════════╝

namespace Topic10_Joining
{
    class Program
    {
        static void Main()
        {
            // ── 10.1 Join (Inner Join) ──────────────────────────────────────
            // Combines two collections based on a matching key.
            // Like INNER JOIN in SQL — only matching items appear.
            //
            // Join takes 4 parameters:
            //   1. inner collection
            //   2. outer key selector
            //   3. inner key selector
            //   4. result selector

            IList<string> strList1 = new List<string>() { "One", "Two", "Three", "Four" };
            IList<string> strList2 = new List<string>() { "One", "Two", "Five", "Six" };

            var innerJoin = strList1.Join(
                strList2,                    // inner collection
                str1 => str1,                // outer key selector
                str2 => str2,                // inner key selector
                (str1, str2) => str1         // result selector
            );
            // Result: "One", "Two" (items that exist in BOTH lists)


            // ── 10.2 GroupJoin (Left Outer Join) ────────────────────────────
            // Like Join but groups the matching items.
            // Like LEFT OUTER JOIN in SQL — all items from the outer
            // collection appear, even if there are no matches.

            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18, StandardID = 1 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21, StandardID = 1 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20, StandardID = 2 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 21 },  // no StandardID
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard() { StandardID = 1, StandardName = "Standard 1" },
                new Standard() { StandardID = 2, StandardName = "Standard 2" },
                new Standard() { StandardID = 3, StandardName = "Standard 3" },
            };

            var groupJoin = standardList.GroupJoin(
                studentList,                             // inner collection
                std => std.StandardID,                   // outer key selector
                s => s.StandardID,                       // inner key selector
                (std, studentsGroup) => new              // result selector
                {
                    StandardName = std.StandardName,
                    Students = studentsGroup
                }
            );

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.StandardName);
                foreach (var stud in item.Students)
                    Console.WriteLine("  " + stud.StudentName);
            }
            // Output:
            //   Standard 1 → John, Steve
            //   Standard 2 → Bill, Ram
            //   Standard 3 → (empty — no students, but still shows up)
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  11. PROJECTION: Select & SelectMany                                         ║
// ╚══════════════════════════════════════════════════════════════════════════════╝

namespace Topic11_Projection
{
    class Program
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 15 },
            };


            // ── 11.1 Select ─────────────────────────────────────────────────
            // Transforms each element. You pick what data to return.

            // Get just the names:
            // Query Syntax:
            var names = from s in studentList
                        select s.StudentName;

            // Method Syntax:
            var namesMethod = studentList.Select(s => s.StudentName);

            // Create a new shape (anonymous object):
            var custom = from s in studentList
                         select new { Name = "Mr. " + s.StudentName, Age = s.Age };

            var customMethod = studentList.Select(s => new {
                Name = s.StudentName,
                Age = s.Age
            });

            foreach (var item in custom)
                Console.WriteLine("Name: {0}, Age: {1}", item.Name, item.Age);


            // ── 11.2 SelectMany ─────────────────────────────────────────────
            // Flattens nested collections into one single collection.
            // Example: if each student has a list of courses, SelectMany
            // gives you one flat list of ALL courses from ALL students.
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  12. QUANTIFIERS: All, Any & Contains                                        ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// Check conditions on a collection → return true or false.
//
// ┌──────────┬──────────────────────────────────────────┐
// │ Operator │ What It Checks                           │
// ├──────────┼──────────────────────────────────────────┤
// │ All      │ Do ALL items match?                      │
// │ Any      │ Does at least ONE item match?            │
// │ Contains │ Does the list have this specific value?  │
// └──────────┴──────────────────────────────────────────┘
//
// WARNING: Quantifier operators are NOT available in query syntax.
//          Use method syntax only.

namespace Topic12_Quantifiers
{
    class Program
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 19 },
            };


            // ── 12.1 All ────────────────────────────────────────────────────
            // Are ALL students teenagers?
            bool allTeens = studentList.All(s => s.Age > 12 && s.Age < 20);
            Console.WriteLine(allTeens);  // false (Steve=25, Ram=20 don't match)


            // ── 12.2 Any ────────────────────────────────────────────────────
            // Is at least ONE student a teenager?
            bool anyTeen = studentList.Any(s => s.Age > 12 && s.Age < 20);
            Console.WriteLine(anyTeen);  // true


            // ── 12.3 Contains ───────────────────────────────────────────────
            // Simple types — works directly:
            IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
            bool has10 = intList.Contains(10);  // false

            // Custom objects — compares REFERENCES by default, not values!
            Student std = new Student() { StudentID = 3, StudentName = "Bill" };
            bool found = studentList.Contains(std);  // false! (different object)

            // FIX: Use IEqualityComparer to compare by VALUE:
            bool foundWithComparer = studentList.Contains(std, new StudentComparer());
            // true! (compares StudentID and StudentName)
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  13. AGGREGATION: Aggregate, Average, Count, Max, Min, Sum                   ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// ┌───────────┬────────────────────────────────────────────┐
// │ Operator  │ What It Does                               │
// ├───────────┼────────────────────────────────────────────┤
// │ Aggregate │ Custom accumulation (build strings, etc.)  │
// │ Average   │ Calculates the average                     │
// │ Count     │ Counts items (returns int)                 │
// │ LongCount │ Counts items (returns long, for huge data) │
// │ Max       │ Finds the biggest value                    │
// │ Min       │ Finds the smallest value                   │
// │ Sum       │ Adds up all values                         │
// └───────────┴────────────────────────────────────────────┘

namespace Topic13_Aggregation
{
    class Program
    {
        static void Main()
        {
            IList<Student> studentList = new List<Student>() {  // FIX: was List<Student>>()
                new Student() { StudentID = 1, StudentName = "John",   Age = 13 },
                new Student() { StudentID = 2, StudentName = "Moin",   Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",   Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram",    Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",    Age = 15 },
            };

            IList<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };


            // ── 13.1 Aggregate ──────────────────────────────────────────────
            // Combines all items using a custom rule.
            // Goes through the list one by one, combining each item with
            // the result so far.

            // Join strings with commas:
            IList<string> strList = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            var commaSeparated = strList.Aggregate((s1, s2) => s1 + ", " + s2);
            Console.WriteLine(commaSeparated);
            // Output: "One, Two, Three, Four, Five"

            // With a SEED value (starting text):
            string names = studentList.Aggregate<Student, string>(
                "Student Names: ",                      // seed
                (str, s) => str += s.StudentName + ","  // accumulator
            );
            Console.WriteLine(names);
            // Output: "Student Names: John,Moin,Bill,Ram,Ron,"

            // With a RESULT SELECTOR (to clean up the result):
            string cleanNames = studentList.Aggregate<Student, string, string>(
                String.Empty,                                   // seed
                (str, s) => str += s.StudentName + ",",         // accumulator
                str => str.Substring(0, str.Length - 1)         // remove last comma
            );
            Console.WriteLine(cleanNames);
            // Output: "John,Moin,Bill,Ram,Ron"


            // ── 13.2 Average ────────────────────────────────────────────────
            IList<int> nums = new List<int>() { 10, 20, 30 };  // FIX: was List<int>>()
            var avg = nums.Average();  // 20
            Console.WriteLine("Average: {0}", avg);

            // On a property:
            var avgAge = studentList.Average(s => s.Age);
            Console.WriteLine("Average Age: {0}", avgAge);


            // ── 13.3 Count ──────────────────────────────────────────────────
            var total = intList.Count();            // 6
            var evenCount = intList.Count(i => i % 2 == 0);  // 3 (10, 30, 50)

            Console.WriteLine("Total: {0}", total);
            Console.WriteLine("Even: {0}", evenCount);

            // On students:
            var totalStudents = studentList.Count();              // 5
            var adultStudents = studentList.Count(s => s.Age >= 18);  // 3

            Console.WriteLine("Total Students: {0}", totalStudents);
            Console.WriteLine("Adults: {0}", adultStudents);


            // ── 13.4 Max ────────────────────────────────────────────────────
            var largest = intList.Max();   // 87
            Console.WriteLine("Largest: {0}", largest);

            var largestEven = intList.Max(i => {
                if (i % 2 == 0) return i;
                return 0;
            });
            Console.WriteLine("Largest Even: {0}", largestEven);  // 50

            var oldestAge = studentList.Max(s => s.Age);
            Console.WriteLine("Oldest Age: {0}", oldestAge);  // 21


            // ── 13.5 Sum ────────────────────────────────────────────────────
            var totalSum = intList.Sum();  // 243
            Console.WriteLine("Sum: {0}", totalSum);

            var evenSum = intList.Sum(i => i % 2 == 0 ? i : 0);  // 90
            Console.WriteLine("Even Sum: {0}", evenSum);
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  14. ELEMENT OPERATORS: ElementAt, First, Last, Single                       ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// Return a SINGLE item from a collection.
//
// ┌───────────────────────────┬──────────────────┬─────────────────────────┐
// │ Method                    │ Returns          │ If Not Found            │
// ├───────────────────────────┼──────────────────┼─────────────────────────┤
// │ ElementAt(index)          │ Item at position │ THROWS exception        │
// │ ElementAtOrDefault(index) │ Item at position │ Returns default (0/null)│
// │ First()                   │ First item       │ THROWS exception        │
// │ FirstOrDefault()          │ First item       │ Returns default         │
// │ Last()                    │ Last item        │ THROWS exception        │
// │ LastOrDefault()           │ Last item        │ Returns default         │
// │ Single()                  │ Only item (=1)   │ THROWS if 0 or >1       │
// │ SingleOrDefault()         │ Only item        │ Default if 0; THROW >1  │
// └───────────────────────────┴──────────────────┴─────────────────────────┘
//
// WARNING: Element operators are NOT available in query syntax.
//          Use method syntax only.

namespace Topic14_ElementOperators
{
    class Program
    {
        static void Main()
        {
            IList<int> intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            IList<string> strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
            IList<string> emptyList = new List<string>();


            // ── 14.1 ElementAt & ElementAtOrDefault ─────────────────────────
            // Index starts at 0.
            Console.WriteLine(intList.ElementAt(0));   // 7
            Console.WriteLine(intList.ElementAt(1));   // 10

            Console.WriteLine(intList.ElementAtOrDefault(9));   // 0 (default for int)
            Console.WriteLine(strList.ElementAtOrDefault(9));   // null (default for string)

            // intList.ElementAt(9);  // THROWS IndexOutOfRangeException!


            // ── 14.2 First & FirstOrDefault ─────────────────────────────────
            Console.WriteLine(intList.First());                   // 7
            Console.WriteLine(intList.First(i => i % 2 == 0));   // 10 (first even)

            Console.WriteLine(strList.First());                   // null (first item)
            Console.WriteLine(emptyList.FirstOrDefault());        // null (empty → default)

            // emptyList.First();  // THROWS InvalidOperationException!


            // ── 14.3 Last & LastOrDefault ───────────────────────────────────
            // Same as First/FirstOrDefault but returns the LAST matching item.
            Console.WriteLine(intList.Last());                     // 87
            Console.WriteLine(intList.LastOrDefault(i => i < 10)); // 7


            // ── 14.4 Single & SingleOrDefault ───────────────────────────────
            // Use when you expect EXACTLY one item.
            // Throws error if 0 or more than 1.
            IList<int> oneItem = new List<int>() { 7 };

            Console.WriteLine(oneItem.Single());              // 7
            Console.WriteLine(oneItem.SingleOrDefault());     // 7

            Console.WriteLine(emptyList.SingleOrDefault());   // null

            Console.WriteLine(intList.Single(i => i < 10));   // 7 (only one < 10)

            // intList.Single();             // THROWS! More than one element
            // intList.SingleOrDefault();    // THROWS! More than one element
            // emptyList.Single();           // THROWS! No elements
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  15. EQUALITY: SequenceEqual                                                 ║
// ╚══════════════════════════════════════════════════════════════════════════════╝

namespace Topic15_Equality
{
    class Program
    {
        static void Main()
        {
            // SequenceEqual checks if two collections have the
            // SAME items in the SAME order.

            // ── Simple types: works directly ────────────────────────────────
            IList<string> list1 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            IList<string> list2 = new List<string>() { "One", "Two", "Three", "Four", "Three" };

            bool isEqual = list1.SequenceEqual(list2);  // true


            // ── Custom objects: needs IEqualityComparer ─────────────────────
            // By default compares REFERENCES, not values.

            // Same reference → true
            Student std = new Student() { StudentID = 1, StudentName = "Bill" };
            IList<Student> sList1 = new List<Student>() { std };
            IList<Student> sList2 = new List<Student>() { std };
            bool sameRef = sList1.SequenceEqual(sList2);  // true (same object)

            // Different references, same values → false (without comparer)
            Student std1 = new Student() { StudentID = 1, StudentName = "Bill" };
            Student std2 = new Student() { StudentID = 1, StudentName = "Bill" };
            IList<Student> sList3 = new List<Student>() { std1 };
            IList<Student> sList4 = new List<Student>() { std2 };
            bool diffRef = sList3.SequenceEqual(sList4);  // false!

            // FIX: Use IEqualityComparer to compare by value
            IList<Student> studentList1 = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
            };
            IList<Student> studentList2 = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
            };

            bool equalByValue = studentList1.SequenceEqual(studentList2, new StudentComparer());
            // true!
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  16. CONCATENATION: Concat                                                   ║
// ╚══════════════════════════════════════════════════════════════════════════════╝

namespace Topic16_Concatenation
{
    class Program
    {
        static void Main()
        {
            // Concat joins two collections into one.
            // Keeps ALL items including duplicates.
            // TIP: If you want to combine AND remove duplicates, use Union.

            IList<string> collection1 = new List<string>() { "One", "Two", "Three" };
            IList<string> collection2 = new List<string>() { "Five", "Six" };

            var combined = collection1.Concat(collection2);

            foreach (string str in combined)
                Console.WriteLine(str);
            // Output: One, Two, Three, Five, Six
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  17. GENERATION: DefaultIfEmpty, Empty, Range, Repeat                        ║
// ╚══════════════════════════════════════════════════════════════════════════════╝

namespace Topic17_Generation
{
    class Program
    {
        static void Main()
        {
            // ── 17.1 DefaultIfEmpty ─────────────────────────────────────────
            // If the collection is empty, returns a new collection with one
            // default value instead.
            IList<string> emptyList = new List<string>();

            var list1 = emptyList.DefaultIfEmpty();        // { null }
            var list2 = emptyList.DefaultIfEmpty("None");  // { "None" }

            Console.WriteLine("Count: {0}", list1.Count());           // 1
            Console.WriteLine("Value: {0}", list1.ElementAt(0));      // (null)
            Console.WriteLine("Count: {0}", list2.Count());           // 1
            Console.WriteLine("Value: {0}", list2.ElementAt(0));      // None


            // ── 17.2 Empty ──────────────────────────────────────────────────
            // Creates an empty collection of a specific type.
            // Useful when you need to return "nothing" safely (instead of null).
            // NOTE: Not an extension method — it's a static method on Enumerable.
            var emptyStrings  = Enumerable.Empty<string>();
            var emptyStudents = Enumerable.Empty<Student>();

            Console.WriteLine("Count: {0}", emptyStrings.Count());    // 0
            Console.WriteLine("Type: {0}", emptyStrings.GetType().Name);


            // ── 17.3 Range ──────────────────────────────────────────────────
            // Creates a sequence of SEQUENTIAL numbers.
            // Range(start, count)
            var numbers = Enumerable.Range(10, 10);
            // Result: 10, 11, 12, 13, 14, 15, 16, 17, 18, 19

            Console.WriteLine("Total Count: {0}", numbers.Count());  // 10
            for (int i = 0; i < numbers.Count(); i++)
                Console.WriteLine("Value at index {0}: {1}", i, numbers.ElementAt(i));


            // ── 17.4 Repeat ─────────────────────────────────────────────────
            // Creates a collection where EVERY element has the SAME value.
            // Repeat(value, count)
            var tens = Enumerable.Repeat<int>(10, 10);
            // Result: 10, 10, 10, 10, 10, 10, 10, 10, 10, 10

            // TIP: Range gives sequential numbers (10, 11, 12...)
            //      Repeat gives the same number over and over (10, 10, 10...)
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  18. SET OPERATORS: Distinct, Except, Intersect, Union                       ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// ┌───────────┬──────────────────────────────────────────────────────────────┐
// │ Operator  │ What It Does                                                 │
// ├───────────┼──────────────────────────────────────────────────────────────┤
// │ Distinct  │ Removes duplicates from ONE list                             │
// │           │ {1,2,2,3} → {1,2,3}                                          │
// │ Except    │ Items in list 1 but NOT in list 2                            │
// │           │ {1,2,3} except {2,3,4} → {1}                                 │
// │ Intersect │ Items in BOTH lists                                          │
// │           │ {1,2,3} intersect {2,3,4} → {2,3}                            │
// │ Union     │ All UNIQUE items from both lists                             │
// │           │ {1,2,3} union {2,3,4} → {1,2,3,4}                            │
// └───────────┴──────────────────────────────────────────────────────────────┘
//
// WARNING: For custom objects, all these compare by REFERENCE, not value.
//          Pass an IEqualityComparer<T> to get correct results.

namespace Topic18_SetOperators
{
    class Program
    {
        static void Main()
        {
            // ── 18.1 Distinct ───────────────────────────────────────────────
            // Removes duplicates.
            IList<string> strList = new List<string>() { "One", "Two", "Three", "Two", "Three" };
            var unique = strList.Distinct();
            // Result: "One", "Two", "Three"

            foreach (var str in unique)
                Console.WriteLine(str);

            IList<int> intList = new List<int>() { 1, 2, 3, 2, 4, 4, 3, 5 };
            var uniqueInts = intList.Distinct();
            // Result: 1, 2, 3, 4, 5

            // Distinct with custom objects — needs IEqualityComparer:
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },  // duplicate
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },  // duplicate
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 19 },
            };

            var distinctStudents = studentList.Distinct(new StudentComparer());
            // Result: John, Steve, Bill, Ron (duplicates removed)

            foreach (Student std in distinctStudents)
                Console.WriteLine(std.StudentName);


            // ── 18.2 Except ─────────────────────────────────────────────────
            // Items in list 1 that are NOT in list 2.
            IList<string> strList1 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            IList<string> strList2 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight" };

            var exceptResult = strList1.Except(strList2);
            // Result: "One", "Two", "Three"

            foreach (string str in exceptResult)
                Console.WriteLine(str);

            // With custom objects — needs IEqualityComparer:
            IList<Student> sList1 = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 15 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 19 },
            };
            IList<Student> sList2 = new List<Student>() {
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 19 },
            };

            var exceptStudents = sList1.Except(sList2, new StudentComparer());
            // Result: John, Steve

            foreach (Student std in exceptStudents)
                Console.WriteLine(std.StudentName);


            // ── 18.3 Intersect ──────────────────────────────────────────────
            // Items in BOTH lists.
            var intersectResult = strList1.Intersect(strList2);
            // Result: "Four", "Five"

            foreach (string str in intersectResult)
                Console.WriteLine(str);

            // With custom objects:
            var intersectStudents = sList1.Intersect(sList2, new StudentComparer());
            // Result: Bill, Ron

            foreach (Student std in intersectStudents)
                Console.WriteLine(std.StudentName);


            // ── 18.4 Union ──────────────────────────────────────────────────
            // All UNIQUE items from both lists combined.
            IList<string> uList1 = new List<string>() { "One", "Two", "three", "Four" };
            IList<string> uList2 = new List<string>() { "Two", "THREE", "Four", "Five" };

            var unionResult = uList1.Union(uList2);
            // Note: "three" and "THREE" are different (case-sensitive)
            // Result: "One", "Two", "three", "Four", "THREE", "Five"

            foreach (string str in unionResult)
                Console.WriteLine(str);

            // With custom objects:
            var unionStudents = sList1.Union(sList2, new StudentComparer());
            // Result: John, Steve, Bill, Ron (no duplicates)

            foreach (Student std in unionStudents)
                Console.WriteLine(std.StudentName);
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  19. PARTITIONING: Skip, SkipWhile, Take, TakeWhile                          ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// Split a collection into two parts and return one part.
//
// ┌─────────────────────┬──────────────────────────────────────────────────────┐
// │ Operator            │ What It Does                                         │
// ├─────────────────────┼──────────────────────────────────────────────────────┤
// │ Skip(n)             │ Skips first n items, returns the rest                │
// │ SkipWhile(cond)     │ Skips while condition is true, then returns rest     │
// │ Take(n)             │ Takes first n items, ignores the rest                │
// │ TakeWhile(cond)     │ Takes while condition is true, then stops            │
// └─────────────────────┴──────────────────────────────────────────────────────┘
//
// WARNING: These are NOT available in query syntax. Use method syntax only.

namespace Topic19_Partitioning
{
    class Program
    {
        static void Main()
        {
            // ── 19.1 Skip ───────────────────────────────────────────────────
            IList<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var skipResult = numbers.Skip(3);
            // Result: 4, 5, 6, 7, 8, 9, 10 (skipped first 3)

            foreach (int num in skipResult)
                Console.WriteLine(num);


            // ── 19.2 SkipWhile ──────────────────────────────────────────────
            // Skips items WHILE condition is true.
            // Once an item FAILS the condition, returns everything from there.
            IList<string> strList = new List<string>() {
                "One", "Two", "Three", "Four", "Five", "Six"
            };

            var skipWhileResult = strList.SkipWhile(s => s.Length < 4);
            // "One" (length 3 < 4 → skip),
            // "Two" (length 3 < 4 → skip),
            // "Three" (length 5 >= 4 → STOP skipping!)
            // Result: "Three", "Four", "Five", "Six"

            foreach (string str in skipWhileResult)
                Console.WriteLine(str);


            // ── 19.3 Take ───────────────────────────────────────────────────
            IList<string> strList2 = new List<string>() { "One", "Two", "Three", "Four", "Five" };

            var takeResult = strList2.Take(2);
            // Result: "One", "Two" (took first 2)

            foreach (var str in takeResult)
                Console.WriteLine(str);


            // ── 19.4 TakeWhile ──────────────────────────────────────────────
            // Takes items WHILE condition is true. Stops at first failure.
            IList<string> strList3 = new List<string>() {
                "Three", "Four", "Five", "Hundred"
            };

            var takeWhileResult = strList3.TakeWhile(s => s.Length > 4);
            // "Three" (length 5 > 4 → take),
            // "Four" (length 4 NOT > 4 → STOP!)
            // Result: "Three"

            foreach (string str in takeWhileResult)
                Console.WriteLine(str);
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  20. CONVERSION OPERATORS                                                    ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// ┌─────────────────┬─────────────────────────────────────────────────────────┐
// │ Type            │ Operators & What They Do                                │
// ├─────────────────┼─────────────────────────────────────────────────────────┤
// │ As Operators    │ AsEnumerable, AsQueryable                               │
// │                 │ → Change compile-time type, not the actual data         │
// ├─────────────────┼─────────────────────────────────────────────────────────┤
// │ To Operators    │ ToArray, ToList, ToDictionary, ToLookup                 │
// │                 │ → Force IMMEDIATE execution + convert type              │
// ├─────────────────┼─────────────────────────────────────────────────────────┤
// │ Cast Operators  │ Cast, OfType                                            │
// │                 │ → Convert non-generic to generic collections            │
// └─────────────────┴─────────────────────────────────────────────────────────┘

namespace Topic20_Conversion
{
    class Program
    {
        static void ReportTypeProperties<T>(T obj)
        {
            Console.WriteLine("Compile-time type: {0}", typeof(T).Name);
            Console.WriteLine("Actual type:       {0}", obj.GetType().Name);
        }

        static void Main()
        {
            Student[] studentArray = {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 31 },
            };


            // ── 20.1 AsEnumerable & AsQueryable ─────────────────────────────
            // Changes how the COMPILER sees your collection.
            // AsEnumerable → forces LINQ-to-Objects (in-memory).
            // AsQueryable  → allows remote queries (like SQL).

            ReportTypeProperties(studentArray);                // Student[]
            ReportTypeProperties(studentArray.AsEnumerable()); // IEnumerable<Student>
            ReportTypeProperties(studentArray.AsQueryable());  // IQueryable<Student>


            // ── 20.2 ToArray, ToList, ToDictionary ──────────────────────────
            // Force the LINQ query to run IMMEDIATELY and return a
            // concrete collection.

            IList<string> strList = new List<string>() { "One", "Two", "Three", "Four", "Three" };

            // List → Array
            string[] strArray = strList.ToArray<string>();

            // Array → List
            IList<string> newList = strArray.ToList<string>();

            // List → Dictionary (must specify the key)
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John",  Age = 18 },
                // FIX: was "age = 18" (lowercase) in original
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 },
                new Student() { StudentID = 4, StudentName = "Ram",   Age = 20 },
                new Student() { StudentID = 5, StudentName = "Ron",   Age = 21 },
            };

            IDictionary<int, Student> studentDict =
                studentList.ToDictionary<Student, int>(s => s.StudentID);
            // Key = StudentID, Value = Student object

            foreach (var key in studentDict.Keys)
                Console.WriteLine("Key: {0}, Name: {1}",
                    key, studentDict[key].StudentName);


            // ── 20.3 Cast ───────────────────────────────────────────────────
            // Converts a non-generic collection to IEnumerable<T>.
            // Throws an error if any item can't be cast.
            var castedStudents = studentArray.Cast<Student>();
            // Same as: (IEnumerable<Student>)studentArray but more readable

            ReportTypeProperties(studentArray);
            ReportTypeProperties(studentArray.Cast<Student>());

            // TIP: Use OfType<T> instead of Cast<T> if the collection
            //      might have mixed types — OfType SKIPS items that can't
            //      be cast, while Cast THROWS an exception.
        }
    }
}


// ╔══════════════════════════════════════════════════════════════════════════════╗
// ║  END OF LINQ REFERENCE                                                       ║
// ╚══════════════════════════════════════════════════════════════════════════════╝
//
// BUGS FIXED FROM ORIGINAL FILE:
//   1. &gt; and &lt; → replaced with > and < in code (wouldn't compile)
//   2. =&gt;        → replaced with => (HTML entity in lambda operators)
//   3. List<Student>>() → List<Student>() (double >> syntax error, 6 places)
//   4. .age (lowercase) → .Age (uppercase) to match the property name
//   5. Missing/extra braces fixed in the delegate example
//   6. Duplicate/redundant explanations condensed
//   7. All code organized into proper namespaces and classes