using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation3
{
    class Program
    {
        const int CELLS = 1000;
        const int VIRUSES = 100;
        const double REPRODUCE_PROBABILITY = 0.1;
        const double CLEARANCE_PROBABILITY = 0.03;
        const bool MED_IMMUNITY = false;
        const double MUTATION_PROBABILITY = 0.005;

        static void Main(string[] args)
        {
            var meds = new List<bool>();
            meds.Add(MED_IMMUNITY);
            Patient patient;

            int[] iter = {0, 75, 150, 300 }; 
            for (int simul = 0; simul < 4; simul++)
            {
                Console.WriteLine("Simulation {0}:\n", simul + 1);
                patient = new Patient
                  (
                   CELLS,
                   VIRUSES,
                   REPRODUCE_PROBABILITY,
                   CLEARANCE_PROBABILITY,
                   meds,
                   MUTATION_PROBABILITY
                  );
                int stopedAfter = 0;
                for (int i = 1; i <= iter[simul]; i++)
                {
                    
                    if (patient.HealthyCells == 0) break;
                    stopedAfter++;
                    patient.PatientUpdate();

                }
                Console.WriteLine($"\n\tAfter {stopedAfter} simulations WITHOUT meds:\n");
                Console.WriteLine("\t" + patient);

                patient.AddMeds();
                stopedAfter = 0;
                for (int i = 1; i <= 150; i++)
                {
                    if (patient.HealthyCells == 0) break;
                    stopedAfter++;
                    patient.PatientUpdate();

                }
                Console.WriteLine($"\n\tAfter {stopedAfter} simulations WITH meds:\n");
                Console.WriteLine("\t" + patient);

            }
           
        }
    }
}
