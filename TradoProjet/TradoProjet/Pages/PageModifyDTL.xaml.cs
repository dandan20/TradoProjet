using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageModifyDTL : ContentPage
	{
		public PageModifyDTL (TradoÉchange tradoÉchange, string courriel)
		{
			InitializeComponent ();
		}
	}
}