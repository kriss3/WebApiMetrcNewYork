# WebApiMetrcNewYork

WebApiMetrcNewYork/  
├─ Controllers/  
│  └─ MetrcOpsController.cs
├─ Models/  
│  ├─ MetrcOptions.cs  
│  └─ ApiEnvelope.cs  
├─ Services/  
│  ├─ IMetrcDeliveriesService.cs  
│  └─ MetrcDeliveriesService.cs  
├─ Client/  
│  ├─ IMetrcHttp.cs  
│  ├─ MetrcHttp.cs  
│  ├─ MetrcAuthHandler.cs  
│  └─ MetrcUrls.cs  
├─ Program.cs  
└─ appsettings.json  


---

### 🔑 Notes

- **Controllers** → thin, delegate all work to services.  
- **Services** → call through the shared HTTP client; handle no auth logic.  
- **Client** → handles authentication, URL composition, and HTTP reuse.  
- **Models** → contain configuration models (`MetrcOptions`) and consistent API responses (`ApiEnvelope`).  
- **User Secrets** → store sensitive keys:
  ```bash
  dotnet user-secrets set "Metrc:VendorApiKey" "<your vendor key>"
  dotnet user-secrets set "Metrc:UserApiKey" "<your user key>"

### 🔑 The Flow
Request Flow: Controller → Service → HttpClient → DelegatingHandler → Metrc API

┌────────────────────┐
│  Controller                        │
│  (MetrcOpsController)        │       
└────────┬───────────┘
         │ calls _svc.PostDeliveriesAsync(...)
         ▼
┌────────────────────┐
│  Service                            │
│  (MetrcDeliveriesService)   │
└────────┬───────────┘
         │ builds absolute URL via MetrcUrls
         │ calls _http.PostAsync(url, body)
         ▼
┌────────────────────┐
│  Generic Client                  │
│  (MetrcHttp)                     │
└────────┬───────────┘
         │
         │ uses IHttpClientFactory.CreateClient("MetrcNY")
         ▼
┌────────────────────┐
│  HttpClient                        |
│  (Named "MetrcNY")          │
│  configured in DI               │
└────────┬───────────┘
         │
         │ (before network)
         ▼
┌────────────────────┐
│  MetrcAuthHandler  │  
│  (inherits built-in)│
│  • Adds Authorization header
│  • Sets Accept header
│  • Calls base.SendAsync()
└────────┬───────────┘
         │
         ▼
┌────────────────────┐
│  HttpClientHandler │   
│  (lowest level)    │
│  • Opens TCP socket
│  • Sends HTTP to https://api-ny.metrc.com
│  • Receives HTTP response
└────────┬───────────┘
         │
         ▼
┌────────────────────┐
│  Metrc API Server              │          
│  (api-ny.metrc.com)           │
│  Authenticates keys           │
│  Executes request             │
│  Returns response             │
└────────────────────┘

### Response

Success/Failure:
```
{
  "status": "success",        // "success" | "failure"
  "httpCode": 200,            // Metrc HTTP status
  "message": "",              // error text on failure; empty on success
  "data": { /* Metrc JSON */ }, // object or array; null on failure
  "receivedAt": "2025-11-01T01:23:45Z" // UTC timestamp from our API
}
```  

### Updates:  
- 2024-11-14: Initial version supporting Metrc NY deliveries endpoint.  
- Added support for Metrc Packages and Active Deliveries endpoints.
- Both endpoints support query string parameters for filtering results.
- Improved error handling and logging for better diagnostics.
- 



