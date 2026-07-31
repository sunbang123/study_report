/* SQLD 문제 */

-- 1. 테이블 생성
CREATE TABLE sales_q (
    dept VARCHAR(50),
    quarter_cd VARCHAR(10),
    amount INT
);

-- 2. 데이터 삽입
INSERT INTO sales_q (dept, quarter_cd, amount) VALUES
('SALES', 'Q1', 100),
('SALES', 'Q2', 120),
('DEV', 'Q1', 200),
('DEV', 'Q2', 180),
('HR', 'Q1', 90),
('HR', 'Q2', 110);

-- 3. Oracle sql
SELECT dept, Q1, Q2
FROM (
SELECT dept, quarter_cd, amount
FROM sales_q
)
PIVOT (
  SUM(amount)
FOR quarter_cd IN ('Q1' AS Q1, 'Q2' AS Q2)
);

-- 4. Mysql
SELECT 
    dept,
    SUM(CASE WHEN quarter_cd = 'Q1' THEN amount ELSE 0 END) AS Q1,
    SUM(CASE WHEN quarter_cd = 'Q2' THEN amount ELSE 0 END) AS Q2
FROM 
    sales_q
GROUP BY 
    dept;


/*
https://www.mycompiler.io/ko/new/sql
결과
=================
DEV|200|180
HR|90|110
SALES|100|120
*/

/*
결과해석
quarter_cd 값이 열로 전개된다.
ㄴ-> DEV의 경우 200은 quarter_cd의 Q1, 180은 quarter_cd의 Q2이므로!
행단위로 쌓여있었던 quarter_cd가 열 방향으로 회전(pivot)된 것

원본
DEV⁠ | ⁠Q1⁠ | ⁠200⁠ (행 1)
⁠DEV⁠ | ⁠Q2⁠ | ⁠180⁠ (행 2)

피벗함수는 from절에서 작동하고 내부적으로 q1 q2열을 미리 만들어둔다음
마지막으로 select구문을 실행함. sql은 FROM -> ... -> SELECT 이런 실행순서이다.
*/





