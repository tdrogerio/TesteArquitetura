# TesteArquitetura - Modelo de Apresentação

Projeto modelo para criação do Teste de Arquitetura com ASP.NET Core 6 com o uso do conceito DDD.

O objetivo deste projeto é implementar as tecnologias usadas mais comuns apresentando a melhor maneira de desenvolver grandes aplicação com DotNet.

## Como usar?

- Você precisará do mais recence Visual Studio 2022 e do mais recente .Net Core SDK;
- Verifique se você instalou a mesma versão mais recente com o comando dotnet --version no promp de comando.
- Os mais recentes SDK e ferramentas podem ser baixados a partir de [https://dot.net/core](https://dot.net/core).

É possível executar o Projeto TesteArquitetura no Visual Studio Code (Windows, Linux ou MacOS).

Para saber mais sobre como configurar seu ambiente, visite o [Guia de Downloads da Microsoft DotNET](https://dotnet.microsoft.com/en-us/download).

## Comandos Visual Studio

- A conexão com o banco de dados de ser feita na camada WebApi no arquivo appsetting.json;
- Com a configuração realizada, no Console do Gerenciador de Pacotes do Visual Studio 2022, escolha o projeto padrão 
**Services\Documentos\TesteArquitetura.Documentos.Data e digite os comandos abaixo:

```
Add-Migration InitialCreate -Context DocumentosContext

Add-Migration InitialCreate -Context ApplicationDbContext
```
O EF Core criará um diretório chamado Migrações em seu projeto e gerará alguns arquivos. É uma boa ideia inspecionar o que foi gerado exatamente no EF Core e, 
possivelmente, corrigi-lo, mas vamos ignorar isso por enquanto.

```
Update-Database -Context DocumentosContext

Update-Database -Context ApplicationDbContext
```
Neste ponto, você pode fazer com que o EF crie seu banco de dados e seu esquema a partir da migração. Isso pode ser feito por meio do seguinte:

## Comandos CLI do .Net Core

- A conexão com o banco de dados de ser feita na camada WebApi no arquivo appsetting.json;
- Com a configuração realizada, no Console do Gerenciador de Pacotes do Visual Studio 2022, escolha o projeto padrão 
**Services\Documentos\TesteArquitetura.Documentos.Data e digite os comandos abaixo:

```
dotnet ef migrations add InitialCreate
```
O EF Core criará um diretório chamado Migrações em seu projeto e gerará alguns arquivos. É uma boa ideia inspecionar o que foi gerado exatamente no EF Core e, 
possivelmente, corrigi-lo, mas vamos ignorar isso por enquanto.

```
dotnet ef database update
```
Neste ponto, você pode fazer com que o EF crie seu banco de dados e seu esquema a partir da migração. Isso pode ser feito por meio do seguinte:

## Tecnologias implementadas:

- ASP.NET Core 6.0;
- ASP.NET WebApi Core;
- ASP.NET WebApi Core com JWT Bearer Authentication;
- ASP.NET Identity Core;
- Entity Framework Core 6.0
- AutoMapper;
- DataAnnotations;
- FluentValidator;
- Swagger UI com suporte JWT.

## Arquitetura

- Arquitetura completa com preocupações de separação de responsabilidade, CÓDIGO SÓLIDO e Limpo;
- Domain Driven Design (Camadas e padrão de modelo de domínio).
- Criação do modelo de banco de dados (tabelas para persistência de dados com schemas).
