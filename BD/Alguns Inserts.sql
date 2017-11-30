UPDATE UsuarioPapel SET IdPapel = 3 WHERE IdUsuario = 'D0593DA5-1B3F-42AF-864D-7C497D752A67'

SELECT * FROM Medico
SELECT * FROM Especialidade
SELECT * FROM Paciente
SELECT * FROM Usuario
SELECT * FROM UsuarioPapel
SELECT * FROM Papel

INSERT Especialidade VALUES('Ginecologista')
INSERT Especialidade VALUES('Ortopedista')
INSERT Especialidade VALUES('Neurologista')

INSERT INTO Usuario VALUES(NEWID(), 'astolfo', 'astolfo@astolfomail.com', 'TROLEI', 'TROLEI2')

INSERT INTO Medico VALUES(NEWID(), 'Astolfo', '1912348765', '1912348765',
'1234567', '01/01/2001', '3176EB72-13F2-4EC3-A52F-0DB351BFE002', 1)

SELECT * FROM Consulta

UPDATE Consulta SET Status=0 WHERE Id='05B300DF-6E4A-4FC2-863F-8484C3505150'