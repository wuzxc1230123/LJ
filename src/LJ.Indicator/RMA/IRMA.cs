using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.RMA
{
    public interface IRMA: IIndicator<RMACalculateDto, RMAResultDto>
    {

        void SetParameter(int length);

    }
}
