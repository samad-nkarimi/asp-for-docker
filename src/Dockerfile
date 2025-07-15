# # Use .NET 8 SDK image for build and dev (needed for dotnet watch)
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

# WORKDIR /app

# # Copy everything and restore (cache-friendly)
# COPY *.csproj ./
# RUN dotnet restore


# RUN dotnet tool install --global dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"

# COPY . ./

# # Expose port 80 for the app
# EXPOSE 80

# # Run with dotnet watch for hot reload
# ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:80"]


# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "AspForDocker.dll"]
