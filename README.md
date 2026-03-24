## Product Inventory Management API

##  Project Overview

This project is a **RESTful Web API** built using **ASP.NET Core** and **Entity Framework Core** to manage product inventory.
It allows users to perform CRUD operations such as adding, retrieving, updating, and deleting products.

---

##  Features

* Get all products
* Get product by ID
* Add new product
* Update existing product
* Delete product
* Input validation and proper error handling

---

##  Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger (for API testing)

---

##  Project Structure


ProductInventoryManagement/
│
├── Controllers/
│   └── ProductsController.cs
│
├── Models/
│   └── Product.cs
│
├── Data/
│   └── ProductDbContext.cs
│
├── Migrations/
│
├── Properties/
│
├── Program.cs
├── appsettings.json
├── ProductInventoryManagement.csproj




##  Setup Instructions

1. Clone the repository

```
git clone <your-repo-link>
```

2. Open the project in Visual Studio

3. Update database connection string in:

```
appsettings.json
```

4. Apply migrations:

```
Update-Database
```

5. Run the project

6. Open Swagger UI:

```
https://localhost:<port>/swagger
```

---

##  API Endpoints

| Method | Endpoint           | Description       |
| ------ | ------------------ | ----------------- |
| GET    | /api/Products      | Get all products  |
| GET    | /api/Products/{id} | Get product by ID |
| POST   | /api/Products      | Add new product   |
| PUT    | /api/Products/{id} | Update product    |
| DELETE | /api/Products/{id} | Delete product    |

---

##  Sample Request (POST)

```
{
  "name": "Mobile",
  "category": "Electronics",
  "price": 40000,
  "stockQuantity": 5
}
```

---

##  Validation & Error Handling

* Returns **400 BadRequest** for invalid input
* Returns **404 NotFound** if product does not exist
* Ensures price is greater than zero

---

##  Key Concepts Used

* Dependency Injection
* Entity Framework Core (Code First Approach)
* REST API Design
* Asynchronous Programming (async/await)

---

##  Conclusion

This project demonstrates how to build a complete backend API using ASP.NET Core with database integration, validations, and structured architecture.


