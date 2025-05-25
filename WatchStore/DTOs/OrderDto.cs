using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderDto(int Id, int UserId, DateOnly OrderDate, double OrderSum, List<OrderItemDto> Products);

}
