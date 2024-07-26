# microservice-demo-sample
Microservice demo sample. With, architecture using ASP.NET Core, Ocelot, MongoDB and JWT


## Development Environment

- Visual Studio Code
- .NET 8.0.3
- MongoDB
- Postman
- Docker

## Architecture

There are three microservices:

- **CatalogMicroservice**: allows to manage the catalog.
- **CartMicroservice**: allows to manage the cart.
- **IdentityMicroservice**: allows to manage users.

## Reference
- [microservices-source](https://github.com/aelassas/microservices)


## Urls / Ports of services

|Service Source     |Container Name (Docker)  |Port http (Development)        |Port http (Local/Docker)         |Port http (Production)         |
| ----------------- | ----------------------- | ----------------------------- | ------------------------------- | ----------------------------- |
|                                                                                                                                               |
|Api Gateway        |apigw.ocelot             |[5500](http://localhost:5500)  |[6500](http://apigw.ocelot:6500) |[xx00](http://xx:x500)         |
|STS => App/User/Sys|.                        |[5501](http://localhost:5501)  |[6502](http://sts.sso:6501)      |[xx01](http://xx:x501)         |
|Admin => Manage STS|.                        |[5502](http://localhost:5502)  |[6501](http://admin.sso:6502)    |[xx02](http://xx:x502)         |
|                                                                                                                                               |
|Product.API        |sample.product.api       |[5002](http://localhost:5002)  |[6002](http://product.api:6002)  |[xx02](http://xx:x002)         |
|Customer.API       |sample.customer.api      |[5003](http://localhost:5003)  |[6003](http://customer.api:6003) |[xx03](http://xx:x003)         |
|Basket.API         |sample.basket.api        |[5004](http://localhost:5004)  |[6004](http://basket.api:6004)   |[xx04](http://xx:x004)         |
|Ordering.API       |sample.ordering.api      |[5005](http://localhost:5005)  |[6005](http://ordering.api:6005) |[xx05](http://xx:x005)         |
|Inventory.API      |sample.inventory.api     |[5006](http://localhost:5006)  |[6006](http://inventory.api:6006)|[xx06](http://xx:x006)         |
|Hangfire.API       |sample.hangfire.api      |[5008](http://localhost:5008)  |[6008](http://.api:6008)         |[xx08](http://xx:x008)         |
|                                                                                                                                               |
|Exam API           |Examination.API          |[5102](http://localhost:5102)  |[6102](http://product.api:6102)  |[xx02](http://xx:x102)         |
|Exam Admin         |AdminApp                 |[5101](http://localhost:5101)  |[6101](http://customer.api:6101) |[xx03](http://xx:x101)         |
|Exam Portal        |exam_web_app             |[5103](http://localhost:5103)  |[6104](http://basket.api:6103)   |[xx04](http://xx:x103)         |
|                                                                                                                                               |


- **Exam Port**
  - Identity STS: https://localhost:5001 => http://localhost:5501
  - Exam API: https://localhost:5002 => http://localhost:5102
  - Exam Admin: https://localhost:6001 => http://localhost:5101
  - Exam Portal: https://localhost:6002 => http://localhost:5103
  - Identity Admin: https://localhost:6003 => http://localhost:5502

# Further Reading

- [Microservices architecture style](https://docs.microsoft.com/en-us/azure/architecture/guide/architecture-styles/microservices)
- [Health monitoring](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/monitor-app-health)
- [Load balancing](https://ocelot.readthedocs.io/en/latest/features/loadbalancer.html)
- [Testing ASP.NET Core services](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/test-aspnet-core-services-web-apps)
