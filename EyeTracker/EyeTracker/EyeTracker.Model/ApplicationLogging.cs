using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace EyeTracker.Common.Logger
{

    public class ApplicationLogging
    {

        private static Collection<string> CreateDefaultCategoriesList(Type parentType)
        {
            var result = new Collection<string>();

            if (parentType != null)
            {
                foreach (string currentString in new[] { parentType.Namespace, parentType.Name })
                {
                    if(!string.IsNullOrEmpty(currentString))
                        result.Add(currentString);
                }
            }

            return result;
        }
        public Guid WriteFatalError(string message)
        {
            return WriteFatalError(null as Exception, message);
        }

        public Guid WriteFatalError(string format, params object[] args)
        {
            return WriteFatalError(string.Format(format, args));
        }

        public Guid WriteFatalError(Exception ex, string format, params object[] args)
        {
            return WriteFatalError(ex, string.Format(format, args));
        }

        public Guid WriteFatalError(Exception ex, string message)
        {
            return WriteError(true, ex, ApplicationEvent.FatalError, message);
        }

        public Guid WriteError(string message)
        {
            return WriteError(null as Exception, message);
        }

        public Guid WriteError(string format, params object[] args)
        {
            return WriteError(string.Format(format, args));
        }

        public Guid WriteError(bool withMessage, Exception ex, string format, params object[] args)
        {
            return WriteError(withMessage, ex, ApplicationEvent.Error, string.Format(format, args));
        }

        public Guid WriteError(Exception ex, string format, params object[] args)
        {
            return WriteError(true, ex, ApplicationEvent.Error, string.Format(format, args));
        }

        public Guid WriteError(Exception ex, string message)
        {
            return WriteError(true, ex, ApplicationEvent.Error, message);
        }

        public Guid WriteError(bool withMessage, Exception ex, ApplicationEvent errorType, string message)
        {
            var logEntry = new LogEntry();
            Guid errorTicket = Guid.NewGuid();
            logEntry.Categories = CreateDefaultCategoriesList(ParentType);
            logEntry.Message = message;
            if (null != ex)
            {
                logEntry.ExtendedProperties.Add("Exeption", ex);
            }
            logEntry.ExtendedProperties.Add("ErrorTicket", errorTicket);
            logEntry.Severity = errorType == ApplicationEvent.FatalError ? TraceEventType.Critical : TraceEventType.Error;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);

            if (withMessage)
            {
                //Send application event
                //Messenger.SendApplicationEvent(errorType, "Error Ticket: " + errorTicket + "<br>Message: " + message);
            }
            return errorTicket;
        }

        public static Guid WriteError(Type parentType, Exception ex, string message)
        {
            return new ApplicationLogging(parentType).WriteError(ex, message);
        }

        public static Guid WriteError(Type parentType, string message)
        {
            return WriteError(parentType, null, message);
        }

        public static void WriteInformation(Type parentType, string message)
        {
            Write(parentType, message, TraceEventType.Information);
        }

        public static void WriteWarning(Type parentType, string message)
        {
            Write(parentType, message, TraceEventType.Warning);
        }

        public void WriteWarning(string message)
        {
            Write(message, TraceEventType.Warning);
        }

        public void WriteWarning(string format, params object[] args)
        {
            WriteWarning(string.Format(format, args));
        }

        private void Write(string message, TraceEventType eventType)
        {
            var logEntry = new LogEntry();
            logEntry.Categories = CreateDefaultCategoriesList(ParentType);
            logEntry.Message = message;
            logEntry.Severity = eventType;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
        }

        /// <summary>
        /// Write debug information to log
        /// WARNING!!! : Use it in cases you need to log with hight frequency.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WriteInformation(string format, params object[] args)
        {
            WriteInformation(string.Format(format, args));
        }

        /// <summary>
        /// Write debug information to log
        /// WARNING!!! : Use it in cases you need to log with hight frequency.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WriteInformation(string message)
        {
            Write(message, TraceEventType.Information);
        }

        /// <summary>
        /// Write debug information to log
        /// WARNING!!! : Use it exept of cases you need to log with hight frequency.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WriteVerbose(string format, params object[] args)
        {
            WriteVerbose(string.Format(format, args));
        }

        /// <summary>
        /// Write debug information to log
        /// WARNING!!! : Use it exept of cases you need to log with hight frequency.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WriteVerbose(string message)
        {
            Write(message, TraceEventType.Verbose);
        }

        private static void Write(Type parentType, string message, TraceEventType eventType)
        {
            new ApplicationLogging(parentType).Write(message, eventType);
        }

        public Type ParentType { get; private set; }

        public ApplicationLogging(Type parentType)
        {
            this.ParentType = parentType;
        }
    }

}
