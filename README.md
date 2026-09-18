DVLD Project

A Windows Forms application for managing driving license and driver-related operations.

Technologies
C#
.NET Framework
Windows Forms
SQL Server
ADO.NET
3-Tier Architecture
Visual Studio
Architecture

The project follows a 3-Tier Architecture:

Presentation Layer (DVLD)
Windows Forms, UserControls, and UI logic.
Business Logic Layer (DVLD.Business)
Business rules and application logic.
Data Access Layer (DVLD.DataAccess)
Database access using ADO.NET and SQL Server.
Current Progress
People Management

Implemented the initial People management functionality, including:

Person data access and business logic.
Add and retrieve person information.
ctrlPersonCard UserControl for displaying person details.
ctrlPersonCardWithFilter UserControl for searching and filtering people.
frmFindPerson for finding a person.
frmShowPersonInfo for displaying person information.
frmListPeople for listing and managing people.
DataGridView with filtering functionality.
Initial Add and Close button functionality.
UI & Resources
Added reusable UserControls for person-related functionality.
Added and organized image resources.
Added person-related icons and images.
Updated .resx, .Designer.cs, and .csproj files.
Improved resource references and ordering.
Organized person-related controls under:
People_folder\Controls
Implemented Components
Component	Purpose
ctrlPersonCard	Display detailed person information
ctrlPersonCardWithFilter	Search and filter people
frmFindPerson	Find a specific person
frmShowPersonInfo	Display selected person information
frmListPeople	List and manage people
Current Status

The project is currently focused on building the People Management module and establishing reusable UI components and a clean 3-Tier architecture.

Some features are still under development and will be implemented progressively.

Project Structure
DVLD
│
├── DVLD
│   ├── People_folder
│   │   ├── Controls
│   │   │   ├── ctrlPersonCard
│   │   │   └── ctrlPersonCardWithFilter
│   │   │
│   │   ├── frmFindPerson
│   │   ├── frmShowPersonInfo
│   │   └── frmListPeople
│   │
│   └── ...
│
├── DVLD.Business
│   └── ...
│
├── DVLD.DataAccess
│   └── ...
│
├── Commen
│   └── ...
│
└── DVLD.slnx
Development Status

In Progress

The application is being developed incrementally, with new modules and functionality added as development progresses.
