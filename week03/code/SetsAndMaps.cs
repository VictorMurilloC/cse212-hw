using System.Text.Json;

public static class SetsAndMaps
{
    // Problem 1: FindPairs
    public static string[] FindPairs(string[] words)
    {
        var set = new HashSet<string>(words);
        var result = new List<string>();

        foreach (var word in words)
        {
            if (word[0] == word[1]) continue; // skip pairs like "aa"
            var reversed = new string(new[] { word[1], word[0] });
            if (set.Contains(reversed) && String.Compare(word, reversed) < 0)
            {
                result.Add($"{word} & {reversed}");
            }
        }

        return result.ToArray();
    }

    // Problem 2: SummarizeDegrees
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length < 4) continue; // skip bad lines
            var degree = fields[3];
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }
        return degrees;
    }

    // Problem 3: IsAnagram
    public static bool IsAnagram(string word1, string word2)
    {
        var w1 = word1.Replace(" ", "").ToLower();
        var w2 = word2.Replace(" ", "").ToLower();

        if (w1.Length != w2.Length) return false;

        var charCount = new Dictionary<char, int>();
        foreach (var c in w1)
        {
            charCount[c] = charCount.GetValueOrDefault(c) + 1;
        }

        foreach (var c in w2)
        {
            if (!charCount.ContainsKey(c)) return false;
            charCount[c]--;
            if (charCount[c] < 0) return false;
        }

        return true;
    }

    // Problem 5: EarthquakeDailySummary
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (featureCollection?.Features == null) return Array.Empty<string>();

        var results = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            if (feature.Properties?.Place != null)
            {
                results.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
            }
        }

        return results.ToArray();
    }
}