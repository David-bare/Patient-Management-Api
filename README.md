# Patient-Management-Api

## Overview

The **Patient Management API** is a backend service built to manage patient records efficiently. It provides endpoints for creating, updating, retrieving, and soft-deleting patient records. The API is designed to be scalable, secure, and easily deployable using containerization with Docker.

## Features

- **Patient Management**: CRUD operations for patient records.
- **Soft Delete Mechanism**: Ensures patient records are not permanently removed but marked as deleted.
- **Containerized Deployment**: The API can be deployed using Docker.
- **Unit Testing**: Includes tests for core functionalities to ensure reliability.
- **RESTful Design**: Follows best practices for API development.

## Technology Stack

- **.NET 8.0** - Core framework for building the API.
- **Entity Framework Core** - ORM for database interactions.
- **SQLite** - Lightweight database for persistent storage.
- **XUnit & Moq** - Testing framework for unit tests.
- **Docker** - Containerization for easy deployment.

## Installation & Setup

### Prerequisites

Ensure you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/get-started)
- [Visual Studio](https://visualstudio.microsoft.com/) (Optional but recommended)

### Running the API Locally

1. Clone the repository:
   ```sh
   git clone https://github.com/David-bare/Patient-Management-Api.git
   cd project-management-api
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Build and run the application:
   ```sh
   dotnet run --project PatientManagement/PatientManagement.csproj
   ```
4. The API should now be running on `http://localhost:5158` or `https://localhost:7052`.

### Running with Docker

1. Build the Docker image:
   ```sh
   docker build -t patient-management-api .
   ```
2. Run the container:
   ```sh
   docker run -p 5000:5000 -p 5001:5001 patient-management-api
   ```

## API Endpoints

### Patient Management

| Method     | Endpoint                               | Description                       |
| ---------- | -------------------------------------- | --------------------------------- |
| **POST**   | `/api/Patients/CreatePatient`          | Create a new patient record       |
| **GET**    | `/api/Patients/GetPatients`            | Retrieve all patients             |
| **GET**    | `/api/Patients/GetPatient/{id}`        | Retrieve a specific patient by ID |
| **PUT**    | `/api/Patients/UpdatePatient/{id}`     | Update a patient’s details        |
| **DELETE** | `/api/Patients/SoftDeletePatient/{id}` | Soft delete a patient record      |

### Patient Records

| Method   | Endpoint                                       | Description                              |
| -------- | ---------------------------------------------- | ---------------------------------------- |
| **POST** | `/api/PatientRecords/CreateRecord`             | Create a new patient record              |
| **GET**  | `/api/PatientRecords/GetRecords`               | Retrieve all patient records             |
| **GET**  | `/api/PatientRecords/GetRecord/{PatientId}`    | Retrieve a specific record by Patient ID |
| **PUT**  | `/api/PatientRecords/UpdateRecord/{PatientId}` | Update a patient record                  |

## Thought Process & Design Decisions

1. **Scalability**: The API is structured to allow easy expansion, following RESTful principles.
2. **Soft Delete Approach**: Instead of permanently deleting records, we mark them as `IsDeleted = true` to maintain historical data.
3. **Containerization**: Docker support ensures a consistent deployment experience across different environments.
4. **Testing**: Unit tests were added using XUnit to validate core functionalities.
5. **Security Considerations**: Authentication and authorization can be added in future iterations.

## Running Tests

To run the unit tests, use the following command:

```sh
cd PatientManagement.Tests
dotnet test
```

## Contributors

- Ogunlaja David



