#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Courses.API/Courses.API.csproj", "Courses.API/"]
COPY ["Courses.Application/Courses.Application.csproj", "Courses.Application/"]
COPY ["Courses.DataOperations/Courses.DataOperations.csproj", "Courses.DataOperations/"]
COPY ["Courses.Entities/Courses.Entities.csproj", "Courses.Entities/"]
COPY ["Courses.DataTransferObjects/Courses.DataTransferObjects.csproj", "Courses.DataTransferObjects/"]
RUN dotnet restore "Courses.API/Courses.API.csproj"
COPY . .
WORKDIR "/src/Courses.API"
RUN dotnet build "Courses.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Courses.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Courses.API.dll"]