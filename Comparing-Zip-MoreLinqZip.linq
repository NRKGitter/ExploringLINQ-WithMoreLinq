<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>


/*
Comparing Linq's Zip method with MoreLinq's EquiZip, ZipLongest, ZipShortest
*/

void Main()
{

	var people = new[] { "Ben", "Lily", "Joel", "Sam", "Annie" };
	var ratings = new[] { 5, 10, 12, 15, 17 };
	// Zip
	people.Zip(ratings, (w, p) => $"{w} wins a {p}").Dump("Linq Zip");

	var prizes = new[] {"Football","Paint","Rubik Cube","Fart m/c","Pie"};

	// Zip
	people.Zip(prizes, (w, p) => $"{w} wins a {p}").Dump("Linq Zip");
	// Vs EquiZip
	people.EquiZip(prizes, (w, p) => $"{w} wins a {p}").Dump("More Linq's EquiZip");

	var crisps = new[] {"Salt and Vinegar", "Cheese and onion", "Ready Salted"};
	// something like inner join
	people.Zip(crisps, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");
	crisps.Zip(people, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");
	
	// EquiZip throws Exception if the Seq's are not equal
	//people.EquiZip(crisps, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");
	
	// More Linq ZipShortest same as EquiZip
	people.ZipShortest(crisps, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");
	
	people.ZipLongest(crisps, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");
	crisps.ZipLongest(people, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");

	/*
	
		My IMPLEMENTATIONS........
	
	
	*/

	people.MyEquiZip(ratings, (w, p) => $"{w} wins a {p}").Dump("Linq Zip");
	people.MyLongestZip(crisps, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");
	crisps.MyLongestZip(people, (w, p) => $"{w} wins a {p}").Dump("Linq Zip with Diff lengths");

}

// Define other methods and classes here
public static class MyExts
{

	public static IEnumerable<R> MyEquiZip<T1, T2, R>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, R> resultSelector)
	{
		if (first == null) throw new ArgumentNullException(nameof(first));
		if (second == null) throw new ArgumentNullException(nameof(second));
		if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

		using (var e1 = first.GetEnumerator())
		using (var e2 = second.GetEnumerator())
		{
			while (e1.MoveNext())
			{
				if (e2.MoveNext())
					yield return resultSelector(e1.Current, e2.Current);
				else
					throw new InvalidOperationException("Second sequence ran out before first");
			}
			if (e2.MoveNext())
				throw new InvalidOperationException("First sequence ran out before second");
		}

	}

	public static IEnumerable<R> MyLongestZip<T1, T2, R>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, R> resultSelector)
	{
		if (first == null) throw new ArgumentNullException(nameof(first));
		if (second == null) throw new ArgumentNullException(nameof(second));
		if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

		using (var e1 = first.GetEnumerator())
		using (var e2 = second.GetEnumerator())
		{
			while (e1.MoveNext())
			{
				if (e2.MoveNext())
					yield return resultSelector(e1.Current, e2.Current);
				else
				{
					do {
						yield return resultSelector(e1.Current, default(T2));
					} while (e1.MoveNext());
				}
			}
			if (e2.MoveNext())
				do
				{
					yield return resultSelector(default(T1), e2.Current);
				} while (e2.MoveNext());
		}

	}




}