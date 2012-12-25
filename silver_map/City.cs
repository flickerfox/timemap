using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Reflection.Emit;

namespace silver_map
{
    public class City
    {
        public String city_name;
        public int rank;
        public double distance_road;          //to 中心城市
        public double distance_railway;
        public double distance_air;
        public string describe;
        public int population = 0;
        public String landscape1;
        public String landscape2;
        public String landscape3;

     
        

 
        public City(string name, int _rank, double d1, double d2, double d3)
        {
            city_name = name;
            rank = _rank;
            distance_road = d1;
            distance_railway = d2;
            distance_air = d3;
        }

        



       
    }
}
