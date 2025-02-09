📚 API para Estudos do .NET Core
Uma API desenvolvida para aprendizado e prática com .NET Core, aplicando boas práticas e conceitos essenciais para desenvolvimento robusto e escalável.

🚀 Tecnologias e Recursos
✅ Banco de Dados: PostgreSQL com Entity Framework Core
✅ Autenticação: JWT Token
✅ Documentação: Swagger integrado
✅ Arquitetura: Clean Architecture
✅ Boas Práticas: Injeção de dependências, manipulação de CORS, tratamento de erros para diferentes ambientes
✅ Versionamento: API versioning implementado
✅ Logs: Monitoramento de logs na aplicação
✅ Migrations: Controle e versionamento do banco de dados

📦 Pacotes Utilizados
- AutoMapper
- AutoMapper.Extensions.Microsoft.DependencyInjection
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Mvc.Versioning
- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
- Microsoft.AspNetCore.OpenApi
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.IdentityModel.Tokens
- Npgsql.EntityFrameworkCore.PostgreSQL
- Swashbuckle.AspNetCore
- Swashbuckle.AspNetCore.Swagger
- System.IdentityModel.Tokens.Jwt


🛠 Como Rodar
Clone o repositório

Instale as dependências
- dotnet restore

Configure o banco de dados em Infra, ConnectionDbContext

Execute as migrations
dotnet ef database update

Inicie a API
dotnet run
🔥 Agora a API estará rodando em: https://localhost:Porta

