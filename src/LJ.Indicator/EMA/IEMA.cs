using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.EMA
{
    public interface IEMA: IIndicator<EMACalculateDto, EMAResultDto>
    {

        void SetParameter(int length);

    }
}
