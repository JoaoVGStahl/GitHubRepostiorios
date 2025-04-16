GitHub Repositórios – Projeto Técnico

Este projeto foi desenvolvido como parte de um desafio técnico, com o objetivo de consumir a API pública do GitHub, realizar buscas de repositórios, gerenciar favoritos em memória e aplicar uma ordenação por relevância com base em critérios definidos.

📦 Tecnologias utilizadas

- Backend: .NET Core 8
- Frontend: Angular 17
- Testes: xUnit, Moq, FluentAssertions (Backend)
- Arquitetura: Clean Architecture (adaptada para ambos)

🚀 Como executar o projeto

🔹 Backend (.NET Core)

1. Acesse a pasta do backend:
   cd RepositoriosGitHub

2. Restaure as dependências:
   dotnet restore

3. Execute o projeto:
   dotnet run

4. Acesse a documentação Swagger:
   https://localhost:5001/swagger

🔹 Frontend (Angular)

1. Acesse a pasta do frontend:
   cd repositorio-web-angular

2. Instale as dependências:
   npm install

3. Execute o projeto:
   ng serve

4. Acesse no navegador:
   http://localhost:4200/home

Certifique-se de que o backend esteja rodando antes de utilizar o frontend.

✍️ Decisões e boas práticas aplicadas

✅ Backend

- Estrutura separada em camadas (Domain, Application, Infrastructure, WebApi)
- HttpClient encapsulado em IGitHubClient, com tratamento de erros via HttpRequestException
- Middleware global para tratamento de exceções e mensagens padronizadas
- Mapeamento manual entre entidades e DTOs via RepositoryMapper
- Lógica de cálculo de relevância extraída para IRelevanciaService
- Favoritos armazenados em memória (Singleton) com injeção de dependência
- Testes unitários com ampla cobertura

✅ Frontend

- Estrutura baseada em Clean Architecture:
- Separação clara entre UI, lógica de aplicação e acesso à API
- Tratamento de erros com catchError e fallback para respostas vazias

📌 Funcionalidades implementadas

- Buscar repositórios por nome
- Favoritar e desfavoritar repositórios (armazenados em memória)
- Listar repositórios ordenados por relevância (estrelas, forks e watchers)
- Swagger UI para documentação dos endpoints
- Testes unitários de todos os serviços, incluindo lógica de relevância

✅ Como foi desenvolver esse projeto

Gostei bastante de realizar esse desafio. Foi uma oportunidade de aplicar boas práticas de arquitetura, organização de camadas, injeção de dependência e testes, tanto no backend quanto no frontend. Além disso, consegui manter o código limpo, desacoplado e testável.
