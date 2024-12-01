using E_COMMERCE_APP.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Data.Entities
{
    public class Order
    {
        public int Id                                   { get; set; }
        public DateTime Date                            { get; set; }
        public string Status                            { get; set; }
        public decimal Price                            { get; set; }
        public string OrderNumber                       { get; set; }
        public bool IsDelivered => DateTime.Now >= Date.AddDays(9);
        public string UserId                            { get; set; }
        public virtual ApplicationUser applicationUser  { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
