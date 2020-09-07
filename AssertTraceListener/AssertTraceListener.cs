using System;
using System.Diagnostics;
using System.Threading;

namespace AssertTraceListener
{
    public class AssertTraceListener : TraceListener
    {
        public AssertTraceListener()
        {
            // WPF initialization creates various trace listeners of its own when a debugger is attached,
            // which share a lock object with the Debug.Trace stack.  Thus, if WPF has not been intialized
            // by the time we try to show our dailog in a Debug.Fail()/Debug.Assert() call, we end up in a
            // deadlock.
            // As a workaround, if we see a debugger now, we'll forcefully initialize the WPF stack.
            if(Debugger.IsAttached)
            {
                InitWpfStack();
            }
        }

        public override void Write(string message)
        {
            // do nothing
        }

        public override void WriteLine(string message)
        {
            // do nothing
        }

        public override void Fail(string? message, string? detailMessage)
        {
            // top 4 frames are TraceListener implementation details, so we need to skip them to get to the Debug.Assert call
            var stackTrace = new StackTrace(4, true);
            var vm = new AssertionViewModel(message ?? string.Empty, detailMessage ?? string.Empty, stackTrace.ToString());

            DialogAction action = ShowAssertDialog(vm);
            switch (action)
            {
                case DialogAction.Quit:
                    string exitMessage = message + (detailMessage?.Length > 0 ? Environment.NewLine + detailMessage : null);
                    Environment.FailFast(exitMessage);
                    break;
                case DialogAction.Debug:
                    if (!Debugger.IsAttached)
                    {
                        Debugger.Launch();
                    }
                    Debugger.Break();
                    break;
            }
        }

        private void InitWpfStack()
        {
            using var resetEvent = new AutoResetEvent(false);

            // We can't be sure that the calling thread is STA (necessary to create the Window),
            // so we'll create a thread to make sure it's compliant.
            var t = new Thread(() => {
                // create an instance of our window to make sure all the WPF tracing gets initialized
                var dialog = new DebugAssertWindow(new AssertionViewModel(string.Empty, string.Empty, string.Empty));
                resetEvent.Set();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            resetEvent.WaitOne();
        }


        private DialogAction ShowAssertDialog(AssertionViewModel vm)
        {
            using var resetEvent = new AutoResetEvent(false);
            DialogAction action = DialogAction.Ignore;
            var t = new Thread(() => {
                var dialog = new DebugAssertWindow(vm);
                dialog.ShowDialog();
                action = dialog.Action;
                resetEvent.Set();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            resetEvent.WaitOne();

            return action;
        }
    }
}
