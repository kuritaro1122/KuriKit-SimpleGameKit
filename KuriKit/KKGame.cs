using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriKit {
    public static class KKGame {
        //value
        public static KuriKitGameManager Manager => KuriKitGameManager.Instance;
        public static KuriKitGameManager.SceneStateEnum SceneState => Manager.SceneState;
        public static bool Pause => Manager.Pause;
        public static float UITimeScale => Manager.UITimeScale;
        public static float UIDeltaTime => Manager.UIDeltaTime;
        public static float GameTimeScale => Manager.GameTimeScale;
        public static float GameDeltaTime => Manager.GameDeltaTime;
        //scene
        public static void Title() => Manager.Title();
        public static void StartGame() => Manager.StartGame();
        public static void GameOver() => Manager.GameOver();
        public static void GameClear() => Manager.GameClear();
        //operation
        public static void QuitApp() => Manager.QuitApp();
        public static void SetPause(bool pause) => Manager.SetPause(pause);
        public static void SetTimeScale(float timeScale) => Manager.SetTimeScale(timeScale);
    }
}