using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.Models
{
    // 如果Menu为部分类，且无法改变这个类，就简历新类MenuMetadata以代替Menu
    // 需要在Menu加上以下代码
    // [ModelMetadataType(typeof(MenuMetadata))]
    // public partial class Menu
    // { }
    public class Menu
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "Menu")]
        public string Text { get; set; }
        [Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(10)]
        public string Category { get; set; }
    }
}
