using System;
using System.Configuration;
using System.Web;

namespace CanvasMvcHelloWorld.Models
{
    public class HelloWorldModel
    {
        public HelloWorldModel(string encodedSignedRequest)
        {
            Greeting = "Hello, World! This is a simple MVC application that accepts a SalesForce Canvas Signed Request.";
            if (String.IsNullOrEmpty(encodedSignedRequest))
            {
                SignedRequestStatus = "Did not find 'signed_request' POSTed in the HttpRequest. Either we are not being called by a SalesForce Canvas, or its associated Connected App isn't configured properly.";
                return; // failed because we are not in canvas, so exit early
            }

            // Validate the signed request using the consumer secret
            string secret = GetConsumerSecret();
            var auth = new SalesForceOAuth.SignedAuthentication(secret, encodedSignedRequest);
            if (!auth.IsAuthenticatedCanvasUser)
            {
                SignedRequestStatus = "SECURITY ALERT: We received a signed request, but it did not match our consumer secret. We should treat this as a forgery and stop processing the request.";
                return; // failed because the request is either a forgery or the connected app doesn't match our consumer secret               
            }

            SignedRequestStatus = String.Format("SUCCESS! Here is the signed request decoded as JSON:\n{0}", auth.CanvasContextJson);
        }

        public string Greeting { get; private set; }
        public string SignedRequestStatus { get; private set; }

        private string GetConsumerSecret()
        {
            // Since the consumer secret shouldn't change often, we'll put it in the Application Cache. You may want cache it differently in a production application.
            string cachedConsumerSecret = (HttpContext.Current.Application["ConsumerSecret"] ?? String.Empty).ToString();
            if (!String.IsNullOrEmpty(cachedConsumerSecret))
            {
                return cachedConsumerSecret;
            }

            // We use key names in the format "cs-key:<server><app-path>" so that we can maintain a consumer secret per server + app instance
            string key = String.Format("cs-key:{0}:{1}{2}", 
                HttpContext.Current.Request.ServerVariables["SERVER_NAME"], 
                HttpContext.Current.Request.Url.Port, 
                HttpContext.Current.Request.ApplicationPath);

            string secret = ConfigurationManager.AppSettings[key];
            if (!String.IsNullOrEmpty(secret))
            {
                HttpContext.Current.Application["ConsumerSecret"] = secret;
            }
            return secret;
        }
    }
}