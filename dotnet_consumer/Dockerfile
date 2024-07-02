FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["dotnet_consumer.csproj", "./"]
RUN dotnet restore "dotnet_consumer.csproj"
COPY . .
RUN dotnet publish "dotnet_consumer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dotnet_consumer.dll"]