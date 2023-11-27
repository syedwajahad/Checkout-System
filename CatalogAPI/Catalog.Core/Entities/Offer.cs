using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class Offer
    {
        public int offerId { get; set; }
        public int offerName { get; set; }
        public string offerType { get; set; }

        public int buyUnits { get; set; }
        public int getUnits { get; set; }

        public int productId { get; set; }

        public int getProductId { get; set; }

        public double discount { get; set; }

        public bool isExpired { get; set; }
        public bool isUnlimited { get; set; }

        public bool isApplied { get; set; }
    }
}
