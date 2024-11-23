#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ecommerce.OrderProcessing.Api/Ecommerce.OrderProcessing.Api.csproj", "Ecommerce.OrderProcessing.Api/"]
COPY ["Ecommerce.OrderProcessing.Application/Ecommerce.OrderProcessing.Application.csproj", "Ecommerce.OrderProcessing.Application/"]
COPY ["Ecommerce.OrderProcessing.Domain/Ecommerce.OrderProcessing.Domain.csproj", "Ecommerce.OrderProcessing.Domain/"]
COPY ["Ecommerce.OrderProcessing.Infras/Ecommerce.OrderProcessing.Infras.csproj", "Ecommerce.OrderProcessing.Infras/"]
RUN dotnet restore "./Ecommerce.OrderProcessing.Api/./Ecommerce.OrderProcessing.Api.csproj"
COPY . .
WORKDIR "/src/Ecommerce.OrderProcessing.Api"
RUN dotnet build "./Ecommerce.OrderProcessing.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ecommerce.OrderProcessing.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.OrderProcessing.Api.dll"]