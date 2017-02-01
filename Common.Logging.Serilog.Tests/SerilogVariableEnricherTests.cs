using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog.Tests
{
    public class TestSerilogSink : ILogEventSink
    {
        public Queue<LogEvent> Events = new Queue<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            this.Events.Enqueue(logEvent);
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

            Assert.AreEqual(s.Sink.Events.Count, 1);

            var @event = s.Sink.Events.Dequeue();

            // count includes "message"
            Assert.AreEqual(@event.Properties.Count, 3);
            Assert.AreEqual(@event.Properties["Var1"].ToString(), "1");
            Assert.AreEqual(@event.Properties["Var2"].ToString(), "2");

            // create new thread and verify that the variables are avaliable
            Task.Factory.StartNew(
                () =>
                    {
                        s.Logger.Info("Fun");
                        @event = s.Sink.Events.Dequeue();

                        Assert.AreEqual(@event.Properties.Count, 3);
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

            Assert.AreEqual(s.Sink.Events.Count, 1);

            var @event = s.Sink.Events.Dequeue();

            // count includes "message"
            Assert.AreEqual(@event.Properties.Count, 2);
            Assert.AreEqual(@event.Properties["Var10"].ToString(), "10");

            // create new thread and verify that the variables are not avaliable
            Task.Factory.StartNew(
                () =>
                    {
                        s.Logger.Info("Fun");
                        @event = s.Sink.Events.Dequeue();

                        Assert.AreEqual(@event.Properties.Count, 1);
                    }).Wait();
        }

        static TestLoggerStructure GetSerilog()
        {
            var sink = new TestSerilogSink();
            var log = new LoggerConfiguration().WriteTo.Sink(sink).MinimumLevel.Verbose().CreateLogger();
            var logger = new SerilogCommonLogger(log);
            logger.GlobalVariablesContext.Clear();
            logger.ThreadVariablesContext.Clear();

            return new TestLoggerStructure(logger, sink);
        }

        internal class TestLoggerStructure
        {
            public SerilogCommonLogger Logger { get; }
            public TestSerilogSink Sink { get; }

            public TestLoggerStructure(SerilogCommonLogger logger, TestSerilogSink sink)
            {
                this.Logger = logger;
                this.Sink = sink;
            }
        }
    }
}