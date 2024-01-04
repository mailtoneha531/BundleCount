using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountBundle.Model
{
    public class BundlePartSubEntity
    {
        public int BundleSubEntityId { get; set; }
        public string Name { get; set; }

        public bool IsPairExist { get; set; } = false;
        public int InventoryCount { get; set; }

        public int BundlePartEntityId { get; set; }
        public BundlePartEntity BundlePartEntity { get; set; }
    }
}
