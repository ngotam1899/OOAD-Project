using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.State
{
    class Monthy : State
    {
        public Monthy(float money, DateTime date, int type)
        {
            currentTime = DateTime.Now;
            this.date = date;
            this.money = money;
            this.type = type;
        }
        public override float CacullatePer()
        {
            return (money / 1000000) * 16000;
        }

        public override float Handle(Interest interest)
        {
            float count = currentTime.Month - date.Month;

            count += 1;

            if(currentTime.Day <= date.Day)
            {
                count -= 1;
            }

            float onemonth = this.CacullatePer();

            return onemonth * count;

        }
    }
}
