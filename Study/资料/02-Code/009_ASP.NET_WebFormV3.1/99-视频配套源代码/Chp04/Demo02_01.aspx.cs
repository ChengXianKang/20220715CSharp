using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo02_01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["HeroId"]))
        {
            return;
        }
        BindDetail();
    }

    private void BindDetail()
    {
        MyHeroDAL dal = new MyHeroDAL();
        int heroId = int.Parse(Request.QueryString["HeroId"]);
        MyHeroEntity entity = dal.Detail(heroId);
        this.lblName.Text = entity.HeroName;
        this.Image1.ImageUrl = "img/" + entity.HeroPic;
        this.lblSkill.Text = entity.HeroSkill;
        this.lblEquipment.Text = entity.HeroEquipment;
    }
}