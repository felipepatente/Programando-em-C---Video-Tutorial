CREATE PROCEDURE uspPessoaJuridicaInserir
	@NomeFantasia AS VARCHAR(50),
	@RazaoSocial AS VARCHAR(50),
	@CNPJ AS VARCHAR(14),
	@InscricaoEstadual AS VARCHAR(20),
	@DataFundacao AS DATE
AS
BEGIN

	BEGIN TRY
		BEGIN TRAN
			-- Não deixa inserir CNPJ igual no banco
			IF (EXISTS(SELECT IDPessoaJuridica FROM tblPessoaJuridica WHERE CNPJ = @CNPJ))
				RAISERROR('CNPJ já existe',14,1);

			--1° Inserir na tabela tblPessoa
			INSERT INTO tblPessoa(IDPessoaTipo)
			VALUES (2);

			-- DECLARE @IDPessoa INT = @@IDENTITY;

			DECLARE @IDPessoa INT
			SET @IDPessoa = @@IDENTITY;

			--2° Inserir na tabela tblPessoaJuridica
			INSERT INTO tblPessoaJuridica
			(
				IDPessoaJuridica,
				NomeFantasia,
				RazaoSocial,
				CNPJ,
				InscricaoEstadual,
				DataFundacao
			)VALUES
			(
				@IDPessoa,
				@NomeFantasia,
				@RazaoSocial,
				@CNPJ,
				@InscricaoEstadual,
				@DataFundacao
			)

			SELECT @IDPessoa AS Retorno;

		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE() AS Retorno
	END CATCH

END