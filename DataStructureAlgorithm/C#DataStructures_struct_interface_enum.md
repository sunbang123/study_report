추상클래스는 인스턴스화 할수 없다.
인터페이스는 다중상속의 문제를 해결하기 위해 등장함.

***인터페이스는 구현된 메서드가 없기 때문에, 다중상속이 가능***

그렇다고 다중상속을 하려고 쓰는건아니고

구현을 강제하려고 쓰는것임!!!

구조체의 생성자는 모든 인수로 값을 초기화해야댐

참조형과 값형

Reference type Value type

구조체 값형 이라서~

```csharp
Point_struct point1 = new Point_struct(1,1);
Point_struct point2 = point1;
point2.x = 2;
point2.y = 2;

Debug.Log(point1.GetPoint());
Debug.Log(point2.GetPoint());

// (1,1)
// (2,2)
```

Status는 Class에 담아

Struct는 애초에 값타입이였음

```c
Point *p2 = &p1;  // p2는 p1의 주소를 참조
// c언어에서도 이렇게안하면 기냥 값 복사나오는거임.
Point point2 = point1; // 값 복사
```

---

```csharp
// 네임스페이스는 클래스의 묶음

namespace A
{
	class A
	{
	
	}
}

namespace B
{
	class B
	{
	
	}
}
```

```csharp
using exampleNS;

ExampleClass Obj1 = new ExampleClass();
exampleNS.exampleClass obj2 = new ExampleClass();
```

[자료구조] 특: 여러개를 담을수있음. 자료구조마다 특징이있어서 꺼내쓰는것임.

배열

리스트

컬렉션 딕셔너리

| 분류 | 자료구조 이름 | 특징 |
| --- | --- | --- |
| **선형 구조** | 🔹 **배열(Array)** | 크기가 고정, 인덱스로 접근, 같은 타입의 데이터만 저장 |
|  | 🔹 **리스트(List)** | 크기가 가변, 삽입·삭제 쉬움 |
| **비선형 구조** | 🔹 **트리(Tree)** | 계층적 구조 (부모–자식 관계) |
|  | 🔹 **그래프(Graph)** | 노드들이 여러 방향으로 연결 |
| **연관 구조** | 🔹 **딕셔너리(Dictionary) / 해시맵(HashMap)** | 키(Key)로 값(Value)에 접근 |
| **기타** | 🔹 **스택(Stack)** | LIFO (후입선출) |
|  | 🔹 **큐(Queue)** | FIFO (선입선출) |

---

클래스 구조체 인터페이스는 자료타입을 여러개를 담잖아.

그래서 인덱서를 쓴대

```csharp
public int[] grades = {24, 75, 90, 74};

grades.grades[3] = 57; // 원래 이렇게

public int this[int index]
{
	get
	{
		return grades[index];
	}
	set
	{
		grades[index] = value;
	}
}

// 프로퍼티처럼 만든거.
Grade grades = new Grade(); // 배열이름

grades[3] = 57; // 인덱서쓰면 이렇게
```

---

열거형 enum

```csharp
enum Days
{
	Mon, Tue, Wed, Thu, Fri, Sat, Sun
}
// 변하지 않는 상수들을 열거한것임.
// 0 1 2 이렇게 꺼내써

// FSM 패턴 유한상태머신

(int)Days.Mon
(int)Days.Sat
// 0
// 5

enum Days
{
	Mon = 3, Tue, Wed
}

(int)Days.Mon
(int)Days.Tue

// 3
// 4
```

숫자에 의미 있는 이름을 부여하여 코드를 더 쉽게 읽고 이해할 수 있게 해줌.

애니메이터랑  비슷한결임.

Defines라고 한다음에 enum만 모아놓음.

배열의 순서와 같게 열거형을 선언해보자~!!

왜? 배열이 많은데 [1]이런식이면 뭘 불러온지 모를수있잖아. 특히 외부에서는.

(int)Days.Mon 아. 월요일 인덱스 불러오는거구나.

const!!

---
