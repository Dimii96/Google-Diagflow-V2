namespace DiagflowWebhook.Models
{
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
}
