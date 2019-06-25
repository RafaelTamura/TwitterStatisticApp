# TwitterStatisticApp

Processo para executar o projeto:
1) Realizar o download e instalação do Node 6.4.1

2) Ir até a pasta TwitterStatisticApp\ClientApp

3) Instalar os pacotes do node no projeto pelo comando, via PowerShell: npm install

4) Instalar o MongoDB via Docker pelo comando:
   docker run -d -p 27017:27917 mongo
   
   ou
   
   Instalar o MongoDB para Windows pelo link: https://www.mongodb.com/download-center
   
5) Realizar o download e instalação do .NET Core 2.2 para a execução da
API

6) Ir até a pasta TwitterStatisticApp/TwitterStatisticApp.Identity

7) Executar o comando para execução da aplicação:
dotnet publish
dotnet run

8) Ir até a pasta TwitterStatisticApp/TwitterStatisticApp

9) Executar o comando para execução da aplicação:
dotnet publish
dotnet run
