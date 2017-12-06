using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReportIscrittiService
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebsitesController : ApiController
    {
        Website[] websites = new Website[]
        {
            new Website { Id = 1, Name = "BirdMMO.com", Description = "Bird Flapping Game For Everybody"},
            new Website { Id = 2, Name = "SpiderMMO.com", Description = "Spider Versus Spider Death Match"},
            new Website { Id = 3, Name = "LiveAutoWheel.com", Description = "Random Number Generator"},
            new Website { Id = 4, Name = "SeanWasEre.com", Description = "A Blog of Trivial Things"}
        };

        // GET api/Websites 
        public IEnumerable Get()
        {
            return websites;
        }

        // GET api/Websites/5 
        public Website Get(int id)
        {
            try
            {
                return websites[id];
            }
            catch (Exception e)
            {
                return new Website();
            }
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
            Console.WriteLine("Post method called with value = " + value);
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
            Console.WriteLine("Put method called with value = " + value);
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
            Console.WriteLine("Delete method called with id = " + id);
        }
    }
}