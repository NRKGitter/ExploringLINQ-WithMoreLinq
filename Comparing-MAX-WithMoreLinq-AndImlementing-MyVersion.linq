<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

/*
Comparing Linq's Max method with MoreLinq's MaxBy and MinBy and implementing my own version of MaxBY and MinBY

This is really useful method when the built-in Max or Min methods in LINQ aren't quite what you need.

For example, say we have an IEnumerable<Book> and we wanted to find which book has the most pages, LINQ's Max method would tell us 
what the maximum PageCount was, but wouldn't tell us which book had that number of pages. MoreLINQ's MaxBy method gives us what we want -
it finds the book(s) that have the maximum number of pages.

Obviously there could be multiple books with the maximum numbers which is why it returns an IEnumerable<T> 
*/


void Main()
{
	var books = new[] {
		new { Author = "Robert Martin", Title = "Clean Code", Pages = 464 },
		new { Author = "Oliver Sturm", Title = "Functional Programming in C#" , Pages = 270 },
		new { Author = "Martin Fowler", Title = "Patterns of Enterprise Application Architecture", Pages = 533 },
		new { Author = "Mark Heath", Title = "More Effective Linq", Pages = 533 },
		new { Author = "Bill Wagner", Title = "Effective C#", Pages = 328 },
		};

	books.Max(b => b.Pages).Dump();
	
	// MoreLinq Nuget Package
	
	books.MaxBy(b => b.Pages).Dump();
	
	books.MinBy(b => b.Pages).Dump();

	books = new[] {
		new { Author = "Robert Martin", Title = "Clean Code", Pages = 464 },
		}.Where(e => false).ToArray();
	books.MaxBy(b => b.Pages).Dump();
	
	// My IMPLEMENTTAION
	
	books.MyMaxBy(b => b.Pages).Dump();


}

// Define other methods and classes here

public static class MyExt 
{
	public static IEnumerable<S> MyMaxBy<S, K>(this IEnumerable<S> source, Func<S, K> selector) // b => b.pages
	{

		var comparer = Comparer<K>.Default;
		if (source.Count() == 0)
		{
//			yield return default(S);
			yield break;
		}

		using ( var iterator = source.GetEnumerator() )
		using ( var iterator1 = source.GetEnumerator() )
		{
			
				
			var max = iterator.Current;
			var maxKey = selector(max);
			
			while(iterator.MoveNext())
			{
				var candidate = iterator.Current;
				var candidateProjected = selector(candidate);
				
				if ( comparer.Compare(candidateProjected, maxKey) > 0 )
				{
					max = candidate;
					maxKey = candidateProjected;
				}
				
			}
			
			

			while (iterator1.MoveNext())
			{
				var candidate = iterator1.Current;
				var candidateProjected = selector(candidate);

				if (comparer.Compare(candidateProjected, maxKey) == 0)
				{
					yield return candidate;
				}

			}
		}

		
		
	}
}
