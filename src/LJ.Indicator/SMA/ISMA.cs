using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.SMA
{
    public interface ISMA: IIndicator<SMACalculateDto, SMAResultDto>
    {

        void SetParameter(int length);

    }
}
