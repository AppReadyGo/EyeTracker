using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization;



namespace EyeTracker.Common
{
    public class OperationResult
    {
        public bool WasError { get; private set; }
        public ErrorNumber Error { get; private set; }
        public string ErrorMessage { get; private set; }

        public OperationResult()
        {
        }

        public OperationResult(ErrorNumber errNumber)
        {
            if (errNumber != ErrorNumber.None)
            {
                ErrorMessage = errNumber.ToString();
                Error = errNumber;
                WasError = true;
            }
        }

        public OperationResult(ErrorNumber errNumber, string errorMessage)
        {
            ErrorMessage = errorMessage;
            Error = errNumber;
            WasError = true;
        }

        public override string ToString()
        {
            if (WasError)
                return string.Format("Error Number:{0}, Message:{1}", (int)Error, ErrorMessage);
            return base.ToString();
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Value { get; private set; }

        public OperationResult()
        {
        }

        public OperationResult(T value)
        {
            Value = value;
        }

        public OperationResult(ErrorNumber errNumber)
            : base(errNumber)
        {
        }

        public OperationResult(ErrorNumber errNumber, string errorMessage)
            : base(errNumber, errorMessage)
        {
        }

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