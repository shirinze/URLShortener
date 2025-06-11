# URL Shortener API
This project is a URL Shortening Service built using ASP.NET Core 8.0 and FastEndpoints. The service allows you to shorten long URLs into compact short URLs and easily redirect or manage them.
## Technologies Used
✅ ASP.NET Core 8.0

✅ FastEndpoints (High-performance API framework)

✅ MongoDB (NoSQL Database)

✅ Dependency Injection

✅ IConfiguration (Configuration Management)

✅ Clean Code & Separation of Concerns

## Features
✅ Shorten long URLs into short codes

✅ Store URL information in MongoDB

✅ Redirect to the original URL via short code

✅ Clean and scalable architecture with FastEndpoints

✅ Manage configurations via appsettings.json

## Available Endpoints
- POST /shorten ➔ Create a shortened URL

- GET /{code} ➔ Redirect to the original URL based on the short code

## Technical Details
- Fully stateless and RESTful API design.

- Clean and minimal endpoint definitions using FastEndpoints.

- MongoDB connection handled via dedicated DbContext class.

- Dependency Injection used for service registrations.

## Why FastEndpoints?
- High performance

- Simpler structure compared to Minimal APIs or MVC

- Built-in dependency injection

- Easy validation and middleware support

