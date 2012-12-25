using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace silver_map
{
    public class Position_compute
    {

        public double ratio_road = 12;
        Random random = new Random();
        public int cities_number=24;//control to set colors
        public double now_set_color_number = 0;
        public double angle_set=0;
        public double px=0;
        public int way = 1;

        public void set_angle_zero()
        {
            angle_set = 0;
        }
        public double angle_compute()
        {
            double angle = (angle_set + random.NextDouble()) / cities_number;
            angle_set++;
            if (angle_set >= cities_number)
                angle_set = 0;
            return angle*Math.PI*2;
        }
        
        public double compute_x_relatetomaincity(double distance, double maincity_X, double angle) //return center x
        {
        
            double x = 0;
            x = Math.Cos(angle) * distance*ratio_road+ maincity_X;
            return x;
        }

        public double compute_X_relateto_maincity(double distance, double maincity_X,double angle) //return center x
        {
          
            double x = 0;
           
        
            x = Math.Cos(angle) * distance *ratio_road + maincity_X;
            return x;
        }
        public double compute_Y_relateto_maincity(double distance, double maincity_Y,double angle)
        {
          
            double y = 0;
            y = Math.Sin(angle) * distance * ratio_road + maincity_Y;
            return y;

        }

        public double compute_x_relatomaincity_Line(double distance, double maincity_x)
        {
            return distance * ratio_road+maincity_x;
        }
        public double compute_y_relatomaincity_Line(double maincity_y)
        {
            return maincity_y-6;
        }
        public void set_citye_brushcolor(City_E  city_e)//set ellipse color at random 
        {
            city_e.radialgradientbrush = new RadialGradientBrush();
        //    city_e.radialgradientbrush.GradientOrigin = new Point(0.5, 0.5);
          //  city_e.radialgradientbrush.Center = new Point(0.5, 0.5);

            RadialGradientBrush brush = new RadialGradientBrush();
            int []c={0,0,0};
            double bili = random.NextDouble();
            int paichu = random.Next(3);
            int left;
            int right;
            c[paichu] = 0;
            if (paichu == 0)
            {
                left = 1;
                right = 2;
            }
            else if (paichu == 1)
            {
                left = 0; right = 2;
            }
            else
            {
                left = 0; right = 1;
            }
            c[right] = (int)(255 * bili*0.2);
            c[left] = (int)(255 * 0.2);
            int a = c[right];
            int b = c[left];
            GradientStop gs=new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0;
        
            city_e.radialgradientbrush.GradientStops.Add(gs);  //deep
            gs = new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0.6;
            city_e.radialgradientbrush.GradientStops.Add(gs);//deep
            c[right] = (int)(c[right] / 0.3);
            c[left] = (int)(c[left] / 0.3);

            gs = new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0.61;
            city_e.radialgradientbrush.GradientStops.Add(gs);//middle
            gs = new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0.69;
            city_e.radialgradientbrush.GradientStops.Add(gs);//middle
              c[left] = b;
             c[right] = a;
             gs = new GradientStop();
             gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
             gs.Offset = 0.7;
            city_e.radialgradientbrush.GradientStops.Add(gs);//deep
            gs = new GradientStop();
            gs.Offset=0.72;
            city_e.radialgradientbrush.GradientStops.Add(gs);//deep

            c[left] = (int)(b / 0.2);
            c[right] = (int)(a / 0.2);
            gs = new GradientStop();
              gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
             gs.Offset = 0.73;
            city_e.radialgradientbrush.GradientStops.Add(gs);//lighgt
            gs = new GradientStop();
            gs.Offset=1;
            gs.Color = Color.FromArgb((byte)0, (byte)c[0], (byte)c[1], (byte)c[2]);
            city_e.radialgradientbrush.GradientStops.Add(gs);  //nothing
            brush.GradientStops.Add(gs);
            city_e.ellipse.Fill = brush;
            city_e.line_brush.Color = Color.FromArgb((byte)10, (byte)c[0], (byte)c[1], (byte)c[2]);

            
        }

        public void set_ellipse_fillbrushcolor(City_E citye)  //is being used
        {
            RadialGradientBrush brush = new RadialGradientBrush();

            int[] c = { 0, 0, 0 };
            if (now_set_color_number >= cities_number)
                now_set_color_number = 0;
             now_set_color_number++;

               px+=((double)6/(double)cities_number)*way;
             

            if(px>0.9)
             way=-1;
            else if(px<0.1)
                way=1;

       
            double bili =px;
         
       
           // now_set_color_number++;
         //   now_set_color_number++;

            int paichu = random.Next(3);
            if (now_set_color_number <= (cities_number / 3))
                paichu = 2;
            else if (now_set_color_number < (cities_number * 2) / 3)
                paichu = 0;
            else
                paichu = 1;
            int left;
            int right;
            c[paichu] = 0;

            if (paichu == 0)
            {
                left = 1;
                right = 2;
            }
            else if (paichu == 1)
            {
                left = 0; right = 2;
            }
            else
            {
                left = 0; right = 1;
            }

            bili = px;

            

            if (paichu == 2 || paichu == 0)
            {
                if (way == 1){
                    c[left] = (int)(255 * 0.2);
                    c[right] = (int)(255 * bili * 0.2);
                }
                else 
                {
                    c[right] = (int)(255 * bili * 0.2);
            c[left] = (int)(255 * 0.2);
                }
                
            }
            else
            {
                  if (way == 1){
                           c[right] = (int)(255 * bili * 0.2);
            c[left] = (int)(255 * 0.2);
                }
                else 
                {
            
                       c[left] = (int)(255 * 0.2);
                    c[right]=(int)(255*bili*0.2);
                }
            }
            int a = c[right];
            int b = c[left];

           GradientStop gs=new GradientStop();
            gs.Color = Color.FromArgb((byte)0, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0;
            gs = new GradientStop();
            brush.GradientStops.Add(gs);  //deep
            gs.Color = Color.FromArgb((byte)0, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0.4;
            gs = new GradientStop();
            brush.GradientStops.Add(gs);//deep
            c[right] = (int)(c[right] / 0.3);
            c[left] = (int)(c[left] / 0.3);
            gs = new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0.41;
            brush.GradientStops.Add(gs);//middle
            gs = new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
            gs.Offset = 0.49;
            brush.GradientStops.Add(gs);//middle
              c[left] = b;
             c[right] = a;
             gs = new GradientStop();
             gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
             gs.Offset = 0.5;
            brush.GradientStops.Add(gs);//deep
            gs = new GradientStop();
            gs.Offset=0.52;
            brush.GradientStops.Add(gs);//deep
            gs = new GradientStop();

            c[left] = (int)(b / 0.2);
            c[right] = (int)(a / 0.2);
              gs.Color = Color.FromArgb((byte)255, (byte)c[0], (byte)c[1], (byte)c[2]);
             gs.Offset = 0.53;
            brush.GradientStops.Add(gs);//lighgt
            gs = new GradientStop();
            gs.Offset=1;
            gs.Color = Color.FromArgb((byte)0, (byte)c[0], (byte)c[1], (byte)c[2]);
            brush.GradientStops.Add(gs);  //nothing

            citye.ellipse.Fill = brush;
            citye.line_brush.Color = Color.FromArgb((byte)175, (byte)c[0], (byte)c[1], (byte)c[2]);
        }
    }
}
