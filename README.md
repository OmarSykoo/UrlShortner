# 🔗 URL Shortener API

A simple and efficient URL shortening service built with **ASP.NET Core** and **SQLite**. It generates a unique 7-character code for any long URL, stores it in a local database, and provides an API to redirect users.

---

## 🚀 Features

- Shorten long URLs into compact 7-character codes.
- Stores original and shortened URLs with timestamps.
- Redirects shortened URLs to the original destination.
- Ensures uniqueness of generated short codes.
- Swagger UI enabled for easy API testing in development.

---

## 🛠️ Tech Stack

- **.NET 8** / **ASP.NET Core Minimal APIs**
- **Entity Framework Core**
- **SQLite**
- **Swagger** for API documentation

---

## 🧪 API Endpoints

### 🔸 POST `/api/shorten`

**Shortens a long URL.**

#### Request Body

```json
{
  "url": "https://example.com/very/long/link"
}
```
#### Response

```json
"https://localhost:5001/api/Ab12XyZ"
```

### 🔹 GET `/api/{code}`

**Redirects to the original long URL associated with the short code.**

GET /api/Ab12XyZ

#### Behavior

- If the code **exists**, the server responds with a `302 Found` and redirects to the original long URL.
- If the code **does not exist**, the server responds with:

```json
{
  "status": 404,
  "message": "Url with code Ab12XyZ doesn't exist"
}
```

