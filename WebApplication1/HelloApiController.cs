using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication1
{
    public class HelloApiController : ApiController
    {
        #region Get requests
        [Route("api/HelloApi/{paramOne}/{paramTwo}")]
        public string Get([FromUri]string paramOne, string paramTwo)
        {
            string str = string.Empty;
             RestClient rc = new RestClient();
            str = rc.getImageFromJson(paramOne, paramTwo);
            str += "," + paramOne + "," + paramTwo;
            return str;
        }

        public bool Get([FromUri]string foo)
        {
            bool boo;
            RestClient rc = new RestClient();
            boo = rc.getCheckBoxStatus(foo);
            return boo;
        }
        public string Get([FromUri]string foo, string foo2, string foo3)
        {
            bool boo;
            RestClient rc = new RestClient();
            boo = rc.getCheckBoxStatus(foo);
            string ar = foo2 + "," + boo;
            return ar;
        }
        #endregion

        #region put request
        public string Put([FromBody]pRootObject foo)
        {
            string eventid = foo.eventid;
            string checkboxid = foo.checkboxid;
            bool boo = foo.checkboxstatus;
            RestClient rc = new RestClient();
           string ret= rc.putStatus(eventid, boo);
              return foo.checkboxid;
                    }
    }
    public class pRootObject
    {
        public string eventid { get; set; }
        public string checkboxid { get; set; }
        public bool checkboxstatus { get; set; }
    }
    #endregion
}