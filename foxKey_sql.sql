CREATE DATABASE fox_key;
USE fox_key;

#drop database fox_key;

CREATE TABLE usuario(
    id_usuario INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    senha VARCHAR(255) NOT NULL,
    perfil_acesso ENUM('ADMIN','GERENTE','VENDEDOR','CAIXA') NOT NULL
);

CREATE TABLE login_exclusivo (
    id_login INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_ativacao DATE NOT NULL,
    id_usuario_fk INT NOT NULL,
    FOREIGN KEY (id_usuario_fk) REFERENCES usuario(id_usuario)
);

CREATE TABLE permissao (
    id_permissao INT PRIMARY KEY AUTO_INCREMENT,
    nome_modulo VARCHAR(100),
    pode_visualizar BOOLEAN,
    pode_criar BOOLEAN,
    pode_editar BOOLEAN,
    pode_excluir BOOLEAN,
    id_usuario_fk INT,
    FOREIGN KEY (id_usuario_fk) REFERENCES usuario(id_usuario)
);

CREATE TABLE despesa (
    id_despesa INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    tipo_despesa ENUM('COMERCIAL','ADMINISTRATIVA','COMPRA') NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    data_gerada DATE NOT NULL,
    descricao VARCHAR(255),
    id_login_fk INT,
    FOREIGN KEY (id_login_fk) REFERENCES login_exclusivo(id_login)
);

CREATE TABLE endereco_contato (
    id_endereco_contato INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    rua VARCHAR(100) NOT NULL,
    numero VARCHAR(100) NOT NULL,
    bairro VARCHAR(100) NOT NULL,
    complemento VARCHAR(100),
    referencia VARCHAR(255),
    cep VARCHAR(20) NOT NULL,
    estado VARCHAR(100) NOT NULL,
    cidade VARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL,
    celular VARCHAR(18) NOT NULL
);

CREATE TABLE fornecedor_pf (
    id_fornecedor_pf INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(150) NOT NULL,
    sobrenome VARCHAR(100) NOT NULL,
    cpf VARCHAR (14) NOT NULL,
    data_nascimento DATE NOT NULL,
    rg VARCHAR(10) NOT NULL,
    sexo ENUM ('MASCULINO','FEMININO','OUTRO') NOT NULL,
    estado_civil ENUM('SOLTEIRO','CASADO','SEPARADO','DIVORCIADO','VIUVO') NOT NULL,
    orgao_expedidor VARCHAR(100) NOT NULL,
    nacionalidade VARCHAR(50) NOT NULL,
    raca ENUM('BRANCA','PARDA','PRETA','INDÍGENA','AMARELA') NOT NULL,
    id_endereco_contato_fk INT NOT NULL,
    FOREIGN KEY (id_endereco_contato_fk) REFERENCES endereco_contato(id_endereco_contato)
);

CREATE TABLE fornecedor_pj (
    id_fornecedor_pj INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_fantasia VARCHAR(100) NOT NULL,
    razao_social VARCHAR(100),
    inscricao_municipal VARCHAR(16),
    cnpj VARCHAR(16) NOT NULL,
    data_abertura DATE,
    representante VARCHAR(150) NOT NULL,
    id_endereco_contato_fk INT NOT NULL,
    FOREIGN KEY (id_endereco_contato_fk) REFERENCES endereco_contato(id_endereco_contato)
);

CREATE TABLE cliente_pf (
    id_cliente_pf INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(150) NOT NULL,
    sobrenome VARCHAR(100) NOT NULL,
    data_nascimento DATE NOT NULL,
    cpf VARCHAR(14) UNIQUE NOT NULL,
    rg VARCHAR(14) NOT NULL,
    sexo ENUM ('MASCULINO','FEMININO','OUTRO') NOT NULL,
    id_endereco_contato_fk INT NOT NULL,
    FOREIGN KEY (id_endereco_contato_fk) REFERENCES endereco_contato(id_endereco_contato)
);

CREATE TABLE cliente_pj (
    id_cliente_pj INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_fantasia VARCHAR(150) NOT NULL,
    razao_social VARCHAR(100),
    inscricao_municipal VARCHAR(16),
    cnpj VARCHAR(16) NOT NULL,
    data_abertura DATE NOT NULL,
    representante VARCHAR(150) NOT NULL,
    id_endereco_contato_fk INT NOT NULL,
    FOREIGN KEY (id_endereco_contato_fk) REFERENCES endereco_contato(id_endereco_contato)
);

