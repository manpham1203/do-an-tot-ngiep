using BO.ViewModels.OrderDetail;
using DAL.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.OrderDetail
{
    public class OrderDetailBLL
    {
        private OrderDetailDAL detailDAL;
        public OrderDetailBLL()
        {
            detailDAL = new OrderDetailDAL();
        }
        public async Task<bool> Create(List<OrderDetailVM> model)
        {
            try
            {
                //model.CreatedAt = DateTime.Now;
                return await detailDAL.Create(model);
            }
            catch
            {
                return false;
            }
        }
    }
}
