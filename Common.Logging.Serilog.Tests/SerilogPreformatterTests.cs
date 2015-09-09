using NUnit.Framework;

namespace Common.Logging.Serilog.Tests
{
    [TestFixture]
    public class SerilogPreformatterTests
    {
        private SerilogPreformatter _preformatter;
        private string _resultTemplate;
        private object[] _resultArgs;

        [SetUp]
        public void Setup()
        {
            _preformatter = new SerilogPreformatter();
            _resultTemplate = string.Empty;
            _resultArgs = null;
        }

        [Test]
        public void Should_Return_Same_Template_String_If_Original_String_Is_Null_Or_Empty()
        {
            /* Setup */
            const string originalTemplate = null;

            /* Test */
            var result = _preformatter.TryPreformat(originalTemplate, null, out _resultTemplate, out _resultArgs);

            /* Assert */
            Assert.That(result, Is.True);
            Assert.That(_resultTemplate, Is.EqualTo(originalTemplate));
        }

        [Test]
        public void Should_Return_Same_Template_String_If_No_Args_Provided()
        {
            /* Setup */
            const string originalTemplate = "This is a template without any args";
            
            /* Test */
            var result = _preformatter.TryPreformat(originalTemplate, null, out _resultTemplate, out _resultArgs);

            /* Assert */
            Assert.That(result, Is.True);
            Assert.That(_resultTemplate, Is.EqualTo(originalTemplate));
        }

        [Test]
        public void Should_Format_Entire_String_If_Only_Numeric_Formatting_Is_Used()
        {
            /* Setup */
            const string originalTemplate = "Look at this, a {0} formatted string!";
            var args = new object[] {"nicely "};
            var expected = string.Format(originalTemplate, args);

            /* Test */
            var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate, out _resultArgs);
            
            /* Assert */
            Assert.That(result, Is.True);
            Assert.That(_resultTemplate, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Keep_String_Unformatted_If_Serilog_Formatting_Used()
        {
            /* Setup */
            const string originalTemplate = "Look at this, a {serilog} formatted string!";
            var args = new object[] { "serilog" };

            /* Test */
            var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate, out _resultArgs);

            /* Assert */
            Assert.That(result, Is.True);
            Assert.That(_resultTemplate, Is.EqualTo(originalTemplate));
        }

        [Test]
        public void Should_Preformat_Numeric_Part_Of_String_But_Keep_Serilog_Formatted_String_And_Arguments()
        {
            /* Setup */
            const string originalTemplate = "This {@string} has {1} numeric match";
            var args = new object[] { "serilog", "1" };
            const string expectedTemplate = "This {@string} has 1 numeric match";


            /* Test */
            var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate, out _resultArgs);

            /* Assert */
            Assert.That(result, Is.True);
            Assert.That(_resultTemplate, Is.EqualTo(expectedTemplate));
            Assert.That(_resultArgs.Length, Is.EqualTo(1));
            Assert.That(_resultArgs[0], Is.EqualTo(args[0]));
        }

        [Test]
        public void Should_Be_Able_To_Correct_Format_Numeric_String_With_Several_Arguments()
        {
            /* Setup */
            const string originalTemplate = "Look at this, a {0} formatted string with {1} arguments!";
            var args = new object[] { "nicely", "2" };
            var expected = string.Format(originalTemplate, args);

            /* Test */
            var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate, out _resultArgs);

            /* Assert */
            Assert.That(result, Is.True);
            Assert.That(_resultTemplate, Is.EqualTo(expected));
        }
    }
}
