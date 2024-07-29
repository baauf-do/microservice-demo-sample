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
|                                                                                                                                               |
|Product.API        |sample.product.api       |[5002](http://localhost:5002)  |[6002](http://product.api:6002)  |[xx02](http://xx:x002)         |
|Customer.API       |sample.customer.api      |[5003](http://localhost:5003)  |[6003](http://customer.api:6003) |[xx03](http://xx:x003)         |
|Basket.API         |sample.basket.api        |[5004](http://localhost:5004)  |[6004](http://basket.api:6004)   |[xx04](http://xx:x004)         |
|Ordering.API       |sample.ordering.api      |[5005](http://localhost:5005)  |[6005](http://ordering.api:6005) |[xx05](http://xx:x005)         |
|Inventory.API      |sample.inventory.api     |[5006](http://localhost:5006)  |[6006](http://inventory.api:6006)|[xx06](http://xx:x006)         |
|Hangfire.API       |sample.hangfire.api      |[5008](http://localhost:5008)  |[6008](http://.api:6008)         |[xx08](http://xx:x008)         |
|                                                                                                                                               |
|                                                                                                                                               |

