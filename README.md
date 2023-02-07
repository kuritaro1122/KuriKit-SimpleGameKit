# [KuriKit]

ゲームの画面を管理する。タイトル画面やゲーム画面、ゲームクリア画面をメソッド一つで切り替えられる。
ポーズ時には、自動的にタイムスケールを0にセットする。

# Requirement
* System.Collections.Generic
* UnityEngine
* System

# Usage
① KuriKitGameManagerを任意のオブジェクトにコンポーネント.（1シーンに一つのみ）\
② KuriKitGameManager以外のクラスを, MonoBehaviourの代わりにBaseKuriKitMonoBehaviourを継承.\
③ 自由にプログラムを組む.\
（baseメソッドを呼ぶことで, ゲーム開始時やタイトル遷移時に処理を行える. KKGameクラスを使ってシーンを切り替えたりする）

# DEMO
```cs
using UnityEngine;
using KuriKit;

public class PlayerMovement : BaseKuriKitMonoBehaviour {
    [SerializeField] float speed = 10f;
    public override void KKOnLoadTitle() {
        this.transform.position = new Vector3(0, this.transform.position.y, 0);
    }
    public override void KKUpdate(float uiDeltaTime, float gameDeltaTime) {
        switch (KKGame.SceneState) {
            case KuriKitGameManager.SceneStateEnum.TitleScene:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    KKGame.StartGame();
                }
                break;
            case KuriKitGameManager.SceneStateEnum.GameScene:
                Movement(gameDeltaTime);
                if (Input.GetKeyDown(KeyCode.Space)) {
                    KKGame.SetPause(!KKGame.Pause);
                }
                if (Input.GetKeyDown(KeyCode.Escape) && KKGame.Pause) {
                    KKGame.Title();
                }
                if (Input.GetKeyDown(KeyCode.A)) {
                    KKGame.GameClear();
                }
                if (Input.GetKeyDown(KeyCode.B)) {
                    KKGame.GameOver();
                }
                break;
            case KuriKitGameManager.SceneStateEnum.GameClearScene:
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    KKGame.Title();
                }
                break;
            case KuriKitGameManager.SceneStateEnum.GameOverScene:
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    KKGame.Title();
                }
                break;
        }
    }
    private void Movement(float deltaTime) {
        Vector3 pos = this.transform.position;
        pos.x += this.speed * Input.GetAxisRaw("Horizontal") * deltaTime;
        this.transform.position = pos;
    }
}
```

# [KuriKitGameManager]

# Contains

## Inspector
![img](/Img/inspector.png/)

## Public Variable
```
SceneStateEnum SceneState { get; }
bool Pause { get; }
float UITimeScale { get; }
float UIDeltaTime { get; }
float GameTimeScale { get; }
float GameDeltaTime { get; }
```
## Public Function
```cs
void Title()
void StartGame()
void GameOver()
void GameClear()
void QuitApp()
void AddKuriKitMonoBehaviours(IKuriKitMonoBehaviour kuriKitMonoBehaviour)
void SetScene(SceneStateEnum sceneState)
void SetPause(bool pause)
void SetTimeScale(float timeScene)
```

## Enum
```cs
SceneStateEnum { GameScene, TitleScene, GameClearScene, GameOverScene }
```

# [BaseKuriKitMonoBehaviour]

# Contains

## Public Function (virtual)
```cs
void Start() //no use!!
void KKUpdate(float uiDeltaTime, float gameDeltaTime)
void KKOnLoadTitle()
void KKOnGameOver()
void KKOnGameClear()
```

# [KKGame] （static class)

## Static Function
```cs
// values
KuriKitGameManager
SceneStateEnum SceneState
bool Pause
float UITimeScale
float UIDeltaTime
float GameTimeScale
float GameDeltaTime

// scene
void Title()
void StartGame()
void GameOver()
void GameClear()

// operation
void QuitApp()
void SetPause(bool pause)
void SetTimeScale(float timeScale)
```

# Note

* BaseKuriKitMonoBehaviourのメソッドはオーバーライドして使ってください. newステートメントで上書きしたり, KuriKitGameManager以外でメソッドを呼ぶと, 予期せぬ動きをする可能性があります.
* timeScaleにはUI用とGame用の2種類を用意しています. UI用は常に Time.unscaledTim eを返しますが, Game用は通常 Time.timeScale を返し, Pause時やGameScene以外の時には 0f を返します.


# License

"KuriKit" is under [The Unlicense](https://ja.wikipedia.org/wiki/Unlicense).
