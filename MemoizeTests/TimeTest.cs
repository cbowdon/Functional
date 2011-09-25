using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
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
			
			// Mem2 is faster than mem1?
			Assert.GreaterOrEqual(memT1, memT2);
			
			// Mem2 is faster than raw?
			//Console.WriteLine("{0}\t{1}\t{2}", rawT, memT1, memT2);			
			return rawT > memT2 ? true : false;
		}
		
		public bool MemzTest (Func<int, int, int> f, int target1, int target2)
		{
			Stopwatch stopwatch = new Stopwatch ();
						
			// time it raw
			stopwatch.Start ();
			var rawR = f (target1, target2);
			stopwatch.Stop ();
			var rawT = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
						
			// memoized function
			f = f.Memoize ();
			
			// time it memoized:
			
			// first run : same speed
			stopwatch.Start ();
			f (target1, target2);
			stopwatch.Stop ();
			var memT1 = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
			
			// second run: faster			
			stopwatch.Start ();
			var memR = f (target1, target2);
			stopwatch.Stop ();
			var memT2 = stopwatch.ElapsedTicks;
				
			// Accurate?
			Assert.AreEqual (rawR, memR);
			
			// Mem2 is faster than mem1?
			Assert.GreaterOrEqual(memT1, memT2);
			
			// Mem2 is faster than raw?
			//Console.WriteLine("{0}\t{1}\t{2}", rawT, memT1, memT2);			
			return rawT > memT2 ? true : false;
		}
		
		public bool MemzTest (Func<int, int, string, int> f, int target1, int target2, string target3)
		{
			Stopwatch stopwatch = new Stopwatch ();
						
			// time it raw
			stopwatch.Start ();
			var rawR = f (target1, target2, target3);
			stopwatch.Stop ();
			var rawT = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
						
			// memoized function
			f = f.Memoize ();
			
			// time it memoized:
			
			// first run : same speed
			stopwatch.Start ();
			f (target1, target2, target3);
			stopwatch.Stop ();
			var memT1 = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
			
			// second run: faster			
			stopwatch.Start ();
			var memR = f (target1, target2, target3);
			stopwatch.Stop ();
			var memT2 = stopwatch.ElapsedTicks;
				
			// Accurate?
			Assert.AreEqual (rawR, memR);
			
			// Mem2 is faster than mem1?
			Assert.GreaterOrEqual(memT1, memT2);
			
			// Mem2 is faster than raw?
			//Console.WriteLine("{0}\t{1}\t{2}", rawT, memT1, memT2);			
			return rawT > memT2 ? true : false;
		}
		
		public bool MemzTest (Func<double, string, byte, List<int>, double> f, double target1, string target2, byte target3, List<int> target4)
		{
			Stopwatch stopwatch = new Stopwatch ();
						
			// time it raw
			stopwatch.Start ();
			var rawR = f (target1, target2, target3, target4);
			stopwatch.Stop ();
			var rawT = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
						
			// memoized function
			f = f.Memoize ();
			
			// time it memoized:
			
			// first run : same speed
			stopwatch.Start ();
			f (target1, target2, target3, target4);
			stopwatch.Stop ();
			var memT1 = stopwatch.ElapsedTicks;
			stopwatch.Reset ();
			
			// second run: faster			
			stopwatch.Start ();
			var memR = f (target1, target2, target3, target4);
			stopwatch.Stop ();
			var memT2 = stopwatch.ElapsedTicks;
				
			// Accurate?
			Assert.AreEqual (rawR, memR);
			
			// Mem2 is faster than mem1?
			Assert.GreaterOrEqual(memT1, memT2);
			
			// Mem2 is faster than raw?
			//Console.WriteLine("{0}\t{1}\t{2}", rawT, memT1, memT2);			
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
				Thread.Sleep (1);
				return n > 1 ? sleeper (n - 1) + sleeper (n - 2) : n;
			};
			
			Assert.IsFalse (MemzTest (fib, 10)); 
			Assert.IsTrue (MemzTest (fact, 10));
			Assert.IsTrue (MemzTest (sleeper, 10)); 
		}
		
		[Test()]
		public void MultiArgTestFunctions ()
		{
			Func<int, int, int> add2Ints = (n1, n2) => {
				Thread.Sleep(1);
				return n1+n2;
			};
			
			Func<int, int, string, int> add3Ints = (n1, n2, n3) => {
				Thread.Sleep(1);
				return n1+n2+n3.GetHashCode();
			};
			
			Func<double, string, byte, List<int>, double> add4Stuffs =
				(n1, n2, n3, n4) => {
				Thread.Sleep(1);
				return n1+n2.GetHashCode()+n3.GetHashCode()+n4.GetHashCode();
			};
		
			Assert.IsTrue(MemzTest(add2Ints, 10, 11));
			Assert.IsTrue(MemzTest(add3Ints, 10, 11, "12"));
			Assert.IsTrue(MemzTest(add4Stuffs, 10.0, "11", 12, new List<int>(){13,14,15}));
		}
	}
}

