using DLL.Context;
using DLL.Models;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApiTest.Controllers
{
    public class ValuesController : ApiController
    {
        IRepository<Interval> intervalRepo = new IntervalRepository();
        IRepository<LogInterval> logRepo = new LogRepository();

        // GET 
        [Route("api/values")]
        public IEnumerable<Interval> Get()
        {
            return intervalRepo.GetAll();
        }
        // GET
        [ActionName("SelLog")]
        [Route("api/values/log")]
        public IEnumerable<LogInterval> GetLog()
        {
            return logRepo.GetAll();
        }

        // GET 
        [ActionName("Select")]
        public IEnumerable<Interval> Get(string date1, string date2)
        {
            Interval interval = new Interval();

            interval.BeginDate = Convert.ToDateTime(date1);
            interval.EndDate = Convert.ToDateTime(date2);
            var value = intervalRepo.GetAll().Where(x => (interval.BeginDate >= x.BeginDate && interval.BeginDate <= x.EndDate) ||
                    (interval.EndDate >= x.BeginDate && interval.EndDate <= x.EndDate)).Distinct();
            
            LogInterval log = new LogInterval(DateTime.Now, "SELECT RESULT", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
            return value;  
        }

        // GET 
        [Route("api/values/{id}")]
        public Interval Get(int id)
        {
            Interval interval = intervalRepo.Get(id);
            if (interval != null)
                return interval;
            else
                return interval;
        }

        // POST
        [HttpPost]
        [Route("api/values")]
        public void Post([FromBody]Interval val)
        {
            intervalRepo.Add(val);
            intervalRepo.Save();
            LogInterval log = new LogInterval(DateTime.Now, "CREATE", val.ID);
            log.BeginDate = val.BeginDate;
            log.EndDate = val.EndDate;
            logRepo.Add(log);
            logRepo.Save();
        }

        // PUT
        [HttpPut]
        [Route("api/values/{id}")]
        public void Put(int id, [FromBody]Interval val)
        {
            intervalRepo.Update(val);
            LogInterval log = new LogInterval(DateTime.Now, "EDIT", val.ID);
            log.BeginDate = val.BeginDate;
            log.EndDate = val.EndDate;
            logRepo.Add(log);
            logRepo.Save();
        }

        // DELETE
        [HttpDelete]
        [Route("api/values/{id}")]
        public void Delete(int id)
        {
            Interval interval = intervalRepo.Get(id); 
            intervalRepo.Remove(interval);
            LogInterval log = new LogInterval(DateTime.Now, "DELETE", interval.ID);
            log.BeginDate = interval.BeginDate;
            log.EndDate = interval.EndDate;
            logRepo.Add(log);
            logRepo.Save();
        }
    }
}