CREATE TABLE funcionario (
    id_funcionario INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(150) NOT NULL,
    sobrenome VARCHAR(150) NOT NULL,
    cpf VARCHAR(14) NOT NULL,
    rg VARCHAR(10) NOT NULL,
    orgao_expedidor VARCHAR(100) NOT NULL,
    nacionalidade VARCHAR(50) NOT NULL,
    numero_ctps VARCHAR(20) NOT NULL,
    numero_pis VARCHAR(20) NOT NULL,
    raca ENUM('BRANCA','PARDA','PRETA','INDÍGENA','AMARELA') NOT NULL,
    sexo ENUM ('MASCULINO','FEMININO','OUTRO') NOT NULL,
    estado_civil ENUM('SOLTEIRO','CASADO','SEPARADO','DIVORCIADO','VIUVO') NOT NULL,
    cargo VARCHAR(150),
    grau_instrucao ENUM('ENSINO FUNDAMENTAL','ENSINO MÉDIO','ENSINO SUPERIOR','PÓS-GRADUAÇÃO'),
    data_nascimento DATE,
    id_endereco_contato_fk INT NOT NULL,
    FOREIGN KEY (id_endereco_contato_fk) REFERENCES endereco_contato(id_endereco_contato)
);

CREATE TABLE categoria (
    id_categoria INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(150) NOT NULL,
    descricao VARCHAR(150),
    prioridade_reposicao INT NOT NULL,
    data_registro DATE NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE
);


CREATE TABLE produto (
    id_produto INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    codigo_barra VARCHAR(100),
    unidade_medida ENUM('un','cx','kg','g','t','l','ml','m'),
    preco_custo DECIMAL(10,2),
    preco_venda DECIMAL(10,2),
    ativo BOOLEAN DEFAULT TRUE,
    data_cadastro DATE,
    nome VARCHAR(150),
    descricao VARCHAR(150),
    id_categoria_fk INT NOT NULL,
    id_fornecedor_pf_fk INT,
    id_fornecedor_pj_fk INT,
    FOREIGN KEY (id_categoria_fk) REFERENCES categoria(id_categoria),
    FOREIGN KEY (id_fornecedor_pf_fk) REFERENCES fornecedor_pf(id_fornecedor_pf),
    FOREIGN KEY (id_fornecedor_pj_fk) REFERENCES fornecedor_pj(id_fornecedor_pj),
        CHECK (
  (id_fornecedor_pf_fk IS NOT NULL AND id_fornecedor_pj_fk IS NULL) OR
  (id_fornecedor_pf_fk IS NULL AND id_fornecedor_pj_fk IS NOT NULL)
)
    
);



CREATE TABLE estoque (
    id_estoque INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    qtd_atual DECIMAL(10,2) NOT NULL,
    qtd_reservada DECIMAL(10,2),
    qtd_minima DECIMAL(10,2),
    status_estoque ENUM(
		'DISPONIVEL','RESERVADO','EM_TRANSPORTE',
        'BLOQUEADO','DEVOLUCAO','QUARENTENA') NOT NULL,
    observacao VARCHAR(255),
    id_produto_fk INT NOT NULL,
    FOREIGN KEY (id_produto_fk) REFERENCES produto(id_produto)
);


CREATE TABLE movimentacao_caixa (
    id_movimentacao_caixa INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    tipo ENUM('ENTRADA', 'SAIDA'),
    valor DECIMAL(10,2),
    descricao VARCHAR(100),
    data_gerada DATE,
    hora TIME
);

CREATE TABLE caixa (
    id_caixa INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_abertura DATE NOT NULL,
    data_fechamento DATE,
    saldo_inicial DECIMAL(10,2) NOT NULL,
    saldo_final DECIMAL(10,2),
    total_entrada DECIMAL(10,2),
    total_saida DECIMAL(10,2),
    id_funcionario_fk INT NOT NULL,
    id_login_fk INT NOT NULL,
    id_movimentacao_fk INT,
    FOREIGN KEY (id_funcionario_fk) REFERENCES funcionario(id_funcionario),
    FOREIGN KEY (id_login_fk) REFERENCES login_exclusivo(id_login),
    FOREIGN KEY (id_movimentacao_fk) REFERENCES movimentacao_caixa(id_movimentacao_caixa)
);

