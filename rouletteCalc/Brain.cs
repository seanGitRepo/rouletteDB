using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rouletteCalc
{
    public class Brain
    {

        public List<Numbera> Numberas = new List<Numbera>();
        public List<Spin> Spins = new List<Spin>();    
        public int[] OrderOfNumbers { get; } =
        {
            0, 28, 9, 26, 30, 11, 7, 20, 32, 17, 5, 22, 34, 15, 3, 24, 36, 13, 1,
            -1, 27, 10, 25, 29, 12, 8, 19, 31, 18, 6, 21, 33, 16, 4, 23, 35, 14, 2
        };
        public Numbera onStartCreate(int v, string c, int b, int t)
        {
            var heroshima = new Numbera { Value = v, Colour= c, BetAmonut = b};

            return heroshima;
        }



        public void StartNewSpin(Guid count,int turn)
        {
            
            Random random = new Random();
            int outcome = 0;

            for (int i = 0; i < 3; i++)
            {
                 outcome = random.Next(0, 38);
            }
           

            outcome = OrderOfNumbers[outcome];

            if (outcome == -1) {
                var x = new Spin { spinId = count, Turn = turn, spinValue = Numberas[0] };
                Spins.Add(x);
            }
            else
            {
                var x = new Spin { spinId = count, Turn = turn, spinValue = Numberas[outcome] };
                Spins.Add(x);
            }
           

            
        }

        public void report()
        {
            int cost = 5 * Spins.Count;

            Console.WriteLine($"Cost: {cost }");

            colourTracker();


            evenOddTracker();

            halvesTracker();

            oneInTenGreen();

            //foreach (var x in Spins)
            //{

            //    Console.WriteLine(x.spinId);

            //    Console.WriteLine(x.spinValue.Value);
            //    Console.WriteLine(x.spinValue.Colour);
            //}



           

            int greensplitreturn = 0;

            foreach (var x in Spins)
            {
                if (x.spinValue.Colour == "green")
                {

                    greensplitreturn = greensplitreturn + (5 * 17);


                }
            }
            Console.WriteLine($"Yolo greens profit after {Spins.Count} spins @ ${5}: {greensplitreturn - cost}");

        }

        public void intialise()
        {
         

            for (int i = -1; i < 37; i++)
            {

                var x = new Numbera();

                if (i == -1)
                {
                   x = onStartCreate(i, "green", 0, 0);

                }
                else if (i == 0)
                {
                   x=  onStartCreate(i, "green", 0, 0);
                }
                else
                {


                    if (i >= 1 && i <= 10 || i >= 19 && i <= 28)
                    {

                        if (i % 2 == 0)
                        {

                           x =  onStartCreate(i, "black", 0, 0);
                        }
                        else
                        {

                           x=  onStartCreate(i, "red", 0, 0);
                        }


                    }else
                    {
                        if (i % 2 == 0)
                        {

                           x=  onStartCreate(i, "red", 0, 0);
                        }
                        else
                        {

                          x=   onStartCreate(i, "black", 0, 0);
                        }

                    }


                }

                Numberas.Add(x);

            }

          


        }


        public void colourTracker()
        {

            int black = 0;
            int red = 0;
            int green = 0;  

            foreach (var x in Spins)
            {

                if (x.spinValue.Colour == "black")
                {
                    black++;
                }else if(x.spinValue.Colour == "red")
                {
                    red++;
                }
                else
                {

                    green++;
                }

            

            }
            Console.WriteLine($"red: {red}\n" +
                $"black: {black}\n" +
                $"green: {green}");
        }


        public void evenOddTracker()
        {
            int even = 0;
            int odd = 0;    
            int green = 0;


            foreach(var x in Spins)
            {

               
                 if(x.spinValue.Value % 2 == 0 && x.spinValue.Value != 0 && x.spinValue.Value != -1)
                {
                    even++;
                }else if( x.spinValue.Value % 2 == 1 && x.spinValue.Value != 0 && x.spinValue.Value != -1)
                {

                    odd++;
                }
                else
                {
                    green++;
                }
               

            }
            Console.WriteLine();
            Console.WriteLine($"odd: {odd}\n" +
                $"even:  {even}\n" +
                $"green: {green}");

        }

        public void halvesTracker()
        {


            int first = 0;
            int second = 0;
            int green = 0;


            foreach (var x in Spins)
            {


                if (x.spinValue.Value >=1 && x.spinValue.Value <= 18)
                {
                    first++;
                }
                else if (x.spinValue.Value >= 19 && x.spinValue.Value <= 36)
                {

                    second++;
                }
                else
                {
                    green++;
                }


            }
            Console.WriteLine();
            Console.WriteLine($"first: {first}\n" +
                $"second:  {second}\n" +
                $"green: {green}");
        }


        public void oneInTenGreen()
        {


            int count = Spins.Count();
            int currentPeriod = 0;
            int hits = 0;

         List<int> ofTen = new List<int>();


            for (int i = 0; i < count; i++) 
            {
              
                if (i % 10 != 1 )
                {

                    if (Spins[i].spinValue.Colour == "green")
                    {
                        currentPeriod++;
                    }
                 

                }
                else
                {
                    ofTen.Add(currentPeriod);

                    currentPeriod = 0;


                    // here we need to reset the count and add the numbers to the array.
                    // this means in 1000 spins, with 100 chains of 10, 50-60 of those chains will it hit green
                    // "splitting the green with $5 returns 5 * 17 = 85
                    // saying 55 times out of 100 
                    // each 10 chain costs 50. there is a 55/100 chance that it hits





                
                }


            }


            foreach (var g in ofTen)
            {


                if(g >= 1)
                {
                    hits++;
                    
                }

            }

            Console.WriteLine();
            Console.WriteLine($"Green hit within every 10 spins: {hits}");


        }



    }
}
