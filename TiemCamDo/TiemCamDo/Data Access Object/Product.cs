using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.Data_Access_Object
{
    public class Product
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string State { get; set; }
        public string SocialID { get; set; }
        public Product(string id, string type, string name, string price, string state, string socialId)
        {
            this.ID = id;
            this.Type = type;
            this.Name = name;
            this.Price = price;
            this.State = state;
            this.SocialID = socialId;
        }
        public Product(DataRow row)
        {
            this.ID = row["Mã hàng"].ToString();
            this.Type = row["Loại hàng"].ToString();
            this.Name = row["Tên món hàng"].ToString();
            this.Price = row["Gía trị thực"].ToString(); 
            this.State = row["Tình trạng"].ToString();
            this.SocialID = row["CMND"].ToString();
        }
    }
}
