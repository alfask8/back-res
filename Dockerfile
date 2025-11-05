
#Fase de compilacion

#Utilizamos la imagen oficial de .NET SDK como base para compilar la aplicacion

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 

# Directorio de trabajo

WORKDIR /app  

#Copiamos TODOS los archivos de proyecto al app del contenedor

COPY . .                

# Restauramos las dependencias del proyecto
RUN dotnet restore

# Compilamos la aplicacion en modo Release
RUN dotnet publish -c Release -o out


#Fase de ejecucion
#Utilizamos la imagen oficial de .NET Runtime como base para ejecutar la aplicacion
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Directorio de trabajo
WORKDIR /app

# Copiamos los archivos compilados desde la fase de compilacion
COPY --from=build /app/out .

# Exponemos el puerto 80 para el trafico HTTP   
EXPOSE 80

# Comando para ejecutar la aplicacion
ENTRYPOINT [ "dotnet","Reservas.API.dll" ]


