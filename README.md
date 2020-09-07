# AssertTraceListener
This library recreates a similar experience that many developers are familiar with when using `Debug.Assert(...)` in .NET Framework projects:

// TODO: image

# Installing and using the library

To use the AssertTraceListener, you need to first remove the default listener, then add the new one:

```
Trace.Listeners.Remove("Default");
Trace.Listeners.Add(new AssertTraceListener.AssertTraceListener());
```

Removing the default listener is necessary, as its implemention will terminate the program when `Debug.Fail()` is handled.

# Building and contributing

// TODO
