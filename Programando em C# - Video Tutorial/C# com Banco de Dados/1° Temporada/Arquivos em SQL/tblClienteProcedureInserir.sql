CREATE PROCEDURE uspClienteInserir
	@Nome VARCHAR(100),
	@DataNascimento DATETIME,
	@Sexo BIT,
	@LimiteCompra DECIMAL(18,2)
AS
BEGIN
	
	INSERT INTO tblCliente
	(
		Nome,
		DataNascimento,
		Sexo,
		LimiteCompra
	)
	VALUES
	(
		@Nome,
		@DataNascimento,
		@Sexo,
		@LimiteCompra
	)

	SELECT @@IDENTITY AS Retorno

END