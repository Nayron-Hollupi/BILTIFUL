CREATE TABLE Cliente
(

    CPF NVARCHAR(11) NOT NULL,
    Nome NVARCHAR(50) NOT NULL,
    Data_Nascimento DATE NOT NULL,
    Sexo CHAR(1) ,
    Ultima_Compra DATE NULL DEFAULT GETDATE(),
    Data_Cadastro DATE NULL DEFAULT GETDATE() ,
    Situacao CHAR(1)NOT NULL DEFAULT 'A',

    CONSTRAINT Pk_Cliente PRIMARY KEY (CPF)

);



CREATE TABLE Fornecedor
(

    CNPJ NVARCHAR(14) NOT NULL,
    Razao_Social NVARCHAR(50) NOT NULL,
    Data_Abertura DATE NULL,
    Ultima_Compra DATE NULL DEFAULT GETDATE(),
    Data_Cadastro DATE NOT NULL DEFAULT GETDATE(),
    Situacao CHAR(1) NOT NULL DEFAULT 'A',

    CONSTRAINT Pk_Fornecedor PRIMARY KEY (CNPJ)
);


CREATE TABLE Materia_Prima
(

    Codigo NVARCHAR(6) NOT NULL,
    Nome NVARCHAR(20) NOT NULL,
    Ultimo_Compra DATE NOT NULL DEFAULT GETDATE(),
    Data_Cadastro DATE NOT NULL DEFAULT GETDATE(),
    Situacao CHAR(1) NOT NULL DEFAULT 'A',

    CONSTRAINT Pk_Materia_Prima PRIMARY KEY (Codigo),


);


CREATE TABLE Produto
(
    Codigo  NVARCHAR(6) NOT NULL
    Codigo_Barras NVARCHAR(12) NOT NULL,
    Nome NVARCHAR(20) NOT NULL,
    Valor_Venda DECIMAL(10,2) NOT NULL,
    Ultima_Venda DATE DEFAULT GETDATE(),
    Data_Cadastro DATE NOT NULL DEFAULT GETDATE(),
    Situacao CHAR(1) NOT NULL DEFAULT 'A',


    CONSTRAINT Pk_Produto PRIMARY KEY (Codigo_Barras)
);


CREATE TABLE Risco
(

    CPF NVARCHAR(11) NOT NULL

        CONSTRAINT Pk_Risco PRIMARY KEY (CPF)
);



CREATE TABLE Bloqueado
(

    CNPJ NVARCHAR(14) NOT NULL,

    CONSTRAINT Pk_Bloqueado PRIMARY KEY (CNPJ)
);


CREATE TABLE Venda
(

    Codigo INT NOT NULL IDENTITY,
    CPF_Cliente NVARCHAR (11) NOT NULL,
    Data_Venda DATE NULL DEFAULT GETDATE(),
    Valor_Total DECIMAL(10,2) NULL,

    CONSTRAINT Pk_Venda PRIMARY KEY (Codigo),

    CONSTRAINT Fk_Cliente_Venda FOREIGN KEY (CPF_Cliente)
                            REFERENCES Cliente (CPF)

);


CREATE TABLE Item_Venda
(
    Codigo INT NOT NULL,
    Codigo_Venda INT NOT NULL,
    Codigo_Barras_Produto NVARCHAR(12) NOT NULL,
    Quantidade DECIMAL(10,2) NOT NULL,
    Valor_Unidade DECIMAL(10,2),
    Total_Item DECIMAL(10,2),

    CONSTRAINT Pk_Item_Venda PRIMARY KEY (Codigo, Codigo_Venda),

    CONSTRAINT Fk_Venda_Item FOREIGN KEY (Codigo_Venda)
                         REFERENCES Venda (Codigo)
);


CREATE TABLE Compra
(

    Codigo INT NOT NULL IDENTITY,
    Data_Compra DATE NOT NULL DEFAULT GETDATE(),
    CNPJ_Fornecedor NVARCHAR(14) NOT NULL,
    Valor_Total DECIMAL(10,2),

    CONSTRAINT Pk_Compra PRIMARY KEY (Codigo),

    CONSTRAINT Pk_Fornecedor_Compra FOREIGN KEY (CNPJ_Fornecedor)
                         REFERENCES Fornecedor (CNPJ)


);


CREATE TABLE Item_Compra
(

    Codigo INT NOT NULL IDENTITY,
    Codigo_Materia_Prima NVARCHAR(6) NOT NULL,
    Codigo_Compra INT NOT NULL,
    Quantidade NUMERIC(3,2) NOT NULL,
    Valor_Unitario DECIMAL(10,2) NOT NULL,
    Total_Item DECIMAL(10,2) NOT NULL,

    CONSTRAINT Pk_Item_Compra PRIMARY KEY (Codigo, Codigo_Materia_Prima),

    CONSTRAINT Fk_Materia_Prima FOREIGN KEY  (Codigo_Materia_Prima)
                            REFERENCES Materia_Prima (Codigo),

    CONSTRAINT Fk_Compra FOREIGN KEY (Codigo_Compra)
                     REFERENCES Compra (Codigo)
);


CREATE TABLE Producao
(

    Codigo INT NOT NULL IDENTITY,
    Codigo_Barras_Produto NVARCHAR(12) NOT NULL,
    Data_Producao DATE NOT NULL DEFAULT GETDATE(),
    Quantidade DECIMAL(10,2) NOT NULL,


    CONSTRAINT Pk_Producao PRIMARY KEY (Codigo),

    CONSTRAINT Fk_Produto FOREIGN KEY (Codigo_Barras_Produto)
                      REFERENCES  Produto (Codigo_Barras)
);



CREATE TABLE Item_Producao
(
    Codigo INT  NOT NULL IDENTITY,
    Codigo_Materia_Prima NVARCHAR(6) NOT NULL,
    Quantidade_Materia_Prima DECIMAL(10,2) NOT NULL,
    Data_Producao DATE NOT NULL DEFAULT GETDATE(),

    CONSTRAINT Pk_Item_Producao PRIMARY KEY (Codigo),

    CONSTRAINT Fk_Materia_Prima_Produto FOREIGN KEY (Codigo_Materia_Prima)
                            REFERENCES Materia_Prima (Codigo)
);
