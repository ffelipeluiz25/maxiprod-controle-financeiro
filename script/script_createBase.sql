CREATE TABLE Pessoas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(200) NOT NULL,
    Idade INT NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DataAlteracao DATETIME NULL
);

CREATE TABLE Categorias (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Descricao VARCHAR(400) NOT NULL,
    Finalidade TINYINT NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DataAlteracao DATETIME NULL
);

INSERT INTO Categorias (Descricao, Finalidade, DataCriacao, DataAlteracao) 
VALUES 
('Despesa', 1, NOW(), NULL),
('Receita', 2, NOW(), NULL),
('Ambas', 3, NOW(), NULL);

CREATE TABLE Transacoes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Descricao VARCHAR(400) NOT NULL,
    Valor DECIMAL(18,2) NOT NULL,
    Tipo TINYINT NOT NULL,
    IdCategoria INT NOT NULL,
    IdPessoa INT NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DataAlteracao DATETIME NULL,
    CONSTRAINT FK_Transacao_Categoria
        FOREIGN KEY (IdCategoria) REFERENCES Categorias(Id),
    CONSTRAINT FK_Transacao_Pessoa
        FOREIGN KEY (IdPessoa) REFERENCES Pessoas(Id)
        ON DELETE CASCADE
);

CREATE INDEX IDX_Transacao_PessoaId ON Transacoes(IdPessoa);
CREATE INDEX IDX_Transacao_CategoriaId ON Transacoes(IdCategoria);



-- =========================================
-- PROCEDURES
-- =========================================
drop procedure spTotaisPorPessoa;
DELIMITER $$

CREATE PROCEDURE spTotaisPorPessoa()
BEGIN
    SELECT 
        p.Id,
        p.Nome,

        COALESCE(SUM(CASE WHEN t.Tipo = 2 THEN t.Valor END), 0) AS TotalReceitas,
        COALESCE(SUM(CASE WHEN t.Tipo = 1 THEN t.Valor END), 0) AS TotalDespesas,
        COALESCE(SUM(CASE WHEN t.Tipo = 2 THEN t.Valor END), 0) -
        COALESCE(SUM(CASE WHEN t.Tipo = 1 THEN t.Valor END), 0) AS Saldo

    FROM Pessoas p
    LEFT JOIN Transacoes t ON t.IdPessoa = p.Id
    GROUP BY p.Id, p.Nome;
END$$

DELIMITER ;

drop procedure spTotaisPorCategoria;
DELIMITER $$

CREATE PROCEDURE spTotaisPorCategoria()
BEGIN
    SELECT 
        c.Id,
        c.Descricao,

        COALESCE(SUM(CASE WHEN t.Tipo = 2 THEN t.Valor END), 0) AS TotalReceitas,
        COALESCE(SUM(CASE WHEN t.Tipo = 1 THEN t.Valor END), 0) AS TotalDespesas,
        COALESCE(SUM(CASE WHEN t.Tipo = 2 THEN t.Valor END), 0) -
        COALESCE(SUM(CASE WHEN t.Tipo = 1 THEN t.Valor END), 0) AS Saldo

    FROM Categorias c
    LEFT JOIN Transacoes t ON t.IdCategoria = c.Id
    GROUP BY c.Id, c.Descricao;
END$$

DELIMITER ;