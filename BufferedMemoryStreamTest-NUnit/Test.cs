using NUnit.Framework;
using System;

namespace BufferedMemoryStreamTestNUnit
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			using (BufferedMemoryStream.BufferedMemoryStream memStream = new BufferedMemoryStream.BufferedMemoryStream())
			{
				byte[] buffer = new byte[2];
				buffer[0] = 1;
				buffer[1] = 2;
				int bytesRead = 2;

				memStream.Write(buffer, 0, bytesRead);

				buffer[0] = 3;
				buffer[1] = 4;

				memStream.Write(buffer, 0, bytesRead);

				byte[] result = memStream.ToArray();

				Assert.AreEqual(4, memStream.Length, "Stream has invalid length");

				CollectionAssert.AreEquivalent(new byte[] { 1, 2, 3, 4 }, result);
			}
		}
	}
}

