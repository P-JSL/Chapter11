using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public class Category
{
    //이 프로퍼티들은 DB 컬럼에 맵핑
    public int CategoryId { get; set; }

    [Required]
    [StringLength(15)]
    public string? CategoryName { get; set; } = null!;

    public string? Description { get; set; }

}
