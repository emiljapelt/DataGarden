
using OCollection;

Console.WriteLine("Starting...");

var tree = new ScapeGoatTree<int, string>(0.51);

for(int i = 0; i < 10; i++) tree.Insert(i, "wow");

tree.Print();

Console.WriteLine("Ending...");
