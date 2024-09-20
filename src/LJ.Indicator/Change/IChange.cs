using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Change
{
    public interface IChange: IIndicator<ChangeCalculateDto, ChangeResultDto>
    {

        void SetParameter(int length);

    }
}
