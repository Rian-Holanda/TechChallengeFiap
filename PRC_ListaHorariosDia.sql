create procedure PRC_ListaHorariosDia

As

select 
	d.Dia, 
	HorarioInicio.Horario, 
	HorarioFim.Horario 
from tb_Dia d
inner join tb_HorarioDia horarioDia on d.Id = horarioDia.IdDia
cross apply (select Horario from tb_Horario where Id = horarioDia.IdHorarioInicio) HorarioInicio
cross apply (select Horario from tb_Horario where Id = horarioDia.IdHorarioFim) HorarioFim