CREATE TABLE relatorio_venda (
    id_relatorio_venda INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_cadastro DATE NOT NULL,
    total_vendas DECIMAL(10,2) NOT NULL,
    total_recibo DECIMAL(10,2) NOT NULL
);

CREATE TABLE relatorio_compra (
    id_relatorio_compra INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_inicial DATE NOT NULL,
    data_final DATE NOT NULL,
    total_compras DECIMAL(10,2) NOT NULL,
    qtd_compras INT NOT NULL,
    data_gerada DATE NOT NULL,
    produto_frequente VARCHAR(150) NOT NULL,
    fornecedor_frequente VARCHAR(150) NOT NULL
);

CREATE TABLE relatorio_caixa (
    id_relatorio_caixa INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_abertura DATE,
    data_fechamento DATE,
    operador VARCHAR(150),
    saldo_inicial DECIMAL(10,2),
    entrada_vendas DECIMAL(10,2),
    entrada_reforco DECIMAL(10,2),
    total_entradas DECIMAL(10,2),
    saida_sangria DECIMAL(10,2),
    saida_despesa DECIMAL(10,2),
    total_saida DECIMAL(10,2),
    saldo_final DECIMAL(10,2),
    observacoes VARCHAR(200),
    data_gerada DATE,
    id_caixa_fk INT NOT NULL,
    FOREIGN KEY (id_caixa_fk) REFERENCES caixa(id_caixa)
);


CREATE TABLE compra (
    id_compra INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_compra DATE NOT NULL,
    valor_total DECIMAL(10,2),
    tipo_pagamento VARCHAR(50) NOT NULL,
    observacao VARCHAR(200),
    status_compra ENUM('PENDENTE', 'FINALIZADA', 'CANCELADA') NOT NULL,
    id_fornecedor_pf_fk INT,
    id_fornecedor_pj_fk INT,
    id_login_fk INT NOT NULL,
    FOREIGN KEY (id_fornecedor_pf_fk) REFERENCES fornecedor_pf(id_fornecedor_pf),
    FOREIGN KEY (id_fornecedor_pj_fk) REFERENCES fornecedor_pj(id_fornecedor_pj),
     CHECK (
  (id_fornecedor_pf_fk IS NOT NULL AND id_fornecedor_pj_fk IS NULL) OR
  (id_fornecedor_pf_fk IS NULL AND id_fornecedor_pj_fk IS NOT NULL)
),
    FOREIGN KEY (id_login_fk) REFERENCES login_exclusivo(id_login)
);


CREATE TABLE compra_item (
    id_compra_item INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    id_compra_fk INT NOT NULL,
    id_produto_fk INT NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) GENERATED ALWAYS AS (quantidade * preco_unitario) STORED,
    FOREIGN KEY (id_compra_fk) REFERENCES compra(id_compra),
    FOREIGN KEY (id_produto_fk) REFERENCES produto(id_produto)
);

#AQUIIIIIIIIIIIIIII CONTINUA DAQUIIIIII
CREATE TABLE venda (
    id_venda INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    data_gerada DATE NOT NULL,
    hora TIME NOT NULL,
    valor_total DECIMAL(10,2) NOT NULL,
    desconto DECIMAL(10,2),
    valor_final DECIMAL(10,2) NOT NULL,
    forma_pagamento ENUM ('PIX','CREDITO','DEBITO','DINHEIRO'),
    status_venda ENUM('FECHADA','EM ABERTO'),
    id_caixa_fk INT NOT NULL,
	id_cliente_pf_fk INT,
    id_cliente_pj_fk INT,
	FOREIGN KEY (id_cliente_pf_fk) REFERENCES cliente_pf(id_cliente_pf),
    FOREIGN KEY (id_cliente_pj_fk) REFERENCES cliente_pj(id_cliente_pj),
    CHECK (
  (id_cliente_pf_fk IS NOT NULL AND id_cliente_pj_fk IS NULL) OR
  (id_cliente_pf_fk IS NULL AND id_cliente_pj_fk IS NOT NULL)
),
    
    FOREIGN KEY (id_caixa_fk) REFERENCES caixa(id_caixa)
);

