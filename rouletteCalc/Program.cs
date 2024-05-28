//Brainless attempt at finding the most efficent way to play roulette, on a night out. Simulation baby.





using rouletteCalc;

Brain x = new Brain();


x.intialise();


for (int i = 0; i < 250; i++)
{
    Guid guid = Guid.NewGuid();

    x.StartNewSpin(guid,i);

}

x.report();



