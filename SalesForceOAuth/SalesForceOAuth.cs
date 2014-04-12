using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace SalesForceOAuth
{
    // Paul D. Short (2014).
    // SalesForceOAuth.dll is a .Net library for handling an OAuth Signed Request from SalesForce Canvas.
    //
    // This code is based on the following resources:
    //
    // 1. SassPie Blogs - Exploring Force.com Canvas with ASP.NET
    // http://www.saaspie.com/2014/02/18/exploring-force-com-canvas-asp-net/
    //
    // 2. Suyati Technologies - Seamless Integration of .Net with Force.com Canvas Webinar Video: 
    // https://www.youtube.com/watch?v=asx2zfOcsPM
    //
    // 3. forcedotcom/SalesforceCanvasFrameworkSDK
    // https://github.com/forcedotcom/SalesforceCanvasFrameworkSDK/blob/master/src/main/java/canvas/SignedRequest.java

    public class SignedAuthentication
    {
        private readonly JavaScriptSerializer _jsSerializer;

        /// <summary>
        /// Immediately after your object is instantiated, check this property to see if the signed 
        /// request request is authentic. If it's false then the request could be a forgery.
        /// </summary>
        public bool IsAuthenticatedCanvasUser { get; private set; }

        /// <summary>Use CanvasContextObject from your codebehind/server-side ASP.Net code.</summary>
        public RootObject CanvasContextObject { get; private set; }

        /// <summary>
        /// Use CanvasContextJson for your client-side JavaScript code. This is the JSON serialization of
        /// the CanvasContextObject property (type:RootObject). For example, you can stash it in a 
        /// hidden field and later deserialize in document.ready: var sr = JSON.parse($('#hiddenfield').val());
        /// </summary>
        public string CanvasContextJson { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consumerSecret">Our app and SalesForce both know the consumer secret.</param>
        /// <param name="encryptedSignedRequest">This request comes from SalesForce. We verify it using 
        /// our consumer secret.</param>
        public SignedAuthentication(string consumerSecret, string encryptedSignedRequest)
        {
            _jsSerializer = new JavaScriptSerializer();
            CanvasContextObject = GetRootObject(consumerSecret, encryptedSignedRequest);
            CanvasContextJson = _jsSerializer.Serialize(CanvasContextObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consumerSecret"></param>
        /// <param name="signedRequest"></param>
        /// <returns>The RootObject is what we pass via JSON to the Salesforce Canvas client (JavaScript)</returns>
        private RootObject GetRootObject(string consumerSecret, string signedRequest)
        {
            /* signedRequest is a string of the following concatenated elements :
                   1. The canvas app consumer secret encrypted with HMAC SHA–256 algorithm
                   2. A period (".")
                   3. The context and authorization token JSON encoded in Base64
               It looks similar to this, although it will be much longer:
               9Rpl6rE7R2bSNjoSfYdERk8nffmgtKQNhr5U/5eSJPI=.eyJjb250ZXh0Ijp7InVzZXIiOnsibGFuZ3V 
            */
            string[] signedRequestLst = signedRequest.Split('.');
            string encodedSignature = signedRequestLst[0];
            string contextPlusToken = signedRequestLst[1];

            IsAuthenticatedCanvasUser = isSignedRequestValid(consumerSecret, encodedSignature, contextPlusToken);

            byte[] contextPlusTokenBytes = Convert.FromBase64String(contextPlusToken);
            var root = _jsSerializer.Deserialize<RootObject>(Encoding.UTF8.GetString(contextPlusTokenBytes));
            return root;
        }

        private bool isSignedRequestValid(string consumerSecret, string encodedSignature, string contextPlusToken)
        {
            /* To ensure that the signed request is valid, we must verify that it was signed using our 
               specific canvas app's consumer secret. If the correct consumer secret was used, then we 
               can trust the context; otherwise, we will assume that the request was NOT initiated by 
               Salesforce. To verify and decode the signed request we :
                   1. Receive the POST message that contains the initial signed request from Salesforce.
                   2. Split the signed request on the first period. The result is two strings: the hashed 
                      Based64 context signed with the consumer secret and the Base64 encoded context itself.
             
                      We pass in the consumer secret and the two strings above to this isSignedRequestValid 
                      method, which does the following:
             
                   3. Use the HMAC SHA-256 algorithm to hash the Base64 encoded context and 
                      sign it using our consumer secret.
                   4. Base64 encode the string created in the previous step.
                   5. Compare the Base64 encoded string with the hashed Base64 context signed with 
                      the consumer secret we received in step 2. 
            */
            byte[] decodedSig = Convert.FromBase64String(encodedSignature);

            var encoding = new ASCIIEncoding();
            var hmacSha256 = new HMACSHA256(encoding.GetBytes(consumerSecret));
            byte[] hashMessage = hmacSha256.ComputeHash(encoding.GetBytes(contextPlusToken));

            return decodedSig.SequenceEqual(hashMessage); // true => we can trust; false otherwise
        }
    }
}

