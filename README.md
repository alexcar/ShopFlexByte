Modelagem do sistema de E-commerce ShopFlexByte utilizando Domain Driven Design (DDD).

## Descrição Geral
O projeto consiste na modelagem do sistema de E-commerce ShopFlexByte, aplicando os princípios de Domain Driven Design (DDD) para estruturar o domínio de forma clara, expressiva e alinhada às regras de negócio. 

## Problema a ser Resolvido
Sistemas de e-commerce frequentemente sofrem com:
- **Baixa clareza nas regras de negócio**, espalhadas por várias camadas.
- **Dificuldade de manutenção**, devido a modelos anêmicos e acoplamento excessivo.
- **Complexidade crescente**, especialmente em fluxos como carrinho, pedidos e pagamentos.
- **Falta de separação entre lógica de domínio e infraestrutura**, dificultando testes e evolução.

O desafio é criar uma modelagem que:
- Centralize regras de negócio no domínio.
- Separe responsabilidades de forma clara.
- Permita evolução sem comprometer partes já implementadas.
- Seja expressiva e reflita a linguagem do negócio (Ubiquitous Language).

DDD é a abordagem ideal para resolver esses problemas, pois organiza o sistema em torno do domínio e de suas invariantes.

## Escopo do Projeto ShopFlexByte

Por se tratar de um trabalho de pós graduação, está fora de escopo a modelagem de todos os processos de um e-commerce realista. Portanto, foi levado em consideração apenas a modelagem dos processos **Gestão de Clientes**, **Identidade e Acesso**, **Catálogo de Produtos**, **Carrinho de Compras**, **Pedidos e Pagamento (Externo)**.

## Justificativa da Abordagem DDD

O DDD foi escolhido porque:
- Permite modelar domínios complexos com clareza.
- Mantém regras de negócio centralizadas e protegidas.
- Reduz acoplamento entre camadas.
- Cria um modelo que conversa com especialistas do domínio.

O resultado é um sistema mais robusto, expressivo e preparado para crescer de forma ordenada.

Imagem da modelagem do domínio ShopFlexByte

![Imagem da modelagem do domínio ShopFlexByte](https://github.com/alexcar/ShopFlexByte/blob/main/docs/domain-model.png)

## Rubrica

1. Rubrica
	- Aplicar os conceitos de Orientação a Objetos com C# ![Common/Entity.cs | Common/ValueObject.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs)
	- Aplicar os conceitos de Orientação a Objetos com C# ![Rubrica A2](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs)
	- Aplicar os conceitos de Orientação a Objetos com C# ![Rubrica A3](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs)
	-  Aplicar os conceitos de Orientação a Objetos com C# ![Rubrica A4](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs)
2. Rubrica
	1. Modelar aplicações utilizando Domain-Driven Design
	2. aaaa
	3. aaaa
	4. aaaa
3. ffffff
	1. ddddd
	2. ddddd
	3. dddd
	4. ddd
4. aaaa
	1. aaa
	2. aaa
	3. aaa


   