CREATE TABLE pagamento (
    id_pagamento INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    valor_pago DECIMAL(10,2) NOT NULL,
    data_pagamento DATE NOT NULL,
    parcelado BOOLEAN NOT NULL,
    qtd_parcelas INT,
    status_pagamento ENUM('PENDENTE','FINALIZADO','PAGO','CANCELADO'),
    id_venda_fk INT NOT NULL,
    id_cliente_pf_fk INT,
    id_cliente_pj_fk INT,
     CHECK (
  (id_cliente_pf_fk IS NOT NULL AND id_cliente_pj_fk IS NULL) OR
  (id_cliente_pf_fk IS NULL AND id_cliente_pj_fk IS NOT NULL)
),
    FOREIGN KEY (id_venda_fk) REFERENCES venda(id_venda),
    FOREIGN KEY (id_cliente_pf_fk) REFERENCES cliente_pf(id_cliente_pf),
    FOREIGN KEY (id_cliente_pj_fk) REFERENCES cliente_pj(id_cliente_pj)
);

CREATE TABLE parcela (
    id_parcela INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    qtd_parcelas INT NOT NULL,
    data_vencimento DATE NOT NULL,
    valor_parcela DECIMAL(10,2) NOT NULL,
    status_parcela ENUM('PAGA','CANCELADA','EM ABERTO') NOT NULL,
    data_pagamento DATE NOT NULL,
    id_pagamento_fk INT NOT NULL,
    id_despesa_fk INT NOT NULL,
    FOREIGN KEY (id_pagamento_fk) REFERENCES pagamento(id_pagamento),
    FOREIGN KEY (id_despesa_fk) REFERENCES despesa(id_despesa)
);

CREATE TABLE nota_fiscal (
    id_nota_fiscal INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    numero VARCHAR(100) NOT NULL,
    data_emissao DATE NOT NULL,
    valor_total DECIMAL(10,2) NOT NULL,
    tipo ENUM('NFC-E', 'NF-E', 'CUPOM'),
    chave_acesso VARCHAR(100) NOT NULL,
    xml_nota TEXT,
    id_venda_fk INT NOT NULL,
    FOREIGN KEY (id_venda_fk) REFERENCES venda(id_venda)
);

CREATE TABLE item_venda (
id_item_venda INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
qtd INT NOT NULL,
preco_unit DECIMAL(10,2),
subtotal DECIMAL(10,2) GENERATED ALWAYS AS (qtd * preco_unit) STORED,
id_venda_fk INT NOT NULL,
id_produto_fk INT NOT NULL,
FOREIGN KEY (id_venda_fk) REFERENCES venda(id_venda),
FOREIGN KEY (id_produto_fk) REFERENCES produto(id_produto)
);


#CONTINANDO DE USUARIO


INSERT INTO usuario (nome, email, senha, perfil_acesso) VALUES 
('João Silva', 'joao.silva@foxkey.com.br', 'senha123', 'ADMIN'),
('Maria Oliveira', 'maria.oliveira@foxkey.com.br', 'mariA123', 'GERENTE'),
('Carlos Souza', 'carlos.souza@foxkey.com.br', 'carlos123', 'VENDEDOR'),
('Ana Santos', 'ana.santos@foxkey.com.br', 'ana12345', 'CAIXA'),
('Pedro Rocha', 'pedro.rocha@foxkey.com.br', 'pedro123', 'VENDEDOR');

INSERT INTO login_exclusivo (data_ativacao, id_usuario_fk) VALUES 
('2023-01-10', 1),
('2023-01-10', 2),
('2023-01-11', 3),
('2023-01-12', 4),
('2023-01-15', 5);

INSERT INTO permissao (nome_modulo, pode_visualizar, pode_criar, pode_editar, pode_excluir, id_usuario_fk) VALUES 
('financeiro', TRUE, TRUE, TRUE, TRUE, 1),
('estoque', TRUE, TRUE, TRUE, TRUE, 1),
('vendas', TRUE, TRUE, TRUE, TRUE, 1),
('compras', TRUE, TRUE, TRUE, TRUE, 1),
('relatorios', TRUE, TRUE, TRUE, TRUE, 1),
('financeiro', TRUE, TRUE, TRUE, FALSE, 2),
('estoque', TRUE, TRUE, TRUE, FALSE, 2),
('vendas', TRUE, TRUE, TRUE, TRUE, 3),
('compras', FALSE, FALSE, FALSE, FALSE, 3),
('caixa', TRUE, FALSE, FALSE, FALSE, 4);

