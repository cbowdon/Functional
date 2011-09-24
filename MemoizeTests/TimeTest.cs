using System;
using System.Diagnostics;
using Memoize;
using NUnit.Framework;

namespace MemoizeTests
{
	/// <summary>
	/// The Memoize function is tested for speed.
	/// Looking up a value is more expensive that performing a simple sum,
	/// so Memoize() is only useful when the sum is complex
	/// </summary>
	[TestFixture()]
	public class TimeTest
	{
		/// <summary>
		/// Tests memoization on fibonacci function
		/// </summary>
		/// <returns>
		/// Bool on whether raw or mem was faster
		/// </returns>
		/// <param name='target'>
		/// Fibonacci number to test to
		/// </param>
		public bool FibTest (int target)
		{
			Stopwatch stopwatch = new Stopwatch();
						
			// raw fib function
			Func<int, int> fib = n => n > 1? fib(n-1)+fib(n-2) : n;
			
			// time it
			stopwatch.Start();
			var rawR = fib(target);
			stopwatch.Stop();
			var rawT = stopwatch.ElapsedTicks;
			stopwatch.Reset();
						
			// memoized fib function
			fib = fib.Memoize();
			
			// time it
			stopwatch.Start();
			var memR = fib(target);
			stopwatch.Stop();
			var memT = stopwatch.ElapsedTicks;
				
			Assert.AreEqual(rawR, memR);
			
			return rawT > memT? true : false;
		}
		
		/// <summary>
		/// Imaginatively just does two fibs
		/// </summary>
		/// <returns>
		/// Bool for mem faster than raw
		/// </returns>
		/// <param name='target1'>
		/// Number to count to
		/// </param>
		/// <param name='target2'>
		/// Second number to count to
		/// </param>
		public bool FactorialTest (int target1)
		{
			Stopwatch stopwatch = new Stopwatch();
			
			// raw dual fib function
			Func<int, int> fact = n => n > 1? n*fact(n-1) : 1;			
			
			Console.WriteLine("FACT: {0}", fact(target1));
			
			// time it
			stopwatch.Start();
			var rawR = fact(target1);
			stopwatch.Stop();
			var rawT = stopwatch.ElapsedTicks;
			
			// mem dual fib function
			fact = fact.Memoize();
			
			fact(target1);
			
			// time it			
			stopwatch.Start();
			var memR = fact(target1);
			stopwatch.Stop();
			var memT = stopwatch.ElapsedTicks;
									
			Console.WriteLine("raw: {0}\t mem: {1}", rawT, memT);
			
			
			Assert.AreEqual(rawR, memR);
			Console.WriteLine("result: {0}", rawR);
			
			return rawT > memT? true : false;
		}
		
		/// <summary>
		/// Not expecting any speed increase here.
		/// </summary>
		[Test()]
		public void CheapTest ()
		{			
			Assert.IsFalse(FibTest(10));
			Assert.IsFalse(FactorialTest(10));
		}
				
		/// <summary>
		/// Should be faster.
		/// </summary>
		[Test()]
		public void ExpensiveTest ()
		{
			Assert.IsTrue(FibTest(35));
			Assert.IsTrue(FactorialTest(16));
		}
	}
}

