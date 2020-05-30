﻿using DIMS.Server.Filters;
using System.Web.Mvc;

namespace DIMS.Server.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionLogger());
        }
    }
}