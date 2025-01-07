﻿using UnityEngine;

namespace UnityX.Timer {
    /// <summary>
    /// A Timer that decreases over time and stops when it reaches zero.
    /// </summary>
    /// <param name="value">Initial countdown time in seconds.</param>
    public class CountdownTimer(float value) : Timer(value) {

        public override void Tick() {
            // Decreases the timer's current time by deltaTime each frame.
            if (IsRunning && CurrentTime > 0f) {
                CurrentTime -= Time.deltaTime;
            }

            // Stops when time reaches zero.
            if (IsRunning && CurrentTime <= 0f) {
                Stop();
            }
        }

        public override bool IsFinished => CurrentTime <= 0f;
    }
}
