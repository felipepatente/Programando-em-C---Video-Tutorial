SELECT * FROM tblPessoaJuridica;
SELECT * FROM tblFilial;

EXEC uspFilialInserir 4;


SELECT * 
FROM 
	tblFilial
JOIN
	tblPessoaJuridica
ON
	tblFilial.IDPessoaFilial = tblPessoaJuridica.IDPessoaJuridica 
