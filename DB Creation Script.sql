create database RMS;
use RMS;
CREATE TABLE menuItems (
  Id INT not null auto_increment,
  Course VARCHAR(15) not null,
  Name VARCHAR(30) not null,
  Price DECIMAL(10, 2) not null,
  Availability BOOLEAN not null,
  Ingredients TEXT not null,
  Category TEXT not null,
  Description TEXT,
  StartTime TIME not null,
  EndTime TIME not null,
  Volume INT,
  AlcPerc DECIMAL(2, 2),
  PRIMARY KEY(Id)
);
CREATE TABLE category (
  Id INT not null auto_increment,
  Name VARCHAR(30) not null,
  Description Text,
  PRIMARY KEY(Id)
);
CREATE TABLE menus (
  Id INT not null auto_increment,
  Name varchar(30) not null,
  StartTime TIME not null,
  EndTime TIME not null,
  primary key (Id)
);
