# StudySpace

StudySpace is a full-stack study productivity application built with **ASP.NET Core Razor Pages**, **SQL Server**, and **JavaScript**.

The application combines task management, notes, a Pomodoro timer, calendar navigation, background audio, 
and customizable themes in a single personal dashboard.

## Live Demo

[Open StudySpace](https://studyspace-despoina-bydpeehdbeahgsec.francecentral-01.azurewebsites.net)

### Demo Account

- **Email:** `test@test.com`
- **Password:** `StudySpace_Test_9284!`

## Preview

### Login
<img src="StudySpaceApp/wwwroot/images/login.png" width="500">

### Dashboard
<img src="StudySpaceApp/wwwroot/images/blue-dashboard.png" width="350"> <img src="StudySpaceApp/wwwroot/images/pink-dashboard.png" width="350">
<img src="StudySpaceApp/wwwroot/images/green-dashboard.png" width="350"> <img src="StudySpaceApp/wwwroot/images/gray-dashboard.png" width="350">

## Features

### Productivity Tools
- **Task Management** - Add, complete, and delete tasks to organize your study priorities
- **Notes** - Create and delete study notes for quick reference
- **Pomodoro Timer** - Structured time management with 25-minute focus sessions and 5-minute breaks
- **Timer Notification** - Sound alerts when Pomodoro sessions end

### Study Environment
- **Background Music & Ambient Sounds** - Curated audio to enhance focus
- **Dynamic Calendar** - Navigate through previous and next months
- **Multiple Visual Themes** - Customize the interface to your preference

### Authentication & Data
- **Authentication** - User login with hashed password verification
- **Session Management** - Session-based access to the personal dashboard
- **Persistent Storage** - Tasks and notes stored in SQL Server
- **Personal Dashboards** - Tasks and notes are associated with each user's account

## Technologies

- **Backend:** C#, ASP.NET Core (.NET 10), Razor Pages
- **Database:** SQL Server, ADO.NET
- **Frontend:** HTML, CSS, JavaScript
- **Cloud:** Azure App Service, Azure SQL Database
- **Version Control:** Git, GitHub

## Architecture

The application follows a layered architecture:

```text
┌─────────────────────────────────┐
│     Razor Pages / PageModels    │
├─────────────────────────────────┤
│     Service Layer               │
│     DTOs / Models               │
├─────────────────────────────────┤
│     DAO Layer                   │
├─────────────────────────────────┤
│     SQL Server                  │
└─────────────────────────────────┘
```

## Project Structure

```text
StudySpaceApp/
├── Pages/                 # Razor Pages and page logic
│   ├── Login.cshtml
│   ├── Dashboard.cshtml
│   └── ...
├── Models/               # Domain models
│   ├── User.cs
│   ├── TodoTask.cs
│   └── Note.cs
├── DTO/                  # Data Transfer Objects
│   ├── UserLoginDTO.cs
│   ├── TodoTaskInsertDTO.cs
│   └── ...
├── Service/              # Business logic
│   ├── IUserService.cs
│   ├── UserServiceImpl.cs
│   └── ...
├── DAO/                  # Data Access Objects
│   ├── IUserDAO.cs
│   ├── UserDAOImpl.cs
│   └── ...
├── Helpers/              # Utility classes
│   └── DBHelper.cs
├── Database/             # Database scripts
│   └── StudySpaceDB.sql
└── wwwroot/              # CSS, images, audio, and other static assets
```

## Prerequisites

- Visual Studio 2022 or later (or Visual Studio Code with .NET SDK)
- .NET SDK 10.0 or later
- SQL Server or SQL Server Express
- SQL Server Management Studio (SSMS)
- Git

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/KrousaniotakiDespina/StudySpaceApp.git
cd StudySpaceApp
```

### 2. Set Up the Database

1. Open SQL Server Management Studio (SSMS).
2. Open `Database/StudySpaceDB.sql`.
3. Execute the script.
4. Confirm that the `StudySpaceDB` database and the `Users`, `TodoTasks`, and `Notes` tables have been created.

### 3. Configure Connection String

Configure the `DefaultConnection` using .NET User Secrets:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=StudySpaceDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
```

### 4. Run the Application

Restore dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

Open the local URL shown by ASP.NET Core in Visual Studio or the terminal.

To log in, use a valid user account that exists in the `Users` table.

## Deployment

The application can be deployed to any hosting environment that supports ASP.NET Core and SQL Server.

### 1. Create a Production Build

Build the application in Release mode:

```bash
dotnet build --configuration Release
```

Publish the application:

```bash
dotnet publish --configuration Release
```

The published files will be generated inside the project's `bin/Release` directory.

### 2. Configure the Production Database

Create a SQL Server database on the target environment and execute:

```text
StudySpaceApp/Database/StudySpaceDB.sql
```

Verify that the following tables exist:

- `Users`
- `TodoTasks`
- `Notes`

### 3. Configure the Production Connection String

The application requires a `DefaultConnection` connection string.

The connection string should be configured through the hosting environment rather than stored directly in the source code.

Example:

```text
Server=YOUR_SERVER;
Database=StudySpaceDB;
User Id=YOUR_USER;
Password=YOUR_PASSWORD;
TrustServerCertificate=True;
```

### 4. Run the Published Application

After configuring the database and connection string, deploy the published files to the target hosting environment.

The target environment must support the required .NET runtime, unless the application is published as self-contained.

### Security

Sensitive information such as database passwords, credentials, and production connection strings should not be committed to GitHub.

For local development, the application uses **.NET User Secrets** for sensitive configuration.

For production, sensitive values should be configured through the hosting environment.

## Usage

1. **Log In** - Enter your credentials to access the dashboard
2. **Manage Tasks** - Add, complete, or delete tasks
3. **Take Notes** - Create and delete study notes
4. **Use Pomodoro** - Start focus and break sessions
5. **Explore Calendar** - Navigate through months
6. **Customize Theme** - Select a visual theme
7. **Play Music** - Enable background music or ambient sounds

## Database Schema

The database contains the following main tables:

- **Users** - User accounts and authentication data
- **TodoTasks** - Tasks associated with users
- **Notes** - Notes associated with users

Refer to `StudySpaceApp/Database/StudySpaceDB.sql` for the complete database schema.