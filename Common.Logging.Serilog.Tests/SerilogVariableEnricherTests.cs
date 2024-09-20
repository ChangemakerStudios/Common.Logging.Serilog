using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog.Tests;

public class TestSerilogSink : ILogEventSink
{
    public Queue<LogEvent> Events = new();

    public void Emit(LogEvent logEvent)
    {
        Events.Enqueue(logEvent);
    }
}

[TestFixture]
public class SerilogVariableEnricherTests
{
    [Test]
    public void GlobalVariableShouldEnrichLog()
    {
        var s = GetSerilog();

        s.Logger.GlobalVariablesContext.Set("Var1", 1);
        s.Logger.GlobalVariablesContext.Set("Var2", 2);

        s.Logger.Info("Nothing");

        s.Sink.Events.Count.Should().Be(1);

        var @event = s.Sink.Events.Dequeue();

        // count includes "message"
        @event.Properties.Count.Should().Be(3);
        @event.Properties["Var1"].ToString().Should().Be("1");
        @event.Properties["Var2"].ToString().Should().Be("2");

        // create new thread and verify that the variables are available
        Task.Run(
            () =>
            {
                s.Logger.Info("Fun");
                @event = s.Sink.Events.Dequeue();

                @event.Properties.Count.Should().Be(3);
            }).Wait();
    }

    [Test]
    public void ThreadVariableShouldEnrichLog()
    {
        var s = GetSerilog();

        s.Logger.ThreadVariablesContext.Set("Var1", 1);
        s.Logger.ThreadVariablesContext.Remove("Var1");
        s.Logger.ThreadVariablesContext.Set("Var10", 10);

        s.Logger.Info("Nothing");

        s.Sink.Events.Count.Should().Be(1);

        var @event = s.Sink.Events.Dequeue();

        // count includes "message"
        @event.Properties.Count.Should().Be(2);
        @event.Properties["Var10"].ToString().Should().Be("10");

        // create new thread and verify that the variables are not available
        Task.Run(
            () =>
            {
                s.Logger.Info("Fun");
                @event = s.Sink.Events.Dequeue();

                @event.Properties.Count.Should().Be(1);
            }).Wait();
    }

    private static TestLoggerStructure GetSerilog()
    {
        var sink = new TestSerilogSink();
        var log = new LoggerConfiguration().WriteTo.Sink(sink).MinimumLevel.Verbose()
            .CreateLogger();
        var logger = new SerilogCommonLogger(log);
        logger.GlobalVariablesContext.Clear();
        logger.ThreadVariablesContext.Clear();

        return new TestLoggerStructure(logger, sink);
    }

    internal class TestLoggerStructure(SerilogCommonLogger logger, TestSerilogSink sink)
    {
        public SerilogCommonLogger Logger { get; } = logger;
        public TestSerilogSink Sink { get; } = sink;
    }
}