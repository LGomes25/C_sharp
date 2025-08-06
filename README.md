# 🛠️ Sistema de Controle de Estoque

Este projeto é um sistema completo de controle de estoque com backend em .NET 9 (C#) utilizando Dapper e PostgreSQL, e frontend em React com Vite. Inclui autenticação, CRUD de usuários e produtos, decremento de estoque, pesquisa e exclusão por ID ou Número de Parte.

-------------------------------------------------------------------------------------------

### Backend (.NET 9 / C#)
- ASP.NET Core Web API
- Dapper (ORM)
- PostgreSQL
- xUnit + Moq (Testes unitários)
- Swagger (Documentação)
- Filtros globais para tratamento de exceções

## ⚙️ Funcionalidades da API

- Autenticação por email e senha (sem JWT)
- Cadastro, atualização, consulta e exclusão de usuários
- Cadastro, edição, pesquisa e exclusão de produtos
- Decremento de estoque com mensagens inteligentes
- Validações personalizadas com exceptions específicas
- Filtro global de erros para evitar `try/catch` em controllers
- Testes automatizados cobrindo casos positivos

## 📁 Documentação

Acesse o Swagger em: `https://localhost:7012/swagger`

## 🧪 Testes

Os testes unitarios cobrem criação, busca, atualização e exclusão de produtos e usuários, além do login.

## 📁 Estrutura do Projeto

ControleEstoque/
├── Api/
│   └── Controllers, Filters
├── Application/
│   └── DTOs, Services, Mappers, Exceptions
├── Domain/
│   └── Entities
├── Infrastructure/
│   └── Repositories, Context (Dapper)
├── Tests/
│   └── Unitários com xUnit e Moq
```

-------------------------------------------------------------------------------------------

## 👨‍💻 Autor

Projeto desenvolvido por **[Leonardo Gomes]** durante estudos e práticas com ASP.NET Core.

-------------------------------------------------------------------------------------------
