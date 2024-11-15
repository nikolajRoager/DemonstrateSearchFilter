using DemonstrateSearchFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace DemonstrateSearchFilter.Services
{
    public class BoxService : IBoxService
    {
        private readonly BoxContext context;

        public BoxService(BoxContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Box>> GetBoxesByKeyValuesAsync(Dictionary<string,string> query)
        {
            Console.WriteLine($"Executing query size {query.Count()}");   
            //Empty query, empty result
            if (query.Count() == 0)
                return new List<Box>();

            //First get a reference to all boxes, we will apply each query one by one, gradually shrinking it
            IQueryable<Box> myQuery = context.Boxes;
            Console.WriteLine("Executing query");   
            //Each query shrinks the query until it only contains everything we need
            foreach (var q in query)
            {
                //If all properties are STRINGS, we could just use this function to get the property from entityFramework directly:
                //myQuery = myQuery.Where(item => EF.Property<string>(item,q.Key)==q.Value);
                
                //Since I do have some integers, and some strings, I find it easier to use a switch statements
                //That way I can enforce insensitive when it comes to key and color, but not Name
                //For now, I simply IGNORE invalid requests
                switch(q.Key.ToLower())
                {
                    //Name IS case sensitive
                    case "name":
                        myQuery = myQuery.Where(item => item.Name==q.Value);
                        break;
                    case "color":
                        myQuery = myQuery.Where(item => item.Color.ToLower()==q.Value.ToLower());
                        break;
                    case "height":
                        if (int.TryParse(q.Value,out int height))
                            myQuery = myQuery.Where(item => item.height==height);
                        break;
                    case "width":
                        if (int.TryParse(q.Value,out int width))
                            myQuery = myQuery.Where(item => item.width ==width);
                        break;
                    case "depth":
                        if (int.TryParse(q.Value,out int depth))
                            myQuery = myQuery.Where(item => item.depth==depth);
                        break;
                    case "volume":
                        if (int.TryParse(q.Value, out int volume))
                            myQuery = myQuery.Where(item => item.volume == volume);
                        break;
                    default:
                        break;

                }
                Console.WriteLine($"Item {q.Key}={q.Value}");   
            }

            return await myQuery.ToListAsync();
        }
    }
}
