using UnityEngine;

namespace UnityX.Timer {
    /// <summary>
    /// A Timer that counts up from zero to infinity.  
    /// Great for measuring durations.
    /// </summary>
    public class StopwatchTimer() : Timer(0f) {
        public override bool IsFinished => false;

        public override void Tick() {
            if (IsRunning) {
                CurrentTime += Time.deltaTime;
            }
        }
    }
}
