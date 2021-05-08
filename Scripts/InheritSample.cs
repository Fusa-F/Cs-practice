using System;
using UnityEngine;

namespace Assets.CsTrain.Samples
{
    /// <summary>
    /// 継承, カプセル化, Setter-Getterのサンプルクラス
    /// </summary>
    [Serializable] // インスペクタに表示されないので付与[1]
    public class InheritSample : Sample // Sampleクラスを継承
    {
        // エディタのインスペクタに表示され...
        /* 1. */ private float a = 1.0f; // ない -> private だから
        /* 2. */ float b = 2.0f;         // ない -> アクセス修飾子未定義の場合 private
        /* 3. */ public float c = 3.0f;  // る -> public だから
        /* 4. */ [SerializeField] private float d = 4.0f; // る -> [SerializeField] 属性だから(privateだとしても見える)[2]

        // 用途によるが、3.はおすすめしない(カプセル化[3])
        // おすすめ：　変数はprivate(1 or 2)でpublicなセッターゲッター変数を用意する↓↓↓
        public float A {
            set {
                a = value;
                /* if文とかの処理も挟める... 例)入力値valueが〇〇以下のときは未処理とか */
            }
            get {
                return a;
                /* 上に同じ */
            }
        }
        public float B { set { b = value; } get { return b; } } // ワンラインで書いても○
        // 使い方：
        // [セット] Sample.A = hoge;        <- a = hoge;
        // [ゲット] var fuga = Sample.A;    <- fuga = a;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text"> テキスト </param>
        /// <param name="a"> a値 </param>
        /// <param name="b"> b値 </param>
        /// <param name="c"> c値 </param>
        /// <param name="d"> d値 </param>
        /// <returns></returns>
        public InheritSample(string text, float a, float b, float c, float d) : base(text)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
    }

    // 脚注：
    // [1][2] [Serializable]と[SerializeField]の違い ... 属性付与の対象と、名前空間が違う
    // [Serializable]   -> System の中にある. MonoBehaviour非継承のclassに対して使用
    // [SerializeField] -> UnityEngine の中にある. 変数に対して使用
    // => 必要に応じてusingして使おう (UnityEngineはたしかデフォで書いてある)

    // [3] テキストエディタはカプセル化を補助してくれたり
    // (例：publicな変数メソッドは候補表示/補完されるけど、privateだと候補表示されない)
}