# Cs-practice
C#/UnityC#の知っておきたかったこと.

## 導入
- 今までC#/UnityC#を触ってきた中で、個人的に早く知っておきたかった機能をまとめてみました。
- なぜまとめたか？？<br>
  => 教材サイト等のソースコードではあまり使われていない(まず動けば良いので)のに、**非常に便利な機能がたくさんある** から！
- 知ることのメリットは？？<br>
  => **出来ることの幅がすごく増える！** 複雑な処理、膨大な記述量が求められても、**可読性/保守性の高いコード** が書ける！
- 可読性/保守性の高いコード？？<br>
  - 適切な命名がされている。
  - 役割ごとに整理されている。(独立性が高い)
  - クラス同士の依存関係が少ない。(Unityは設計を怠ると依存関係多くなりがちだよね)
  - 拡張性がある。(特にゲーム/アプリ開発ではアプデとかで拡張する場面が多いと思う)　などなど

  => メンテナンス、デバッグがしやすい！**ソースコードの読解時に悩む時間が少ない！**<br>
  => **時間経過、チーム開発時で特に力を発揮する！** (先月自分が書いたコードとか、他人が書いたコードって読むの時間かかるよね)
- 注意点
  - チーム開発の場合、全員が使用技術を抑えておく必要がある。<br>
  => 便利な機能たちでも、暗号になりかねない。ただ、**学習コストに見合った利便性あり！**
- 設計も考慮すると(今勉強中です)さらに良いプロジェクトになるのですが、難しいので今回は自分が知って嬉しかったアツい機能紹介をメインにします。<br>

## 目次
### C#
- [ドキュメントコメント](#ドキュメントコメント)
- [set/get](#setget)
- [enum(列挙型)](#enum列挙型)
- [Dictionary<TKey, TValue>(辞書型)](#dictionarytkeytvalue辞書型)
### UnityC#
- 

# C#
## ドキュメントコメント
- ///(トリプルスラッシュ)ではじめる特殊なタグ付きコメント
- => **フォーマットが決まってるので必要な情報を書きやすい！**
- => ドキュメントコメント付きの変数、メソッド等の **参照箇所をマウスオーバーすると、コメント内容がポップアップ表示される。** 便利！
* `<summary>`
* `<param>`
* ... ほかにも色々
```
/// <summary>
/// プレイヤーを生成する
/// </summary>
/// <param name="status"> プレイヤーのステータス </param>
public void CreatePlayer(PlayerStatus status)
{
  /* 処理 */
}
```
[実例：Scripts/Sample.cs](https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/Sample.cs)

## set/get
- 変数のSetter/Getterは、set/getプロパティで定義できる。
- => **わざわざメソッド作る必要なし！**
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
- => 状態判別、ID化等、 **特別な意味をもたせたい数値はここで定義しとくと◯**
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

## Dictionary<TKey,TValue>(辞書型)
- **Keyに対応するValue**の形でリストを作れる(連想配列)。
- => インデックス指定でなく特定の **Keyで値を取り出せる！** めちゃ便利
- => **Keyは前述のenumで定義** しておくと◯
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
