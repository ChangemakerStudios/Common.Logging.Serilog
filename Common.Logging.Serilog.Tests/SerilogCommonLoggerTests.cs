﻿using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog.Tests;

[TestFixture]
public class SerilogCommonLoggerTests
{
    [SetUp]
    public void Setup()
    {
        _seriLogger = new Mock<ILogger>();

        _seriLogger.Setup(s => s.ForContext(It.IsAny<ILogEventEnricher>()))
            .Returns(_seriLogger.Object);
        _seriLogger.Setup(l => l.IsEnabled(It.IsAny<LogEventLevel>())).Returns(true);

        _commonLogger = new SerilogCommonLogger(_seriLogger.Object);
    }

    private Mock<ILogger> _seriLogger;
    private SerilogCommonLogger _commonLogger;

    [Test]
    public void Should_Leave_String_As_Is_If_Formatted_With_Serilog_Syntax()
    {
        /* Setup */
        const string templateString = "This is a {@serilog} formatted string";
        const string expectedString = templateString;

        var arg = new { Type = "Serilog" };
        var expectedArgs = new object[] { arg };
        _seriLogger
            .Setup(l => l.Write(
                It.IsAny<LogEventLevel>(),
                It.IsAny<Exception>(),
                expectedString,
                expectedArgs
            ))
            .Verifiable();

        /* Test */
        _commonLogger.DebugFormat(templateString, arg);

        /* Should be same result */
        _commonLogger.Debug(m => m(templateString, arg));

        /* Assert */
        _seriLogger.VerifyAll();
    }

    [Test]
    public void Should_Preformat_String_If_Numerical_Formatted()
    {
        /* Setup */
        const string templateString = "This is a {0} formatted string";
        var arg = "nummeric";
        var expectedString = string.Format(templateString, arg);

        _seriLogger
            .Setup(l => l.Write(
                It.IsAny<LogEventLevel>(),
                It.IsAny<Exception>(),
                expectedString,
                It.Is<object[]>(s => !s.Any())
            ))
            .Verifiable();

        /* Test */
        _commonLogger.DebugFormat(templateString, arg);

        /* Assert */
        _seriLogger.VerifyAll();
    }

    [Test]
    public void Should_Preformat_Numeric_Formatting_But_Leave_Serilog_Formating()
    {
        /* Setup */
        const string templateString = "This is a {0} formatted string with {@serilog} args, too";
        var args = new object[] { "nummeric", new { type = "Serilog" } };
        const string expectedString =
            "This is a nummeric formatted string with {@serilog} args, too";

        _seriLogger
            .Setup(l => l.Write(
                It.IsAny<LogEventLevel>(),
                It.IsAny<Exception>(),
                expectedString,
                It.Is<object[]>(s => s.Count() == 1 && s[0] == args[1])
            ))
            .Verifiable();

        /* Test */
        _commonLogger.DebugFormat(templateString, args);

        /* Assert */
        _seriLogger.VerifyAll();
    }
}