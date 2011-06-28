using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization;
using EyeTracker.Common.Logger;
using System.Reflection;



namespace EyeTracker.Common
{
    public class OperationResult
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

//#if DEBUG
//            this.showExceptionMessages = true;//Always show errors in debug mode
//#endif

        public bool HasError { get; private set; }
        public ErrorNumber Error { get; private set; }
        public string ErrorMessage { get; private set; }

        public OperationResult(){}

        public OperationResult(OperationResult res) : this(res.Error) { }

        public OperationResult(ErrorNumber errNumber)
        {
            if (errNumber != ErrorNumber.None)
            {
                ErrorMessage = errNumber.ToString();
                Error = errNumber;
                HasError = true;
            }
        }

        public OperationResult(ErrorNumber errNumber, string errorMessage, params object[] args)
        {
            errorMessage = string.Format(errorMessage, args);
            ErrorMessage = errorMessage;
            Error = errNumber;
            HasError = true;
        }

        public OperationResult(Exception exp)
            : this(false, exp, string.Empty)
        {
        }

        public OperationResult(bool showExceptionMessages, Exception exp)
            : this(showExceptionMessages, exp, string.Empty)
        {
        }

        public OperationResult(Exception exp, string errorMessage, params object[] args)
            : this(false, exp, errorMessage, args)
        {
        }
        public OperationResult(bool showExceptionMessages, Exception exp, string errorMessage, params object[] args)
            : this(ErrorNumber.General)
        {
            errorMessage = string.Format(errorMessage, args);
            log.WriteError(exp, errorMessage);
            ErrorMessage = errorMessage;
#if DEBUG
            showExceptionMessages = true;
#endif
            if (showExceptionMessages)
            {
                ErrorMessage += string.IsNullOrEmpty(errorMessage) ? string.Empty : ", Exception:";
                ErrorMessage += (exp.InnerException == null ? exp.Message : exp.InnerException.Message);
            }
        }

        public override string ToString()
        {
            if (HasError)
                return string.Format("Error Number:{0}, Message:{1}", (int)Error, ErrorMessage);
            return base.ToString();
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Value { get; private set; }

        public OperationResult() {}

        public OperationResult(T value)
        {
            Value = value;
        }

        public OperationResult(OperationResult res) : base(res.Error) { }

        public OperationResult(ErrorNumber errNumber) : base(errNumber){}

        public OperationResult(bool showExceptionMessages, Exception exp) : base(showExceptionMessages, exp){}

        public OperationResult(bool showExceptionMessages, Exception exp, string errorMessage, params object[] args)
            : base(showExceptionMessages, exp, errorMessage, args)
        {
        }

        public OperationResult(ErrorNumber errNumber, string errorMessage, params object[] args)
            : base(errNumber, errorMessage, args){ }

        public OperationResult(Exception exp) : base(exp){}

        public OperationResult(Exception exp, string errorMessage, params object[] args) : base(exp, errorMessage, args){}

        public OperationResult(T value, ErrorNumber errNumber)
            : base(errNumber)
        {
            Value = value;
        }

     }

    public class PagesOperationResult<T> : OperationResult<T>
    {
        public long PagesCount { get; set; }
        public long RowsCount { get; set; }
        
        public PagesOperationResult(){}

        public PagesOperationResult(T result) : base(result){ }

        public PagesOperationResult(T result, ErrorNumber errNumber) : base(result, errNumber) { }

        public PagesOperationResult(ErrorNumber errNumber) : base(errNumber) { }

        public PagesOperationResult(ErrorNumber errNumber, string errorMessage) : base(errNumber, errorMessage) { }

        public PagesOperationResult(T result, long pagesCount, long rowsCount)
            : base(result)
        {
            PagesCount = pagesCount;
            RowsCount = rowsCount;
        }
    }
}