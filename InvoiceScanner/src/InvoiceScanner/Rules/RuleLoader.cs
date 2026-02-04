using System;
using System.IO;
using System.Text.Json;

namespace InvoiceScanner.Rules;

public static class RuleLoader
{
    public static RuleSet Load(string path)
    {
        try
        {
            if (!File.Exists(path)) return RuleSet.Default();
            var json = File.ReadAllText(path);
            var rules = JsonSerializer.Deserialize<RuleSet>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return rules ?? RuleSet.Default();
        }
        catch
        {
            return RuleSet.Default();
        }
    }
}
