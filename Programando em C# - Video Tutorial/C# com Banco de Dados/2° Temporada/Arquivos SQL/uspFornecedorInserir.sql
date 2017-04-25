CREATE PROCEDURE uspFornecedorInserir
	@IDPessoaFonecedor AS INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			IF (EXISTS(SELECT @IDPessoaFonecedor FROM tblFornecedor WHERE @IDPessoaFonecedor = IDPessoaFornecedor))	
				RAISERROR('Fornecedor já existe',14,1);
			
			-- Inserir o Fornecedor
			INSERT INTO tblFornecedor (IDPessoaFornecedor)
			VALUES (@IDPessoaFonecedor)

			SELECT @IDPessoaFonecedor AS Retorno

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS Retorno;
	END CATCH
END