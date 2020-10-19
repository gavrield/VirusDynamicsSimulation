﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Simulation2._1
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
            List<Record> records = new List<Record>();
            var meds = new List<bool>();
            meds.Add(MED_IMMUNITY);
            Patient patient = 
                new Patient
                (
                    CELLS,
                    VIRUSES,
                    REPRODUCE_PROBABILITY,
                    CLEARANCE_PROBABILITY,
                    meds,
                    MUTATION_PROBABILITY
                );
            int stopedAfter = 0;
            for(int i = 1; i <= 150; i++)
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
            Console.WriteLine($"\nAfter {stopedAfter} simulations WITHOUT meds:\n");
            Console.WriteLine(patient);

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
            Console.WriteLine($"\nAfter {stopedAfter} simulations WITH meds:\n");
            Console.WriteLine(patient);
            var path ="..//..//records//sim2_1.csv";
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }

        }
    }


}
