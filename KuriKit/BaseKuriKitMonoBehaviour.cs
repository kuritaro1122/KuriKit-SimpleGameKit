using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriKit {
    public abstract class BaseKuriKitMonoBehaviour : MonoBehaviour, IKuriKitMonoBehaviour {
        public virtual void Start() {
            KuriKit.KuriKitGameManager.Instance.AddKuriKitMonoBehaviours(this);
        }
        public virtual void KKUpdate(float uiDeltaTime, float gameDeltaTime) {}
        public virtual void KKOnLoadTitle() {}
        public virtual void KKOnLoadGame() {}
        public virtual void KKOnGameOver() {}
        public virtual void KKOnGameClear() {}
    }
}