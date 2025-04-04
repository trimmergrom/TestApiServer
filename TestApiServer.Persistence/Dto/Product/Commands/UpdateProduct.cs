using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiServer.Persistence.Dto.Product.Commands
{
    public class UpdateProduct
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required decimal Price { get; set; }

        public required int IdProductCategory { get; set; }
    }
}
