namespace Kesco.Web.Mvc.UI.Grid
{
    using System;

    internal class DataTypeNotSetException : Exception
    {
        public DataTypeNotSetException(string message) : base(message)
        {
        }
    }
}
