using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VirusWithMeds : Virus
    {
        private double mutationProb;
        private List<bool> meds;

        public double MutationProb
        {
            get { return mutationProb; }
            set
            {
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException(
                   $"{nameof(value)} must be between 0 and 1.");
                mutationProb = value;
            }
        }
        public List<bool> MedsImmunityList
        {
            get { return meds; }
            set
            {
                if (meds == null)
                    meds = value;
                else if (value != null) // only adding to the list
                {
                    foreach (bool b in value)
                        meds.Add(b);
                }
                    
            }
        }
        public VirusWithMeds
            (double reproduceProb, double clearanceProb, List<bool> meds, double mutationProb) 
            : base(reproduceProb, clearanceProb)
        {
            MutationProb = mutationProb;
            MedsImmunityList = meds;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString()
                + $"Mutation probability: {MutationProb}\nImmune to:\n");
            for(int i = 1; i <= MedsImmunityList.Count; i++)
            {
                sb.Append($"\tm{i}: {MedsImmunityList.ElementAt(i - 1)}\n");
            }
            return sb.ToString();
        }
    }
}
