using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriKit {
    public interface IKuriKitMonoBehaviour {
        void Start();
        void KKReset();
        void KKStartGame();
        void KKGameOver();
        void KKGameClear();
    }
}