-- Inserindo usu�rios na tabela tb_usuario
INSERT INTO tb_usuario (nome, email, telefone, senha, is_adm, ativo)
VALUES 
('Jo�o Silva', 'joao.silva@example.com', '11987654321', 'senha123', 0, 1),
('Alex Batista', 'alecao@gmail.com', '11912345678', 'acerola1', 1, 1),
('Carlos Souza', 'carlos.souza@example.com', '11987651234', 'senha789', 0, 0),
('Fernanda Costa', 'fernanda.costa@example.com', '11933445566', 'senha012', 0, 1),
('Ana Pereira', 'ana.pereira@example.com', '11966778899', 'senha345', 0, 1);
	
-- Inserindo dados na tabela tb_denuncia
INSERT INTO tb_denuncia 
(descricao, status_denuncia, categoria, rua, numero, bairro, cidade, estado, cep, url_imagem, is_anonimo, ativo)
VALUES
('Sacola de lixo descartada na cal�ada', 1, 0, 'Rua das Flores', '123', 'Jardim Alegre', 'Mat�o', 'SP', '15990-000', 'https://example.com/lixo1.jpg', 0, 1),
('�rvore caiu na avenida principal devido � tempestade', 2, 1, 'Avenida Central', '456', 'Centro', 'Mat�o', 'SP', '15990-001', 'https://example.com/arvore.jpg', 1, 1),
('Vazamento de �gua em frente � escola', 1, 2, 'Rua das Palmeiras', '789', 'Vila Nova', 'Mat�o', 'SP', '15990-002', NULL, 0, 1),
('Constru��o sem autoriza��o no bairro', 3, 3, 'Rua S�o Jo�o', '101', 'Jardim S�o Paulo', 'Mat�o', 'SP', '15990-003', 'https://example.com/obra.jpg', 1, 1),
('Den�ncia gen�rica sobre problemas na cidade', 4, 4, 'Rua do Sol', '999', 'Parque das �rvores', 'Mat�o', 'SP', '15990-004', NULL, 0, 1);

-- Inserindo dados na tabela tb_contato
INSERT INTO tb_contato 
(nome_remetente, email_remetente, mensagem)
VALUES
('Jo�o Silva', 'joao.silva@example.com', 'Gostaria de relatar problemas no servi�o de coleta de lixo.'),
('Maria Oliveira', 'maria.oliveira@example.com', 'Sugiro melhorias no tr�fego de ve�culos no centro da cidade.'),
('Carlos Almeida', 'carlos.almeida@example.com', 'Parab�ns pelo site, muito �til para a comunidade.'),
('Ana Costa', 'ana.costa@example.com', 'H� um problema recorrente com ilumina��o p�blica no meu bairro.'),
('Pedro Santos', 'pedro.santos@example.com', 'Gostaria de saber mais sobre como posso contribuir com o projeto.');




