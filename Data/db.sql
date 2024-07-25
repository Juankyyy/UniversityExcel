-- Active: 1721868051215@@bm3xnn3ofdxscr0uo078-mysql.services.clever-cloud.com@3306@bm3xnn3ofdxscr0uo078
CREATE TABLE Students (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Phone VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE Universities (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    DeanName VARCHAR(100) NOT NULL
);

CREATE TABLE Careers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    UniversityId INT NOT NULL,
    FOREIGN KEY (UniversityId) REFERENCES Universities(Id)
);

CREATE TABLE Inscriptions (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Status ENUM("Pendiente", "Aprobado", "Cancelado"),
    StudentId INT NOT NULL,
    UniversityId INT NOT NULL,
    CareerId INT NOT NULL,
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (UniversityId) REFERENCES Universities(Id),
    FOREIGN KEY (CareerId) REFERENCES Careers(Id)
);

CREATE TABLE Semesters (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    SemesterNumber INT NOT NULL,
    Year INT NOT NULL
);

CREATE TABLE Teachers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Phone VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE Subjects (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    TeacherId INT NOT NULL,
    CareerId INT NOT NULL,
    SemesterId INT NOT NULL,
    FOREIGN KEY (TeacherId) REFERENCES Teachers(Id),
    FOREIGN KEY (CareerId) REFERENCES Careers(Id),
    FOREIGN KEY (SemesterId) REFERENCES Semesters(Id)
);