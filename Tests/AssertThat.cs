using System;
using System.Linq.Expressions;

namespace LeetCode.Tests
{
	public static class AssertThat
	{
		public static void AreEqual(this Assert that, string expected, Expression<Func<string>> actual)
		{
			try
			{
				string v = actual.Compile()();
				Assert.Equal(expected, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nActual: {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}

		//[DebuggerHidden]
		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AreEqual<T>(this Assert that, int expected, Expression<Func<T>> actual)
		{
			try
			{
				T v = actual.Compile()();
				Assert.Equal(expected, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nActual: {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}

		public static void AreEqual<T>(this Assert that, Expression<Func<T>> expected, Expression<Func<T>> actual)
		{
			try
			{
				T c = expected.Compile()();
				T v = actual.Compile()();
				Assert.Equal(c, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nExpected: {ExpressionToCode.ToCode(expected.Body)}\n  Actual: {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}
	}
}
