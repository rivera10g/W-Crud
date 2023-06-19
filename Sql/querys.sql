CREATE TABLE TBL_ALUMNO (
   ID INT PRIMARY KEY,
   NOMBRE VARCHAR(45),
   APELLIDOS VARCHAR(45),
   GENERO VARCHAR(45),
   F_NACIMIENTO VARCHAR(45)
);

CREATE TABLE TBL_PROFESOR (
   ID INT PRIMARY KEY,
   NOMBRE VARCHAR(45),
   APELLIDOS VARCHAR(45),
   GENERO VARCHAR(45)
);

CREATE TABLE TBL_GRADO (
   ID INT PRIMARY KEY,
   NOMBRE VARCHAR(45),
   PROFESOR_ID INT,
   FOREIGN KEY (PROFESOR_ID) REFERENCES TBL_PROFESOR(ID)
);


CREATE TABLE TBL_ALUMNO_GRADO (
   ID INT PRIMARY KEY,
   ALUMNO_ID INT,
   GRADO_ID INT,
   SECCION VARCHAR(45),
   FOREIGN KEY (ALUMNO_ID) REFERENCES TBL_ALUMNO(ID),
   FOREIGN KEY (GRADO_ID) REFERENCES TBL_GRADO(ID)
);


INSERT INTO TBL_ALUMNO (ID, NOMBRE, APELLIDOS, GENERO, F_NACIMIENTO)
VALUES (1, 'Carlos', 'Rivera', 'Masculino', '2001-05-13');
INSERT INTO TBL_ALUMNO (ID, NOMBRE, APELLIDOS, GENERO, F_NACIMIENTO)
VALUES (2, 'Ana', 'G�mez', 'Femenino', '1999-09-22');

INSERT INTO TBL_PROFESOR (ID, NOMBRE, APELLIDOS, GENERO)
VALUES (50, 'Jos�', 'Rivas', 'Masculino');
INSERT INTO TBL_PROFESOR (ID, NOMBRE, APELLIDOS, GENERO)
VALUES (51, 'Mar�a', 'L�pez', 'Femenino');

INSERT INTO TBL_GRADO (ID, NOMBRE, PROFESOR_ID)
VALUES (1, 'Grado 1', 50);
INSERT INTO TBL_GRADO (ID, NOMBRE, PROFESOR_ID)
VALUES (2, 'Grado 2', 51);

INSERT INTO TBL_ALUMNO_GRADO (ID, ALUMNO_ID, GRADO_ID, SECCION)
VALUES (1, 1, 1, 'A');
INSERT INTO TBL_ALUMNO_GRADO (ID, ALUMNO_ID, GRADO_ID, SECCION)
VALUES (2, 2, 1, 'B');


select top 10 * from TBL_ALUMNO
select top 10 * from TBL_PROFESOR
select top 10 * from TBL_GRADO
select top 10 * from TBL_ALUMNO_GRADO

update TBL_ALUMNO set APELLIDOS = 'Alvez' where ID = 2

delete from TBL_ALUMNO where ID =  5