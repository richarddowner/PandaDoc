 # PandaDoc .NET

PandaDoc is a web and mobile application which helps you to send, sign and track documents online. You can learn more about the application at our web site (https://www.pandadoc.com/).

With PandaDoc's programmable API you can send a document for signing straight from your backend system, or embed a document in your web site to request a signature from your clients.

## Basic usage

The best way to learn the API is to have a read of the [tests](https://github.com/superlogical/PandaDoc/blob/master/PandaDoc.Tests/PandaDocHttpClientTests.cs)

Once you are familiar with the API you can install PandaDoc using nuget:

````
Install-Package PanadaDoc
````

## Login

````csharp
var settings = new PandaDocHttpClientSettings();
var client = new PandaDocHttpClient(settings);

var login = await client.Login(username: Username, password: Password);
client.SetBearerToken(login.Value);
````

### Get Documents
````csharp
PandaDocHttpResponse<GetDocumentsResponse> response = await client.GetDocuments();
````

### Create a Document
````csharp
var request = new CreateDocumentRequest
{
    Name = "Sample Document",
    Url = SampleDocUrl,
    Recipients = new[]
    {
        new Models.CreateDocument.Recipient
        {
            Email = "jake.net@gmail.com",
            FirstName = "Jake",
            LastName = "Scott",
            Role = "u1",
        }
    },
    Fields = new Dictionary<string, Field>
    {
        {"optId", new Field {Title = "Field 1"}}
    }
};

PandaDocHttpResponse<CreateDocumentResponse> response = await client.CreateDocument(request);
````

### Get a Document
````csharp
PandaDocHttpResponse<GetDocumentResponse> response = await client.GetDocument(uuid);
````

#### Send a Document
````csharp
var sendRequest = new SendDocumentRequest
{
    Message = "Please sign this document"
};

PandaDocHttpResponse<SendDocumentResponse> response = await client.SendDocument(createResponse.Value.Uuid, sendRequest);
````

## Webhook Test Server
 
In an elevated command prompt run the following command:

````
netsh http add urlacl url=http://127.0.0.1:9000/ user=everyone
````

Signup to https://ngrok.com and download ngrok

Then in another command prompt run ngrok:

````
C:\Users\Jake\Downloads\ngrok>ngrok -authtoken <YOUR_AUTH_TOKEN> 9000
````

It should say something like this:

````
ngrok

Tunnel Status                 online
Version                       1.7/1.6
Forwarding                    http://3730a8f6.ngrok.com -> 127.0.0.1:9000
Forwarding                    https://3730a8f6.ngrok.com -> 127.0.0.1:9000
Web Interface                 127.0.0.1:4040
# Conn                        0
Avg Conn Time                 0.00ms
````

Now run the PandaDoc.WebhookTests web server:
````
C:\dev\PandaDoc\PandaDoc.WebhookTests\bin\Debug\PandaDoc.WebhookTests.exe
````

Then using a tool like POSTMAN send a some Http POST's to ngrok:

````
POST HTTP/1.1
Host: 3730a8f6.ngrok.com
Content-Type: application/json
Cache-Control: no-cache

[ { "type": "document.completed", "data": { "id": "DOCUMENT_UUID" }, "triggered_at": "2014-06-15T10:00:00.000Z" }, { "type": "document.error", "data": { "id": "DOCUMENT_UUID" }, "triggered_at": "2014-06-15T10:00:00.000Z" } ]
````

Open up the ngrok local web interface http://localhost:4040/ and you should see some successful inbound requests!

In the PandaDoc.WebhookTests console window you should see the following:

````
Listening on http://127.0.0.1:9000/
Notification: DOCUMENT_UUID
Notification: DOCUMENT_UUID
Notification: DOCUMENT_UUID
Notification: DOCUMENT_UUID
````