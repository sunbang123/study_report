-- create a table
CREATE TABLE students (
  id INTEGER PRIMARY KEY,
  student_name TEXT NOT NULL,
  major_id INTEGER REFERENCES major(id),
  gender TEXT NOT NULL
);

-- create a table
CREATE TABLE major (
  id INTEGER PRIMARY KEY,
  major_name TEXT NOT NULL
);

-- insert some values
INSERT INTO students VALUES (10000001, 'Ryan', 1, 'M');
INSERT INTO students VALUES (10000002, 'Joanna', 2, 'F');
INSERT INTO students VALUES (10000003, 'Harry', 3, 'M');
INSERT INTO students VALUES (10000004, 'Ron', 3, 'M');
INSERT INTO students VALUES (10000005, 'Hermione', 3, 'F');
INSERT INTO students VALUES (10000006, 'Voldmort', NULL, 'M');
INSERT INTO major VALUES (01, 'computer');
INSERT INTO major VALUES (02, 'electric');
INSERT INTO major VALUES (03, 'magic');

-- fetch some values
-- cross join (암시적)
SELECT * FROM students, major;

-- INNER JOIN (Equi Join)
-- A
SELECT students.id, students.student_name,
    students.gender, major.major_name AS students_major
FROM students, major
WHERE students.major_id = major.id;
-- B
SELECT students.id, students.student_name,
    students.gender, major.major_name AS students_major
FROM students INNER JOIN major
ON students.major_id = major.id;

-- SELF JOIN
SELECT s1.id, s1.student_name, s1.major_id
FROM students AS s1, students AS s2
WHERE s1.major_id = s2.major_id
AND s2.id = 10000003;

-- LEFT OUTER JOIN
SELECT 
    students.id, 
    students.student_name,
    students.gender, 
    major.major_name AS students_major
FROM students 
LEFT OUTER JOIN major ON students.major_id = major.id;
-- 학생테이블을 기준으로 조회하므로 볼드모트는 조회되었다.

-- RIGHT OUTER JOIN
SELECT 
    students.id, 
    students.student_name,
    students.gender, 
    major.major_name AS students_major
FROM students 
RIGHT OUTER JOIN major ON students.major_id = major.id;
-- 전공테이블을 기준으로 조회하는데 볼드모트의 자료는 전공 테이블에 없으므로 조회되지 않았다.