using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using WeiXinBiz.DataBase;
using WeiXinBiz.Models;

namespace WeiXinServer.Controllers
{
    public class UserController : ApiController
    {



        public IList<User> Get(string paramsString)
        {
            var userFilter = JsonConvert.DeserializeObject<User>(paramsString);
            var db = new DbUser();
            return db.Query(userFilter);



        }
    }
}
