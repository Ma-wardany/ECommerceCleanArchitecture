using E_COMMERCE_APP.Data.Enums;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace E_COMMERCE_APP.Services.BackGroundServices
{
    public class SetOrderStatusServices : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SetOrderStatusServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) // Loop until cancellation
            {
                // Create a scope to resolve scoped services
                using (var scope = _serviceProvider.CreateScope())
                {
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    await UpdateOrderStatus(orderRepository);
                }

                // Wait for 1 hour (or desired delay)
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task UpdateOrderStatus(IOrderRepository orderRepository)
        {
            var orders = orderRepository.GetTableAsTracking().ToList(); // Ensure proper execution

            foreach (var order in orders)
            {
                if (order.Status == "Pending" &&
                    (DateTime.UtcNow - order.Date).TotalDays >= 3)
                {
                    order.Status = "Shipped";
                    await orderRepository.UpdateAsync(order);
                }
                else if (order.Status == "Shipped" &&
                         (DateTime.UtcNow - order.Date).TotalDays >= 5)
                {
                    order.Status = "Delivered";
                    await orderRepository.UpdateAsync(order);
                }
            }
        }
    }

}
