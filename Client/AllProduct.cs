﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class AllProduct
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required decimal Price { get; set; }

    }
}
