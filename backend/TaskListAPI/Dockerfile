# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /TaskList/TaskListAPI

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /TaskList/TaskListAPI
COPY --from=build /TaskList/TaskListAPI/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "TaskListAPI.dll"]
