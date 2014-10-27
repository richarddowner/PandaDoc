 # Webhook Test Server
 
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