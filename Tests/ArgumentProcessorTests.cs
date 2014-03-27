using System;

using NUnit.Framework;

using Salamander.Core;

namespace Salamander.Tests
{
	public class ArgumentProcessorTests
	{
		[Test]
		public void Arguments_ProcessArgumentString_SingleKeyValuePair()
		{
			string[] arguments = new string[] {"--test=myTestValue"};

			Arguments argProcessor = new Arguments(arguments);

			string expectedKey = "--test";
			string expectedValue = "myTestValue";

			Assert.AreEqual (expectedValue, argProcessor[expectedKey]);
		}

		[Test]
		public void Arguments_ProcessArgumentString_MultipleKeyValuePairs()
		{
			string[] arguments = new string[] {"--test=myTestValue", "-s=myString"};
			
			Arguments argProcessor = new Arguments(arguments);

			string expectedKey = "--test";
			string expectedValue = "myTestValue";

			Assert.AreEqual (expectedValue, argProcessor[expectedKey]);

			expectedKey = "-s";
			expectedValue = "myString";
			
			Assert.AreEqual (expectedValue, argProcessor[expectedKey]);
		}

		[Test]
		public void Arguments_ProcessArgumentString_SingleKeyValuePairQuotedString()
		{
			string[] arguments = new string[] {"--test=\"This is my quoted string\""};
			
			Arguments argProcessor = new Arguments(arguments);
			
			string expectedKey = "--test";
			string expectedValue = "This is my quoted string";
			
			Assert.AreEqual (expectedValue, argProcessor[expectedKey]);
		}

		[Test]
		public void Arguments_ProcessArgumentString_SingleKey()
		{
			string[] arguments = new string[] {"--doSomethingCool"};
			
			Arguments argProcessor = new Arguments(arguments);
			
			string expectedKey = "--doSomethingCool";
			string expectedValue = "";
			
			Assert.AreEqual (expectedValue, argProcessor[expectedKey]);
		}

		[Test]
		public void Arguments_ProcessArgumentString_ContainsSpecificKey()
		{
			string[] arguments = new string[] {"--doSomethingCool"};
			
			Arguments argProcessor = new Arguments(arguments);

			string expectedKey = "--doSomethingCool";
			bool expected = true;
			
			Assert.AreEqual (expected, argProcessor.ContainsKey(expectedKey));
		}

		[Test]
		public void Arguments_ProcessArgumentString_ContainsSpecificValue()
		{
			string[] arguments = new string[] {"--myKey=Quintillion"};
			
			Arguments argProcessor = new Arguments(arguments);
			
			string expectedValue = "Quintillion";
			bool expected = true;
			
			Assert.AreEqual (expected, argProcessor.ContainsValue(expectedValue));
		}

	}
}

