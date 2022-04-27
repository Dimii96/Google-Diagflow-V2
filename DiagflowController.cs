using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
 
 
namespace DiagflowWebhook.Controllers
{
    public class DiagflowController : Controller
    {
        private MyDatabaseDbContext db = new MyDatabaseDbContext();
        JavaScriptSerializer ser = new JavaScriptSerializer();
 
        /* GET: Values */
        [WebMethod]
        //[HttpGet]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ActionResult Index(GoogleRequest result)
        {
            try
            {
                // Grab intent and run function relative to intent to send back data to Google
                var intent = result.queryResult.intent.displayName;
 
                switch (intent)
                {
                    case "IntentNameHere":
                        return IntentNameHere(result); // function name doesnt have to be same as Intent name
                    default:
                        // Diagflow should throw an error before even reaching this, but it it breaks somehow, run Fallback to say error found
                        return Fallback(result); 
                }
            }
            catch (Exception e)
            {
                // Return a fallback if there is an exception
                return Fallback(result); 
            }
        }
        
        // Intents - Different function for each intent
 
        public ActionResult IntentNameHere(GoogleRequest result)
        {
            var speech = "This string here is what will be said by Google back to the user";
 
            WebhookResponse jsonData = new WebhookResponse
            {
                fulfillmentText = speech, // This is what is said back to the user
                source = "Diagflow API",
            };
 
            string str = ser.Serialize(jsonData);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
 
        public ActionResult Fallback(GoogleRequest result)
        {
            var speech = "Sorry, I had an issue with completing your request. Please try again later.";
 
            WebhookResponse jsonData = new WebhookResponse
            {
                fulfillmentText = speech, // This is what is said back to the user
                source = "DialogFlow API",
            };
 
            string str = ser.Serialize(jsonData);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
 
 
        // Request - What we get from Diagflow
        public class GoogleRequest
        {
            public string responseId { get; set; }
            public string session { get; set; }
            public QueryResult queryResult { get; set; }
            public OriginalDetectInentRequest originalDetectIntentRequest { get; set; }
             
        }
 
        public class QueryResult
        {
            public string queryText { get; set; }
            public Parameters parameters { get; set; }
            public bool allRequiredParamsPresent { get; set; }
            public string fulfillmentText { get; set; }
            public FulFillmentMessages fulfullmentMessages { get; set; }
            public Contexts outputContexts { get; set;}
            public Intent intent { get; set; }
            public int intentDetection { get; set; }
            public DiagnosticInfo diagnosticInfo { get; set; }
            public string languageCode { get; set; }
        }
 
        // Paramaters userd in Dialogflow
        public class Parameters
        {
            public string eventName { get; set; }
            public DateTime dateTime { get; set; }
            public DateTime time { get; set;  }
            public DateTime date { get; set; }
        }
        
        public class FulFillmentMessages { }
        public class Contexts { }
        public class DiagnosticInfo { }
        public class Intent
        {
            public string name { get; set; }
            public string displayName { get; set; }
        }
        public class OriginalDetectInentRequest { }
 
 
        // Response - What we send to Diagflow
        public class WebhookResponse
        {
            public string fulfillmentText { get; set; }
            public Message fulfillmentMessages { get; set; } 
            public string source { get; set; }
            public Context outputContexts { get; set; }
            public EventInput followupEventInput { get; set; }
        }
 
        public class Message { }
        public class Context { }
        public class EventInput
        {
            public string followUp { get; set; }
        } 
    } // END values controller
} // END namespace
