#FROM microsoft/aspnetcore:2.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.0 AS build
#WORKDIR /src
#COPY ["Lobster.Quartz/Lobster.Quartz.csproj", "Lobster.Quartz/"]
#RUN dotnet restore "Lobster.Quartz/Lobster.Quartz.csproj"
#COPY . .
#WORKDIR "/src/Lobster.Quartz"
#RUN dotnet build "Lobster.Quartz.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "Lobster.Quartz.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Lobster.Quartz.dll"]

FROM microsoft/dotnet:2.2-aspnetcore-runtime
ARG source
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
COPY ${source:-Lobster.Quartz/publish/} .
ENTRYPOINT ["dotnet", "Lobster.Quartz.dll"]