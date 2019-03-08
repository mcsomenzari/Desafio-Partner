# Desafio-Partner
Desafio Partner C#

Arquitetura do sistema


1. Ferramentas utilizadas
Microsoft Visual Studio Community 2017
Microsoft SQL Server 2017 Express


2. Models

Além da criação das Models necessárias, foi criado um model para retorno de sucesso ou não incluindo uma mensagem com resultado das operações.


3. String de conexão

Armazenada no arquivo appsettings.json.


4. Utilização do banco de dados

4.1. Para a criação do banco de dados (tabelas e stored procedures) é necessário a execução do script SQL fornecido: DesfioPartner.sql.

4.2. Foi criada uma classe SqlHelper na pasta Util. Nesta classe, haverá vários métodos que serão necessários em todo o aplicativo, como um método para executar stored procedures para inserção, alteração e exclusão de registros e que retornam strings com resultados de operações), um método que retornará os dados (objeto/lista de um objeto) de acordo com a nossa necessidade e alguns métodos para obter os valores da coluna do SqlDataReader.
Foi criado um método genérico - ExecuteProcedureReturnData - que retorna dados do tipo (modelo) que estão sendo esperados. Há mais um parâmetro específico que é Func <SqlDataReader, TData> translator. Essa entrada direciona o reader como entrada e faz retornar o tipo de classe que estamos esperando. 
Foram criados um DbCliente para cada classe para conectar-se ao banco de dados e retornar os dados esperados.
Foi usado um factory client com carregamento "lazy" afim de melhorar a performance através de um gerenciamento de memória otimizado fornecido pelo ASP.Net Core, resumidamente.


5. Escopo da api

De acordo com a solicitação, a api criada registra informações de patrimônios com suas respectivas marcas, ou seja, para inserir um novo patrimônio é necessário que sua marca seja cadastrada anteriormente.

5.1 Principais características

- Nº do tombo e Id da marca são gerados automaticamente pelo sistema e não podem ser alterados;
- Não é permitida a existência de duas marcas com o mesmo nome. Ao incluir ou alterar o nome de uma marca para um nome existente, uma mensagem de erro é retornada;
- Não é permitido cadastrar patrimônio sem uma marca préviamente cadastrada. Ao realizar esta operação, uma mensagem de erro é retornada;
- Ao realizar a consulta de um patrimônio ou marca inexistentes, uma mensagem de erro é retornada;
- Caso seja informado algum campo obrigatório em branco, o sistema retorna uma mensagem sobre o erro; *************
- O sistema não trata campos não informados na estrutura de entrada dos dados tanto para inclusão quanto para alterações. Uma exceção informando a inexistência do campo será retornada. Deve-se seguir as orientações para utilização dos endpoints da api.


6. Endpoints

Patrimônio

Campos:
•	Nome - obrigatório
•	MarcaId - obrigatório
•	Descrição
•	Nº do tombo

Endpoints:
•	GET patrimonios - Obter todos os patrimônios
•	GET patrimonios/{id} - Obter um patrimônio por ID
•	POST patrimonios - Inserir um novo patrimônio
•	PUT patrimonios/{id} - Alterar os dados de um patrimônio
•	DELETE patrimonios/{id} - Excluir um patrimônio

Regras:
•	O nº do tombo deve ser gerado automaticamente pelo sistema, e não pode ser alterado pelos usuários.


Marca

Campos:
•	Nome – obrigatório
•	MarcaId - obrigatório
Endpoints:
•	GET marcas - Obter todas as marcas
•	GET marcas/{id} - Obter uma marca por ID
•	GET marcas/patrimônios/{id} – Obter todos os patrimônios de uma marca *************
•	POST marcas - Inserir uma nova marca
•	PUT marcas/{id} - Alterar os dados de uma marca
•	DELETE marcas/{id} - Excluir uma marca
Regras:
•	Não deve permitir a existência de duas marcas com o mesmo nome.







