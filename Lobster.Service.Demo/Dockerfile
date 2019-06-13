#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY ["Lobster.Service.Demo/Lobster.Service.Demo.csproj", "Lobster.Service.Demo/"]
#RUN dotnet restore "Lobster.Service.Demo/Lobster.Service.Demo.csproj"
#COPY . .
#WORKDIR "/src/Lobster.Service.Demo"
#RUN dotnet build "Lobster.Service.Demo.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "Lobster.Service.Demo.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Lobster.Service.Demo.dll"]

FROM microsoft/dotnet:2.2-aspnetcore-runtime
ARG source
WORKDIR /app
COPY . .
EXPOSE 80
EXPOSE 443
COPY ${source:-publish/} .
ENTRYPOINT ["dotnet", "Lobster.Service.Demo.dll"]