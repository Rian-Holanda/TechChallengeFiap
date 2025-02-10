CREATE OR ALTER   procedure [dbo].[PRC_HorariosConsultas]

@IdMedico int

as

select
    medico.Nome as Medico,
	paciente.Nome as Paciente,
	consulta.DataMarcacaoConsulta,
	dia.Dia,
	HorarioInicio.Horario,
	historico.DataConsulta
from tb_Consulta consulta 
inner join tb_HistoricoConsulta historico on historico.IdConsuta = consulta.Id
inner join tb_HorarioDia horarioDia on horarioDia.Id = historico.IdHorarioDia
inner join tb_Dia dia on dia.Id = horarioDia.IdDia
cross apply (select Horario from tb_Horario where Id = horarioDia.IdHorarioInicio) HorarioInicio
inner join tb_Medico medico on medico.Id = consulta.IdMedico and medico.Id = @IdMedico
inner join tb_Paciente paciente on paciente.Id = consulta.IdPaciente
where consulta.IdMedico = @IdMedico
and consulta.ConsultaAprovada = 1