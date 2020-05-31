using DIMS.Logger;
using System.Web.Mvc;

namespace DIMS.Server.Filters
{
    public class ExceptionLogger : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception == null)
            {
                return;
            }

            CustomLogger.Error(exceptionContext.Exception);
        }
    }
}