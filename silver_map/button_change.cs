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
    public class button_change
    {
        public Boolean isShowing = false;

        public Button button=new Button();
        Brush brush;
        Storyboard sb;
        DoubleAnimation da;

        public button_change(Canvas canvas)
        {
            button.Content="作为中心城市";
            canvas.Children.Add(button);
            button.RenderTransform = new TranslateTransform();
            button.Opacity = 0;
            brush=new SolidColorBrush(Colors.Orange);
            button.Background = brush;
            button.FontFamily=new FontFamily("SimHei");
            button.MouseEnter += new MouseEventHandler(button_MouseEnter);



        }

        void button_MouseEnter(object sender, MouseEventArgs e)
        {
     //      button.Background= new SolidColorBrush(Colors.Gray);
        }

        public void showbutton(double targetx, double targety)
        {
           
            button.Opacity = 0;
           // button.Background.
            isShowing = true;
          
            button.RenderTransform.SetValue(TranslateTransform.XProperty, targetx);
            button.RenderTransform.SetValue(TranslateTransform.YProperty, targety);

            sb = new Storyboard();
            da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1.5);
            Storyboard.SetTarget(da, button);
            Storyboard.SetTargetProperty(da, new PropertyPath(Button.OpacityProperty));
            sb.Children.Add(da);
            sb.Begin();

        }
        public void disapear()
        {
            isShowing = false;
            button.Opacity = 0;
        }
    }
}
