using System;

namespace ValueAndReference {
    class Program {
        public static void Main(string[] args) {
            string s = "before passing";
            Console.WriteLine("original string: " + s);

            Test(s);
            Console.WriteLine("no use keyword: " + s);
            Test(ref s);
            Console.WriteLine("use ref keyword: " + s);
        }

        public static void Test(string s){
            s = "after passing";
        }

        public static void Test(ref string s){
            s = "after passing";
        }
    }
}

// 온라인 컴파일러
// https://www.mycompiler.io/ko/new/csharp
// original string: before passing
// no use keyword: before passing
// use ref keyword: after passing

// ref 키워드는 일반적인 비즈니스 로직 코드에서는 자주 쓰이지 않으며, 꼭 필요한 특수한 상황(struct 전달)에서만 제한적으로 사용
