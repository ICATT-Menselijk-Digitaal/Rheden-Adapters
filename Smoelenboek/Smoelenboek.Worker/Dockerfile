FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY RhedenAdapters.slnx .
COPY Smoelenboek/Entra.Client/Entra.Client.csproj                   Smoelenboek/Entra.Client/
COPY Shared/OpenObjects.Client/OpenObjects.Client.csproj             Shared/OpenObjects.Client/
COPY Smoelenboek/Smoelenboek.Adapter/Smoelenboek.Adapter.csproj     Smoelenboek/Smoelenboek.Adapter/
COPY Smoelenboek/Smoelenboek.Worker/Smoelenboek.Worker.csproj       Smoelenboek/Smoelenboek.Worker/
RUN dotnet restore Smoelenboek/Smoelenboek.Worker/Smoelenboek.Worker.csproj

COPY Smoelenboek/ Smoelenboek/
COPY Shared/ Shared/
RUN dotnet publish Smoelenboek/Smoelenboek.Worker/Smoelenboek.Worker.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Smoelenboek.Worker.dll"]
