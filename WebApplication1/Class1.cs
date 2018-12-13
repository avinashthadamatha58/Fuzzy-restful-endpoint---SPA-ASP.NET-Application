using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApplication1
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum AuthenticationType
    {
        Basic,
        NTLM
    }

    public class RestClient
    {
        public string EndPoint { get; set; }
        public string ImgEndPoint { get; set; }
        public HttpVerb HttpMethod { get; set; }
        public AuthenticationType AuthTyoe { get; set; }
        public string UserName = "anything";
        public string Password = "evalpass";

        public RestClient()
        {
            EndPoint = string.Empty;
            HttpMethod = HttpVerb.GET;
        }

        #region Load all events feom API to a C# object - list<T>
        public List<RootObject> makeRequest()
        {
            List<RootObject> emptyList = new List<RootObject>();
            string strResponseValue = string.Empty;
            HttpWebResponse response = httpResponse(EndPoint);
            if (response == null)
            {
                return emptyList;
            }
            else
            {
                while (1 == 1)
                {
                    try
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (StreamReader reader = new StreamReader(responseStream))
                                {
                                    strResponseValue = reader.ReadToEnd();
                                    List<RootObject> rob = JsonConvert.DeserializeObject<List<RootObject>>(strResponseValue);
                                    return rob;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return emptyList;

        }
        #endregion

        #region Response for all get requests
        private HttpWebResponse httpResponse(string endPoint)
        {
            int countForMakeRequest = 0;
            while (1 == 1)
            {
                countForMakeRequest++;
                if (countForMakeRequest > 10)  //assuming
                {
                    break;
                    //throw new ApplicationException("Error, cannot get response");
                }
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

                    request.Method = HttpMethod.ToString();

                    string authheader = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(UserName + ":" + Password));
                    request.Headers.Add("Authorization", AuthTyoe.ToString() + " " + authheader);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("Error" + response.StatusCode);

                    }
                    else
                    {
                        return response;
                    }

                }
                catch (ApplicationException ae)
                {
                    Console.WriteLine("The status code was not OK", ae.Message);
                }
                catch (Exception)
                {

                }
            }
            HttpWebResponse response1 = null;
            return response1;
        }
        #endregion

        #region getting image from API
        public string getImageFromJson(string eventID, string imgID)
        {
            string iID = imgID;
            if (imgID != null)
            {
                if (imgID.Contains(' '))
                {
                    iID = imgID.Replace(' ', '+');
                }
            }
            int countForImage = 0;
            string strResponseValue = string.Empty;
            string base64 = string.Empty;
            while (1 == 1)
            {
                countForImage++;
                if (countForImage > 15)
                {
                    return string.Empty;
                }
                try
                {

                    HttpWebResponse response = httpResponse("http://dev.dragonflyathletics.com:1337/api/dfkey/events/" + eventID + "/media/" + iID);

                    if (response == null)
                    {
                        return string.Empty;
                    }
                    else
                    {

                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (System.Drawing.Image image = System.Drawing.Image.FromStream(responseStream))
                                {
                                    using (Bitmap bm = new Bitmap(image))
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            bm.Save(ms, ImageFormat.Jpeg);
                                            base64 = Convert.ToBase64String(ms.ToArray());
                                            return base64;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Image could not be created from system.drawing");
                }
                catch (Exception)
                {
                    Console.WriteLine("Stream error"); //vague error declaration
                }
            }
            return string.Empty;
        }
        #endregion

        #region put coming true or false based on check
        public string putStatus(string eventIDforCheck, bool checkedOrNot)
        {
            int checkNoOfPuts = 0;
            while (1 == 1)
            {
                checkNoOfPuts++;
                if (checkNoOfPuts > 15)
                {
                    return "Failed";
                }
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dev.dragonflyathletics.com:1337/api/dfkey/events/" + eventIDforCheck + "/status/" + UserName);

                    request.Method = "PUT";

                    string authheader = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(UserName + ":" + Password));
                    request.Headers.Add("Authorization", AuthTyoe.ToString() + " " + authheader);
                    PutRootObject pro = new PutRootObject();
                    pro.Coming = checkedOrNot;
                    var postD = JsonConvert.SerializeObject(pro);

                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] byte1 = encoding.GetBytes(postD);

                    // Set the content type of the data being posted.
                    request.ContentType = "application/json";

                    // Set the content length of the string being posted.
                    request.ContentLength = byte1.Length;
                    using (var newStream = new StreamWriter(request.GetRequestStream()))
                    {

                        newStream.Write(postD);
                    }
                    bool test = getCheckBoxStatus(eventIDforCheck);
                    if (checkedOrNot == test)
                    {
                        return "Success";
                    }
                    throw new ApplicationException("Put was not successful");
                }
                catch (ApplicationException ar)
                {
                    Console.WriteLine(ar.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error getting images"); //vague handling
                }
            }
            return "Failed";
        }
        #endregion

        #region get status of coming true or not from API
        public bool getCheckBoxStatus(string eventIDforCheckStatus)
        {
            string strResponseValue = string.Empty;
            int checkNoOfGet = 0;
            while (1 == 1)
            {
                checkNoOfGet++;
                if (checkNoOfGet > 10)
                {
                    return false;       //assuming every event has true or false for coming, otherwise it returns as false default.
                }
                try
                {
                    HttpWebResponse response = httpResponse("http://dev.dragonflyathletics.com:1337/api/dfkey/events/" + eventIDforCheckStatus + "/status/" + UserName);
                    if (response == null)
                    {
                        throw new ApplicationException("Response was null");
                    }
                    else
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (StreamReader reader = new StreamReader(responseStream))
                                {
                                    strResponseValue = reader.ReadToEnd();
                                    PutRootObject robb = JsonConvert.DeserializeObject<PutRootObject>(strResponseValue);
                                    return robb.Coming;
                                    // return strResponseValue;
                                }
                            }
                        }
                    }
                }
                catch (ApplicationException ar)
                {
                    Console.WriteLine(ar.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error with the stream"); //vague handling
                }
            }
        }
        #endregion
    }

}
