using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSender.Domain.Boxes
{
    public class Box
    {


    }

    public record Box_capacity(int Number_of_spots)
    {
        public static Box_capacity From_number_of_spots(int number_of_spots)
        {
            var capacity = number_of_spots switch
            {
                <= 6 => new Box_capacity(6),
                <= 12 => new Box_capacity(12),
                _ => new Box_capacity(24)
            };
            return capacity;
        }
    }



}
