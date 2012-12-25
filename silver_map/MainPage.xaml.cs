using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using silver_map.WCF;

namespace silver_map
{
	public partial class MainPage : UserControl
	{
        Ellipse e_temp,e_temp2;
        DoubleAnimation da;
        Storyboard sb;
        City_E maincity_e;

        City_E[] cities_e = { new City_E("上海", 1, 0, 0, 0), new City_E("南京", 1, 3.5, 1, 0.5), new City_E("江苏", 1, 2.5, 1, 0.5), new City_E("重庆", 1, 48, 30, 2) };

        List<City_E> cities_EL=new List<City_E>(4);

        Random r23 = new Random();
        
        List<city_info> City_list = new List<city_info>();
    
        int show_rank = 1;

        Boolean show_cities = false;
        button_change bu_change;
        Panel_info panel_info;
        Position_compute position_compute = new Position_compute();
        Time_circle time_circle;
     
        DoubleAnimation daX = new DoubleAnimation();
        DoubleAnimation daY = new DoubleAnimation();

        Boolean ismousedown = false;
        Boolean ismousedownfromellipse = false;
        double mouse_beginx = -1;
        double mouse_beginy = -1;
        double deltax = 0,deltax2=0;
        double deltay = 0,deltay2=0;

        int last_citynumber = 1;
       DateTime  last_move_time ;
       DateTime last_mouseup_time;
       int flag_click = 0;

       int tempp = 52;

       MoveAnimation moveanimation=new MoveAnimation();
       MoveAnimation biggersmalleranimation = new MoveAnimation();

       City_E chosencity;

       TextBlock tb_showway_oncanvas = new TextBlock();

       LineWay_Support lineway_support;
       int showway_flag = 0;
       
		public MainPage()
		{
			// 为初始化变量所必需
			InitializeComponent();
            
            
		}
        //void client_search_cityCompleted(object sender, search_cityCompletedEventArgs e)
        //{
        //    List<city_info> list = e.Result;
        //    textBlock1.Text = list[0].city_name;
        //}
        


