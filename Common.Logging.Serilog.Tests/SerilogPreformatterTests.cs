using NUnit.Framework;

namespace Common.Logging.Serilog.Tests
{
    [TestFixture]
    public class SerilogPreformatterTests
    {
        private SerilogPreformatter _preformatter;

        [SetUp]
        public void Setup()
        {
            _preformatter = new SerilogPreformatter();
        }
    }
}
