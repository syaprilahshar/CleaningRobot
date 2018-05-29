using CleaningRobot.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CleaningRobot.WebAPI.Controllers
{
    //[Authorize]
    public class DataController : ApiController
    {
        // GET api/data/cleaned
        [Route("cleaned")]
        [HttpGet]
        public OutputModel Cleaned()
        {
            OutputModel output = new OutputModel();
            bool cleaned = true;
            try
            {
                if (cleaned)
                {
                    output.status = "success";
                    output.data = "A, B, C";
                    output.message = "get data cleaned";
                }
                else
                {
                    output.status = "failed";
                    output.message = "failed get data cleaned";
                }
            }
            catch (Exception ex)
            {
                output.status = "error";
                output.message = ex.ToString();
            }
            return output;
        }

        // POST api/data/request/clean
        [Route("request/clean")]
        [HttpPost]
        public OutputModel RequestClean([FromBody] RequestModel request)
        {
            OutputModel output = new OutputModel();
            try
            {
                if (request == null)
                {
                    output.status = "failed";
                    output.message = "no request data found";
                }
                else
                {
                    ResultModel result = new ResultModel();
                    //run robot
                    output.status = "success";
                    output.data = result.Cleaned;
                    output.message = "success clean data";
                }
            }
            catch (Exception ex)
            {
                output.status = "error";
                output.message = ex.ToString();
            }
            return output;
        }

        // GET api/data/request/clean/id
        [Route("request/clean/{id}")]
        [HttpGet]
        public OutputModel CleanById(int id)
        {
            OutputModel output = new OutputModel();
            try
            {
                if (id == 1)
                {
                    output.status = "failed";
                    output.message = "id " + id + " + already cleaned";
                }
                else
                {
                    output.status = "success";
                    output.data = id;
                    output.message = "success clean id " + id;
                }
            }
            catch (Exception ex)
            {
                output.status = "error";
                output.message = ex.ToString();
            }
            return output;
        }
    }
}
