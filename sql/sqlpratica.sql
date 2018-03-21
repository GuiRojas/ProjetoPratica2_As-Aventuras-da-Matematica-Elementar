create table usuario(
	email varchar(50) primary key,
	nome  varchar(30) not null,
	senha ntext not null,
	progresso int 
	
	constraint chkProg
		check (progresso between 1 and 14)
)

create table desempenho(
	emUsuario varchar(50) primary key,
	fase int not null,
	tempo time not null

	constraint fkEmUs
		foreign key (emUsuario) references usuario(email),
	constraint chkFase
		check (fase between 1 and 7)
)

select * from usuario
