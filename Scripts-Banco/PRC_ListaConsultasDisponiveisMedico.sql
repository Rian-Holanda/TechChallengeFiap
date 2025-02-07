create or alter procedure PRC_ListaConsultasDisponiveisMedico
@IdMedico int,
@DataConsulta datetime

AS

select 
	 historico.IdHorarioDia,
	 historico.DataConsulta
into #consultas_agendadas
from tb_Dia d
inner join tb_HorarioDia horarioDia on d.Id = horarioDia.IdDia
cross apply (select Horario from tb_Horario where Id = horarioDia.IdHorarioInicio) HorarioInicio
cross apply (select Horario from tb_Horario where Id = horarioDia.IdHorarioFim) HorarioFim
inner join tb_HistoricoConsulta historico on historico.IdHorarioDia = horarioDia.Id
inner join tb_Consulta consulta on consulta.Id = historico.IdConsuta and consulta.IdMedico = @IdMedico
inner join tb_Medico medico on medico.Id = consulta.IdMedico and medico.Id = @IdMedico
where YEAR(historico.DataConsulta) =  YEAR(@DataConsulta)
and   MONTH(historico.DataConsulta) = MONTH(@DataConsulta)
and   DAY(historico.DataConsulta) =    DAY(@DataConsulta)

select * from tb_HorarioDia horarioDia
where horarioDia.Id not in (select IdHorarioDia from #consultas_agendadas)



drop table #consultas_agendadas