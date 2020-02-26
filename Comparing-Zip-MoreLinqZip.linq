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
	
	
	
}

// Define other methods and classes here
