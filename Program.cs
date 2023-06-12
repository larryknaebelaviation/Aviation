using aircraft;
using System;
using spatial;
using System.Transactions;
using System.Runtime.InteropServices.ComTypes;
using flightmanagementcomputer;
using System.Threading;
using flightPlanProcessing;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using airtrafficcontrol;

namespace Aviation
{
    class Program
    {
        private string mystr { get; set; }
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.flyTheFleet();
            //Thread.Sleep(300000);
        }

        //public async Task run()
        public void flyTheFleet()
        {
            //mystr = "Hello";
            //Console.WriteLine(mystr);
            FlightPlan fp1 = new FlightPlan(@"C:\Flight\fp1.fpn");
            fp1.dump();
            FlightPlan fp2 = new FlightPlan(@"C:\Flight\fp2.fpn");
            fp2.dump();
            FlightPlan fp3 = new FlightPlan(@"C:\Flight\fp3.fpn");
            fp3.dump();
            //Console.WriteLine(fp.getSegment(1).ToString());
            //Console.WriteLine(fp.getStartingPosition().ToString());
            //Console.WriteLine("Flight Plan size:" + fp.getSize());
            //Console.WriteLine("First segment:" + fp.getSegment(fp.getFirstNavSegmentIndex()).ToString());

            //Console.WriteLine("Aviation World!");
            Aircraft a1 = new Aircraft("Boeing", "727D", 29187);
            Aircraft a2 = new Aircraft("Boeing", "737F", 29888);
            Aircraft a3 = new Aircraft("Douglas", "M80", 49184);

           

            FMC fmc1 = new FMC(a1.getId().ToString(),new Position(42.763d, 83.234d, 0.01d), new VVector(0d, 0d, DateTime.UtcNow));
            FMC fmc2 = new FMC(a2.getId().ToString(),new Position(42.763d, 90.234d, 0.01d), new VVector(0d, 0d, DateTime.UtcNow));
            FMC fmc3 = new FMC(a3.getId().ToString(),new Position(41.979722d, 87.911111d, 0.01d), new VVector(0d, 0d, DateTime.UtcNow));
            fmc1.setFlightPlan(fp1);
            fmc2.setFlightPlan(fp2);
            fmc3.setFlightPlan(fp3);



            //Console.WriteLine(fmc.GetFlightPlan().getSegment(1).ToString());
            //Console.WriteLine(fmc.GetFlightPlan().getStartingPosition().ToString());
            //Console.WriteLine("Flight Plan size:" + fmc.GetFlightPlan().getSize());
            //Console.WriteLine("First segment:" + fmc.GetFlightPlan().getSegment(fmc.GetFlightPlan().getFirstNavSegmentIndex()).ToString());
            //Console.WriteLine("Next segment:" + fmc.GetFlightPlan().getNextNavSegment().ToString());
            //Console.WriteLine("Next segment:" + fmc.GetFlightPlan().getNextNavSegment().ToString());
            //Console.WriteLine("Next segment:" + fmc.GetFlightPlan().getNextNavSegment().ToString());
            //Console.WriteLine("Next segment:" + fmc.GetFlightPlan().getNextNavSegment().ToString());

            
            a1.setFMC(fmc1);           
            a2.setFMC(fmc2);            
            a3.setFMC(fmc3);
            a1.setSpeed(245d);
            ////a1.getFMC().activateFlightPlan();
            ////a2.getFMC().activateFlightPlan();
            ////a3.getFMC().activateFlightPlan();

            //Method 1 async
            //await Task.Run(() => a1.getFMC().activateFlightPlan());
            //await Task.Run(() => a2.getFMC().activateFlightPlan());
            //await Task.Run(() => a3.getFMC().activateFlightPlan());


            // Method 2 async
            //List<Task<Aircraft>> fleet = new List<Task<Aircraft>>();            
            //fleet.Add(Task<Aircraft>.Run(() => a1.getFMC().activateFlightPlan()));

            //List<Task> fleet = new List<Task>();
            //fleet.Add(Task.Run(() => a1.getFMC().activateFlightPlan()));
            //fleet.Add(Task.Run(() => a2.getFMC().activateFlightPlan()));
            //fleet.Add(Task.Run(() => a3.getFMC().activateFlightPlan()));
            //var results = Task.WhenAll(fleet);
            //Console.WriteLine(results.IsCompletedSuccessfully);

            // Parallel method
            // The Invoke automatically waits            
            List<Aircraft> al = new List<Aircraft>();
            al.Add(a1);
            al.Add(a2);
            al.Add(a3);
            //Parallel.ForEach<List<Aircraft>>((al,state,0L) =>
            //{
            //    if (al is null)
            //    {
            //        throw new ArgumentNullException(nameof(al));
            //    }

            //    return getFMC().activateFlightPlan();
            //});

            ATC atc = new ATC("Detroit TRACON");
            atc.squawk(al[0]);
            atc.squawk(al[1]);
            atc.squawk(al[2]);
            Console.WriteLine("ATC POS: " + al[2].getId() + " " + atc.getPosition(al[2].getId()).ToString());

            Parallel.Invoke(
                () => al[0].getFMC().activateFlightPlan(),
                () => al[1].getFMC().activateFlightPlan(),
                () => al[2].getFMC().activateFlightPlan());
          
            
            //List<Task> flights = new List<Task>();
            //flights.Add(Task.Run(() => a1.getFMC().activateFlightPlan()));
            //flights.Add(Task.Run(() => a2.getFMC().activateFlightPlan()));
            //flights.Add(Task.Run(() => a3.getFMC().activateFlightPlan()));

            //flights[0].Wait();
            //flights[1].Wait();
            //flights[2].Wait();



            //Thread.Sleep(300000);
            //DateTime TIMESECS = DateTime.UtcNow;
            //const int WAITTIME = 120000;
            //Thread.Sleep(3000);
            //a1.setAltitude(16000);
            //a1.setHeadingAndSpeed(090d, 300d);
            //Thread.Sleep(WAITTIME);
            //Thread.Sleep(WAITTIME);
            //a1.setAltitude(35000);
            //a1.setHeading(45d);
            //Thread.Sleep(WAITTIME);
            //a1.setHeadingAndSpeed(180d, 500d);
            //Thread.Sleep(WAITTIME * 2);
            //a1.setAltitude(700);
            //a1.setHeadingAndSpeed(0d, 0d);
            //a1.getFMC().dumpHistory();
            //a2.getFMC().dumpHistory();
            //a3.getFMC().dumpHistory();


        }

    }
}
        

