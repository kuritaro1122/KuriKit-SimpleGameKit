using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriKit {
    [AddComponentMenu(MenuText + "/KuriKitGameManager")]
    public class KuriKitGameManager : MonoBehaviour {
        public const string MenuText = "KuriKit";
        public enum SceneStateEnum { GameScene, TitleScene, GameClearScene, GameOverScene }
        public static KuriKitGameManager Instance = null;
        //SceneState
        [Header("--- Status ---")]
        [SerializeField] SceneStateEnum _sceneState = SceneStateEnum.GameScene;
        public SceneStateEnum SceneState { get; private set; } = SceneStateEnum.GameScene;
        //Pause
        [SerializeField] bool _pause = false;
        public bool Pause { get; private set; } = false;
        [Header("--- KuriKit MonoBehaviours ---")]
        [SerializeField] List<BaseKuriKitMonoBehaviour> _kuriKitMonoBehaviours = new List<BaseKuriKitMonoBehaviour>();
        private List<IKuriKitMonoBehaviour> kuriKitMonoBehaviours = new List<IKuriKitMonoBehaviour>();
        //Time
        [SerializeField] float gameTimeScale = 1f;
        public float UITimeScale => Time.unscaledTime;
        public float UIDeltaTime => Time.unscaledDeltaTime;
        public float GameTimeScale => (this.Pause || this.SceneState != SceneStateEnum.GameScene) ? 0f : this.gameTimeScale;
        public float GameDeltaTime => Time.deltaTime;
        [Header("--- Scene Object ---")]
        [SerializeField] SceneObjectControl sceneObjectControl = new SceneObjectControl();
        [Header("--- Option ---")]
        [SerializeField] bool autoRestartOnStart = true;

        #region basic operation
        public void Title() => SetScene(SceneStateEnum.TitleScene);
        public void StartGame() => SetScene(SceneStateEnum.GameScene);
        public void GameOver() => SetScene(SceneStateEnum.GameOverScene);
        public void GameClear() => SetScene(SceneStateEnum.GameClearScene);
        public void QuitApp() {
            Application.Quit();
        }
        #endregion

        #region Detailed operation
        public void AddKuriKitMonoBehaviours(IKuriKitMonoBehaviour kuriKitMonoBehaviour) {
            if (!this.kuriKitMonoBehaviours.Contains(kuriKitMonoBehaviour)) {
                this.kuriKitMonoBehaviours.Add(kuriKitMonoBehaviour);
            }
        }
        public void SetScene(SceneStateEnum sceneState) {
            this.SceneState = this._sceneState = sceneState;
            SetPause(false);
            UpdateSceneGameObject();
            CallKuriKitMonoBehaviourOnState();
            UpdateTimeScale();
        }
        public void SetPause(bool pause) {
            this.Pause = this._pause = pause;
            UpdateSceneGameObject();
            UpdateTimeScale();
        }
        public void SetTimeScale(float timeScale) {
            this.gameTimeScale = timeScale;
            UpdateTimeScale();
        }
        #endregion

        #region MonoBehaviour function
        void Awake() {
            if (Instance != null) Debug.LogError($"KuriKitGameManager/GameManager is already exist.", this);
            KuriKitGameManager.Instance = this;
            foreach (var k in this._kuriKitMonoBehaviours) {
                AddKuriKitMonoBehaviours(k);
            }
        }
        void Start() {
            if (this.autoRestartOnStart) Title();
        }
        void Update() {
            CheckInspector();
            foreach (var k in this.kuriKitMonoBehaviours) {
                k.KKUpdate(uiDeltaTime: this.UIDeltaTime, gameDeltaTime: this.GameDeltaTime);
            }
        }
        void OnValidate() {
            CheckInspector();
        }
        #endregion

        private void CheckInspector() {
            if (this._sceneState != this.SceneState) SetScene(this._sceneState);
            if (this._pause != this.Pause) SetPause(this._pause);
        }
        private void CallKuriKitMonoBehaviourOnState() {
            System.Action<IKuriKitMonoBehaviour> action;
            switch (this.SceneState) {
                case SceneStateEnum.TitleScene:
                    action = k => k.KKOnLoadTitle();
                    break;
                case SceneStateEnum.GameScene:
                    action = k => k.KKOnLoadGame();
                    break;
                case SceneStateEnum.GameOverScene:
                    action = k => k.KKOnGameOver();
                    break;
                case SceneStateEnum.GameClearScene:
                    action = k => k.KKOnGameClear();
                    break;
                default:
                    action = k => { };
                    break;
            }
            foreach (var k in this.kuriKitMonoBehaviours) action(k);
        }
        private void UpdateTimeScale() {
            Time.timeScale = this.GameTimeScale;
        }
        private void UpdateSceneGameObject() {
            this.sceneObjectControl.UpdateSceneGameObject(this.SceneState, this.Pause);
        }

        [System.Serializable]
        class SceneObjectControl {
            /* 各シーンの時に表示されるGameObject.
             * ポーズ画面やゲームクリアテキストなど
             */
            [Header("--- Scene Object ---")]
            [SerializeField] GameObject Title = null;
            [SerializeField] GameObject Game = null;
            [SerializeField] GameObject GameClear = null;
            [SerializeField] GameObject GameOver = null;
            [Header("--- Pause Object ---")]
            [SerializeField] GameObject Pause = null;
            public void UpdateSceneGameObject(SceneStateEnum sceneState, bool pause) {
                if (this.Title != null) this.Title.SetActive(sceneState == SceneStateEnum.TitleScene);
                if (this.Game != null) this.Game.SetActive(sceneState == SceneStateEnum.GameScene);
                if (this.GameClear != null) this.GameClear.SetActive(sceneState == SceneStateEnum.GameClearScene);
                if (this.GameOver) this.GameOver.SetActive(sceneState == SceneStateEnum.GameOverScene);
                if (this.Pause != null) this.Pause.SetActive(pause);
            }
        }
    }
}