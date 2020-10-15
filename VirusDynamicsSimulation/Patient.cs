using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusDynamicsSimulation
{
    class Patient
    {
        public int StartCells { get; set; }
        public int InfectedCells { get; set; }
        public List<Virus> VirusPop { get; set; }

        public Patient(int startCells, int startVirusPop, Double reproduceProb, Double clearanceProb)
        {
            this.InfectedCells = 0;
            this.StartCells = startCells;
            VirusPop = new List<Virus>();
            for (int i = 0; i < startVirusPop; i++)
            {
                VirusPop.Add(new Virus(reproduceProb, clearanceProb));
            }
        }

        public void PatientUpdate()
        {
            Random random = new Random();
            foreach (Virus v in VirusPop)
            {
                v.SurvivalState = chanceGenerator(random, 1 - v.ClearanceProb);
                if (v.SurvivalState)
                    v.IsRepreduced = chanceGenerator
                    (
                        random,
                        v.RepreduceChance(InfectedCells / StartCells)
                    );
                if (v.IsRepreduced) InfectedCells++;
            }
        }
          
        private bool chanceGenerator(Random random, double rate)
        {
            if (random.NextDouble() <= rate)
                return true;
            return false;
        }
    }
}
