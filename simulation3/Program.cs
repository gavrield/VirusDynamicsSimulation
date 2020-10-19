using ClassLibrary;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation2._2
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
            List<Record> records;
            int[] iter = {0, 75, 150, 300 }; 
            for (int simul = 0; simul < 4; simul++)
            {
                records = new List<Record>();
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
                    records.Add(new Record
                    {
                        HealthyCells = patient.HealthyCells,
                        InfectedCells = patient.InfectedCells,
                        NumOfViruses = patient.VirusPop.Count
                    });

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
                    records.Add(new Record
                    {
                        HealthyCells = patient.HealthyCells,
                        InfectedCells = patient.InfectedCells,
                        NumOfViruses = patient.VirusPop.Count
                    });

                }
                Console.WriteLine($"\n\tAfter {stopedAfter} simulations WITH meds:\n");
                Console.WriteLine("\t" + patient);

                var path = $"../../records/sim2_2_{simul + 1}.csv";
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
           
        }
    }
}
