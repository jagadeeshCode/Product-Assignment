# ProductsAssesment API Documentation
## Overview
The ProductsAssesment API enables access to product-related operations.

## Technologies
.NET 8

C#

Web API

xUnit

## Authentication
Obtain a Bearer Token (JwtBearer) through the following endpoint without providing login details:

### Auth - POST /api/Products/Auth
Method: POST

Response Status Codes:

200: Success

Include the obtained Bearer Token in the headers for accessing protected endpoints.

## Endpoints
### Health Check - GET /api/Products/healthCheck

Check the health of the service.

Method: GET

Response Status Codes:

200: Success

## Products

### Get All Products - GET /api/Products

Get a list of all products.

Method: GET

Security:Bearer Token

Response Status Codes:

200: Success

401: Unauthorized

403: Forbidden

### Get Products by Color - GET /api/Products/{color}

Get products filtered by color.

Parameters:

color (path): The color to filter by (string).

Method: GET

Security:Bearer Token

Response Status Codes:

200: Success

401: Unauthorized

403: Forbidden

