CREATE PROCEDURE sp_agendaMedico
@medico varchar(40)	-- CRIADO
AS BEGIN
	SELECT paciente, dataInicio, dataFim, situacao, id
	FROM Consulta, Medico
	WHERE medico = @medico
END

CREATE PROCEDURE sp_prontuario					-- CRIADO
@consulta int	
AS BEGIN
	SELECT medico, paciente, anotacoes, avaliacao 
	FROM Consulta
	WHERE id = @consulta
END

ALTER PROCEDURE cadastrarMedico					-- CRIADO
@nome VARCHAR(40),
@dataNascimento DATE,
@eMail VARCHAR(50),
@celular CHAR(14),
@telefone CHAR(13),
@especialidade VARCHAR(30),
@foto VARCHAR(1000)
AS
BEGIN
	DECLARE @indiceEspec INT;
	SELECT @indiceEspec = id FROM Especialidade WHERE nome = @especialidade;

	DECLARE @indiceAtual INT;

	IF IDENT_CURRENT('Medico') = NULL
		SET @indiceAtual = 0;
	ELSE
		SET @indiceAtual = IDENT_CURRENT('Paciente');

	SET @indiceAtual = '2' + CAST(@indiceAtual AS VARCHAR(5));

	INSERT INTO Imagens (id, foto)
	SELECT @indiceAtual, *
	FROM OPENROWSET(BULK N''' + @foto + ''', SINGLE_BLOB) IMAGE;

	INSERT Medico VALUES (@nome, @dataNascimento, @email, @celular, @telefone, @indiceEspec, @indiceAtual)
END




ALTER PROCEDURE cadastrarPaciente				-- CRIADO
	@nome VARCHAR(40),
	@endereco VARCHAR(50),
	@dataNasc DATE,
	@idade INT,
	@email VARCHAR(50),
	@celular VARCHAR(14),
	@telefone VARCHAR(13),
	@foto VARCHAR(1000)
AS
BEGIN

	DECLARE @indiceAtual INT;

	IF IDENT_CURRENT('Paciente') = NULL
		SET @indiceAtual = 0;
	ELSE
		SET @indiceAtual = IDENT_CURRENT('Paciente');

	SET @indiceAtual = '1' + CAST(@indiceAtual AS VARCHAR(5));

	INSERT INTO Imagens (id, foto)
	SELECT @indiceAtual, *
	FROM OPENROWSET(BULK N''' + @foto + ''', SINGLE_BLOB) IMAGE;

	INSERT Paciente VALUES (@nome, @endereco, @dataNasc, @idade, @email, @celular, @telefone, @indiceAtual)
END




CREATE PROCEDURE novaConsulta
	@medico VARCHAR(40),
	@horaInicio DATETIME,
	@horaFim DATETIME,
	@paciente VARCHAR(40)
AS BEGIN
	DECLARE @indiceMedico INT, @indicePaciente INT;
	SELECT @indiceMedico, id FROM Medico WHERE nome = @medico

	SELECT @indicePaciente, id FROM Paciente WHERE nome = @paciente

	IF NOT (
			DATEPART(HOUR, @horaInicio) < 9 OR											-- Começou antes das 9h
			(DATEPART(HOUR, @horaInicio) > 12 AND DATEPART(HOUR, @horaInicio) < 14) OR	-- Começou no horário de almoço
			(DATEPART(HOUR, @horaFim) > 12 AND DATEPART(HOUR, @horaFim) < 14) OR		-- Terminou no horário de almoço
			(DATEPART(HOUR, @horaFim) - DATEPART(HOUR, @horaInicio) < 30) OR			-- Terminou em menos de 30min
			(DATEPART(HOUR, @horaFim) - DATEPART(HOUR, @horaInicio) > 60) OR			-- Terminou em mais de 1h
			DATEPART(HOUR, @horaInicio) > 17											-- Começou depois das 17h
	)
		INSERT Consulta (medico, paciente, dataInicio, dataFim, Anotacoes, situacao) VALUES (@indiceMedico, @indicePaciente, @horaInicio, @horaFim, '', 0);
	ELSE
		RETURN 0
		;
END

GRANT ADMINISTER BULK OPERATIONS TO [PRII16187]

sp_help Consulta