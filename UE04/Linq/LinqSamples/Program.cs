using LinqSamples.Data;
using System.Security.Principal;

var repository = new CustomerRepository();
var customers = repository.GetCustomers();

// Key-Punkte:
// 1)  hier wird nur die Query definiert, aber noch nicht ausgeführt (erst wo in Print customersWithA verwendet wird, wird das Query ausgeführt)
// 2)  wenn ich das Query mehrfach ausführe (z.B. Print 2x ausführe), wird auch jedes Mal das Query ausgeführt (also auch 2x)
// 2b) Man könnte das Query aber auch direkt in eine Liste umwandeln (.ToList), dann wird es nur 1x ausgeführt (Klammerung notwendig)
var customersWithA =
    ( // 2b)
    from c in customers
    where c.Name.StartsWith("A", StringComparison.CurrentCultureIgnoreCase)
    //select c;
    select c.Name).ToList(); // 2b)

//var customersWithA = customers
//    .Where(c => c.Name.StartsWith("A", StringComparison.CurrentCultureIgnoreCase))
//    .Select(c => c.Name)
//    .ToList();

Print("Customers with A", customersWithA);

var customersByRevenue =
    from c in customers
    where c.Revenue > 1_000_000
    orderby c.Revenue descending
    select c;

Print("Customers by Revenue", customersByRevenue);

//var customersByRevenue = customers
//    .Where(c => c.Revenue > 1_000_000)
//    .OrderByDescending(c => c.Revenue);

// das macht der Compiler aus dem darüber geschriebenen (= IL-Code):
//Enumerable.Select(
//    Enumerable.OrderByDescending(
//        Enumerable.Where(
//            customers,
//            c => c.Revenue > 1_000_000),
//        c => c.Revenue),
//    c => c);

var largestCustomers =
    (from c in customers
    orderby c.Revenue descending
    select c).Take(3);

//var largestCustomers = customers.OrderByDescending(c => c.Revenue).Take(3);

Print("Largest Customers", largestCustomers);

//decimal avgRevenueOfACustomers =
//    (from c in customers
//     where c.Rating == Rating.A
//     select c).Average(c => c.Revenue); // Achtung falls where nicht erfüllt, gibt es beim Aufruf von Average() eine Exception (DivByZero), daher Schreibweise mit Any() besser
//Console.WriteLine($"Average Revenue of A Customers: {avgRevenueOfACustomers:N2}");

var aCustomers = customers.Where(c => c.Rating == Rating.A);
if (aCustomers.Any())
{
    Console.WriteLine($"Average Revenue of A Customers: {aCustomers.Average(c => c.Revenue):N2}");
}
else
{
    Console.WriteLine("No A customers found");
}

var famia = customers.FirstOrDefault(c => c.Name.Equals("famia", StringComparison.CurrentCultureIgnoreCase)); // Default für Customers ist null, Default von int ist 0
//var famia = customers.SingleOrDefault(c => c.Name == "famia", StringComparison.CurrentCultureIgnoreCase); // überprüft zusätzlich, ob es nur 1 Element gibt, und falls nicht wirft es Exception
if (famia is not null)
{
    Console.WriteLine(famia);
}
Console.WriteLine();

var customersPerCountry =
    from c in customers
    group c by c.Location.Country into countryGroup // liefert mehrere kleinere Enumerables zurück (1 für jeden Country) - also wir haben 1 großes Enumerable mit kleineren Enumerables drinnen, daher neue Variable "countryGroup" notwendig (Datentyp: IEnumerable<IGrouping<string, Customer>>)
    select new
    {
        Country = countryGroup.Key,
        Customers = (IEnumerable<Customer>)countryGroup
    };

//var customersPerCountry = customers.GroupBy(c => c.Location.Country).Select(countryGroup => new
//{
//    Country = countryGroup.Key,
//    Customers = (IEnumerable<Customer>)countryGroup
//});

foreach (var group in customersPerCountry.OrderBy(g => g.Country))
{
    Console.WriteLine(group.Country);
    foreach (var customer in group.Customers)
    {
        Console.WriteLine($" {customer.Name}");
    }
    Console.WriteLine();
}


void Print<T>(string title, IEnumerable<T> items)
{
    Console.WriteLine($"{title}:");
    Console.WriteLine();
    
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }

    Console.WriteLine();
    Console.WriteLine();
}