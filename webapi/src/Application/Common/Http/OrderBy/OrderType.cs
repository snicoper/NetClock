using System.ComponentModel.DataAnnotations;

namespace NetClock.Application.Common.Http.OrderBy
{
    public enum OrderType
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "ASC")]
        Asc = 1,

        [Display(Name = "DESC")]
        Desc = 2
    }
}
