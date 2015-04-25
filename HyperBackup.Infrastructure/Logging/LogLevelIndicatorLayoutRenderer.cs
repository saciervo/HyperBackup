using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LayoutRenderers;

namespace HyperBackup.Infrastructure.Logging
{
    [LayoutRenderer("LogLevelIndicator")]
    class LogLevelIndicatorLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            // Only print LogLevel when it is Error or above. Otherwise print spaces instead.
            builder.Append(logEvent.Level >= LogLevel.Error ? string.Format("[{0}]", logEvent.Level) : "       ");
        }
    }
}
