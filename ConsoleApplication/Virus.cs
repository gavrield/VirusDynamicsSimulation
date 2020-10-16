using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusDynamicsSimulation
{
    class Virus
    {
        private Double reproduceProb;
        private Double clearanceProb;

        public bool SurvivalState { get; set; }
        public bool IsRepreduced { get; set; }
        public Double ReproduceProb
        {
            get { return reproduceProb; }
            set
            {
                if ( value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException(
                   $"{nameof(value)} must be between 0 and 1.");
                reproduceProb = value;
            }
        } 
        public Double ClearanceProb
        {
            get { return clearanceProb; }
            set
            {
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException(
                   $"{nameof(value)} must be between 0 and 1.");
                clearanceProb = value;
            }

        }

        public Virus(Double reproduceProb, Double clearanceProb)
        {
            this.ReproduceProb = reproduceProb;
            this.ClearanceProb = clearanceProb;
            this.SurvivalState = true;
            this.IsRepreduced = false;
        }

        public Double RepreduceChance(double capacity)
        {
            return ReproduceProb * (1 - capacity);
        }

    }
}
