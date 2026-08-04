# 이진검색트리가 자식 노드가 2개라면 삼진검색트리는 3개의 자식노드를가짐.
# 자식노드는 하나의 문자로 전체 문자열이 아님
# BSTree에서 크기가 작은건 왼쪽 크거나 같은것은 오른쪽노드 였음.
# TSTree에서는 왼쪽 중간 오른쪽 가지로, 보다작은것 같은것 큰것을 의미함.
# 트리의 끝까지 가면 찾고자 하는 키를 찾을 수 있고, 최악의 경우 모든 노드를 비교...
# 삼진검색트리 TST는 자동완성 및 검색어 추천이나 단어사전으로 쓰인다.
# 추천 공부방법: 복사하기 => 주석달기 => 요약하기 => 기억하기 => 구현하기 => 반복하기

# TSTree
class TSTreeNode(object):
    def __init__(self, key, value, low, eq, high):
        self.key = key
        self.low = low
        self.eq = eq
        self.high = high
        self.value = value


class TSTree(object):
    def __init__(self):
        self.root = None

    def _get(self, node, keys):
        key = keys[0]
        if key < node.key:
            return self._get(node.low, keys)
        elif key == node.key:
            if len(keys) > 1:
                return self._get(node.eq, keys[1:])
            else:
                return node.value
        else:
            return self._get(node.high, keys)

    def get(self, key):
        keys = [x for x in key]
        return self._get(self.root, keys)

    def _set(self, node, keys, value):
        next_key = keys[0]
        
        if not node:
            # 여기서 값을 바로 추가하면 어떻게 될까? 단어의 끝이 아닌 중간글자에 값이 저장된
            # 값을 너무 일찍 저장하면 엉뚱한 단어가 저장됨.
            node = TSTreeNode(next_key, None, None, None, None)

        if next_key < node.key:
            node.low = self._set(node.low, keys, value)
        elif next_key == node.key:
            if len(keys) > 1:
                node.eq = self._set(node.eq, keys[1:], value)
            else:
                # 여기서 값을 설정하지 않으면 어떻게 될까? 트리에 글자들(경로)는 만들어지지만 단어검색 못하게된다.
                # 마지막에 값 저장하지 않으면 유령단어가 된다.
                node.value = value
        else:
            node.high = self._set(node.high, keys, value)

        return node

    def set(self, key, value):
        keys = [x for x in key]
        self.root = self._set(self.root, keys, value)

    def _starts_with(self, node, keys):
        if not node:
            return False # 경로가 끊겼으므로 해당 부분 문자열은 없음
            
        key = keys[0]
        if key < node.key:
            return self._starts_with(node.low, keys)
        elif key == node.key:
            if len(keys) > 1:
                return self._starts_with(node.eq, keys[1:])
            else:
                return True # 찾고자 하는 부분 문자열의 끝까지 경로가 이어져 있음!
        else:
            return self._starts_with(node.high, keys)

    def starts_with(self, prefix):
        if not prefix:
            return True
        keys = [x for x in prefix]
        return self._starts_with(self.root, keys)
    
magic = "abracadabra"

# 접미사 배열 만드는 코드
magic_sa = []
for i in range(0, len(magic)):
    magic_sa.append(magic[i:])

magic_sa = sorted(magic_sa)

# 접미사를 TSTree에 넣는방법
tree = TSTree()

# 모든 접미사를 트리에 저장(value로는 원본 문자열의 시작 인덱스 i를 저장)
for i in range(0, len(magic)):
    tree.set(magic[i:], i)

print(f"'cad'가 포함되어 있나요?: {tree.starts_with('cad')}")
print(f"'bra'가 포함되어 있나요?: {tree.starts_with('bra')}")
print(f"'z'가 포함되어 있나요?: {tree.starts_with('z')}")

print("\n--- get() 메서드 활용 (정확히 일치하는 단어 찾기) ---")

# 1. 완벽하게 일치하는 접미사를 검색했을 때
# 'cadabra'는 'abracadabra'의 4번째 인덱스부터 시작하는 접미사로 저장되었습니다.
# 따라서 저장했던 값인 4가 출력됩니다.
print(f"'cadabra'의 시작 인덱스는?: {tree.get('cadabra')}") 

# 'dabra'는 6번째 인덱스부터 시작하므로 6이 출력됩니다.
print(f"'dabra'의 시작 인덱스는?: {tree.get('dabra')}")

# 2. 중간에 겹치는 단어를 검색했을 때
# 'abra'는 0번째에도 있고('abracadabra'), 7번째에도 있습니다('abra').
# 반복문이 0부터 돌면서 나중에 7번째 인덱스의 'abra'를 덮어씌웠기 때문에 7이 출력됩니다.
print(f"'abra'의 시작 인덱스는?: {tree.get('abra')}") 

# 3. 부분 문자열(접두사)을 검색했을 때
# 'cad'는 'cadabra'의 앞부분일 뿐, 'cad'라는 단어(접미사) 자체를 넣은 적은 없습니다.
# 정확히 일치하는 단어가 아니므로 None이 출력됩니다. (starts_with와 확실히 다른 점!)
print(f"'cad'의 시작 인덱스는?: {tree.get('cad')}")


# 온라인 컴파일러에서 실행
# https://www.mycompiler.io/ko/new/python