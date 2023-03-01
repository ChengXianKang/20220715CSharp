using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GoodTypeEntity 的摘要说明
/// </summary>
public class GoodTypeEntity
{
    public int TypeId { get; set; }  //编号
    public int ParentId { get; set; } //父类编号
    public string TypeName { get; set; } //名称
}