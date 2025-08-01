using LL.MDE.Components.Qvt.Common.DataModels;
using LL.MDE.Components.Qvt.Common.Services;
using MDD4All.FileAccess.Contracts;
using MDD4All.FileAccess.WPF;
using MDD4All.UI.BlazorComponents.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MDD4All.QVT.TransformationStarter.WpfBlazor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceCollection _services = new ServiceCollection();

        public MainWindow()
        {
            InitializeComponent();

            
            _services.AddWpfBlazorWebView();
            _services.AddBlazorWebViewDeveloperTools();

            _services.AddLocalization(options =>
            {

                options.ResourcesPath = "Resources";
            });

            _services.AddSingleton<DragDropDataProvider>();
            _services.AddSingleton<IFileLoader, WpfFileLoader>();
            _services.AddSingleton<IFileSaver, WpfFileSaver>();
            _services.AddSingleton<TransformationDescriptorProvider>();

            Resources.Add("services", _services.BuildServiceProvider());
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            ITransformationDescriptor descriptor = DataContext as ITransformationDescriptor;
            if (descriptor != null)
            {
                TransformationDescriptorProvider provider = blazorWebView.Services.GetService<TransformationDescriptorProvider>();
                
                provider.TransformationDescriptor = descriptor;
            }
        }
    }
}
