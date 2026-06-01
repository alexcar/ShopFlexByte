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


| Rubrica                                                                                                                                                                                      | Arquivos           |
| :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------------------ |
| A1. Aplicar os conceitos de Orientação a Objetos com C# <br> O aluno implementou as classes aplicando os conceitos básicos de OO como Encapsulamento, Abstração, Herança e Polimorfismo? | ![Entity.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs) <br> ![ValueObject.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/ValueObject.cs) |
| B1. Aplicar os conceitos de Orientação a Objetos com C# <br> O aluno implementou as classes e objetos em C#, aplicando corretamente modificadores de acesso, propriedades, métodos e construtores? | ![Entity.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs) |
| C1. Aplicar os conceitos de Orientação a Objetos com C# <br> O aluno aplicou herança e polimorfismo em C# para criar hierarquias de classes flexíveis e extensíveis? | ![Entity.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Entity.cs) <br> ![Result.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Result.cs) |
| D1. Aplicar os conceitos de Orientação a Objetos com C# <br> O aluno aplicou abstração e encapsulamento em C# para ocultar detalhes de implementação e expor interfaces claras e concisas? | ![Result.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Common/Result.cs) <br> ![Email.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/ValueObjects/Email.cs) |
| E2. Modelar aplicações utilizando Domain-Driven Design <br> O aluno modelou o domínio utilizando Ubiquitous Language, Entities, Value Objects e Repositories de forma coerente com os conceitos de DDD? | ![Customer.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Entities/Customer.cs) <br> ![Order.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Entities/Order.cs) |
| F2. Modelar aplicações utilizando Domain-Driven Design <br> O aluno modelou o domínio utilizando Aggregate, Bounded Contexts e Domain Services de maneira estruturada e adequada ao problema? | ![ShoppingCart.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Entities/ShoppingCart.cs) <br> ![Order.cs](https://github.com/alexcar/ShopFlexByte/blob/main/src/ShopFlexByte.Domain/Entities/Order.cs) |
| G2. Modelar aplicações utilizando Domain-Driven Design <br> O aluno diferenciou claramente Domain Services e Factories na modelagem do domínio, justificando sua escolha com base na responsabilidade de cada elemento? | Conteúdo A <br> A2 |
| H2. Modelar aplicações utilizando Domain-Driven Design <br> O aluno modelou o domínio considerando a integração entre Bounded Contexts, aplicando padrões como Anti-Corruption Layer e Context Map com clareza e eficácia? | Conteúdo A <br> A2 |
| I3. Criar aplicações empregando padrões de projeto - SOLID e GRASP <br> O aluno aplicou os princípios SOLID no design das classes, garantindo coesão, alta responsabilidade e baixo acoplamento? | Conteúdo A <br> A2 |
| J3. Criar aplicações empregando padrões de projeto - SOLID e GRASP <br> O aluno utilizou corretamente o princípio de Single Responsibility nas classes, evitando a concentração excessiva de responsabilidades? | Conteúdo A <br> A2 |
| K3. Criar aplicações empregando padrões de projeto - SOLID e GRASP <br> O aluno aplicou o padrão Low Coupling para garantir a independência entre as classes e promover a reutilização de código? | Conteúdo A <br> A2 |
| L3. Criar aplicações empregando padrões de projeto - SOLID e GRASP <br> O aluno utilizou o padrão Controller de forma adequada, promovendo a separação entre lógica de controle e demais responsabilidades? | Conteúdo A <br> A2 |
| M4. Desenvolver testes unitários e aplicar TDD <br> O aluno aplicou corretamente os princípios de testes unitários como isolamento, repetibilidade, rapidez, auto-verificação e abrangência? | Conteúdo A <br> A2 |
| N4. Desenvolver testes unitários e aplicar TDD <br> O aluno implementou testes unitários abrangendo todos os métodos que contêm regras de negócio relevantes? | Conteúdo A <br> A2 |
| O4. Desenvolver testes unitários e aplicar TDD <br> O aluno utilizou mocks e stubs de maneira adequada para isolar o código sob teste durante a implementação dos testes unitários? | Conteúdo A <br> A2 |
| P4. Desenvolver testes unitários e aplicar TDD <br> O aluno implementou testes unitários com cobertura superior a 80% do código de domínio, garantindo qualidade e confiabilidade da aplicação? | Conteúdo A <br> A2 |

   



