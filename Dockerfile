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
# ENV ASPNETCORE_URLS=http://+:80
# EXPOSE 80

# # Run with dotnet watch for hot reload
# ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:80"]


# Build the migrator + app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/ ./src/
COPY Migrator/ ./Migrator/

RUN dotnet restore
RUN dotnet publish Migrator/Migrator.csproj -c Release -o /out/migrator
RUN dotnet publish src/AspForDocker.csproj -c Release -o /out/web

# Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy app and migrator

COPY --from=build /out/migrator ./migrator/
COPY --from=build /out/web ./web/

ENV ASPNETCORE_URLS=http://+:80

# Entrypoint script that runs migrator first, then starts app
CMD bash -c "dotnet ./migrator/Migrator.dll && dotnet ./web/AspForDocker.dll"


