using System;
using System.Collections.Generic;

namespace Exercise
{
    class PriorityQueue
    {
        List<int> _heap = new List<int>();
        public void Push(int data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입한다.
            _heap.Add(data);

            int now = _heap.Count - 1;
            // 도장깨기 시작
            while(now > 0)
            {
                // 도장깨기 시도
                int next = (now - 1) / 2; // 부모의 인덱스
                if (_heap[now] < _heap[next])
                    break; // 실패
                // 두 값을 교체한다
                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 검사 위치를 이동한다.
                now = next;
            }
        }
        public int Pop()
        {
            // 반환할 데이터를 따로 저장
            int ret = _heap[0];
            // 마지막 데이터를 루트로 이동
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            // 역으로 내려가는 도장깨기 시작
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                // 왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (left <= lastIndex && _heap[next] < _heap[left])
                    next = left;
                // 오른값이 현재값보다 크면 오른쪽으로 이동
                if (right <= lastIndex && _heap[next] < _heap[right])
                    next = right;

                // 왼쪽/오른쪽 모두 현재값보다 작으면 종료
                if(next == now)
                    break;

                // 두 값을 교체한다
                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;
                // 검사 위치를 이동한다.
                now = next;
            }
            return ret;
        }

        public int Count()
        {
            return _heap.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue q = new PriorityQueue();

            q.Push(20);
            q.Push(10);
            q.Push(30);
            q.Push(90);
            q.Push(40);

            while (q.Count() > 0)
                Console.WriteLine(q.Pop());
        }
    }
}