        private void Canvas_Loaded(object sender, RoutedEventArgs e) 
        {

            cities_EL.Add(new City_E("上海", 1, 3.5, 2, 1));
            cities_EL.Add(new City_E("南京", 1, 3.5, 1, 0.5));
            cities_EL.Add(new City_E("江苏", 1, 2.5, 1, 0.5));
            cities_EL.Add(new City_E("重庆", 1, 48, 30, 2));
            String[] str_cityname = { "台北", "福州", "广州", "南宁", "南昌", "长沙", "武汉", "成都", "沈阳", "长春", "哈尔滨", "呼和浩特", "石家庄", "郑州", "香港", "澳门", "银川", "海口", "长春", "昆明", "兰州" };
            Random ran=new Random();
            for (int i = 4; i <= 24; i++)
            {
                cities_EL.Add(new City_E(str_cityname[i-4], 1,7+ran.Next(30), 19, 1));
            }

                //  set main city
                maincity_e = cities_EL[0];
            maincity_e.is_centralcity = true;
            time_circle = new Time_circle(240, 200, canvas1);

            for (int i = 0; i < cities_EL.Count; i++)
            {
                //set line
                cities_EL[i].create_linetomaincity(10, 10, canvas1);
                cities_EL[i].create_ellipse_oncavas(canvas1, 240-i*20 , 200);//2
                position_compute.set_ellipse_fillbrushcolor(cities_EL[i]);
                cities_EL[i].update();
                cities_EL[i].ellipse.MouseEnter += new MouseEventHandler(city_e_MouseEnter);
               cities_EL[i].ellipse.MouseLeave += new MouseEventHandler(ellipse_e_MouseLeave);
               cities_EL[i].ellipse.MouseLeftButtonDown += new MouseButtonEventHandler(ellipse_MouseLeftButtonDown);
               cities_EL[i].ellipse.MouseMove += new MouseEventHandler(ellipse_MouseMove);
               cities_EL[i].ellipse.MouseLeftButtonUp += new MouseButtonEventHandler(ellipse_MouseLeftButtonUp);   
                          
            }

             for (int i = 1; i < cities_EL.Count; i++)
            {
               cities_EL[i].set_line_position(maincity_e.x_center, maincity_e.y_center, cities_EL[i].x_center, cities_EL[i].y_center);
            } 

       
            double maincityx = (double)maincity_e.ellipse.RenderTransform.GetValue(TranslateTransform.XProperty) + maincity_e.ellipse.Width / 2;
            double maincityy = (double)maincity_e.ellipse.RenderTransform.GetValue(TranslateTransform.YProperty) + maincity_e.ellipse.Height / 2;
             sb=new Storyboard();
     
            Random r1 = new Random();
            //set animation
            for (int i = 1; i < cities_EL.Count; i++)
            {

                double angle1 = position_compute.angle_compute();
                double dx1 = position_compute.compute_x_relatetomaincity(cities_EL[i].distance_road, maincityx, angle1);
                double dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[i].distance_road, maincityy, angle1);
                cities_EL[i].angle_remember = angle1;

                cities_EL[i].addall_to_StoryBoard(sb, dx1, dy1, maincityx, maincityy);

                cities_EL[i].set_line_position(maincityx, maincityy, dx1 + 12.5,dy1+12.5);

            }
           sb.Begin();
           time_circle.tc_e.MouseLeftButtonDown += new MouseButtonEventHandler(tc_e_MouseLeftButtonDown);
           time_circle.tc_e.MouseMove += new MouseEventHandler(ellipse_MouseMove);
           time_circle.tc_e.MouseLeftButtonUp += new MouseButtonEventHandler(ellipse_MouseLeftButtonUp);
           canvas1.MouseWheel += new MouseWheelEventHandler(canvas1_MouseWheel);
           canvas1.MouseLeftButtonDown += new MouseButtonEventHandler(canvas1_MouseLeftButtonDown);
           canvas1.MouseMove+=new MouseEventHandler(canvas1_MouseMove);
           canvas1.MouseLeftButtonUp+=new MouseButtonEventHandler(canvas1_MouseLeftButtonUp);
           bu_change = new button_change(canvas1);
           bu_change.button.Click += new RoutedEventHandler(changebutton_MouseLeftButtonDown);
           panel_info = new Panel_info(canvas1);
           moveanimation.create_a_thread();
           biggersmalleranimation.create_a_thread();
           moveanimation.listtargetcities = cities_EL;
           moveanimation.target_circle = time_circle;
        //   moveanimation.lsp = lineway_support;
           tb_showway_oncanvas.Foreground = new SolidColorBrush(Colors.White);
           tb_showway_oncanvas.FontFamily = new System.Windows.Media.FontFamily("SimHei");
           tb_showway_oncanvas.FontSize = 21;
           tb_showway_oncanvas.Text = "火车时间图";
           canvas1.Children.Add(tb_showway_oncanvas);
           lineway_support = new LineWay_Support();
           lineway_support.Create(maincity_e, canvas1);
        }

       
        void client_search_cityCompleted(object sender, search_cityCompletedEventArgs e)
        {
            maincity_e.textblock.Text += "得到结果前。";
            City_list = e.Result;
            maincity_e.textblock.Text += "得到结果后。";
        }

