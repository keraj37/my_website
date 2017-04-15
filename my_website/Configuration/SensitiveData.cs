using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
/// <summary>
/// I need to reseacrh/ask how to include it correctly in web.config
/// </summary>
namespace my_website.Configuration
{
    public class SensitiveData : ConfigurationSection
    {
        [ConfigurationProperty("googleClientId", DefaultValue = "", IsRequired = false)]
        public string GoogleClientId
        {
            get
            {
                return (string)this["googleClientId"];
            }
            set
            {
                this["googleClientId"] = value;
            }
        }

        [ConfigurationProperty("googleSecret", DefaultValue = "", IsRequired = false)]
        public string GoogleSecret
        {
            get
            {
                return (string)this["googleSecret"];
            }
            set
            {
                this["googleSecret"] = value;
            }
        }
    }
}