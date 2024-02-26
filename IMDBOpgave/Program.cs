// See https://aka.ms/new-console-template for more information

using IMDBOpgave;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Starting insert");

Stopwatch sw = Stopwatch.StartNew();

List<Title> titles = new List<Title>();

IEnumerable<string> lines = File.ReadAllLines
    (@"C:\temp\title.basics.tsv\data.tsv")
    .Skip(1);

foreach (string line in lines)
{
    string[] values = line.Split("\t");
    if (values.Length == 9)
    {
        Title title = new Title(values[0], values[1], values[2],
            values[3], ConvertToBool(values[4]), ConvertToInt(values[5]),
            ConvertToInt(values[6]), ConvertToInt(values[7]),
            values[8]);
        titles.Add(title);
    }
}
sw.Stop();
Console.WriteLine(titles.Count);
Console.WriteLine(sw.ElapsedMilliseconds);

Console.WriteLine("Write 1 for normal Insert");
Console.WriteLine("Write 2 for prepared Insert");
Console.WriteLine("Write 3 for Bulk Insert");
string input = Console.ReadLine();

IInserter? inserter = null;

switch (input)
{
    case "1":
        inserter = new NormalInserter();
        break;
    case "2":
        inserter = new PreparedInserter();
        break;
    case "3":
        inserter = new BulkInserter();
        break;
}

using (SqlConnection sqlConn = new SqlConnection(
    "server=localhost;database=IMDB;" +
    "integrated security=true;TrustServerCertificate=True"))
{
    sw.Restart();
    sqlConn.Open();

    inserter.InsertTitles(sqlConn, titles);

    sw.Stop();
    Console.WriteLine(sw.ElapsedMilliseconds);
}


bool ConvertToBool(string input)
{
    if (input == "0")
    {
        return false;
    }
    else if (input == "1")
    {
        return true;
    }
    throw new ArgumentException("Ukendt værdi: " + input);
}

int? ConvertToInt(string input)
{
    if (input.ToLower() == @"\n")
    {
        return null;
    }
    return int.Parse(input);
}