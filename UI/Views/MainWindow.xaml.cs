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
using Model;
using UI.Controls;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScrollViewer _content { get; set; }

        string _address = @"https://newlms.magtu.ru/";

        public MainWindow()
        {
            InitializeComponent();

            _content = ContentMain;

            PresentNews();
        }

        public void PresentNews()
        {

            var task = Task.Run(() => ParserHtml.ParseNews(_address));
            task.Wait();

            News[] news = task.Result;
            StackPanel newsPanel = new StackPanel() {Orientation=Orientation.Vertical};
            for (int i = 0; i < news.Length; i++)
            {
                NewsPost post = new NewsPost(news[i].Title, news[i].Author, news[i].Content.ToString());
                newsPanel.Children.Add(post);
            }
            _content.Content = newsPanel;
        }

        private void DragPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
