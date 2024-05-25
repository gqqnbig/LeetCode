using System;
using System.Linq.Expressions;

namespace LeetCode.Tests
{
	public static class CollectionAssertThat
	{
		public static void AreEqual<T>(this CollectionAssert that, T[] expected, Expression<Func<T[]>> actual)
		{
			T[] v = default;
			try
			{
				v = actual.Compile()();
				CollectionAssert.Equal(expected, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nExpected: {ObjectToCode.ComplexObjectToPseudoCode(expected)}\nActual:   {ObjectToCode.ComplexObjectToPseudoCode(v)} <-- {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}

		public static void AreEqual<T>(this CollectionAssert that, Expression<Func<T[]>> expected, Expression<Func<T[]>> actual)
		{
			T[] v = default;
			T[] c = default;
			try
			{
				c = expected.Compile()();
				v = actual.Compile()();
				CollectionAssert.Equal(c, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nExpected: {ObjectToCode.ComplexObjectToPseudoCode(c)} <-- {ExpressionToCode.ToCode(expected.Body)}\nActual:   {ObjectToCode.ComplexObjectToPseudoCode(v)} <-- {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}
	}
}
