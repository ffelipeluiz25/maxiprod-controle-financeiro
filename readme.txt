==================================================================================================================
									PROJETO REACT + VITE COM INTEGRAÇÃO DE APIS C#
==================================================================================================================
--------------------------------
		Sobre o Projeto
--------------------------------

	Este projeto consiste em uma aplicação frontend desenvolvida com React + Vite, integrada a duas APIs distintas desenvolvidas em C#, cada uma seguindo um padrão arquitetural diferente.

	API 1: Arquitetura baseada em CQRS (Command Query Responsibility Segregation)
	API 2: Arquitetura baseada em Service/Repository

	O objetivo é permitir a alternância entre as APIs de forma simples pelo frontend sem nenhuma outra alteração necessária.

--------------------------------
		Arquitetura das APIs
--------------------------------

	CQRS:
	A API baseada em CQRS separa responsabilidades entre leitura e escrita.

	Commands: operações que alteram estado
	Queries: operações de leitura

	Service/Repository:
	A outra API segue um padrão mais tradicional:

	Repository: responsável pelo acesso a dados
	Service: responsável pelas regras de negócio

--------------------------------
	Frontend (React + Vite)
--------------------------------

	1- Instalação de dependências:

		npm install
		npm install axios
		npm install sweetalert2

	2- Execução do projeto:

		npm run dev

	3- Configuração da API no Frontend

		A comunicação com as APIs é feita utilizando Axios.

	4- Para alterar qual API será consumida, basta editar o arquivo:

		src/services/api.ts

	5- E modificar a propriedade baseURL:

		import axios from "axios";

		export const api = axios.create({
		baseURL: "https://localhost:44300"
		ou 
		baseURL: "https://localhost:44308"
		});

	Alterando essa URL, o frontend passará a consumir automaticamente a API selecionada.

----------------------------------------------------------------
			Uso de Inteligência Artificial
----------------------------------------------------------------

	O layout da aplicação foi gerado com auxílio de Inteligência Artificial.

	Importante:

	A utilização de IA foi exclusivamente para o layout/design
	Toda a lógica de negócio, integração com APIs e estrutura do projeto foram desenvolvidas manualmente

--------------------------------
		Tecnologias Utilizadas
--------------------------------

		Frontend:

		React
		Vite
		Axios
		SweetAlert2

		Backend:

		C#
		ASP.NET
		CQRS
		Service/Repository

--------------------------------
		Observações
--------------------------------

	Certifique-se de que as APIs estejam rodando antes de iniciar o frontend
	Verifique as URLs e portas configuradas no arquivo api.ts
	Cada API pode possuir endpoints e comportamentos diferentes
	
	ATENÇÃO: Rodar o script do banco de dados para construção da estrutura, criado com MySQL