using OnlineCarWash.Options.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Options.Helpers
{
    public class TestOptionFactory
    {
        public static Option CreateOption(int id)
        {
            return new Option
            {
                Id = id,
                Name = "test" + id,
                Price = id * 10
            };
        }

        public static List<Option> CreateOptions(int count)
        {
            var options = new List<Option>();

            for (int i = 0; i < count; i++)
            {
                options.Add(CreateOption(i));
            }

            return options;
        }

    }
}
