
EXEC uspClienteConsultarPorNome 'Drausio'

EXEC uspClienteConsultarPorId 3

EXEC uspClienteInserir 'Drausio1', '2013-03-11', 1, 1000.50
EXEC uspClienteInserir 'Drausio2', '2013-03-12', 1, 600.35

EXEC uspClienteAlterar 2, 'Drausio dois', '2013-03-12', 1, 600.35

EXEC uspClienteExcluir 1
EXEC uspClienteExcluir 3
EXEC uspClienteExcluir 2