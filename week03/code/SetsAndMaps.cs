using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        var set = new HashSet<string>(words);
        var result = new List<string>();
        var visited = new HashSet<string>();

        foreach (var word in words)
        {
            if (word[0] == word[1]) continue;

            var rev = new string(new[] { word[1], word[0] });

            if (set.Contains(rev) && !visited.Contains(word) && !visited.Contains(rev))
            {
                var pair = new[] { word, rev }.OrderBy(s => s).Aggregate((a, b) => a + " & " + b);
                result.Add(pair);
                visited.Add(word);
                visited.Add(rev);
            }
        }

        return result.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');

            if (fields.Length < 4) continue;

            var degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        if (word1 == null || word2 == null)
            return false;

        string a = new string(word1.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        string b = new string(word2.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();

        if (a.Length != b.Length)
            return false;

        var count = new Dictionary<char, int>();

        foreach (var c in a)
            count[c] = count.GetValueOrDefault(c, 0) + 1;

        foreach (var c in b)
        {
            if (!count.ContainsKey(c))
                return false;

            count[c]--;
            if (count[c] < 0)
                return false;
        }

        return true;
    }

    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        var json = client.GetStringAsync(uri).Result;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (featureCollection == null || featureCollection.Features == null)
            return Array.Empty<string>();

        var summaries = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            var place = feature.Properties.Place ?? "Unknown location";
            var mag = feature.Properties.Mag.HasValue ? feature.Properties.Mag.Value.ToString("F1") : "N/A";

            summaries.Add($"{place} - Mag {mag}");
        }

        return summaries.ToArray();
    }
}