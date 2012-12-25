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
   public class City_E
    {
        public String city_name;
        public int rank;
        public double distance_road;          //to 中心城市
        public double distance_railway;
        public double distance_air;
        public string describe = "这座城市风景优美，引人入胜";
        public int population = 1000;
        public String landscape1="长城";
        public String landscape2="故宫";
        public String landscape3="外滩";

        public int which_distance = 0;
        public double []actual_distance={6,6,6};

        private Random random=new Random();


        public Ellipse ellipse;
     
        public TextBlock textblock;

         public Boolean is_oncavas = false;
         public Boolean is_centralcity = false;

         public double x_center = 0;
         public double y_center = 0;
         public double r = 72;
         public double angle = 0;
         public double angle_remember = 0;

         public int enable_biggersmaller = 1;

         public RadialGradientBrush radialgradientbrush=new RadialGradientBrush();
         Ellipse change_e;
         public Line line_entity;
         public SolidColorBrush line_brush = new SolidColorBrush(Colors.Yellow);
         public Storyboard storyboard=new Storyboard();
         public Storyboard storyboard2 = new Storyboard();
         public DoubleAnimation da1,da2,da3,da4;
         public int twinkling = 0;
         public double textoffset = 25;

      

        public City_E(string name, int _rank, double d1, double d2, double d3)
        {
            city_name = name;
            rank = _rank;
            distance_road = d1;
            distance_railway = d2;
            distance_air = d3;
            actual_distance[0] = d1;
            actual_distance[1] = d2;
            actual_distance[2] = d3;
       
       }
        public City_E()
        {
            city_name = "重庆";
            rank = 1;
            distance_road = 1;
            distance_railway = 1;
            distance_air = 1;
         
        }

        
        public void create_ellipse_oncavas(Canvas canvas,double xoffset_center,double yoffset_center)  //把椭圆放到画布上
        {
            x_center = xoffset_center;
            y_center = yoffset_center;

            ellipse = new Ellipse();
            //    this.ellipse.Stroke = new SolidColorBrush(Colors.Black);
            this.ellipse.StrokeThickness = 0;
            this.ellipse.Width = r;
            this.ellipse.Height = r;
            this.ellipse.Fill = radialgradientbrush;
            this.ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
         
  
            textblock = new TextBlock();
            textblock.FontFamily = new System.Windows.Media.FontFamily("SimHei");
            textblock.Text = city_name;
            textblock.FontSize =14;
            textblock.Foreground = new SolidColorBrush(Color.FromArgb((byte)255, (byte)255, (byte)255, (byte)255));
         //   textblock.Background = new SolidColorBrush(Color.FromArgb((byte)0, (byte)0, (byte)0, (byte)0));
          
          //   textblock.Background.Opacity = 0;
            textblock.RenderTransform = new TranslateTransform();
            canvas.Children.Add(ellipse);
            canvas.Children.Add(textblock);
            Canvas.SetLeft(this.textblock, 0 - 2);
            Canvas.SetTop(this.textblock, 0);

           ellipse.RenderTransform = new TranslateTransform();
          ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
          ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(x_center));
          ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(y_center));
          textblock.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(x_center) + textoffset);
          textblock.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(y_center) + textoffset);

          is_oncavas = true;
        }

       public void create_linetomaincity(double maincity_x,double maincity_y,Canvas canvas)
        {
            if (is_centralcity == false)
            {
                line_entity = new Line();

                line_entity.StrokeThickness = 1.5;
                line_entity.Stroke = line_brush;
                canvas.Children.Add(line_entity);

            }
            else line_entity = new Line();
        }
       public void set_line_position(double maincity_centerx, double maincity_centery, double own_centerx, double own_centery)
       {
           if (is_centralcity == false)
           {
               angle = Math.Atan((own_centery - maincity_centery) / (own_centerx - maincity_centerx));

               if (maincity_centerx < own_centerx)
               {
                   line_entity.X1 = own_centerx - Math.Cos(angle) * (r-10) / 2;
                   line_entity.Y1 = own_centery - Math.Sin(angle) * (r - 10) / 2;
                   line_entity.X2 = maincity_centerx + Math.Cos(angle) * (r - 10) / 2;
                   line_entity.Y2 = maincity_centery + Math.Sin(angle) * (r - 10) / 2;
               }
               else
               {
                   line_entity.X1 = own_centerx + Math.Cos(angle) * (r - 10) / 2;
                   line_entity.Y1 = own_centery + Math.Sin(angle) * (r - 10) / 2;
                   line_entity.X2 = maincity_centerx - Math.Cos(angle) * (r - 10) / 2;
                   line_entity.Y2 = maincity_centery - Math.Sin(angle) * (r - 10) / 2;
               }
           }
           }
       public void addellipse_to_storyBoard(Storyboard sb1, double targetX_center, double targetY_center)
       {
           DoubleAnimation daX = new DoubleAnimation();
           DoubleAnimation daX1 = new DoubleAnimation();
           DoubleAnimation daY = new DoubleAnimation();
           DoubleAnimation daY1 = new DoubleAnimation();
       
         
           daX.To = targetX_center - r / 2;
           daY.To = targetY_center - r / 2;
           daX1.To = daX.To + textoffset;
           daY1.To = daY.To + textoffset;

          
           daX.Duration = TimeSpan.FromSeconds(2);
           daY.Duration = TimeSpan.FromSeconds(2);
           if (is_centralcity == true)
           {
               daX.Duration = TimeSpan.FromSeconds(0.5);
               daY.Duration = TimeSpan.FromSeconds(0.5);
           }
           daX1.Duration = daX.Duration;
           daY1.Duration = daY.Duration;
           if (is_centralcity == false)
           {
           daY.EasingFunction = new BackEase();
           daX.EasingFunction = new BackEase();
               daX1.EasingFunction = daX.EasingFunction;
               daY1.EasingFunction = daY.EasingFunction;
           }
           Storyboard.SetTarget(daX, ellipse.RenderTransform);
           Storyboard.SetTargetProperty(daX, new PropertyPath(TranslateTransform.XProperty));
           sb1.Children.Add(daX);
           Storyboard.SetTarget(daY, ellipse.RenderTransform);
           Storyboard.SetTargetProperty(daY, new PropertyPath(TranslateTransform.YProperty));
           sb1.Children.Add(daY);
           Storyboard.SetTarget(daX1, textblock.RenderTransform);
           Storyboard.SetTargetProperty(daX1, new PropertyPath(TranslateTransform.XProperty));
           sb1.Children.Add(daX1);
           Storyboard.SetTarget(daY1, textblock.RenderTransform);
           Storyboard.SetTargetProperty(daY1, new PropertyPath(TranslateTransform.YProperty));
           sb1.Children.Add(daY1);
        
           x_center = targetX_center;
           y_center = targetY_center;
       }
       public void addall_to_StoryBoard(Storyboard sb1, double targetX_center, double targetY_center, double maincityX, double maincityY)
       {

          
           DoubleAnimation daX = new DoubleAnimation();
           DoubleAnimation daX1 = new DoubleAnimation();
           DoubleAnimation daY = new DoubleAnimation();
           DoubleAnimation daY1 = new DoubleAnimation();
           DoubleAnimation daX_line1 = new DoubleAnimation();
           DoubleAnimation daY_line1 = new DoubleAnimation();
           DoubleAnimation daX_line2= new DoubleAnimation();
           DoubleAnimation daY_line2 = new DoubleAnimation();
           angle = Math.Atan((targetY_center - maincityY) / (targetX_center - maincityX));

           if (maincityX < targetX_center)
           {
               daX_line1.To = targetX_center -Math.Cos(angle) * r / 2;
               daY_line1.To = targetY_center -Math.Sin(angle) * r / 2;
               daX_line2.To= maincityX + Math.Cos(angle) * r / 2;
               daY_line2.To = maincityY + Math.Sin(angle) * r / 2;
           }
           else
           {
               daX_line1.To = targetX_center +Math.Cos(angle) * r / 2;
               daY_line1.To = targetY_center +Math.Sin(angle) * r / 2;
               daX_line2.To = maincityX - Math.Cos(angle) * r / 2;
               daY_line2.To = maincityY - Math.Sin(angle) * r / 2;
           }

           daX.To = targetX_center - r / 2;
           daY.To = targetY_center - r / 2;
           daX1.To = daX.To + textoffset;
           daY1.To = daY.To + textoffset;
          
           daX.Duration = TimeSpan.FromSeconds(1);
           daY.Duration = TimeSpan.FromSeconds(1);
           daX1.Duration = daX.Duration;
           daY1.Duration = daY.Duration;
           daX_line1.Duration = daX.Duration;
           daY_line1.Duration = daY.Duration;
           daX_line2.Duration = daX.Duration;
           daY_line2.Duration = daY.Duration;
           daY.EasingFunction = new BackEase();
           daX.EasingFunction = new BackEase();
           daX1.EasingFunction = daX.EasingFunction;
           daY1.EasingFunction = daY.EasingFunction;
           Storyboard.SetTarget(daX, ellipse.RenderTransform);
           Storyboard.SetTargetProperty(daX, new PropertyPath(TranslateTransform.XProperty));
           sb1.Children.Add(daX);
           Storyboard.SetTarget(daY, ellipse.RenderTransform);
           Storyboard.SetTargetProperty(daY, new PropertyPath(TranslateTransform.YProperty));
           sb1.Children.Add(daY);
           Storyboard.SetTarget(daX1, textblock.RenderTransform);
           Storyboard.SetTargetProperty(daX1, new PropertyPath(TranslateTransform.XProperty));
           sb1.Children.Add(daX1);
           Storyboard.SetTarget(daY1, textblock.RenderTransform);
           Storyboard.SetTargetProperty(daY1, new PropertyPath(TranslateTransform.YProperty));
           sb1.Children.Add(daY1);
           Storyboard.SetTarget(daX_line1, line_entity);
           Storyboard.SetTargetProperty(daX_line1,new PropertyPath(Line.X1Property));
           sb1.Children.Add(daX_line1);
           Storyboard.SetTarget(daY_line1, line_entity);
           Storyboard.SetTargetProperty(daY_line1, new PropertyPath(Line.Y1Property));
           sb1.Children.Add(daY_line1);
           Storyboard.SetTarget(daX_line2, line_entity);
           Storyboard.SetTargetProperty(daX_line2, new PropertyPath(Line.X2Property));
           sb1.Children.Add(daX_line2);
           Storyboard.SetTarget(daY_line2, line_entity);
           Storyboard.SetTargetProperty(daY_line2, new PropertyPath(Line.Y2Property));
           sb1.Children.Add(daY_line2);

           x_center = targetX_center;
           y_center = targetY_center;
       }

       public void move_all_translation(double dx, double dy)
       {
           ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, (double)ellipse.RenderTransform.GetValue(TranslateTransform.XProperty) + dx);
           textblock.RenderTransform.SetValue(TranslateTransform.XProperty, (double)textblock.RenderTransform.GetValue(TranslateTransform.XProperty) + dx);
           ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, (double)ellipse.RenderTransform.GetValue(TranslateTransform.YProperty) + dy);
           textblock.RenderTransform.SetValue(TranslateTransform.YProperty, (double)textblock.RenderTransform.GetValue(TranslateTransform.YProperty) + dy);
           
           if(is_centralcity==false)
           {
               line_entity.X1 += dx;
               line_entity.X2 += dx;
               line_entity.Y1 += dy;
               line_entity.Y2 += dy;
           }
           update();
       

       }
       public void move_all_tostoryboard_translation(Storyboard sb1, double dx, double dy,double time)
       {
           storyboard.Stop();
           update();
           DoubleAnimation daX = new DoubleAnimation();
           DoubleAnimation daX1 = new DoubleAnimation();
           DoubleAnimation daY = new DoubleAnimation();
           DoubleAnimation daY1 = new DoubleAnimation();
           DoubleAnimation daX_line1 = new DoubleAnimation();
           DoubleAnimation daY_line1 = new DoubleAnimation();
           DoubleAnimation daX_line2 = new DoubleAnimation();
           DoubleAnimation daY_line2 = new DoubleAnimation();
             daX_line1.To = line_entity.X1 + dx;
            daY_line1.To = line_entity.Y1 + dy;
            daX_line2.To = line_entity.X2 + dx;
            daY_line2.To = line_entity.Y2 + dy;
             daX.To = (double)ellipse.RenderTransform.GetValue(TranslateTransform.XProperty)+dx;
             daX1.To = (double)textblock.RenderTransform.GetValue(TranslateTransform.XProperty) + dx;
             daY.To = (double)ellipse.RenderTransform.GetValue(TranslateTransform.YProperty) + dy;
             daY1.To = (double)textblock.RenderTransform.GetValue(TranslateTransform.YProperty) + dy;
             daX.Duration = TimeSpan.FromSeconds(time);
             daX1.Duration = daX.Duration;
             daY.Duration = daX.Duration;
             daY1.Duration = daX.Duration;
             daX_line1.Duration = daX.Duration;
             daX_line2.Duration = daX.Duration;
             daY_line1.Duration = daX.Duration;
             daY_line2.Duration = daX.Duration;
             Storyboard.SetTarget(daX, ellipse.RenderTransform);
             Storyboard.SetTargetProperty(daX, new PropertyPath(TranslateTransform.XProperty));
             sb1.Children.Add(daX);
             Storyboard.SetTarget(daY, ellipse.RenderTransform);
             Storyboard.SetTargetProperty(daY, new PropertyPath(TranslateTransform.YProperty));
             sb1.Children.Add(daY);
             Storyboard.SetTarget(daX1, textblock.RenderTransform);
             Storyboard.SetTargetProperty(daX1, new PropertyPath(TranslateTransform.XProperty));
             sb1.Children.Add(daX1);
             Storyboard.SetTarget(daY1, textblock.RenderTransform);
             Storyboard.SetTargetProperty(daY1, new PropertyPath(TranslateTransform.YProperty));
             sb1.Children.Add(daY1);
             Storyboard.SetTarget(daX_line1, line_entity);
             Storyboard.SetTargetProperty(daX_line1, new PropertyPath(Line.X1Property));
             sb1.Children.Add(daX_line1);
             Storyboard.SetTarget(daY_line1, line_entity);
             Storyboard.SetTargetProperty(daY_line1, new PropertyPath(Line.Y1Property));
             sb1.Children.Add(daY_line1);
             Storyboard.SetTarget(daX_line2, line_entity);
             Storyboard.SetTargetProperty(daX_line2, new PropertyPath(Line.X2Property));
             sb1.Children.Add(daX_line2);
             Storyboard.SetTarget(daY_line2, line_entity);
             Storyboard.SetTargetProperty(daY_line2, new PropertyPath(Line.Y2Property));
             sb1.Children.Add(daY_line2);
           x_center += dx;
           y_center += dy;       
       }
       public void set_all_position(double targetx_center, double targety_center,double maincity_xcenter,double maincity_ycenter)
       {

           if (is_centralcity == false)
           {
           ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(targetx_center));
           textblock.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(targetx_center) +textoffset);
           ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(targety_center));
           textblock.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(targety_center)+textoffset);
           ellipse.Width = r;
           ellipse.Height = r;

               if (maincity_xcenter < targetx_center)
               {
                   line_entity.X1 = targetx_center - Math.Cos(angle) * r*0.9 / 2;
                   line_entity.Y1 = targety_center - Math.Sin(angle) * r *0.9/ 2;             
               }
               else
               {
                   line_entity.X1 = targetx_center + Math.Cos(angle) * r*0.9 / 2;
                   line_entity.Y1 = targety_center + Math.Sin(angle) * r*0.9 / 2;
                } 
           }
           update();
       }
       public void update()
        {
            x_center = (double)ellipse.RenderTransform.GetValue(TranslateTransform.XProperty) + ellipse.Width / 2;
            y_center = (double)ellipse.RenderTransform.GetValue(TranslateTransform.YProperty) + ellipse.Height / 2;
        }
       public void animation_bigger()
       {
           if (enable_biggersmaller==1)
              {
              	update();
	           storyboard = new Storyboard();
	           da1 = new DoubleAnimation();
	           da2 = new DoubleAnimation();
	           da3 = new DoubleAnimation();
	           da4 = new DoubleAnimation();
	           da1.To = r + 20;
	           da2.To = r + 20;
	           da3.To =center2x(x_center) -10;
	           da4.To = center2y(y_center)  -10;
	           da1.Duration = TimeSpan.FromSeconds(0.3);
	           da2.Duration = TimeSpan.FromSeconds(0.3);
	           da3.Duration = TimeSpan.FromSeconds(0.3);
	           da4.Duration = TimeSpan.FromSeconds(0.3);
	           Storyboard.SetTarget(da1, ellipse);
	           Storyboard.SetTargetProperty(da1, new PropertyPath(Ellipse.WidthProperty));
	           Storyboard.SetTarget(da2, ellipse);
	           Storyboard.SetTargetProperty(da2, new PropertyPath(Ellipse.HeightProperty));
	           Storyboard.SetTarget(da3, ellipse.RenderTransform);
	           Storyboard.SetTargetProperty(da3, new PropertyPath(TranslateTransform.XProperty));
	           Storyboard.SetTarget(da4, ellipse.RenderTransform);
	           Storyboard.SetTargetProperty(da4, new PropertyPath(TranslateTransform.YProperty));
	           storyboard.Children.Add(da1);
	           storyboard.Children.Add(da2);
	           storyboard.Children.Add(da3);
	           storyboard.Children.Add(da4);
	           storyboard.Begin();
             }
       }
       public void animation_smaller()
       {
           if (enable_biggersmaller==1)
           {
	           storyboard.Stop();
	           storyboard = new Storyboard();
	           da1 = new DoubleAnimation();
	           da2 = new DoubleAnimation();
	           da3 = new DoubleAnimation();
	           da4 = new DoubleAnimation();
	           da1.To = r ;
	           da2.To = r ;
	           da3.To = center2x(x_center) ;
	           da4.To = center2y(y_center) ;
	           da3.Duration = TimeSpan.FromSeconds(0.3);
	           da4.Duration = TimeSpan.FromSeconds(0.3);
	           da1.Duration = TimeSpan.FromSeconds(0.5);
	           da2.Duration = TimeSpan.FromSeconds(0.5);
	           Storyboard.SetTarget(da1, ellipse);
	           Storyboard.SetTargetProperty(da1, new PropertyPath(Ellipse.WidthProperty));
	           Storyboard.SetTarget(da2, ellipse);
	           Storyboard.SetTargetProperty(da2, new PropertyPath(Ellipse.HeightProperty));
	           Storyboard.SetTarget(da3, ellipse.RenderTransform);
	           Storyboard.SetTargetProperty(da3, new PropertyPath(TranslateTransform.XProperty));
	           Storyboard.SetTarget(da4, ellipse.RenderTransform);
	           Storyboard.SetTargetProperty(da4, new PropertyPath(TranslateTransform.YProperty));
	           storyboard.Children.Add(da1);
	           storyboard.Children.Add(da2);
	           storyboard.Children.Add(da3);
	           storyboard.Children.Add(da4);
	           storyboard.Begin();
               }
       }
       public void start_twikling()
       {
           if (twinkling == 0)
           {
               twinkling = 1;
               storyboard = new Storyboard();
               da1 = new DoubleAnimation();
               da2 = new DoubleAnimation();
               da3 = new DoubleAnimation();
               da4 = new DoubleAnimation();
               da1.To = r + 30;
               da2.To = r + 30;
               da3.To = center2x(x_center) - 15;
               da4.To = center2y(y_center) - 15;
               da1.Duration = TimeSpan.FromSeconds(0.5);
               da2.Duration = TimeSpan.FromSeconds(0.5);
               da3.Duration = TimeSpan.FromSeconds(0.5);
               da4.Duration = TimeSpan.FromSeconds(0.5);
               Storyboard.SetTarget(da1, ellipse);
               Storyboard.SetTargetProperty(da1, new PropertyPath(Ellipse.WidthProperty));
               Storyboard.SetTarget(da2, ellipse);
               Storyboard.SetTargetProperty(da2, new PropertyPath(Ellipse.HeightProperty));
               Storyboard.SetTarget(da3, ellipse.RenderTransform);
               Storyboard.SetTargetProperty(da3, new PropertyPath(TranslateTransform.XProperty));
               Storyboard.SetTarget(da4, ellipse.RenderTransform);
               Storyboard.SetTargetProperty(da4, new PropertyPath(TranslateTransform.YProperty));
               storyboard.Children.Add(da1);
               storyboard.Children.Add(da2);
               storyboard.Children.Add(da3);
               storyboard.Children.Add(da4);
            //   da1.AutoReverse = true;
             //  da2.AutoReverse = true;
               //da3.AutoReverse = true;
          //     da4.AutoReverse = true;
               
               storyboard.AutoReverse = true;
               storyboard.RepeatBehavior = RepeatBehavior.Forever;
               storyboard.Begin();
           }
       }
       public void stop_twikling()
       {
           if (twinkling == 1)
           {
               twinkling = 0;
               storyboard.Stop();
               storyboard.Children.Clear();
               ellipse.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(x_center));
               textblock.RenderTransform.SetValue(TranslateTransform.XProperty, center2x(x_center) + textoffset);
               ellipse.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(y_center));
               textblock.RenderTransform.SetValue(TranslateTransform.YProperty, center2y(y_center) + textoffset);
               ellipse.Width = r;
               ellipse.Height = r;  
           }
       }
       public void add_line_storyboard(Storyboard sb1) //control the line opacity
       {
           da1 = new DoubleAnimation();
           da1.From = 0;
           da1.Duration = TimeSpan.FromSeconds(1);
           da1.To = 1;
           Storyboard.SetTarget(da1, line_entity);
           Storyboard.SetTargetProperty(da1, new PropertyPath(Line.OpacityProperty));
           sb1.Children.Add(da1);

       }
       public void disappear()
       {
         //  ellipse.Opacity = 0;
         //  line_entity.Opacity = 0;
        //   textblock.Opacity = 0;
           storyboard2 = new Storyboard();
           da1 = new DoubleAnimation();
           da2 = new DoubleAnimation();
           da3 = new DoubleAnimation();
           da1.From = 1;
           da2.From = 1;
           da3.From = 1;
           da1.To = 0;
           da2.To = 0;
           da3.To = 0;
           da1.Duration = TimeSpan.FromSeconds(0.8);
           da2.Duration = da1.Duration;
           da3.Duration = da1.Duration;
           Storyboard.SetTarget(da1, ellipse);
          Storyboard.SetTarget(da2, line_entity);
          Storyboard.SetTarget(da3, textblock);
          Storyboard.SetTargetProperty(da1, new PropertyPath(Ellipse.OpacityProperty));
          Storyboard.SetTargetProperty(da2, new PropertyPath(Line.OpacityProperty));
          Storyboard.SetTargetProperty(da3, new PropertyPath(TextBlock.OpacityProperty));
          storyboard2.Children.Add(da1);
          storyboard2.Children.Add(da2);
          storyboard2.Children.Add(da3);
          storyboard2.Begin();
       }
       public void appear()
       {
         //  ellipse.Opacity = 1;
         //  line_entity.Opacity = 1;
      //     textblock.Opacity = 1;
             storyboard2 = new Storyboard();
           da1 = new DoubleAnimation();
           da2 = new DoubleAnimation();
           da3 = new DoubleAnimation();
           da1.From = 0;
           da2.From = 0;
           da3.From = 0;
           da1.To = 1;
           da2.To = 1;
           da3.To = 1;
           da1.Duration = TimeSpan.FromSeconds(0.8);
           da2.Duration = da1.Duration;
           da3.Duration = da1.Duration;
           Storyboard.SetTarget(da1, ellipse);
          Storyboard.SetTarget(da2, line_entity);
          Storyboard.SetTarget(da3, textblock);
          Storyboard.SetTargetProperty(da1, new PropertyPath(Ellipse.OpacityProperty));
          Storyboard.SetTargetProperty(da2, new PropertyPath(Line.OpacityProperty));
          Storyboard.SetTargetProperty(da3, new PropertyPath(TextBlock.OpacityProperty));
          storyboard2.Children.Add(da1);
          storyboard2.Children.Add(da2);
          storyboard2.Children.Add(da3);
          storyboard2.Begin();
       }
       public double center2x(double center_x)
       {
           return center_x - r/2;
       }
       public double center2y(double center_y)
       {
           return center_y - r/2;
       }
       public double get_chosen_distance()
       {
           return actual_distance[which_distance];
       }
       public void update_chosendistance()
       {
           actual_distance[0] = distance_road;
           actual_distance[1] = distance_railway;
           actual_distance[2] = distance_air;
       }
    }
}
