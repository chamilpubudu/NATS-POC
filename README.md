# NATS POC Application

## Getting Started

Make sure you have [installed](https://docs.docker.com/docker-for-windows/install/) docker in your environment. After that, you can run the below commands from the **/src/** directory to start **2** `Chat Clients`, `NATS server` and `Redis` immediately.

```powershell
cd src
docker-compose build
docker-compose up
```

>Ensure Port **4222** **6379** **5001** **5002** are not in use before start the application.

&nbsp;

You should be able to browse **2** `Chat Clients` by below URLs:

 `client1`  :  [http://localhost:5001/swagger/index.html](http://localhost:5001/swagger/index.html)

 `client2`  :  [http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html)

&nbsp;

JSON Post Body to send chat messages:
```json
{
  "receiverId": "client1",
  "content": "Hello!"
}
```

&nbsp;

You can modify default Environment Variables in **/src/docker-compose.override.yml**.

```yaml
 - NatsConnectionString=nats://nats-server:4222
 - RedisConnectionString=redis:6379
 - Username=client1
````

&nbsp;

***Tested on**: Windows, MacOS*

&nbsp;

### External Packages:
* Swashbuckle
* Serilog
* MediatR
* NATS.Client