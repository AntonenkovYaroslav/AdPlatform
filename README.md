# AdPlatform - Advertising Platform Service

# Description
A web service for selecting advertising platforms by location using the REST API

# Technologies
- .NET 8.0
- ASP.NET Core Web API
- xUnit
- Swagger

# Solution structure
- **AdPlatform** - the main Web API project
- **AdPlatform.Tests** - a project with unit tests

# API Methods

# Loading platforms
POST /api/Advertising/upload

Loads advertising platforms from a file

# Site search
GET /api/Advertising/search?location=/ru/msk


Retrieves the list of sites for the specified location

# How to launch
1. Open 'AdPlatform.sln` in Visual Studio
2. Install 'AdPlatform` like a startup project
3. Press F5 to launch
4. Open Swagger UI for API testing

# Sample file with ad platforms 
AdPlatform/SwaggerTest.txt
