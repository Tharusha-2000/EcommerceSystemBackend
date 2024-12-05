# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the solution file
COPY ../*.sln ./

# Copy the project files
COPY ../Ecommerce.OrderProcessing.Api/*.csproj Ecommerce.OrderProcessing.Api/
COPY ../Ecommerce.OrderProcessing.Application/*.csproj Ecommerce.OrderProcessing.Application/
COPY ../Ecommerce.OrderProcessing.Domain/*.csproj Ecommerce.OrderProcessing.Domain/
COPY ../Ecommerce.OrderProcessing.Infras/*.csproj Ecommerce.OrderProcessing.Infras/

# Restore dependencies
RUN dotnet restore Ecommerce.OrderProcessing.Api/Ecommerce.OrderProcessing.Api.csproj

# Copy the entire solution
COPY ../ .

# Build the application
RUN dotnet build Ecommerce.OrderProcessing.Api/Ecommerce.OrderProcessing.Api.csproj -c Release -o /app/build

# Publish the application
RUN dotnet publish Ecommerce.OrderProcessing.Api/Ecommerce.OrderProcessing.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use the official ASP.NET Core runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port on which the application runs
EXPOSE 5000

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Ecommerce.OrderProcessing.Api.dll"]
