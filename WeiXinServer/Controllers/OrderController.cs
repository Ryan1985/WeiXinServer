using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeiXinBiz;
using WeiXinBiz.DataBase;
using WeiXinBiz.Models;

namespace WeiXinServer.Controllers
{
    public class OrderController : ApiController
    {
        private OrderBiz _orderBiz = new OrderBiz();

        public QueryResultModel<Order> Get(string OpenId)
        {
            //根据OPENID查询订单
            var orders =
                _orderBiz.QueryByFilterString(string.Format(@" AND OpenId = '{0}' and Status in ({1})", OpenId,
                    "0,1,2,3"));

            if (orders.Count == 0)
            {
                return new QueryResultModel<Order>
                {
                    ErrorMessage = string.Empty,
                    HasError = false,
                    QueryResult = new Order()
                };
            }

            if (orders.Count > 1)
            {
                var result = orders.OrderByDescending(o => o.CreateTime).First();
                return new QueryResultModel<Order>
                {
                    ErrorMessage = string.Empty,
                    HasError = false,
                    QueryResult = result
                };
            }

            var order = orders.First();
            return new QueryResultModel<Order>
            {
                ErrorMessage = string.Empty,
                HasError = false,
                QueryResult = order
            };
        }


        public ActionResultModel<Order> Post([FromBody] Order model)
        {
            //根据OPENID查询订单
            var orders =
                _orderBiz.QueryByFilterString(string.Format(@" AND OpenId = '{0}' and Status in ({1})", model.OpenId,
                    "0,1,2,3"));

            if (!orders.Any())
            {
                var result = _orderBiz.Save(model, null);
                if (!string.IsNullOrEmpty(result))
                {
                    return new ActionResultModel<Order>
                    {
                        ActionResult = null,
                        ErrorMessage = result,
                        HasError = true
                    };
                }

                return new ActionResultModel<Order>
                {
                    ActionResult = model,
                    ErrorMessage = string.Empty,
                    HasError = false
                };
            }

            var order = orders.First();
            if (orders.Count > 1)
            {
                order = orders.OrderByDescending(o => o.CreateTime).First();
            }

            var saveResult = _orderBiz.Save(model, order);
            if (!string.IsNullOrEmpty(saveResult))
            {
                return new ActionResultModel<Order>
                {
                    ActionResult = null,
                    ErrorMessage = saveResult,
                    HasError = true
                };
            }

            return new ActionResultModel<Order>
            {
                ActionResult = model,
                ErrorMessage = string.Empty,
                HasError = false
            };

        }




    }
}
