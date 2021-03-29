using System;
using System.Collections.Generic;

namespace RIDER_60503
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			var values = new HashSet<CustomVector3>(ScaleCubeVectorExtender.GetValuesForX(new CustomVector3(1, 1, 1)));
			Console.WriteLine("Checking breakpoint");
			Console.ReadKey();
		}
	}


}
