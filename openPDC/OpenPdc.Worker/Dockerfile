FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY RhedenAdapters.slnx .
COPY openPDC/OpenPdc.Client/OpenPdc.Client.csproj       openPDC/OpenPdc.Client/
COPY Shared/OpenObjects.Client/OpenObjects.Client.csproj Shared/OpenObjects.Client/
COPY openPDC/OpenPdc.Adapter/OpenPdc.Adapter.csproj     openPDC/OpenPdc.Adapter/
COPY openPDC/OpenPdc.Worker/OpenPdc.Worker.csproj       openPDC/OpenPdc.Worker/
RUN dotnet restore openPDC/OpenPdc.Worker/OpenPdc.Worker.csproj

COPY openPDC/ openPDC/
COPY Shared/ Shared/
RUN dotnet publish openPDC/OpenPdc.Worker/OpenPdc.Worker.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OpenPdc.Worker.dll"]