INSERT INTO endereco_contato (rua, numero, bairro, complemento, referencia, cep, estado, cidade, email, celular) VALUES 
('Av. Paulista', '1000', 'Bela Vista', '10º andar', 'Próximo ao metrô Trianon', '01310-100', 'SP', 'São Paulo', 'contato@foxkey.com.br', '(11) 99999-9999'),
('Rua das Flores', '123', 'Centro', 'Apto 101', 'Edifício Rosa', '01001-000', 'SP', 'São Paulo', 'cliente1@email.com', '(11) 98888-8888'),
('Av. Brasil', '2000', 'Jardim América', 'Sala 5', 'Próximo ao shopping', '01431-000', 'SP', 'São Paulo', 'fornecedor1@email.com', '(11) 97777-7777'),
('Rua XV de Novembro', '500', 'Centro', NULL, 'Em frente à praça', '80020-310', 'PR', 'Curitiba', 'empresa1@email.com', '(41) 96666-6666'),
('Av. Rio Branco', '300', 'Centro', '3º andar', NULL, '20040-007', 'RJ', 'Rio de Janeiro', 'funcionario1@email.com', '(21) 95555-5555');

INSERT INTO fornecedor_pf (nome, sobrenome, cpf, data_nascimento, rg, sexo, estado_civil, orgao_expedidor, nacionalidade, raca, id_endereco_contato_fk) VALUES 
('Marcos', 'Aurelio', '123.456.789-01', '1980-05-15', '1234567', 'MASCULINO', 'CASADO', 'SSP/SP', 'Brasileira', 'BRANCA', 3),
('Fernanda', 'Lima', '987.654.321-09', '1975-08-22', '7654321', 'FEMININO', 'SOLTEIRO', 'SSP/SP', 'Brasileira', 'PARDA', 5);

INSERT INTO fornecedor_pj (nome_fantasia, razao_social, inscricao_municipal, cnpj, data_abertura, representante, id_endereco_contato_fk) VALUES 
('Distribuidora ABC', 'ABC Distribuidora Ltda', '123456', '12345678000199', '2010-03-10', 'Roberto Almeida', 3),
('Alimentos SA', 'Alimentos e Bebidas SA', '654321', '98765432000155', '2005-07-20', 'Patricia Mendes', 4);

INSERT INTO cliente_pf (nome, sobrenome, data_nascimento, cpf, rg, sexo, id_endereco_contato_fk) VALUES 
('Ana', 'Carvalho', '1990-04-25', '111.222.333-44', '11222333', 'FEMININO', 2),
('Lucas', 'Martins', '1985-11-30', '555.666.777-88', '55666777', 'MASCULINO', 5);

INSERT INTO cliente_pj (nome_fantasia, razao_social, inscricao_municipal, cnpj, data_abertura, representante, id_endereco_contato_fk) VALUES 
('Mercado Central', 'Mercado Central Ltda', '112233', '11222333000144', '2015-06-18', 'Carlos Eduardo', 2),
('Restaurante Bom Sabor', 'Bom Sabor Alimentos ME', '445566', '55666777000188', '2018-09-22', 'Juliana Pereira', 4);

INSERT INTO funcionario (nome, sobrenome, cpf, rg, orgao_expedidor, nacionalidade, numero_ctps, numero_pis, raca, sexo, estado_civil, cargo, grau_instrucao, data_nascimento, id_endereco_contato_fk) VALUES 
('João', 'Silva', '999.888.777-66', '99888777', 'SSP/SP', 'Brasileira', '12345', '123456789', 'BRANCA', 'MASCULINO', 'CASADO', 'Gerente', 'ENSINO SUPERIOR', '1980-02-15', 1),
('Maria', 'Oliveira', '777.666.555-44', '77666555', 'SSP/SP', 'Brasileira', '54321', '987654321', 'PARDA', 'FEMININO', 'SOLTEIRO', 'Vendedor', 'ENSINO MÉDIO', '1990-07-20', 2),
('Carlos', 'Souza', '555.444.333-22', '55444333', 'SSP/SP', 'Brasileira', '67890', '456789123', 'PRETA', 'MASCULINO', 'DIVORCIADO', 'Caixa', 'ENSINO MÉDIO', '1985-11-10', 3);

INSERT INTO categoria (nome, descricao, prioridade_reposicao, data_registro, ativo) VALUES 
('Bebidas', 'Refrigerantes, sucos e águas', 1, '2023-01-01', TRUE),
('Limpeza', 'Produtos de limpeza em geral', 2, '2023-01-01', TRUE),
('Alimentos', 'Alimentos não perecíveis', 3, '2023-01-01', TRUE),
('Higiene', 'Produtos de higiene pessoal', 2, '2023-01-01', TRUE),
('Pet', 'Produtos para animais de estimação', 4, '2023-01-15', TRUE);

