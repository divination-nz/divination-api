using System.Reflection;

namespace Divination.Data;

public class ResourceLoader
{
    public static String LoadRulesFromFile()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        
        using (Stream? stream = assembly.GetManifestResourceStream("Divination.Resources.rules.txt"))
        {
            if (stream == null) throw new FileNotFoundException();
            return (new StreamReader(stream).ReadToEnd());
        }
    }
}