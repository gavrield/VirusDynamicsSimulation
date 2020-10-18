using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Patient
    {
        public int StartCells { get; set; }
        public int InfectedCells { get; set; }
        public List<IVirus> VirusPop { get; set; }

        public Patient(int startCells, int startVirusPop, Double reproduceProb, Double clearanceProb)
        {
            this.InfectedCells = 0;
            this.StartCells = startCells;
            VirusPop = new List<IVirus>();
            for (int i = 0; i < startVirusPop; i++)
            {
                VirusPop.Add(new Virus(reproduceProb, clearanceProb));
            }
        }
        public Patient(int startCells, int startVirusPop,
           double reproduceProb, double clearanceProb,
           List<bool> meds, double mutationProb)
        {
            this.InfectedCells = 0;
            this.StartCells = startCells;
            VirusPop = new List<IVirus>();
            for (int i = 0; i < startVirusPop; i++)
            {
                VirusPop.Add(new VirusWithMeds
                    (reproduceProb, clearanceProb, meds, mutationProb)
                    );
            }
        }

        public void PatientUpdate()
        {
            Random random = new Random();
            foreach (IVirus v in VirusPop)
            {
                
                v.SurvivalState = chanceGenerator(random, 1 - v.ClearanceProb);
                if (v.SurvivalState)
                    v.IsRepreduced = chanceGenerator
                    (
                        random,
                        v.RepreduceChance(InfectedCells / StartCells)
                    );
                else // If NOT survived remove it from the list
                {
                    VirusPop.Remove(v);
                    return;
                }
                if (v.IsRepreduced) InfectedCells++;
            }
        }

        static public bool chanceGenerator(Random random, double rate)
        {
            if (random.NextDouble() <= rate)
                return true;
            return false;
        }

        public override string ToString()
        {
            return
                $"\tInfected cells: {InfectedCells}\n\tHealthy cells: {StartCells - InfectedCells}\n\tVirus population size: {VirusPop.Count}\n";
        }
    }
}
