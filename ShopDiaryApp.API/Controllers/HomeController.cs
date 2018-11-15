using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace ShopDiaryApp.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //[HttpGet]
        //[Route("Send Message")]
        //public System.Web.Http.IHttpActionResult SendMessage()
        //{
        //    var data = new
        //    {
        //        to = "AIzaSyCWAG02mEtU_s7tUAqCMQN-jyqTSeU46Ao",
        //        data = new
        //        {
        //            Message = "You have items that nearly expired",
        //            name = "test notification",
        //            userId = ""
        //        }
        //    };
        //    SendNotification(data);
        //    return Ok();
        //}
        //public  void SendNotification(object data)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    var json = serializer.Serialize(data);
        //    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //    SendNotification(byteArray);
        //}

        //public void SendNotification(Byte[] byteArray)
        //{
        //    try
        //    {
        //        string server_api_key = ConfigurationManager.AppSettings["SERVER_API_KEY"];
        //        string sender_id = ConfigurationManager.AppSettings["Sender_ID"];

        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        tRequest.Headers.Add($"Authorization: key={server_api_key}");
        //        tRequest.Headers.Add($"Sender: id={sender_id}");

        //        tRequest.ContentLength = byteArray.Length;
        //        Stream dataStream = tRequest.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();

        //        WebResponse tResponse = tRequest.GetResponse();
        //        dataStream = tResponse.GetResponseStream();
        //        StreamReader tReader = new StreamReader(dataStream);

        //        string sResponseFromServer = tReader.ReadToEnd();

        //        tReader.Close();
        //        dataStream.Close();
        //        tResponse.Close();
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}
