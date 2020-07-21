using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.State
{
    class Daily : State
    { 
        public Daily(float money, DateTime date, int type)
        {
            currentTime = DateTime.Now;
            this.date = date;
            this.money = money;
            this.type = type;
        }

        public override float CacullatePer()
        {
            return (money / 1000000) * 550;
        }

        public override float Handle(Interest interest)
        {
            if(this.type==1)
            {
                interest.State = new Weekly(money, date, type);
                return interest.CaculateInterest();
            }
            else if(this.type==2)
            {
                interest.State = new Monthy(money,date,type);
                return interest.CaculateInterest();
            }

            TimeSpan time = currentTime - date;
            float count = time.Days;

            float oneday = this.CacullatePer();

            return oneday * count;

        }
    }
}
