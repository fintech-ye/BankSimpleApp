FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR /BankSimpleApp

COPY ["BankSimpleApp.csproj", "."]
# RUN dotnet restore
RUN dotnet restore "BankSimpleApp.csproj"
COPY . .

# Build and publish a release
RUN dotnet build "BankSimpleApp.csproj" -c Release -o out
RUN dotnet publish "BankSimpleApp.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /BankSimpleApp
COPY --from=build-env /BankSimpleApp/out .

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

EXPOSE 80

ENTRYPOINT ["dotnet", "BankSimpleApp.dll"]