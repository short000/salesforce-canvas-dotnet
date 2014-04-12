using System.Collections.Generic;

// ReSharper disable InconsistentNaming
// Instead of using standard .Net PascalCase, we use camelCase to match the Apex names from SalesForce.
namespace SalesForceOAuth
{
    public class RootObject
    {
        public string algorithm { get; set; }
        public int issuedAt { get; set; }
        public string userId { get; set; }
        public Client client { get; set; }
        public Context context { get; set; }

        public class Client
        {
            public string instanceId { get; set; }
            public string targetOrigin { get; set; }
            public string instanceUrl { get; set; }
            public string oauthToken { get; set; }
        }
        public class Context
        {
            public User user { get; set; }
            public Links links { get; set; }
            public Application application { get; set; }
            public Organization organization { get; set; }
            public Environment environment { get; set; }
            public class User
            {
                public string language { get; set; }
                public string profilePhotoUrl { get; set; }
                public string userId { get; set; }
                public bool isDefaultNetwork { get; set; }
                public string userType { get; set; }
                public string profileId { get; set; }
                public string email { get; set; }
                public object networkId { get; set; }
                public object siteUrl { get; set; }
                public string timeZone { get; set; }
                public string userName { get; set; }
                public string locale { get; set; }
                public string fullName { get; set; }
                public bool accessibilityModeEnabled { get; set; }
                public string profileThumbnailUrl { get; set; }
                public object roleId { get; set; }
                public object siteUrlPrefix { get; set; }
                public string currencyISOCode { get; set; }
                public string firstName { get; set; }
                public string lastName { get; set; }
            }
            public class Links
            {
                public string loginUrl { get; set; }
                public string enterpriseUrl { get; set; }
                public string metadataUrl { get; set; }
                public string partnerUrl { get; set; }
                public string restUrl { get; set; }
                public string sobjectUrl { get; set; }
                public string searchUrl { get; set; }
                public string queryUrl { get; set; }
                public string recentItemsUrl { get; set; }
                public string chatterFeedsUrl { get; set; }
                public string chatterGroupsUrl { get; set; }
                public string chatterUsersUrl { get; set; }
                public string chatterFeedItemsUrl { get; set; }
                public string userUrl { get; set; }
            }
            public class Application
            {
                public string @namespace { get; set; }
                public string name { get; set; }
                public string canvasUrl { get; set; }
                public string applicationId { get; set; }
                public string version { get; set; }
                public string authType { get; set; }
                public string referenceId { get; set; }
                public List<string> options { get; set; }
                public string developerName { get; set; }
            }
            public class Organization
            {
                public string organizationId { get; set; }
                public string name { get; set; }
                public bool multicurrencyEnabled { get; set; }
                public string namespacePrefix { get; set; }
                public string currencyIsoCode { get; set; }
            }
            public class Environment
            {
                public string locationUrl { get; set; }
                public object displayLocation { get; set; }
                public string uiTheme { get; set; }
                public Dimensions dimensions { get; set; }
                public Dictionary<string, string> parameters { get; set; }
                public Version version { get; set; }
                public class Dimensions
                {
                    public string width { get; set; }
                    public string maxHeight { get; set; }
                    public string maxWidth { get; set; }
                    public string height { get; set; }
                    public string clientWidth { get; set; }
                    public string clientHeight { get; set; }
                }
                public class Version
                {
                    public string season { get; set; }
                    public string api { get; set; }
                }
            }
        }
    }
}

// ReSharper restore InconsistentNaming

/* Example RootObject serialized into JSON (formatted alphabetically):

{
    "algorithm": "HMACSHA256",
    "client": {
        "instanceId": "_:LOCAL_myDemo:capp1",
        "instanceUrl": "https://fake-org.cs11.my.salesforce.com",
        "oauthToken": "11DZ111111N9IbI!ARsAQHz1ZappGcrY9CuJ4OQGlfe9g.21Q3xYmG4ow12IFN1lwovqEKvyA8d16l3QHmuUUUZqc.yLJ3IZXVPLxqi6gfmuLOU.",
        "targetOrigin": "https://fake-org-c.cs11.visual.force.com"
    },
    "context": {
        "application": {
            "applicationId": "06PZ0111110CcWP",
            "authType": "SIGNED_REQUEST",
            "canvasUrl": "https://localhost:44301/myCanvasDemo.aspx",
            "developerName": "LOCAL_myDemo",
            "name": "LOCAL_myDemo",
            "namespace": null,
            "options": [],
            "referenceId": "09HW111110001iY",
            "version": "1.0"
        },
        "environment": {
            "dimensions": {
                "clientHeight": "30px",
                "clientWidth": "923px",
                "height": "600px",
                "maxHeight": "2000px",
                "maxWidth": "1000px",
                "width": "100%"
            },
            "displayLocation": "Visualforce",
            "locationUrl": "https://fake-org-c.cs11.visual.force.com/apex/myDemo_LOCAL?core.apexpages.devmode.url=1",
            "parameters": {
                "debug": "true"
            },
            "uiTheme": "Theme3",
            "version": {
                "api": "30.0",
                "season": "SPRING"
            }
        },
        "links": {
            "chatterFeedItemsUrl": "/services/data/v30.0/chatter/feed-items",
            "chatterFeedsUrl": "/services/data/v30.0/chatter/feeds",
            "chatterGroupsUrl": "/services/data/v30.0/chatter/groups",
            "chatterUsersUrl": "/services/data/v30.0/chatter/users",
            "enterpriseUrl": "/services/Soap/c/30.0/00DZ000000N9IbI",
            "loginUrl": "https://fake-org.cs11.my.salesforce.com",
            "metadataUrl": "/services/Soap/m/30.0/00DZ000000N9IbI",
            "partnerUrl": "/services/Soap/u/30.0/00DZ000000N9IbI",
            "queryUrl": "/services/data/v30.0/query/",
            "recentItemsUrl": "/services/data/v30.0/recent/",
            "restUrl": "/services/data/v30.0/",
            "searchUrl": "/services/data/v30.0/search/",
            "sobjectUrl": "/services/data/v30.0/sobjects/",
            "userUrl": "/014Z1010001dtAWQAQ"
        },
        "organization": {
            "currencyIsoCode": "USD",
            "multicurrencyEnabled": true,
            "name": "Your Company or Org Name",
            "namespacePrefix": null,
            "organizationId": "11DZ100100N92BIMAV"
        },
        "user": {
            "accessibilityModeEnabled": false,
            "currencyISOCode": "USD",
            "email": "fake.name@foo.com",
            "firstName": "Fake",
            "fullName": "Fake Name",
            "isDefaultNetwork": true,
            "language": "en_US",
            "lastName": "Name",
            "locale": "en_US",
            "networkId": null,
            "profileId": "00ed0000000dmuj",
            "profilePhotoUrl": "https://fake-org-c.cs11.content.force.com/profilephoto/005/F",
            "profileThumbnailUrl": "https://fake-org-c.cs11.content.force.com/profilephoto/005/T",
            "roleId": "00EG0000000RJ9O",
            "siteUrl": null,
            "siteUrlPrefix": null,
            "timeZone": "America/Chicago",
            "userId": "014Z1010001dtAWQAQ",
            "userName": "fake.name@foo.com.fake.2",
            "userType": "STANDARD"
        }
    },
    "issuedAt": 594787204,
    "userId": "006Z0001001ctaW"
}

*/