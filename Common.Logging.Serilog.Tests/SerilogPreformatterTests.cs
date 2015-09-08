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

        public class When_Calling_GetIndecesOfNumericalFormatting : SerilogPreformatterTests
        {
            [Test]
            public void Should_Return_Empty_List_If_Template_String_Is_Not_Formatted()
            {
                /* Setup */
                const string originalTemplate = "This is a template without any args";

                /* Test */
                var result = _preformatter.GetIndecesOfNumericalFormatting(originalTemplate);

                /* Assert */
                Assert.That(result, Is.Empty);
            }

            [TestCase("A pure numeric string with {0} and {1}.", new[] {0,1})]
            [TestCase("A pure {@Serilog} formatted string", new int[0])]
            [TestCase("A mixed string with both {@Seri} and numeric {1} formatting", new[] {1})]
            [TestCase("A string with {1}, {10} and {100}", new[] {1, 10, 100})]
            public void Should_Return_Expected_Indeces(string originalTemplate, int[] expectedResult)
            {
                /* Setup */
                /* Test */
                var result = _preformatter.GetIndecesOfNumericalFormatting(originalTemplate);

                /* Assert */
                Assert.That(result, Is.EquivalentTo(expectedResult));
            }
        }
    }
}
