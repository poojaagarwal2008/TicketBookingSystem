
# 🎟️ Ticket Booking System API

A robust, concurrency-safe seat booking system built with ASP.NET Core 8 Web API. Designed to simulate a real-world high-load environment with thread safety, data consistency, and extensibility.

---

## ✅ Setup Instructions

### 📋 Prerequisites
- .NET SDK 8.0+
- Visual Studio 2022
- SQL Server / LocalDB (installed and running)

---

### ⚙️ Getting Started

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/TicketBookingSystem.git
   cd TicketBookingSystem
   ```

2. **Update Database Connection**
   Edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TicketBookingDb;Trusted_Connection=True;"
   }
   ```

3. **Apply Migrations**
   Open the Package Manager Console and run:
   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```

4. **Run the API**
   Use Visual Studio or:
   ```bash
   dotnet run
   ```

5. **Test via Swagger**
   Visit:
   ```
   https://localhost:<port>/swagger
   ```

---

## 🔌 API Endpoints

### 🎯 Book a Seat
```http
POST /api/seats/book
```

**Request Body**
```json
{
  "seatId": 1,
  "userName": "Pooja Agarwal"
}
```

**Responses**
- ✅ `200 OK` – Seat successfully booked
- ❌ `409 Conflict` – Seat already booked or concurrency issue
- ❌ `400 Bad Request` – Validation failed
- ❌ `429 Too Many Requests` – Rate limit exceeded

---

## 📌 Assumptions Made

- The system is built as a **stateless REST API** that does not rely on session memory.
- Each request is **limited to booking only one seat**.
- If a seat is already booked, further booking attempts are rejected.
- A **RowVersion** field ensures **optimistic concurrency**, so two users cannot book the same seat simultaneously.
- **Input validation** (required fields, character limits) is enforced using `[DataAnnotations]`.
- Rate limiting is enabled via `AspNetCoreRateLimit`, configured as:
  - Max 10 requests per minute per IP.
  - Max 100 requests per hour per IP.
- The API currently does not implement authentication/authorization but is extensible for token-based auth.
- Database schema is seeded manually or via migrations — no initial dummy data included.

---

## 🧪 Testing

Unit tests are located under the `TicketBookingSystem.Tests` project using:
- **XUnit** for assertions
- **Moq** for mocking repositories

Run tests with:
```bash
dotnet test
```

---

## 🤝 Author

**Pooja Agarwal**  
Senior Software Engineer | Full-Stack .NET Developer  
📧 [poojaagarwal2008@gmail.com](mailto:poojaagarwal2008@gmail.com)  
📍 India

---
