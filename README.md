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



