CREATE PROCEDURE uspFilialInserir
	@IDPessoaFilial AS INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			-- Não deixar inserir Pessoa que ná é filial
			IF (EXISTS(SELECT IDPessoaFilial FROM tblFilial WHERE IDPessoaFilial = @IDPessoaFilial))
				RAISERROR('Filial já existe',14,1);
			
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
