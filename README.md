# Sistema de Controle de Gastos Residenciais

Sistema completo para controle de gastos residenciais desenvolvido com .NET (C#) no back-end e React com TypeScript no front-end.

## 📋 Estrutura do Projeto

```
controle-gastos-residenciais/
├── ControleGastos.API/          # Back-end (Web API .NET)
│   ├── Controllers/             # Controladores REST
│   ├── Data/                    # DbContext e configuração do banco
│   ├── Models/                  # Entidades e DTOs
│   ├── Services/                # Lógica de negócio
│   └── Program.cs               # Configuração da aplicação
└── [Front-end React será criado aqui]
```

## 🚀 Back-end - ControleGastos.API

### Tecnologias Utilizadas

- **.NET 10.0** - Framework principal
- **Entity Framework Core 10.0** - ORM para acesso a dados
- **SQLite** - Banco de dados (persistência após reiniciar o sistema)
- **Swagger/OpenAPI** - Documentação da API

### Pré-requisitos

- .NET SDK 10.0 ou superior
- Visual Studio Code, Visual Studio ou Rider (opcional)

### Como Executar a API

1. **Navegue até a pasta do projeto:**
   ```bash
   cd ControleGastos.API
   ```

2. **Restaure as dependências (se necessário):**
   ```bash
   dotnet restore
   ```

3. **Execute a aplicação:**
   ```bash
   dotnet run
   ```

4. **Acesse a documentação Swagger:**
   - Abra o navegador em: `https://localhost:5001/swagger` ou `http://localhost:5000/swagger`
   - A URL exata será exibida no console após iniciar a aplicação

### Endpoints da API

#### Pessoas (`/api/pessoas`)
- `GET /api/pessoas` - Lista todas as pessoas
- `GET /api/pessoas/{id}` - Busca pessoa por ID
- `POST /api/pessoas` - Cria uma nova pessoa
- `DELETE /api/pessoas/{id}` - Deleta uma pessoa (e todas suas transações em cascata)

#### Categorias (`/api/categorias`)
- `GET /api/categorias` - Lista todas as categorias
- `GET /api/categorias/{id}` - Busca categoria por ID
- `POST /api/categorias` - Cria uma nova categoria

#### Transações (`/api/transacoes`)
- `GET /api/transacoes` - Lista todas as transações
- `GET /api/transacoes/{id}` - Busca transação por ID
- `POST /api/transacoes` - Cria uma nova transação

#### Consultas (`/api/consultas`)
- `GET /api/consultas/totais-por-pessoa` - Totais financeiros agrupados por pessoa
- `GET /api/consultas/totais-por-categoria` - Totais financeiros agrupados por categoria (opcional)

### Regras de Negócio Implementadas

1. **Cadastro de Pessoas:**
   - Identificador único gerado automaticamente
   - Nome (texto obrigatório)
   - Idade (número inteiro positivo)

2. **Cadastro de Categorias:**
   - Identificador único gerado automaticamente
   - Descrição (texto obrigatório)
   - Finalidade: Despesa, Receita ou Ambas

3. **Cadastro de Transações:**
   - Identificador único gerado automaticamente
   - Descrição (texto obrigatório)
   - Valor (decimal positivo)
   - Tipo: Despesa ou Receita
   - **Regra especial:** Menores de 18 anos só podem criar despesas (não receitas)
   - A categoria deve ser compatível com o tipo da transação:
     - Se transação é Despesa → categoria deve ter finalidade "Despesa" ou "Ambas"
     - Se transação é Receita → categoria deve ter finalidade "Receita" ou "Ambas"

4. **Deleção em Cascata:**
   - Ao deletar uma pessoa, todas as suas transações são automaticamente deletadas

5. **Consultas de Totais:**
   - **Por Pessoa:** Lista todas as pessoas com total de receitas, despesas e saldo (receita - despesa)
   - **Por Categoria:** Lista todas as categorias com total de receitas, despesas e saldo (opcional)
   - Ambas incluem totais gerais consolidados no final

### Banco de Dados

O sistema utiliza **SQLite** como banco de dados, criando automaticamente o arquivo `controle-gastos.db` na primeira execução. Os dados persistem mesmo após reiniciar o sistema.

### CORS

A API está configurada para aceitar requisições do front-end React nas portas:
- `http://localhost:3000` (Create React App padrão)
- `http://localhost:5173` (Vite padrão)

## 📝 Documentação do Código

Todo o código está amplamente documentado com comentários XML explicando:
- Propósito de cada classe, método e propriedade
- Regras de negócio implementadas
- Validações aplicadas
- Comportamentos esperados

## 🎯 Próximos Passos

- [ ] Criar o front-end React com TypeScript
- [ ] Implementar interface para cadastro de pessoas
- [ ] Implementar interface para cadastro de categorias
- [ ] Implementar interface para cadastro de transações
- [ ] Implementar visualização de totais por pessoa
- [ ] Implementar visualização de totais por categoria

## 📄 Licença

Este projeto foi desenvolvido como teste técnico.
