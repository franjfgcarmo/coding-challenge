FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

COPY ./Directory.Build.props ./Directory.Build.props
COPY ./Directory.Packages.props ./Directory.Packages.props
COPY ./global.json ./global.json
COPY ./src ./src
RUN dotnet restore src/Customer.Host/Customer.Host.csproj

WORKDIR /build/src/Customer.Host
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ARG CONNECTION_STRING="Server=host.docker.internal;persist security info=True;Database=customer;User Id=sa;Password=.Password1;TrustServerCertificate=True;"

ENV ConnectionStrings__Default="$CONNECTION_STRING"

ENTRYPOINT ["dotnet", "Customer.Host.dll"]