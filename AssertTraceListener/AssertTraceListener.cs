using System;
using System.Diagnostics;

namespace AssertTraceListener
{
    public class AssertTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            // do nothing
        }

        public override void WriteLine(string message)
        {
            // do nothing
        }

        public override void Fail(string message)
        {
            base.Fail(message);
        }

        public override void Fail(string message, string detailMessage)
        {
            base.Fail(message, detailMessage);
        }
    }
}
