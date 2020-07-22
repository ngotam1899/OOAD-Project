using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.State
{
    class Weekly : State
    {
        public Weekly(float money, DateTime date, int type)
        {
            currentTime = DateTime.Now;
            this.date = date;
            this.money = money;
            this.type = type;
        }

        public override float CacullatePer()
        {
            return (money / 1000000) * 3800;
        }

        public override float Handle(Interest interest)
        {
            TimeSpan time = currentTime - date;
            float count = time.Days;

            if(count % 7 >0)
            {
                count = (count / 7) + 1;
            }
            else
            {
                count = count / 7;
            }

            float oneweek = this.CacullatePer();

            return count * oneweek;
        }
    }
}