INSERT INTO produto (codigo_barra, unidade_medida, preco_custo, preco_venda, ativo, data_cadastro, nome, descricao, id_categoria_fk, id_fornecedor_pj_fk) VALUES 
('7891000310107', 'un', 2.50, 5.99, TRUE, '2023-01-05', 'Coca-Cola 2L', 'Refrigerante Coca-Cola 2 litros', 1, 1),
('7891000310206', 'un', 2.20, 4.99, TRUE, '2023-01-05', 'Guaraná Antártica 2L', 'Refrigerante Guaraná 2 litros', 1, 1),
('7896035700011', 'un', 1.80, 3.50, TRUE, '2023-01-06', 'Sabão em pó Omo', 'Sabão em pó Omo 1kg', 2, 2),
('7896035700028', 'un', 0.90, 2.20, TRUE, '2023-01-06', 'Detergente Ypê', 'Detergente líquido Ypê 500ml', 2, 2),
('7891234567890', 'kg', 8.50, 15.99, TRUE, '2023-01-10', 'Arroz Tio João', 'Arroz tipo 1 5kg', 3, 1);

INSERT INTO estoque (qtd_atual, qtd_reservada, qtd_minima, status_estoque, observacao, id_produto_fk) VALUES 
(150, 20, 50, 'DISPONIVEL', 'Estoque principal', 1),
(200, 15, 50, 'DISPONIVEL', 'Estoque principal', 2),
(80, 5, 30, 'DISPONIVEL', 'Estoque secundário', 3),
(120, 10, 40, 'DISPONIVEL', 'Estoque principal', 4),
(60, 0, 20, 'DISPONIVEL', 'Estoque novo', 5);

INSERT INTO movimentacao_caixa (tipo, valor, descricao, data_gerada, hora) VALUES 
('ENTRADA', 1000.00, 'Abertura de caixa', '2023-01-10', '08:00:00'),
('SAIDA', 150.00, 'Compra de material', '2023-01-10', '10:30:00'),
('ENTRADA', 350.50, 'Venda 001', '2023-01-10', '11:15:00'),
('ENTRADA', 420.75, 'Venda 002', '2023-01-10', '14:20:00'),
('SAIDA', 200.00, 'Sangria', '2023-01-10', '16:45:00');

INSERT INTO caixa (data_abertura, data_fechamento, saldo_inicial, saldo_final, total_entrada, total_saida, id_funcionario_fk, id_login_fk, id_movimentacao_fk) VALUES 
('2023-01-10', '2023-01-10', 1000.00, 1421.25, 1771.25, 350.00, 3, 4, 1),
('2023-01-11', '2023-01-11', 1000.00, 1560.80, 1860.80, 300.00, 3, 4, NULL),
('2023-01-12', NULL, 1000.00, NULL, NULL, NULL, 3, 4, NULL);

INSERT INTO relatorio_venda (data_cadastro, total_vendas, total_recibo) VALUES 
('2023-01-10', 771.25, 771.25),
('2023-01-11', 860.80, 860.80),
('2023-01-12', 720.50, 720.50);

INSERT INTO relatorio_compra (data_inicial, data_final, total_compras, qtd_compras, data_gerada, produto_frequente, fornecedor_frequente) VALUES 
('2023-01-01', '2023-01-10', 2500.00, 5, '2023-01-11', 'Coca-Cola 2L', 'Distribuidora ABC'),
('2023-01-11', '2023-01-20', 1800.00, 3, '2023-01-21', 'Arroz Tio João', 'Distribuidora ABC'),
('2023-01-21', '2023-01-31', 3200.00, 7, '2023-02-01', 'Sabão em pó Omo', 'Alimentos SA');

INSERT INTO relatorio_caixa (data_abertura, data_fechamento, operador, saldo_inicial, entrada_vendas, entrada_reforco, total_entradas, saida_sangria, saida_despesa, total_saida, saldo_final, observacoes, data_gerada, id_caixa_fk) VALUES 
('2023-01-10', '2023-01-10', 'Ana Santos', 1000.00, 771.25, 0.00, 771.25, 200.00, 150.00, 350.00, 1421.25, 'Caixa normal', '2023-01-10', 1),
('2023-01-11', '2023-01-11', 'Ana Santos', 1000.00, 860.80, 0.00, 860.80, 200.00, 100.00, 300.00, 1560.80, 'Movimentação normal', '2023-01-11', 2);

