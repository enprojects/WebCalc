using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCalc.Interfaces;
using WebCalc.Models;


namespace WebCalc.Controllers
{

    [Route("api")]
    public class ApiController : Controller
    {

        private readonly ICalculateService _serv;

        public ApiController(ICalculateService serv)
        {
            _serv = serv;
        }
        

        [HttpPost("calculate")]
        public IActionResult CalcResult([FromBody]CalculatorRequest request)
        {
            var result = _serv.Evaluate(request.Expression);

            return Json(new
            {
                result = result != null ?  result.ToString() : "Invalid expression",
            });
        }
    }
}
