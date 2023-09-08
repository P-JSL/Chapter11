using System.Security;
using static System.Console;

string[] names = new[] { "Michael", "Pam", "Jim", "Dwight" , "Angela","Kevin","Toby","Creed"};

WriteLine("지연실행");

//m으로 끝나는 이름은?

var query1 = names.Where(name => name.EndsWith("m"));

var query2 = from name in names where name.EndsWith("m") select name;

string[] result1 = query1.ToArray();
List<string> result2 = query2.ToList();

foreach(var name in query1)
{
    WriteLine(name);
    names[2] = "Jimmy";
}

//var query = names.Where(new Func<string, bool>(NameLongerrThanFour));
//var query = names.Where(NameLongerrThanFour);
var query = names.Where(name => name.Length > 4)
    .OrderBy(name => name.Length)
    .ThenBy(name => name);
foreach(string item in query)
{
    WriteLine(item);
}
static bool NameLongerrThanFour(string name)
{
    return name.Length > 4;
}

WriteLine();
WriteLine("형식 필터링");
List<Exception> exceptions = new()
{
    new ArgumentException(),
    new SystemException(),
    new IndexOutOfRangeException(),
    new InvalidOperationException(),
    new NullReferenceException(),
    new InvalidCastException(),
    new OverflowException(),
    new DivideByZeroException(),
    new ApplicationException()
};

IEnumerable<ArithmeticException> arithmeticExceptionsQuery = exceptions.OfType<ArithmeticException>();
foreach(ArithmeticException exception in arithmeticExceptionsQuery)
{
    WriteLine(exception);
}