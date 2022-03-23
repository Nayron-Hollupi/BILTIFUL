GO
ALTER PROCEDURE Inserir_Cliente
    @CPF NVARCHAR(11),
    @Nome NVARCHAR(50) ,
    @Data_Nascimento DATE,
    @Sexo CHAR(1)

AS 
 INSERT INTO Cliente
    (CPF ,Nome ,Data_Nascimento , Sexo )
VALUES
    (@CPF, @Nome, @Data_Nascimento, @Sexo )
 

SELECT *
FROM Cliente


Delete FRom Cliente


--------------------------------------------------------------- 
GO
CREATE PROCEDURE Inserir_Fornecedor
    @CNPJ NVARCHAR(14),
    @Razao_Social NVARCHAR(50),
    @Data_Abertura DATE

AS
INSERT INTO Fornecedor
    (CNPJ, Razao_Social, Data_Abertura)
VALUES
    (@CNPJ, @Razao_Social, @Data_Abertura )



SELECT *
FROM Fornecedor


DELETE FROM Fornecedor


-------------------------------------------------------------------------------------------
GO
CREATE PROCEDURE Inserir_Materia_Prima
    @Codigo NVARCHAR(6) ,
    @Nome NVARCHAR(20)

AS
INSERT INTO Materia_Prima
    (Codigo, Nome)
VALUES(@Codigo, @Nome)


SELECT *
FROM Materia_Prima


   GO
CREATE PROCEDURE Inserir_Produto
    @Codigo_Barras NVARCHAR(12),
    @Nome NVARCHAR(20),
    @Valor_Venda DECIMAL(10,2)

AS
INSERT INTO Produto
    (Codigo_Barras,Nome,Valor_Venda )
VALUES(@Codigo_Barras, @Nome, @Valor_Venda)

SELECT *
FROM Produto



    GO
CREATE PROCEDURE Inserir_Risco
    @CPF NVARCHAR(11)

AS
INSERT INTO Risco
    (CPF)
VALUES
    (@CPF)

GO
CREATE PROCEDURE Remove_Risco
    @CPF NVARCHAR(11)

AS
DELETE FROM Risco
       WHERE @CPF = CPF

SELECT CPF
FROM Risco


       GO
CREATE PROCEDURE Inserir_Bloqueado
    @CNPJ NVARCHAR(14)

AS
INSERT INTO Bloqueado
    (CNPJ)
VALUES
    (@CNPJ)

GO
CREATE PROCEDURE Remove_Bloqueado
    @CNPJ NVARCHAR(14)

AS
DELETE FROM Bloqueado
       WHERE @CNPJ = CNPJ




SELECT *
FROM Bloqueado



       GO
ALTER PROCEDURE Inserir_Item_Producao
   @Codigo_Materia_Prima NVARCHAR(6), 
    @Quantidade_Materia_Prima DECIMAL(10,2)

AS
INSERT INTO Item_Producao
    (Codigo_Materia_Prima ,Quantidade_Materia_Prima )
VALUES
    ( @Codigo_Materia_Prima, @Quantidade_Materia_Prima )



     GO
ALTER PROCEDURE Inserir_Producao
    @Codigo_Barras_Produto NVARCHAR(12),
    @Quantidade DECIMAL(10,2)

AS

INSERT INTO Producao
    (Codigo_Barras_Produto ,Quantidade )
VALUES
    (@Codigo_Barras_Produto,  @Quantidade)



SELECT *
FROM Produto

SELECT * FROM Producao

SELECT * FROM   Item_Producao

