CREATE DATABASE db_cidade_unida

USE db_cidade_unida;

-- Tabela Usuario
CREATE TABLE tb_usuario (
    id_usuario INT NOT NULL IDENTITY(1,1),
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
	telefone VARCHAR(15) NOT NULL,
    senha VARCHAR(255) NOT NULL,
    is_adm BIT DEFAULT 0 NOT NULL,
    ativo BIT DEFAULT 1 NOT NULL,
	CONSTRAINT pk_usuario PRIMARY KEY (id_usuario)
);

-- Tabela Denuncia
CREATE TABLE tb_denuncia (
    id_denuncia INT NOT NULL IDENTITY(1,1),
    descricao VARCHAR(MAX) NOT NULL,
    status_denuncia INT DEFAULT 0 NOT NULL,
    categoria INT NOT NULL,
    rua VARCHAR(100) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    bairro VARCHAR(50) NOT NULL,
    cidade VARCHAR(50) NOT NULL,
    estado CHAR(2) NOT NULL,
    cep VARCHAR(10) NOT NULL,
    url_imagem VARCHAR(255) NULL,
    is_anonimo BIT DEFAULT 0 NOT NULL,
    data_envio DATETIME DEFAULT GETDATE() NOT NULL,
    ativo BIT DEFAULT 1 NOT NULL,
	CONSTRAINT pk_denuncia PRIMARY KEY (id_denuncia)
);

-- Tabela Realiza Denuncia (relacionamento entre Usuario e Denuncia)
CREATE TABLE tb_realiza_denuncia (
    id_usuario INT NOT NULL,
    id_denuncia INT NOT NULL,
	CONSTRAINT pk_realiza_denuncia PRIMARY KEY (id_usuario, id_denuncia),
	CONSTRAINT fk_realiza_denuncia_usuario FOREIGN KEY (id_usuario)
		REFERENCES tb_usuario (id_usuario),
	CONSTRAINT fk_realiza_denuncia_denuncia FOREIGN KEY (id_denuncia)
		REFERENCES tb_denuncia (id_denuncia)
);

-- Tabela Contato
CREATE TABLE tb_contato (
    id_contato INT NOT NULL IDENTITY(1,1),
    nome_remetente VARCHAR(100) NOT NULL,
    email_remetente VARCHAR(100) NOT NULL,
    mensagem VARCHAR(MAX) NOT NULL,
    data_envio DATETIME DEFAULT GETDATE(),
	CONSTRAINT pk_contato PRIMARY KEY (id_contato)
);

-- Tabela Faz Contato (relacionamento entre Usuario e Contato)
CREATE TABLE tb_faz_contato (
    id_usuario INT NOT NULL,
    id_contato INT NOT NULL,
	CONSTRAINT pk_faz_contato PRIMARY KEY (id_usuario, id_contato),
	CONSTRAINT fk_faz_contato_usuario FOREIGN KEY (id_usuario)
		REFERENCES tb_usuario (id_usuario),
	CONSTRAINT fk_faz_contato_contato FOREIGN KEY (id_contato)
		REFERENCES tb_contato (id_contato)
);