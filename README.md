# APEX-WMS: Advanced Warehouse Management System

A comprehensive ASP.NET Core MVC-based warehouse management system designed for inventory tracking, stock management, supplier coordination, and order fulfillment.

## Features

- **Inventory Management**: Track items, quantities, and storage locations
- **Stock Operations**: In/out transactions with audit trails
- **Product Management**: Comprehensive product catalog with SKU tracking
- **Supplier Management**: Vendor information and purchase orders
- **Order Management**: Sales and purchase order processing
- **User Authentication**: Role-based access control (Admin, Manager, Operator)
- **Reporting & Analytics**: Inventory reports, stock movement, and KPIs
- **Dashboard**: Real-time warehouse metrics and alerts

## Technology Stack

- **Framework**: ASP.NET Core 6.0+ MVC
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, jQuery

## Project Structure

```
APEX-WMS/
├── Models/                 # Data models
├── Controllers/            # Business logic controllers
├── Views/                  # Razor templates (UI)
├── Data/                   # DbContext and migrations
├── Services/               # Business services
├── wwwroot/               # Static files (CSS, JS, images)
├── appsettings.json       # Configuration
└── Program.cs             # Application entry point
```

## Getting Started

### Prerequisites
- .NET 6.0 SDK or higher
- SQL Server (2019+)
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
```bash
git clone https://github.com/INMAD11/APEX-WMS.git
cd APEX-WMS
```

2. Restore NuGet packages
```bash
dotnet restore
```

3. Update connection string in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=APEX_WMS;Trusted_Connection=true;"
  }
}
```

4. Apply database migrations
```bash
dotnet ef database update
```

5. Run the application
```bash
dotnet run
```

6. Access at `http://localhost:5000`

## Default Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@apex-wms.com | Admin@123 |
| Manager | manager@apex-wms.com | Manager@123 |
| Operator | operator@apex-wms.com | Operator@123 |

## Database Schema

### Core Tables
- **Users**: Application users with roles
- **Products**: Product catalog with SKU
- **Inventory**: Stock levels by location
- **Suppliers**: Vendor information
- **Orders**: Sales and purchase orders
- **StockMovements**: Transaction audit trail

## License

MIT License - see LICENSE file for details

## Support

For issues and feature requests, please create an issue in the repository.
