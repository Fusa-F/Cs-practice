using System.Collections.Generic; // Dictionaryの在り処
using UnityEngine;

namespace Assets.CsTrain.Samples
{
    // 列挙型!! 激便利 <- 状態(state)判別とかIDとしての使い方が多いかも
    public enum SpriteId
    {
        Red,
        Green,
        Blue       
    }

    /// <summary>
    /// 辞書型のサンプルクラス
    /// </summary>
    public class SampleDictionary : MonoBehaviour
    {
        Dictionary<SpriteId, Sprite> _spriteList; // 辞書型<Key, Value> Keyに対応するValue の形でリスト化できる
        // Key -> 列挙型、Value -> 任意の型(変数でもコンポーネントでも画像でもAudioClipでもなんでも)　の形がおすすめ

        [Header("画像たち")] // [Header("")]属性 -> インスペクタに文字を表示することが出来る[1]
        [SerializeField] Sprite _red = null;   // インスペクタから画像をアタッチできるように[SerializeField] <- 画像の変更がUnityエディタ上で完結するので便利だよね
        [SerializeField] Sprite _green = null; // 初期値null <- Unityエディタから変数の初期化できてないよ警告が来るのでnullいれとくと◯
        [SerializeField] Sprite _blue = null;

        // Awake() Start()より早く実行される[2]
        private void Awake()
        {
            // 辞書の初期化
            _spriteList = new Dictionary<SpriteId, Sprite> {
                { SpriteId.Red, _red },
                { SpriteId.Green, _green },
                { SpriteId.Blue, _blue }
            };
            // 辞書の使い方：
            // var i = _spriteList[SpriteId.Red] => iにSprite型_redが代入される
        }

        /// <summary>
        /// 指定のスプライトをオブジェクトに適用する
        /// </summary>
        /// <param name="spriteId"> スプライトのID </param>
        public void ApplySprite(SpriteId spriteId)
        {
            var value = _spriteList[spriteId];
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = value;
        }
        // => メソッドを呼ぶときはただIDを指定すれば良い.という状態を作っておくほうがわかりやすいし安全そうだよね
    }

    // 脚注；
    // [1] インスペクタを変化させる属性はほかにもいくつか(下記以外にもある)
    // [SerializeField]: 変数の可視化/編集可能化(privateでも)
    // [Header("")]: 文字を表示
    // [Range(float min, float max)]: min~maxの範囲のスライダーを表示.変数をスライダーで調整可能化

    // [2] Awake() vs Start() ... 実行順は Awake > Start
    // Awake() -> 自クラスのみで完結する処理のとき (例：変数の初期化とか)
    // Start() -> 他クラスを扱う処理のとき (例：他クラスのインスタンス化とか)
    // Awake()は早いので、準備のできてない他クラスを触るとエラーでがち
}