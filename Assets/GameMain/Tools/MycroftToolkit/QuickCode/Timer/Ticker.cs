using System;
using QuickGameFramework.Runtime;

namespace MycroftToolkit.QuickCode {
    public class Ticker {
        /// <summary>
        /// 当前执行次数
        /// </summary>
        public int CurrentExecuteTime;
        
        /// <summary>
        /// 目标执行次数
        /// </summary>
        public int TargetExecuteTime;
        
        /// <summary>
        /// 剩余执行次数
        /// </summary>
        public int RemainingExecuteTime => IsLoop ? -1 : TargetExecuteTime - CurrentExecuteTime;
        
        /// <summary>
        /// 当前计数
        /// </summary>
        public int CurrentTickTime => _ticks + CurrentExecuteTime * Interval;

        /// <summary>
        /// 目标计数
        /// </summary>
        public float TargetTickTime => IsLoop ? -1 : TargetExecuteTime * Interval;

        /// <summary>
        /// 剩余计数
        /// </summary>
        public float RemainingTime => IsLoop? -1: _ticks + RemainingExecuteTime * Interval;

        public int Interval {
            get => _interval;
            set {
                if (value == _interval) return;
                if (value < 0) {
                    QLog.Error($"Ticker>间隔时间(次数)不合法! interval:{_interval}");
                    return;
                }

                _interval = value;
            }
        }
        
        public bool IsPause { get; private set; }
        public bool IsFinish { get; private set; }
        public bool IsCancel { get; private set; }
        public bool IsLoop;


        public Action<int> OnTick;
        public Action<int> OnExecute;
        public Action OnPause;
        public Action OnResume;
        public Action OnCancel;
        public Action OnFinish;
        
        private int _ticks;
        private int _interval;

        /// <summary>
        /// 循环手动计数执行器
        /// </summary>
        /// <param name="interval">执行间隔(次数)</param>
        public Ticker(int interval) {
            IsLoop = true;
            IsPause = false;
            IsFinish = false;
            IsCancel = false;
            Interval = interval;
            TargetExecuteTime = -1;
        }

        /// <summary>
        /// 手动计数执行器
        /// </summary>
        /// <param name="targetExecuteTime">目标执行数(次数)</param>
        /// <param name="interval">执行间隔(次数)</param>
        public Ticker(int targetExecuteTime, int interval) {
            IsLoop = false;
            IsPause = false;
            IsFinish = false;
            Interval = interval;
            TargetExecuteTime = targetExecuteTime;
            _ticks = 0;
            CurrentExecuteTime = 0;
        }
        
        public void DoTick() {
            if (IsCancel || IsPause || IsFinish) return;
            _ticks++;
            OnTick?.Invoke(CurrentTickTime);
            if (_ticks < Interval) return;
            
            _ticks = 0;
            CurrentExecuteTime++;
            if (CurrentExecuteTime == int.MaxValue) {
                CurrentExecuteTime = 0;
            }
            OnExecute?.Invoke(CurrentExecuteTime);
            if (IsLoop || CurrentExecuteTime != TargetExecuteTime) return;
            
            IsFinish = true;
            OnFinish?.Invoke();
        }
        public void Pause() {
            if (IsCancel || IsFinish || IsPause) return;
            IsPause = true;
            OnPause?.Invoke();
        }
        public void Resume() {
            if (IsCancel || IsFinish || !IsPause) return;
            IsPause = false;
            OnResume?.Invoke();
        }
        public void Cancel() {
            IsCancel = true;
            OnCancel?.Invoke();
        }
    }
}
