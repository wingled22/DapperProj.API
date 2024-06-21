using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DapperProj.API.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DapperProj.API.Controllers
{
    [Route("api/[controller]")]
    public class SampleExceptionController : Controller
    {
        private readonly ExceptionsSampleLogic _exceptionsSampleLogic;

        public SampleExceptionController(ExceptionsSampleLogic exceptionsSampleLogic)
        {
            _exceptionsSampleLogic = exceptionsSampleLogic;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            _exceptionsSampleLogic.ReturnException(null!);
            return View();
        }


    }

}