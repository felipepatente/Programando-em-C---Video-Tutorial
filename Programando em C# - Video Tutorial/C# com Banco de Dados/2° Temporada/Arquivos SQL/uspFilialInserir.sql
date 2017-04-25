CREATE PROCEDURE uspFilialInserir
	@IDPessoaFilial AS INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			-- N�o deixar inserir Pessoa que n� � filial
			IF (EXISTS(SELECT IDPessoaFilial FROM tblFilial WHERE IDPessoaFilial = @IDPessoaFilial))
				RAISERROR('Filial j� existe',14,1);
			
			-- Inserir a filial
			INSERT INTO tblFilial(IDPessoaFilial)
			VALUES(@IDPessoaFilial)

			SELECT @IDPessoaFilial AS Retorno;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS Retorno;
	END CATCH
END
