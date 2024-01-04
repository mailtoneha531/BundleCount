using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountBundle.Model
{
    public class BundleEntity
    {
        public int BundleEntityId { get; set; }
        public bool IsPairExist { get; set; } = false;
        public string Name { get; set; }
        public int InventoryCount { get; set; }
        public List<BundlePartEntity> Parts { get; set; }
    }
}
