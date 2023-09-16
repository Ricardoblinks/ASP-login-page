using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Xml;
using System;
using System.Net.Http;
using System.Xml.Linq;
using System.Text;

namespace Full_Stack_.NET_Developer.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult login(string email, string password)
        {
            HttpWebRequest request = CreateWebRequest();

            XmlDocument soapEnvelopeXml = new XmlDocument();

            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""UTF-8""?>
            <env:Envelope xmlns:env=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns1=""urn:ICUTech.Intf-IICUTech"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
            xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:enc=""http://www.w3.org/2003/05/soap-encoding""><env:Body>
            <ns1:Login env:encodingStyle=""http://www.w3.org/2003/05/soap-encoding""><UserName xsi:type=""xsd:string"">" + email + 
            @"</UserName><Password xsi:type=""xsd:string"">" + password + 
            @"</Password><IPs xsi:type=""xsd:string""></IPs></ns1:Login></env:Body></env:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd().Replace(",", "," + System.Environment.NewLine);
                    ViewBag.Message = System.Web.HttpUtility.JavaScriptStringEncode(soapResult);
                }
            }

            return View("~/Views/Home/Index.cshtml");
        }
        public static HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://isapi.icu-tech.com/icutech-test.dll/soap/IICUTech");
            webRequest.Headers.Add(@"SOAP:Login");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "Post";
            return webRequest;
        }
    }

}
