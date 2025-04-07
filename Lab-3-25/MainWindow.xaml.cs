using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Windows.Threading;

namespace Lab_3_25
{
    public partial class MainWindow : Window
    {
        Random rnd;
        DispatcherTimer timer;
        int sizeX = 50;
        int sizeY = 200;
        bool canTouch = false;

        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();
            

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(800);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(grid.Children.Count ==32) 
            {
                timer.Stop();
                MessageBox.Show("You lost!");
                canTouch=false;
                
            }
            else if (grid.Children.Count==0&&canTouch)
            {
                timer.Stop();
                MessageBox.Show("You win!");
            }
            else
            {
                CreateRectangles(1);
            }

            if (grid.Children.Count==5&&!canTouch)
            {
                canTouch=true;

            }
            
            
        }

        private void CreateRectangles(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = sizeX;
                rect.Height = sizeY;

                int w = sizeX;
                sizeX = sizeY;
                sizeY=w;

                int x = rnd.Next((int)(this.Width-2*rect.Width));
                int y = rnd.Next((int)(this.Height-2*rect.Height));

                rect.Margin = new Thickness(x, y, 0, 0);
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.VerticalAlignment = VerticalAlignment.Top;

                byte r = (byte)rnd.Next(255);
                byte g = (byte)rnd.Next(255);
                byte b = (byte)rnd.Next(255);
                rect.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));

                rect.Stroke = Brushes.Black;
                rect.MouseDown += Rect_MouseDown;
                grid.Children.Add(rect);
            }
        }

        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            // Если прямоугольник НЕ перекрыт другими
            // Для проверки пересечения используем:
            Rect r = new Rect(rect.Margin.Left, rect.Margin.Top, rect.Width, rect.Height);

            bool over=true; //=r.IntersectsWith(r); // !!
            int iRect = grid.Children.IndexOf(rect);
            for (int i = iRect+1; i<grid.Children.Count; i++)
            {
                Rectangle temp = (Rectangle)grid.Children[i];
                Rect h = new Rect(temp.Margin.Left, temp.Margin.Top, temp.Width, temp.Height);
                if (r.IntersectsWith(h))
                {
                    over = false;
                    break;
                }
                
                
            }

            if (!canTouch) {
                return;
            }
            else if (over)
            {
                grid.Children.Remove(rect);
            }  
            
        }
    }
}
