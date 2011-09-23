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
			fib(target);
			stopwatch.Stop();
			var rawT = stopwatch.ElapsedTicks;
			stopwatch.Reset();
						
			// memoized fib function
			fib = fib.Memoize();
			
			// time it
			stopwatch.Start();
			fib(target);
			stopwatch.Stop();
			var memT = stopwatch.ElapsedTicks;
												
			return rawT > memT? true : false;
		}
		
		/// <summary>
		/// Not expecting any speed increase here.
		/// </summary>
		[Test()]
		public void CheapTest ()
		{			
			Assert.IsFalse(FibTest(10));
		}
				
		/// <summary>
		/// Should be faster.
		/// </summary>
		[Test()]
		public void ExpensiveTest ()
		{
			Assert.IsTrue(FibTest(35));
		}
	}
}

