CREATE PROCEDURE uspClienteExcluir
	@IdCliente INT
AS
BEGIN
	
	DELETE FROM
		tblCliente
	WHERE
		IdCliente = @IdCliente

	SELECT @IdCliente AS Retorno
END