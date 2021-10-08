#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Chat/NATS.POC.Chat.Client/NATS.POC.Chat.Client.csproj", "Services/Chat/NATS.POC.Chat.Client/"]
RUN dotnet restore "Services/Chat/NATS.POC.Chat.Client/NATS.POC.Chat.Client.csproj"
COPY . .
WORKDIR "/src/Services/Chat/NATS.POC.Chat.Client"
RUN dotnet build "NATS.POC.Chat.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NATS.POC.Chat.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NATS.POC.Chat.Client.dll"]