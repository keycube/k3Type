using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utils {

	private static string[] words = { "a", "is", "the", "from", "hello", "typing", "glasgow", "keyboard", "computing", "technology", "interactive", "practitioner", "multicultural", "interconnected", "world", "key", "cube", "chi", "human", "community", "friendship", "researcher", "designer" };

	/// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts) 
	{
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) 
		{
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }


	public static List<string> GetWords()
	{
		return words.ToList();
	}
}
