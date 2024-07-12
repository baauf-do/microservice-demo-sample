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

|Service Source     |Port http (Development)        |Port http (Local/Docker)         |Port http (Production)         |
| ----------------- | ----------------------------- | ------------------------------- | ----------------------------- |
|                                                                                                                     |
|Product.API        |[5002](http://localhost:5002)  |[6002](http://product.api:6002)  |[5002](http://:5002)           |
|Customer.API       |[5003](http://localhost:5003)  |[6003](http://customer.api:6003) |[5003](http://:5003)           |
|Basket.API         |[5004](http://localhost:5004)  |[6004](http://basket.api:6004)   |[5004](http://:5004)           |
|Ordering.API       |[5005](http://localhost:5005)  |[6005](http://ordering.api:6005) |[5005](http://:5005)           |
|Inventory.API      |[5006](http://localhost:5006)  |[6006](http://inventory.api:6006)|[5006](http://:5006)           |
|                                                                                                                     |



# Further Reading

- [Microservices architecture style](https://docs.microsoft.com/en-us/azure/architecture/guide/architecture-styles/microservices)
- [Health monitoring](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/monitor-app-health)
- [Load balancing](https://ocelot.readthedocs.io/en/latest/features/loadbalancer.html)
- [Testing ASP.NET Core services](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/test-aspnet-core-services-web-apps)
