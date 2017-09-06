using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            DisplayStatus();
    }
    private void DisplayStatus()
    {
        // Open file.
        Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        // Display the current connection string.
        Label1.Text = config.ConnectionStrings.ConnectionStrings["MyConn"].ConnectionString;

        ConfigurationSection objConfigSection = config.ConnectionStrings;
        if (config.ConnectionStrings.SectionInformation.IsProtected)
        {
            Label2.Text = "Encrypted";
        }
        else
        {
            Label2.Text = "Decrypted";
        }
    }

    // Enable ConnectionStringsSection Encryption
    private void EncryptConfigSection()
    {
        Configuration objConfig = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        ConfigurationSection objConfigSection = objConfig.ConnectionStrings;
        //ConfigurationSection objConfigSection = objConfig.GetSection("connectionStrings");
        if (!objConfigSection.SectionInformation.IsProtected)
        {
            objConfigSection.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
            objConfigSection.SectionInformation.ForceSave = true;
            objConfig.Save(ConfigurationSaveMode.Modified);
        }
        DisplayStatus();
    }

    // Disable ConnectionStringsSection Encryption
    private void DecryptConfigSection()
    {
        Configuration objConfig = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        ConfigurationSection objConfigSection = objConfig.ConnectionStrings;
        //ConfigurationSection objConfigSection = objConfig.GetSection("connectionStrings");

        if (objConfigSection.SectionInformation.IsProtected)
        {
            objConfigSection.SectionInformation.UnprotectSection();
            objConfigSection.SectionInformation.ForceSave = true;
            objConfig.Save(ConfigurationSaveMode.Modified);
        }
        DisplayStatus();
    }

    // Encrypt web.config connectionString section
    protected void Button1_Click1(object sender, EventArgs e)
    {
        EncryptConfigSection();
    }
    // Decrypt web.config connectionString section
    protected void Button2_Click1(object sender, EventArgs e)
    {
        DecryptConfigSection();
    }
}
