CREATE TABLE Especialidade (
	id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nome VARCHAR(15) NOT NULL
)


CREATE TABLE Medico (
	id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nome VARCHAR(40) NOT NULL,
	dataNascimento DATETIME NOT NULL,
	eMail VARCHAR(50) NOT NULL,
	celular CHAR(16) NOT NULL,
	telefone CHAR(15) NOT NULL,
	especialidade INT NOT NULL,
	foto IMAGE NOT NULL,

	CONSTRAINT fk_MedicoEspecialidade FOREIGN KEY(especialidade) REFERENCES Especialidade(id)
)

CREATE TABLE Imagens (
	id INT PRIMARY KEY NOT NULL,
	foto VARBINARY(MAX) NOT NULL
)

(medico, paciente, dataInicio, dataFim, Anotacoes, situacao)
CREATE TABLE Consulta (
    id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    idMedico INT NOT NULL,
    -- TERMINAR
)