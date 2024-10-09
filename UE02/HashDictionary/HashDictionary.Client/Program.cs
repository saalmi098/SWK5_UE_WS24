using HashDictionary.Impl;

// classes per default verwenden, wenn Immutability eine Rolle spielt dann Records
// (structs nur in speziellen Ausnahmefällen verwenden)
// class (ref), records (ref + immutable), structs (value, immutable)

IDictionary<string, int> TestIndexerAndAdd()
{
    var cityInfo = new HashDictionary<string, int>();

    try
    {

        cityInfo["Hagenberg"] = 2_500;
        cityInfo["Linz"] = 180_000;
        cityInfo["Linz"] = 200_000;
        //cityInfo["Linz"] = DateTime.Now; // Compiler Error

        cityInfo.Add("Wien", 1_700_000);
        cityInfo.Add("Wien", 1_750_000); // ArgumentException: Key already exists
    }
    catch (ArgumentException e)
    {
        Console.WriteLine($"{e.GetType().Name}: {e.Message}");
    }

    try
    {
        Console.WriteLine($"cityInfo[\"Hagenberg\"] = {cityInfo["Hagenberg"]}");
        Console.WriteLine($"cityInfo[\"Linz\"] = {cityInfo["Linz"]}");
        Console.WriteLine($"cityInfo[\"Wien\"] = {cityInfo["Wien"]}");
        Console.WriteLine($"cityInfo[\"Graz\"] = {cityInfo["Graz"]}"); // KeyNotFoundException
    }
    catch (KeyNotFoundException e)
    {
        Console.WriteLine($"{e.GetType().Name}: {e.Message}");
    }

    return cityInfo;
}

void PrintDictionary<K, V>(IDictionary<K, V> dictionary)
{
    foreach (KeyValuePair<K, V> pair in dictionary)
    {
        Console.WriteLine($"{pair.Key} -> {pair.Value}");
    }

    Console.WriteLine("---");
    Console.WriteLine("Keys: ");
    foreach (var key in dictionary.Keys.ToList())
    {
        Console.WriteLine(key.ToString());
    }

    Console.WriteLine("Values: ");
    foreach (var key in dictionary.Values.ToList())
    {
        Console.WriteLine(key.ToString());
    }
}

var cityInfo = TestIndexerAndAdd();
PrintDictionary(cityInfo);