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
using System.Windows.Threading;
using System.Collections.Generic;

namespace silver_map
{
    public class MoveAnimation
    {

        public DispatcherTimer dispatchertimer;
        public double targetX;
        public double targetY;
        public double v_x = 0;
        public double v_y = 0;
        public double target_time = 0;
      
        public Boolean isrunning = false;
        public Boolean ischosenmethod = false;
        public double running_time=0;
        public List<City_E> listtargetcities = new List<City_E>();
        public City_E citye=new City_E();

        public City_E target_citye;
        public Time_circle target_circle;
        public LineWay_Support lsp;
        public int biggerlock=0;

        public void  create_a_thread()
        {
            dispatchertimer=new DispatcherTimer();
            //dispatchertimer.Tick += new EventHandler(dispatchertimer_Tick);
            dispatchertimer.Interval = TimeSpan.FromMilliseconds(30);
        }
        public void start_CTN_animation(double vx,double vy,double time)
        {
            if (ischosenmethod == true&&isrunning==false)
            {
                dispatchertimer.Tick += new EventHandler(dispatchertimer_Tick_CTN);
                v_x = vx;
                v_y = vy;
                target_time = time;
                running_time = 0;
                dispatchertimer.Start();
                isrunning = true;
            }         
        }
        public void start_Bigger_animation(City_E ce)
        {
            if(biggerlock==0)
            citye = ce;

            if (ischosenmethod == true && isrunning == false&&biggerlock==0)
            {
                citye.ellipse.Width=citye.r;
                dispatchertimer.Tick += new EventHandler(dispatchertimer_Tick_Bigger);       
               // target_time = time;
                running_time = 0;
                dispatchertimer.Start();
                isrunning = true;
                biggerlock = 1;
            }         
        }
        public void start_smaller_animation(City_E ce)
        {
            citye = ce;

            if (ischosenmethod == true && isrunning == false)
            {
          
                dispatchertimer.Tick += new EventHandler(dispatchertimer_Tick_Smaller);
                // target_time = time;
                running_time = 0;
                dispatchertimer.Start();
                isrunning = true;
            }         
        }
        public void start_smaller_animation()
        {
           

            if (ischosenmethod == true && isrunning == false)
            {

                dispatchertimer.Tick += new EventHandler(dispatchertimer_Tick_Smaller);
                // target_time = time;
                running_time = 0;
                dispatchertimer.Start();
                isrunning = true;
            }
        }
        public void stop_animation_CTN()
        {
            dispatchertimer.Stop();
            dispatchertimer.Tick -= dispatchertimer_Tick_CTN;
            isrunning = false;
            ischosenmethod = false;
        }
        public void stop_animation_bigger()
        {
            dispatchertimer.Stop();
            dispatchertimer.Tick -= dispatchertimer_Tick_CTN;
            isrunning = false;
            ischosenmethod = false;
        }
        public void dispatchertimer_Tick_Bigger(object sender, EventArgs e)
        {
            running_time += 30;
            if (citye.ellipse.Width < citye.r + 30)
            {
                citye.ellipse.Width += 5;
                citye.ellipse.Height += 5;
                citye.ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, (double)citye.ellipse.RenderTransform.GetAnimationBaseValue(TranslateTransform.XProperty) - 2.5);
                citye.ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, (double)citye.ellipse.RenderTransform.GetAnimationBaseValue(TranslateTransform.YProperty) - 2.5);
            }
            else if(citye.ellipse.Width>citye.r+30)
            {
                citye.ellipse.Width = citye.r + 30;
                citye.ellipse.Height = citye.r + 30;
                dispatchertimer.Stop();
                dispatchertimer.Tick -= dispatchertimer_Tick_Bigger;
                isrunning = false;
                ischosenmethod = false;
            }
        }
        public void dispatchertimer_Tick_Smaller(object sender, EventArgs e)
        {
            running_time += 30;
            if (citye.ellipse.Width >citye.r)
            {
                citye.ellipse.Width -=7;
                citye.ellipse.Height -= 7;
                citye.ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, (double)citye.ellipse.RenderTransform.GetAnimationBaseValue(TranslateTransform.XProperty) + 3.5);
                citye.ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, (double)citye.ellipse.RenderTransform.GetAnimationBaseValue(TranslateTransform.YProperty) +3.5);
            }
            else if (citye.ellipse.Width <= citye.r )
            {
                citye.ellipse.Width = citye.r;
                citye.ellipse.Height = citye.r;
                citye.ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, citye.center2x(citye.x_center));
                citye.ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, citye.center2y(citye.y_center));
                dispatchertimer.Stop();
                dispatchertimer.Tick -= dispatchertimer_Tick_Smaller;
                isrunning = false;
                ischosenmethod = false;
             
                biggerlock = 0;
            }
        }

        public void dispatchertimer_Tick_CTN(object sender,EventArgs e) //continue animation
        {
            running_time += 30;
            for(int i=0;i<listtargetcities.Count;i++)
            {
                listtargetcities[i].enable_biggersmaller = 0;
                listtargetcities[i].move_all_translation(v_x, v_y);
               
           }
            target_circle.move(v_x, v_y);
           // lsp.move(v_x, v_y);
            v_x *= 0.95;
            v_y *= 0.95;
            if (running_time >= target_time)
            {
                dispatchertimer.Stop();
                dispatchertimer.Tick -= dispatchertimer_Tick_CTN;
                isrunning = false;
                ischosenmethod = false;
            }
          
        }
       
    }
}
