FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MedicalRecords.Service.Api/MedicalRecords.Service.Api.csproj", "MedicalRecords.Service.Api/"]
COPY ["MedicalRecords.Service.Core/MedicalRecords.Service.Core.csproj", "MedicalRecords.Service.Core/"]
COPY ["MedicalRecords.Service.Services/MedicalRecords.Service.Services.csproj", "MedicalRecords.Service.Services/"]

RUN dotnet restore "MedicalRecords.Service.Api/MedicalRecords.Service.Api.csproj"

COPY . .

WORKDIR "/src/MedicalRecords.Service.Api"
RUN dotnet build "MedicalRecords.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedicalRecords.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MedicalRecords.Service.Api.dll"]