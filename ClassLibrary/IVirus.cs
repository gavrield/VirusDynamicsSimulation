using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface IVirus
    {
        bool SurvivalState { get; set; }
        bool IsRepreduced { get; set; }
        Double ClearanceProb { get; set; }
        Double ReproduceProb { get; set; }
        double RepreduceChance(double capacity);
    }
}
