using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionToCodeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
				CollectionAssert.AreEqual(expected, v);
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
				CollectionAssert.AreEqual(c, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nExpected: {ObjectToCode.ComplexObjectToPseudoCode(c)} <-- {ExpressionToCode.ToCode(expected.Body)}\nActual:   {ObjectToCode.ComplexObjectToPseudoCode(v)} <-- {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}
	}
}
