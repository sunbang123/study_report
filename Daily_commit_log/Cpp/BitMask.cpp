// 각 요소의 포함 여부를 0 혹은 1로 나타내어 부분집합을 확인하는 방법
// 집합과 부분집합 체크하는 경우의 수 탐색에 유리함.
#include <iostream>
using namespace std;

enum Day
{
	Mon = 1 << 0,
	Tue = 1 << 1,
	Wed = 1 << 2,
	Thu = 1 << 3,
	Fri = 1 << 4,
	Sat = 1 << 5,
	Sun = 1 << 6,
};

int main()
{
	unsigned char studentA = 0;
	// |= 는 비트 OR와 대입연산자를 축약한것임. 비트를 추가하는역할
	studentA |= (Mon | Tue | Wed | Fri); // 00010111

	if(studentA & Wed)
		cout << "수요일 출석했다." << endl;

	cout << (int)studentA << endl;
}

// https://www.mycompiler.io/ko/new/cpp
//====================
// 출력결과
// 수요일 출석했다.
// 23
//====================
// 결과해석
// 23 = 16+4+2+1
// 23은 이 학생이 무슨요일에 출석했는지 역추적가능해짐.
// 이 학생은 월 화 수 금 요일에 출석했을것! Why? 이진수로 생각해보기.