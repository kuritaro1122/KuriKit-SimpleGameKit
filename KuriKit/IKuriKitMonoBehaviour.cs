using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KuriKit {
    public interface IKuriKitMonoBehaviour {
        void KKUpdate(float uiDeltaTime, float gameDeltaTime);
        void KKOnLoadTitle();
        void KKOnLoadGame();
        void KKOnGameOver();
        void KKOnGameClear();
    }
}