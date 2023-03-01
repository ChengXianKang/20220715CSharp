using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MyHeroEntity 的摘要说明
/// </summary>
public class MyHeroEntity
{
    public int HeroId { get; set; }  //编号
    public string HeroName { get; set; } //名字
    public string HeroPic { get; set; } //图片
    public string HeroSkill { get; set; } //技能介绍
    public string HeroEquipment { get; set; } //出装介绍
}