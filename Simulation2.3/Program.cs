using ClassLibrary;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation2._3
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
            Patient patient;
            List<Record> records;
            for (int m = 1; m <= 5; m++)
            {
                records = new List<Record>();
                meds.Add(MED_IMMUNITY);
                patient = new Patient
                (
                    CELLS,
                    VIRUSES,
                    REPRODUCE_PROBABILITY,
                    CLEARANCE_PROBABILITY,
                    meds,
                    MUTATION_PROBABILITY
                );
                patient.AddMeds();
                int stopedAfter = 0;
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
                Console.WriteLine($"\n\tAfter {stopedAfter} simulations WITH {m} meds:\n");
                Console.WriteLine(patient);
                var path = $"../../records/sim2_3_{m}.csv";
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
        }
    }
}
