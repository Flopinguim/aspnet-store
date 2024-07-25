### README

# aspnet_store

Este projeto é uma aplicação ASP.NET Core MVC para gerenciamento de ordens de compra, entrada e saída de produtos.

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configuração do Banco de Dados

### Passo 1: Clonar o Repositório

Clone o repositório para a sua máquina local:

```bash
git clone https://github.com/seu-usuario/aspnet_store.git
cd aspnet_store
```

### Passo 2: Configurar a String de Conexão

Abra o arquivo `appsettings.json` e configure a string de conexão para o seu ambiente de banco de dados SQL Server.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StoreAspNet;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

- **Server:** O nome do servidor SQL. Pode ser `(localdb)\\mssqllocaldb` para uma instância local.
- **Database:** O nome do banco de dados que será criado, por exemplo, `StoreAspNet`.
- **Trusted_Connection:** Define se a conexão usa autenticação do Windows.
- **MultipleActiveResultSets:** Permite a execução de várias operações simultâneas no banco de dados.

### Passo 3: Aplicar as Migrações

Para configurar o banco de dados com as migrações já prontas, siga os passos abaixo:

1. **Restaurar Dependências:**

    No terminal, dentro do diretório do projeto, execute o comando para restaurar as dependências do projeto:

    ```bash
    dotnet restore
    ```

2. **Aplicar as Migrações:**

    Após restaurar as dependências, aplique as migrações para criar o banco de dados e as tabelas:

    ```bash
    dotnet ef database update
    ```

## Executar a Aplicação

Para iniciar a aplicação, execute o comando:

```bash
dotnet run
```

A aplicação estará disponível no endereço `https://localhost:5001` ou `http://localhost:5000`.

## Estrutura do Projeto

- **Controllers:** Contém os controladores que gerenciam as requisições HTTP.
- **Models:** Contém as entidades e os view models.
- **Views:** Contém as páginas Razor para exibição de dados e interação do usuário.

## Funcionalidades Principais

- **Gerenciamento de Fornecedores:** Adicionar, editar e remover fornecedores.
- **Gerenciamento de Produtos e Serviços:** Adicionar, editar e remover produtos e serviços.
- **Gerenciamento de Pedidos:** Criar pedidos de produtos e serviços.
- **Gerenciamento de Ordens de Compra:** Associar pedidos a ordens de compra.
- **Entrada e Saída de Produtos:** Registrar entrada e saída de produtos no estoque.

---

Este é um guia básico para configurar e executar a aplicação. Certifique-se de ajustar as configurações conforme necessário para o seu ambiente de desenvolvimento.
