using Microsoft.Extensions.Logging;

namespace Exemplo.Api.Logger;

public class TraceLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new TraceLogger(categoryName);

    public void Dispose() { }
}









