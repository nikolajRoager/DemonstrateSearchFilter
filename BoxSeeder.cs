using DemonstrateSearchFilter.Models;

namespace DemonstrateSearchFilter
{
    //Seed the database with a bunch of boxes we can search for
    public static class BoxSeeder
    {
        //The this keyword makes C# think the BoxContext has a function BoxContext.Seed
        //This function creates count boxes, starting with all boxes with volume=1, then volumne = 2, etc.
        public static void Seed(this BoxContext context, int count = 10)
        {
            Random random = new Random();
            long id = 1;

            for (int volume = 1; id<=count; volume++)
            {
                //Brute force loop through all 3 divisor pairs of the volume
                for (int height = 1; height <= volume && id<=count; ++height)
                {
                    if (volume % height != 0)
                        continue;

                    //What surface area do we need to get the volume with the height?
                    int Area = volume / height;

                    //Loop through all widths, and if they divide the area, get the depth
                    for (int width = 1; width <= Area && id<=count; ++width)
                    {
                        if (Area % width != 0)
                            continue;

                        int depth = Area / width;

                        //Now we have width,height and depth, make a random colored box

                        string color;
                        switch (random.Next(0, 5))
                        {
                            default:
                            case 0:
                                color = "Red";
                                break;
                            case 1:
                                color = "Green";
                                break;
                            case 2:
                                color = "Blue";
                                break;
                            case 3:
                                color = "White";
                                break;
                            case 4:
                                color = "Black";
                                break;
                        }
                        Console.WriteLine($" {id} {color} {height} {width} {depth}");
                        context.Boxes.Add(new Box(id, $"Box {id}", color, height, width, depth));
                        ++id;



                    }
                }
            }
            context.SaveChanges();
        }
    }
}
