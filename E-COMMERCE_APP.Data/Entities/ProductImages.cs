using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Data.Entities
{
    public class ProductImages
    {
        [Key]
        public int Id          { get; set; }
        public string ImgPath  { get; set; }
        public int ProductId   { get; set; }
        public Product product { get; set; }
    }
}
