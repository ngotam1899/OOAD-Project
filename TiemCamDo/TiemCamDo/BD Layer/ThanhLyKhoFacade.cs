using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemCamDo.BD_Layer
{
    //Lớp này để xóa mặt hàng khỏi csdl của cửa hàng
    // vì Mặt hàng tồn kho có liên quan đến phiếu cầm
    //Muốn xóa mặt hàng phải xóa Phiếu cầm đồ trước
    
    class ThanhLyKhoFacade
    {

        private BLMatHang matHang;
        private BLCamDo camDo;

        public ThanhLyKhoFacade(BLMatHang matHang, BLCamDo camDo)
        {
            this.matHang = matHang;
            this.camDo = camDo;
        }

        public bool ThanhLyKho(string MaHang)
        {
            bool isSuccess=false;
            //Xóa phiếu cầm đồ
            isSuccess = this.camDo.DeleteCamDoFromMaHangCD(MaHang);
            //Xóa mặt hàng
            isSuccess = this.matHang.DeleteMH(MaHang);

            return isSuccess;
        }
    }


}
