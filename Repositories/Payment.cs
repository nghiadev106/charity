using ProjectFinal.Models;
using ProjectFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFinal.Repositories
{
    public interface IPayment
    {
        Order CreateOrder(Order order);
        Order GetOrderInfo(string OrderId);
        List<Order> GetListOrder();
        List<OrderViewModel> GetListOrderAdmin();
        OrderViewModel GetOrderDetail(string orderId);
    }
    public class Payment : IPayment
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        public Order CreateOrder(Order order)
        {
            var result = db.Order.Add(order);
            db.SaveChanges();
            return result;
        }

        public List<OrderViewModel> GetListOrderAdmin()
        {
            var query = from c in db.Order
                        join cc in db.Service on c.ServiceId equals cc.Id
                        where c.PaymentStatusId ==1
                        select new OrderViewModel()
                        {
                            OrderId = c.OrderId,
                            Name = c.Name,
                            TotalMoney = c.TotalMoney,
                            PaymentTypeId = c.PaymentTypeId,
                            Phone = c.Phone,
                            Address = c.Address,
                            Email = c.Email,
                            CreateDate = c.CreateDate,
                            ServiceName = cc.Name                           
                        };
            return query.OrderByDescending(y => y.CreateDate).ToList();
        }
        public OrderViewModel GetOrderDetail(string orderId)
        {
            var query = from c in db.Order
                        join cc in db.Service on c.ServiceId equals cc.Id
                        where c.PaymentStatusId == 1 && c.OrderId==orderId
                        select new OrderViewModel()
                        {
                            OrderId = c.OrderId,
                            Name = c.Name,
                            TotalMoney = c.TotalMoney,
                            PaymentTypeId = c.PaymentTypeId,
                            Phone = c.Phone,
                            Address = c.Address,
                            Email = c.Email,
                            CreateDate = c.CreateDate,
                            ServiceName = cc.Name
                        };
            return query.SingleOrDefault();
        }

        public List<Order> GetListOrder()
        {
            var lst = db.Order.Where(x => x.PaymentStatusId==1).OrderByDescending(y => y.CreateDate).Take(10).ToList();          
            return lst;
        }

        public Order GetOrderInfo(string OrderId)
        {
            var result = db.Order.SingleOrDefault(x => x.OrderId == OrderId);
            return result;
        }
    }
}