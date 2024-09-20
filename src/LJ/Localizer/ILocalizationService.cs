using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Localizer
{
    /// <summary>
    /// 本地化
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// 获取当前
        /// </summary>d
        CultureInfo CurrentCulture { get; }

       /// <summary>
       /// 设置
       /// </summary>
       /// <param name="culture"></param>
        void SetLanguage(CultureInfo culture);

       /// <summary>
       /// 切换事件
       /// </summary>
        event EventHandler<CultureInfo>? LanguageChanged;
    }
}
