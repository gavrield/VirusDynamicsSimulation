using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace VirusDynamicsSimulation
{
    class Program
    {
        const int CELLS = 1000;
        const int VIRUSES = 100;
        const double REPRODUCE_PROBABILITY = 0.1;
        const double CLEARANCE_PROBABILITY = 0.03;

        static void Main(string[] args)
        {
            Patient patient = new Patient
                (CELLS, VIRUSES, REPRODUCE_PROBABILITY, CLEARANCE_PROBABILITY);
            Console.WriteLine("\n\nBefore simulation:");
            Console.WriteLine(patient);
            Console.WriteLine("\n\nAfter 10 simulations:");
            patient.PatientUpdate();
            Console.WriteLine(patient);

        }
    }
}
