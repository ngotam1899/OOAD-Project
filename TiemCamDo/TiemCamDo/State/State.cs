
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.State
{
    abstract class State
    {
        protected int type = 0;
        protected DateTime date;
        protected float money;
        protected DateTime currentTime;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public DateTime Date
        { 
            get { return date; } 
            set { date = value; }
        }

        public float Money
        {
            get { return money; }
            set { money = value; }
        }
        public abstract float Handle(Interest interest);
        public abstract float CacullatePer();
    }
}
