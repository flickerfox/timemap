using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Reflection.Emit;


namespace silver_map
{
    public class Time_circle
    {
        public Ellipse tc_e;
        public double Ratio=12;
        public double time_distance = 2.5;
        public Label label;
        public TextBlock textblock;
        public double x_center;
        public double y_center;
        Storyboard sb1;
        DoubleAnimation da1, da2;

        public Time_circle(double xcenter,double ycenter,Canvas cavas)
        {

            x_center = xcenter;
            y_center = ycenter;
            tc_e = new Ellipse();
            label = new Label();
            textblock = new TextBlock();
            textblock.FontSize = 18;
            textblock.Text =time_distance.ToString()+"小时圈";
            textblock.RenderTransform = new TranslateTransform();
            textblock.RenderTransform.SetValue(TranslateTransform.XProperty, x_center + Ratio * time_distance);
            textblock.RenderTransform.SetValue(TranslateTransform.YProperty, y_center);
            cavas.Children.Add(textblock);
            tc_e.StrokeThickness = 1;
            tc_e.Stroke = new SolidColorBrush(Color.FromArgb((byte)200, (byte)150, (byte)150, (byte)150));
            tc_e.Width = time_distance*Ratio*2;
            tc_e.Height = time_distance*Ratio*2;
            tc_e.RenderTransform = new TranslateTransform();
            tc_e.RenderTransformOrigin = new Point(0.5, 0.5);
          
            tc_e.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(x_center));
            tc_e.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(y_center));


            SolidColorBrush lb = new SolidColorBrush();
            lb.Color = Color.FromArgb((byte)20, (byte)150, (byte)150, (byte)150);
            
            tc_e.Fill = lb;
     
               
            Point pt=new Point(0,0);

          //  lb.GradientStops.Add(new GradientStop(Colors.Blue,0.6));

            cavas.Children.Add(tc_e);
            textblock.Foreground = new SolidColorBrush(Color.FromArgb((byte)255,(byte)250,(byte)250,(byte)120));
            
         //   Canvas.SetLeft(tc_e, x);
        //    Canvas.SetTop(tc_e, y);
        }

        public void move(double dx,double dy)
        {
            tc_e.RenderTransform.SetValue(TranslateTransform.XProperty, (double)tc_e.RenderTransform.GetValue(TranslateTransform.XProperty) + dx);
            tc_e.RenderTransform.SetValue(TranslateTransform.YProperty, (double)tc_e.RenderTransform.GetValue(TranslateTransform.YProperty) + dy);
            textblock.RenderTransform.SetValue(TranslateTransform.XProperty, (double)textblock.RenderTransform.GetValue(TranslateTransform.XProperty) + dx);
            textblock.RenderTransform.SetValue(TranslateTransform.YProperty, (double)textblock.RenderTransform.GetValue(TranslateTransform.YProperty) + dy);
        }
        public void move_tostoryboard(Storyboard sb1, double dx, double dy, double time)
        {
            DoubleAnimation daX = new DoubleAnimation();
            DoubleAnimation daX1 = new DoubleAnimation();
            DoubleAnimation daY = new DoubleAnimation();
            DoubleAnimation daY1 = new DoubleAnimation();
            daX.To = (double)tc_e.RenderTransform.GetValue(TranslateTransform.XProperty) + dx;
            daX1.To = (double)textblock.RenderTransform.GetValue(TranslateTransform.XProperty) + dx;
            daY.To = (double)tc_e.RenderTransform.GetValue(TranslateTransform.YProperty) + dy;
            daY1.To = (double)textblock.RenderTransform.GetValue(TranslateTransform.YProperty) + dy;
            daX.Duration = TimeSpan.FromSeconds(time);
            daX1.Duration = daX.Duration;
            daY.Duration = daX.Duration;
            daY1.Duration = daX.Duration;
            Storyboard.SetTarget(daX, tc_e.RenderTransform);
            Storyboard.SetTargetProperty(daX, new PropertyPath(TranslateTransform.XProperty));
            sb1.Children.Add(daX);
            Storyboard.SetTarget(daY, tc_e.RenderTransform);
            Storyboard.SetTargetProperty(daY, new PropertyPath(TranslateTransform.YProperty));
            sb1.Children.Add(daY);
            Storyboard.SetTarget(daX1, textblock.RenderTransform);
            Storyboard.SetTargetProperty(daX1, new PropertyPath(TranslateTransform.XProperty));
            sb1.Children.Add(daX1);
            Storyboard.SetTarget(daY1, textblock.RenderTransform);
            Storyboard.SetTargetProperty(daY1, new PropertyPath(TranslateTransform.YProperty));
            sb1.Children.Add(daY1);
        }
        void update()
        {
            tc_e.Width = Ratio * time_distance*2;
            tc_e.Height = Ratio * time_distance*2;

            x_center = (double)tc_e.RenderTransform.GetValue(TranslateTransform.XProperty) + tc_e.Width / 2;
            y_center = (double)tc_e.RenderTransform.GetValue(TranslateTransform.YProperty) + tc_e.Height / 2;
        
        }
        public void reset(double maincity_xcenter,double maincity_ycenter,double ratio1,double distance1)
        {
            x_center = maincity_xcenter;
            y_center = maincity_ycenter;
            Ratio = ratio1;
            time_distance = distance1;
            
            tc_e.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(x_center));
            tc_e.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(y_center));
           
            update();
            textblock.RenderTransform.SetValue(TranslateTransform.XProperty, x_center + Ratio * time_distance);
            textblock.RenderTransform.SetValue(TranslateTransform.YProperty, y_center);
            textblock.Text = time_distance.ToString() + "小时圈";

        }

        double center2x(double center_x)
        {
            return center_x - Ratio * time_distance;
        }
        double center2y(double center_y)
        {
            return center_y - Ratio * time_distance;
        }
        public void disappear()
        {
            sb1 = new Storyboard();
            da1 = new DoubleAnimation();
            da2 = new DoubleAnimation();
            tc_e.Opacity = 0;
            textblock.Opacity = 0;

        }
        public void appear()
        {
            tc_e.Opacity = 1;
            textblock.Opacity = 1;
        }
    }
}
