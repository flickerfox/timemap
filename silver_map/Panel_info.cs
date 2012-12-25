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
using System.Windows.Media.Imaging;
using System.Windows.Browser;


namespace silver_map
{
    public class Panel_info
    {
       
        public StackPanel stackpanel;
        public StackPanel stackpanel2;
        public Button tb_cityname=new Button();
        public TextBlock tb_rank = new TextBlock();
        public TextBlock tb_roaddis = new TextBlock();
        public TextBlock tb_raildis = new TextBlock();
        public TextBlock tb_airdis = new TextBlock();
        public TextBlock tb_describe = new TextBlock();
        public TextBlock tb_population = new TextBlock();
        public TextBlock tb_landscape = new TextBlock();
        public TextBlock tb_landscape2 = new TextBlock();
        public TextBlock tb_landscape3 = new TextBlock();

        public Button bu1 = new Button();
        public Button bu2 = new Button();
        public Button bu3 = new Button();
        public City_E citye=new City_E();

        public Boolean isShowing = false;

        Storyboard sb1;
        DoubleAnimation dax, day,da1;


        public Panel_info(Canvas canvas)
        {
            stackpanel = new StackPanel();
            stackpanel2 = new StackPanel();
         
            stackpanel.Opacity =0;
            stackpanel2.Opacity = 0;

            stackpanel.Background = new SolidColorBrush(Colors.White);
            stackpanel2.Background = new SolidColorBrush(Colors.White);


            tb_cityname.FontFamily = new FontFamily("SimHei");
            tb_cityname.FontWeight = FontWeights.Bold;
            tb_rank.FontFamily = new FontFamily("SimHei");
            tb_roaddis.FontFamily = new FontFamily("SimHei");
            tb_raildis.FontFamily = new FontFamily("SimHei");
            tb_airdis.FontFamily = new FontFamily("SimHei");
            tb_describe.FontFamily = new FontFamily("SimHei");
            tb_population.FontFamily = new FontFamily("SimHei");
            tb_landscape.FontFamily = new FontFamily("SimHei");
            tb_landscape2.FontFamily = new FontFamily("SimHei");
            tb_landscape3.FontFamily = new FontFamily("SimHei");
            bu1.FontFamily = new FontFamily("SimHei");
            bu2.FontFamily = new FontFamily("SimHei");
            bu3.FontFamily = new FontFamily("SimHei");

            tb_cityname.FontSize = 24;
            tb_cityname.Foreground = new SolidColorBrush(Colors.Black);
            tb_cityname.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)255, (byte)255, (byte)0));
         //    = new SolidColorBrush(Colors.Orange);
            tb_rank.FontSize = 15;
            tb_roaddis.FontSize = 15;
            tb_raildis.FontSize = 15;
            tb_airdis.FontSize = 15;
            tb_describe.FontSize = 15;
            tb_population.FontSize = 15;
            tb_landscape.FontSize = 15;
            tb_landscape2.FontSize = 15;
            tb_landscape3.FontSize = 15;

            tb_cityname.Content = citye.city_name;
            tb_rank.Text ="规模等级";
            tb_roaddis.Text ="驾车前往所需时间：";
            tb_raildis.Text ="火车前往所需时间：";
            tb_airdis.Text ="飞机前往所需时间：";
            tb_describe.Text ="haha";
            tb_population.Text = "12";
            tb_landscape.Text = "haha";
            
            stackpanel.Children.Add(tb_cityname);
            stackpanel.Children.Add(tb_rank);
            stackpanel.Children.Add(tb_roaddis);
            stackpanel.Children.Add(tb_raildis);
            stackpanel.Children.Add(tb_airdis);
            stackpanel2.Children.Add(tb_describe);
            stackpanel2.Children.Add(tb_population);
            tb_population.Foreground = new SolidColorBrush(Colors.White);
            stackpanel2.Children.Add(tb_landscape);
            tb_landscape.Foreground = new SolidColorBrush(Colors.White);
            stackpanel2.Children.Add(tb_landscape2);
            tb_landscape2.Foreground = new SolidColorBrush(Colors.White);
            stackpanel2.Children.Add(tb_landscape3);
            tb_landscape3.Foreground = new SolidColorBrush(Colors.White);

            stackpanel2.Children.Add(bu1);
            stackpanel2.Children.Add(bu2);
            stackpanel2.Children.Add(bu3);

       //     bu1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)118, (byte)180));
       /*    LinearGradientBrush lbru=new LinearGradientBrush();
            lbru.StartPoint=new Point(0,0);
            GradientStop gs=new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)0, (byte)141, (byte)166);
            gs.Offset = 0;
            lbru.GradientStops.Add(gs);
            gs=new GradientStop();
            gs.Color = Color.FromArgb((byte)255, (byte)0, (byte)141, (byte)166);
            gs.Offset = 1;
            lbru.GradientStops.Add(gs);*/

            bu1.Background = new SolidColorBrush(Colors.Blue);
            bu1.Foreground = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)0));
            bu1.FontSize = 13;
            stackpanel2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)145, (byte)109));
         
            
            canvas.Children.Add(stackpanel);
            canvas.Children.Add(stackpanel2);
            stackpanel.RenderTransform = new TranslateTransform();
            stackpanel2.RenderTransform = new TranslateTransform();


            tb_cityname.Click += new RoutedEventHandler(tb_cityname_Click);
            bu3.Click += new RoutedEventHandler(bu3_Click);
        }

        void bu3_Click(object sender, RoutedEventArgs e)
        {
            HtmlWindow html = HtmlPage.Window;
           // html.Navigate(new Uri("http://www.baidu.com/s?wd="+citye.city_name, UriKind.RelativeOrAbsolute));
        }

        void tb_cityname_Click(object sender, RoutedEventArgs e)
        {
            stackpanel2.Opacity = 1;
            dax = new DoubleAnimation();
            day = new DoubleAnimation();
            da1 = new DoubleAnimation();
            sb1 = new Storyboard();
            dax.From = 1;
            day.From = 1;
            da1.From = 0;
            dax.To = 180;
            day.To = stackpanel2.ActualHeight;
            da1.To = 1;
            dax.Duration = TimeSpan.FromSeconds(0.6);
            day.Duration = TimeSpan.FromSeconds(0.6);
            da1.Duration = TimeSpan.FromSeconds(0.5);
            ElasticEase be = new ElasticEase();
            be.Oscillations = 1;
            dax.EasingFunction = be;


            day.EasingFunction = be;
            Storyboard.SetTarget(dax, stackpanel2);
            Storyboard.SetTargetProperty(dax, new PropertyPath(StackPanel.WidthProperty));
            Storyboard.SetTarget(day, stackpanel2);
            Storyboard.SetTargetProperty(day, new PropertyPath(StackPanel.HeightProperty));
            Storyboard.SetTarget(da1, stackpanel2);
            Storyboard.SetTargetProperty(da1, new PropertyPath(StackPanel.OpacityProperty));
            sb1.Children.Add(dax);
            sb1.Children.Add(day);
            sb1.Children.Add(da1);
            sb1.Begin();
        }
        public void  update_message(City_E ce)
        {
            citye=ce;
            tb_cityname.Content = citye.city_name;
            tb_rank.Text = "规模等级"+citye.rank.ToString();
            tb_roaddis.Text = "驾车前往所需时间："+citye.distance_road+"小时";
            tb_raildis.Text = "火车前往所需时间：" + citye.distance_railway + "小时";
            tb_airdis.Text = "飞机前往所需时间：" + citye.distance_air + "小时";
            tb_describe.Text = citye.describe;
            tb_population.Text = citye.population.ToString() + "万";
            tb_landscape.Text = citye.landscape1;
            tb_landscape2.Text = citye.landscape2;
            tb_landscape3.Text = citye.landscape3;

            bu1.Content = "查找酒店信息";
            bu2.Content = "小吃信息";
            bu3.Content = "感兴趣！";

           // stackpanel.MaxWidth = 1;
           // stackpanel.MaxHeight = 1;
         
        }
        public void show(double targetx, double targety)
        {

            stackpanel.RenderTransform.SetValue(TranslateTransform.XProperty, targetx);
            stackpanel.RenderTransform.SetValue(TranslateTransform.YProperty, targety);
            stackpanel2.RenderTransform.SetValue(TranslateTransform.XProperty, targetx+stackpanel.ActualWidth+2);
            stackpanel2.RenderTransform.SetValue(TranslateTransform.YProperty, targety-30);
            stackpanel.Opacity =1;
            stackpanel2.Opacity = 0;
          //  stackpanel.Background.Opacity = 1;
            isShowing = true;

            dax = new DoubleAnimation();
            day = new DoubleAnimation();
            da1 = new DoubleAnimation();            
            sb1 = new Storyboard();
            dax.From = 1;
            day.From = 1;
            da1.From = 0;
            dax.To =180;
            day.To = stackpanel.ActualHeight;
            da1.To = 1;
            dax.Duration = TimeSpan.FromSeconds(0.6);
            day.Duration = TimeSpan.FromSeconds(0.6);
            da1.Duration = TimeSpan.FromSeconds(0.5);
            ElasticEase be = new ElasticEase();
            be.Oscillations = 1;
            dax.EasingFunction = be;
          

            day.EasingFunction = be;
            Storyboard.SetTarget(dax, stackpanel);
            Storyboard.SetTargetProperty(dax, new PropertyPath(StackPanel.WidthProperty));
            Storyboard.SetTarget(day, stackpanel);
            Storyboard.SetTargetProperty(day, new PropertyPath(StackPanel.HeightProperty));
            Storyboard.SetTarget(da1, stackpanel);
            Storyboard.SetTargetProperty(da1, new PropertyPath(StackPanel.OpacityProperty));
            sb1.Children.Add(dax);
            sb1.Children.Add(day);
            sb1.Children.Add(da1);
            sb1.Begin();
            }
        public void disappear()
        {
            stackpanel.Opacity = 0;
            isShowing = false;
            stackpanel2.Opacity = 0;

        }
    }
}
