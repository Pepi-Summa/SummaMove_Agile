using System.Windows;

namespace SummaMove
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();

            this.Close();
        }
    }
}