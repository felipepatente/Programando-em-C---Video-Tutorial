CREATE PROCEDURE uspClienteConsultarPorId
	@idCliente INT
AS
BEGIN
	
	SELECT
		IdCliente,
		Nome,
		DataNascimento,
		Sexo,
		LimiteCompra
	FROM
		tblCliente
	WHERE
		IdCliente = @idCliente

END