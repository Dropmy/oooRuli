using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oooRuli
{
    public class ProductCell
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Image Image { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Manufacturer { get; set; }
    }
}
