# Cs-practice
C#/UnityC#の知っておきたかったこと.

## set/get
- 変数のSetter/Getterは、set/getプロパティで定義できる。
- => わざわざメソッド作る必要なし！
```
private int _n;
public int N {
  set {
    _n = value;
  }
  get {
    return _n;
  }
}
```
[実例：Scripts/InheritSample.cs](https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/InheritSample.cs)

## enum(列挙型)
- ある名前の定数をまとめて定義できる。
- => 状態判別、ID化等、特別な意味をもたせたい数値はここで定義しとくと◯
- switch-case文で分岐処理を設けたり、後述の辞書型と組み合わせたりする。
```
public enum GameState {
  Ready,
  Start,
  End,
  Result
}
```
```
public enum CharacterId {
  Human = 0,
  Dog   = 1,
  Cat   = 2,
  Zombi = 99
}
```
[実例：Scripts/SampleDictionary.cs](https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleDictionary.cs)

## Dictionary<TKey, TValue>(辞書型)
- **Keyに対応するValue**の形でリストを作れる(連想配列)。
- => インデックス指定でなく特定のKeyで値を取り出せる！めちゃ便利
- => Keyは前述のenumで定義しておくと◯
- アセットを入れないと、Unityエディタのインスペクタに表示されないので注意。
```
var _characterDictionary = new Dictionary<CharacterId, GameObject>() {
  { CharacterId.Human, _humanObj },
  { CharacterId.Dog,   _dogObj },
  { CharacterId.Cat,   _catObj },
  { CharacterId.Zombi, _zombiObj }
}
var _obj = _characterDictionary[CharacterId.Dog] // _objにはGameObject型_dogObjが代入される
```
[実例：Scripts/SampleDictionary.cs](https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleDictionary.cs)
