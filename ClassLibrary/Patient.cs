using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Patient
    {
        public int HealthyCells { get; set; }
        public int InfectedCells { get; set; }
        public List<IVirus> VirusPop { get; set; }
        private bool onMeds;
        private double reproduceProb;
        private double clearanceProb;
        private int startCells;
        private List<bool> startMedsImmunity;
        private double mutationProbabilty;

        public Patient(int startCells, int startVirusPop, Double reproduceProb, Double clearanceProb)
        {
            this.InfectedCells = 0;
            this.HealthyCells = startCells;
            VirusPop = new List<IVirus>();
            this.reproduceProb = reproduceProb;
            this.clearanceProb = clearanceProb;
            this.startCells = startCells;
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
            this.HealthyCells = startCells;
            this.startCells = startCells;
            this.startMedsImmunity = meds;
            this.mutationProbabilty = mutationProb;
            VirusPop = new List<IVirus>();
            for (int i = 0; i < startVirusPop; i++)
            {
                VirusPop.Add(new VirusWithMeds
                    (reproduceProb, clearanceProb, meds, mutationProb));
            }
        }

        public void AddMeds()
        {
            onMeds = true;
        }

        public void PatientUpdate()
        {
            
            // clear the infected cells of the last round and for each one add one virus 
            if (VirusPop.ElementAt(0).GetType() == typeof(Virus))
            {
                for (int i = 0; i < InfectedCells; i++)
                {
                    VirusPop.Add(new Virus(this.reproduceProb, this.clearanceProb));
                }
            }
            else
                for (int i = 0; i < InfectedCells; i++)
                {
                    VirusPop.Add(new VirusWithMeds(this.reproduceProb, this.clearanceProb, startMedsImmunity, this.mutationProbabilty));
                }
            InfectedCells = 0;
            if (HealthyCells == 0) return;

            Random random = new Random();
            
                try
                {
                    foreach (IVirus v in VirusPop)
                    {
                        v.SurvivalState = chanceGenerator(random, 1 - v.ClearanceProb);
                        if (v.SurvivalState && onMeds)
                        {
                            VirusWithMeds virus = (VirusWithMeds)v;
                            for (int i = 0; i < virus.MedsImmunityList.Count; i++)
                            {
                                bool b = chanceGenerator(random, virus.MutationProb);
                                if (b != virus.MedsImmunityList.ElementAt(i))
                                {
                                    virus.MedsImmunityList.RemoveAt(i);
                                    virus.MedsImmunityList.Insert(i, b);
                                }
                            }
                            if (virus.MedsImmunityList.Contains(true))
                                v.IsRepreduced = false;
                            else 
                                v.IsRepreduced = chanceGenerator
                                (
                                    random,
                                    v.RepreduceChance(VirusPop.Count / startCells)
                                );
                    }
                    else if (v.SurvivalState && !onMeds)
                        v.IsRepreduced = chanceGenerator(
                                 random, v.RepreduceChance(VirusPop.Count / startCells));

                    if (v.IsRepreduced)
                        {
                            InfectedCells++;
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                }

            if (HealthyCells - InfectedCells > 0)
                HealthyCells = HealthyCells - InfectedCells;
            else HealthyCells = 0;
            // clear the VirusPop list
            List<IVirus> survivedViruses = new List<IVirus>();
            foreach (IVirus v in VirusPop)
                if (v.SurvivalState) survivedViruses.Add(v);
            VirusPop = survivedViruses;
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
                $"\tInfected cells: {InfectedCells}\n\tHealthy cells: {HealthyCells}\n\tVirus population size: {VirusPop.Count}\n";
        }
    }
}
