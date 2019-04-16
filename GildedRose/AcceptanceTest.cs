using System;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace GildedRose
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class AcceptanceTest
    {
        [Test]
        public void ThirtyDays()
        {
            var consoleOutput = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOutput));
            Console.SetIn(new StringReader("a\n"));

            Program.Main(new string[] { });
            Approvals.Verify(consoleOutput.ToString());
        }
    }
}