# Employee Timesheet Management

## Project Description

Employee Timesheet Management is a streamlined application designed to manage employee work hours effectively. This system enables employees to log their daily work hours and allows managers to approve or reject these logged hours efficiently. The application ensures smooth tracking and management of employee timesheets through an integrated console app and ASP.NET Core web application.

## Business Process

### Employees
- **Log Work Hours**: Employees use a console application to log their daily work hours.

### Managers
- **Approve/Reject Logged Hours**: Managers use an ASP.NET Core web application to view, approve, or reject the timesheets submitted by employees.
- **Triggers**: Get data audit in trigger 

## Database Structure

### Tables Involved

1. **Employees**
   - `EmployeeID` (Primary Key)
   - `Name`

2. **Timesheets**
   - `TimesheetID` (Primary Key)
   - `EmployeeID` (Foreign Key)
   - `Date`
   - `HoursWorked`
   - `Status` (Pending, Approved, Rejected)

## Key Features

### Employee Login (Console App)
- Employees can log their work hours via a console application.
- Simple and user-friendly interface for easy time logging.

### Manager Approval (ASP.NET Core)
- Managers can view a list of timesheets submitted by employees.
- Ability to approve or reject timesheets.
- Real-time status updates for each timesheet.

### Timesheet Status Update (SQL Trigger)
- SQL trigger to automatically update the status of timesheets when they are approved or rejected by managers.

### Daily Reminder (Hangfire)
- Hangfire job to send daily reminders to employees to log their work hours.
- Ensures timely logging of work hours by all employees.

## Getting Started

### Prerequisites
- .NET Core SDK
- SQL Server management studio
- SQL Server Developer
- Docker
- Hangfire
- Visual Studio

### Installation

1. **Clone the Repository**
   ```sh
   git clone https://github.com/rizal-mujahiddan/MiniProjectRizalM.git
   cd MiniProjectRizalM
   ```

2. **Set Up the Database**
   - Configure your SQL Server database.
   - Update the connection string in `appsettings.json`.
   - Like this

3. **Run the Console Application**
   ```sh
   cd EmployeeConsoleApp
   dotnet run
   ```

4. **Run the ASP.NET Core Web Application**
   ```sh
   cd ManagerApprove
   dotnet run
   ```

5. **Run Docker Containers**
   ```sh
   docker-compose up
   ```

6. **Set Up Hangfire**
   - Using Rizal Mujahiddan

## Usage

### Employee Console Application
- Launch the console app.
- Log in with your employee ID.
- Enter your daily work hours and submit.

### Manager ASP.NET Core Application
- Launch the web application.
- Dont forget connecting using docker website
- View pending and approve timesheets.
- Approve or reject timesheets as necessary.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request for review.
