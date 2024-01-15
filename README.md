# ThreeAmigos.Services.ProductCatalogue


# Overview
ThreeAmigos.Services.ProductCatalogue is an ASP.NET Core Web API microservice designed to interface with a CosmosDB to retrieve and manage product data for an e-commerce web application. It supports operations such as fetching all products, retrieving filtered products based on search terms, and integrating with front-end services to display products.


# Features
- Fetch Products: Retrieve a list of all products from CosmosDB.
- Search Functionality: Return filtered products based on a search term.
- Integration: Designed to work seamlessly with front-end web applications.
- Unit Testing: Includes a suite of unit tests using XUnit to ensure service reliability.


# Built With
- ASP.NET Core: The framework used to build the microservice.
- CosmosDB: Azure's NoSQL database for managing product data.
- XUnit: The testing framework used for unit tests.


# API Testing with Swagger
Upon running the microservice, Swagger UI will be available for testing the API endpoints interactively.

To access Swagger UI:

- Start the microservice.
- Open a web browser and navigate to https://localhost:7030/swagger/index.html. This will load the Swagger UI with the API documentation.
- Use the Swagger UI to send requests to the API and view the responses.
Swagger provides a straightforward and interactive method for exploring and testing the API endpoints, allowing you to:

- View the list of available API endpoints and their details.
- Expand each endpoint to see the parameters it accepts and the response it returns.
- Try out each endpoint directly from the browser by entering required parameters and executing the request.
