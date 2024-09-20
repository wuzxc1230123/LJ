using LJ.Localizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Localizer
{
    public class LocalizationService : ILocalizationService
    {
        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public event EventHandler<CultureInfo>? LanguageChanged ;
        public void SetLanguage(CultureInfo culture)
        {
            if (!CurrentCulture.Equals(culture))
            {
                CultureInfo.CurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                LanguageChanged?.Invoke(this, culture);
            }
        }
    }
}
