# Web Stock Management <!-- omit in toc -->
This project is meant to create a stock management web tool for toys and games

- [Requirements](#requirements)
  - [UI Mockups](#ui-mockups)
- [Architecture](#architecture)
    - [**Testing and access**](#testing-and-access)
  

# Requirements

Create a stock management web tool for “Toys and Games” store.

- [x] Should be able to list the available products in a grid
- [x] Should be able to Create, Update, Delete products
- [x] Should provide a simple Form when creating or updating products
- [x] Should provide user confirmation for product Deletion
- [ ] **(OPTIONAL)** display product images

### **Table design** <!-- omit in toc -->

Name | Type | Optional | Constrains
---- | ---- | -------- | ----------
**Id** | int | No | Unique
**Name** | string | No | Max length 50
**Description** | string | Yes | Max length 100
**AgeRestriction** | int | Yes | 0 to 100
**Company** | string | No | Max length 50
**Price** | decimal | No | 1$ to $1000

## UI Mockups

![](images/form.png)

![](images/table.png)

# Architecture

**Resource definition**
Action | Verbs | URL
------ | ----- | ---
Create Product | **POST** | {{server}}/products
Update Product | **PUT** |{{server}}/products/{productId}
Delete Product | **DELETE** | {{server}}/products/{productId}
Get All Products | **GET** | {{server}}/products?startIndex=1&itemCount=10
Get Product By ID | **GET** | {{server}}/products/{productId}

### **Testing and access** <!--omit in toc-->
You can use Swagger for manual testing in your browser. You can access it by typing swagger into the API URL like: http://localhost:8080/swagger

![](images/swagger.png)

**Data Persistence**
1. Use simple file storage (EF In memory database, JSON, Redis, etc)
2. Implement IRepository pattern
3. Use Code First
4. (OPTIONAL) Use seed data

**Server Side**
1. Use ASP.NET Core 3.1 (web API)
2. Model/Entity validation
3. Use Dependency Injection
4. Add a Unit test project with at least 5 unit tests using either xUnit, MSTest
5. (OPTIONAL) Use mocking framework

**(OPTIONAL) Client Side**
1. Use framework Angular 6 or Above
2. Use simple CSS or a CSS framework like
3. Bootstrap to provide a simple UI experience
4. Input form validation
5. (OPTIONAL) Use granular components
6. (OPTIONAL) Use SASS or LESS
7. (OPTIONAL) Use responsive design
8.  (OPTIONAL) Add JS unit test