INSERT INTO compra (data_compra, valor_total, tipo_pagamento, observacao, status_compra, id_fornecedor_pj_fk, id_login_fk) VALUES 
('2023-01-05', 1200.00, 'BOLETO', 'Compra mensal', 'FINALIZADA', 1, 1),
('2023-01-12', 800.00, 'CARTAO', 'Reposição de estoque', 'FINALIZADA', 2, 2),
('2023-01-20', 1500.00, 'PIX', 'Compra emergencial', 'PENDENTE', 1, 1);

INSERT INTO compra_item (id_compra_fk, id_produto_fk, quantidade, preco_unitario) VALUES 
(1, 1, 200, 2.50),
(1, 2, 150, 2.20),
(2, 3, 100, 1.80),
(2, 4, 150, 0.90),
(3, 5, 80, 8.50);

INSERT INTO venda 
(data_gerada, hora, valor_total, desconto, valor_final, forma_pagamento, status_venda, id_caixa_fk, id_cliente_pf_fk, id_cliente_pj_fk) VALUES 
('2023-01-10', '11:15:00', 350.50, 0.00, 350.50, 'DINHEIRO', 'FECHADA', 1, 1, NULL),  -- Cliente PF
('2023-01-10', '14:20:00', 420.75, 20.00, 400.75, 'CREDITO', 'FECHADA', 1, NULL, 1),   -- Cliente PJ
('2023-01-11', '10:30:00', 280.00, 0.00, 280.00, 'DEBITO', 'FECHADA', 2, 2, NULL),     -- Cliente PF
('2023-01-11', '15:45:00', 580.80, 0.00, 580.80, 'PIX', 'FECHADA', 2, NULL, 2);       -- Cliente PJ


INSERT INTO pagamento 
(valor_pago, data_pagamento, parcelado, qtd_parcelas, status_pagamento, id_venda_fk, id_cliente_pf_fk, id_cliente_pj_fk) VALUES 
(350.50, '2023-01-10', FALSE, NULL, 'PAGO', 1, 1, NULL),
(400.75, '2023-01-10', FALSE, NULL, 'PAGO', 2, NULL, 1),
(280.00, '2023-01-11', FALSE, NULL, 'PAGO', 3, 2, NULL),
(580.80, '2023-01-11', TRUE, 3, 'PENDENTE', 4, NULL, 2);

INSERT INTO despesa (tipo_despesa, valor, data_gerada, descricao, id_login_fk) VALUES 
('ADMINISTRATIVA', 150.00, '2023-01-10', 'Material de escritório', 1),
('COMERCIAL', 200.00, '2023-01-11', 'Propaganda local', 2),
('COMPRA', 1200.00, '2023-01-05', 'Compra de produtos', 1);


INSERT INTO parcela (qtd_parcelas, data_vencimento, valor_parcela, status_parcela, data_pagamento, id_pagamento_fk, id_despesa_fk) VALUES 
(3, '2023-02-11', 193.60, 'EM ABERTO', '2023-01-11', 13, 1),
(3, '2023-03-11', 193.60, 'EM ABERTO', '2023-01-11', 14, 1),
(3, '2023-04-11', 193.60, 'EM ABERTO', '2023-01-11', 15, 1);

INSERT INTO nota_fiscal (numero, data_emissao, valor_total, tipo, chave_acesso, xml_nota, id_venda_fk) VALUES 
('000001', '2023-01-10', 350.50, 'NFC-E', 'NFe41190306117473000150550010000000011000000010', NULL, 1),
('000002', '2023-01-10', 400.75, 'NFC-E', 'NFe41190306117473000150550010000000021000000020', NULL, 2),
('000003', '2023-01-11', 280.00, 'NFC-E', 'NFe41190306117473000150550010000000031000000030', NULL, 3);

INSERT INTO item_venda (qtd, preco_unit, id_venda_fk, id_produto_fk) VALUES 
(10, 5.99, 1, 1),
(5, 4.99, 1, 2),
(8, 3.50, 2, 3),
(15, 2.20, 2, 4),
(3, 15.99, 3, 5),
(10, 5.99, 4, 1),
(20, 4.99, 4, 2);


