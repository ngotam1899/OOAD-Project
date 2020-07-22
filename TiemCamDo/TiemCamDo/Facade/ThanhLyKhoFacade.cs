using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiemCamDo.BD_Layer;

namespace TiemCamDo.Facade
{
    //Lớp này để xóa mặt hàng khỏi csdl của cửa hàng
    // vì Mặt hàng tồn kho có liên quan đến phiếu cầm
    //Muốn xóa mặt hàng phải xóa Phiếu cầm đồ trước

    class ThanhLyKhoFacade
    {

        private BLMatHang matHang;
        private BLCamDo camDo;
        private BLChuocDo chuocDo;
        private BLTraGop traGop;

        private static ThanhLyKhoFacade instance;
        public static ThanhLyKhoFacade Instance
        {
            get { if (instance == null) instance = new ThanhLyKhoFacade(); return ThanhLyKhoFacade.instance; }
            private set { ThanhLyKhoFacade.instance = value; }
        }
        private ThanhLyKhoFacade()
        {
            matHang = BLMatHang.Instance;
            camDo = BLCamDo.Instance;
            chuocDo = BLChuocDo.Instance;
            traGop = BLTraGop.Instance;
        }
        //Chức năng của Admin
        public bool DeleteMatHang(string MaHang)
        {
            bool isSuccess = false;
            //Xóa phiếu cầm đồ
            isSuccess = this.camDo.DeleteCamDoFromMaHang(MaHang);
            //Xóa phiếu chuộc đồ
            isSuccess = this.chuocDo.DeleteChuocDoFromMaHang(MaHang);
            //Xóa phiếu trả góp
            isSuccess = this.traGop.DeleteTraGopFromMaHang(MaHang);
            //Xóa mặt hàng
            isSuccess = this.matHang.DeleteMH(MaHang);
            return isSuccess;
        }
    }
}
