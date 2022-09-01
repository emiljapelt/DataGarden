
using OCollection;
using OtherCollections;

var list1 = new LAList<int>();

for(int i = 0; i < 20; i++) list1.Add(i);

for(int i = 0; i < 20; i++) System.Console.Write(list1[i]+" ");
System.Console.WriteLine();


var list2 = new LAList<int>(5);

for(int i = 0; i < 20; i++) list2.Add(i);

for(int i = 0; i < 20; i++) System.Console.Write(list2[i]+" ");
System.Console.WriteLine();


var ODict = new ODictionary<string, int>();

ODict.Add("emil", 1);
ODict.Add("hans", 2);
ODict.Add("benny", 3);

System.Console.WriteLine(ODict.Get("emil"));
System.Console.WriteLine(ODict.Get(1));
System.Console.WriteLine(ODict.Get("hans"));
System.Console.WriteLine(ODict.Get(2));
System.Console.WriteLine(ODict.Get("benny"));
System.Console.WriteLine(ODict.Get(3));

var MEMEM = new ODictionary<string, string>();

MEMEM.Add("emil", "hans");

System.Console.WriteLine(MEMEM.Get(l: "mil"));
