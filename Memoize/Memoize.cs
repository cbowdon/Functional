using System;
using System.Collections.Generic;

/// <summary>
/// This builds on a blog post by Wes Dyer ("Yet Another Language Geek")
/// where he gives the following memoization function and suggests that
/// it could be generalised to multiple-argument functions
/// </summary>
namespace Memoize
{
	public static class FP
	{
		/// <summary>
		/// Memoize the specified f.
		/// </summary>
		/// <param name='f'>
		/// Any function
		/// </param>
		/// <typeparam name='A'>
		/// The 1st type parameter.
		/// </typeparam>
		/// <typeparam name='R'>
		/// The 2nd type parameter.
		/// </typeparam>
		public static Func<T1, R> Memoize<T1, R> (this Func<T1, R> f)
		// Wes' original function	
		// using 'this' keyword so that the method can be employed as an extension
		{
			var hash = new Dictionary<T1, R> ();
			return a =>
			{
				R value;
				if (hash.TryGetValue (a, out value)) {
					return value;
				} else {
					value = f (a);
					hash.Add (a, value);
					return value;
				}
			};				
		}
		
		public static Func<T1, T2, R> Memoize<T1, T2, R> (this Func<T1, T2, R> f)
		// Extended for two args
		{			
			
			var hash = new Dictionary<int, R> ();
			
			return (a, b) =>
			{
				int z = a.GetHashCode() ^ b.GetHashCode();
				R value;
				if (hash.TryGetValue (z, out value)) {
					return value;
				} else {
					value = f (a, b);
					hash.Add (z, value);
					return value;
				}
			};				
		}
	}
}

