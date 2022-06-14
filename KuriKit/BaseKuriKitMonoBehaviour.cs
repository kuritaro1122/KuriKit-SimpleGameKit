using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriKit {
    public abstract class BaseKuriKitMonoBehaviour : MonoBehaviour, IKuriKitMonoBehaviour {
        public virtual void Start() {
            KuriKit.KuriKitGameManager.Instance.AddKuriKitMonoBehaviours(this);
        }
        public virtual void KKReset() {

        }
        public virtual void KKStartGame() {

        }
        public virtual void KKGameOver() {

        }
        public virtual void KKGameClear() {

        }
    }
}