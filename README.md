# Cs-practice
C#/UnityC#の知っておきたかったこと.

## 導入
- 今までC#/UnityC#を触ってきた中で、個人的に早く知っておきたかった機能をまとめてみました。
- C#/UnityC#とは？？
  - C# => 一般的な普通のC#。
  - UnityC# => `using UnityEngine;`をすることで使用できるUnity用のC#。
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
- [導入](#導入)
### C#
- [ドキュメントコメント](#ドキュメントコメント)
- [set/get](#setget)
- [enum(列挙型)](#enum列挙型)
- [Dictionary<TKey,TValue>(辞書型)](#dictionarytkeytvalue辞書型)
- [コルーチン](#コルーチン)
### UnityC#
- [Attribute(属性)](#attribute属性)
- [MonoBehaviorの継承](#monobehaviorの継承)
- [シングルトン](#シングルトン)(デザインパターン)

# C#
## ドキュメントコメント
- ///(トリプルスラッシュ)ではじめる特殊なタグ付きコメント
- => **フォーマットが決まってるので必要な情報を書きやすい！**
- => ドキュメントコメント付きの変数、メソッド等の **参照箇所をマウスオーバーすると、コメント内容がポップアップ表示される。** 便利！
* `<summary>`
* `<param>`
ほかにも色々
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
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/Sample.cs" target="_blank">実例：Scripts/Sample.cs</a>

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
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/InheritSample.cs" target="_blank">実例：Scripts/InheritSample.cs</a>

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
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleDictionary.cs" target="_blank">実例：Scripts/SampleDictionary.cs</a>

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
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleDictionary.cs" target="_blank">実例：Scripts/SampleDictionary.cs</a>

### コルーチン
- **任意のタイミングで処理の中断/再開ができる機能。**
- returnを待たず、コルーチン内で処理を完結させる **非同期処理** の形。
- => 〇〇秒(frame)毎に何かをするとか、**時間絡みの処理はUpdate()より格段に書きやすい！**
```
void Start()
{
  var maxTime = 10;
  StartCoroutine(CountDownTimer(maxTime));  
}

public IEnumerator CountDownTimer(int maxTime)
{
  if(maxTime < 0) yield break; // 引数が0未満のときは抜ける

  for(int i = maxTime; i >= 0; i--)
  {
    print(i);
    yield return new WaitForSeconds(1f); // 1秒待機
  }

  print("End!");
}
```
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleCoroutine.cs" target="_blank">実例：Scripts/SampleCoroutine.cs</a>


# UnityC#
### Attribute(属性)
- フィールドに特定の属性を付与することで、Unityエディタの **インスペクタ表示をカスタマイズできる。**
* `[SerializeField]`：private変数でも表示(インスペクタ上でのみpublicな振る舞い)
* `[Header(string text)]`：*text*を変数の上部に表示
* `[Range(float min, float max)]`：変数値を操作できる*min~max*の範囲のスライダーを表示
など
- => **変更が多発しそうな画像とか音声ファイル** は、`[SerializeField]`付与しておけば **エディタ上で編集が完結する** ので便利！
- なぜ`[SerializeField]`を使う？？<br>
=> publicでもインスペクタに表示されるけど、他クラスからは見える必要ないはずなので！(カプセル化を意識)
```
[Header("UI要素")]
[SerializeField] private Transform _canvas; // 他クラスからは見えないが、インスペクタから割り当て可
[SerializeField, Header("スコア表示テキスト")] private Text _scoreText;
[SerializeField, Header("文字送り速度"), Range(0f, 1f)]
private float _textAnimationSpd = .5f;
```
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/Sample.cs" target="_blank">実例：Scripts/Sample.cs</a>
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleDictionary.cs" target="_blank">実例：Scripts/SampleDictionary.cs</a>

### MonoBehaviorの継承
- Unityで作成したC#スクリプトはデフォルトでMonoBehaviorクラスを継承している。
- MonoBehaviorを継承すると何が出来る？？
  => Start()、Update()とかUnity特有のメソッドが使える。
- パラメータ等値の保持が目的のクラスでは、**継承しないという選択肢もある！**
- => **授業で触れるJavaの感覚で使える**よ！
```
using System;
[Serializable] // 非MonoBehaviour継承のクラスの場合、付けないとインスペクタに表示されない
public class CharacterParameter
{
  private int _hpMax;
  private int _hpCurrent;
  private bool _isAlive;

  public CharacterParameter(int hpMax)
  {
    this._hpMax = hpMax;
    this._hpCurrent = hpMax;
    this._isAlive = true;
  }
}
```
```
public class Character : MonoBehaviour
{
  private CharacterParameter _parameter;

  void Start()
  {
    var hpMax = 100;
    _parameter = new CharacterParameter(hpMax);
  }
}
```
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/Sample.cs" target="_blank">実例：Scripts/Sample.cs</a>


### シングルトン
- 機能ではなくデザインパターン。
- 〇〇Managerとか**存在が1つであるべきクラス**のインスタンスは、**1つしか生成されない**ようにしよう！
- 1つしかないんだから**staticなクラス**にして、**どこからでも簡単にアクセスできる**ようにしよう！
という考え方に基づいて作られたフォーマット(ググるといくつか出てくる)が以下のコード。このクラスを継承して使う！
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SingletonMonoBehaviour.cs" target="_blank">Scripts/SingletonMonoBehaviour.cs</a>
- => クラス(コンポーネント)がアタッチされている**GameObject、およびコンポーネントの取得をせずに呼び出すことが出来る！**
```
public class GameManager : SingletonMonoBehaviour<GameManager>
{
  public void GameOver()
  {
    /* ゲームオーバー時の処理 */
  }
}
```
```
public class Player : MonoBehaviour
{
  public void Dead()
  {
    GameManager.Instance.GameOver(); // GameObject、Componentの取得不要
  }
}
```
<a href="https://github.com/Fusa-F/Cs-practice/blob/main/Scripts/SampleCoroutine.cs" target="_blank">実例：Scripts/SampleCoroutine.cs</a>
