using LJ.Localizer;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Localizer
{
    public class LJStringLocalizer<TResourceSource> :
          IStringLocalizer<TResourceSource>,
          IStringLocalizer
    {
        private readonly IStringLocalizerFactory _factory;
        private readonly ILocalizationService _localizationService;
        protected IStringLocalizer _localizer=null!;
        private readonly EventHandler<CultureInfo> _languageChanged;

        public LJStringLocalizer(IStringLocalizerFactory factory, ILocalizationService localizationService)
        {
            _factory = factory;
            _localizationService = localizationService;
            _languageChanged = LanguageChanged;
            _localizationService.LanguageChanged += _languageChanged;
            if (_localizer == null)
            {
                LanguageChanged(this, CultureInfo.CurrentCulture);
            }
        }
        public LocalizedString this[string name]
        {
            get
            {
                return _localizer[name];
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return _localizer[name, arguments];
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _localizer.GetAllStrings(includeParentCultures);
        }
        public void LanguageChanged(object? sender, CultureInfo culture)
        {
           _localizer = _factory.Create(typeof(TResourceSource));
        }

        public void Dispose()
        {
            _localizationService.LanguageChanged -= _languageChanged;
        }
    }
}