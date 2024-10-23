using System.Collections.Generic;

namespace LinqSamples.Data;

public class CustomerRepository
{
    public IEnumerable<Customer> GetCustomers()
    {
        yield return new Customer() { Name = "manuyo", Revenue = 2200000, YearOfFoundation = 1986, Location = new Address() { City = "Wien", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "acero GmbH", Revenue = 2800000, YearOfFoundation = 2007, Location = new Address() { City = "Innsbruck", Country = "Österreich" }, Rating = Rating.A };
        yield return new Customer() { Name = "PIXOPE", Revenue = 2500000, YearOfFoundation = 1992, Location = new Address() { City = "Eisenstadt", Country = "Österreich" }, Rating = Rating.A };
        yield return new Customer() { Name = "hydrojo GmbH", Revenue = 2500000, YearOfFoundation = 1977, Location = new Address() { City = "San Francisco", Country = "USA" }, Rating = Rating.C };
        yield return new Customer() { Name = "placee", Revenue = 200000, YearOfFoundation = 2003, Location = new Address() { City = "Linz", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "PYROCEE GmbH", Revenue = 900000, YearOfFoundation = 1990, Location = new Address() { City = "Klagenfurt", Country = "Österreich" }, Rating = Rating.A };
        yield return new Customer() { Name = "DISIBLE", Revenue = 1400000, YearOfFoundation = 2001, Location = new Address() { City = "St. Pölten", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "Duomba AG", Revenue = 1700000, YearOfFoundation = 1974, Location = new Address() { City = "Mailand", Country = "Italien" }, Rating = Rating.A };
        yield return new Customer() { Name = "biofix", Revenue = 1800000, YearOfFoundation = 1996, Location = new Address() { City = "Graz", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "voomba", Revenue = 1500000, YearOfFoundation = 2013, Location = new Address() { City = "Graz", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "CALCOID", Revenue = 2700000, YearOfFoundation = 1996, Location = new Address() { City = "Mailand", Country = "Italien" }, Rating = Rating.A };
        yield return new Customer() { Name = "Octotude GmbH", Revenue = 3100000, YearOfFoundation = 1985, Location = new Address() { City = "Eisenstadt", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "GENOODLE GmbH", Revenue = 3500000, YearOfFoundation = 2000, Location = new Address() { City = "Bologna", Country = "Italien" }, Rating = Rating.C };
        yield return new Customer() { Name = "circumer GmbH", Revenue = 2100000, YearOfFoundation = 1973, Location = new Address() { City = "New York", Country = "USA" }, Rating = Rating.A };
        yield return new Customer() { Name = "CORATA GmbH", Revenue = 3400000, YearOfFoundation = 1988, Location = new Address() { City = "Linz", Country = "Österreich" }, Rating = Rating.A };
        yield return new Customer() { Name = "ARCHOSIS GmbH", Revenue = 2600000, YearOfFoundation = 1991, Location = new Address() { City = "Innsbruck", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "rhyoid AG", Revenue = 2100000, YearOfFoundation = 2015, Location = new Address() { City = "Salzburg", Country = "Österreich" }, Rating = Rating.A };
        yield return new Customer() { Name = "camicy AG", Revenue = 700000, YearOfFoundation = 1999, Location = new Address() { City = "Boston", Country = "USA" }, Rating = Rating.B };
        yield return new Customer() { Name = "Bijo", Revenue = 2000000, YearOfFoundation = 2017, Location = new Address() { City = "San Francisco", Country = "USA" }, Rating = Rating.B };
        yield return new Customer() { Name = "Hyperosis AG", Revenue = 3800000, YearOfFoundation = 1991, Location = new Address() { City = "Eisenstadt", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "tritri", Revenue = 1100000, YearOfFoundation = 2009, Location = new Address() { City = "Rom", Country = "Italien" }, Rating = Rating.A };
        yield return new Customer() { Name = "prolane", Revenue = 3600000, YearOfFoundation = 1999, Location = new Address() { City = "Linz", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "aqutz GmbH", Revenue = 500000, YearOfFoundation = 2004, Location = new Address() { City = "Barcelona", Country = "Spanien" }, Rating = Rating.A };
        yield return new Customer() { Name = "contramm GmbH", Revenue = 2300000, YearOfFoundation = 1984, Location = new Address() { City = "Mailand", Country = "Italien" }, Rating = Rating.B };
        yield return new Customer() { Name = "Aipe AG", Revenue = 1200000, YearOfFoundation = 2007, Location = new Address() { City = "New York", Country = "USA" }, Rating = Rating.A };
        yield return new Customer() { Name = "paleofix GmbH", Revenue = 1200000, YearOfFoundation = 2015, Location = new Address() { City = "Eisenstadt", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "mific AG", Revenue = 3500000, YearOfFoundation = 1976, Location = new Address() { City = "Madrid", Country = "Spanien" }, Rating = Rating.B };
        yield return new Customer() { Name = "kayose GmbH", Revenue = 2400000, YearOfFoundation = 2017, Location = new Address() { City = "Barcelona", Country = "Spanien" }, Rating = Rating.B };
        yield return new Customer() { Name = "agideo AG", Revenue = 1400000, YearOfFoundation = 1998, Location = new Address() { City = "Wien", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "Eape AG", Revenue = 2200000, YearOfFoundation = 1989, Location = new Address() { City = "Madrid", Country = "Spanien" }, Rating = Rating.B };
        yield return new Customer() { Name = "wikinder", Revenue = 3300000, YearOfFoundation = 1982, Location = new Address() { City = "Klagenfurt", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "famia", Revenue = 1900000, YearOfFoundation = 2012, Location = new Address() { City = "Bologna", Country = "Italien" }, Rating = Rating.B };
        yield return new Customer() { Name = "HYPODOO AG", Revenue = 400000, YearOfFoundation = 2018, Location = new Address() { City = "San Francisco", Country = "USA" }, Rating = Rating.A };
        yield return new Customer() { Name = "panoid AG", Revenue = 400000, YearOfFoundation = 2018, Location = new Address() { City = "Eisenstadt", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "nonicious", Revenue = 3000000, YearOfFoundation = 2004, Location = new Address() { City = "Linz", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "infrazzy GmbH", Revenue = 500000, YearOfFoundation = 1977, Location = new Address() { City = "Madrid", Country = "Spanien" }, Rating = Rating.C };
        yield return new Customer() { Name = "Forist", Revenue = 1000000, YearOfFoundation = 1988, Location = new Address() { City = "Bregenz", Country = "Österreich" }, Rating = Rating.C };
        yield return new Customer() { Name = "Vertise", Revenue = 1100000, YearOfFoundation = 1991, Location = new Address() { City = "Mailand", Country = "Italien" }, Rating = Rating.B };
        yield return new Customer() { Name = "corous GmbH", Revenue = 2300000, YearOfFoundation = 1983, Location = new Address() { City = "Klagenfurt", Country = "Österreich" }, Rating = Rating.B };
        yield return new Customer() { Name = "idiovu GmbH", Revenue = 2500000, YearOfFoundation = 2016, Location = new Address() { City = "Bologna", Country = "Italien" }, Rating = Rating.A };

    }
}
