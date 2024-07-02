FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/dotnet_consumer/
COPY ["dotnet_consumer/dotnet_consumer.csproj", "./"]
COPY ["dotnet_rabbitmq/dotnet_rabbitmq.csproj", "../dotnet_rabbitmq/"]
RUN dotnet restore "dotnet_consumer.csproj"
COPY ["dotnet_consumer/", "."]
COPY ["dotnet_rabbitmq/", "../dotnet_rabbitmq/"]
RUN dotnet publish "dotnet_consumer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dotnet_consumer.dll"]