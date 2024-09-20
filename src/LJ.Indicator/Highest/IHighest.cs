using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Highest
{
    public interface IHighest: IIndicator<HighestCalculateDto, HighestResultDto>
    {

        void SetParameter(int length);

    }
}
