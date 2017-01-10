create table Support
(
KeyID int identity(1,1) primary key,
Keysword nvarchar(200),
Title nvarchar(200),
Content text,
LookCount int not null default 0
)
Create table SupportGetRecord
(
KeyID int identity(1,1) primary key,
GetContent nvarchar(200),
SetSupportID int not null default 0,
AddTime datetime not null default getdate(),
GetIP nvarchar(20),
GetBrowserInfo nvarchar(200)
)
Create table SupportOperateRecord
(
KeyID int identity(1,1) primary key,
SupportID int not null default 0,
ManagerID int not null default 0,
AddTime datetime not null default getdate(),
TypeID int not null default 0,
Detail text
)