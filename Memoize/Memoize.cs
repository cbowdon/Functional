using System;
using System.Collections.Generic;

/// <summary>
/// This builds on a blog post by Wes Dyer ("Yet Another Language Geek")
/// where he gives the following memoiargsKeyation function and suggests that
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
			
			var hash = new Dictionary<object, R> ();
			
			return (a, b) =>
			{
				object[] argsKey = new object[2];
				argsKey[0] = a;
				argsKey[1] = b;
				R value;
				if (hash.TryGetValue (argsKey, out value)) {
					return value;
				} else {
					value = f (a, b);
					hash.Add (argsKey, value);
					return value;
				}
			};				
		}
		
		public static Func<T1, T2, T3, R> Memoize<T1, T2, T3, R> (this Func<T1, T2, T3, R> f)
		// Extended for three args
		{			
			
			var hash = new Dictionary<object, R> ();
			
			return (a, b, c) =>
			{
				object[] argsKey = new object[3];
				argsKey[0] = a;
				argsKey[1] = b;
				argsKey[2] = c;
				R value;
				if (hash.TryGetValue (argsKey, out value)) {
					return value;
				} else {
					value = f (a, b, c);
					hash.Add (argsKey, value);
					return value;
				}
			};				
		}
		
		public static Func<T1, T2, T3, T4, R> Memoize<T1, T2, T3, T4, R> (this Func<T1, T2, T3, T4, R> f)
		// Extended for four args
		{			
			
			var hash = new Dictionary<object, R> ();
			
			return (a, b, c, d) =>
			{
				object[] argsKey = new object[4];
				argsKey[0] = a;
				argsKey[1] = b;
				argsKey[2] = c;
				argsKey[3] = d;
				R value;
				if (hash.TryGetValue (argsKey, out value)) {
					return value;
				} else {
					value = f (a, b, c, d);
					hash.Add (argsKey, value);
					return value;
				}
			};				
		}
	}
}

