SELECT * FROM Medico
SELECT * FROM Usuario

INSERT Especialidade VALUES('Ginecologista')
INSERT Especialidade VALUES('Ortopedista')
INSERT Especialidade VALUES('Neurologista')

INSERT INTO Usuario VALUES(NEWID(), 'astolfo', 'astolfo@astolfomail.com', 'TROLEI', 'TROLEI2')

INSERT INTO Medico VALUES(NEWID(), 'Astolfo', '1912348765', '1912348765',
'1234567', '01/01/2001', '3176EB72-13F2-4EC3-A52F-0DB351BFE002', 1)

SELECT * FROM Consulta

UPDATE Consulta SET Status=0 WHERE Id='05B300DF-6E4A-4FC2-863F-8484C3505150'