using System;
using System.Diagnostics;
using System.Threading;
using Memoize;
using NUnit.Framework;

namespace MemoizeTests
{
	/// <summary>
	/// The Memoize function is tested for speed and accuracy
	/// </summary>
	[TestFixture()]
	public class TimeTest
	{
		/// <summary>
		/// General memoization test routine
		/// </summary>
		/// <returns>
		/// True if mem'd function is faster
		/// </returns>
		/// <param name='f'>
		/// The function to test
		/// </param>
		public bool MemzTest (Func<int, int> f, int target)
		{
			Stopwatch stopwatch = new Stopwatch ();
						
			// time it raw
			stopwatch.Start ();
			var rawR = f (target);
			stopwatch.Stop ();
			var rawT = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
						
			// memoized function
			f = f.Memoize ();
			
			// time it memoized:
			
			// first run : same speed
			stopwatch.Start ();
			f (target);
			stopwatch.Stop ();
			var memT1 = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
			
			// second run: faster			
			stopwatch.Start ();
			var memR = f (target);
			stopwatch.Stop ();
			var memT2 = stopwatch.ElapsedTicks;
				
			// Accurate?
			Assert.AreEqual (rawR, memR);
			
			// Mem is faster?
//			Console.WriteLine("{0}\t{1}\t{2}", rawT, memT1, memT2);			
			return rawT > memT2 ? true : false;
		}
		
		/// <summary>
		/// Well let's see then
		/// </summary>
		[Test()]
		public void SingleArgTestFunctions ()
		{						
			Func<int, int > fib = n => n > 1 ? fib (n - 1) + fib (n - 2) : n;
			Func<int, int > fact = n => n > 1 ? n * fact (n - 1) : 1;			
			Func<int, int > sleeper = n => {
				Thread.Sleep (10);
				return n > 1 ? sleeper (n - 1) + sleeper (n - 2) : n;
			};
			
			Assert.IsTrue (MemzTest (fib, 10)); 
			Assert.IsTrue (MemzTest (fact, 10));
			Assert.IsTrue (MemzTest (sleeper, 10)); 
		}
		
		[Test()]
		public void MultiArgTestFunctions ()
		{
			Assert.IsTrue(false);
		}
	}
}

