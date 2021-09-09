using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui3_gui {
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window {
        public MainWindow() {
            this.InitializeComponent();
        }
        /*
        private void Control2_Checked(object sender, RoutedEventArgs e) {
            checkSetting.Content = "I am checked";
        }

        private void Control2_Unchecked(object sender, RoutedEventArgs e) {
            checkSetting.Content = "I am unchecked";
        }

        // XAML
        <CheckBox x:Name="checkSetting" Grid.Row="0" Grid.Column="0" Content="Crawl subfolders"
                Checked="Control2_Checked"
                Unchecked="Control2_Unchecked" />
        */
    }
}
