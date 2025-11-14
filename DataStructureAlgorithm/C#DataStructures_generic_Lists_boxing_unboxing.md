## 오늘의 자료구조 공부

Generic에는 arraylist, hashtable, stack, que가 있다.

```csharp
class DataClass<T>
{
	public T data;
	public T GetData()
	{
		return data
	}
}

GetComponent<Rigidbody2D>(); // 이런것도 제네릭임
```

**HashTable**

```csharp
HashTable myHT = new Hashtable();

myHT.Add("first", 187);
myHT.Add("second", 1.8f);
myHT.Add("third", "Star");

myHT.Remove("first");
```

**Dictionary<TKey, TValue>**

```csharp
Dictionary<string, int> myDict = new Dictionary<string, int>();
myDict.Add("first", 187);
myDict["second"] = 200;
myDict.Remove("first");`
```

- Hashtable보다 타입 안전성이 좋고 성능도 더 우수합니다
- 제네릭을 사용해서 박싱/언박싱 오버헤드가 없어요

**List<T>**

```csharp
List<int> myList = new List<int>();
myList.Add(10);
myList.Add(20);
myList.RemoveAt(0);
```

- 동적 배열로, 크기가 자동으로 조정됩니다.

**Queue<T>** (큐 - 선입선출)

```csharp
Queue<string> myQueue = new Queue<string>();
myQueue.Enqueue("first");
myQueue.Enqueue("second");
string item = myQueue.Dequeue(); *// "first"*
```

**Stack<T>** (스택 - 후입선출)

```csharp
Stack<int> myStack = new Stack<int>();
myStack.Push(1);
myStack.Push(2);
int top = myStack.Pop(); *// 2*
```

**HashSet<T>**

```csharp
HashSet<string> mySet = new HashSet<string>();
mySet.Add("unique");
mySet.Add("unique"); *// 중복은 추가되지 않음*
```

- 중복을 허용하지 않는 집합 자료구조입니다

**LinkedList<T>**

```csharp
LinkedList<int> myLinkedList = new LinkedList<int>();
myLinkedList.AddLast(10);
myLinkedList.AddFirst(5);
```

일반적으로 **Hashtable보다는 Dictionary<TKey, TValue>를 사용하는 것을 권장**합니다.

타입 안전성과 성능 면에서 더 우수하거든요!

---

박싱 언박싱

## 박싱 (Boxing)

**값 타입(value type)을 참조 타입(reference type)으로 변환하는 것**

```csharp
int num = 123;           *// 값 타입 (스택에 저장)*
object obj = num;        *// 박싱! (힙에 저장)*
```

값 타입인 `int`를 `object`(참조 타입)에 넣으면, CLR이 힙 메모리에 새로운 객체를 만들고 거기에 값을 복사해요. 이게 박싱입니다.

## 언박싱 (Unboxing)

**참조 타입을 다시 값 타입으로 변환하는 것**

```csharp
object obj = 123;        *// 박싱된 상태*
int num = (int)obj;      *// 언박싱!*
```

박싱된 객체를 다시 값 타입으로 꺼내는 과정이에요.

### 요약

```csharp
int a = 1;
object obj = a; //박싱

object obj = 1;
int a = (int)obj; //언박싱
```

## 왜 문제가 되냐면...

```csharp
*// Hashtable 예시 (박싱 발생)*
Hashtable ht = new Hashtable();
ht.Add("age", 25);       *// 25가 박싱됨 (int → object)*
int age = (int)ht["age"]; *// 언박싱 (object → int)// Dictionary 예시 (박싱 없음)*
Dictionary<string, int> dict = new Dictionary<string, int>();
dict.Add("age", 25);     *// 박싱 없음! 그냥 int 그대로*
int age = dict["age"];   *// 언박싱 없음!*
```

**성능 문제**

- 박싱/언박싱은 메모리 할당과 복사 작업이 필요해서 느려요
- 특히 반복문 안에서 수천~수만 번 발생하면 성능 차이가 확 나죠

**예시로 보면:**

```csharp
*// 나쁜 예 - 박싱이 10,000번 발생*
Hashtable ht = new Hashtable();
for(int i = 0; i < 10000; i++) {
    ht.Add(i.ToString(), i);  *// 매번 박싱!*
}

*// 좋은 예 - 박싱 없음*
Dictionary<string, int> dict = new Dictionary<string, int>();
for(int i = 0; i < 10000; i++) {
    dict.Add(i.ToString(), i);  *// 박싱 없음!*
}
```

그래서 요즘은 Hashtable 대신 Dictionary를 쓰는 거예요!
