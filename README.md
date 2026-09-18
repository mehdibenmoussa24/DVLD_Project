# DVLD Project

A Windows Forms application for managing people and driving license services.

## Technologies

- C#
- .NET Framework
- Windows Forms
- SQL Server
- ADO.NET
- 3-Tier Architecture
- Visual Studio

## Architecture

The project follows a 3-Tier Architecture:

### Presentation Layer
`DVLD`

- Windows Forms
- UserControls
- UI logic

### Business Logic Layer
`DVLD.Business`

- Business rules
- Application logic

### Data Access Layer
`DVLD.DataAccess`

- Database access
- ADO.NET
- SQL Server

## Current Progress

### People Management

The initial People Management module has been implemented.

#### Implemented Features

- Add and retrieve person information.
- Person data access and business logic.
- `ctrlPersonCard` for displaying detailed person information.
- `ctrlPersonCardWithFilter` for searching and filtering people.
- `frmFindPerson` for finding a person.
- `frmShowPersonInfo` for displaying person information.
- `frmListPeople` for listing and managing people.
- DataGridView with filtering functionality.
- Initial Add and Close button functionality.

### Person Management Improvements

- Added `frmAddUpdatePerson` for adding and updating person information.
- Standardized control names for better clarity and maintainability.
- Added validation for required fields, email, and national number.
- Added gender-based default images and image handling.
- Improved form initialization and Add/Update mode handling.
- Added placeholders, focus events, and error feedback.
- Renamed the `Country` property to `CountryInfo`.
- Added `Util.cs` with reusable utility methods for GUIDs, folders, file names, and image handling.
- Added validation and utility classes.
- Updated project files and resources.
- Removed obsolete controls and event handlers.

## UI & Resources

- Added reusable UserControls for person-related functionality.
- Added and organized person-related images and icons.
- Added `Person_ico` resource.
- Added `PeopleList.png` and `stop.png`.
- Updated `.resx` and `.Designer.cs` files.
- Updated `.csproj` with new forms and resources.
- Organized person-related controls under:

`People_folder\Controls`

## Implemented Components

| Component | Purpose |
|---|---|
| `ctrlPersonCard` | Display detailed person information |
| `ctrlPersonCardWithFilter` | Search and filter people |
| `frmFindPerson` | Find a specific person |
| `frmShowPersonInfo` | Display selected person information |
| `frmListPeople` | List and manage people |

## Project Structure

```text
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
