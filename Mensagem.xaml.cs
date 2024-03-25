using MahApps.Metro.SimpleChildWindow;
using System.Windows.Media;

namespace TimeVersion
{
    /// <summary>
    ///JANELA FILHA = NOTIFICACAO
    /// </summary>
    public partial class Mensagem : ChildWindow
    {

        public Mensagem()
        {
            InitializeComponent();
            AllowMove = true;
            ShowTitleBar = true;
            TitleBarBackground = Brushes.IndianRed;
            TitleForeground = Brushes.White;
            NonActiveGlowBrush = Brushes.IndianRed;
            TitleBarNonActiveBackground = Brushes.IndianRed;
            TitleHorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            TitleVerticalAlignment = System.Windows.VerticalAlignment.Center;
            CloseByEscape = true;

        }

        private void confirmar_click(object sender, System.Windows.RoutedEventArgs e) => this.Close();

        private void cancelar_click(object sender, System.Windows.RoutedEventArgs e) => this.Close();
    }
}
