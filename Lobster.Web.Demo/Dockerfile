#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY ["Lobster.Web.Demo/Lobster.Web.Demo.csproj", "Lobster.Web.Demo/"]
#RUN dotnet restore "Lobster.Web.Demo/Lobster.Web.Demo.csproj"
#COPY . .
#WORKDIR "/src/Lobster.Web.Demo"
#RUN dotnet build "Lobster.Web.Demo.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "Lobster.Web.Demo.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Lobster.Web.Demo.dll"]

FROM microsoft/dotnet:2.2-aspnetcore-runtime
ARG source
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
COPY ${source:-Lobster.Web.Demo/publish/} .
ENTRYPOINT ["dotnet", "Lobster.Web.Demo.dll"]