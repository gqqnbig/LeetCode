using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExpressionToCodeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Tests
{
	public static class AssertThat
	{
		//[DebuggerHidden]
		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AreEqual(this Assert that, int expected, Expression<Func<int>> actual)
		{
			try
			{
				int v = actual.Compile()();
				Assert.AreEqual(expected, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nActual: {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}

		public static void AreEqual(this Assert that, Expression<Func<int>> expected, Expression<Func<int>> actual)
		{
			try
			{
				int c = expected.Compile()();
				int v = actual.Compile()();
				Assert.AreEqual(c, v);
			}
			catch (AssertFailedException e)
			{
				throw new AssertFailedException($"{e.Message}\nExpected: {ExpressionToCode.ToCode(expected.Body)}\n  Actual: {ExpressionToCode.ToCode(actual.Body)}", e);
			}
		}
	}
}
