using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Lowest
{
    public interface ILowest: IIndicator<LowestCalculateDto, LowestResultDto>
    {

        void SetParameter(int length);

    }
}
