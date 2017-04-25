CREATE PROCEDURE UspClienteInserir
	@IDPessoaCliente AS INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		
			IF(EXISTS(SELECT IDPessoaCliente FROM tblCliente WHERE IDPessoaCliente = @IDPessoaCliente))
				RAISERROR('Cliente j� existe',14,1);

			INSERT INTO tblCliente(IDPessoaCliente)
			VALUES (@IDPessoaCliente);

			SELECT @IDPessoaCliente AS Retorno;

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS Retorno;
	END CATCH
END