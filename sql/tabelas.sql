create table usuario(
	email ntext primary key,
	nome varchar(30) not null,
	senha ntext not null, --senha será criptografada
	progresso int not null
	constraint chkProgresso
		check (progresso between 0 and 14)
)

create table desempenho(
	emUsuario ntext not null,
	fase int not null,
	tempo time not null
	constraint fkDesempenho 
		foreign key(emUsuario) references usuario(email)
	constraint chkFase
		check (fase between 0 and 7)
)

create proc adicionarUsuario_sp
@em ntext = null,
@nm varchar(30) = null,
@sn ntext = null,
@pr int = null
as
begin
	insert into usuario values(@em,@nm,@sn,@pr)
end

create proc adicionarTempo_sp
@em ntext = null,
@fs int = null,
@tp time = null
as
begin
	if(exists(select fase from desempenho where emUsuario = @em))
	begin
		if(@tp < (select tempo from desempenho where emUsuario = @em and fase = @fs))
		begin
			update desempenho set tempo = @tp where	emUsuario = @em and fase = @fs
		end
	end
	else
	begin
		insert into desempenho values(@em,@fs,@tp)
	end
end