#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 53864
#EXPOSE 44304
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY ["Lobster.Gateway/Lobster.Gateway.csproj", "Lobster.Gateway/"]
#RUN dotnet restore "Lobster.Gateway/Lobster.Gateway.csproj"
#COPY . .
#WORKDIR "/src/Lobster.Gateway"
#RUN dotnet build "Lobster.Gateway.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "Lobster.Gateway.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Lobster.Gateway.dll"]

FROM microsoft/dotnet:2.2-aspnetcore-runtime
ARG source
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
COPY ${source:-Lobster.Gateway/publish/} .
ENTRYPOINT ["dotnet", "Lobster.Gateway.dll"]