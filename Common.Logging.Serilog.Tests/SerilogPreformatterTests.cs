﻿using NUnit.Framework;

namespace Common.Logging.Serilog.Tests;

[TestFixture]
public class SerilogPreformatterTests
{
    [SetUp]
    public void Setup()
    {
        _preformatter = new SerilogPreformatter();
        _resultTemplate = string.Empty;
        _resultArgs = null;
    }

    private SerilogPreformatter _preformatter;
    private string _resultTemplate;
    private object[] _resultArgs;

    [Test]
    public void Should_Return_Same_Template_String_If_Original_String_Is_Null_Or_Empty()
    {
        /* Setup */
        const string originalTemplate = null;

        /* Test */
        var result = _preformatter.TryPreformat(originalTemplate, null, out _resultTemplate,
            out _resultArgs);

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
        var result = _preformatter.TryPreformat(originalTemplate, null, out _resultTemplate,
            out _resultArgs);

        /* Assert */
        Assert.That(result, Is.True);
        Assert.That(_resultTemplate, Is.EqualTo(originalTemplate));
    }

    [Test]
    public void Should_Format_Entire_String_If_Only_Numeric_Formatting_Is_Used()
    {
        /* Setup */
        const string originalTemplate = "Look at this, a {0} formatted string!";
        var args = new object[] { "nicely " };
        var expected = string.Format(originalTemplate, args);

        /* Test */
        var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate,
            out _resultArgs);

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
        var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate,
            out _resultArgs);

        /* Assert */
        Assert.That(result, Is.True);
        Assert.That(_resultTemplate, Is.EqualTo(originalTemplate));
    }

    [Test]
    public void
        Should_Preformat_Numeric_Part_Of_String_But_Keep_Serilog_Formatted_String_And_Arguments()
    {
        /* Setup */
        const string originalTemplate = "This {@string} has {1} numeric match";
        var args = new object[] { "serilog", "1" };
        const string expectedTemplate = "This {@string} has 1 numeric match";


        /* Test */
        var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate,
            out _resultArgs);

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
        var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate,
            out _resultArgs);

        /* Assert */
        Assert.That(result, Is.True);
        Assert.That(_resultTemplate, Is.EqualTo(expected));
    }

    [TestCase("Two different numbers {Number1} and {Number2}!", new object[] { 0, 1 },
        "Two different numbers {Number1} and {Number2}!", new object[] { 0, 1 })]
    [TestCase("Two duplicate numbers {Number1} and {Number2}!", new object[] { 1, 1 },
        "Two duplicate numbers {Number1} and {Number2}!", new object[] { 1, 1 })]
    [TestCase("Three numbers, only one unique {Number1}, not {1}, and {Number2}!",
        new object[] { 0, 42, 0 },
        "Three numbers, only one unique {Number1}, not 42, and {Number2}!", new object[] { 0, 0 })]
    [TestCase("Reverse order numbers: {1} and {0}!", new object[] { 0, 1 },
        "Reverse order numbers: 1 and 0!", new object[0])]
    [TestCase("Same number twice: {0} and {0}!", new object[] { 0 },
        "Same number twice: 0 and 0!", new object[0])]
    public void Should_Be_Able_To_Correct_Format_Numeric_Arguments(string originalTemplate,
        object[] args, string expectedResultTemplate, object[] expectedArgs)
    {
        /* Test */
        var result = _preformatter.TryPreformat(originalTemplate, args, out _resultTemplate,
            out _resultArgs);

        /* Assert */
        Assert.That(result, Is.True);
        Assert.That(_resultTemplate, Is.EqualTo(expectedResultTemplate));
        Assert.That(_resultArgs, Is.EquivalentTo(expectedArgs));
    }
}