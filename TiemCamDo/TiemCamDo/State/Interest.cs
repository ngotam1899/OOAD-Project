using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.State
{
    class Interest
    {
        private State _state;

        // Constructor

        public Interest(State state)
        {
            this.State = state;
        }

        // Gets or sets the state

        public State State
        {
            get { return _state; }
            set

            {
                _state = value;
            }
        }

        public float CaculateInterest()
        {
             return _state.Handle(this);
        }
    }
}
