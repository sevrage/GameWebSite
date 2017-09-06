using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GWSiteClassLibrary.IMap m = GWSiteClassLibrary.Factory.CreateMapService();
        GWSiteClassLibrary.IScore s = GWSiteClassLibrary.Factory.CreateScoreService();
        GWSiteClassLibrary.IScore p = GWSiteClassLibrary.Factory.CreateScoreService();

        DataSet dsMList = null;
        DataSet dsMRank = null;
        //DataSet dsPRank = null;

        string user = (string)Session["userName"];
        string pass = (string)Session["passWord"];
        dsMList = m.GetAll(user, pass);
        //dsMRank = s.GetMapScoreRank(user, pass, int.Parse(dsMList.Tables[0].Rows[0].ItemArray[0].ToString()), 3, "DESC");
        //dsPRank = p.GetPlayerRank(user, pass, int.Parse(dsMList.Tables[0].Rows[0].ItemArray[0].ToString()), int.Parse(dsMList.Tables[0].Rows[0].ItemArray[0].ToString()), 2, "DESC");
        //dsScore = s.GetPlayerRank(user,pass,
        
        int size = dsMList.Tables[0].Rows.Count;
        // Criar o XML
        XElement root = new XElement("mapas");
        for (int i = 0; i < size; i++)
        {
            string[] x = new string[10];
            x[0] = dsMList.Tables[0].Rows[i].ItemArray[1].ToString();
            dsMRank = s.GetMapScoreRank(user, pass, int.Parse(dsMList.Tables[0].Rows[i].ItemArray[0].ToString()), 3, "DESC");
            // 1º Lugar
            x[1] = dsMRank.Tables[0].Rows[0].ItemArray[1].ToString();
            x[2] = dsMRank.Tables[0].Rows[0].ItemArray[2].ToString();
            //dsPRank = p.GetPlayerRank(user, pass, int.Parse(dsMList.Tables[0].Rows[i].ItemArray[0].ToString()), int.Parse(dsMList.Tables[0].Rows[i].ItemArray[0].ToString()), 2, "DESC");

            //x[3] = dsPRank.Tables[0].Rows[0].ItemArray[2].ToString();

            // 2º Lugar
            x[4] = dsMRank.Tables[0].Rows[1].ItemArray[1].ToString();
            x[5] = dsMRank.Tables[0].Rows[1].ItemArray[2].ToString();
            //x[6] = dsPRank.Tables[0].Rows[0].ItemArray[2].ToString();

            // 3º Lugar
            x[7] = dsMRank.Tables[0].Rows[2].ItemArray[1].ToString();
            x[8] = dsMRank.Tables[0].Rows[2].ItemArray[2].ToString();
            //x[9] = dsPRank.Tables[0].Rows[0].ItemArray[2].ToString();

            // Top 3 de cada mapa + 2 scores de cada jogador
            /*
            root.Add(new XElement("mapa", new XAttribute("nome", x[0]),
                new XElement("jogador", new XAttribute("nome", x[1]),
                    new XElement("score", x[2]), new XElement("score", x[3])),
                new XElement("jogador", new XAttribute("nome", x[4]),
                    new XElement("score", x[5]), new XElement("score", x[6])),
                new XElement("jogador", new XAttribute("nome", x[7]),
                    new XElement("score", x[8]), new XElement("score", x[9]))
                    ));*/

            // TOP 3 de cada mapa + 1 score de cada jogador
            root.Add(new XElement("mapa", new XAttribute("nome", x[0]),
                new XElement("jogador", new XAttribute("nome", x[1]),
                    new XElement("score", x[2])),
                new XElement("jogador", new XAttribute("nome", x[4]),
                    new XElement("score", x[5])),
                new XElement("jogador", new XAttribute("nome", x[7]),
                    new XElement("score", x[8]))
                    ));


        }
        string str = Server.MapPath(".");
        root.Save(str+@"\TopMap.xml");
   
    }
}
