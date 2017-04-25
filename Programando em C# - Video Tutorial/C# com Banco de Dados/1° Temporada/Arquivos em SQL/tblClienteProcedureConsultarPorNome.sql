CREATE PROCEDURE uspClienteConsultarPorNome
	@Nome VARCHAR(100)
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
		Nome LIKE '%' + @Nome + '%'

END