        void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ismousedown == false)
            {
                ismousedown = true;
                ismousedownfromellipse = false;
                mouse_beginx = e.GetPosition(null).X;
                mouse_beginy = e.GetPosition(null).Y;
                if (moveanimation.isrunning == true)
                    moveanimation.stop_animation_CTN();
                if (bu_change.isShowing == true)
                    bu_change.disapear();
            }        
        }

        void canvas1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
             int d= e.Delta;
             cities_EL[last_citynumber].stop_twikling();
             bu_change.disapear();
             if (d > 0)//bigger
             {
                
                 time_circle.Ratio *= 0.9;
                 position_compute.ratio_road *= 0.9;
                 for (int i = 0; i < cities_EL.Count; i++)
                 {
                     double dx1;
                     double dy1;
                     if (showway_flag == 0)
                     {
                         dx1 = position_compute.compute_x_relatetomaincity(cities_EL[i].get_chosen_distance(), maincity_e.x_center, cities_EL[i].angle_remember);
                         dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[i].get_chosen_distance(), maincity_e.y_center, cities_EL[i].angle_remember);
                     }
                     else
                     {
                         dx1 = position_compute.compute_x_relatomaincity_Line(cities_EL[i].get_chosen_distance(),maincity_e.x_center);
                         dy1 = position_compute.compute_y_relatomaincity_Line(maincity_e.y_center);
                     }
                         cities_EL[i].set_all_position(dx1, dy1, maincity_e.x_center, maincity_e.y_center);
                 }
                 time_circle.reset(maincity_e.x_center, maincity_e.y_center, time_circle.Ratio, time_circle.time_distance);
             }
             else if (d < 0)
             {
                 time_circle.Ratio *= 1.1;
                 position_compute.ratio_road *= 1.1;
                 for (int i = 0; i < cities_EL.Count; i++)
                 {
                     double dx1;
                     double dy1;
                     if (showway_flag == 0)
                     {
                         dx1 = position_compute.compute_x_relatetomaincity(cities_EL[i].get_chosen_distance(), maincity_e.x_center, cities_EL[i].angle_remember);
                         dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[i].get_chosen_distance(), maincity_e.y_center, cities_EL[i].angle_remember);
                     }
                     else
                     {
                         dx1 = position_compute.compute_x_relatomaincity_Line(cities_EL[i].get_chosen_distance(), maincity_e.x_center);
                         dy1 = position_compute.compute_y_relatomaincity_Line(maincity_e.y_center);
                     }
                     cities_EL[i].set_all_position(dx1, dy1, maincity_e.x_center, maincity_e.y_center);
                 }
                 time_circle.reset(maincity_e.x_center, maincity_e.y_center, time_circle.Ratio, time_circle.time_distance);
             }

        }

        void tc_e_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bu_change.disapear();
            ismousedown = true;
            ismousedownfromellipse = true;
            last_move_time = DateTime.Now;
            mouse_beginx = e.GetPosition(null).X;
            mouse_beginy = e.GetPosition(null).Y;
            if (moveanimation.isrunning == true)
                moveanimation.stop_animation_CTN();

         //   Random ra=new Random();
          //  Control_Display_ShowWay(ra.Next(2));
        }

        void ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ismousedown == true&&ismousedownfromellipse==true) //continue move a little farther as animation
            {

                if ((Math.Abs(deltax) + Math.Abs(deltay)) > 0)
                {
                    moveanimation.ischosenmethod = true;
                    moveanimation.start_CTN_animation(deltax * 1, deltay * 1, 100 + 100 * (Math.Abs(deltax) + Math.Abs(deltay)));
                    deltay = 0;
                    deltax = 0;
                }  

            }
         
            ismousedown = false;
            ismousedownfromellipse = false;
           //detect double click
         /*   if (time_circle.tc_e != (sender as Ellipse))
            {
                if (flag_click == 0)
                {
                    flag_click = 1;
                    last_mouseup_time = DateTime.Now;
                }
                else if (flag_click == 1)
                {
                    double deltatime = (DateTime.Now - last_mouseup_time).Milliseconds;
                    if (deltatime < 200)// double click;
                    {
                        int i;
                        flag_click = 0;
                        last_mouseup_time = DateTime.Now;
                        for (i = 0; i < cities_EL.Count; i++)
                        {
                            if (cities_EL[i].ellipse == (sender as Ellipse))
                                break;
                        }
                       // cities_EL[i].textblock.Text += "doubleclick";
                        // change main city
                        maincity_e.is_centralcity = false;
                        cities_EL[i].is_centralcity = true;
                        maincity_e = cities_EL[i];
                        double maincityx = (double)maincity_e.ellipse.RenderTransform.GetValue(TranslateTransform.XProperty) + maincity_e.ellipse.Width / 2;
                        double maincityy = (double)maincity_e.ellipse.RenderTransform.GetValue(TranslateTransform.YProperty) + maincity_e.ellipse.Height / 2;
                        sb = new Storyboard();

                        Random r1 = new Random();
                        //set animation
                        for (int j = 1;j < cities_EL.Count;j++)
                        {
                            if (j != i)
                            {
                                double angle1 = r1.NextDouble() * 6.28;
                                double dx1 = position_compute.compute_x_relatetomaincity(cities_EL[j].distance_road, maincityx, angle1);
                                double dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[j].distance_road, maincityy, angle1);
                                cities_EL[j].angle_remember = angle1;

                                cities_EL[j].addall_to_StoryBoard(sb, dx1, dy1, maincityx, maincityy);

                                cities_EL[j].set_line_position(maincityx, maincityy, dx1 + 12.5, dy1 + 12.5);
                              
                            }
                          
                        }
                        sb.Begin();
                    }
                    else if (deltatime > 200)
                    {
                        last_mouseup_time = DateTime.Now;
                    }

                }  
            }*/
                   
        }

        void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (ismousedown == true&&ismousedownfromellipse==true)
            {
                panel_info.disappear();
                cities_EL[last_citynumber].stop_twikling();
                double currentx = e.GetPosition(null).X;
                double currenty = e.GetPosition(null).Y;
                deltax = currentx - mouse_beginx;
                deltay = currenty - mouse_beginy;
                for (int i = 0; i < cities_EL.Count; i++)
                {
                    cities_EL[i].move_all_translation(currentx - mouse_beginx, currenty - mouse_beginy);
                   
                }
                time_circle.move(currentx - mouse_beginx, currenty - mouse_beginy);
                mouse_beginx = currentx;
                mouse_beginy = currenty;

            //    cities_EL[0].textblock.Text = (DateTime.Now - last_move_time).Milliseconds.ToString();
                last_move_time = DateTime.Now;
            }
          
        }

        void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
       //     Search_cityClient client = new Search_cityClient();
       //     client.search_cityCompleted += new EventHandler<search_cityCompletedEventArgs>(client_search_cityCompleted);
       //     maincity_e.textblock.Text += "+";
            
        //    client.search_cityAsync("杭州", 52);
      //      if (tempp == 52)
          //      tempp = 5;
            
        //    for (int u = 0; u < City_list.Count; u++)
       //     {
        //        maincity_e.textblock.Text += City_list[u].city_name;
       //     }
        //    Control_Display_Time(r23.Next(40));
            if (moveanimation.isrunning == true)
            {
                moveanimation.stop_animation_CTN();
         //       biggersmalleranimation.stop_animation_bigger();
            }
            ismousedown = true;
            ismousedownfromellipse = true;
            mouse_beginx = e.GetPosition(null).X;
            mouse_beginy = e.GetPosition(null).Y;
            int i=0;           
            // first clean the last used city_entity
            if (maincity_e.ellipse != (sender as Ellipse))
            {
                if (last_citynumber >= 0)
                {
                    cities_EL[last_citynumber].line_entity.StrokeThickness = 1.5;
                    SolidColorBrush scb1 = cities_EL[last_citynumber].line_brush;
                    cities_EL[last_citynumber].line_brush.Color = Color.FromArgb((byte)150, (byte)scb1.Color.R, (byte)scb1.Color.G, (byte)scb1.Color.B);
                    cities_EL[last_citynumber].ellipse.Width = cities_EL[last_citynumber].r;
                    cities_EL[last_citynumber].ellipse.Height = cities_EL[last_citynumber].r;
                    cities_EL[last_citynumber].stop_twikling();
                 //   cities_EL[last_citynumber].
                }
                bu_change.disapear();
                panel_info.disappear();

                for (i = 0; i < cities_EL.Count; i++)
                {
                    if (cities_EL[i].ellipse == (sender as Ellipse))
                        break;
                }
           //     biggersmalleranimation.stop_animation_bigger();

           //      client.search_cityAsync("上海",52);
       

                if (cities_EL[i].twinkling == 0)
                {
                    cities_EL[i].start_twikling();
                    chosencity = cities_EL[i];
                }
                if (bu_change.isShowing == false)
                {       
                  //  bu_change.isShowing = true;
                    bu_change.showbutton(e.GetPosition(canvas1).X, e.GetPosition(canvas1).Y + 20);
                }
                if (panel_info.isShowing == false)
                {
                    panel_info.update_message(cities_EL[i]);
                    panel_info.show(cities_EL[i].x_center +40, cities_EL[i].y_center -80);

                }
           

                SolidColorBrush scb = cities_EL[i].line_brush;
                cities_EL[i].line_brush.Color = Color.FromArgb((byte)255, (byte)scb.Color.R, (byte)scb.Color.G, (byte)scb.Color.B);
               cities_EL[i].line_entity.StrokeThickness = 2.5;
              //  cities_EL[i].ellipse.Width =cities_EL[i].r+10;
             //   cities_EL[i].ellipse.Height = cities_EL[i].r + 10;
                last_citynumber = i;
                //set time circle
                time_circle.reset(maincity_e.x_center, maincity_e.y_center, position_compute.ratio_road, cities_EL[i].get_chosen_distance());
            }
        }

        void city_e_MouseEnter(object sender, EventArgs e)
        {
            int p;
            if (ismousedown == false)
            {
             
                City_E ce;
                for (p= 0;p < cities_EL.Count;p++)
                {
                    if(cities_EL[p].ellipse==(sender as Ellipse))
                        break;
                }
                
               // cities_EL[p].animation_bigger();
                if (cities_EL[p].twinkling == 0)
                {
              //      biggersmalleranimation.ischosenmethod = true;
              //      biggersmalleranimation.start_Bigger_animation(cities_EL[p]);
              //      cities_EL[p].textblock.Text += "+";
                    cities_EL[p].ellipse.Width = cities_EL[p].r + 5;
                    cities_EL[p].ellipse.Height = cities_EL[p].r + 5;
                }
                //cities_EL[p].ellipse.Width = cities_EL[p].r;
                //cities_EL[p].ellipse.Height = cities_EL[p].r;
                //cities_EL[p].ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, cities_EL[p].center2x(cities_EL[p].x_center));
                //cities_EL[p].ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, cities_EL[p].center2y(cities_EL[p].y_center));
            }
          
        }
        void ellipse_e_MouseLeave(object sender, EventArgs e)
        {
           
             if (ismousedown == false)
            {
                ismousedown = false;
                ismousedownfromellipse = false;
                //City_E ce;
                int p;
                for (p = 0; p < cities_EL.Count; p++)
                {
                    if (cities_EL[p].ellipse == (sender as Ellipse))
                        break;
                }
                if (cities_EL[p].twinkling == 0)
                {
                    cities_EL[p].ellipse.Width = cities_EL[p].r ;
                    cities_EL[p].ellipse.Height = cities_EL[p].r;
                 }
              
            //    biggersmalleranimation.stop_animation_bigger();
             //   biggersmalleranimation.ischosenmethod = true;
             //   biggersmalleranimation.start_smaller_animation(cities_EL[p]);
             //   cities_EL[p].textblock.Text += "!";
                
            }
         
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
           /* if (biggersmalleranimation.biggerlock == 1&&ismousedownfromellipse==false) // mistake 
            {
            //    biggersmalleranimation.stop_animation_bigger();
                City_E acity = biggersmalleranimation.citye;
                acity.ellipse.Width = acity.r;
                acity.ellipse.Height = acity.r;
                acity.ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, acity.center2x(acity.x_center));
                acity.ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, acity.center2y(acity.y_center));
                biggersmalleranimation.stop_animation_bigger();
                biggersmalleranimation.biggerlock = 0;
              
            }*/
            bu_change.disapear();
          if (ismousedown == true&&ismousedownfromellipse==true)
            {
                cities_EL[last_citynumber].stop_twikling();
                double currentx = e.GetPosition(null).X;
                double currenty = e.GetPosition(null).Y;
                for (int i = 0; i < cities_EL.Count; i++)
                {
                    cities_EL[i].move_all_translation(currentx - mouse_beginx, currenty - mouse_beginy);
                }
                time_circle.move(currentx - mouse_beginx, currenty - mouse_beginy);
                
                mouse_beginx = currentx;
                mouse_beginy = currenty;
             }
          else if (ismousedown == true && ismousedownfromellipse == false)
          {
              panel_info.disappear();
              cities_EL[last_citynumber].stop_twikling();
              double currentx = e.GetPosition(null).X;
              double currenty = e.GetPosition(null).Y;
              double smaller=0.8;
              for (int i = 0; i < cities_EL.Count; i++)
              {  
                  cities_EL[i].move_all_translation((currentx - mouse_beginx)*smaller, (currenty - mouse_beginy)*smaller);
              }
              time_circle.move((currentx - mouse_beginx) * smaller, (currenty - mouse_beginy) * smaller);
              lineway_support.move((currentx - mouse_beginx) * smaller, (currenty - mouse_beginy) * smaller);
              deltax2 = deltax;
              deltay2 = deltay;
              deltax = currentx- mouse_beginx;
              deltay = currenty - mouse_beginy;
           
              mouse_beginx = currentx;
              mouse_beginy = currenty;
          }
        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        //Control_Display_Time(18);
       // Control_Display_ShowWay(1);
            //Control_Display_FindCityAsTargetCity("兰州");
            if (ismousedown == true)
            {
              
                moveanimation.ischosenmethod = true;
                if(Math.Abs(deltax2) + Math.Abs(deltay2)>0)
                moveanimation.start_CTN_animation(deltax2 * 1, deltay2 * 1, 100 + 100 * (Math.Abs(deltax2) + Math.Abs(deltay2)));
           
                deltax = 0;
                deltay = 0;
                deltax2 = 0;
                deltay2 = 0;
                ismousedown = false;
                ismousedownfromellipse = false;
               

            }
        }

        private void changebutton_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
          //  maincity_e.textblock.Text += "+";
        //    Search_cityClient client = new Search_cityClient();
        //    client.search_cityCompleted += new EventHandler<search_cityCompletedEventArgs>(client_search_cityCompleted);
        //    client.search_cityAsync("上海",52);
         //   for (int u = 0; u < City_list.Count(); u++)
       //     {
        //               maincity_e.textblock.Text += City_list[u].city_name;
         //   }         
            get_to_new_condition(chosencity);
        }


        private void get_to_new_condition(City_E newmaincity)
        {
            List<City_E> Oldlist = cities_EL;
            maincity_e.is_centralcity = false;
            newmaincity.is_centralcity = true;
           // maincity_e = newmaincity;
            City_E oldmaincity=maincity_e;
            time_circle.tc_e.MouseLeftButtonDown -= tc_e_MouseLeftButtonDown;
            time_circle.tc_e.MouseMove -= ellipse_MouseMove;
            time_circle.tc_e.MouseLeftButtonUp -= ellipse_MouseLeftButtonUp;
            canvas1.MouseWheel-= canvas1_MouseWheel;
            canvas1.MouseLeftButtonDown -= canvas1_MouseLeftButtonDown;
            canvas1.MouseMove -= canvas1_MouseMove;
            canvas1.MouseLeftButtonUp -= canvas1_MouseLeftButtonUp;
            bu_change.button.Click -= changebutton_MouseLeftButtonDown;

            for (int i = 0; i < cities_EL.Count; i++)
            {
         //       if (cities_EL[i] != chosencity)
                {
                    canvas1.Children.Remove(cities_EL[i].ellipse);
                    canvas1.Children.Remove(cities_EL[i].line_entity);
                    canvas1.Children.Remove(cities_EL[i].textblock);
                    cities_EL[i].ellipse.MouseEnter -= city_e_MouseEnter;
                    cities_EL[i].ellipse.MouseLeave -= ellipse_e_MouseLeave;
                    cities_EL[i].ellipse.MouseLeftButtonDown -= ellipse_MouseLeftButtonDown;
                    cities_EL[i].ellipse.MouseMove -= ellipse_MouseMove;
                    cities_EL[i].ellipse.MouseLeftButtonUp -= ellipse_MouseLeftButtonUp;                            
                }          
            }
           cities_EL = new List<City_E>();

           position_compute.angle_set = 0;
           position_compute.now_set_color_number = 0;
           for (int i = 0; i < Oldlist.Count; i++)
           {
               cities_EL.Add(Oldlist[i]);
               if (cities_EL[i].is_centralcity == true)//set new maincity
                   maincity_e = cities_EL[i];
              
          //     cities_EL[i].create_linetomaincity(10, 10, canvas1);
               cities_EL[i].create_ellipse_oncavas(canvas1, cities_EL[i].x_center, cities_EL[i].y_center);
               
               position_compute.set_ellipse_fillbrushcolor(cities_EL[i]);
           }

            //animation
           sb = new Storyboard();
           maincity_e.addellipse_to_storyBoard(sb, oldmaincity.x_center, oldmaincity.y_center);
           sb.Begin();  //make the maincity to center

           for (int i = 0; i < cities_EL.Count; i++)
           {
               random_set_citytime_value(cities_EL[i]);
            }
           cities_EL.Reverse();

           Storyboard sb1 = new Storyboard();
           position_compute.angle_set = 0;
           position_compute.cities_number = cities_EL.Count;
           for (int i = 0; i < cities_EL.Count; i++)
           {
               if (cities_EL[i] != maincity_e)
               {
                   double angle1 = position_compute.angle_compute();
                   cities_EL[i].angle_remember = angle1;
                   double dx1 = position_compute.compute_x_relatetomaincity(cities_EL[i].actual_distance[cities_EL[i].which_distance], maincity_e.x_center, angle1);
                   double dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[i].actual_distance[cities_EL[i].which_distance], maincity_e.y_center, angle1);
                   cities_EL[i].angle_remember = angle1;
                  cities_EL[i].addellipse_to_storyBoard(sb1, dx1, dy1);
                   //cities_EL[i].set_line_position(maincity_e.x_center, maincity_e.y_center, dx1 + 12.5, dy1 + 12.5);
               }
              }
            sb1.BeginTime=TimeSpan.FromSeconds(0.65);
           sb1.Begin();

      //set line
           Storyboard sb2 = new Storyboard();
           for (int i = 0; i < cities_EL.Count; i++)
           {
               cities_EL[i].create_linetomaincity(maincity_e.x_center, maincity_e.y_center, canvas1);
               cities_EL[i].line_entity.X1 = maincity_e.x_center;
               cities_EL[i].line_entity.X2 = maincity_e.x_center;
               cities_EL[i].line_entity.Y1 = maincity_e.y_center;
               cities_EL[i].line_entity.Y2 = maincity_e.y_center;
               cities_EL[i].line_entity.Opacity=0;
               if(cities_EL[i].is_centralcity==false)
               cities_EL[i].set_line_position(maincity_e.x_center, maincity_e.y_center, cities_EL[i].x_center, cities_EL[i].y_center);
               cities_EL[i].ellipse.MouseEnter += new MouseEventHandler(city_e_MouseEnter);
               cities_EL[i].ellipse.MouseLeave += new MouseEventHandler(ellipse_e_MouseLeave);
               cities_EL[i].ellipse.MouseLeftButtonDown += new MouseButtonEventHandler(ellipse_MouseLeftButtonDown);
               cities_EL[i].ellipse.MouseMove += new MouseEventHandler(ellipse_MouseMove);
               cities_EL[i].ellipse.MouseLeftButtonUp += new MouseButtonEventHandler(ellipse_MouseLeftButtonUp);   
               if(cities_EL[i]!=maincity_e)
                   cities_EL[i].is_centralcity = false;
               cities_EL[i].add_line_storyboard(sb2);
            }
            sb2.BeginTime=TimeSpan.FromSeconds(1.4);
            sb2.Begin();

           time_circle.tc_e.MouseLeftButtonDown += new MouseButtonEventHandler(tc_e_MouseLeftButtonDown);
           time_circle.tc_e.MouseMove += new MouseEventHandler(ellipse_MouseMove);
           time_circle.tc_e.MouseLeftButtonUp += new MouseButtonEventHandler(ellipse_MouseLeftButtonUp);
           canvas1.MouseWheel += new MouseWheelEventHandler(canvas1_MouseWheel);
           canvas1.MouseLeftButtonDown += new MouseButtonEventHandler(canvas1_MouseLeftButtonDown);
           canvas1.MouseMove += new MouseEventHandler(canvas1_MouseMove);
           canvas1.MouseLeftButtonUp += new MouseButtonEventHandler(canvas1_MouseLeftButtonUp);
        
           bu_change.button.Click += new RoutedEventHandler(changebutton_MouseLeftButtonDown);
           moveanimation.listtargetcities = cities_EL;
           moveanimation.target_circle = time_circle;


           canvas1.Children.Remove(panel_info.stackpanel);
           canvas1.Children.Remove(panel_info.stackpanel2);
           canvas1.Children.Add(panel_info.stackpanel);
           canvas1.Children.Add(panel_info.stackpanel2);
           showway_flag = 0;
           lineway_support.disappear();
           panel_info.disappear();
           bu_change.disapear();
        }

        public void random_set_citytime_value(City_E acity)
        {
          //  Random r = new Random();
            acity.distance_road = r23.Next(30) + 2;
            acity.distance_railway = r23.Next(24) + 4;
            acity.distance_air = r23.Next(2) + 0.5;
            acity.update_chosendistance();
        }

        public void Control_Display_Time(double timelimit)
        {
            for (int i = 0; i < cities_EL.Count; i++)
            {
                if (cities_EL[i].get_chosen_distance() > timelimit)
                {
                    if(cities_EL[i].line_entity.Opacity==1)
                    cities_EL[i].disappear();
                }
                else
                {
                    if(cities_EL[i].line_entity.Opacity==0)
                    cities_EL[i].appear();
                }
            }
        } //only show those cities which arriving time is within the timelimit.

        public void Control_Display_TravelWay(int way) // 0=by car;1=by train; 2=by air
        {
            Storyboard sb1 = new Storyboard();
            if (maincity_e.which_distance != way)
            {
                if(way==0)
                {
                     tb_showway_oncanvas.Text = "时间地图：汽车";
                }
                else if(way==1)
                {
                     tb_showway_oncanvas.Text = "时间地图：火车";
                }
                else if(way==2)
                {
                     tb_showway_oncanvas.Text = "时间地图：飞机";
                }
                for (int i = 0; i < cities_EL.Count; i++)
                {
                    cities_EL[i].which_distance = way;
                    if (cities_EL[i] != maincity_e)
                    {
                        double dx1 = position_compute.compute_x_relatetomaincity(cities_EL[i].get_chosen_distance(), maincity_e.x_center, cities_EL[i].angle_remember);
                        double dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[i].get_chosen_distance(), maincity_e.y_center, cities_EL[i].angle_remember);
                        cities_EL[i].addellipse_to_storyBoard(sb1, dx1, dy1);
                        cities_EL[i].set_line_position(maincity_e.x_center, maincity_e.y_center, cities_EL[i].x_center, cities_EL[i].y_center);
                        cities_EL[i].add_line_storyboard(sb1);
                    }

                }
                sb1.Begin();
            }
        }

        public void Control_Display_ShowWay(int showway)// 0 is normal way, 1 is Line way
        {
            if (showway == 0)
            {
                showway_flag = 0;
                lineway_support.disappear();
                Control_Display_ShowEllipse(1);
                Storyboard sb1 = new Storyboard();
                for (int i = 0; i < cities_EL.Count; i++)
                {
                   
                    if (cities_EL[i] != maincity_e)
                    {
                        double dx1 = position_compute.compute_x_relatetomaincity(cities_EL[i].get_chosen_distance(), maincity_e.x_center, cities_EL[i].angle_remember);
                        double dy1 = position_compute.compute_Y_relateto_maincity(cities_EL[i].get_chosen_distance(), maincity_e.y_center, cities_EL[i].angle_remember);
                        cities_EL[i].addellipse_to_storyBoard(sb1, dx1, dy1);
                        cities_EL[i].set_line_position(maincity_e.x_center, maincity_e.y_center, cities_EL[i].x_center, cities_EL[i].y_center);
                        cities_EL[i].add_line_storyboard(sb1);
                        cities_EL[i].textblock.FontSize =14;
                    }
                }
                sb1.Begin();
            }
               else if(showway==1)//change to line way
            {
                lineway_support.set(maincity_e);
               lineway_support.appear();
               Control_Display_ShowEllipse(0);
                showway_flag = 1;
                Storyboard sb1 = new Storyboard();
                for (int i = 0; i < cities_EL.Count; i++)
                {
                    if (cities_EL[i] != maincity_e)
                    {
                        double dx1 = position_compute.compute_x_relatomaincity_Line(cities_EL[i].get_chosen_distance(), maincity_e.x_center);
                        double dy1 = position_compute.compute_y_relatomaincity_Line(maincity_e.y_center);
                        cities_EL[i].addellipse_to_storyBoard(sb1, dx1, dy1);
                        cities_EL[i].set_line_position(maincity_e.x_center, maincity_e.y_center, cities_EL[i].x_center, cities_EL[i].y_center);
                      //  cities_EL[i].add_line_storyboard(sb1);
                        cities_EL[i].line_entity.Opacity = 0;
                        cities_EL[i].textblock.FontSize = 17;
                    }
                }
                sb1.Begin();
            }
        }

        public void Control_Display_ShowEllipse(int WhetherShow)//0 = not show;1=show
        {
            for (int i = 0; i < cities_EL.Count; i++)
            {
                if(WhetherShow==0)
                cities_EL[i].ellipse.Opacity = 0;
                else
                    cities_EL[i].ellipse.Opacity = 1;
            }
        }

        public int Control_Display_FindCityAsTargetCity(string city_name)// set the city as if mouse down on it
        {
            if (maincity_e.city_name.Equals(city_name))
                return 0;
            if (moveanimation.isrunning == true)
            {
                moveanimation.stop_animation_CTN();
                //       biggersmalleranimation.stop_animation_bigger();
            }
            if (last_citynumber >= 0)
            {
                cities_EL[last_citynumber].line_entity.StrokeThickness = 1.5;
                SolidColorBrush scb1 = cities_EL[last_citynumber].line_brush;
                cities_EL[last_citynumber].line_brush.Color = Color.FromArgb((byte)150, (byte)scb1.Color.R, (byte)scb1.Color.G, (byte)scb1.Color.B);
                cities_EL[last_citynumber].ellipse.Width = cities_EL[last_citynumber].r;
                cities_EL[last_citynumber].ellipse.Height = cities_EL[last_citynumber].r;
                cities_EL[last_citynumber].stop_twikling();
                //   cities_EL[last_citynumber].
            }
            bu_change.disapear();
            panel_info.disappear();
            int i = 0;
            for (i = 0; i < cities_EL.Count; i++)
            {
                if (cities_EL[i].city_name.Equals(city_name))
                    break;
            }
          

                if (cities_EL[i].twinkling == 0)
                {
                    cities_EL[i].start_twikling();
                    chosencity = cities_EL[i];
                }
            if (bu_change.isShowing == false)
            {
                //  bu_change.isShowing = true;
                bu_change.showbutton(cities_EL[i].x_center+40, cities_EL[i].y_center + 40);
            }
            if (panel_info.isShowing == false)
            {
                panel_info.update_message(cities_EL[i]);
                panel_info.show(cities_EL[i].x_center + 40, cities_EL[i].y_center - 80);

            }


            SolidColorBrush scb = cities_EL[i].line_brush;
            cities_EL[i].line_brush.Color = Color.FromArgb((byte)255, (byte)scb.Color.R, (byte)scb.Color.G, (byte)scb.Color.B);
            cities_EL[i].line_entity.StrokeThickness = 2.5;
            //  cities_EL[i].ellipse.Width =cities_EL[i].r+10;
            //   cities_EL[i].ellipse.Height = cities_EL[i].r + 10;
            last_citynumber = i;
            //set time circle
            time_circle.reset(maincity_e.x_center, maincity_e.y_center, position_compute.ratio_road, cities_EL[i].get_chosen_distance());
            return 0;
        }

	}
}