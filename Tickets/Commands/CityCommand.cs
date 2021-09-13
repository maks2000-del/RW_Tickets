using System;
using System.Collections.Generic;
using System.Linq;
using Tickets.DB;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Commands
{
    public interface ICityInterface
    {
        List<string> getCitys();
        string getCity(int index);
        int getIdCity(string region);
    }

    class CityCommand : ICityInterface
    {
        private Tickets_db context;

        public CityCommand()
        {
            context = new Tickets_db();
        }

        public List<string> getCitys()
        {
            List<string> tmp = new List<string>();

            foreach (City region in context.Citys)
                tmp.Add(region.city1);

            return tmp.Distinct().ToList();
        }

        public string getCity(int index)
        {
            return context.Citys.FirstOrDefault(x => x.id == index).city1;
        }

        public int getIdCity(string region)
        {
            return context.Citys.FirstOrDefault(x => x.city1.Equals(region)).id;
        }
    }
}
