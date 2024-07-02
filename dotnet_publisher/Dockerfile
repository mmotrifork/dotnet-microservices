FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/dotnet_publisher/
COPY ["dotnet_publisher/dotnet_publisher.csproj", "./"]
COPY ["dotnet_rabbitmq/dotnet_rabbitmq.csproj", "../dotnet_rabbitmq/"]
RUN dotnet restore "dotnet_publisher.csproj"
COPY ["dotnet_publisher/", "."]
COPY ["dotnet_rabbitmq/", "../dotnet_rabbitmq/"]
RUN dotnet publish "dotnet_publisher.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dotnet_publisher.dll"]