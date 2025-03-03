using System.Reflection;

namespace Divination.Data;

public class ResourceLoader
{
    public static string LoadRulesFromFile()
    {
        var assembly = Assembly.GetExecutingAssembly();

        using (var stream = assembly.GetManifestResourceStream("Divination.Resources.rules.txt"))
        {
            if (stream == null) throw new FileNotFoundException();
            return new StreamReader(stream).ReadToEnd();
        }
    }
}