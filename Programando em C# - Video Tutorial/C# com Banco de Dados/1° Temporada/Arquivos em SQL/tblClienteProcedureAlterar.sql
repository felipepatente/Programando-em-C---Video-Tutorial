CREATE PROCEDURE uspClienteAlterar
	@IdCliente INT,
	@Nome VARCHAR(100),
	@DataNascimento DATETIME,
	@Sexo BIT,
	@LimiteCompra DECIMAL(18,2)
AS
BEGIN
	UPDATE
		tblCliente
	SET
		Nome = @Nome,
		DataNascimento = @DataNascimento,
		Sexo = @Sexo,
		LimiteCompra = @LimiteCompra
	WHERE
		IdCliente = @IdCliente

	SELECT @IdCliente AS Retorno
END