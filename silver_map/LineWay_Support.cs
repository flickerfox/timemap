using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace silver_map
{
    public class LineWay_Support
    {
        public City_E maincity;
        public Line absscisse=new Line();
        public Line ordinate = new Line();
        Storyboard sb;
        DoubleAnimation da1,da2;
        Canvas c1;

        public void set(City_E main_city)
        {
            maincity = main_city;
        }

        public void Create(City_E main_city, Canvas canvas)
        {
            c1 = canvas;
            maincity = main_city;
            canvas.Children.Add(ordinate);
            canvas.Children.Add(absscisse);
            ordinate.X1 = 1;
            ordinate.X2 = 2;
            ordinate.Y1 = 1;
            ordinate.Y2 = 2;
            absscisse.X1 = 1;
            absscisse.X2 = 2;
            absscisse.Y1 = 1;
            absscisse.Y2 = 2;
            ordinate.StrokeThickness = 3;
            ordinate.Stroke= new SolidColorBrush(Colors.White);
            absscisse.StrokeThickness = 3;
            absscisse.Stroke = new SolidColorBrush(Colors.White);

            absscisse.Opacity = 0;
            ordinate.Opacity = 0;

        }
        public void move(double dx, double dy)
        {
            absscisse.X1 += dx;
            absscisse.X2 += dx;
            absscisse.Y1 += dy;
            absscisse.Y2 += dy;
            ordinate.X1 += dx;
            ordinate.X2 += dx;
            ordinate.Y1 += dy;
            ordinate.Y2 += dy;
        }
        public void appear()
        {
            sb = new Storyboard();
            da1 = new DoubleAnimation();
            da2 = new DoubleAnimation();
            absscisse.X1 = maincity.x_center + (maincity.r - 10) / 2;
            absscisse.X2 = maincity.x_center + (maincity.r - 10) / 2+1;
            absscisse.Y1 = maincity.y_center ;
           absscisse.Y2=  absscisse.Y1;
            ordinate.Y1 = maincity.y_center - maincity.r / 2;
           ordinate.X1 = maincity.x_center ;
          ordinate.X2 = maincity.x_center ;
          ordinate.Y2 = ordinate.Y1 + 1;
          // da1.From = maincity.x_center+(maincity.r-10)/2;
        //   da2.From = maincity.y_center-maincity.r/2;
         //  da1.To = c1.Width-maincity.x_center;
        //    da2.To = c1.Height - maincity.y_center;
           da1.To = 800;
            da2.To = 0;
            da1.Duration = TimeSpan.FromSeconds(1);
            da2.Duration = da1.Duration;

           Storyboard.SetTarget(da1, absscisse);
            Storyboard.SetTargetProperty(da1, new PropertyPath(Line.X2Property));
            Storyboard.SetTarget(da2, ordinate);
            Storyboard.SetTargetProperty(da2, new PropertyPath(Line.Y2Property));
            sb.Children.Add(da1);
           sb.Children.Add(da2);
           sb.Begin();
        //   maincity.textblock.Text += "lalallalalallalaaaaa";
          absscisse.Opacity = 1;
          ordinate.Opacity = 1;
        }
        public void disappear()
        {
            absscisse.Opacity = 0;
            ordinate.Opacity = 0;
        }
    }
}
