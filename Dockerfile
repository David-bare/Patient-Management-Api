# Use the official .NET SDK image for build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only necessary project files
COPY PatientManagement.csproj ./
RUN dotnet restore

# Copy the entire project after restore (to avoid unnecessary changes triggering restore)
COPY . ./

# Build and publish the app
RUN dotnet publish PatientManagement.csproj -c Release -o out

# Use a lightweight runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PatientManagement.dll"]
