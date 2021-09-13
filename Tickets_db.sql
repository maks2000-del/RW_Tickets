create database Tickets_db;
go
use Tickets_db;
go
create table Users
(
id int primary key identity(1,1),
firstName nvarchar(30),
secondName nvarchar(30),
patronymic nvarchar(30),
mail nvarchar(50),
password nvarchar(100),
telNumber varchar(20),
date_of_birth varchar(20),
passport_id varchar(20),
sex nchar(6) check (sex in ('male', 'female')),
privilege nvarchar(5) default 'user'
);
go
create table Citys
(
id int primary key identity(0,1),
city nvarchar(50)
);
go
create table Seat_types
(
id int primary key identity(0,1),
seat_type nvarchar(50)
);
go
create table Voyages
(
id int primary key identity(1,1),
name nvarchar(50),
[id_city_​​of_departure] int foreign key references Citys(id),
date_of_departure varchar(20),
time_of_departure varchar(8),
[id_city_​​of_arrival] int foreign key references Citys(id),
date_of_arrival varchar(20),
time_of_arrival varchar(8),
cost money
);
go
create table Seats
(
id int primary key identity(1,1),
voyage_id int foreign key references Voyages(id),
type_of_seat int foreign key references Seat_types(id),
num_of_seat int,
num_of_carriage int,
is_free bit
);
go
create table Tickets
(
id int primary key identity(1,1),
client_id int foreign key references Users(id),
voyage_id int foreign key references Voyages(id),
seat_id int foreign key references Seats(id),
order_date varchar(20),
order_time varchar(20)
);

go
insert into Citys (city) values('Все');
insert into Citys (city) values('Брест');
insert into Citys (city) values('Витебск');
insert into Citys (city) values('Гомель');
insert into Citys (city) values('Гродно');
insert into Citys (city) values('Минск');
insert into Citys (city) values('Могилёв');

insert into Seat_types (seat_type) values('Все');
insert into Seat_types (seat_type) values('Эконом класс');
insert into Seat_types (seat_type) values('Бизнес класс');
insert into Seat_types (seat_type) values('Плацкарт');
go
insert into Users (firstName,secondName,patronymic,mail,password,telNumber,date_of_birth,passport_id,sex,privilege)
values ('admin', 'admin', 'admin', 'admin@mail.ru','ISMvKXpXpadDiUoOSoAfww==','+375292807908','2001-06-06', 'AB1111111', 'male', 'admin');

go
insert into Voyages(name,[id_city_​​of_departure],date_of_departure,time_of_departure,[id_city_​​of_arrival],date_of_arrival,time_of_arrival,cost)
values ('Брест-Минск', 1, '2001-06-06', '11:20:00', 5,'2001-06-06', '15:20:00', 300);
go
use Tickets_db
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 1, 1, 1, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 1, 2, 1, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 1, 3, 1, 'True');

insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 2, 1, 2, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 2, 2, 2, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 2, 3, 2, 'True');

insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 3, 1, 3, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 3, 2, 3, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (1, 3, 3, 3, 'True');

insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (2, 1, 1, 1, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (2, 2, 1, 2, 'True');
insert into Seats (voyage_id,type_of_seat,num_of_seat,num_of_carriage,is_free) values (2, 3, 1, 3, 'True');