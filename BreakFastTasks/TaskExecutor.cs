using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakFastTasks
{
    class TaskExecutor
    {
        #region תכונות
        private string name;
        private int timeInMiliSec;
        #endregion

        #region Properties
        public string Name { get { return this.name; } }
        #endregion

        #region אירועים
       //אירוע בתהליך - עדכון קצב התקדמות
       //המידע שיועבר בזמן ביצוע האירוע - מה אחוז ההתקדמות
        public event EventHandler<int> OnProgressUpdate;

        //אירוע סיום ביצוע- בסיום 
        //אין מידע נוסך שישמר על האירוע
        public event EventHandler OnFinish;
        #endregion

        //בנאי
        public TaskExecutor(string name, int ms)
        {
            this.timeInMiliSec = ms;
            this.name = name;
        }

      
       
        
        //מחקה זמן ביצוע פעולה
        public void Start()
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(this.timeInMiliSec / 10);

                #region מימוש אופציונלי
                //if (OnProgressUpdate != null)
                //    OnProgressUpdate(this, (i+1) * 10);
                #endregion

                OnProgressUpdate?.Invoke(this, i  * 10);
            }

            #region מימוש אופציונלי
            //if (OnFinish != null)
            //    OnFinish(this,new EventArgs());
            #endregion
            OnFinish?.Invoke(this, new EventArgs());
           
        }


    }

    class Omlette : TaskExecutor
    {
        public Omlette(string name) : base(name, 30000)
        {

        }
    }

    class Cucumber : TaskExecutor
    {
        public Cucumber(string name) : base(name, 5000)
        { }
    }

    class Tomato : TaskExecutor
    {
        public Tomato(string name) : base(name, 1000)
        { }
    }

    class Toast : TaskExecutor
    {
        public Toast(string name) : base(name, 30000)
        {

        }
    }

}

