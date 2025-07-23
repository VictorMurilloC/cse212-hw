using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class FindPairsTests
{
    [TestMethod]
    public void FindPairs_TwoPairs()
    {
        var actual = SetsAndMaps.FindPairs(new[] { "am", "at", "ma", "if", "fi" });
        var expected = new[] { "ma & am", "fi & if" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_OnePair()
    {
        var actual = SetsAndMaps.FindPairs(new[] { "ab", "bc", "cd", "de", "ba" });
        var expected = new[] { "ba & ab" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_SameChar()
    {
        var actual = SetsAndMaps.FindPairs(new[] { "ab", "aa", "ba" });
        var expected = new[] { "ba & ab" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_ThreePairs()
    {
        var actual = SetsAndMaps.FindPairs(new[] { "ab", "ba", "ac", "ad", "da", "ca" });
        var expected = new[] { "ba & ab", "da & ad", "ca & ac" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_ThreePairsNumbers()
    {
        var actual = SetsAndMaps.FindPairs(new[] { "23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14" });
        var expected = new[] { "32 & 23", "94 & 49", "31 & 13" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_NoPairs()
    {
        var actual = SetsAndMaps.FindPairs(new[] { "ab", "ac" });
        var expected = new string[0];

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    private string Canonicalize(string[] array)
    {
        if (array.Length == 0)
            return "";

        var canonicalString = array.Select(item =>
        {
            var parts = item.Split('&');
            return parts.Select(part => part.Trim()).OrderBy(x => x).Aggregate((current, next) => current + "&" + next);
        })
        .OrderBy(x => x)
        .Aggregate((current, next) => current + "," + next);

        return canonicalString;
    }
}