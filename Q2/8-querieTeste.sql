USE ProvaSafra
GO

--2a
select c.Nome, c.CPF from Clientes c inner join Estados e 
 on c.EstadoId = e.Id inner join Financiamentos f
 on f.CPF = C.CPF inner join Parcelamentos p
 on p.FinanciamentoId = f.Id 
where e.UF = 'SP' 
group by c.Nome, c.CPF
having count(case when p.DataPagamento is not null then p.NumeroParcela end) >= 0.6 * count(p.NumeroParcela)

--2b
select c.Nome, c.CPF, count(p.Id) as QtdParcelasAtrasadas from Clientes c inner join Estados e 
 on c.EstadoId = e.Id inner join Financiamentos f
 on f.CPF = C.CPF inner join Parcelamentos p
 on p.FinanciamentoId = f.Id 
where p.DataPagamento is null
 and p.DataVencimento < cast(GetDate() -5 as date)
group by c.Nome, c.CPF


