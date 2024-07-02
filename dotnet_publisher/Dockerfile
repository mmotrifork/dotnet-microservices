FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["dotnet_publisher.csproj", "./"]
RUN dotnet restore "dotnet_publisher.csproj"
COPY . .
RUN dotnet publish "dotnet_publisher.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dotnet_publisher.dll"]