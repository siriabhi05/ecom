# # Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and the .csproj files for all projects into the container
COPY Ecom.Services.Product.sln ./
COPY Ecom.Services.Product.API/Ecom.Services.Product.Api.csproj ./Ecom.Services.Product.API/Ecom.Services.Product.Api.csproj
COPY Ecom.Services.Product.Database/Ecom.Services.Product.Database.csproj ./Ecom.Services.Product.Database/Ecom.Services.Product.Database.csproj
COPY Ecom.Services.Product.Service/Ecom.Services.Product.Service.csproj ./Ecom.Services.Product.Service/Ecom.Services.Product.Service.csproj

# Restore dependencies for all projects via the solution file
RUN dotnet restore Ecom.Services.Product.sln

# Copy the remaining source code for all projects into the container
COPY . ./

# Publish the API project (which depends on the other projects)
RUN dotnet publish Ecom.Services.Product.API/Ecom.Services.Product.Api.csproj -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV AWS_REGION="ap-south-1"

# Expose port 80 for the web API
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Ecom.Services.Product.Api.dll"]
