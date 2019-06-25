# TwitterStatisticApp

Configurações:
- Front-end: Angular 6
- Node: 6.4.1
- .NET Core 2.2
- MongoDB
- IdentityServer4

Processo para executar o projeto:
1) Realizar o download e instalação do Node 6.4.1

2) Ir até a pasta TwitterStatisticApp\ClientApp

3) Instalar os pacotes NPMs no projeto pelo comando, via PowerShell: npm install

4) Instalar o MongoDB via Docker pelo comando:
   docker run -d -p 27017:27017 mongo
   
   ou
   
   Instalar o MongoDB para Windows pelo link: https://www.mongodb.com/download-center
   
5) Realizar o download e instalação do .NET Core 2.2 para a execução da
API

6) Ir até a pasta TwitterStatisticApp/TwitterStatisticApp.Identity

7) Executar os comandos para execução da aplicação de autenticação, via PowerShell:

- dotnet publish

- dotnet run

8) Ir até a pasta TwitterStatisticApp/TwitterStatisticApp

9) Executar os comandos para execução da aplicação do Twitter Statistic App, via PowerShell:

- dotnet publish

- dotnet run
