# [KuriKit]

ゲームの画面を管理する。タイトル画面やゲーム画面、ゲームクリア画面をメソッド一つで切り替えられる。
ポーズ時には、自動的にタイムスケールを0にセットする。

<!--# DEMO

-->


# Requirement

* System.Collections.Generic
* UnityEngine
* System

# Usage

<!--
① EntityStatus.cs を任意のGameObjectにコンポーネントして、Tagに「Entity」を追加\
② RigidBodyとColliderをコンポーネント\
③ EntityStatusのパラメータを調整

※「Entity」タグが追加されていれば、Start()時に自動的にタグが変更されます。\
※ RigidBodyのisTriggerはtrueでもfalseでも問題なく動作します。
-->
① KuriKitGameManagerを任意のオブジェクトにコンポーネント.（1シーンに一つのみ）\
② KuriKitGameManager以外のクラスを, MonoBehaviourの代わりにBaseKuriKitMonoBehaviourを継承.\
③ 自由にプログラムを組む.\
（baseメソッドを呼ぶことで, ゲーム開始時やタイトル遷移時に処理を行える. KKGameクラスを使ってシーンを切り替えたりする）

# [KuriKitGameManager]

# Contains

## Inspector

--

## Public Variable
```
SceneStateEnum SceneState { get; }
bool Pause { get; }
float UITimeScale { get; }
float UIDeltaTime { get; }
float GameTimeScale { get; }
float GameDeltaTime { get; }
```
## public Function
```
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
```
SceneStateEnum { GameScene, TitleScene, GameClearScene, GameOverScene }
```

# [BaseKuriKitMonoBehaviour]

# Contains

## public Function (virtual)
```
void Start() //no use!!
void KKUpdate(float uiDeltaTime, float gameDeltaTime)
void KKOnLoadTitle()
void KKOnGameOver()
void KKOnGameClear()
```

# [KKGame] （static class)

## static Function
```
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
