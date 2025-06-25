using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class MachineStore
{
    [Display(Name = "رقم الماكينة")]
    public int MachineStoreId { get; set; }

    [Display(Name = "اسم الماكينة")]
    [Required(ErrorMessage = "يجب ادخال اسم الماكينة")]
    public string Name { get; set; } = null!;

    [Display(Name = "التفعيل")]

    public bool? Activated { get; set; }

    public virtual ICollection<PerpetualRequisiteOrder> PerpetualRequisiteOrders { get; set; } = new List<PerpetualRequisiteOrder>();
}
