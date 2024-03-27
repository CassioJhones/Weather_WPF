using MahApps.Metro.SimpleChildWindow;
using System.Windows;
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
            TitleHorizontalAlignment = HorizontalAlignment.Center;
            TitleVerticalAlignment = VerticalAlignment.Center;
            CloseByEscape = true;
        }

        private void confirmar_click(object sender, RoutedEventArgs e) => this.Close();       
    }
}
