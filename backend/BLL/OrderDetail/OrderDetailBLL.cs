using BLL.Product;
using BO.ViewModels.OrderDetail;
using BO.ViewModels.Product;
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
        private CommonBLL cm;
        public OrderDetailBLL()
        {
            detailDAL = new OrderDetailDAL();
        }
        public async Task<bool> Create(List<OrderDetailVM> model, string orderId)
        {
            try
            {
                cm = new CommonBLL();
                for (int i = 0; i < model.Count; i++)
                {
                    model[i].Id = cm.RandomString(12);
                    model[i].OrderId = orderId;
                    model[i].CreatedAt = DateTime.Now;
                }
                return await detailDAL.Create(model);
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<OrderDetailVM>> GetListDetailByOrderId(string orderId)
        {
            try
            {
                var resulFromDAL = await detailDAL.GetListDetailByOrderId(orderId);
                if (resulFromDAL == null)
                {
                    return null;
                }
                if (resulFromDAL.Count == 0)
                {
                    return new List<OrderDetailVM>();
                }
                var productBLL = new ProductBLL();
                for (int i = 0; i < resulFromDAL.Count; i++)
                {

                    var products = await productBLL.GetProductByOrderDetail(resulFromDAL[i].ProductId);
                    if (products == null)
                    {
                        continue;
                    }
                    resulFromDAL[i].ProductOrderVM = products;
                }
                return resulFromDAL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderDetailVM>> GetListDetailByOrderIdUserId(string orderId, string userId)
        {
            try
            {
                var resulFromDAL = await detailDAL.GetListDetailByOrderId(orderId);
                if (resulFromDAL == null)
                {
                    return null;
                }
                if (resulFromDAL.Count == 0)
                {
                    return new List<OrderDetailVM>();
                }
                var productBLL = new ProductBLL();
                for (int i = 0; i < resulFromDAL.Count; i++)
                {

                    var products = await productBLL.GetProductByOrderDetail(resulFromDAL[i].ProductId);
                    if (products == null)
                    {
                        continue;
                    }
                    resulFromDAL[i].ProductOrderVM = products;

                    var commented = await CheckCommented(userId, products.Id , resulFromDAL[i].Id);
                    if (commented)
                    {
                        resulFromDAL[i].ProductOrderVM.Commented = true;
                    }
                    else
                    {
                        resulFromDAL[i].ProductOrderVM.Commented = false;

                    }
                }



                return resulFromDAL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<OrderDetailVM>> OrderedProduct(string orderId)
        {
            try
            {
                var resulFromDAL = await detailDAL.GetListDetailByOrderId(orderId);
                if (resulFromDAL == null)
                {
                    return null;
                }
                if (resulFromDAL.Count == 0)
                {
                    return new List<OrderDetailVM>();
                }
                var productBLL = new ProductBLL();
                for (int i = 0; i < resulFromDAL.Count; i++)
                {
                    var products = await productBLL.CartRow(resulFromDAL[i].ProductId);
                    if (products == null)
                    {
                        continue;
                    }
                    resulFromDAL[i].CartRowVM = products;
                }
                return resulFromDAL;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CheckCommented(string userId, string productId, string detailId)
        {
            try
            {
                return await detailDAL.CheckCommented(userId, productId, detailId);
            }
            catch
            {
                return true;
            }
        }

    }
}
