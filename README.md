# Reward System Project

## Overview

The Reward System Project is a web-based application designed to manage and facilitate a customer rewards program. It allows users to register, log in, earn points based on purchases, check their points balance, and redeem rewards or coupons. The project features a frontend interface for user interaction and a backend API for business logic and data management.

---

## Features

- **User Registration & Login:**  
  New users can register an account, and existing users can securely log in.

- **Points Management:**  
  Users can add points to their account by entering purchase amounts. The system calculates and awards points accordingly.

- **Dashboard:**  
  Once logged in, users access a dashboard where they can:
  - Add points for purchases
  - Check their current points balance
  - Redeem their points for available rewards or coupons

- **Coupon Redemption:**  
  Users can redeem their accumulated points for coupons directly from the dashboard interface.

---

## Project Structure

```
Reward-system-project-/
│
├── frontend/
│   └── index.html           # Main frontend HTML file for user interface and dashboard
│
├── RewardSystemAPI/         # (Implied from build output) Backend business logic and REST API
│
├── obj/Debug/net8.0/        # .NET build artifacts
│   ├── RewardSystemAPI.csproj.FileListAbsolute.txt
│   ├── RewardSystemAPI.AssemblyInfo.cs
│   ├── RewardSystemAPI.MvcApplicationPartsAssemblyInfo.cs
│   └── ...                  # Additional build and configuration files
│
└── ...                      # Other supporting files and configurations
```

---

## Technologies Used

- **Frontend:**  
  HTML, CSS, JavaScript (runs in the browser, provides the user interface)

- **Backend:**  
  .NET 8 (RewardSystemAPI)  
  Entity Framework Core (implied)  
  Swagger/OpenAPI for API documentation  
  MySQL (via Pomelo.EntityFrameworkCore.MySql)

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MySQL Server](https://www.mysql.com/downloads/)
- (Optional) Any modern web browser for the frontend

### 1. Clone the Repository

```bash
git clone https://github.com/Chourey481/Reward-system-project-.git
cd Reward-system-project-
```

### 2. Configure the Backend

- Go to the backend project directory (likely `RewardSystemAPI`).
- Edit `appsettings.json` or `appsettings.Development.json` to set your MySQL connection string and other environment variables as needed.

Example connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=reward_system;user=root;password=yourpassword;"
}
```

### 3. Run Database Migrations

If migrations are set up, run:
```bash
dotnet ef database update --project RewardSystemAPI
```
*(If the EF CLI is not installed, add it with `dotnet tool install --global dotnet-ef`)*

### 4. Run the Backend API

```bash
cd RewardSystemAPI
dotnet run
```
The API will typically be available at `http://localhost:5062` (or see your console output for the exact port).

### 5. Run the Frontend

- Open `frontend/index.html` directly in your browser.

**Note:** The frontend JavaScript expects the backend API to be running at `http://localhost:5062`. If your backend runs on a different port, update the fetch URLs in `frontend/index.html` accordingly.

---

## Usage

1. Register a new account with your mobile number, name, and password.
2. Log in using your registered credentials.
3. Use the dashboard to:
   - Add points (by entering a purchase amount)
   - View your total points
   - Redeem your points for coupons (at 500 or 1000 points thresholds)

---

## Contributing

Contributions are welcome!  
Please open issues and submit pull requests for new features, bug fixes, or improvements.

---

## Acknowledgements

- Built with .NET 8 and Entity Framework Core
- Postman collection URL : https://.postman.co/workspace/My-Workspace~9aa3db63-ab6a-413a-8873-e7b054891f43/collection/47933332-116715c2-8614-4e13-8ff9-d82cf693a96f?action=share&creator=47933332
- Pomelo EntityFrameworkCore MySQL provider
