using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeniusAcademy.EF.Respsitory;
using GeniusAcademy.Types;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeniusAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController<Test, EfCoreTestRepository>
    {

        public TestController(EfCoreTestRepository repository) : base(repository)
        {
        }



        //// GET: api/[controller]/GetSeniorCitizens/5
        //[HttpGet("GetSeniorCitizens/{id}")]
        //public Task<List<Test>> GetSeniorCitizens(long id, Test test)
        //{
        //    // string name = test.Name;
        //}
    }
}