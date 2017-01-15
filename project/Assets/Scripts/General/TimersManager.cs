using System;
using System.Collections.Generic;
using UnityEngine;

public class TimersManager : MonoBehaviour {

    public interface ITimer
    {
        void tick(float deltaTime);
        bool isFinished();
    }

    public abstract class AbstractTimer : ITimer
    {
        private bool isRunning = false;
        private bool finished = false;
        private float time = 1;

        public bool isFinished()
        {
            return finished;
        }

        public AbstractTimer(float time)
        {
            this.time = time;
        }

        public AbstractTimer(float time, bool running) : this(time)
        {
            this.isRunning = running;

        }

        public AbstractTimer run()
        {
            isRunning = true;
            return this;
        }

        public AbstractTimer stop()
        {
            isRunning = false;
            return this;
        }

        public void tick(float deltaTime)
        {
            if (!isRunning) return;

            time -= deltaTime;

            if (time < 0)
            {
                isRunning = false;
                finished = true;
                onFinish();
            }
        }

        protected abstract void onFinish();
    }

    public class CallbackTimer :AbstractTimer
    {
        private Action callback;
        public CallbackTimer(float time, Action callback) : base(time)
        {
            this.callback = callback;
        }

        public CallbackTimer(float time, bool running, Action callback) : base(time, running)
        {
            this.callback = callback;
        }

        protected override void onFinish()
        {
            callback();
        }
    }

    //public для просмотра в инспекторе
    public List<ITimer> timers = new List<ITimer>();

    private static TimersManager instance = null;

    public static TimersManager Instance
    {
        get { return instance; }
    }

    //Singleton
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
	void Update () {
	    for (int i = 0; i < timers.Count; i++)
	    {
	        var timer = timers[i];
	        timer.tick(Time.deltaTime);
	        if (timer.isFinished())
	        {
	            timers.RemoveAt(i--);
	        }
	    }
	}


    public void addTimer(ITimer timer)
    {
        timers.Add(timer);
    }
}
