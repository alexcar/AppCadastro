# Aplicativo Cadastro Api
Aplicativo para realizar a manutenção de cadastro.

### Instrução de Instalação

### Objetivo
Preparar o backend para testar o aplicativo manutenção de cadastro.

### Clonando o reposrepositório
1. Abra o terminal e crie um diretório de nome "code" por exemplo.<br>
`mkdir code` 

2. A partir do diretório criado, realize o clone do projeto.<br>
`cd code`<br>
`git clone https://github.com/alexcar/AppCadastroApi.git`

### Preparando o projeto Backend

#### Banco de Dados Sql Server
1. Execute o SQL Server Management Studio e realize a autenticação com um usuário que tenha permissão para criar banco de dados.
2. Será necessário a execução dos arquivos abaixo no banco de dados. Analise os arquivos scripts e faça as devidas alterações caso seja necessário.
3. Execute no SQL Server Management Studio os seguintes arquivos: <br>
`code\AppCadastroApi\BancoDados\CriaBancoDados.sql` para criar o banco de dados da aplicação.<br>
`code\AppCadastroApi\BancoDados\CriaUsuarioBancoDados.sql` para criar o usuário do banco de dados.

#### Visual Studio 
1. Execute o Visual Studio 2019.
2. Abra o arquivo da solução no caminho:<br> 
`code\AppCadastroApi\AppCadastro.sln`
3. Realize a compilação da solução.
4. Abra a console Package Manager e selecione como default o projeto AppCadastro.Api.
5. Execute os seguintes comandos para que o Entity Framework Core possa criar as tabelas no banco de dados:<br>
`dotnet ef migrations add criacao_das_tabelas`<br>
`dotnet ef database update`
6. Execute no SQL Server Management Studio o seguinte arquivo script:<br>
`code\AppCadastroApi\BancoDados\PopulaTabelaSexo.sql` para povoar a tabela Sexo no banco de dados.
7. Escolha o projeto AppCadastro.Api para ser o projeto de execução se for necessário.
8. Em seguida, execute a aplicação em modo debugger pressionando F5.
9. O serviço Api deve ficar disponível no endereço:<br> 
`https://localhost:44391/api/usuario`

