# Projeto Exemplo ASP.NET

Este projeto é uma aplicação web desenvolvida com ASP.NET Core, utilizando o Entity Framework Core para persistência de dados com um banco de dados PostgreSQL. A aplicação permite o gerenciamento de processos, com funcionalidades de cadastro, edição, exclusão e listagem.

## Requisitos

Antes de rodar o projeto, verifique se você possui os seguintes requisitos instalados:

- **Visual Studio 2022** (ou superior)
- **PostgreSQL** 
- **Git** (para clonar o repositório)

## Configuração do Banco de Dados

1. Certifique-se de que o PostgreSQL está instalado e rodando na sua máquina.
2. Crie um banco de dados com o nome `projeto-exemplo-aspnet`.
3. Configure as credenciais do banco:
   - **Username:** `postgres`
   - **Senha:** `1234`

## Clonando o Repositório

Clone o repositório em sua máquina local:

```bash
git clone https://github.com/MarcilioKaua/ProjetoExemplo.git
```

## Configuração do Projeto

1. Abra o projeto no Visual Studio 2022.
2. Configure a string de conexão no arquivo `appsettings.json`. Ela já está configurada por padrão como:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=projeto-exemplo-aspnet;Username=postgres;Password=1234"
}
```

Se necessário, ajuste conforme suas configurações locais.

3. Execute o seguinte comando no **Package Manager Console** para aplicar as migrações ao banco de dados:

```powershell
Update-Database
```

## Rodando o Projeto

1. No Visual Studio, configure o projeto para rodar no modo **HTTPS** ou **HTTP**.
2. Clique no botão **Run**.
3. Acesse a aplicação no navegador através da URL:

```
https://localhost:7203/
```

## Testando o Projeto

1. Navegue pela aplicação para testar as funcionalidades de:
   - Listagem de processos
   - Cadastro de novos processos
   - Edição de processos existentes
   - Exclusão de processos

2. Para testar a integração com o banco de dados, certifique-se de que as ações realizadas na interface estão refletidas corretamente no banco.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework principal para a aplicação.
- **Entity Framework Core**: ORM para gerenciar o banco de dados PostgreSQL.
- **PostgreSQL**: Banco de dados relacional para persistência.
- **Bootstrap**: Para estilização e criação de componentes visuais (modais, toasts, etc.).
- **JavaScript**: Para validações e manipulação do DOM.
- **jQuery**: Auxiliar no carregamento dinâmico de dados e interações.
- **HTML/CSS**: Para a estrutura e estilização das páginas.

