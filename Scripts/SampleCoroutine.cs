using System.Collections; // IEnumeratorの在り処
using UnityEngine;

namespace Assets.CsTrain.Samples
{
    /// <summary>
    /// コルーチン[1]のサンプルクラス
    /// </summary>
    public class SampleCoroutine : SingletonMonoBehaviour<SampleCoroutine> // シングルトン[2]を継承
    {
        protected override void Awake()
        {
            base.Awake(); // overrideしているので、親(スーパー)クラスのAwake()呼び出し
            DontDestroyOnLoad(this.gameObject); // シーン遷移してもこのオブジェクトは破棄しない <- シングルトンクラスはこのようにして存在し続けさせる
        }

        private void Start()
        {
            // コルーチンの呼び出し方↓
            StartCoroutine(PrintTextPerSecond(new string[]{"1", "2"}));
        }

        /// <summary>
        /// 1秒毎に文字列配列の中身を出力する
        /// </summary>
        /// <param name="texts"> 文字列配列 </param>
        public IEnumerator PrintTextPerSecond(string[] texts)
        {
            foreach(var item in texts)
            {
                print(item); // Debug.Log()と同義.typo減る/楽なのでおすすめ
                yield return new WaitForSeconds(1f); // 1秒待機
            }
        }
    }

    // 脚注：
    // [1] コルーチン ... 処理の中断/再開ができる
    // => Update()でやりたくてもできなかったこと(面倒だったこと)も簡単にできたり
    // (例：何秒(フレーム)後に〇〇をする. フェードイン/アウト

    // [2] シングルトン ... そのクラスのインスタンスは必ず1つですよ(staticですよ)
    // => 故にGameObject.Find()したりインスペクタから設置しなくても"アクセスできる"よ(名前空間は気にしてね)
    // 使い方：
    // var _sampleCoroutine = SampleCoroutine.Instance; <- この1行でクラスを参照できる
}